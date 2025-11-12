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
        private Panel contenedorReporte;
        private Panel headerPanel;
        private Label lblTitulo;
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
            Width = 1300;
            Height = 700;
            BackColor = Color.White;
            StartPosition = FormStartPosition.CenterScreen;

            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            Controls.Add(contenedorReporte);

            // ---------- ENCABEZADO ----------
            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 120,
                BackColor = ColorTranslator.FromHtml("#002060")
            };
            contenedorReporte.Controls.Add(headerPanel);

            PictureBox logo = new PictureBox()
            {
                Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\..\\src\\login\\Image\\logo_g.jpg")),
                SizeMode = PictureBoxSizeMode.Zoom,
                Location = new Point(20, 10),
                Size = new Size(120, 100)
            };
            headerPanel.Controls.Add(logo);

            lblTitulo = new Label()
            {
                Text = "Tiempo promedio en reparación",
                Font = new Font("Century Gothic", 20, FontStyle.Bold),
                ForeColor = Color.White,
                Location = new Point(180, 25),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblTitulo);

            lblPeriodo = new Label()
            {
                Text = "Periodo: (25/09/25 - 25/10/25)",
                Font = new Font("Century Gothic", 10, FontStyle.Italic),
                ForeColor = Color.White,
                Location = new Point(180, 70),
                AutoSize = true
            };
            headerPanel.Controls.Add(lblPeriodo);

            // ---------- CONTENEDOR PRINCIPAL ----------
            SplitContainer splitContainer = new SplitContainer()
            {
                Dock = DockStyle.Fill,
                Orientation = Orientation.Vertical,
                SplitterDistance = 750
            };
            contenedorReporte.Controls.Add(splitContainer);

            Panel panelIzquierdo = new Panel()
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                BackColor = Color.White
            };
            splitContainer.Panel1.Controls.Add(panelIzquierdo);

            Panel panelDerecho = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White
            };
            splitContainer.Panel2.Controls.Add(panelDerecho);

            // ---------- GRÁFICO ----------
            chartTiempos = new Chart() { Dock = DockStyle.Fill, BackColor = Color.White };
            ChartArea chartArea = new ChartArea("Main");
            chartArea.AxisX.MajorGrid.Enabled = false;
            chartArea.AxisY.MajorGrid.LineColor = Color.LightGray;
            chartArea.AxisX.LabelStyle.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            chartArea.AxisY.LabelStyle.Font = new Font("Century Gothic", 9);
            chartArea.AxisY.Title = "Tiempo total (hrs)";
            chartArea.AxisY.TitleFont = new Font("Century Gothic", 10, FontStyle.Bold);
            chartArea.AxisX.Title = "Técnicos encargados";
            chartArea.AxisX.TitleFont = new Font("Century Gothic", 10, FontStyle.Bold);
            chartArea.BackColor = Color.White;
            chartTiempos.ChartAreas.Add(chartArea);
            panelDerecho.Controls.Add(chartTiempos);

            Series serie = new Series("Totales")
            {
                ChartType = SeriesChartType.Column,
                Font = new Font("Century Gothic", 9, FontStyle.Bold),
                IsValueShownAsLabel = true
            };
            chartTiempos.Series.Add(serie);

            // ---------- DATOS ----------
            listaTiempos = new List<TiempoTecnico>
            {
                new TiempoTecnico("Raul Padilla", 4, 2, 6),
                new TiempoTecnico("Marcos Turcios", 3, 4, 2),
                new TiempoTecnico("Alejandra Pineda", 5, 1, 1)
            };

            int y = 10;
            foreach (var t in listaTiempos)
            {
                Panel panelTecnico = CrearPanelTecnico(t);
                panelTecnico.Location = new Point(10, y);
                panelIzquierdo.Controls.Add(panelTecnico);
                y += panelTecnico.Height + 20;
            }

            // ---------- GRÁFICO DE TOTALES ----------
            chartTiempos.Series["Totales"].Points.Clear();
            chartTiempos.Series["Totales"].Points.AddXY("Raul Padilla", 12);
            chartTiempos.Series["Totales"].Points.AddXY("Marcos Turcios", 9);
            chartTiempos.Series["Totales"].Points.AddXY("Alejandra Pineda", 7);

            chartTiempos.Series["Totales"].Points[0].Color = ColorTranslator.FromHtml("#002060");
            chartTiempos.Series["Totales"].Points[1].Color = ColorTranslator.FromHtml("#FFD700");
            chartTiempos.Series["Totales"].Points[2].Color = ColorTranslator.FromHtml("#0070C0");

            chartTiempos.Titles.Add("Tiempo total de los técnicos encargados");
            chartTiempos.Titles[0].Font = new Font("Century Gothic", 12, FontStyle.Bold);
            chartTiempos.Titles[0].ForeColor = ColorTranslator.FromHtml("#002060");
        }

        private Panel CrearPanelTecnico(TiempoTecnico t)
        {
            Panel panel = new Panel()
            {
                Width = 720,
                Height = 180,
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
                Height = 30,
                TextAlign = ContentAlignment.MiddleLeft,
                BackColor = ColorTranslator.FromHtml("#002060"),
                ForeColor = Color.White,
                Font = new Font("Century Gothic", 10, FontStyle.Bold),
                Padding = new Padding(10, 0, 0, 0)
            };
            contInterno.Controls.Add(lblHeader);

            // Crear DataGridView
            DataGridView dgv = new DataGridView()
            {
                Dock = DockStyle.Top,
                Height = 110,
                ReadOnly = true,
                AllowUserToAddRows = false,
                AllowUserToDeleteRows = false,
                RowHeadersVisible = false,
                BackgroundColor = Color.White,
                GridColor = Color.LightGray,
                BorderStyle = BorderStyle.None
            };
            dgv.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#002060");
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Century Gothic", 9, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 32;
            dgv.EnableHeadersVisualStyles = false;
            dgv.DefaultCellStyle.Font = new Font("Century Gothic", 9, FontStyle.Regular);
            dgv.DefaultCellStyle.SelectionBackColor = ColorTranslator.FromHtml("#E6E6E6");
            dgv.DefaultCellStyle.SelectionForeColor = Color.Black;

            dgv.Columns.Add("Tipo", "Tipo de equipo");
            dgv.Columns.Add("Tiempo", "Tiempo promedio (hrs)");
            dgv.Columns[0].Width = 300;
            dgv.Columns[1].Width = 200;

            dgv.Rows.Add("Laptop", t.Laptop);
            dgv.Rows.Add("Teléfono", t.Telefono);
            dgv.Rows.Add("Impresora", t.Impresora);

            contInterno.Controls.Add(dgv);

            Label lblTotal = new Label()
            {
                Text = $"Total: {t.Total} hrs",
                Dock = DockStyle.Bottom,
                Height = 25,
                Font = new Font("Century Gothic", 9, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleRight,
                Padding = new Padding(0, 0, 10, 0)
            };
            contInterno.Controls.Add(lblTotal);

            return panel;
        }
    }

    public class TiempoTecnico
    {
        public string Tecnico { get; set; }
        public double Laptop { get; set; }
        public double Telefono { get; set; }
        public double Impresora { get; set; }
        public double Total => Laptop + Telefono + Impresora;

        public TiempoTecnico(string tecnico, double laptop, double telefono, double impresora)
        {
            Tecnico = tecnico;
            Laptop = laptop;
            Telefono = telefono;
            Impresora = impresora;
        }
    }
}
