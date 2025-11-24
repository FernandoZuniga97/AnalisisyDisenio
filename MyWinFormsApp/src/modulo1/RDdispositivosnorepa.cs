using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Text.RegularExpressions;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Data.SqlClient;
using System.Data;
using MyWinFormsApp.Database;

namespace MyWinFormsApp
{
    public class RDdispositivosnorepaForm : Form
    {
        private const string TableName = "[dbo].[DispositivosNoReparados]";
        //--------------------------
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private Panel headerPanel;
        private Panel separatorLine;
        private DataGridView dgDnRepa;
        private Panel contentPanel;
        private Panel contenedorReporte;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnImprimir; // Botón de imprimir

        // Paginación
        private Button btnPaginaAnterior;
        private Button btnPaginaSiguiente;
        private Label lblPagina;
        private int paginaActual = 1;
        private int registrosPorPagina = 20; // 20 registros por página
        private int totalPaginas = 1;

        // **USAR SOLO listaDispositivos para la persistencia**
        private List<Norepa> listaDispositivos;
        private int contadorID = 4; // Variable que ya no se utiliza al depender de la DB

        public RDdispositivosnorepaForm()
        {
            Text = "Dispositivos No Reparados";
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
                AutoScroll = true,
                Padding = new Padding(8)
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
                Padding = new Padding(0, 15, 0, 15),
            };

            separatorLine = new Panel()
            {
                Height = 3,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 15, 0, 15)
            };
            // los botones arriba
            FlowLayoutPanel panelBotonesAccion = new FlowLayoutPanel()
            {
                Dock = DockStyle.Top, // Colocar en la parte superior del contentPanel
                Height = 40, // Altura ajustada
                BackColor = Color.White,
                Padding = new Padding(10, 5, 10, 5), // Relleno interno
                FlowDirection = FlowDirection.RightToLeft, // Alinear de izquierda a derecha
                WrapContents = false // Asegurar que no se envuelvan a la siguiente línea
            };

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
                else
                {
                    MessageBox.Show("Logo image not found. The application will continue without it.", "Warning",
                        MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            titlePanel.Controls.Add(logo, 0, 0);

            Panel textoPanel = new Panel()
            {
                Dock = DockStyle.Fill
            };

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
                Text = "Dispositivos No Reparados",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = "Periodo: (27/08/2025) -(27/09/2025)",
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

            // --- DataGridView ---
            dgDnRepa = new DataGridView()
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
                ScrollBars = ScrollBars.Vertical, // Cambiado a Vertical para paginación
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.Single,
                RowTemplate = { MinimumHeight = 35 }
            };

            // Estilos del DataGridView
            dgDnRepa.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgDnRepa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgDnRepa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgDnRepa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDnRepa.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");

            dgDnRepa.RowPrePaint += (s, ev) =>
            {
                if (ev.RowIndex % 2 == 0) // filas pares
                    dgDnRepa.Rows[ev.RowIndex].DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");
                else
                    dgDnRepa.Rows[ev.RowIndex].DefaultCellStyle.BackColor = Color.White;
            };
            Panel panelTabla = new Panel() { Dock = DockStyle.Fill };
            panelTabla.Controls.Add(dgDnRepa);
            var buttonBaseStyle = new Font("Segoe UI", 9, FontStyle.Bold);
            var buttonWidth = 120;
            var buttonHeight = 30;
            var buttonMargin = new Padding(0, 0, 10, 0);
            // --- Botones de Acción ---
            btnAgregar = new Button()
            {
                Text = "Agregar",
                Width = buttonWidth, // Ancho reducido
                Height = buttonHeight, // Altura ajustada
                BackColor = Color.FromArgb(0, 112, 192),
                ForeColor = Color.White,
                Font = buttonBaseStyle,
                Margin = buttonMargin
            };
            btnAgregar.Click += BtnAgregar_Click;

            btnEliminar = new Button()
            {
                Text = "Eliminar",
                Width = buttonWidth,
                Height = buttonHeight,
                BackColor = Color.FromArgb(0, 84, 153),
                ForeColor = Color.White,
                Font = buttonBaseStyle,
                Margin = buttonMargin // Aplicar margen uniforme
            };
            btnEliminar.Click += BtnEliminar_Click;

            btnEditar = new Button()
            {
                Text = "Editar",
                Width = buttonWidth,
                Height = buttonHeight,
                BackColor = Color.FromArgb(0, 128, 255),
                ForeColor = Color.White,
                Font = buttonBaseStyle,
                Margin = buttonMargin
            };
            btnEditar.Click += BtnEditar_Click;

            btnImprimir = new Button()
            {
                Text = "Imprimir",
                Width = buttonWidth,
                Height = buttonHeight,
                BackColor = Color.FromArgb(0, 65, 130),
                ForeColor = Color.White,
                Font = buttonBaseStyle,
                Margin = new Padding(0)
            };
            btnImprimir.Click += BtnImprimir_Click;

            // --- Panel de Paginación ---
            Panel panelPaginacion = new Panel() { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.White };
            btnPaginaAnterior = new Button() { Text = "<", Width = 35, Height = 25, Left = 10, Top = 5 };
            btnPaginaAnterior.Click += BtnPaginaAnterior_Click;
            btnPaginaSiguiente = new Button() { Text = ">", Width = 35, Height = 25, Left = 60, Top = 5 };
            btnPaginaSiguiente.Click += BtnPaginaSiguiente_Click;
            lblPagina = new Label() { Text = "Pag. 1 de 1", AutoSize = true, Left = 120, Top = 10 };
            panelPaginacion.Controls.AddRange(new Control[] { btnPaginaAnterior, btnPaginaSiguiente, lblPagina });


            // Agregar los botones al FlowLayoutPanel
            panelBotonesAccion.Controls.Add(btnImprimir);
            panelBotonesAccion.Controls.Add(btnEditar);
            panelBotonesAccion.Controls.Add(btnEliminar);
            panelBotonesAccion.Controls.Add(btnAgregar);
            // Agregar controles al contentPanel
            contentPanel.Controls.Add(panelPaginacion);
            contentPanel.Controls.Add(panelTabla);
            contentPanel.Controls.Add(panelBotonesAccion);
            contentPanel.Controls.Add(separatorLine);

            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            Load += DnRForm_Load;
        }

        private void DnRForm_Load(object sender, EventArgs e)
        {
            CargarDispositivosDesdeDB();
            MostrarPagina();
        }
        // LÓGICA DE CONEXIÓN Y DATOS
        // Método para ejecutar comandos SQL que no devuelven datos (INSERT, UPDATE, DELETE)
        private int ExecuteNonQuery(string sql, List<SqlParameter> parameters = null)
        {
            int rowsAffected = 0;
            try
            {
                using (SqlConnection connection = DbConfig.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(sql, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }
                        rowsAffected = command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la operación: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return rowsAffected;
        }
        //-------------------------------------
        // --- NUEVA FUNCIÓN: Ejecuta INSERT y devuelve el ID generado (SCOPE_IDENTITY) ---
        private int ExecuteInsertAndGetId(string sqlInsert, List<SqlParameter> parameters)
        {
            try
            {
                using (SqlConnection connection = DbConfig.GetConnection())
                {
                    connection.Open();
                    // La consulta debe ser: INSERT ...; SELECT SCOPE_IDENTITY();
                    using (SqlCommand command = new SqlCommand(sqlInsert, connection))
                    {
                        if (parameters != null)
                        {
                            command.Parameters.AddRange(parameters.ToArray());
                        }
                        object result = command.ExecuteScalar();
                        if (result != null && result != DBNull.Value)
                        {
                            // SCOPE_IDENTITY retorna un tipo numérico/decimal, se convierte a int
                            return Convert.ToInt32(result);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al ejecutar la inserción y obtener ID: {ex.Message}", "Error de Base de Datos", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            return -1; // Indica fallo
        }
        // Nuevo método para cargar datos desde la DB
        private void CargarDispositivosDesdeDB()
        {
            listaDispositivos = new List<Norepa>();
            // Se selecciona Id como INT y se asume que Codigo (indice 1) se omite en el mapeo a Norepa.
            string query = $"SELECT Id, Codigo, FechaIngreso, Dispositivo, Cliente, TecnicoAsignado, DescripcionDanio, CostoEstimado, Observaciones FROM {TableName} ORDER BY Id ASC";
            try
            {
                using (SqlConnection connection = DbConfig.GetConnection())
                {
                    connection.Open();
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                listaDispositivos.Add(new Norepa
                                {
                                    ID = reader.GetInt32(0),
                                    CodigoDisplay = reader.GetString(1),
                                    FechadeIngreso = reader.GetDateTime(2).ToString("dd/MM/yyyy"),
                                    Dispositivo = reader.GetString(3),
                                    Cliente = reader.GetString(4),
                                    TecnicoAsignado = reader.GetString(5),
                                    DescripciondelDano = reader.GetString(6),
                                    CostoEstimado = reader.IsDBNull(7) ? 0m : reader.GetDecimal(7),
                                    Observaciones = reader.IsDBNull(8) ? "-" : reader.GetString(8)
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar datos: {ex.Message}\nVerifica la cadena de conexión y que la DB/Tabla existan.", "Error de Carga", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            // Recalcular total de páginas después de cargar la lista
            totalPaginas = (int)Math.Ceiling((double)listaDispositivos.Count / registrosPorPagina);
            if (totalPaginas == 0) totalPaginas = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;
        }
        // LÓGICA DE INTERFAZ DE USUARIO (Actualizada para usar la DB)
        private void MostrarPagina()
        {
            // **CORRECCIÓN 2: Uso consistente de listaDispositivos**
            totalPaginas = (int)Math.Ceiling((double)listaDispositivos.Count / registrosPorPagina);
            if (paginaActual < 1) paginaActual = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            var registros = listaDispositivos
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            dgDnRepa.DataSource = null;
            dgDnRepa.DataSource = registros;
            dgDnRepa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // Nombres de encabezado
            if (dgDnRepa.Columns.Count > 0)
            {
                // 1. Ocultar la columna de ID numérico (de la DB)
                dgDnRepa.Columns["ID"].Visible = false;
                // 2. Mostrar y nombrar la columna de CÓDIGO NR-XXX
                dgDnRepa.Columns["CodigoDisplay"].Visible = true; // Aseguramos que sea visible
                dgDnRepa.Columns["CodigoDisplay"].DisplayIndex = 0; // Colocamos el código como primera columna
                dgDnRepa.Columns["CodigoDisplay"].HeaderText = "Código";
                dgDnRepa.Columns["FechadeIngreso"].HeaderText = "Fecha de Ingreso";
                dgDnRepa.Columns["Dispositivo"].HeaderText = "Dispositivo";
                dgDnRepa.Columns["Cliente"].HeaderText = "Cliente";
                dgDnRepa.Columns["TecnicoAsignado"].HeaderText = "Técnico Asignado";
                dgDnRepa.Columns["DescripciondelDano"].HeaderText = "Descripción del Daño";
                dgDnRepa.Columns["CostoEstimado"].HeaderText = "Costo Estimado";
                dgDnRepa.Columns["Observaciones"].HeaderText = "Observaciones";
                // Formato de moneda
                dgDnRepa.Columns["CostoEstimado"].DefaultCellStyle.Format = "'L' #,##0.00";
                dgDnRepa.Columns["CostoEstimado"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgDnRepa.Columns["Observaciones"].DefaultCellStyle.WrapMode = DataGridViewTriState.True;

            }

            // Aplicar estilos de fila alternada (Ajustado para el índice visible)
            for (int i = 0; i < dgDnRepa.Rows.Count; i++)
            {
                dgDnRepa.Rows[i].DefaultCellStyle.BackColor = (i % 2 == 1) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White;
            }
            var widths = new Dictionary<string, int>
            {
                ["ID"] = 80,
                ["CodigoDisplay"] = 100,
                ["FechadeIngreso"] = 180,
                ["Dispositivo"] = 140,
                ["Cliente"] = 160,
                ["TecnicoAsignado"] = 140,
                ["DescripciondelDano"] = 280,
                ["CostoEstimado"] = 120,
                ["Observaciones"] = 200
            };

            int totalWidth = 0;
            foreach (DataGridViewColumn col in dgDnRepa.Columns)
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

            // Asegurar desplazamiento horizontal si es necesario
            contenedorReporte.AutoScrollMinSize = new Size(Math.Max(totalWidth + 40, this.Width), 0);
            lblPagina.Text = $"Pag. {paginaActual} de {totalPaginas}";
            int tablaWidth = dgDnRepa.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
            // === Línea amarilla bajo encabezados (Reemplaza la lógica de CellPainting) ===
            Panel lineaAmarilla = new Panel()
            {
                BackColor = Color.Gold,
                Height = 3,
                Width = tablaWidth,
                Left = dgDnRepa.Left,
                Top = dgDnRepa.Top + dgDnRepa.ColumnHeadersHeight,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            var existente = dgDnRepa.Parent.Controls.OfType<Panel>().FirstOrDefault(p => p.BackColor == Color.Gold);
            if (existente != null) dgDnRepa.Parent.Controls.Remove(existente);
            dgDnRepa.Parent.Controls.Add(lineaAmarilla);
            lineaAmarilla.BringToFront();
            dgDnRepa.SizeChanged += (s, e) =>
            {
                // lineaAmarilla.Width = tablaWidth;
                lineaAmarilla.Top = dgDnRepa.Top + dgDnRepa.ColumnHeadersHeight;
            };

            // Habilitar/Deshabilitar botones de paginación
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
            // --- Captura header ---
            Bitmap headerBitmap = new Bitmap(headerPanel.Width, headerPanel.Height);
            headerPanel.DrawToBitmap(headerBitmap, new Rectangle(0, 0, headerPanel.Width, headerPanel.Height));

            Bitmap separatorBitmap = new Bitmap(separatorLine.Width, separatorLine.Height);
            separatorLine.DrawToBitmap(separatorBitmap, new Rectangle(0, 0, separatorLine.Width, separatorLine.Height));

            // --- Captura DataGridView (Solo la página actual) ---
            int tablaWidth = dgDnRepa.Columns.Cast<DataGridViewColumn>().Where(c => c.Visible).Sum(c => c.Width);
            int tablaHeight = dgDnRepa.ColumnHeadersHeight + dgDnRepa.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            Bitmap dgvBitmap = new Bitmap(tablaWidth, tablaHeight);
            using (Graphics g = Graphics.FromImage(dgvBitmap))
            {
                g.Clear(Color.White);
                int xPos = 0;

                // Cabecera
                for (int i = 0; i < dgDnRepa.Columns.Count; i++)
                {
                    var col = dgDnRepa.Columns[i];
                    if (!col.Visible)
                        continue;
                    Rectangle headerRect = new Rectangle(xPos, 0, col.Width, dgDnRepa.ColumnHeadersHeight);
                    using (Brush backBrush = new SolidBrush(dgDnRepa.ColumnHeadersDefaultCellStyle.BackColor))
                        g.FillRectangle(backBrush, headerRect);
                    using (Brush foreBrush = new SolidBrush(dgDnRepa.ColumnHeadersDefaultCellStyle.ForeColor))
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString(col.HeaderText, dgDnRepa.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, sf);
                    }
                    g.DrawRectangle(Pens.Black, headerRect);
                    xPos += col.Width;
                }

                // --- Línea amarilla debajo de encabezado ---
                g.FillRectangle(Brushes.Gold, 0, dgDnRepa.ColumnHeadersHeight, tablaWidth, 3);

                int yPos = dgDnRepa.ColumnHeadersHeight + 3; // Ajusta por línea amarilla
                foreach (DataGridViewRow row in dgDnRepa.Rows)
                {
                    xPos = 0;
                    int filaIndex = dgDnRepa.Rows.IndexOf(row);
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        var col = dgDnRepa.Columns[i];
                        if (!col.Visible)
                            continue;
                        var cell = row.Cells[i];
                        Rectangle cellRect = new Rectangle(xPos, yPos, cell.OwningColumn.Width, row.Height);
                        // Usar el color de fila alternado
                        Color backColor = filaIndex % 2 == 1 ? ColorTranslator.FromHtml("#F0F0F0") : Color.White;
                        using (Brush backBrush = new SolidBrush(backColor))
                            g.FillRectangle(backBrush, cellRect);

                        using (Brush foreBrush = new SolidBrush(cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor))
                        {
                            StringFormat sf = new StringFormat { Alignment = DataGridViewContentAlignment.MiddleCenter.ToString().Contains("Right") ? StringAlignment.Far : StringAlignment.Center, LineAlignment = StringAlignment.Center };

                            // Formatear Costo Estimado
                            string valor = cell.FormattedValue?.ToString();
                            if (cell.OwningColumn.Name == "CostoEstimado")
                            {
                                valor = ((decimal)cell.Value).ToString("'L' #,##0.00", CultureInfo.InvariantCulture);
                                sf.Alignment = StringAlignment.Far; // Alinear a la derecha para moneda
                            }
                            else if (cell.Value != null)
                            {
                                valor = cell.Value.ToString();
                            }
                            g.DrawString(valor, cell.InheritedStyle.Font, foreBrush, cellRect, sf);
                        }
                        using (Pen whitePen = new Pen(Color.White))
                            g.DrawRectangle(whitePen, cellRect);
                        xPos += cell.OwningColumn.Width;
                    }
                    yPos += row.Height;
                }
            }

            // Captura de etiqueta de paginación
            Label lblPaginaPrint = new Label()
            {
                Text = $"Pag. {paginaActual} de {totalPaginas}",
                Width = dgvBitmap.Width,
                Height = 25,
                TextAlign = ContentAlignment.MiddleCenter
            };
            Bitmap paginaBitmap = new Bitmap(lblPaginaPrint.Width, lblPaginaPrint.Height);
            lblPaginaPrint.DrawToBitmap(paginaBitmap, new Rectangle(0, 0, lblPaginaPrint.Width, lblPaginaPrint.Height));

            int totalWidth = Math.Max(headerBitmap.Width, dgvBitmap.Width);
            int totalHeight = headerBitmap.Height + separatorBitmap.Height + paginaBitmap.Height + dgvBitmap.Height;

            Bitmap printBitmap = new Bitmap(totalWidth, totalHeight);
            using (Graphics g = Graphics.FromImage(printBitmap))
            {
                g.Clear(Color.White);
                g.DrawImage(headerBitmap, 0, 0);
                g.DrawImage(separatorBitmap, 0, headerBitmap.Height);
                g.DrawImage(paginaBitmap, 0, headerBitmap.Height + separatorBitmap.Height);
                g.DrawImage(dgvBitmap, 0, headerBitmap.Height + separatorBitmap.Height + paginaBitmap.Height);
            }

            // Escalar la imagen al área de impresión
            float scale = Math.Min(
                (float)e.MarginBounds.Width / printBitmap.Width,
                (float)e.MarginBounds.Height / printBitmap.Height
            );

            int printWidth = (int)(printBitmap.Width * scale);
            int printHeight = (int)(printBitmap.Height * scale);

            e.Graphics.DrawImage(printBitmap, e.MarginBounds.Left, e.MarginBounds.Top, printWidth, printHeight);
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Form formAgregar = new Form()
            {
                Width = 400,
                Height = 450,
                Text = "Agregar Nuevo Dispositivo",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            Panel panel = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            formAgregar.Controls.Add(panel);

            int labelWidth = 120;
            int textBoxWidth = 200;
            int top = 20;
            int gap = 40;

            Label lblFechadeIngreso = new Label() { Text = "Fecha de ingreso", Left = 20, Top = top, Width = labelWidth };
            TextBox txtFechadeIngreso = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = DateTime.Now.ToString("dd/MM/yyyy") };
            top += gap;

            Label lblDispositivo = new Label() { Text = "Dispositivo", Left = 20, Top = top, Width = labelWidth };
            TextBox txtDispositivo = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblCliente = new Label() { Text = "Cliente", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCliente = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblTecnicoAsignado = new Label() { Text = "Tecnico Asignado", Left = 20, Top = top, Width = labelWidth };
            TextBox txtTecnicoAsignado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblDescripciondelDano = new Label() { Text = "Descripción del Daño", Left = 20, Top = top, Width = labelWidth };
            TextBox txtDescripciondelDano = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblCostoEstimado = new Label() { Text = "Costo Estimado(L.)", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCostoEstimado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Button btnGuardar = new Button() { Text = "Guardar", Left = 150, Width = 100, Top = top };
            btnGuardar.Click += (s2, e2) =>
            {
                if (string.IsNullOrWhiteSpace(txtFechadeIngreso.Text) ||
                    string.IsNullOrWhiteSpace(txtDispositivo.Text) ||
                    string.IsNullOrWhiteSpace(txtCliente.Text) ||
                    string.IsNullOrWhiteSpace(txtTecnicoAsignado.Text) ||
                    string.IsNullOrWhiteSpace(txtDescripciondelDano.Text) ||
                    string.IsNullOrWhiteSpace(txtCostoEstimado.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios excepto Observaciones.", "Datos Incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (!DateTime.TryParseExact(txtFechadeIngreso.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                {
                    MessageBox.Show("El formato de fecha debe ser dd/MM/yyyy.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (!decimal.TryParse(txtCostoEstimado.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costo))
                {
                    MessageBox.Show("El Costo Estimado debe ser un valor numérico válido (ej. 120.50).", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- LÓGICA DE INSERCIÓN EN LA BASE DE DATOS ---
                string sql = $@"INSERT INTO {TableName} 
                                (Codigo, FechaIngreso, Dispositivo, Cliente, TecnicoAsignado, DescripcionDanio, CostoEstimado, Observaciones)
                                VALUES 
                                (@Codigo, @FechaIngreso, @Dispositivo, @Cliente, @TecnicoAsignado, @DescripcionDanio, @CostoEstimado, @Observaciones)";
                //--------------
                // 1. Insertar el registro con un código temporal y obtener el ID.
                string sqlInsert = $@"INSERT INTO {TableName} 
                                    (Codigo, FechaIngreso, Dispositivo, Cliente, TecnicoAsignado, DescripcionDanio, CostoEstimado, Observaciones)
                                    VALUES 
                                    ('TEMP', @FechaIngreso, @Dispositivo, @Cliente, @TecnicoAsignado, @DescripcionDanio, @CostoEstimado, @Observaciones);
                                    SELECT SCOPE_IDENTITY();";

                List<SqlParameter> insertParameters = new List<SqlParameter>
                {
                    new SqlParameter("@FechaIngreso", fecha.ToString("yyyy-MM-dd")), // Formato ISO para SQL DATE
                    new SqlParameter("@Dispositivo", txtDispositivo.Text),
                    new SqlParameter("@Cliente", txtCliente.Text),
                    new SqlParameter("@TecnicoAsignado", txtTecnicoAsignado.Text),
                    new SqlParameter("@DescripcionDanio", txtDescripciondelDano.Text),
                    new SqlParameter("@CostoEstimado", costo),
                    new SqlParameter("@Observaciones", string.IsNullOrWhiteSpace(txtObs.Text) ? (object)DBNull.Value : txtObs.Text)
                };
                int newId = ExecuteInsertAndGetId(sqlInsert, insertParameters);
                if (newId > 0)
                {
                    // 2. Construir el código secuencial NR-001, NR-002, etc.
                    string codigoGenerado = $"NR-{newId:D3}"; // D3 asegura 3 dígitos (001, 010, 100)

                    // 3. Actualizar el registro para colocar el código generado.
                    string sqlUpdate = $"UPDATE {TableName} SET Codigo = @Codigo WHERE Id = @Id";
                    List<SqlParameter> parameters = new List<SqlParameter>
                {
                    new SqlParameter("@Codigo", codigoGenerado),
                        new SqlParameter("@Id", newId),
                    new SqlParameter("@FechaIngreso", fecha.ToString("yyyy-MM-dd")), // Formato ISO para SQL DATE
                    new SqlParameter("@Dispositivo", txtDispositivo.Text),
                    new SqlParameter("@Cliente", txtCliente.Text),
                    new SqlParameter("@TecnicoAsignado", txtTecnicoAsignado.Text),
                    new SqlParameter("@DescripcionDanio", txtDescripciondelDano.Text),
                    new SqlParameter("@CostoEstimado", costo),
                    new SqlParameter("@Observaciones", string.IsNullOrWhiteSpace(txtObs.Text) ? (object)DBNull.Value : txtObs.Text)
                };
                    if (ExecuteNonQuery(sqlUpdate, new List<SqlParameter>
                {
                    new SqlParameter("@Codigo", codigoGenerado),
                    new SqlParameter("@Id", newId)
                    }) > 0)
                    {
                        MessageBox.Show("Registro agregado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDispositivosDesdeDB();
                        MostrarPagina();
                        formAgregar.Close();
                    }
                }
            }; // Cierre correcto del handler Click
            panel.Controls.AddRange(new Control[] {
                lblFechadeIngreso, txtFechadeIngreso, lblDispositivo, txtDispositivo, lblCliente, txtCliente,
                lblTecnicoAsignado, txtTecnicoAsignado, lblDescripciondelDano, txtDescripciondelDano, lblCostoEstimado, txtCostoEstimado,
                lblObs, txtObs, btnGuardar
            });
            formAgregar.ShowDialog();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgDnRepa.CurrentRow != null)
            {
                // Obtener el ID de la fila seleccionada
                var registroVisible = dgDnRepa.CurrentRow.DataBoundItem as Norepa;
                if (registroVisible == null) return;

                if (MessageBox.Show($"¿Está seguro de eliminar el registro {registroVisible.CodigoDisplay}?", "Confirmar Eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    // --- LÓGICA DE ELIMINACIÓN EN LA BASE DE DATOS ---
                    string sql = $"DELETE FROM {TableName} WHERE Id = @Id";
                    List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        // El ID se pasa como INT
                        new SqlParameter("@Id", registroVisible.ID)
                    };

                    if (ExecuteNonQuery(sql, parameters) > 0)
                    {
                        MessageBox.Show("Registro eliminado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        CargarDispositivosDesdeDB(); // Recargar datos
                        MostrarPagina();
                    }
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgDnRepa.CurrentRow == null) return;

            // Obtener el objeto Norepa de la fila seleccionada
            var norepa = dgDnRepa.CurrentRow.DataBoundItem as Norepa;
            if (norepa == null) return;

            Form formEditar = new Form()
            {
                Width = 400,
                Height = 450,
                Text = "Editar Dispositivo",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            Panel panel = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            formEditar.Controls.Add(panel);

            int labelWidth = 120;
            int textBoxWidth = 200;
            int top = 20;
            int gap = 40;

            Label lblFechadeIngreso = new Label() { Text = "Fecha de ingreso", Left = 20, Top = top, Width = labelWidth };
            TextBox txtFechadeIngreso = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.FechadeIngreso };
            top += gap;

            Label lblDispositivo = new Label() { Text = "Dispositivo", Left = 20, Top = top, Width = labelWidth };
            TextBox txtDispositivo = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Dispositivo };
            top += gap;

            Label lblCliente = new Label() { Text = "Cliente", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCliente = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Cliente };
            top += gap;

            Label lblTecnicoAsignado = new Label() { Text = "Tecnico Asignado", Left = 20, Top = top, Width = labelWidth };
            TextBox txtTecnicoAsignado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.TecnicoAsignado };
            top += gap;

            Label lblDescripciondelDano = new Label() { Text = "Descripcion del daño", Left = 20, Top = top, Width = labelWidth };
            TextBox txtDescripciondelDano = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.DescripciondelDano };
            top += gap;

            Label lblCostoEstimado = new Label() { Text = "Costo Estimado", Left = 20, Top = top, Width = labelWidth };
            // Mostrar Costo Estimado sin formato de moneda para facilitar la edición
            TextBox txtCostoEstimado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.CostoEstimado.ToString(CultureInfo.InvariantCulture) };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Observaciones };
            top += gap;

            Button btnGuardar = new Button() { Text = "Guardar", Left = 150, Width = 100, Top = top };
            btnGuardar.Click += (s2, e2) =>
            {
                // Validación y conversión de datos (igual que en el original)
                if (!DateTime.TryParseExact(txtFechadeIngreso.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
                {
                    MessageBox.Show("El formato de fecha debe ser dd/MM/yyyy.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                // Usar NumberStyles.Number con CultureInfo.InvariantCulture para manejar la entrada de decimales.
                if (!decimal.TryParse(txtCostoEstimado.Text, NumberStyles.Number, CultureInfo.InvariantCulture, out decimal costo))
                {
                    MessageBox.Show("El Costo Estimado debe ser un valor numérico válido.", "Error de Formato", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // --- LÓGICA DE ACTUALIZACIÓN EN LA BASE DE DATOS ---
                string sql = $@"UPDATE {TableName} SET 
                                    FechaIngreso = @FechaIngreso, 
                                    Dispositivo = @Dispositivo, 
                                    Cliente = @Cliente, 
                                    TecnicoAsignado = @TecnicoAsignado, 
                                    DescripcionDanio = @DescripcionDanio, 
                                    CostoEstimado = @CostoEstimado, 
                                    Observaciones = @Observaciones 
                                    WHERE Id = @Id";

                List<SqlParameter> parameters = new List<SqlParameter>
                    {
                        new SqlParameter("@FechaIngreso", fecha.ToString("yyyy-MM-dd")), // Formato ISO
                        new SqlParameter("@Dispositivo", txtDispositivo.Text),
                        new SqlParameter("@Cliente", txtCliente.Text),
                        new SqlParameter("@TecnicoAsignado", txtTecnicoAsignado.Text),
                        new SqlParameter("@DescripcionDanio", txtDescripciondelDano.Text), // Mapeo a DescripcionDanio
                        new SqlParameter("@CostoEstimado", costo),
                        new SqlParameter("@Observaciones", string.IsNullOrWhiteSpace(txtObs.Text) ? (object)DBNull.Value : txtObs.Text),
                        new SqlParameter("@Id", norepa.ID) // ID para la cláusula WHERE
                    };

                if (ExecuteNonQuery(sql, parameters) > 0)
                {
                    // Si la actualización es exitosa, actualiza el objeto local y recarga.
                    MessageBox.Show("Registro editado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    CargarDispositivosDesdeDB();
                    MostrarPagina();
                    formEditar.Close();
                }
            };

            panel.Controls.AddRange(new Control[] {
                lblFechadeIngreso, txtFechadeIngreso, lblDispositivo, txtDispositivo, lblCliente, txtCliente,
                lblTecnicoAsignado, txtTecnicoAsignado, lblDescripciondelDano, txtDescripciondelDano, lblCostoEstimado, txtCostoEstimado,
                lblObs, txtObs, btnGuardar });

            formEditar.ShowDialog();
        }


    }

    public class Norepa
    {
        public int ID { get; set; } // ID de la DB
        public string CodigoDisplay { get; set; }
        public string FechadeIngreso { get; set; }
        public string Dispositivo { get; set; }
        public string Cliente { get; set; }
        public string TecnicoAsignado { get; set; }
        public string DescripciondelDano { get; set; }
        public decimal CostoEstimado { get; set; }
        public string Observaciones { get; set; }
    }
}