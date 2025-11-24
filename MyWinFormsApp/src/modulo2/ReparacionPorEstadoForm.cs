using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MyWinFormsApp.src.modulo2
{
    public class ReparacionPorEstadoForm : Form
    {
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private ComboBox cmbEstado;
        private Panel headerPanel;
        private DataGridView dgReparaciones;
        private Panel contentPanel;
        private Panel contenedorReporte;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnEditar;
        private Button btnImprimir;
        private List<Reparacion> lista;
        private int contadorID = 18;

        private PrintDocument printDocument;
        private PrintPreviewDialog printPreviewDialog;
        private int currentRowIndex;
        private int pageNumber;

        public ReparacionPorEstadoForm()
        {
            Text = "Reporte de Reparaciones por Estado";
            Width = 1300;
            Height = 750;
            BackColor = Color.FromArgb(242, 242, 242);
            TopLevel = false;
            Dock = DockStyle.Fill;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;

            // 🔹 CONTENEDOR PRINCIPAL
            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Padding = new Padding(8)
            };

            // 🔹 ENCABEZADO
            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = ColorTranslator.FromHtml("#002060")
            };

            // 🔹 CONTENIDO
            contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 15),
                AutoScroll = true
            };

            // ---------------------------
            // LOGO + TÍTULOS
            // ---------------------------
            TableLayoutPanel titlePanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 150F));
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));

            PictureBox logo = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 120,
                Height = 120,
                Dock = DockStyle.Left,
                Margin = new Padding(25, 0, 0, 0)
            };

            try
            {
                string[] possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src","Login","Image","logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "src","Login","Image","logo_g.jpg"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image","logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "Image","logo_g.jpg")
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
            catch { }

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
                Text = "Reparaciones Por Estado",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = $"Fecha: ({DateTime.Now:dd/MM/yyyy})",
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

            // ---------------------------
            // DATA GRID VIEW
            // ---------------------------
            dgReparaciones = new DataGridView()
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
                ScrollBars = ScrollBars.Both,
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical
            };

            dgReparaciones.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgReparaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgReparaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgReparaciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgReparaciones.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            dgReparaciones.DefaultCellStyle.SelectionBackColor = Color.LightGray;

            dgReparaciones.Paint += (s, e) =>
            {
                int y = dgReparaciones.ColumnHeadersHeight;
                e.Graphics.FillRectangle(
                    new SolidBrush(ColorTranslator.FromHtml("#FFD966")),
                    0, y, dgReparaciones.Width, 4
                );
            };

 // --------------------------
// Panel blanco superior con botones y ComboBox
// --------------------------
Panel separatorLine = new Panel()
{
    Height = 50,
    BackColor = Color.White,
    Dock = DockStyle.Top,
    Padding = new Padding(10)
};

// FlowLayoutPanel izquierdo para el ComboBox
FlowLayoutPanel flpIzquierda = new FlowLayoutPanel()
{
    Dock = DockStyle.Left,
    FlowDirection = FlowDirection.LeftToRight,
    AutoSize = true,
    WrapContents = false
};

// ComboBox de estado
cmbEstado = new ComboBox()
{
    Width = 150,
    Height = 35, // altura más grande
    DropDownStyle = ComboBoxStyle.DropDownList,
    Font = new Font("Segoe UI", 10, FontStyle.Bold),
    ForeColor = Color.Black,
    BackColor = Color.White
};
cmbEstado.Items.AddRange(new string[] { "Todos", "En proceso", "Finalizada", "Retrasada" });
cmbEstado.SelectedIndex = 0;
cmbEstado.SelectedIndexChanged += (s, e) => FiltrarPorEstado();

flpIzquierda.Controls.Add(cmbEstado);

// FlowLayoutPanel derecho para los botones
FlowLayoutPanel flpDerecha = new FlowLayoutPanel()
{
    Dock = DockStyle.Right,
    FlowDirection = FlowDirection.LeftToRight,
    AutoSize = true,
    WrapContents = false
};

// Botones
btnAgregar = new Button() { Text = "Agregar Reparación", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 112, 192), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
btnEditar = new Button() { Text = "Editar Reparación", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 128, 255), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
btnEliminar = new Button() { Text = "Eliminar Reparación", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 84, 153), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
btnImprimir = new Button() { Text = "Imprimir", Width = 120, Height = 30, BackColor = Color.FromArgb(0, 65, 130), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };

// Eventos
btnAgregar.Click += BtnAgregar_Click;
btnEditar.Click += BtnEditar_Click;
btnEliminar.Click += BtnEliminar_Click;
btnImprimir.Click += BtnImprimir_Click;

// Agregar botones al FlowLayoutPanel derecho
flpDerecha.Controls.Add(btnAgregar);
flpDerecha.Controls.Add(btnEditar);
flpDerecha.Controls.Add(btnEliminar);
flpDerecha.Controls.Add(btnImprimir);

// Añadir ambos FlowLayoutPanel al panel blanco
separatorLine.Controls.Add(flpIzquierda);
separatorLine.Controls.Add(flpDerecha);

// Añadir panel de botones al contentPanel
contentPanel.Controls.Add(separatorLine);


            // ---------------------------
            // ARMAR ESTRUCTURA
            // ---------------------------
            dgReparaciones.Dock = DockStyle.Fill;
            contentPanel.Controls.Add(dgReparaciones);
            contentPanel.Controls.Add(separatorLine);
            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            // 🔹 CONFIGURAR IMPRESIÓN
            printDocument = new PrintDocument();
            printDocument.PrintPage += PrintDocument_PrintPage;

            printPreviewDialog = new PrintPreviewDialog();
            printPreviewDialog.Document = printDocument;

            Load += ReparacionPorEstadoForm_Load;
        }

        private void ReparacionPorEstadoForm_Load(object sender, EventArgs e)
        {
            lista = new List<Reparacion>()
            {
                new Reparacion{ ID="R-001", FechadeIngreso="01/10/2025", Dispositivo="Huawei P30 Lite", Cliente="Ana García", TecnicoAsignado="J. Martínez", DescripciondelDano="No enciende el dispositivo", EstadoReparacion="En proceso", FechaEstimadaEntrega="02/11/2025", CostoEstimado=1200, Observaciones="Sospecha de placa dañada"},
                new Reparacion{ ID="R-002", FechadeIngreso="03/10/2025", Dispositivo="iPhone SE (2020)", Cliente="Carlos Mejía", TecnicoAsignado="C. Flores", DescripciondelDano="Botón Home sin respuesta", EstadoReparacion="Finalizada", FechaEstimadaEntrega="31/10/2025", CostoEstimado=1400, Observaciones="Touch ID funcional"},
                new Reparacion{ ID="R-003", FechadeIngreso="05/10/2025", Dispositivo="Samsung S23 Ultra", Cliente="Luis Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Revisión general y limpieza", EstadoReparacion="Retrasada", FechaEstimadaEntrega="31/10/2025", CostoEstimado=1100, Observaciones="Esperando repuesto"},
            new Reparacion{ ID="R-004", FechadeIngreso="07/10/2025", Dispositivo="Xiaomi Redmi Note 11", Cliente="Pedro López", TecnicoAsignado="L. Reyes", DescripciondelDano="Puerto de carga dañado", EstadoReparacion="En proceso", FechaEstimadaEntrega="04/11/2025", CostoEstimado=1000, Observaciones="Repuesto en stock"},
    new Reparacion{ ID="R-005", FechadeIngreso="08/10/2025", Dispositivo="iPhone 12", Cliente="María Santos", TecnicoAsignado="J. Martínez", DescripciondelDano="Pantalla rota", EstadoReparacion="En proceso", FechaEstimadaEntrega="10/11/2025", CostoEstimado=1500, Observaciones="Pantalla en camino"},
    new Reparacion{ ID="R-006", FechadeIngreso="09/10/2025", Dispositivo="Samsung A52", Cliente="Juan Pérez", TecnicoAsignado="C. Flores", DescripciondelDano="Batería no carga", EstadoReparacion="En proceso", FechaEstimadaEntrega="15/11/2025", CostoEstimado=800, Observaciones="Batería reemplazada"},
    new Reparacion{ ID="R-007", FechadeIngreso="10/10/2025", Dispositivo="Huawei Mate 40", Cliente="Ana Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Cámara trasera dañada", EstadoReparacion="En proceso", FechaEstimadaEntrega="12/11/2025", CostoEstimado=900, Observaciones="Cámara nueva pedida"},
    new Reparacion{ ID="R-008", FechadeIngreso="11/10/2025", Dispositivo="Xiaomi Mi 11", Cliente="Carlos Ramírez", TecnicoAsignado="L. Reyes", DescripciondelDano="Altavoz sin sonido", EstadoReparacion="En proceso", FechaEstimadaEntrega="18/11/2025", CostoEstimado=700, Observaciones="Altavoz de reemplazo"},
    new Reparacion{ ID="R-009", FechadeIngreso="12/10/2025", Dispositivo="Samsung S21", Cliente="Luis Hernández", TecnicoAsignado="J. Martínez", DescripciondelDano="Micrófono fallando", EstadoReparacion="En proceso", FechaEstimadaEntrega="20/11/2025", CostoEstimado=650, Observaciones="Micrófono pedido"},
    new Reparacion{ ID="R-010", FechadeIngreso="13/10/2025", Dispositivo="iPhone 13", Cliente="Pedro Gómez", TecnicoAsignado="C. Flores", DescripciondelDano="Botón lateral dañado", EstadoReparacion="En proceso", FechaEstimadaEntrega="22/11/2025", CostoEstimado=500, Observaciones="Botón de reemplazo"},
    new Reparacion{ ID="R-011", FechadeIngreso="14/10/2025", Dispositivo="Huawei P40", Cliente="Ana López", TecnicoAsignado="M. Pérez", DescripciondelDano="Pantalla táctil no responde", EstadoReparacion="En proceso", FechaEstimadaEntrega="25/11/2025", CostoEstimado=1300, Observaciones="Pantalla táctil pedida"},
    new Reparacion{ ID="R-012", FechadeIngreso="15/10/2025", Dispositivo="Samsung Note 20", Cliente="Luis García", TecnicoAsignado="L. Reyes", DescripciondelDano="Cargador interno dañado", EstadoReparacion="En proceso", FechaEstimadaEntrega="28/11/2025", CostoEstimado=850, Observaciones="Repuesto en stock"},
    new Reparacion{ ID="R-013", FechadeIngreso="16/10/2025", Dispositivo="Xiaomi Redmi 10", Cliente="María López", TecnicoAsignado="J. Martínez", DescripciondelDano="Pantalla quebrada", EstadoReparacion="En proceso", FechaEstimadaEntrega="30/11/2025", CostoEstimado=900, Observaciones="Pantalla pedida"},
    new Reparacion{ ID="R-014", FechadeIngreso="17/10/2025", Dispositivo="iPhone 11", Cliente="Juan Torres", TecnicoAsignado="C. Flores", DescripciondelDano="Batería no carga", EstadoReparacion="En proceso", FechaEstimadaEntrega="05/12/2025", CostoEstimado=750, Observaciones="Batería nueva"},
    new Reparacion{ ID="R-015", FechadeIngreso="18/10/2025", Dispositivo="Samsung A72", Cliente="Pedro Ramírez", TecnicoAsignado="M. Pérez", DescripciondelDano="Puerto de carga flojo", EstadoReparacion="En proceso", FechaEstimadaEntrega="06/12/2025", CostoEstimado=600, Observaciones="Puerto reemplazado"},
    new Reparacion{ ID="R-016", FechadeIngreso="19/10/2025", Dispositivo="Huawei P20 Pro", Cliente="Ana Gómez", TecnicoAsignado="L. Reyes", DescripciondelDano="Pantalla con manchas", EstadoReparacion="Finalizada", FechaEstimadaEntrega="26/10/2025", CostoEstimado=1200, Observaciones="Reparación completada"},
    new Reparacion{ ID="R-017", FechadeIngreso="20/10/2025", Dispositivo="iPhone XR", Cliente="Carlos Torres", TecnicoAsignado="J. Martínez", DescripciondelDano="Botón volumen dañado", EstadoReparacion="Finalizada", FechaEstimadaEntrega="27/10/2025", CostoEstimado=500, Observaciones="Botón reemplazado"},
    new Reparacion{ ID="R-018", FechadeIngreso="21/10/2025", Dispositivo="Samsung S20", Cliente="Luis Ramírez", TecnicoAsignado="C. Flores", DescripciondelDano="Cámara delantera fallando", EstadoReparacion="Finalizada", FechaEstimadaEntrega="28/10/2025", CostoEstimado=800, Observaciones="Cámara reemplazada"},
    new Reparacion{ ID="R-019", FechadeIngreso="22/10/2025", Dispositivo="Xiaomi Mi 10", Cliente="María Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Altavoz no funciona", EstadoReparacion="Finalizada", FechaEstimadaEntrega="29/10/2025", CostoEstimado=700, Observaciones="Altavoz reparado"},
    new Reparacion{ ID="R-020", FechadeIngreso="23/10/2025", Dispositivo="Huawei Mate 30", Cliente="Juan López", TecnicoAsignado="L. Reyes", DescripciondelDano="Pantalla negra intermitente", EstadoReparacion="Retrasada", FechaEstimadaEntrega="05/11/2025", CostoEstimado=1100, Observaciones="Esperando repuesto"},
    new Reparacion{ ID="R-021", FechadeIngreso="24/10/2025", Dispositivo="iPhone 12 Pro", Cliente="Pedro García", TecnicoAsignado="J. Martínez", DescripciondelDano="Micrófono dañado", EstadoReparacion="Retrasada", FechaEstimadaEntrega="06/11/2025", CostoEstimado=650, Observaciones="Micrófono en camino"}
 };

            ActualizarGrid();
        }

        private void ActualizarGrid()
        {
            dgReparaciones.DataSource = null;
            dgReparaciones.DataSource = lista;

            if (dgReparaciones.Columns.Contains("CostoEstimado"))
            {
                dgReparaciones.Columns["CostoEstimado"].DefaultCellStyle.Format = "L #,##0.00";
                dgReparaciones.Columns["CostoEstimado"].HeaderText = "Costo Estimado (L.)";
            }

            if (dgReparaciones.Columns.Contains("FechadeIngreso"))
                dgReparaciones.Columns["FechadeIngreso"].HeaderText = "Fecha de ingreso";

            if (dgReparaciones.Columns.Contains("TecnicoAsignado"))
                dgReparaciones.Columns["TecnicoAsignado"].HeaderText = "Técnico asignado";

            if (dgReparaciones.Columns.Contains("DescripciondelDano"))
                dgReparaciones.Columns["DescripciondelDano"].HeaderText = "Descripción del daño";

            if (dgReparaciones.Columns.Contains("EstadoReparacion"))
                dgReparaciones.Columns["EstadoReparacion"].HeaderText = "Estado de reparación";

            if (dgReparaciones.Columns.Contains("FechaEstimadaEntrega"))
                dgReparaciones.Columns["FechaEstimadaEntrega"].HeaderText = "Fecha estimada de entrega";
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
{
    // Calcular próximo ID según la lista actual
    int proximoID = 1;
    if (lista.Count > 0)
    {
        proximoID = lista
            .Select(r => int.Parse(r.ID.Substring(2))) // quitar "R-"
            .Max() + 1;
    }

    var form = new ReparacionDialog(null, proximoID);
    if (form.ShowDialog() == DialogResult.OK)
    {
        // Ya no necesitas contadorID++, el ID se asigna en el dialog
        lista.Add(form.ReparacionNueva);
        ActualizarGrid();
    }
}


        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgReparaciones.CurrentRow == null) return;

            var reparacion = (Reparacion)dgReparaciones.CurrentRow.DataBoundItem;
            var form = new ReparacionDialog(reparacion);
            if (form.ShowDialog() == DialogResult.OK)
            {
                int index = lista.IndexOf(reparacion);
                lista[index] = form.ReparacionNueva;
                ActualizarGrid();
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgReparaciones.CurrentRow == null) return;

            var reparacion = (Reparacion)dgReparaciones.CurrentRow.DataBoundItem;
            var result = MessageBox.Show($"¿Seguro que desea eliminar la reparación {reparacion.ID}?",
                "Confirmación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
            if (result == DialogResult.Yes)
            {
                lista.Remove(reparacion);
                ActualizarGrid();
            }
        }

        private void FiltrarPorEstado()
        {
            if (cmbEstado.SelectedIndex <= 0)
            {
                dgReparaciones.DataSource = lista;
            }
            else
            {
                string estado = cmbEstado.SelectedItem.ToString();
                dgReparaciones.DataSource = lista.Where(r => r.EstadoReparacion.Equals(estado, StringComparison.OrdinalIgnoreCase)).ToList();
            }
        }

        // 🔹 BOTÓN IMPRIMIR CLICK
        private void BtnImprimir_Click(object sender, EventArgs e)
{
    currentRowIndex = 0;
    pageNumber = 1;

    // Establecer horizontal antes de mostrar vista previa
    printDocument.DefaultPageSettings.Landscape = true;

    printPreviewDialog.Document = printDocument;
    printPreviewDialog.ShowDialog();
}


        // 🔹 IMPRESIÓN CON SALTO DE PÁGINA AUTOMÁTICO
        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
{
    printDocument.DefaultPageSettings.Landscape = true;

    // --- Captura header ---
    Bitmap headerBitmap = new Bitmap(headerPanel.Width, headerPanel.Height);
    headerPanel.DrawToBitmap(headerBitmap, new Rectangle(0, 0, headerPanel.Width, headerPanel.Height));

    // --- Franja blanca de 50px (sin botones) ---
    int franjaHeight = 50;
    Bitmap franjaBitmap = new Bitmap(dgReparaciones.Width, franjaHeight);
    using (Graphics g = Graphics.FromImage(franjaBitmap))
    {
        g.Clear(Color.White);
    }

    // --- Captura DataGridView ---
    int tablaWidth = dgReparaciones.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
    int tablaHeight = dgReparaciones.ColumnHeadersHeight + dgReparaciones.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
    Bitmap dgvBitmap = new Bitmap(tablaWidth, tablaHeight);

    using (Graphics g = Graphics.FromImage(dgvBitmap))
    {
        g.Clear(Color.White);
        int xPos = 0;

        // Cabecera
        for (int i = 0; i < dgReparaciones.Columns.Count; i++)
        {
            var col = dgReparaciones.Columns[i];
            Rectangle headerRect = new Rectangle(xPos, 0, col.Width, dgReparaciones.ColumnHeadersHeight);
            using (Brush backBrush = new SolidBrush(dgReparaciones.ColumnHeadersDefaultCellStyle.BackColor))
                g.FillRectangle(backBrush, headerRect);
            using (Brush foreBrush = new SolidBrush(dgReparaciones.ColumnHeadersDefaultCellStyle.ForeColor))
            {
                StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString(col.HeaderText, dgReparaciones.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, sf);
            }
            g.DrawRectangle(Pens.Black, headerRect);
            xPos += col.Width;
        }

        // Línea amarilla debajo de cabecera
        g.FillRectangle(Brushes.Gold, 0, dgReparaciones.ColumnHeadersHeight, tablaWidth, 3);

        // Filas
        int yPos = dgReparaciones.ColumnHeadersHeight + 3;
        foreach (DataGridViewRow row in dgReparaciones.Rows)
        {
            xPos = 0;
            int filaIndex = dgReparaciones.Rows.IndexOf(row);
            for (int i = 0; i < row.Cells.Count; i++)
            {
                var cell = row.Cells[i];
                Rectangle cellRect = new Rectangle(xPos, yPos, cell.OwningColumn.Width, row.Height);
                using (Brush backBrush = new SolidBrush(filaIndex % 2 == 1 ? Color.LightGray : Color.White))
                    g.FillRectangle(backBrush, cellRect);
                using (Brush foreBrush = new SolidBrush(cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor))
                {
                    StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                    g.DrawString(cell.FormattedValue?.ToString(), cell.InheritedStyle.Font, foreBrush, cellRect, sf);
                }
                g.DrawRectangle(Pens.White, cellRect);
                xPos += cell.OwningColumn.Width;
            }
            yPos += row.Height;

            // CORRECCIÓN: Evitar páginas infinitas
            if (yPos + row.Height > e.MarginBounds.Bottom)
            {
                e.HasMorePages = true;
                return;
            }
        }
    }

    // Combinar header + franja blanca + DataGridView
    int totalWidth = Math.Max(headerBitmap.Width, dgvBitmap.Width);
    int totalHeight = headerBitmap.Height + franjaHeight + dgvBitmap.Height;
    Bitmap printBitmap = new Bitmap(totalWidth, totalHeight);

    using (Graphics g = Graphics.FromImage(printBitmap))
    {
        g.Clear(Color.White);
        g.DrawImage(headerBitmap, 0, 0);
        g.DrawImage(franjaBitmap, 0, headerBitmap.Height);
        g.DrawImage(dgvBitmap, 0, headerBitmap.Height + franjaHeight);
    }

    // Escalar al área imprimible
    float scale = Math.Min(
        (float)e.MarginBounds.Width / printBitmap.Width,
        (float)e.MarginBounds.Height / printBitmap.Height
    );
    int printWidth = (int)(printBitmap.Width * scale);
    int printHeight = (int)(printBitmap.Height * scale);

    e.Graphics.DrawImage(printBitmap, e.MarginBounds.Left, e.MarginBounds.Top, printWidth, printHeight);

    e.HasMorePages = false;
}

    }

    public class Reparacion
    {
        public string ID { get; set; }
        public string FechadeIngreso { get; set; }
        public string Dispositivo { get; set; }
        public string Cliente { get; set; }
        public string TecnicoAsignado { get; set; }
        public string DescripciondelDano { get; set; }
        public string EstadoReparacion { get; set; }
        public string FechaEstimadaEntrega { get; set; }
        public decimal CostoEstimado { get; set; }
        public string Observaciones { get; set; }
    }

   public class ReparacionDialog : Form
{
    public Reparacion ReparacionNueva { get; private set; }

    private TextBox txtID, txtDispositivo, txtCliente, txtDescripcion, txtCosto, txtObservaciones;
    private ComboBox cmbEstado, cmbTecnico;

    public ReparacionDialog(Reparacion existente = null, int proximoID = 1)
    {
        Text = existente == null ? "Agregar Reparación" : "Editar Reparación";
        Width = 400;
        Height = 550;
        StartPosition = FormStartPosition.CenterParent;
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;

        var panel = new TableLayoutPanel() { Dock = DockStyle.Fill, RowCount = 8, ColumnCount = 2, Padding = new Padding(10) };
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 35));
        panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 65));

        string[] labels = { "ID:", "Dispositivo:", "Cliente:", "Técnico:", "Descripción:", "Estado:", "Costo (L):", "Observaciones:" };
        for (int i = 0; i < labels.Length; i++)
        {
            panel.Controls.Add(new Label() { Text = labels[i], AutoSize = true, Font = new Font("Segoe UI", 10, FontStyle.Bold) }, 0, i);
        }

        // --- Controles ---
        txtID = new TextBox() { ReadOnly = true };
        txtDispositivo = new TextBox();
        txtCliente = new TextBox();
        cmbTecnico = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
        cmbTecnico.Items.AddRange(new[] { "J. Martínez", "C. Flores", "M. Pérez", "L. Reyes" });
        txtDescripcion = new TextBox();
        cmbEstado = new ComboBox() { DropDownStyle = ComboBoxStyle.DropDownList };
        cmbEstado.Items.AddRange(new[] { "En proceso", "Finalizada", "Retrasada" });
        txtCosto = new TextBox();
        txtObservaciones = new TextBox();

        panel.Controls.Add(txtID, 1, 0);
        panel.Controls.Add(txtDispositivo, 1, 1);
        panel.Controls.Add(txtCliente, 1, 2);
        panel.Controls.Add(cmbTecnico, 1, 3);
        panel.Controls.Add(txtDescripcion, 1, 4);
        panel.Controls.Add(cmbEstado, 1, 5);
        panel.Controls.Add(txtCosto, 1, 6);
        panel.Controls.Add(txtObservaciones, 1, 7);

        var btnGuardar = new Button() { Text = "Guardar", Dock = DockStyle.Bottom, Height = 40, BackColor = ColorTranslator.FromHtml("#0070C0"), ForeColor = Color.White };
        btnGuardar.Click += (s, e) =>
        {
            if (!ValidarFormulario())
                return;

            ReparacionNueva = new Reparacion
            {
                ID = txtID.Text,
                Dispositivo = txtDispositivo.Text.Trim(),
                Cliente = txtCliente.Text.Trim(),
                TecnicoAsignado = cmbTecnico.SelectedItem.ToString(),
                DescripciondelDano = txtDescripcion.Text.Trim(),
                EstadoReparacion = cmbEstado.SelectedItem.ToString(),
                CostoEstimado = decimal.Parse(txtCosto.Text),
                Observaciones = txtObservaciones.Text.Trim(),
                FechadeIngreso = DateTime.Now.ToShortDateString(),
                FechaEstimadaEntrega = DateTime.Now.AddDays(7).ToShortDateString()
            };

            DialogResult = DialogResult.OK;
        };

        Controls.Add(btnGuardar);
        Controls.Add(panel);

        // --- Cargar datos existentes o generar nuevo ID ---
        if (existente != null)
        {
            txtID.Text = existente.ID;
            txtDispositivo.Text = existente.Dispositivo;
            txtCliente.Text = existente.Cliente;
            cmbTecnico.SelectedItem = existente.TecnicoAsignado;
            txtDescripcion.Text = existente.DescripciondelDano;
            cmbEstado.SelectedItem = existente.EstadoReparacion;
            txtCosto.Text = existente.CostoEstimado.ToString();
            txtObservaciones.Text = existente.Observaciones;
        }
        else
        {
            // ID automático R-### según próximo valor disponible
            txtID.Text = $"R-{proximoID:D3}";
        }
    }

    private bool ValidarFormulario()
    {
        if (string.IsNullOrWhiteSpace(txtDispositivo.Text))
        {
            MessageBox.Show("El campo Dispositivo es obligatorio.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtDispositivo.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtCliente.Text) || txtCliente.Text.Trim().Length < 3 || !System.Text.RegularExpressions.Regex.IsMatch(txtCliente.Text, @"^[a-zA-Z\s]+$"))
        {
            MessageBox.Show("El campo Cliente es obligatorio y solo puede contener letras y espacios, mínimo 3 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtCliente.Focus();
            return false;
        }

        if (cmbTecnico.SelectedItem == null)
        {
            MessageBox.Show("Seleccione un Técnico válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            cmbTecnico.Focus();
            return false;
        }

        if (string.IsNullOrWhiteSpace(txtDescripcion.Text) || txtDescripcion.Text.Trim().Length < 10)
        {
            MessageBox.Show("La Descripción del daño es obligatoria y debe tener al menos 10 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtDescripcion.Focus();
            return false;
        }

        if (cmbEstado.SelectedItem == null)
        {
            MessageBox.Show("Seleccione un Estado de reparación válido.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            cmbEstado.Focus();
            return false;
        }

        if (!decimal.TryParse(txtCosto.Text, out decimal costo) || costo <= 0 || costo > 10000)
        {
            MessageBox.Show("Ingrese un Costo válido entre 1 y 10,000.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtCosto.Focus();
            return false;
        }

        if (txtObservaciones.Text.Length > 200)
        {
            MessageBox.Show("Las Observaciones no pueden exceder 200 caracteres.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            txtObservaciones.Focus();
            return false;
        }

        return true;
    }
}


}
