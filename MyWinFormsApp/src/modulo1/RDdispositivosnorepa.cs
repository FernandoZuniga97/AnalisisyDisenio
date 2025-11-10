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
                Width = 150,
                Height = 150,
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
                Text = "Inventario De Partes",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = "Fecha: (01/11/2025)",
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
                AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None,
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
            };

            dgDnRepa.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgDnRepa.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgDnRepa.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgDnRepa.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

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
                new Norepa { ID="P-001", Nombre="Pantalla iPhone 12", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=12, Unidad="unidades", CostoUnitario=2500m, ValorTotal=30000m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
                new Norepa { ID="P-002", Nombre="Puerto de carga tipo C", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=8, Unidad="unidades", CostoUnitario=180m, ValorTotal=1440m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
                new Norepa { ID="P-003", Nombre="Batería Samsung A52", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=15, Unidad="unidades", CostoUnitario=600m, ValorTotal=9000m, EstadoStock="Suficiente", Observaciones="-" },
                new Norepa { ID="P-004", Nombre="Pasta térmica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=20, Unidad="tubos", CostoUnitario=120m, ValorTotal=2400m, EstadoStock="Suficiente", Observaciones="Uso frecuente" }
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

            Label lblNombre = new Label() { Text = "Nombre", Left = 20, Top = top, Width = labelWidth };
            TextBox txtNombre = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblCategoria = new Label() { Text = "Categoría", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCategoria = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblProveedor = new Label() { Text = "Proveedor", Left = 20, Top = top, Width = labelWidth };
            TextBox txtProveedor = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblCantidad = new Label() { Text = "Cantidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCantidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblUnidad = new Label() { Text = "Unidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtUnidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblCosto = new Label() { Text = "Costo Unitario", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCosto = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblEstado = new Label() { Text = "Estado Stock", Left = 20, Top = top, Width = labelWidth };
            TextBox txtEstado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth };
            top += gap;

            Button btnGuardar = new Button() { Text = "Guardar", Left = 150, Width = 100, Top = top };
            btnGuardar.Click += (s2, e2) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtProveedor.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    string.IsNullOrWhiteSpace(txtUnidad.Text) ||
                    string.IsNullOrWhiteSpace(txtCosto.Text) ||
                    string.IsNullOrWhiteSpace(txtEstado.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios excepto Observaciones.");
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad))
                {
                    MessageBox.Show("Solo se permiten números en la cantidad.");
                    return;
                }

                if (!decimal.TryParse(txtCosto.Text, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números en el costo unitario.");
                    return;
                }

                contadorID++; // Avanzar el contador solo al agregar
                string nuevoID = $"P-{contadorID.ToString("D3")}";
                decimal valorTotal = cantidad * costo;

                lista.Add(new Norepa()
                {
                    ID = nuevoID,
                    Nombre = txtNombre.Text,
                    Categoria = txtCategoria.Text,
                    Proveedor = txtProveedor.Text,
                    CantidadActual = cantidad,
                    Unidad = txtUnidad.Text,
                    CostoUnitario = costo,
                    ValorTotal = valorTotal,
                    EstadoStock = txtEstado.Text,
                    Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text
                });

                ActualizarGrid();
                formAgregar.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblNombre, txtNombre, lblCategoria, txtCategoria, lblProveedor, txtProveedor,
                lblCantidad, txtCantidad, lblUnidad, txtUnidad, lblCosto, txtCosto,
                lblEstado, txtEstado, lblObs, txtObs, btnGuardar });
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

            Label lblNombre = new Label() { Text = "Nombre", Left = 20, Top = top, Width = labelWidth };
            TextBox txtNombre = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Nombre };
            top += gap;

            Label lblCategoria = new Label() { Text = "Categoría", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCategoria = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Categoria };
            top += gap;

            Label lblProveedor = new Label() { Text = "Proveedor", Left = 20, Top = top, Width = labelWidth };
            TextBox txtProveedor = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Proveedor };
            top += gap;

            Label lblCantidad = new Label() { Text = "Cantidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCantidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.CantidadActual.ToString() };
            top += gap;

            Label lblUnidad = new Label() { Text = "Unidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtUnidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Unidad };
            top += gap;

            Label lblCosto = new Label() { Text = "Costo Unitario", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCosto = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.CostoUnitario.ToString() };
            top += gap;

            Label lblEstado = new Label() { Text = "Estado Stock", Left = 20, Top = top, Width = labelWidth };
            TextBox txtEstado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.EstadoStock };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = norepa.Observaciones };
            top += gap;

            Button btnGuardar = new Button() { Text = "Guardar", Left = 150, Width = 100, Top = top };
            btnGuardar.Click += (s2, e2) =>
            {
                if (string.IsNullOrWhiteSpace(txtNombre.Text) ||
                    string.IsNullOrWhiteSpace(txtCategoria.Text) ||
                    string.IsNullOrWhiteSpace(txtProveedor.Text) ||
                    string.IsNullOrWhiteSpace(txtCantidad.Text) ||
                    string.IsNullOrWhiteSpace(txtUnidad.Text) ||
                    string.IsNullOrWhiteSpace(txtCosto.Text) ||
                    string.IsNullOrWhiteSpace(txtEstado.Text))
                {
                    MessageBox.Show("Todos los campos son obligatorios excepto Observaciones.");
                    return;
                }

                if (!int.TryParse(txtCantidad.Text, out int cantidad))
                {
                    MessageBox.Show("Solo se permiten números en la cantidad.");
                    return;
                }

                if (!decimal.TryParse(txtCosto.Text, out decimal costo))
                {
                    MessageBox.Show("Solo se permiten números en el costo unitario.");
                    return;
                }

                norepa.Nombre = txtNombre.Text;
                norepa.Categoria = txtCategoria.Text;
                norepa.Proveedor = txtProveedor.Text;
                norepa.CantidadActual = cantidad;
                norepa.Unidad = txtUnidad.Text;
                norepa.CostoUnitario = costo;
                norepa.ValorTotal = cantidad * costo;
                norepa.EstadoStock = txtEstado.Text;
                norepa.Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text;

                ActualizarGrid();
                formEditar.Close();
            };

            panel.Controls.AddRange(new Control[] {
                lblNombre, txtNombre, lblCategoria, txtCategoria, lblProveedor, txtProveedor,
                lblCantidad, txtCantidad, lblUnidad, txtUnidad, lblCosto, txtCosto,
                lblEstado, txtEstado, lblObs, txtObs, btnGuardar });

            formEditar.ShowDialog();
        }

        private void ActualizarGrid()
        {
            dgDnRepa.DataSource = null;
            dgDnRepa.DataSource = lista;

            // ...encabezados y formatos...

            // Forzar modo None antes de fijar anchos
            dgDnRepa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.None;

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
            contenedorReporte.AutoScrollMinSize = new Size(Math.Max(totalWidth + 40, this.Width), 0);

            // ...estilizado de filas...
        }

    }

    public class Norepa
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
