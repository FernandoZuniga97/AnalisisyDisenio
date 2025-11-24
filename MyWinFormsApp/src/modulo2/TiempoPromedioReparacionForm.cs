using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace MyWinFormsApp.src.modulo2
{
    public class TiempoPromedioReparacionForm : Form
    {
        private TableLayoutPanel mainLayout;
        private Panel headerPanel;
        private Panel topPanel; // franja blanca con botón
        private Button btnImprimir;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblPeriodo;
        private Chart chartTiempos;
        private List<TiempoTecnico> listaTiempos;
        private int currentPage = 1;

        public TiempoPromedioReparacionForm()
        {
            InicializarComponentes();
        }

        private void InicializarComponentes()
        {
            Text = "Tiempo Promedio en Reparación";
            Width = 1500;
            Height = 750;
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterScreen;

            mainLayout = new TableLayoutPanel()
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 3
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140)); // Header azul
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 60));  // Franja blanca con botón
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F)); // Contenido
            Controls.Add(mainLayout);

            // ---------- HEADER AZUL ----------
            headerPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = ColorTranslator.FromHtml("#002060")
            };
            mainLayout.Controls.Add(headerPanel, 0, 0);

            PictureBox logo = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory,
                    "..\\..\\..\\src\\login\\Image\\logo_g.jpg")),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(20, 15),
                Size = new Size(120, 110)
            };
            headerPanel.Controls.Add(logo);

            Panel panelTextos = new Panel() { BackColor = Color.Transparent, AutoSize = true };
            headerPanel.Controls.Add(panelTextos);

            lblTitulo = new Label()
            {
                Text = "IMPORTACIONES GICELL",
                Font = new Font("Century Gothic", 22, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true
            };
            panelTextos.Controls.Add(lblTitulo);

            lblSubtitulo = new Label()
            {
                Text = "Tiempo Promedio en Reparación",
                Font = new Font("Century Gothic", 13, FontStyle.Italic),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, lblTitulo.Bottom + 5)
            };
            panelTextos.Controls.Add(lblSubtitulo);

            lblPeriodo = new Label()
            {
                Text = "Periodo: (25/09/25 - 25/10/25)",
                Font = new Font("Century Gothic", 10, FontStyle.Italic),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point(0, lblSubtitulo.Bottom + 5)
            };
            panelTextos.Controls.Add(lblPeriodo);

            panelTextos.Location = new Point(
                (headerPanel.Width - panelTextos.PreferredSize.Width) / 2,
                (headerPanel.Height - panelTextos.PreferredSize.Height) / 2
            );

            headerPanel.Resize += (s, e) =>
            {
                panelTextos.Location = new Point(
                    (headerPanel.Width - panelTextos.PreferredSize.Width) / 2,
                    (headerPanel.Height - panelTextos.PreferredSize.Height) / 2
                );
            };

            // ---------- FRANJA BLANCA CON BOTÓN IMPRIMIR ----------
            topPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };

            btnImprimir = new Button()
            {
                Text = "Imprimir",
                Width = 120,
                Height = 40,
                BackColor = Color.FromArgb(0, 65, 130),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnImprimir.Click += BtnImprimir_Click;
            topPanel.Controls.Add(btnImprimir);

            topPanel.Resize += (s, e) =>
            {
                btnImprimir.Location = new Point(
                    topPanel.Width - btnImprimir.Width - 20,
                    (topPanel.Height - btnImprimir.Height) / 2
                );
            };

            mainLayout.Controls.Add(topPanel, 0, 1);

            // ---------- CONTENIDO ----------
            SplitContainer splitContainer = new SplitContainer()
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 820
            };
            mainLayout.Controls.Add(splitContainer, 0, 2);

            Panel panelIzquierdo = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            splitContainer.Panel1.Controls.Add(panelIzquierdo);

            Panel panelDerecho = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(10)
            };
            splitContainer.Panel2.Controls.Add(panelDerecho);

            // ---------- GRAFICO ----------
            chartTiempos = new Chart() { Dock = DockStyle.Fill, BackColor = Color.White };
            ChartArea chartArea = new ChartArea("Main");
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Century Gothic", 10);
            chartArea.AxisY.LabelStyle.Font = new Font("Century Gothic", 10);
            chartArea.AxisY.Title = "Tiempo promedio (hrs)";
            chartArea.AxisY.TitleFont = new Font("Century Gothic", 11, FontStyle.Bold);
            chartArea.AxisX.Title = "Técnicos encargados";
            chartArea.AxisX.TitleFont = new Font("Century Gothic", 11, FontStyle.Bold);
            chartArea.BackColor = Color.White;
            chartTiempos.ChartAreas.Add(chartArea);
            panelDerecho.Controls.Add(chartTiempos);

            // Colores
            Color amarillo = ColorTranslator.FromHtml("#FFD700");
            Color azul = ColorTranslator.FromHtml("#002060");
            Color azulClaro = ColorTranslator.FromHtml("#4A6BB2");

            chartTiempos.Series.Add(new Series("Laptop")
            {
                ChartType = SeriesChartType.Column,
                Color = amarillo,
                IsValueShownAsLabel = true,
                Font = new Font("Century Gothic", 9, FontStyle.Bold)
            });
            chartTiempos.Series.Add(new Series("Teléfono")
            {
                ChartType = SeriesChartType.Column,
                Color = azul,
                IsValueShownAsLabel = true,
                Font = new Font("Century Gothic", 9, FontStyle.Bold)
            });
            chartTiempos.Series.Add(new Series("Impresora")
            {
                ChartType = SeriesChartType.Column,
                Color = azulClaro,
                IsValueShownAsLabel = true,
                Font = new Font("Century Gothic", 9, FontStyle.Bold)
            });

            // ---------- DATOS ----------
            listaTiempos = new List<TiempoTecnico>
            {
                new TiempoTecnico("Raul Padilla", 12,3, 6,3, 18,3),
                new TiempoTecnico("Marcos Turcios", 9,3, 12,3, 6,3),
                new TiempoTecnico("Alejandra Pineda", 15,3, 3,3, 3,3)
            };

            int y = 10;
            foreach (var t in listaTiempos)
            {
                Panel panelTecnico = CrearPanelTecnico(t);
                panelTecnico.Location = new Point(10, y);
                panelIzquierdo.Controls.Add(panelTecnico);
                y += panelTecnico.Height + 20;
            }

            // ---------- GRAFICAR ----------
            foreach (var t in listaTiempos)
            {
                chartTiempos.Series["Laptop"].Points.AddXY(t.Tecnico, t.Laptop);
                chartTiempos.Series["Teléfono"].Points.AddXY(t.Tecnico, t.Telefono);
                chartTiempos.Series["Impresora"].Points.AddXY(t.Tecnico, t.Impresora);
            }
        }

        private void BtnImprimir_Click(object sender, EventArgs e)
        {
            PrintDocument printDoc = new PrintDocument();
            
    printDoc.DefaultPageSettings.Landscape = true; 
            printDoc.PrintPage += PrintDoc_PrintPage;
            currentPage = 1;

            PrintPreviewDialog preview = new PrintPreviewDialog
            {
                Document = printDoc,
                Width = 1200,
                Height = 800
            };
            preview.ShowDialog();
        }

       private void btnImprimir_Click(object sender, EventArgs e)
{
    PrintDocument printDoc = new PrintDocument();

    // Configurar impresión horizontal
    printDoc.DefaultPageSettings.Landscape = true;

    printDoc.PrintPage += PrintDoc_PrintPage;
    
    PrintPreviewDialog preview = new PrintPreviewDialog();
    preview.Document = printDoc;
    preview.WindowState = FormWindowState.Maximized;
    preview.ShowDialog();
}

private void PrintDoc_PrintPage(object sender, PrintPageEventArgs e)
{
    // ================= HEADER =================
    Bitmap headerBitmap = new Bitmap(headerPanel.Width, headerPanel.Height);
    headerPanel.DrawToBitmap(headerBitmap, new Rectangle(0, 0, headerPanel.Width, headerPanel.Height));

    // ================= FRANJA BLANCA SIN BOTÓN =================
    Bitmap topBitmap = new Bitmap(topPanel.Width, topPanel.Height);
    foreach (Control c in topPanel.Controls) c.Visible = false;
    topPanel.DrawToBitmap(topBitmap, new Rectangle(0, 0, topPanel.Width, topPanel.Height));
    foreach (Control c in topPanel.Controls) c.Visible = true;

    // ================= CONTENIDO (TABLAS + GRÁFICO) =================
    SplitContainer split = (SplitContainer)mainLayout.Controls[2];
    Panel panelIzq = split.Panel1;
    Panel panelDer = split.Panel2;

    // Calcular tamaño de contenido: ancho suma de tabla + gráfico, altura máxima
    int contentWidth = panelIzq.Width + panelDer.Width + 20; // 20px de separación
    int contentHeight = Math.Max(
        panelIzq.Controls.Cast<Control>().Sum(c => c.Height + 20), 
        chartTiempos.Height
    );

    Bitmap contentBitmap = new Bitmap(contentWidth, contentHeight);
    using (Graphics g = Graphics.FromImage(contentBitmap))
    {
        g.Clear(Color.White);

        // Dibujar tablas (lado izquierdo)
        int yOffsetIzq = 0;
        foreach (Control c in panelIzq.Controls)
        {
            Bitmap bmp = new Bitmap(c.Width, c.Height);
            c.DrawToBitmap(bmp, new Rectangle(0, 0, c.Width, c.Height));
            g.DrawImage(bmp, 0, yOffsetIzq);
            yOffsetIzq += c.Height + 20;
        }

        // Dibujar gráfico al lado derecho
        Bitmap chartBitmap = new Bitmap(chartTiempos.Width, chartTiempos.Height);
        chartTiempos.DrawToBitmap(chartBitmap, new Rectangle(0, 0, chartTiempos.Width, chartTiempos.Height));
        g.DrawImage(chartBitmap, panelIzq.Width + 20, 0); // +20px separación
    }

    // ================= COMBINAR TODO =================
    int totalWidth = Math.Max(Math.Max(headerBitmap.Width, topBitmap.Width), contentBitmap.Width);
    int totalHeight = headerBitmap.Height + topBitmap.Height + contentBitmap.Height + 40;

    Bitmap printBitmap = new Bitmap(totalWidth, totalHeight);
    using (Graphics g = Graphics.FromImage(printBitmap))
    {
        g.Clear(Color.White);
        int yOffset = 0;
        g.DrawImage(headerBitmap, 0, yOffset);
        yOffset += headerBitmap.Height;
        g.DrawImage(topBitmap, 0, yOffset);
        yOffset += topBitmap.Height + 10;
        g.DrawImage(contentBitmap, 0, yOffset);
    }

   // ================= ESCALAR Y DIBUJAR EN HOJA HORIZONTAL =================
e.PageSettings.Landscape = true; // forzar horizontal

Rectangle printableArea = e.MarginBounds; // <-- es int, no float

float scaleX = (float)printableArea.Width / printBitmap.Width;
float scaleY = (float)printableArea.Height / printBitmap.Height;
float scale = Math.Min(scaleX, scaleY);

int scaledWidth = (int)(printBitmap.Width * scale);
int scaledHeight = (int)(printBitmap.Height * scale);

// Centrar horizontalmente
int xOffset = printableArea.X + (printableArea.Width - scaledWidth) / 2;
int yOffsetPrint = printableArea.Y;

e.Graphics.DrawImage(printBitmap, xOffset, yOffsetPrint, scaledWidth, scaledHeight);


    // ================= NUMERO DE PAGINA =================
    string pageText = $"Pag. {currentPage}";
    using (Font pageFont = new Font("Segoe UI", 9))
    {
        SizeF textSize = e.Graphics.MeasureString(pageText, pageFont);
        float x = printableArea.Right - textSize.Width;
        float y = printableArea.Bottom + 10;
        e.Graphics.DrawString(pageText, pageFont, Brushes.Black, x, y);
    }

    currentPage++;
    e.HasMorePages = false;
}




        private Panel CrearPanelTecnico(TiempoTecnico t)
        {
            Panel panel = new Panel()
            {
                Width = 760,
                Height = 220,
                BorderStyle = BorderStyle.None,
                BackColor = ColorTranslator.FromHtml("#FFD700"),
                Padding = new Padding(2)
            };

            Panel contInterno = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            panel.Controls.Add(contInterno);

            Label lblHeader = new Label()
            {
                Text = $"Técnico encargado: {t.Tecnico}",
                Dock = DockStyle.Top,
                Height = 32,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = ColorTranslator.FromHtml("#002060"),
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 11, FontStyle.Bold),
                Padding = new Padding(10, 0, 0, 0)
            };
            contInterno.Controls.Add(lblHeader);

            DataGridView dgv = new DataGridView()
            {
                Dock = DockStyle.Top,
                Height = 150,
                ReadOnly = true,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                GridColor = Color.LightGray,
                BorderStyle = BorderStyle.None
            };

            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#002060");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 10, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 30;
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.Font = new Font("Century Gothic", 10);

            dgv.Columns.Add("Tipo", "Tipo de equipo");
            dgv.Columns.Add("Cantidad", "Cantidad");
            dgv.Columns.Add("HorasTotales", "Horas (totales)");
            dgv.Columns.Add("Promedio", "Promedio (hrs)");

            dgv.Columns[0].Width = 200;
            dgv.Columns[1].Width = 110;
            dgv.Columns[2].Width = 150;
            dgv.Columns[3].Width = 150;

            dgv.Rows.Add("Laptop", t.LaptopCount, t.LaptopHoras, t.Laptop);
            dgv.Rows.Add("Teléfono", t.TelefonoCount, t.TelefonoHoras, t.Telefono);
            dgv.Rows.Add("Impresora", t.ImpresoraCount, t.ImpresoraHoras, t.Impresora);

            contInterno.Controls.Add(dgv);

            return panel;
        }
    }

    public class TiempoTecnico
    {
        public string Tecnico { get; set; }

        public double LaptopHoras { get; set; }
        public int LaptopCount { get; set; }

        public double TelefonoHoras { get; set; }
        public int TelefonoCount { get; set; }

        public double ImpresoraHoras { get; set; }
        public int ImpresoraCount { get; set; }

        public double Laptop => LaptopCount == 0 ? 0 : Math.Round(LaptopHoras / LaptopCount, 2);
        public double Telefono => TelefonoCount == 0 ? 0 : Math.Round(TelefonoHoras / TelefonoCount, 2);
        public double Impresora => ImpresoraCount == 0 ? 0 : Math.Round(ImpresoraHoras / ImpresoraCount, 2);

        public TiempoTecnico(string tecnico,
            double laptopHoras, int laptopCount,
            double telefonoHoras, int telefonoCount,
            double impresoraHoras, int impresoraCount)
        {
            Tecnico = tecnico;
            LaptopHoras = laptopHoras;
            LaptopCount = laptopCount;
            TelefonoHoras = telefonoHoras;
            TelefonoCount = telefonoCount;
            ImpresoraHoras = impresoraHoras;
            ImpresoraCount = impresoraCount;
        }
    }
}
