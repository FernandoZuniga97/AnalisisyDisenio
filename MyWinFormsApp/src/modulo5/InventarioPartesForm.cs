using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using MyWinFormsApp.Database;

namespace MyWinFormsApp
{
    public class InventarioPartesForm : Form
    {
        // UI
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private Panel headerPanel;
        private Panel separatorLine;
        private DataGridView dgvInventario;
        private Panel contentPanel;
        private Panel contenedorReporte;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnImprimir;

        // Paginación
        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPagina;
        private int paginaActual = 1;
        private int registrosPorPagina = 20;
        private int totalPaginas = 1;

        // Cache de datos (para paginar fácilmente)
        private List<ParteDb> lista = new List<ParteDb>();

        // Tabla DB
        private const string TableName = "[dbo].[InventarioP]";

        public InventarioPartesForm()
        {
            Text = "Administración General - Inventario de Partes";
            Width = 1250;
            Height = 700;
            BackColor = Color.FromArgb(242, 242, 242);
            TopLevel = false;
            Dock = DockStyle.Fill;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;

            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Padding = new Padding(8),
                AutoScroll = true
            };

            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = ColorTranslator.FromHtml("#002060")
            };

            contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 15)
            };

            separatorLine = new Panel()
            {
                Height = 50,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Padding = new Padding(10)
            };

            // Botones
            btnAgregar = new Button() { Text = "Agregar Parte", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 112, 192), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnEliminar = new Button() { Text = "Eliminar Parte", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 84, 153), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnEditar = new Button() { Text = "Editar Parte", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 128, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnImprimir = new Button() { Text = "Imprimir", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 65, 130), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

            btnAgregar.Click += BtnAgregar_Click;
            btnEliminar.Click += BtnEliminar_Click;
            btnEditar.Click += BtnEditar_Click;
            btnImprimir.Click += BtnImprimir_Click;

            FlowLayoutPanel flp = new FlowLayoutPanel()
            {
                Dock = DockStyle.Right,
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                WrapContents = false
            };
            flp.Controls.AddRange(new Control[] { btnAgregar, btnEliminar, btnEditar, btnImprimir });
            separatorLine.Controls.Add(flp);

            // DataGridView (estilo conservado del original)
            dgvInventario = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 35,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                ScrollBars = ScrollBars.Vertical,
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                RowTemplate = { MinimumHeight = 35 }
            };

            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvInventario.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");

            dgvInventario.RowPrePaint += (s, e) =>
            {
                if (e.RowIndex % 2 == 0)
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                else
                    dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
            };

            Panel panelTabla = new Panel() { Dock = DockStyle.Fill };
            panelTabla.Controls.Add(dgvInventario);

            Panel panelPaginacion = new Panel() { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.White };
            btnPaginaAnterior = new Button() { Text = "<", Width = 35, Height = 25, Left = 10, Top = 5 }; btnPaginaAnterior.Click += BtnPaginaAnterior_Click;
            btnPaginaSiguiente = new Button() { Text = ">", Width = 35, Height = 25, Left = 60, Top = 5 }; btnPaginaSiguiente.Click += BtnPaginaSiguiente_Click;
            lblPagina = new Label() { Text = "Pag. 1 de 1", AutoSize = true, Left = 120, Top = 10 };
            panelPaginacion.Controls.AddRange(new Control[] { btnPaginaAnterior, btnPaginaSiguiente, lblPagina });

            // Title panel and logo (kept from original)
            TableLayoutPanel titlePanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            titlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            PictureBox logo = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Left,
                Margin = new Padding(25, 0, 0, 0)
            };

            try
            {
                string[] possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src","Login", "Image", "logo_g.jpg"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "src","Login", "Image", "logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "Image", "logo_g.jpg")
                };

                string foundPath = possiblePaths.FirstOrDefault(File.Exists);
                if (foundPath != null)
                {
                    using (var stream = new FileStream(foundPath, FileMode.Open, FileAccess.Read))
                    {
                        logo.Image = Image.FromStream(stream);
                    }
                }
            }
            catch { /* non-fatal */ }

            titlePanel.Controls.Add(logo, 0, 0);

            Panel textoPanel = new Panel() { Dock = DockStyle.Fill };

            lblTitulo = new Label()
            {
                Text = "IMPORTACIONES GICELL",
                Font = new Font("Century Gothic", 30, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 60
            };

            lblSubtitulo = new Label()
            {
                Text = "Inventario De Partes",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = $"Fecha: {DateTime.Now:dd/MM/yyyy}",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };

            textoPanel.Controls.Add(lblFecha);
            textoPanel.Controls.Add(lblSubtitulo);
            textoPanel.Controls.Add(lblTitulo);

            titlePanel.Controls.Add(textoPanel, 1, 0);
            headerPanel.Controls.Add(titlePanel);

            // assemble
            contentPanel.Controls.Add(panelTabla);
            contentPanel.Controls.Add(separatorLine);
            contentPanel.Controls.Add(panelPaginacion);

            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            Load += InventarioPartesForm_Load;
        }

        private void InventarioPartesForm_Load(object sender, EventArgs e)
        {
            CargarDesdeBD();
            MostrarPagina();
        }

        // -------------------------
        // Cargar desde DB (cache)
        // -------------------------
        private void CargarDesdeBD()
        {
            lista.Clear();
            string sql = $@"SELECT Id, Codigo, NombreParte, Categoria, Proveedor, Cantidad, Unidad, CostoUnitario, Observaciones, ValorTotal
                            FROM {TableName}
                            ORDER BY Id ASC";
            try
            {
                using (SqlConnection conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            var p = new ParteDb
                            {
                                Id = r.GetInt32(0),
                                Codigo = r.IsDBNull(1) ? "" : r.GetString(1),
                                NombreParte = r.IsDBNull(2) ? "" : r.GetString(2),
                                Categoria = r.IsDBNull(3) ? "" : r.GetString(3),
                                Proveedor = r.IsDBNull(4) ? "" : r.GetString(4),
                                Cantidad = r.IsDBNull(5) ? 0 : r.GetInt32(5),
                                Unidad = r.IsDBNull(6) ? "" : r.GetString(6),
                                CostoUnitario = r.IsDBNull(7) ? 0m : r.GetDecimal(7),
                                Observaciones = r.IsDBNull(8) ? "" : r.GetString(8),
                                ValorTotal = r.IsDBNull(9) ? 0m : r.GetDecimal(9)
                            };
                            lista.Add(p);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando inventario: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            totalPaginas = (int)Math.Ceiling((double)lista.Count / registrosPorPagina);
            if (totalPaginas == 0) totalPaginas = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;
        }

        // -------------------------
        // Mostrar (sin Id visible)
        // -------------------------
        private void MostrarPagina()
        {
            totalPaginas = (int)Math.Ceiling((double)lista.Count / registrosPorPagina);
            if (paginaActual < 1) paginaActual = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            var registros = lista
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .Select(x => new
                {
                    x.Codigo,            // mostramos Código (P-001...)
                    x.NombreParte,
                    x.Categoria,
                    x.Proveedor,
                    x.Cantidad,
                    x.Unidad,
                    CostoUnitario = x.CostoUnitario,
                    ValorTotal = x.ValorTotal,
                    x.Observaciones,
                    _HiddenId = x.Id    // mantenemos id en el datasource pero nombre no es "Id"
                })
                .ToList();

            dgvInventario.DataSource = null;
            dgvInventario.DataSource = registros;
            dgvInventario.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;

            // Aplicar nombres de encabezado y estilos, mantener el id oculto y no mostrar "Id"
            if (dgvInventario.Columns.Count > 0)
            {
                // Aseguramos que la columna con Id interno quede oculta (nombre: _HiddenId)
                if (dgvInventario.Columns.Contains("_HiddenId"))
                    dgvInventario.Columns["_HiddenId"].Visible = false;

                dgvInventario.Columns["Codigo"].HeaderText = "Código";
                dgvInventario.Columns["NombreParte"].HeaderText = "Nombre de la Parte";
                dgvInventario.Columns["Categoria"].HeaderText = "Categoría";
                dgvInventario.Columns["Proveedor"].HeaderText = "Proveedor";
                dgvInventario.Columns["Cantidad"].HeaderText = "Cantidad";
                dgvInventario.Columns["Unidad"].HeaderText = "Unidad";
                dgvInventario.Columns["CostoUnitario"].HeaderText = "Costo Unitario";
                dgvInventario.Columns["ValorTotal"].HeaderText = "Valor Total";
                dgvInventario.Columns["Observaciones"].HeaderText = "Observaciones";

                dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.Format = "'L' #,##0.00";
                dgvInventario.Columns["ValorTotal"].DefaultCellStyle.Format = "'L' #,##0.00";
                dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvInventario.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            // Anchos fijos (copiado estilo original)
            var widths = new Dictionary<string, int>
            {
                ["Codigo"] = 100,
                ["NombreParte"] = 300,
                ["Categoria"] = 140,
                ["Proveedor"] = 160,
                ["Cantidad"] = 110,
                ["Unidad"] = 90,
                ["CostoUnitario"] = 120,
                ["ValorTotal"] = 120,
                ["Observaciones"] = 240
            };

            int totalWidth = 0;
            foreach (DataGridViewColumn col in dgvInventario.Columns)
            {
                if (widths.TryGetValue(col.Name, out int w))
                {
                    col.Width = w;
                    totalWidth += w;
                }
                else
                {
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    totalWidth += col.Width;
                }
            }

            // Alternado de filas ya aplicado en RowPrePaint; reforzarlo
            for (int i = 0; i < dgvInventario.Rows.Count; i++)
            {
                dgvInventario.Rows[i].DefaultCellStyle.BackColor = (i % 2 == 1) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White;
            }

            // Línea amarilla bajo encabezados (usa solo ancho visible)
            int tablaVisibleWidth = dgvInventario.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Sum(c => c.Width);
            Panel lineaAmarilla = new Panel()
            {
                BackColor = Color.Gold,
                Height = 3,
                Width = tablaVisibleWidth,
                Left = dgvInventario.Left,
                Top = dgvInventario.Top + dgvInventario.ColumnHeadersHeight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var existente = dgvInventario.Parent.Controls.OfType<Panel>().FirstOrDefault(p => p.BackColor == Color.Gold);
            if (existente != null) dgvInventario.Parent.Controls.Remove(existente);
            dgvInventario.Parent.Controls.Add(lineaAmarilla);
            lineaAmarilla.BringToFront();
            dgvInventario.SizeChanged += (s, e) =>
            {
                lineaAmarilla.Top = dgvInventario.Top + dgvInventario.ColumnHeadersHeight;
            };

            contenedorReporte.AutoScrollMinSize = new Size(Math.Max(totalWidth + 40, this.Width), 0);

            lblPagina.Text = $"Pag. {paginaActual} de {totalPaginas}";
            btnPaginaAnterior.Enabled = paginaActual > 1;
            btnPaginaSiguiente.Enabled = paginaActual < totalPaginas;
        }

        private void BtnPaginaAnterior_Click(object sender, EventArgs e)
        {
            if (paginaActual > 1) { paginaActual--; MostrarPagina(); }
        }

        private void BtnPaginaSiguiente_Click(object sender, EventArgs e)
        {
            if (paginaActual < totalPaginas) { paginaActual++; MostrarPagina(); }
        }

        // -------------------------
        // Impresión (oculta Id)
        // -------------------------
        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage += PrintDoc_PrintPage;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 1000,
                Height = 800
            };
            preview.ShowDialog();
        }

        private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Bitmap headerBitmap = new Bitmap(headerPanel.Width, headerPanel.Height);
            headerPanel.DrawToBitmap(headerBitmap, new Rectangle(0, 0, headerPanel.Width, headerPanel.Height));
            Bitmap separatorBitmap = new Bitmap(separatorLine.Width, separatorLine.Height);
            separatorLine.DrawToBitmap(separatorBitmap, new Rectangle(0, 0, separatorLine.Width, separatorLine.Height));

            int tablaWidth = dgvInventario.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Sum(c => c.Width);
            int tablaHeight = dgvInventario.ColumnHeadersHeight + dgvInventario.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            Bitmap dgvBitmap = new Bitmap(Math.Max(1, tablaWidth), Math.Max(1, tablaHeight));

            using (Graphics g = Graphics.FromImage(dgvBitmap))
            {
                g.Clear(Color.White);
                int xPos = 0;

                // Encabezados (solo visibles)
                for (int i = 0; i < dgvInventario.Columns.Count; i++)
                {
                    var col = dgvInventario.Columns[i];
                    if (!col.Visible) continue;

                    Rectangle headerRect = new Rectangle(xPos, 0, col.Width, dgvInventario.ColumnHeadersHeight);
                    using (Brush backBrush = new SolidBrush(dgvInventario.ColumnHeadersDefaultCellStyle.BackColor))
                        g.FillRectangle(backBrush, headerRect);
                    using (Brush foreBrush = new SolidBrush(dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor))
                        g.DrawString(col.HeaderText, dgvInventario.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

                    g.DrawRectangle(Pens.Black, headerRect);
                    xPos += col.Width;
                }

                // Línea amarilla
                g.FillRectangle(Brushes.Gold, 0, dgvInventario.ColumnHeadersHeight, tablaWidth, 3);

                int yPos = dgvInventario.ColumnHeadersHeight + 3;

                // Filas
                foreach (DataGridViewRow row in dgvInventario.Rows)
                {
                    xPos = 0;
                    int filaIndex = dgvInventario.Rows.IndexOf(row);

                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        var col = dgvInventario.Columns[i];
                        if (!col.Visible) continue;

                        var cell = row.Cells[i];
                        Rectangle cellRect = new Rectangle(xPos, yPos, col.Width, row.Height);
                        using (Brush backBrush = new SolidBrush(filaIndex % 2 == 1 ? ColorTranslator.FromHtml("#F0F0F0") : Color.White))
                            g.FillRectangle(backBrush, cellRect);

                        using (Brush foreBrush = new SolidBrush(cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor))
                        {
                            string valor = cell.FormattedValue?.ToString() ?? "";
                            if (col.Name == "CostoUnitario" || col.Name == "ValorTotal")
                            {
                                g.DrawString(valor, cell.InheritedStyle.Font, foreBrush, cellRect, new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center });
                            }
                            else
                            {
                                g.DrawString(valor, cell.InheritedStyle.Font, foreBrush, cellRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                            }
                        }

                        g.DrawRectangle(Pens.White, cellRect);
                        xPos += col.Width;
                    }

                    yPos += row.Height;
                }
            }

            int totalWidth = Math.Max(headerBitmap.Width, dgvBitmap.Width);
            int totalHeight = headerBitmap.Height + separatorBitmap.Height + dgvBitmap.Height + 30;

            Bitmap printBitmap = new Bitmap(totalWidth, totalHeight);
            using (Graphics g = Graphics.FromImage(printBitmap))
            {
                g.Clear(Color.White);
                g.DrawImage(headerBitmap, 0, 0);
                g.DrawImage(separatorBitmap, 0, headerBitmap.Height);
                g.DrawImage(dgvBitmap, 0, headerBitmap.Height + separatorBitmap.Height);
            }

            float scale = Math.Min(
                (float)e.MarginBounds.Width / printBitmap.Width,
                (float)e.MarginBounds.Height / printBitmap.Height
            );

            int printWidth = (int)(printBitmap.Width * scale);
            int printHeight = (int)(printBitmap.Height * scale);

            e.Graphics.DrawImage(printBitmap, e.MarginBounds.Left, e.MarginBounds.Top, printWidth, printHeight);

            // Número de página
            string pageText = $"Pag. {paginaActual} de {totalPaginas}";
            using (Font pageFont = new Font("Segoe UI", 9))
            {
                SizeF textSize = e.Graphics.MeasureString(pageText, pageFont);
                float x = e.MarginBounds.Right - textSize.Width;
                float y = e.MarginBounds.Top + (headerBitmap.Height + separatorBitmap.Height + dgvBitmap.Height) * scale + 5;
                e.Graphics.DrawString(pageText, pageFont, Brushes.Black, x, y);
            }
        }

        // -------------------------
        // CRUD
        // -------------------------
        private void BtnAgregar_Click(object sender, EventArgs e) => MostrarFormularioAgregarEditar(null);

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null) return;

            int rowIndex = dgvInventario.CurrentRow.Index;
            int listaIndex = (paginaActual - 1) * registrosPorPagina + rowIndex;
            if (listaIndex < 0 || listaIndex >= lista.Count) return;

            var parte = lista[listaIndex];
            MostrarFormularioAgregarEditar(parte);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null) return;

            int rowIndex = dgvInventario.CurrentRow.Index;
            int listaIndex = (paginaActual - 1) * registrosPorPagina + rowIndex;
            if (listaIndex < 0 || listaIndex >= lista.Count) return;

            var parte = lista[listaIndex];
            var res = MessageBox.Show($"¿Eliminar la parte '{parte.Codigo} - {parte.NombreParte}'?", "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (res != DialogResult.Yes) return;

            string sql = $"DELETE FROM {TableName} WHERE Id = @Id";
            try
            {
                using (SqlConnection conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.Parameters.AddWithValue("@Id", parte.Id);
                        int rows = cmd.ExecuteNonQuery();
                        if (rows > 0)
                        {
                            MessageBox.Show("Registro eliminado.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            CargarDesdeBD();
                            MostrarPagina();
                        }
                        else
                        {
                            MessageBox.Show("No se pudo eliminar el registro.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error eliminando: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // Mostrar formulario de Agregar o Editar (comboboxes recreados cada vez)
        private void MostrarFormularioAgregarEditar(ParteDb parte)
        {
            bool esEditar = parte != null;

            Form form = new Form()
            {
                Width = 420,
                Height = 560,
                Text = esEditar ? "Editar Parte" : "Agregar Nueva Parte",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            Panel panel = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            form.Controls.Add(panel);

            int labelWidth = 130;
            int controlWidth = 220;
            int top = 20;
            int gap = 45;

            // Nombre
            Label lblNombre = new Label() { Text = "Nombre", Left = 20, Top = top, Width = labelWidth };
            TextBox txtNombre = new TextBox() { Left = 160, Top = top, Width = controlWidth, Text = esEditar ? parte.NombreParte : "" };
            top += gap;

            // Categoría (ComboBox) - siempre crear nuevo combobox y llenar
            Label lblCategoria = new Label() { Text = "Categoría", Left = 20, Top = top, Width = labelWidth };
            ComboBox cbCategoria = new ComboBox() { Left = 160, Top = top, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            var categorias = ObtenerValores("Categoria", new List<string>
            {
               "Pantallas", "Conectores", "Baterías", "Insumos", "Accesorios", "Otros"
            });
            cbCategoria.Items.Clear();
            if (categorias.Any()) cbCategoria.Items.AddRange(categorias.ToArray());
            else cbCategoria.Items.AddRange(new string[] { "Pantallas", "Conectores", "Baterías", "Insumos", "Accesorios", "Otros" });
            if (esEditar && !string.IsNullOrWhiteSpace(parte.Categoria))
            {
                if (!cbCategoria.Items.Contains(parte.Categoria))
                    cbCategoria.Items.Insert(0, parte.Categoria);
                cbCategoria.SelectedItem = parte.Categoria;
            }
            top += gap;

            // Proveedor (ComboBox)
            Label lblProveedor = new Label() { Text = "Proveedor", Left = 20, Top = top, Width = labelWidth };
            ComboBox cbProveedor = new ComboBox() { Left = 160, Top = top, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            var proveedores = ObtenerValores("Proveedor", new List<string>
                {
                    "G-Tech Supply", "ElectroParts", "MobilePro", "Tecnocell", "TechWorld"
                });

            cbProveedor.Items.Clear();
            if (proveedores.Any()) cbProveedor.Items.AddRange(proveedores.ToArray());
            else cbProveedor.Items.AddRange(new string[] { "G-Tech Supply", "ElectroParts", "MobilePro", "Tecnocell", "TechWorld" });
            if (esEditar && !string.IsNullOrWhiteSpace(parte.Proveedor))
            {
                if (!cbProveedor.Items.Contains(parte.Proveedor))
                    cbProveedor.Items.Insert(0, parte.Proveedor);
                cbProveedor.SelectedItem = parte.Proveedor;
            }
            top += gap;

            // Cantidad
            Label lblCantidad = new Label() { Text = "Cantidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCantidad = new TextBox() { Left = 160, Top = top, Width = controlWidth, Text = esEditar ? parte.Cantidad.ToString() : "0" };
            top += gap;

            // Unidad (ComboBox)
            Label lblUnidad = new Label() { Text = "Unidad", Left = 20, Top = top, Width = labelWidth };
            ComboBox cbUnidad = new ComboBox() { Left = 160, Top = top, Width = controlWidth, DropDownStyle = ComboBoxStyle.DropDownList };
            var unidades = ObtenerValores("Unidad", new List<string>
            {
                "unidades", "tubos", "cajas"
            });

            cbUnidad.Items.Clear();
            if (unidades.Any()) cbUnidad.Items.AddRange(unidades.ToArray());
            else cbUnidad.Items.AddRange(new string[] { "unidades", "tubos", "cajas" });
            if (esEditar && !string.IsNullOrWhiteSpace(parte.Unidad))
            {
                if (!cbUnidad.Items.Contains(parte.Unidad))
                    cbUnidad.Items.Insert(0, parte.Unidad);
                cbUnidad.SelectedItem = parte.Unidad;
            }
            top += gap;

            // Costo unitario
            Label lblCosto = new Label() { Text = "Costo Unitario (L.)", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCosto = new TextBox() { Left = 160, Top = top, Width = controlWidth, Text = esEditar ? parte.CostoUnitario.ToString("F2") : "0.00" };
            top += gap;

            // Observaciones
            Label lblObservaciones = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObservaciones = new TextBox() { Left = 160, Top = top, Width = controlWidth, Text = esEditar ? parte.Observaciones : "" };
            top += gap;

            // Guardar
            Button btnGuardar = new Button() { Text = "Guardar", Left = 160, Top = top + 10, Width = 100 };
            btnGuardar.Click += (s, e) =>
            {
                // Validaciones
                if (string.IsNullOrWhiteSpace(txtNombre.Text) || txtNombre.Text.Length < 3)
                {
                    MessageBox.Show("Nombre obligatorio (>=3 caracteres).", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbCategoria.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una categoría.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbProveedor.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione un proveedor.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!int.TryParse(txtCantidad.Text, out int cantidad) || cantidad < 0)
                {
                    MessageBox.Show("Cantidad debe ser entero >= 0.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cbUnidad.SelectedItem == null)
                {
                    MessageBox.Show("Seleccione una unidad.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtCosto.Text, out decimal costo) || costo < 0m)
                {
                    MessageBox.Show("Costo unitario inválido.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (txtObservaciones.Text.Length > 300)
                {
                    MessageBox.Show("Observaciones debe tener máximo 300 caracteres.", "Validación", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // Guardar en BD
                if (!esEditar)
                {
                    string sqlInsert = $@"INSERT INTO {TableName}
                                            (Codigo, NombreParte, Categoria, Proveedor, Cantidad, Unidad, CostoUnitario, Observaciones)
                                          VALUES
                                            (@Codigo, @NombreParte, @Categoria, @Proveedor, @Cantidad, @Unidad, @CostoUnitario, @Observaciones);
                                          SELECT SCOPE_IDENTITY();";
                    try
                    {
                        int newId = -1;
                        using (SqlConnection conn = DbConfig.GetConnection())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlInsert, conn))
                            {
                                cmd.Parameters.AddWithValue("@Codigo", "TEMP");
                                cmd.Parameters.AddWithValue("@NombreParte", txtNombre.Text.Trim());
                                cmd.Parameters.AddWithValue("@Categoria", cbCategoria.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Proveedor", cbProveedor.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                cmd.Parameters.AddWithValue("@Unidad", cbUnidad.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@CostoUnitario", costo);
                                cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(txtObservaciones.Text) ? (object)DBNull.Value : txtObservaciones.Text.Trim());

                                object o = cmd.ExecuteScalar();
                                if (o != null && o != DBNull.Value)
                                    newId = Convert.ToInt32(o);
                            }

                            if (newId > 0)
                            {
                                string codigoGenerado = $"P-{newId:D3}"; // Opción A
                                string sqlUpdate = $"UPDATE {TableName} SET Codigo = @Codigo WHERE Id = @Id";
                                using (SqlCommand cmd2 = new SqlCommand(sqlUpdate, conn))
                                {
                                    cmd2.Parameters.AddWithValue("@Codigo", codigoGenerado);
                                    cmd2.Parameters.AddWithValue("@Id", newId);
                                    cmd2.ExecuteNonQuery();
                                }

                                MessageBox.Show("Parte agregada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                CargarDesdeBD();
                                MostrarPagina();
                                form.Close();
                            }
                            else
                            {
                                MessageBox.Show("No se pudo obtener el Id de la inserción.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al insertar: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    string sqlUpdate = $@"UPDATE {TableName}
                                          SET NombreParte = @NombreParte,
                                              Categoria = @Categoria,
                                              Proveedor = @Proveedor,
                                              Cantidad = @Cantidad,
                                              Unidad = @Unidad,
                                              CostoUnitario = @CostoUnitario,
                                              Observaciones = @Observaciones
                                          WHERE Id = @Id";
                    try
                    {
                        using (SqlConnection conn = DbConfig.GetConnection())
                        {
                            conn.Open();
                            using (SqlCommand cmd = new SqlCommand(sqlUpdate, conn))
                            {
                                cmd.Parameters.AddWithValue("@NombreParte", txtNombre.Text.Trim());
                                cmd.Parameters.AddWithValue("@Categoria", cbCategoria.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Proveedor", cbProveedor.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@Cantidad", cantidad);
                                cmd.Parameters.AddWithValue("@Unidad", cbUnidad.SelectedItem.ToString());
                                cmd.Parameters.AddWithValue("@CostoUnitario", costo);
                                cmd.Parameters.AddWithValue("@Observaciones", string.IsNullOrWhiteSpace(txtObservaciones.Text) ? (object)DBNull.Value : txtObservaciones.Text.Trim());
                                cmd.Parameters.AddWithValue("@Id", parte.Id);

                                int rows = cmd.ExecuteNonQuery();
                                if (rows > 0)
                                {
                                    MessageBox.Show("Parte actualizada correctamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    CargarDesdeBD();
                                    MostrarPagina();
                                    form.Close();
                                }
                                else
                                {
                                    MessageBox.Show("No se actualizó ningún registro.", "Precaución", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Error al actualizar: {ex.Message}", "Error de BD", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }; // fin btnGuardar

            panel.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre,
                lblCategoria, cbCategoria,
                lblProveedor, cbProveedor,
                lblCantidad, txtCantidad,
                lblUnidad, cbUnidad,
                lblCosto, txtCosto,
                lblObservaciones, txtObservaciones,
                btnGuardar
            });

            form.ShowDialog();
        }

        // Obtener valores distintos para poblar comboboxes
        private List<string> ObtenerValores(string columna, List<string> valoresBase)
        {
            var items = new HashSet<string>(StringComparer.OrdinalIgnoreCase);

            // Agregar valores base INDEPENDIENTES de la BD
            foreach (var item in valoresBase)
                items.Add(item);

            // Ahora agregar valores de la BD
            string sql = $@"SELECT DISTINCT [{columna}] 
                    FROM {TableName} 
                    WHERE [{columna}] IS NOT NULL 
                    AND LTRIM(RTRIM([{columna}])) <> '' 
                    ORDER BY [{columna}]";

            try
            {
                using (SqlConnection conn = DbConfig.GetConnection())
                {
                    conn.Open();
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    using (SqlDataReader r = cmd.ExecuteReader())
                    {
                        while (r.Read())
                        {
                            items.Add(r.GetString(0));
                        }
                    }
                }
            }
            catch
            {
                // No romper si falla la BD
            }

            return items.OrderBy(x => x).ToList();
        }

        // Clase que mapea la tabla (Id permanece interno, no mostrado)
        private class ParteDb
        {
            public int Id { get; set; }
            public string Codigo { get; set; }
            public string NombreParte { get; set; }
            public string Categoria { get; set; }
            public string Proveedor { get; set; }
            public int Cantidad { get; set; }
            public string Unidad { get; set; }
            public decimal CostoUnitario { get; set; }
            public string Observaciones { get; set; }
            public decimal ValorTotal { get; set; }
        }
    }
}
