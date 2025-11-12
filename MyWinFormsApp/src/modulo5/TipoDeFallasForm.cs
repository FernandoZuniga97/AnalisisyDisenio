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
        // private Label lblFecha;
        private Label lblTotal;
        private Label lblPeriodo;
        private DataGridView dgvFallas;
        private Chart chartFallas;
        private SplitContainer splitContainer;
        private Button btnExportar;
        private ComboBox cmbTrimestre;

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

            // ------------------
            // TITLE PANEL
            // ------------------
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
                        "..\\..\\..\\src\\login\\Image\\logo_g.jpg"
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

            lblPeriodo = new Label()
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter, // Centrado
                Dock = DockStyle.Top,   // Ocupa todo el ancho del header
                Height = 30
            };

            headerPanel.Controls.Add(lblPeriodo);


            textoPanel.Controls.Add(lblPeriodo);
            textoPanel.Controls.Add(lblSubtitulo);
            textoPanel.Controls.Add(lblTitulo);
            titlePanel.Controls.Add(textoPanel, 1, 0);
            headerPanel.Controls.Add(titlePanel);

            // ------------------
            // COMBOBOX TRIMESTRE
            // ------------------
            cmbTrimestre = new ComboBox()
            {
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbTrimestre.Items.AddRange(new string[]
            {
                "1er Trimestre",
                "2do Trimestre",
                "3er Trimestre",
                "4to Trimestre"
            });
            cmbTrimestre.SelectedIndex = 0;

            cmbTrimestre.Top = 10;
            cmbTrimestre.Left = headerPanel.Width - cmbTrimestre.Width - 20;
            cmbTrimestre.Anchor = AnchorStyles.Top | AnchorStyles.Right;

            headerPanel.Controls.Add(cmbTrimestre);
            cmbTrimestre.BringToFront();

            headerPanel.Resize += (s, e) =>
            {
                cmbTrimestre.Left = headerPanel.Width - cmbTrimestre.Width - 20;
            };

            cmbTrimestre.SelectedIndexChanged += CmbTrimestre_SelectedIndexChanged;

            // ------------------
            // SPLIT CONTAINER
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
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White
            };
            dgvFallas.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvFallas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFallas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFallas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFallas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;

            dgvFallas.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(Color.Gold, 3))
                {
                    e.Graphics.DrawLine(pen, 0, dgvFallas.ColumnHeadersHeight - 1, dgvFallas.Width, dgvFallas.ColumnHeadersHeight - 1);
                }
            };

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
            // BOTÓN EXPORTAR
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
                // Datos de ejemplo (5-7 fallas por trimestre)
                new TipoFalla { Numero = 1, Tipo = "Pantalla rota", Frecuencia = 12, Fecha = new DateTime(2025, 1, 15) },
                new TipoFalla { Numero = 2, Tipo = "Batería defectuosa", Frecuencia = 10, Fecha = new DateTime(2025, 2, 2) },
                new TipoFalla { Numero = 3, Tipo = "Botón de encendido", Frecuencia = 8, Fecha = new DateTime(2025, 2, 18) },
                new TipoFalla { Numero = 4, Tipo = "Placa lógica dañada", Frecuencia = 14, Fecha = new DateTime(2025, 3, 4) },
                new TipoFalla { Numero = 5, Tipo = "Puerto de carga dañado", Frecuencia = 9, Fecha = new DateTime(2025, 3, 19) },
                new TipoFalla { Numero = 6, Tipo = "Altavoz sin sonido", Frecuencia = 7, Fecha = new DateTime(2025, 3, 26) },
                new TipoFalla { Numero = 7, Tipo = "Micrófono no funciona", Frecuencia = 6, Fecha = new DateTime(2025, 3, 28) },

                new TipoFalla { Numero = 8, Tipo = "Pantalla sin brillo", Frecuencia = 10, Fecha = new DateTime(2025, 4, 10) },
                new TipoFalla { Numero = 9, Tipo = "Falla de software", Frecuencia = 11, Fecha = new DateTime(2025, 4, 20) },
                new TipoFalla { Numero = 10, Tipo = "Wi-Fi no conecta", Frecuencia = 7, Fecha = new DateTime(2025, 5, 2) },
                new TipoFalla { Numero = 11, Tipo = "Bluetooth inestable", Frecuencia = 6, Fecha = new DateTime(2025, 5, 14) },
                new TipoFalla { Numero = 12, Tipo = "No carga batería", Frecuencia = 9, Fecha = new DateTime(2025, 5, 29) },
                new TipoFalla { Numero = 13, Tipo = "Sensor de proximidad", Frecuencia = 8, Fecha = new DateTime(2025, 6, 10) },

                new TipoFalla { Numero = 14, Tipo = "Pantalla táctil falla", Frecuencia = 12, Fecha = new DateTime(2025, 7, 5) },
                new TipoFalla { Numero = 15, Tipo = "Falla de cámara trasera", Frecuencia = 9, Fecha = new DateTime(2025, 7, 20) },
                new TipoFalla { Numero = 16, Tipo = "Altavoz distorsionado", Frecuencia = 11, Fecha = new DateTime(2025, 8, 4) },
                new TipoFalla { Numero = 17, Tipo = "GPS no responde", Frecuencia = 8, Fecha = new DateTime(2025, 8, 19) },
                new TipoFalla { Numero = 18, Tipo = "Problemas de encendido", Frecuencia = 10, Fecha = new DateTime(2025, 9, 1) },
                new TipoFalla { Numero = 19, Tipo = "Error en sistema", Frecuencia = 9, Fecha = new DateTime(2025, 9, 17) },

                new TipoFalla { Numero = 20, Tipo = "Pantalla negra", Frecuencia = 9, Fecha = new DateTime(2025, 10, 3) },
                new TipoFalla { Numero = 21, Tipo = "Conector USB flojo", Frecuencia = 2, Fecha = new DateTime(2025, 10, 18) },
                new TipoFalla { Numero = 22, Tipo = "Cámara frontal defectuosa", Frecuencia = 5, Fecha = new DateTime(2025, 11, 5) },
                new TipoFalla { Numero = 23, Tipo = "Vibrador no funciona", Frecuencia = 7, Fecha = new DateTime(2025, 11, 20) },
                };

            // Mostrar trimestre inicial
            CmbTrimestre_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void CmbTrimestre_SelectedIndexChanged(object sender, EventArgs e)
        {
            DateTime inicio = DateTime.MinValue;
            DateTime fin = DateTime.MinValue;
            int year = DateTime.Now.Year;

            switch (cmbTrimestre.SelectedIndex)
            {
                case 0: inicio = new DateTime(year, 1, 1); fin = new DateTime(year, 3, 31); break;
                case 1: inicio = new DateTime(year, 4, 1); fin = new DateTime(year, 6, 30); break;
                case 2: inicio = new DateTime(year, 7, 1); fin = new DateTime(year, 9, 30); break;
                case 3: inicio = new DateTime(year, 10, 1); fin = new DateTime(year, 12, 31); break;
            }

            lblPeriodo.Text = $"Periodo: ({inicio:dd/MM/yyyy} - {fin:dd/MM/yyyy})";
            FiltrarPorTrimestre(inicio, fin);
        }

        private void FiltrarPorTrimestre(DateTime fechaInicio, DateTime fechaFin)
        {
            var filtrada = lista.Where(f => f.Fecha >= fechaInicio && f.Fecha <= fechaFin).ToList();
            ActualizarTablaYGrafico(filtrada);
        }

        private void ActualizarTablaYGrafico(List<TipoFalla> listaFiltrada = null)
        {
            var data = listaFiltrada ?? lista;

            dgvFallas.DataSource = null;

            var listaOrdenada = data.OrderByDescending(f => f.Frecuencia)
                                    .Select((falla, index) => new
                                    {
                                        Orden = index + 1,
                                        falla.Tipo,
                                        falla.Frecuencia
                                    }).ToList();

            dgvFallas.DataSource = listaOrdenada;

            dgvFallas.Columns[0].HeaderText = "#";
            dgvFallas.Columns[1].HeaderText = "Tipo de falla";
            dgvFallas.Columns[2].HeaderText = "Frecuencia";

            dgvFallas.RowPrePaint += (s, e) =>
            {
                var row = dgvFallas.Rows[e.RowIndex];
                if (row.Cells[0].Value != null && int.TryParse(row.Cells[0].Value.ToString(), out int orden))
                {
                    row.DefaultCellStyle.BackColor = (orden % 2 == 0) ? Color.LightGray : Color.White;
                }
            };

            dgvFallas.Height = dgvFallas.ColumnHeadersHeight + dgvFallas.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);
            splitContainer.SplitterDistance = dgvFallas.Height + lblTotal.Height + 10;

            chartFallas.Series["Frecuencia"].Points.Clear();
            foreach (var falla in data)
                chartFallas.Series["Frecuencia"].Points.AddXY(falla.Tipo, falla.Frecuencia);

            int total = data.Sum(f => f.Frecuencia);
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
                        g.DrawString(col.HeaderText, dgvFallas.ColumnHeadersDefaultCellStyle.Font, foreBrush, headerRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
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
                            g.DrawString(cell.FormattedValue?.ToString(), cell.InheritedStyle.Font, foreBrush, cellRect, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
                        g.DrawRectangle(Pens.White, cellRect);
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

            // Escalar y dibujar
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
        public DateTime Fecha { get; set; }
    }
}
