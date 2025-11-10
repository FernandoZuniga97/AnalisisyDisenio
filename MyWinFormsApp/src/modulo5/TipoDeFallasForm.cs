using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.IO;
namespace MyWinFormsApp
{
    public class TipoDeFallasForm : Form
    {
        private List<TipoFalla> lista;

        private Panel contenedorReporte;
        private Panel headerPanel;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private Label lblTotal;
        private DataGridView dgvFallas;
        private Chart chartFallas;
        private SplitContainer splitContainer;

        public TipoDeFallasForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // ------------------
            // FORM
            // ------------------
            Text = "Tipo de Fallas";
            Width = 1250;
            Height = 700;
            BackColor = Color.FromArgb(242, 242, 242);
            StartPosition = FormStartPosition.CenterScreen;

            // ------------------
            // CONTENEDOR PRINCIPAL
            // ------------------
            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };
            Controls.Add(contenedorReporte);

            // ------------------
            // HEADER PANEL
            // ------------------
            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = ColorTranslator.FromHtml("#002060")
            };
            contenedorReporte.Controls.Add(headerPanel);

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
                Image = Image.FromFile(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                        "..\\..\\..\\src\\login\\Image\\logo_g.jpg" // Ajuste la ruta subiendo 3 niveles para llegar a la raíz del proyecto
                    )
                ),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };
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
                Text = "Tipo de Fallas en los Dispositivos",
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

            // ------------------
            // SPLIT CONTAINER (TABLA ARRIBA, GRAFICA ABAJO)
            // ------------------
            splitContainer = new SplitContainer()
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Horizontal,
                SplitterDistance = 300,
                IsSplitterFixed = false
            };
            contenedorReporte.Controls.Add(splitContainer);
            splitContainer.BringToFront();

            // ------------------
            // DATAGRIDVIEW
            // ------------------
            dgvFallas = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill,
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 35,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter }
            };
            dgvFallas.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvFallas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFallas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFallas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            splitContainer.Panel1.Controls.Add(dgvFallas);

            // ------------------
            // CHART
            // ------------------
            chartFallas = new Chart() { Dock = DockStyle.Fill, BackColor = Color.White };
            ChartArea chartArea = new ChartArea("ChartArea1");
            chartArea.AxisX.Interval = 1;
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Arial", 9, FontStyle.Bold);
            chartArea.AxisY.LabelStyle.Font = new Font("Arial", 9);
            chartArea.AxisX.Title = "Tipo de Falla";
            chartArea.AxisY.Title = "Frecuencia";
            chartArea.AxisX.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartArea.AxisY.TitleFont = new Font("Arial", 10, FontStyle.Bold);
            chartFallas.ChartAreas.Add(chartArea);

            Series serie = new Series("Frecuencia")
            {
                ChartType = SeriesChartType.Column,
                Color = ColorTranslator.FromHtml("#002060"),
                Font = new Font("Arial", 9, FontStyle.Bold),
                IsValueShownAsLabel = true
            };
            chartFallas.Series.Add(serie);
            splitContainer.Panel2.Controls.Add(chartFallas);

            // ------------------
            // TOTAL LABEL ABAJO
            // ------------------
            lblTotal = new Label()
            {
                Dock = DockStyle.Bottom,
                Height = 30,
                Font = new Font("Segoe UI", 12, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                ForeColor = Color.Black
            };
            splitContainer.Panel1.Controls.Add(lblTotal);

            Load += TipoDeFallasForm_Load;
        }

        private void TipoDeFallasForm_Load(object sender, EventArgs e)
        {
            lista = new List<TipoFalla>
            {
                new TipoFalla { Numero = 1, Tipo = "Botón de encendido", Frecuencia = 2 },
                new TipoFalla { Numero = 2, Tipo = "Placa lógica dañada", Frecuencia = 2 },
                new TipoFalla { Numero = 3, Tipo = "Puerto de carga malo", Frecuencia = 8 },
                new TipoFalla { Numero = 4, Tipo = "Pantalla rota", Frecuencia = 9 },
                new TipoFalla { Numero = 5, Tipo = "Batería defectuosa", Frecuencia = 4 },
                new TipoFalla { Numero = 6, Tipo = "Falla de software", Frecuencia = 1 }
            };

            // Ordenar de mayor a menor por frecuencia
            lista = lista.OrderByDescending(f => f.Frecuencia).ToList();

            ActualizarTablaYGrafico();
        }

        private void ActualizarTablaYGrafico()
        {
            dgvFallas.DataSource = null;

            // Crear lista con # de orden basado en frecuencia
            var listaOrdenada = lista.Select((falla, index) => new
            {
                Orden = index + 1, // nuevo #
                falla.Tipo,
                falla.Frecuencia
            }).ToList();

            dgvFallas.DataSource = listaOrdenada;

            dgvFallas.Columns[0].HeaderText = "#";
            dgvFallas.Columns[1].HeaderText = "Tipo de falla";
            dgvFallas.Columns[2].HeaderText = "Frecuencia";

            // Colorear filas pares (según el nuevo #)
            foreach (DataGridViewRow row in dgvFallas.Rows)
            {
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int orden))
                {
                    if (orden % 2 == 0)
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            // Gráfica
            chartFallas.Series["Frecuencia"].Points.Clear();
            foreach (var falla in lista)
                chartFallas.Series["Frecuencia"].Points.AddXY(falla.Tipo, falla.Frecuencia);

            // Total debajo de la tabla
            int total = lista.Sum(f => f.Frecuencia);
            lblTotal.Text = $"Total: {total} dispositivos con fallas";
        }
    }

    public class TipoFalla
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Frecuencia { get; set; }
    }
}
