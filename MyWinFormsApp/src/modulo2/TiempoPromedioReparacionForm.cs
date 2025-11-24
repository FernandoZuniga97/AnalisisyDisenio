using System;
using System.Collections.Generic;
using System.Drawing;
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
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblPeriodo;
        private Chart chartTiempos;

        private List<TiempoTecnico> listaTiempos;

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
                RowCount = 2
            };
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Absolute, 140));
            mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Controls.Add(mainLayout);

            // ---------- ENCABEZADO ----------
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

            // ---------- CONTENIDO ----------
            SplitContainer splitContainer = new SplitContainer()
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 820
            };
            mainLayout.Controls.Add(splitContainer, 0, 1);

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

            // COLORES
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

            chartTiempos.Titles.Add("Promedio por tipo de dispositivo");
            chartTiempos.Titles[0].Font = new Font("Century Gothic", 13, FontStyle.Bold);
            chartTiempos.Titles[0].ForeColor = azul;
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

            // ---------- TABLA ----------
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

    // ---------- CLASE CON HORAS Y PROMEDIOS ----------
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
