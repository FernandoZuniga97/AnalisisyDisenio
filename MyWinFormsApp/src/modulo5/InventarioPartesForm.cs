using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace MyWinFormsApp
{
    public class InventarioPartesForm : Form
    {
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

        private List<Parte> lista;
        private int contadorID = 4;

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
                Padding = new Padding(0, 15, 0, 15)
            };

            separatorLine = new Panel()
            {
                Height = 3,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 15, 0, 15)
            };

            // Panel título y logo
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
                Anchor = AnchorStyles.Left | AnchorStyles.Top
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
            catch (Exception ex)
            {
                MessageBox.Show($"Error loading logo: {ex.Message}", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

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

            // DataGridView
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
                ScrollBars = ScrollBars.Vertical
            };

            dgvInventario.RowPrePaint += (s, e) =>
{
    if (e.RowIndex % 2 == 0) // filas pares
        dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.LightGray;
    else
        dgvInventario.Rows[e.RowIndex].DefaultCellStyle.BackColor = Color.White;
};


            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            Panel panelTabla = new Panel() { Dock = DockStyle.Fill };
            panelTabla.Controls.Add(dgvInventario);



            // Botones
            btnAgregar = new Button() { Text = "Agregar Parte", Dock = DockStyle.Bottom, Height = 30, BackColor = ColorTranslator.FromHtml("#0070C0"), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnAgregar.Click += BtnAgregar_Click;
            btnEliminar = new Button() { Text = "Eliminar Parte", Dock = DockStyle.Bottom, Height = 30, BackColor = Color.Red, ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnEliminar.Click += BtnEliminar_Click;
            btnEditar = new Button() { Text = "Editar Parte", Dock = DockStyle.Bottom, Height = 30, BackColor = ColorTranslator.FromHtml("#E1E11F"), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnEditar.Click += BtnEditar_Click;
            btnImprimir = new Button() { Text = "Imprimir", Dock = DockStyle.Bottom, Height = 30, BackColor = ColorTranslator.FromHtml("#009933"), ForeColor = Color.White, Font = new Font("Segoe UI", 10, FontStyle.Bold) };
            btnImprimir.Click += BtnImprimir_Click;

            Panel panelPaginacion = new Panel() { Dock = DockStyle.Bottom, Height = 35, BackColor = Color.White };
            btnPaginaAnterior = new Button() { Text = "<", Width = 35, Height = 25, Left = 10, Top = 5 }; btnPaginaAnterior.Click += BtnPaginaAnterior_Click;
            btnPaginaSiguiente = new Button() { Text = ">", Width = 35, Height = 25, Left = 60, Top = 5 }; btnPaginaSiguiente.Click += BtnPaginaSiguiente_Click;
            lblPagina = new Label() { Text = "Pag. 1 de 1", AutoSize = true, Left = 120, Top = 10 };
            panelPaginacion.Controls.AddRange(new Control[] { btnPaginaAnterior, btnPaginaSiguiente, lblPagina });

            contentPanel.Controls.Add(separatorLine);
            contentPanel.Controls.Add(panelTabla);
            contentPanel.Controls.Add(panelPaginacion);
            contentPanel.Controls.Add(btnAgregar);
            contentPanel.Controls.Add(btnEliminar);
            contentPanel.Controls.Add(btnEditar);
            contentPanel.Controls.Add(btnImprimir);

            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            Load += InventarioPartesForm_Load;
        }

        private void InventarioPartesForm_Load(object sender, EventArgs e)
        {
            lista = new List<Parte>()
            {
               new Parte { ID="P-001", Nombre="Pantalla iPhone 12", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=12, Unidad="unidades", CostoUnitario=2500m, ValorTotal=30000m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
               new Parte { ID="P-002", Nombre="Puerto de carga tipo C", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=8, Unidad="unidades", CostoUnitario=180m, ValorTotal=1440m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
               new Parte { ID="P-003", Nombre="Batería Samsung A52", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=15, Unidad="unidades", CostoUnitario=600m, ValorTotal=9000m, EstadoStock="Suficiente", Observaciones="-" },
               new Parte { ID="P-004", Nombre="Pasta térmica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=20, Unidad="tubos", CostoUnitario=120m, ValorTotal=2400m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
               new Parte { ID="P-001", Nombre="Pantalla iPhone 12", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=12, Unidad="unidades", CostoUnitario=2500m, ValorTotal=30000m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-002", Nombre="Puerto de carga tipo C", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=8, Unidad="unidades", CostoUnitario=180m, ValorTotal=1440m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-003", Nombre="Batería Samsung A52", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=15, Unidad="unidades", CostoUnitario=600m, ValorTotal=9000m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-004", Nombre="Pasta térmica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=20, Unidad="tubos", CostoUnitario=120m, ValorTotal=2400m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-005", Nombre="Pantalla Samsung S21", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=10, Unidad="unidades", CostoUnitario=2800m, ValorTotal=28000m, EstadoStock="Suficiente", Observaciones="Alta demanda" },
new Parte { ID="P-006", Nombre="Puerto Lightning", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=5, Unidad="unidades", CostoUnitario=200m, ValorTotal=1000m, EstadoStock="Bajo", Observaciones="Pocas unidades disponibles" },
new Parte { ID="P-007", Nombre="Batería iPhone 11", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=20, Unidad="unidades", CostoUnitario=550m, ValorTotal=11000m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-008", Nombre="Cargador inalámbrico", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=12, Unidad="unidades", CostoUnitario=300m, ValorTotal=3600m, EstadoStock="Suficiente", Observaciones="Popular" },
new Parte { ID="P-009", Nombre="Pantalla Xiaomi Mi 10", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=7, Unidad="unidades", CostoUnitario=2400m, ValorTotal=16800m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-010", Nombre="Conector USB-C Micro", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=18, Unidad="unidades", CostoUnitario=150m, ValorTotal=2700m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-011", Nombre="Batería Huawei P30", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=9, Unidad="unidades", CostoUnitario=620m, ValorTotal=5580m, EstadoStock="Bajo", Observaciones="Rotación rápida" },
new Parte { ID="P-012", Nombre="Pasta térmica profesional", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=15, Unidad="tubos", CostoUnitario=150m, ValorTotal=2250m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-013", Nombre="Pantalla iPhone 13", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=14, Unidad="unidades", CostoUnitario=2600m, ValorTotal=36400m, EstadoStock="Suficiente", Observaciones="Alta demanda" },
new Parte { ID="P-014", Nombre="Cable HDMI", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=25, Unidad="unidades", CostoUnitario=120m, ValorTotal=3000m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-015", Nombre="Batería iPhone 12", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=17, Unidad="unidades", CostoUnitario=580m, ValorTotal=9860m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-016", Nombre="Cargador rápido USB-C", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=20, Unidad="unidades", CostoUnitario=350m, ValorTotal=7000m, EstadoStock="Suficiente", Observaciones="Popular" },
new Parte { ID="P-017", Nombre="Pantalla OnePlus 9", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=6, Unidad="unidades", CostoUnitario=2300m, ValorTotal=13800m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-018", Nombre="Conector Lightning", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=12, Unidad="unidades", CostoUnitario=180m, ValorTotal=2160m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-019", Nombre="Batería Samsung Note 20", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=11, Unidad="unidades", CostoUnitario=640m, ValorTotal=7040m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-020", Nombre="Pasta térmica básica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=30, Unidad="tubos", CostoUnitario=100m, ValorTotal=3000m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-021", Nombre="Pantalla Motorola G9", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=8, Unidad="unidades", CostoUnitario=2200m, ValorTotal=17600m, EstadoStock="Suficiente", Observaciones="Alta demanda" },
new Parte { ID="P-022", Nombre="Cable USB-C", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=30, Unidad="unidades", CostoUnitario=140m, ValorTotal=4200m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-023", Nombre="Batería LG G8", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=13, Unidad="unidades", CostoUnitario=600m, ValorTotal=7800m, EstadoStock="Suficiente", Observaciones="Rotación rápida" },
new Parte { ID="P-024", Nombre="Cargador portátil 10.000mAh", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=10, Unidad="unidades", CostoUnitario=400m, ValorTotal=4000m, EstadoStock="Suficiente", Observaciones="Popular" },
new Parte { ID="P-025", Nombre="Pantalla iPhone SE", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=9, Unidad="unidades", CostoUnitario=2100m, ValorTotal=18900m, EstadoStock="Suficiente", Observaciones="Alta demanda" },
new Parte { ID="P-026", Nombre="Puerto micro USB", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=20, Unidad="unidades", CostoUnitario=130m, ValorTotal=2600m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-027", Nombre="Batería Xiaomi Mi 11", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=16, Unidad="unidades", CostoUnitario=610m, ValorTotal=9760m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-028", Nombre="Pasta térmica premium", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=18, Unidad="tubos", CostoUnitario=200m, ValorTotal=3600m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-029", Nombre="Pantalla Huawei P40", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=5, Unidad="unidades", CostoUnitario=2500m, ValorTotal=12500m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-030", Nombre="Cable HDMI 2.1", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=14, Unidad="unidades", CostoUnitario=180m, ValorTotal=2520m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-031", Nombre="Batería Oppo Reno 5", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=12, Unidad="unidades", CostoUnitario=590m, ValorTotal=7080m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-032", Nombre="Cargador inalámbrico Qi", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=8, Unidad="unidades", CostoUnitario=350m, ValorTotal=2800m, EstadoStock="Suficiente", Observaciones="Popular" },
new Parte { ID="P-033", Nombre="Pantalla Nokia 5.4", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=6, Unidad="unidades", CostoUnitario=2000m, ValorTotal=12000m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-034", Nombre="Conector USB-A", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=22, Unidad="unidades", CostoUnitario=120m, ValorTotal=2640m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-035", Nombre="Batería Realme 8", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=14, Unidad="unidades", CostoUnitario=570m, ValorTotal=7980m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-036", Nombre="Pasta térmica básica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=25, Unidad="tubos", CostoUnitario=110m, ValorTotal=2750m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-037", Nombre="Pantalla Sony Xperia 10", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=7, Unidad="unidades", CostoUnitario=2100m, ValorTotal=14700m, EstadoStock="Suficiente", Observaciones="Alta demanda" },
new Parte { ID="P-038", Nombre="Cable USB-C 2m", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=30, Unidad="unidades", CostoUnitario=150m, ValorTotal=4500m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-039", Nombre="Batería Samsung A72", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=10, Unidad="unidades", CostoUnitario=630m, ValorTotal=6300m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-040", Nombre="Cargador rápido 20W", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=12, Unidad="unidades", CostoUnitario=400m, ValorTotal=4800m, EstadoStock="Suficiente", Observaciones="Popular" },
new Parte { ID="P-041", Nombre="Pantalla LG V60", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=5, Unidad="unidades", CostoUnitario=2400m, ValorTotal=12000m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
new Parte { ID="P-042", Nombre="Puerto USB-C Mini", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=15, Unidad="unidades", CostoUnitario=160m, ValorTotal=2400m, EstadoStock="Suficiente", Observaciones="-" },
new Parte { ID="P-043", Nombre="Batería iPhone 13 Pro", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=11, Unidad="unidades", CostoUnitario=650m, ValorTotal=7150m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
new Parte { ID="P-044", Nombre="Pasta térmica avanzada", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=20, Unidad="tubos", CostoUnitario=180m, ValorTotal=3600m, EstadoStock="Suficiente", Observaciones="Uso frecuente" },
new Parte { ID="P-045", Nombre="Cargador inalámbrico rápido", Categoria="Accesorios", Proveedor="TechWorld", CantidadActual=10, Unidad="unidades", CostoUnitario=450m, ValorTotal=4500m, EstadoStock="Suficiente", Observaciones="Popular" }
 };

            MostrarPagina();
        }

        private void MostrarPagina()
        {
            totalPaginas = (int)Math.Ceiling((double)lista.Count / registrosPorPagina);
            if (paginaActual < 1) paginaActual = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            var registros = lista
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            dgvInventario.DataSource = null;
            dgvInventario.DataSource = registros;

            dgvInventario.GridColor = Color.White;
            dgvInventario.CellBorderStyle = DataGridViewCellBorderStyle.Single;

            if (dgvInventario.Columns.Count > 0)
            {
                dgvInventario.Columns["ID"].HeaderText = "Código";
                dgvInventario.Columns["Nombre"].HeaderText = "Nombre de la Parte";
                dgvInventario.Columns["Categoria"].HeaderText = "Categoría";
                dgvInventario.Columns["Proveedor"].HeaderText = "Proveedor";
                dgvInventario.Columns["CantidadActual"].HeaderText = "Cantidad";
                dgvInventario.Columns["Unidad"].HeaderText = "Unidad";
                dgvInventario.Columns["CostoUnitario"].HeaderText = "Costo Unitario";
                dgvInventario.Columns["ValorTotal"].HeaderText = "Valor Total ";
                dgvInventario.Columns["EstadoStock"].HeaderText = "Estado del Stock";
                dgvInventario.Columns["Observaciones"].HeaderText = "Observaciones";

                dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.Format = "'L' #,##0.00";
                dgvInventario.Columns["ValorTotal"].DefaultCellStyle.Format = "'L' #,##0.00";
                dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                dgvInventario.Columns["ValorTotal"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            }

            for (int i = 0; i < dgvInventario.Rows.Count; i++)
            {
                dgvInventario.Rows[i].DefaultCellStyle.BackColor = (i % 2 == 1) ? Color.LightGray : Color.White;
            }



            var widths = new Dictionary<string, int>
            {
                ["ID"] = 80,
                ["Nombre"] = 300,
                ["Categoria"] = 140,
                ["Proveedor"] = 160,
                ["CantidadActual"] = 110,
                ["Unidad"] = 90,
                ["CostoUnitario"] = 120,
                ["ValorTotal"] = 120,
                ["EstadoStock"] = 140,
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

            contenedorReporte.AutoScrollMinSize = new Size(Math.Max(totalWidth + 40, this.Width), 0);
            lblPagina.Text = $"Pag. {paginaActual} de {totalPaginas}";

            // === Línea amarilla bajo encabezados ===
            Panel lineaAmarilla = new Panel()
            {
                BackColor = Color.Gold,
                Height = 3,
                Width = dgvInventario.Width,
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
                lineaAmarilla.Width = dgvInventario.Width;
                lineaAmarilla.Top = dgvInventario.Top + dgvInventario.ColumnHeadersHeight;
            };
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

            // --- Captura DataGridView ---
            int tablaWidth = dgvInventario.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
            int tablaHeight = dgvInventario.ColumnHeadersHeight + dgvInventario.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            Bitmap dgvBitmap = new Bitmap(tablaWidth, tablaHeight);
            using (Graphics g = Graphics.FromImage(dgvBitmap))
            {
                g.Clear(Color.White);
                int xPos = 0;

                // Cabecera
                for (int i = 0; i < dgvInventario.Columns.Count; i++)
                {
                    var col = dgvInventario.Columns[i];
                    Rectangle headerRect = new Rectangle(xPos, 0, col.Width, dgvInventario.ColumnHeadersHeight);
                    using (Brush backBrush = new SolidBrush(dgvInventario.ColumnHeadersDefaultCellStyle.BackColor))
                        g.FillRectangle(backBrush, headerRect);
                    using (Brush foreBrush = new SolidBrush(dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor))
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString(col.HeaderText, dgvInventario.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, sf);
                    }
                    g.DrawRectangle(Pens.Black, headerRect);
                    xPos += col.Width;
                }

                // --- Línea amarilla debajo de encabezado ---
                g.FillRectangle(Brushes.Gold, 0, dgvInventario.ColumnHeadersHeight, tablaWidth, 3);

                int yPos = dgvInventario.ColumnHeadersHeight + 3; // Ajusta por línea amarilla
                foreach (DataGridViewRow row in dgvInventario.Rows)
                {
                    xPos = 0;
                    int filaIndex = dgvInventario.Rows.IndexOf(row);
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
                        using (Pen whitePen = new Pen(Color.White))
                            g.DrawRectangle(whitePen, cellRect);
                        xPos += cell.OwningColumn.Width;
                    }
                    yPos += row.Height;
                }
            }

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

            float scale = Math.Min(
                (float)e.MarginBounds.Width / printBitmap.Width,
                (float)e.MarginBounds.Height / printBitmap.Height
            );

            int printWidth = (int)(printBitmap.Width * scale);
            int printHeight = (int)(printBitmap.Height * scale);

            e.Graphics.DrawImage(printBitmap, e.MarginBounds.Left, e.MarginBounds.Top, printWidth, printHeight);
        }

        private void BtnAgregar_Click(object sender, EventArgs e) => AgregarEditarParte(null);
        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow == null) return;
            int index = dgvInventario.CurrentRow.Index;
            Parte parte = lista[index];
            AgregarEditarParte(parte);
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.CurrentRow != null)
            {
                string id = dgvInventario.CurrentRow.Cells["ID"].Value.ToString();
                DialogResult result = MessageBox.Show($"¿Está seguro de borrar el registro '{id}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    lista.RemoveAt(dgvInventario.CurrentRow.Index);
                    MostrarPagina();
                }
            }
        }

        private void AgregarEditarParte(Parte parte)
        {
            bool esEditar = parte != null;
            Form form = new Form()
            {
                Width = 400,
                Height = 450,
                Text = esEditar ? "Editar Parte" : "Agregar Nueva Parte",
                FormBorderStyle = FormBorderStyle.FixedDialog,
                StartPosition = FormStartPosition.CenterParent
            };

            Panel panel = new Panel() { Dock = DockStyle.Fill, AutoScroll = true };
            form.Controls.Add(panel);

            int labelWidth = 120;
            int textBoxWidth = 200;
            int top = 20;
            int gap = 40;

            Label lblNombre = new Label() { Text = "Nombre", Left = 20, Top = top, Width = labelWidth };
            TextBox txtNombre = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.Nombre : "" };
            top += gap;

            Label lblCategoria = new Label() { Text = "Categoría", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCategoria = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.Categoria : "" };
            top += gap;

            Label lblProveedor = new Label() { Text = "Proveedor", Left = 20, Top = top, Width = labelWidth };
            TextBox txtProveedor = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.Proveedor : "" };
            top += gap;

            Label lblCantidad = new Label() { Text = "Cantidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCantidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.CantidadActual.ToString() : "" };
            top += gap;

            Label lblUnidad = new Label() { Text = "Unidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtUnidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.Unidad : "" };
            top += gap;

            Label lblCosto = new Label() { Text = "Costo Unitario", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCosto = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.CostoUnitario.ToString() : "" };
            top += gap;

            Label lblObservaciones = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObservaciones = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = esEditar ? parte.Observaciones : "" };
            top += gap;

            Button btnGuardar = new Button() { Text = "Guardar", Left = 150, Top = top + 10, Width = 100 };
            btnGuardar.Click += (s, e) =>
            {
                if (!esEditar)
                {
                    Parte nueva = new Parte
                    {
                        ID = $"P-{++contadorID:D3}",
                        Nombre = txtNombre.Text,
                        Categoria = txtCategoria.Text,
                        Proveedor = txtProveedor.Text,
                        CantidadActual = int.Parse(txtCantidad.Text),
                        Unidad = txtUnidad.Text,
                        CostoUnitario = decimal.Parse(txtCosto.Text),
                        ValorTotal = decimal.Parse(txtCantidad.Text) * decimal.Parse(txtCosto.Text),
                        EstadoStock = "Suficiente",
                        Observaciones = txtObservaciones.Text
                    };
                    lista.Add(nueva);
                }
                else
                {
                    parte.Nombre = txtNombre.Text;
                    parte.Categoria = txtCategoria.Text;
                    parte.Proveedor = txtProveedor.Text;
                    parte.CantidadActual = int.Parse(txtCantidad.Text);
                    parte.Unidad = txtUnidad.Text;
                    parte.CostoUnitario = decimal.Parse(txtCosto.Text);
                    parte.ValorTotal = parte.CantidadActual * parte.CostoUnitario;
                    parte.Observaciones = txtObservaciones.Text;
                }

                MostrarPagina();
                form.Close();
            };

            panel.Controls.AddRange(new Control[] { lblNombre, txtNombre, lblCategoria, txtCategoria,
                lblProveedor, txtProveedor, lblCantidad, txtCantidad, lblUnidad, txtUnidad, lblCosto, txtCosto,
                lblObservaciones, txtObservaciones, btnGuardar });

            form.ShowDialog();
        }
    }

    public class Parte
    {
        public string ID { get; set; }
        public string Nombre { get; set; }
        public string Categoria { get; set; }
        public string Proveedor { get; set; }
        public int CantidadActual { get; set; }
        public string Unidad { get; set; }
        public decimal CostoUnitario { get; set; }
        public decimal ValorTotal { get; set; }
        public string EstadoStock { get; set; }
        public string Observaciones { get; set; }
    }
}
