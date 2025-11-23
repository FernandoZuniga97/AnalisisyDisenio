using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing; // Importante para impresión

namespace MyWinFormsApp
{
    public class RDdispositivosnorepaForm : Form
    {
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

        private List<Norepa> lista;
        private int contadorID = 4;

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

            // Remover CellPainting
            // dgDnRepa.CellPainting += (s, ev) => { ... };

            Panel panelTabla = new Panel() { Dock = DockStyle.Fill };
            panelTabla.Controls.Add(dgDnRepa);
            //-------------
            // Propiedades base para los botones
            var buttonBaseStyle = new Font("Segoe UI", 9, FontStyle.Bold);
            var buttonWidth = 120;
            var buttonHeight = 30;
            // Usamos un margen uniforme (por ejemplo, 0, 0, 10, 0) para separarlos
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
            lista = new List<Norepa>()
            {
            new Norepa { ID="NR-001", FechadeIngreso="27/08/2025", Dispositivo="Samsung S23 Plus", Cliente="Ana García", TecnicoAsignado="M. Pérez", DescripciondelDano="Revisión general y limpieza", CostoEstimado=2500m, Observaciones="Cliente decidió no continuar con la reparación por el costo elevado" },
            new Norepa { ID="NR-002", FechadeIngreso="27/08/2025", Dispositivo="iPhone 14 Pro", Cliente="Carlos Mejía", TecnicoAsignado="D. López", DescripciondelDano="Botón Home sin respuesta", CostoEstimado=180m, Observaciones="No se encontró repuesto disponible en el mercado" },
            new Norepa { ID="NR-003", FechadeIngreso="27/08/2025", Dispositivo="iPhone 13", Cliente="Luis Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Batería se descarga muy rápido", CostoEstimado=600m, Observaciones="Cliente no aprobó el cambio de batería" },
            new Norepa { ID="NR-004", FechadeIngreso="27/08/2025", Dispositivo="Xiaomi Redmi Note 11", Cliente="Pedro López", TecnicoAsignado="L. Reyes", DescripciondelDano="Puerto de carga dañado", CostoEstimado=120m, Observaciones="Pieza no disponible en inventario" },
            new Norepa { ID="NR-005", FechadeIngreso="27/08/2025", Dispositivo="Motorola G Stylus", Cliente="María Soto", TecnicoAsignado="F. Cabrera", DescripciondelDano="Pantalla rota", CostoEstimado=850m, Observaciones="Cliente retiró el equipo sin autorización de reparación" },
            new Norepa { ID="NR-006", FechadeIngreso="27/08/2025", Dispositivo="Huawei P40 Lite", Cliente="José Molina", TecnicoAsignado="M. Pérez", DescripciondelDano="Cámara trasera dañada", CostoEstimado=400m, Observaciones="Proveedor no entregó el repuesto solicitado" },
            new Norepa { ID="NR-007", FechadeIngreso="27/08/2025", Dispositivo="Samsung A54", Cliente="Karla Ruiz", TecnicoAsignado="L. Reyes", DescripciondelDano="Altavoz no funciona", CostoEstimado=220m, Observaciones="Cliente no regresó para aprobación del presupuesto" },
            new Norepa { ID="NR-008", FechadeIngreso="28/08/2025", Dispositivo="Xiaomi Redmi Note 12", Cliente="Pedro López", TecnicoAsignado="D. López", DescripciondelDano="Pantalla táctil intermitente", CostoEstimado=310m, Observaciones="El repuesto no es compatible con el modelo exacto" },
            new Norepa { ID="NR-009", FechadeIngreso="29/08/2025", Dispositivo="iPhone 12 Mini", Cliente="Carmen Díaz", TecnicoAsignado="M. Pérez", DescripciondelDano="Micrófono principal sin sonido", CostoEstimado=270m, Observaciones="Cliente no aprobó la reparación por demora en entrega" },
            new Norepa { ID="NR-010", FechadeIngreso="28/08/2025", Dispositivo="Samsung Galaxy A32", Cliente="Antonio Rivera", TecnicoAsignado="F. Cabrera", DescripciondelDano="No enciende", CostoEstimado=180m, Observaciones="Placa base irreparable según diagnóstico técnico" },
            new Norepa { ID="NR-011", FechadeIngreso="30/08/2025", Dispositivo="Oppo Reno 7", Cliente="Rosa Martínez", TecnicoAsignado="L. Reyes", DescripciondelDano="Cámara frontal dañada", CostoEstimado=350m, Observaciones="El cliente decidió no invertir en la reparación" },
            new Norepa { ID="NR-012", FechadeIngreso="29/08/2025", Dispositivo="Realme 9 Pro", Cliente="Mario Aguilar", TecnicoAsignado="D. López", DescripciondelDano="Conector de carga flojo", CostoEstimado=200m, Observaciones="El repuesto llegó defectuoso, reparación cancelada" },
            new Norepa { ID="NR-013", FechadeIngreso="27/09/2025", Dispositivo="Xiaomi Poco X5", Cliente="Lucía Navarro", TecnicoAsignado="M. Pérez", DescripciondelDano="No detecta SIM", CostoEstimado=190m, Observaciones="El problema es de placa y no se dispone de repuesto" },
            new Norepa { ID="NR-014", FechadeIngreso="14/09/2025", Dispositivo="Samsung S21 FE", Cliente="Daniel Castro", TecnicoAsignado="F. Cabrera", DescripciondelDano="Batería inflada", CostoEstimado=320m, Observaciones="Cliente no autorizó el reemplazo por costo elevado" },
            new Norepa { ID="NR-015", FechadeIngreso="08/09/2025", Dispositivo="iPhone XR", Cliente="Andrea Méndez", TecnicoAsignado="L. Reyes", DescripciondelDano="Pantalla no responde", CostoEstimado=480m, Observaciones="Falta de disponibilidad de pantalla original" },
            new Norepa { ID="NR-016", FechadeIngreso="10/09/2025", Dispositivo="Xiaomi Redmi 10C", Cliente="Pedro López", TecnicoAsignado="D. López", DescripciondelDano="Puerto USB dañado", CostoEstimado=150m, Observaciones="Cliente no aceptó reparación temporal" },
            new Norepa { ID="NR-017", FechadeIngreso="12/09/2025", Dispositivo="Samsung A13", Cliente="José Molina", TecnicoAsignado="M. Pérez", DescripciondelDano="No carga", CostoEstimado=200m, Observaciones="El repuesto no se consigue en el país" },
            new Norepa { ID="NR-018", FechadeIngreso="14/09/2025", Dispositivo="iPhone 11", Cliente="Carla Ramos", TecnicoAsignado="F. Cabrera", DescripciondelDano="Cristal trasero roto", CostoEstimado=900m, Observaciones="Cliente decidió reemplazar el equipo" },
            new Norepa { ID="NR-019", FechadeIngreso="17/09/2025", Dispositivo="Huawei Nova 10", Cliente="Luis Torres", TecnicoAsignado="D. López", DescripciondelDano="Problema de audio", CostoEstimado=230m, Observaciones="Reparación no autorizada por falta de repuesto" },
            new Norepa { ID="NR-020", FechadeIngreso="20/09/2025", Dispositivo="Xiaomi Redmi Note 13", Cliente="María Soto", TecnicoAsignado="M. Pérez", DescripciondelDano="Pantalla con líneas verticales", CostoEstimado=310m, Observaciones="Proveedor canceló el envío del repuesto" },
            new Norepa { ID="NR-021", FechadeIngreso="22/09/2025", Dispositivo="Motorola Edge 30", Cliente="Carlos Mejía", TecnicoAsignado="L. Reyes", DescripciondelDano="No detecta Wi-Fi", CostoEstimado=250m, Observaciones="Cliente no aprobó la reparación por tiempo de espera" },
            new Norepa { ID="NR-022", FechadeIngreso="25/09/2025", Dispositivo="Samsung S24 Ultra", Cliente="Pedro López", TecnicoAsignado="F. Cabrera", DescripciondelDano="Vidrio agrietado", CostoEstimado=950m, Observaciones="No se consiguió pantalla compatible" },
            new Norepa { ID="NR-023", FechadeIngreso="27/09/2025", Dispositivo="iPhone 15", Cliente="Ana García", TecnicoAsignado="D. López", DescripciondelDano="Problema de encendido", CostoEstimado=700m, Observaciones="Cliente canceló la reparación antes del inicio" },
            new Norepa { ID="NR-024", FechadeIngreso="12/09/2025", Dispositivo="Xiaomi Redmi 13C", Cliente="Pedro López", TecnicoAsignado="L. Reyes", DescripciondelDano="No reconoce carga rápida", CostoEstimado=210m, Observaciones="El equipo no pudo ser abierto sin riesgo" },
            new Norepa { ID="NR-025", FechadeIngreso="19/09/2025", Dispositivo="Realme GT Neo", Cliente="Lucía Navarro", TecnicoAsignado="M. Pérez", DescripciondelDano="Placa base dañada", CostoEstimado=1300m, Observaciones="Reparación no viable, placa no disponible" }

            };
            MostrarPagina();
        }

        // Reemplazo de ActualizarGrid por MostrarPagina para manejar la paginación
        private void MostrarPagina()
        {
            totalPaginas = (int)Math.Ceiling((double)lista.Count / registrosPorPagina);
            if (paginaActual < 1) paginaActual = 1;
            if (paginaActual > totalPaginas) paginaActual = totalPaginas;

            var registros = lista
                .Skip((paginaActual - 1) * registrosPorPagina)
                .Take(registrosPorPagina)
                .ToList();

            dgDnRepa.DataSource = null;
            dgDnRepa.DataSource = registros;
            dgDnRepa.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            // Nombres de encabezado
            if (dgDnRepa.Columns.Count > 0)
            {
                dgDnRepa.Columns["ID"].HeaderText = "Código";
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

            // Ajuste de anchos para DataGridView (similar al archivo original, pero usando el diccionario)
            var widths = new Dictionary<string, int>
            {
                ["ID"] = 80,
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
            int tablaWidth = dgDnRepa.Columns.Cast<DataGridViewColumn>().Sum(c => c.Width);
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
                    MessageBox.Show("Todos los campos son obligatorios excepto Observaciones.");
                    return;
                }

                if (!decimal.TryParse(txtCostoEstimado.Text.Replace("L", "").Replace(",", "").Trim(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números válidos en el costo estimado.");
                    return;
                }

                contadorID++;
                string nuevoID = $"NR-{contadorID.ToString("D3")}";

                lista.Add(new Norepa()
                {
                    ID = nuevoID,
                    FechadeIngreso = txtFechadeIngreso.Text,
                    Dispositivo = txtDispositivo.Text,
                    Cliente = txtCliente.Text,
                    TecnicoAsignado = txtTecnicoAsignado.Text,
                    DescripciondelDano = txtDescripciondelDano.Text,
                    CostoEstimado = costo,
                    Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text
                });

                MostrarPagina();
                formAgregar.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblFechadeIngreso, txtFechadeIngreso, lblDispositivo, txtDispositivo, lblCliente, txtCliente,
                lblTecnicoAsignado, txtTecnicoAsignado, lblDescripciondelDano, txtDescripciondelDano, lblCostoEstimado, txtCostoEstimado,
                lblObs, txtObs, btnGuardar });
            formAgregar.ShowDialog();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgDnRepa.CurrentRow != null)
            {
                // Obtener el registro de la lista subyacente
                var registroVisible = (Norepa)dgDnRepa.CurrentRow.DataBoundItem;
                lista.Remove(registroVisible);

                DialogResult result = MessageBox.Show($"¿Está seguro de borrar el registro '{registroVisible.ID}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    MostrarPagina();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgDnRepa.CurrentRow == null) return;

            // Obtener el objeto Norepa de la fila seleccionada
            Norepa norepa = (Norepa)dgDnRepa.CurrentRow.DataBoundItem;

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
            TextBox txtCostoEstimado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.CostoEstimado.ToString() };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Observaciones };
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
                    MessageBox.Show("Todos los campos son obligatorios excepto Observaciones.");
                    return;
                }

                if (!decimal.TryParse(txtCostoEstimado.Text.Replace("L", "").Replace(",", "").Trim(), NumberStyles.Currency, CultureInfo.InvariantCulture, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números válidos en el costo estimado.");
                    return;
                }

                norepa.FechadeIngreso = txtFechadeIngreso.Text;
                norepa.Dispositivo = txtDispositivo.Text;
                norepa.Cliente = txtCliente.Text;
                norepa.TecnicoAsignado = txtTecnicoAsignado.Text;
                norepa.DescripciondelDano = txtDescripciondelDano.Text;
                norepa.CostoEstimado = costo;
                norepa.Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text;

                MostrarPagina();
                formEditar.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblFechadeIngreso, txtFechadeIngreso, lblDispositivo, txtDispositivo, lblCliente, txtCliente,
                lblTecnicoAsignado, txtTecnicoAsignado, lblDescripciondelDano, txtDescripciondelDano, lblCostoEstimado, txtCostoEstimado,
                lblObs, txtObs, btnGuardar });

            formEditar.ShowDialog();
        }

        // Se elimina el método ActualizarGrid y se usa MostrarPagina en su lugar.

    }

    public class Norepa
    {
        public string ID { get; set; }
        public string FechadeIngreso { get; set; }
        public string Dispositivo { get; set; }
        public string Cliente { get; set; }
        public string TecnicoAsignado { get; set; }
        public string DescripciondelDano { get; set; }
        public decimal CostoEstimado { get; set; }
        public string Observaciones { get; set; }
    }
}