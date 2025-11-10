using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows.Forms;

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
        private List<Norepa> lista;
        private int contadorID = 4;

        public RDdispositivosnorepaForm()
        {
            Text = "Administración General - Inventario de Partes";
            Width = 1250;
            Height = 700;
            BackColor = Color.FromArgb(242, 242, 242);
            TopLevel = false; // Agregar esta línea
            Dock = DockStyle.Fill;
            //para quitar la basura del minimizar y demas
            this.FormBorderStyle = FormBorderStyle.None;
            this.ControlBox = false;   // opcional, asegura que no haya botones
            this.TopLevel = false;
            this.Dock = DockStyle.Fill;
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
                AutoScroll = true            // por si el contenido interno excede el espacio
            };

            separatorLine = new Panel()
            {
                Height = 3,
                BackColor = Color.White,
                Dock = DockStyle.Top,
                Margin = new Padding(0, 15, 0, 15)
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
            //---------------
            PictureBox logo = new PictureBox()
            {
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 100,
                Height = 100,
                Anchor = AnchorStyles.None,
                Dock = DockStyle.Left, // Esto centrará la imagen verticalmente dentro de su celda de 150x150
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
                    // If no image is found, continue without it
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
            //--------------
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

            dgDnRepa = new DataGridView()
            {
                Dock = DockStyle.Fill,
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill, //estaba en none
                ReadOnly = true,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                ColumnHeadersHeight = 35,
                EnableHeadersVisualStyles = false,
                RowHeadersVisible = false,
                DefaultCellStyle = { Alignment = DataGridViewContentAlignment.MiddleCenter },
                AllowUserToResizeColumns = false,        // Prevenir redimensionamiento de columnas
                AllowUserToResizeRows = false,           // Prevenir redimensionamiento de filas
                AllowUserToOrderColumns = false,
                ScrollBars = ScrollBars.Both,
                GridColor = Color.White, // Establecer el color del borde de la celda a BLANCO
                CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical,


            };
            //dgDnRepa.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            //dgDnRepa.BorderStyle = BorderStyle.FixedSingle;
            dgDnRepa.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgDnRepa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgDnRepa.CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical;
            dgDnRepa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgDnRepa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            dgDnRepa.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.Single;
            // **1. Añadir el borde inferior amarillo (Franja)**
            dgDnRepa.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            dgDnRepa.Columns.Cast<DataGridViewColumn>().ToList().ForEach(col =>
            {
                // Crear un borde amarillo de 3 píxeles
                col.HeaderCell.Style.Padding = new Padding(0, 0, 0, 5);
                col.HeaderCell.Style.SelectionBackColor = ColorTranslator.FromHtml("#0070C0");
            });

            // **2. Estilo de fila alternada (Gris salteado)**
            dgDnRepa.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0"); // Gris claro

            // **3. Borde de celdas (Blanco) y Centrado General**
            dgDnRepa.DefaultCellStyle.SelectionBackColor = Color.LightGray; // Para que no sea azul al seleccionar
            dgDnRepa.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            // dgDnRepa.GridColor ya se estableció en Color.White
            // Usamos CellPainting para dibujar el borde blanco y la franja amarilla en el encabezado
            dgDnRepa.CellPainting += (s, ev) =>
            {
                if (ev.RowIndex == -1 && ev.ColumnIndex >= 0) // Solo para las celdas del encabezado
                {
                    ev.Paint(ev.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border); // Pinta todo excepto el borde por defecto

                    Rectangle rect = ev.CellBounds;
                    int lineThickness = 1; // Grosor del borde blanco (1px)
                    int yellowThickness = 3; // Grosor de la franja amarilla

                    // 1. Dibuja el Borde Separador (Blanco) a la DERECHA de la celda del encabezado
                    using (Pen whitePen = new Pen(Color.White, lineThickness))
                    {
                        // Dibuja la línea blanca en el lado derecho de la celda del encabezado
                        // Esto crea el borde de separación entre columnas (vertical)
                        ev.Graphics.DrawLine(whitePen, rect.Right - lineThickness / 2, rect.Top, rect.Right - lineThickness / 2, rect.Bottom);
                    }

                    // 2. Dibuja la Franja Amarilla (Horizontal) en la parte INFERIOR de la celda
                    using (Pen yellowPen = new Pen(ColorTranslator.FromHtml("#FFC000"), yellowThickness))
                    {
                        // Altura donde dibujar la franja amarilla.
                        // Usamos rect.Bottom (borde inferior del encabezado) y le restamos 2px para subirla un poco (separación).
                        int yellowLineY = rect.Bottom - (lineThickness / 2) - 2;

                        // Dibuja la franja amarilla horizontal en toda la extensión de la celda del encabezado
                        ev.Graphics.DrawLine(yellowPen, rect.Left, yellowLineY, rect.Right, yellowLineY);
                    }

                    ev.Handled = true; // Indica que ya manejamos la pintura de la celda
                }
            };
            //------
            btnAgregar = new Button()
            {
                Text = "Agregar Parte",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#0070C0"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAgregar.Click += BtnAgregar_Click;

            btnEliminar = new Button()
            {
                Text = "Eliminar Parte",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnEliminar.Click += BtnEliminar_Click;

            btnEditar = new Button()
            {
                Text = "Editar Parte",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#E1E11F"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnEditar.Click += BtnEditar_Click;
            contentPanel.Controls.Add(separatorLine);
            contentPanel.Controls.Add(btnAgregar);
            contentPanel.Controls.Add(btnEliminar);
            contentPanel.Controls.Add(btnEditar);
            contentPanel.Controls.Add(dgDnRepa);
            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            Load += DnRForm_Load;
        }

        private void DnRForm_Load(object sender, EventArgs e)
        {
            lista = new List<Norepa>()
            {
                new Norepa { ID="NR-001", FechadeIngreso="13/9/2025", Dispositivo="Samsung S23 plus", Cliente=" Ana García", TecnicoAsignado=" M. Pérez", DescripciondelDano="Revisión general y limpieza", CostoEstimado=2500m, Observaciones="Rotación alta" },
                new Norepa { ID="NR-002", FechadeIngreso="13/10/2025", Dispositivo=" iPhone 14 Pro", Cliente=" Carlos Mejía", TecnicoAsignado=" D. López", DescripciondelDano="Botón Home sin respuesta", CostoEstimado=180m,  Observaciones="Solicitar reposición" },
                new Norepa { ID="NR-003", FechadeIngreso="25/10/2025", Dispositivo="iPhone 13", Cliente="Luis Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Batería se descarga muy rápido", CostoEstimado=600m,  Observaciones="-" },
                new Norepa { ID="NR-004", FechadeIngreso="17/10/2025", Dispositivo=" Xiaomi Redmi 11", Cliente=" Pedro López", TecnicoAsignado="L. Reyes", DescripciondelDano="Puerto de carga dañado", CostoEstimado=120m, Observaciones="Uso frecuente" }
            };
            ActualizarGrid();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            Form formAgregar = new Form()
            {
                Width = 400,
                Height = 450,
                Text = "Agregar Nueva Parte",
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
            TextBox txtFechadeIngreso = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
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

            Label lblCostoEstimado = new Label() { Text = "Costo Esimado(L.)", Left = 20, Top = top, Width = labelWidth };
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

                //if (!int.TryParse(txtTecnicoAsignado.Text, out int cantidad))
                //{
                //     MessageBox.Show("Solo se permiten números en la cantidad.");
                //      return;
                //}

                if (!decimal.TryParse(txtCostoEstimado.Text, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números en el costo unitario.");
                    return;
                }

                contadorID++; // Avanzar el contador solo al agregar
                string nuevoID = $"P-{contadorID.ToString("D3")}";
                //  decimal valorTotal = cantidad * costo;

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

                ActualizarGrid();
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
                string id = dgDnRepa.CurrentRow.Cells["ID"].Value.ToString();
                DialogResult result = MessageBox.Show($"¿Está seguro de borrar el registro '{id}'?", "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    lista.RemoveAt(dgDnRepa.CurrentRow.Index);
                    ActualizarGrid();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgDnRepa.CurrentRow == null) return;

            int index = dgDnRepa.CurrentRow.Index;
            Norepa norepa = lista[index];

            Form formEditar = new Form()
            {
                Width = 400,
                Height = 450,
                Text = "Editar Parte",
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

                //  if (!int.TryParse(txtTecnicoAsignado.Text, out int cantidad))
                //  {
                //       MessageBox.Show("Solo se permiten números en la cantidad.");
                //      return;
                // }

                if (!decimal.TryParse(txtCostoEstimado.Text, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números en el costo unitario.");
                    return;
                }

                norepa.FechadeIngreso = txtFechadeIngreso.Text;
                norepa.Dispositivo = txtDispositivo.Text;
                norepa.Cliente = txtCliente.Text;
                norepa.TecnicoAsignado = txtTecnicoAsignado.Text;
                norepa.DescripciondelDano = txtDescripciondelDano.Text;
                norepa.CostoEstimado = costo;
                norepa.Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text;
                ActualizarGrid();
                formEditar.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblFechadeIngreso, txtFechadeIngreso, lblDispositivo, txtDispositivo, lblCliente, txtCliente,
                lblTecnicoAsignado, txtTecnicoAsignado, lblDescripciondelDano, txtDescripciondelDano, lblCostoEstimado, txtCostoEstimado,
                lblObs, txtObs, btnGuardar });

            formEditar.ShowDialog();
        }

        private void ActualizarGrid()
        {
            dgDnRepa.DataSource = null;
            dgDnRepa.DataSource = lista;

            // ...encabezados y formatos...
            if (dgDnRepa.Columns.Contains("FechadeIngreso"))
                dgDnRepa.Columns["FechadeIngreso"].HeaderText = "Fecha de Ingreso";

            if (dgDnRepa.Columns.Contains("TecnicoAsignado"))
                dgDnRepa.Columns["TecnicoAsignado"].HeaderText = "Técnico Asignado";

            if (dgDnRepa.Columns.Contains("DescripciondelDano"))
                dgDnRepa.Columns["DescripciondelDano"].HeaderText = "Descripción del Daño";

            if (dgDnRepa.Columns.Contains("CostoEstimado"))
                dgDnRepa.Columns["CostoEstimado"].HeaderText = "Costo Estimado (L.)";
            // ----------------------------

            // Usar Fill para que el grid se autoajuste al ancho del panel padre.
            dgDnRepa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            // Nota: Cuando usas DataGridViewAutoSizeColumnsMode.Fill,
            // definir un 'widths' fijo ya no es necesario, pero puedes usarlo para establecer 
            // pesos relativos.

            // Si quieres anchos relativos:
            if (dgDnRepa.Columns.Contains("ID")) dgDnRepa.Columns["ID"].FillWeight = 50;
            if (dgDnRepa.Columns.Contains("Observaciones")) dgDnRepa.Columns["Observaciones"].FillWeight = 150;
            // El resto se repartirá el espacio sobrante equitativamente.

            var widths = new Dictionary<string, int>
            {
                ["ID"] = 80,
                ["FechadeIngreso"] = 160,
                ["Dispositivo"] = 140,
                ["Cliente"] = 160,
                ["TecnicoAsignado"] = 160,
                ["DescripciondelDaño"] = 300,
                ["Costo Estimado"] = 120,
                ["Observaciones"] = 120,
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
                    // ancho por defecto si falta en el diccionario
                    col.AutoSizeMode = DataGridViewAutoSizeColumnMode.AllCells;
                    totalWidth += col.Width;
                }
            }

            // Asegura que el contenedor permita desplazamiento horizontal si el grid es más ancho
            contenedorReporte.AutoScroll = true;
            contenedorReporte.AutoScrollMinSize = new Size(dgDnRepa.Width, 0);

            // ...estilizado de filas...
        }

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
