using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing.Printing;


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
        private Button btnExportar;

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
                Dock = DockStyle.Top,
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
            // LABEL TOTAL
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
            // BOTÓN EXPORTAR A PDF / IMPRIMIR
            // ------------------
            btnExportar = new Button()
            {
                Text = "Generar PDF",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#0070C0"),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += BtnExportar_Click;
            contenedorReporte.Controls.Add(btnExportar);
            btnExportar.BringToFront();

            Load += TipoDeFallasForm_Load;
        }

        private void TipoDeFallasForm_Load(object sender, EventArgs e)
        {
            lista = new List<TipoFalla>
            {
                new TipoFalla { Numero = 1, Tipo = "Botón de encendido", Frecuencia = 2 },
                new TipoFalla { Numero = 2, Tipo = "Placa lógica dañada", Frecuencia = 2 },
                new TipoFalla { Numero = 3, Tipo = "Puerto de carga malo", Frecuencia = 8 },
                new TipoFalla { Numero = 4, Tipo = "Pantalla rota", Frecuencia = 14 },
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
                Orden = index + 1,
                falla.Tipo,
                falla.Frecuencia
            }).ToList();

            dgvFallas.DataSource = listaOrdenada;

            dgvFallas.Columns[0].HeaderText = "#";
            dgvFallas.Columns[1].HeaderText = "Tipo de falla";
            dgvFallas.Columns[2].HeaderText = "Frecuencia";

            // Colorear filas pares
            foreach (DataGridViewRow row in dgvFallas.Rows)
            {
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int orden))
                {
                    if (orden % 2 == 0)
                        row.DefaultCellStyle.BackColor = Color.LightGray;
                }
            }

            // Ajustar altura de dgvFallas según su contenido
            dgvFallas.Height = dgvFallas.ColumnHeadersHeight + dgvFallas.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

            // Ajustar SplitterDistance dinámicamente
            splitContainer.SplitterDistance = dgvFallas.Height + lblTotal.Height + 10;

            // Actualizar gráfica
            chartFallas.Series["Frecuencia"].Points.Clear();
            foreach (var falla in lista)
                chartFallas.Series["Frecuencia"].Points.AddXY(falla.Tipo, falla.Frecuencia);

            // Mostrar total
            int total = lista.Sum(f => f.Frecuencia);
            lblTotal.Text = $"Total: {total} dispositivos con fallas";
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            printDoc.DefaultPageSettings.Landscape = true;
            printDoc.PrintPage += PrintDoc_PrintPageFallas;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 1000,
                Height = 800
            };
            preview.ShowDialog();
        }

        private void PrintDoc_PrintPageFallas(object sender, PrintPageEventArgs e)
        {
            // Capturar header
            Bitmap headerBitmap = new Bitmap(headerPanel.Width, headerPanel.Height);
            headerPanel.DrawToBitmap(headerBitmap, new Rectangle(0, 0, headerPanel.Width, headerPanel.Height));

            // Capturar tabla
            int tablaWidth = dgvFallas.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
            int tablaHeight = dgvFallas.ColumnHeadersHeight + dgvFallas.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            Bitmap dgvBitmap = new Bitmap(tablaWidth, tablaHeight);
            using (Graphics g = Graphics.FromImage(dgvBitmap))
            {
                g.Clear(Color.White);
                int xPos = 0;
                for (int i = 0; i < dgvFallas.Columns.Count; i++)
                {
                    var col = dgvFallas.Columns[i];
                    Rectangle headerRect = new Rectangle(xPos, 0, col.Width, dgvFallas.ColumnHeadersHeight);
                    using (Brush backBrush = new SolidBrush(dgvFallas.ColumnHeadersDefaultCellStyle.BackColor))
                        g.FillRectangle(backBrush, headerRect);
                    using (Brush foreBrush = new SolidBrush(dgvFallas.ColumnHeadersDefaultCellStyle.ForeColor))
                    {
                        StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                        g.DrawString(col.HeaderText, dgvFallas.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, sf);
                    }
                    g.DrawRectangle(Pens.Black, headerRect);
                    xPos += col.Width;
                }

                int yPos = dgvFallas.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvFallas.Rows)
                {
                    xPos = 0;
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        var cell = row.Cells[i];
                        Rectangle cellRect = new Rectangle(xPos, yPos, cell.OwningColumn.Width, row.Height);
                        using (Brush backBrush = new SolidBrush(row.Index % 2 == 1 ? Color.LightGray : Color.White))
                            g.FillRectangle(backBrush, cellRect);
                        using (Brush foreBrush = new SolidBrush(cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor))
                        {
                            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                            g.DrawString(cell.FormattedValue?.ToString(), cell.InheritedStyle.Font, foreBrush, cellRect, sf);
                        }
                        g.DrawRectangle(Pens.Black, cellRect);
                        xPos += cell.OwningColumn.Width;
                    }
                    yPos += row.Height;
                }
            }

            // Capturar gráfico
            Bitmap chartBitmap = new Bitmap(chartFallas.Width, chartFallas.Height);
            chartFallas.DrawToBitmap(chartBitmap, new Rectangle(0, 0, chartFallas.Width, chartFallas.Height));

            // Combinar todo
            int totalWidth = Math.Max(headerBitmap.Width, Math.Max(dgvBitmap.Width, chartBitmap.Width));
            int totalHeight = headerBitmap.Height + dgvBitmap.Height + chartBitmap.Height + 20;

            Bitmap printBitmap = new Bitmap(totalWidth, totalHeight);
            using (Graphics g = Graphics.FromImage(printBitmap))
            {
                g.Clear(Color.White);
                g.DrawImage(headerBitmap, 0, 0);
                g.DrawImage(dgvBitmap, 0, headerBitmap.Height);
                g.DrawImage(chartBitmap, 0, headerBitmap.Height + dgvBitmap.Height + 10);
            }

            // Escalar y dibujar en página
            float scale = Math.Min((float)e.MarginBounds.Width / printBitmap.Width, (float)e.MarginBounds.Height / printBitmap.Height);
            int printWidth = (int)(printBitmap.Width * scale);
            int printHeight = (int)(printBitmap.Height * scale);
            e.Graphics.DrawImage(printBitmap, e.MarginBounds.Left, e.MarginBounds.Top, printWidth, printHeight);
        }
    }

    public class TipoFalla
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Frecuencia { get; set; }
    }
}
