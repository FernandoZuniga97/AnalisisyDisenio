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
    public class DispositivosnRyRForm : Form
    {
        private List<DispositivosnRyR> lista;

        private Panel contenedorReporte;
        private Panel headerPanel;
        private Panel separatorLine;
        private Panel contentPanel;
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblPeriodo; // Cambiado de lblFecha a lblPeriodo
        private Label lblTotal;
        private DataGridView dgvFallas;
        private Chart chartFallas;
        private SplitContainer splitContainer;
        private Button btnExportar;
        private ComboBox cmbMes; // Nuevo ComboBox para Mes

        public DispositivosnRyRForm()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            // ------------------
            // FORM
            // ------------------
            Text = "Dispositivos no reparados y reparados";
            Width = 1250;
            Height = 700;
            BackColor = Color.FromArgb(242, 242, 242);
            TopLevel = false;
            Dock = DockStyle.Fill;
            FormBorderStyle = FormBorderStyle.None;
            ControlBox = false;

            // ------------------
            // CONTENEDOR PRINCIPAL
            // ------------------
            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                AutoScroll = true,
                Padding = new Padding(8)
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

            separatorLine = new Panel()
            {
                Height = 17,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 15, 0, 15)
            };
            contenedorReporte.Controls.Add(separatorLine);
            separatorLine.BringToFront();
            // ------------------
            // BOTÓN EXPORTAR A PDF / IMPRIMIR (Implementación similar a TipoDeFallasForm)
            // ------------------
            btnExportar = new Button()
            {
                Text = "Imprimir Reporte",
                Dock = DockStyle.Bottom,
                Height = 30, // Reducido para mejor look and feel
                BackColor = ColorTranslator.FromHtml("#009933"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnExportar.FlatAppearance.BorderSize = 0;
            btnExportar.Click += BtnExportar_Click;
            contenedorReporte.Controls.Add(btnExportar);
            //btnExportar.BringToFront();
            contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                Padding = new Padding(0, 15, 0, 15),
            };

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
            titlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            PictureBox logo = new PictureBox()
            {
                Image = Image.FromFile(
                Path.Combine(
                    AppDomain.CurrentDomain.BaseDirectory,
                        "..\\..\\..\\src\\login\\Image\\logo_g.jpg"
                    )
                ),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Left,
                Margin = new Padding(25, 0, 0, 0)
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
                Text = "Dispositivos Reparados y No Reparados",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblPeriodo = new Label() // Usando lblPeriodo
            {
                Text = "",
                Font = new Font("Segoe UI", 10, FontStyle.Italic),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 30
            };


            textoPanel.Controls.Add(lblPeriodo); // Agregar lblPeriodo
            textoPanel.Controls.Add(lblSubtitulo);
            textoPanel.Controls.Add(lblTitulo);
            titlePanel.Controls.Add(textoPanel, 1, 0);
            headerPanel.Controls.Add(titlePanel);


            // ------------------
            // COMBOBOX MES (Implementación similar a cmbTrimestre de TipoDeFallasForm)
            // ------------------
            cmbMes = new ComboBox()
            {
                Width = 160,
                DropDownStyle = ComboBoxStyle.DropDownList,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            cmbMes.Items.AddRange(new string[]
            {
                "Enero", "Febrero", "Marzo", "Abril", "Mayo", "Junio",
                "Julio", "Agosto", "Septiembre", "Octubre", "Noviembre", "Diciembre"
            });
            cmbMes.SelectedIndex = 0;

            cmbMes.Top = 10;
            cmbMes.Left = headerPanel.Width - cmbMes.Width - 20;
            cmbMes.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            cmbMes.BackColor = Color.White;
            cmbMes.ForeColor = Color.Black;
            cmbMes.FlatStyle = FlatStyle.Flat;
            headerPanel.Controls.Add(cmbMes);
            cmbMes.BringToFront();

            headerPanel.Resize += (s, e) =>
            {
                cmbMes.Left = headerPanel.Width - cmbMes.Width - 20;
            };

            cmbMes.SelectedIndexChanged += CmbMes_SelectedIndexChanged;

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
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                //CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal,
                GridColor = Color.White, // Estilo de TipoDeFallasForm
                AllowUserToResizeColumns = false,
                AllowUserToResizeRows = false,
                AllowUserToOrderColumns = false,
                ScrollBars = ScrollBars.Vertical, // Cambiado a Vertical para paginación
                CellBorderStyle = DataGridViewCellBorderStyle.Single
            };
            dgvFallas.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvFallas.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvFallas.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvFallas.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgvFallas.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            dgvFallas.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");

            dgvFallas.Paint += (s, e) => // Estilo de TipoDeFallasForm
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
            // CHART (Modificado a Pie Chart)
            // ------------------
            chartFallas = new Chart() { Dock = DockStyle.Fill, BackColor = Color.White };
            ChartArea chartArea = new ChartArea("ChartArea1");
            // Eliminar propiedades de ejes que no aplican a Pie Chart
            chartFallas.ChartAreas.Add(chartArea);

            Title chartTitle = new Title("Distribución Mensual");
            chartFallas.Titles.Add(chartTitle);


            Series serie = new Series("Frecuencia")
            {
                ChartType = SeriesChartType.Pie, // *** CAMBIADO A PIE CHART ***
                Font = new Font("Arial", 9, FontStyle.Bold),
                IsValueShownAsLabel = true,
                LabelFormat = "{0} ({P})", // Mostrar valor y porcentaje
                LegendText = "#VALX", // Usar nombre del punto (Tipo) como leyenda
            };
            chartFallas.Series.Add(serie);
            splitContainer.Panel2.Controls.Add(chartFallas);



            Load += DispositivosnRyRForm_Load;
        }

        private void DispositivosnRyRForm_Load(object sender, EventArgs e)
        {
            // *** NUEVOS DATOS SIMULADOS POR MES (Solo 2 tipos: No Reparables y Reparables) ***
            lista = new List<DispositivosnRyR>
            {
                // Enero
                new DispositivosnRyR { Numero = 1, Tipo = "No Reparables", Frecuencia = 15, Fecha = new DateTime(DateTime.Now.Year, 1, 1) },
                new DispositivosnRyR { Numero = 2, Tipo = "Reparables", Frecuencia = 35, Fecha = new DateTime(DateTime.Now.Year, 1, 1) },
                // Febrero
                new DispositivosnRyR { Numero = 3, Tipo = "No Reparables", Frecuencia = 10, Fecha = new DateTime(DateTime.Now.Year, 2, 1) },
                new DispositivosnRyR { Numero = 4, Tipo = "Reparables", Frecuencia = 40, Fecha = new DateTime(DateTime.Now.Year, 2, 1) },
                // Marzo
                new DispositivosnRyR { Numero = 5, Tipo = "No Reparables", Frecuencia = 20, Fecha = new DateTime(DateTime.Now.Year, 3, 1) },
                new DispositivosnRyR { Numero = 6, Tipo = "Reparables", Frecuencia = 30, Fecha = new DateTime(DateTime.Now.Year, 3, 1) },
                // Abril
                new DispositivosnRyR { Numero = 7, Tipo = "No Reparables", Frecuencia = 12, Fecha = new DateTime(DateTime.Now.Year, 4, 1) },
                new DispositivosnRyR { Numero = 8, Tipo = "Reparables", Frecuencia = 45, Fecha = new DateTime(DateTime.Now.Year, 4, 1) },
                // Mayo
                new DispositivosnRyR { Numero = 9, Tipo = "No Reparables", Frecuencia = 8, Fecha = new DateTime(DateTime.Now.Year, 5, 1) },
                new DispositivosnRyR { Numero = 10, Tipo = "Reparables", Frecuencia = 32, Fecha = new DateTime(DateTime.Now.Year, 5, 1) },
                // Junio
                new DispositivosnRyR { Numero = 11, Tipo = "No Reparables", Frecuencia = 18, Fecha = new DateTime(DateTime.Now.Year, 6, 1) },
                new DispositivosnRyR { Numero = 12, Tipo = "Reparables", Frecuencia = 28, Fecha = new DateTime(DateTime.Now.Year, 6, 1) },
                // Julio
                new DispositivosnRyR { Numero = 13, Tipo = "No Reparables", Frecuencia = 16, Fecha = new DateTime(DateTime.Now.Year, 7, 1) },
                new DispositivosnRyR { Numero = 14, Tipo = "Reparables", Frecuencia = 38, Fecha = new DateTime(DateTime.Now.Year, 7, 1) },
                // Agosto
                new DispositivosnRyR { Numero = 15, Tipo = "No Reparables", Frecuencia = 9, Fecha = new DateTime(DateTime.Now.Year, 8, 1) },
                new DispositivosnRyR { Numero = 16, Tipo = "Reparables", Frecuencia = 41, Fecha = new DateTime(DateTime.Now.Year, 8, 1) },
                // Septiembre
                new DispositivosnRyR { Numero = 17, Tipo = "No Reparables", Frecuencia = 22, Fecha = new DateTime(DateTime.Now.Year, 9, 1) },
                new DispositivosnRyR { Numero = 18, Tipo = "Reparables", Frecuencia = 25, Fecha = new DateTime(DateTime.Now.Year, 9, 1) },
                // Octubre
                new DispositivosnRyR { Numero = 19, Tipo = "No Reparables", Frecuencia = 14, Fecha = new DateTime(DateTime.Now.Year, 10, 1) },
                new DispositivosnRyR { Numero = 20, Tipo = "Reparables", Frecuencia = 36, Fecha = new DateTime(DateTime.Now.Year, 10, 1) },
                // Noviembre
                new DispositivosnRyR { Numero = 21, Tipo = "No Reparables", Frecuencia = 11, Fecha = new DateTime(DateTime.Now.Year, 11, 1) },
                new DispositivosnRyR { Numero = 22, Tipo = "Reparables", Frecuencia = 39, Fecha = new DateTime(DateTime.Now.Year, 11, 1) },
                // Diciembre
                new DispositivosnRyR { Numero = 23, Tipo = "No Reparables", Frecuencia = 17, Fecha = new DateTime(DateTime.Now.Year, 12, 1) },
                new DispositivosnRyR { Numero = 24, Tipo = "Reparables", Frecuencia = 33, Fecha = new DateTime(DateTime.Now.Year, 12, 1) }
            };

            // Mostrar el mes inicial
            CmbMes_SelectedIndexChanged(this, EventArgs.Empty);
        }

        private void CmbMes_SelectedIndexChanged(object sender, EventArgs e)
        {
            int month = cmbMes.SelectedIndex + 1;
            int year = DateTime.Now.Year;

            // Determinar el último día del mes
            DateTime inicio = new DateTime(year, month, 1);
            DateTime fin = new DateTime(year, month, DateTime.DaysInMonth(year, month));

            lblPeriodo.Text = $"Periodo: {cmbMes.SelectedItem} ({inicio:dd/MM/yyyy} - {fin:dd/MM/yyyy})";
            FiltrarPorMes(inicio, fin);
        }

        private void FiltrarPorMes(DateTime fechaInicio, DateTime fechaFin)
        {
            // Filtrar los datos que tienen la fecha de inicio del mes (un mes solo tiene 2 entradas)
            var filtrada = lista.Where(f => f.Fecha.Month == fechaInicio.Month && f.Fecha.Year == fechaInicio.Year).ToList();
            ActualizarTablaYGrafico(filtrada);
        }

        private void ActualizarTablaYGrafico(List<DispositivosnRyR> listaFiltrada = null)
        {
            var data = listaFiltrada ?? lista;

            dgvFallas.DataSource = null;

            // Crear lista con # de orden para el DataGridView (solo tendremos 2 filas)
            var listaOrdenada = data.OrderByDescending(f => f.Frecuencia)
                                    .Select((falla) => new
                                    {
                                        // Orden = index + 1,
                                        falla.Tipo,
                                        falla.Frecuencia
                                    }).ToList();

            dgvFallas.DataSource = listaOrdenada;

            // *** AJUSTE DE ENCABEZADOS DE COLUMNA ***
            // dgvFallas.Columns[0].HeaderText = "#";
            dgvFallas.Columns[0].HeaderText = "Categoría";
            dgvFallas.Columns[1].HeaderText = "Cantidad";

            // Colorear filas pares
            // Se usa RowPrePaint para aplicar el estilo correctamente después de enlazar los datos
            dgvFallas.RowPrePaint += (s, e) =>
            {
                var row = dgvFallas.Rows[e.RowIndex];
                row.DefaultCellStyle.BackColor = (e.RowIndex % 2 == 1) ? ColorTranslator.FromHtml("#F0F0F0") : Color.White;
            };

            // Ajustar altura de dgvFallas según su contenido
            dgvFallas.Height = dgvFallas.ColumnHeadersHeight + dgvFallas.Rows.Cast<DataGridViewRow>().Sum(r => r.Height);

            // Ajustar SplitterDistance dinámicamente
            splitContainer.SplitterDistance = dgvFallas.Height + lblTotal.Height + 10;

            // Actualizar gráfica
            chartFallas.Series["Frecuencia"].Points.Clear();

            // Usar una paleta de colores para el Pie Chart
            chartFallas.Series["Frecuencia"].Points.AddXY("No Reparables", data.Where(f => f.Tipo == "No Reparables").Sum(f => f.Frecuencia));
            chartFallas.Series["Frecuencia"].Points.AddXY("Reparables", data.Where(f => f.Tipo == "Reparables").Sum(f => f.Frecuencia));

            // Asignar colores específicos
            chartFallas.Series["Frecuencia"].Points[0].Color = Color.Gold; // Color para No Reparables
            chartFallas.Series["Frecuencia"].Points[1].Color = ColorTranslator.FromHtml("#002060");// Color para Reparables

            // Mostrar total
            int total = data.Sum(f => f.Frecuencia);
            lblTotal.Text = $"Total: {total} dispositivos en el mes";
        }

        private void BtnExportar_Click(object sender, EventArgs e)
        {
            // La implementación de impresión es idéntica a TipoDeFallasForm.cs
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
            // ... (Captura del Header se mantiene igual)
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

                // *** INICIO: CÓDIGO AÑADIDO PARA DIBUJAR LA LÍNEA GOLDEN BAJO EL ENCABEZADO ***
                // Color Golden: ColorTranslator.FromHtml("#E1E11F") y Grosor: 3
                using (Pen pen = new Pen(ColorTranslator.FromHtml("#E1E11F"), 3))
                {
                    // Dibuja la línea justo debajo de la altura de los encabezados (dgvFallas.ColumnHeadersHeight - 1)
                    g.DrawLine(pen, 0, dgvFallas.ColumnHeadersHeight - 1, dgvFallas.Width, dgvFallas.ColumnHeadersHeight - 1);
                }
                // *** FIN: CÓDIGO AÑADIDO PARA DIBUJAR LA LÍNEA GOLDEN ***

                int yPos = dgvFallas.ColumnHeadersHeight;
                foreach (DataGridViewRow row in dgvFallas.Rows)
                {
                    // ... (Lógica de dibujo de filas se mantiene igual)
                    xPos = 0;
                    for (int i = 0; i < row.Cells.Count; i++)
                    {
                        var cell = row.Cells[i];
                        Rectangle cellRect = new Rectangle(xPos, yPos, cell.OwningColumn.Width, row.Height);
                        // Usar la lógica de color de fila de TipoDeFallasForm (si la fila es impar usa LightGray)
                        // Usamos row.Index % 2 == 1 para coincidir con la lógica que queda tras la eliminación de la columna 'Orden'
                        using (Brush backBrush = new SolidBrush(row.Index % 2 == 1 ? ColorTranslator.FromHtml("#F0F0F0") : Color.White))
                            g.FillRectangle(backBrush, cellRect);
                        using (Brush foreBrush = new SolidBrush(cell.Style.ForeColor.IsEmpty ? Color.Black : cell.Style.ForeColor))
                        {
                            StringFormat sf = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                            g.DrawString(cell.FormattedValue?.ToString(), cell.InheritedStyle.Font, foreBrush, cellRect, sf);
                        }
                        g.DrawRectangle(Pens.White, cellRect); // Dibujar bordes de celda en blanco para simular el estilo
                        xPos += cell.OwningColumn.Width;
                    }
                    yPos += row.Height;
                }
            }

            // Capturar gráfico
            Bitmap chartBitmap = new Bitmap(chartFallas.Width, chartFallas.Height);
            chartFallas.DrawToBitmap(chartBitmap, new Rectangle(0, 0, chartFallas.Width, chartFallas.Height));

            // Combinar todo
            // Ya no necesitamos calcular el totalWidth/Height de esta manera compleja, 
            // ya que solo necesitamos dibujar los elementos en el orden correcto.

            // Establecer posiciones de dibujo en la página
            int currentY = e.MarginBounds.Top;
            int printAreaWidth = e.MarginBounds.Width;
            int printAreaLeft = e.MarginBounds.Left;

            // 1. Dibujar Encabezado
            e.Graphics.DrawImage(headerBitmap, printAreaLeft, currentY, printAreaWidth, headerBitmap.Height);
            currentY += headerBitmap.Height;

            // 2. Dibujar Separator Line
            // Usamos el color y la altura definidos en InitializeComponent (17px y Blanco)
            // Aunque el original tenía un margen, aquí lo dibujaremos como un bloque sólido
            int separatorHeight = separatorLine.Height; // 17
            Color separatorColor = separatorLine.BackColor; // Blanco
            using (Brush separatorBrush = new SolidBrush(separatorColor))
            {
                e.Graphics.FillRectangle(separatorBrush, printAreaLeft, currentY, printAreaWidth, separatorHeight);
            }
            // Considerar un pequeño espacio después de la línea
            currentY += separatorHeight + 5;

            // 3. Dibujar Tabla
            // Escalamos la tabla a lo ancho del área de impresión
            float dgvScale = (float)printAreaWidth / dgvBitmap.Width;
            int scaledDgvHeight = (int)(dgvBitmap.Height * dgvScale);
            e.Graphics.DrawImage(dgvBitmap, printAreaLeft, currentY, printAreaWidth, scaledDgvHeight);
            currentY += scaledDgvHeight + 10; // Espacio después de la tabla

            // 4. Dibujar Gráfico
            // Escalamos el gráfico para que no exceda el ancho
            float chartScale = (float)printAreaWidth / chartBitmap.Width;
            int scaledChartHeight = (int)(chartBitmap.Height * chartScale);
            e.Graphics.DrawImage(chartBitmap, printAreaLeft, currentY, printAreaWidth, scaledChartHeight);
            currentY += scaledChartHeight;

            // No es necesario calcular 'e.HasMorePages' ya que es un reporte corto.
        }
    }

    public class DispositivosnRyR
    {
        public int Numero { get; set; }
        public string Tipo { get; set; }
        public int Frecuencia { get; set; }
        public DateTime Fecha { get; set; } // Agregada la propiedad Fecha para el filtro mensual
    }
}