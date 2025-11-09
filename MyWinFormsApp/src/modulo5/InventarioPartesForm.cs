using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    public class InventarioPartesForm : Form
    {
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private Panel headerPanel;
        private Panel separatorLine;
        private DataGridView dgvInventario;
        private Panel contentPanel;
        private Panel contenedorReporte;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnEditar;

        private List<Parte> lista;

        public InventarioPartesForm()
        {
            Text = "Administración General - Inventario de Partes";
            Width = 1250;
            Height = 700;
            BackColor = Color.FromArgb(242, 242, 242);

            contenedorReporte = new Panel()
            {
                Dock = DockStyle.Fill,
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle
            };

            headerPanel = new Panel()
            {
                Dock = DockStyle.Top,
                Height = 150,
                BackColor = ColorTranslator.FromHtml("#002060")
            };

            // Panel para título e imagen
            TableLayoutPanel titlePanel = new TableLayoutPanel()
            {
                Dock = DockStyle.Top,
                Height = 150,
                ColumnCount = 2,
                RowCount = 1
            };
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
            titlePanel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            titlePanel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            PictureBox logo = new PictureBox()
            {
                Image = Image.FromFile("Image\\logo_g.jpg"),
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 150,
                Height = 150,
                Anchor = AnchorStyles.Left | AnchorStyles.Top
            };

            lblTitulo = new Label()
            {
                Text = "IMPORTACIONES GICELL",
                Font = new Font("Century Gothic", 30, FontStyle.Bold),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Fill
            };

            titlePanel.Controls.Add(logo, 0, 0);
            titlePanel.Controls.Add(lblTitulo, 1, 0);

            lblSubtitulo = new Label()
            {
                Text = "Inventario De Partes",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = "Fecha: (01/11/2025)",
                Font = new Font("Segoe UI", 11, FontStyle.Italic),
                ForeColor = Color.White,
                Dock = DockStyle.Top,
                TextAlign = ContentAlignment.MiddleCenter,
                Height = 30
            };

            // Agregar controles en orden correcto para que se muestren
            headerPanel.Controls.Add(lblFecha);
            headerPanel.Controls.Add(lblSubtitulo);
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

            dgvInventario = new DataGridView()
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
                SelectionMode = DataGridViewSelectionMode.FullRowSelect
            };

            dgvInventario.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgvInventario.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvInventario.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgvInventario.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            // Botón Agregar
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

            // Botón Eliminar
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

            // Botón Editar
            btnEditar = new Button()
            {
                Text = "Editar Parte",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#FFA500"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnEditar.Click += BtnEditar_Click;

            contentPanel.Controls.Add(dgvInventario);
            contentPanel.Controls.Add(separatorLine);
            contentPanel.Controls.Add(btnAgregar);
            contentPanel.Controls.Add(btnEditar);
            contentPanel.Controls.Add(btnEliminar);

            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);

            Controls.Add(contenedorReporte);

            Load += InventarioPartesForm_Load;
        }

        private void InventarioPartesForm_Load(object sender, EventArgs e)
        {
            lista = new List<Parte>()
            {
                new Parte { ID="P-001", Nombre="Pantalla iPhone 12", Categoria="Pantallas", Proveedor="G-Tech Supply", CantidadActual=12, Unidad="unidades", CostoUnitario=2500m, ValorTotal=30000m, EstadoStock="Suficiente", Observaciones="Rotación alta" },
                new Parte { ID="P-002", Nombre="Puerto de carga tipo C", Categoria="Conectores", Proveedor="ElectroParts", CantidadActual=8, Unidad="unidades", CostoUnitario=180m, ValorTotal=1440m, EstadoStock="Bajo", Observaciones="Solicitar reposición" },
                new Parte { ID="P-003", Nombre="Batería Samsung A52", Categoria="Baterías", Proveedor="MobilePro", CantidadActual=15, Unidad="unidades", CostoUnitario=600m, ValorTotal=9000m, EstadoStock="Suficiente", Observaciones="-" },
                new Parte { ID="P-004", Nombre="Pasta térmica", Categoria="Insumos", Proveedor="Tecnocell", CantidadActual=20, Unidad="tubos", CostoUnitario=120m, ValorTotal=2400m, EstadoStock="Suficiente", Observaciones="Uso frecuente" }
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

                decimal valorTotal = cantidad * costo;
                string nuevoID = $"P-{(lista.Count + 1).ToString("D3")}";

                lista.Add(new Parte()
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

            panel.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre,
                lblCategoria, txtCategoria,
                lblProveedor, txtProveedor,
                lblCantidad, txtCantidad,
                lblUnidad, txtUnidad,
                lblCosto, txtCosto,
                lblEstado, txtEstado,
                lblObs, txtObs,
                btnGuardar
            });

            formAgregar.ShowDialog();
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para eliminar.");
                return;
            }

            var fila = dgvInventario.SelectedRows[0];
            string id = fila.Cells["ID"].Value.ToString();

            DialogResult dr = MessageBox.Show($"¿Está seguro de borrar el registro \"{id}\"?", "Confirmar eliminación", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                int index = lista.FindIndex(p => p.ID == id);
                if (index >= 0)
                {
                    lista.RemoveAt(index);

                    // Reasignar IDs
                    for (int i = 0; i < lista.Count; i++)
                    {
                        lista[i].ID = $"P-{(i + 1).ToString("D3")}";
                    }

                    ActualizarGrid();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            if (dgvInventario.SelectedRows.Count == 0)
            {
                MessageBox.Show("Seleccione un registro para editar.");
                return;
            }

            var fila = dgvInventario.SelectedRows[0];
            string id = fila.Cells["ID"].Value.ToString();
            Parte parte = lista.Find(p => p.ID == id);

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
            TextBox txtNombre = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.Nombre };
            top += gap;

            Label lblCategoria = new Label() { Text = "Categoría", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCategoria = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.Categoria };
            top += gap;

            Label lblProveedor = new Label() { Text = "Proveedor", Left = 20, Top = top, Width = labelWidth };
            TextBox txtProveedor = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.Proveedor };
            top += gap;

            Label lblCantidad = new Label() { Text = "Cantidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCantidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.CantidadActual.ToString() };
            top += gap;

            Label lblUnidad = new Label() { Text = "Unidad", Left = 20, Top = top, Width = labelWidth };
            TextBox txtUnidad = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.Unidad };
            top += gap;

            Label lblCosto = new Label() { Text = "Costo Unitario", Left = 20, Top = top, Width = labelWidth };
            TextBox txtCosto = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.CostoUnitario.ToString() };
            top += gap;

            Label lblEstado = new Label() { Text = "Estado Stock", Left = 20, Top = top, Width = labelWidth };
            TextBox txtEstado = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.EstadoStock };
            top += gap;

            Label lblObs = new Label() { Text = "Observaciones", Left = 20, Top = top, Width = labelWidth };
            TextBox txtObs = new TextBox() { Left = 150, Top = top, Width = textBoxWidth, Text = parte.Observaciones };
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

                parte.Nombre = txtNombre.Text;
                parte.Categoria = txtCategoria.Text;
                parte.Proveedor = txtProveedor.Text;
                parte.CantidadActual = cantidad;
                parte.Unidad = txtUnidad.Text;
                parte.CostoUnitario = costo;
                parte.ValorTotal = cantidad * costo;
                parte.EstadoStock = txtEstado.Text;
                parte.Observaciones = string.IsNullOrWhiteSpace(txtObs.Text) ? "-" : txtObs.Text;

                ActualizarGrid();
                formEditar.Close();
            };

            panel.Controls.AddRange(new Control[]
            {
                lblNombre, txtNombre,
                lblCategoria, txtCategoria,
                lblProveedor, txtProveedor,
                lblCantidad, txtCantidad,
                lblUnidad, txtUnidad,
                lblCosto, txtCosto,
                lblEstado, txtEstado,
                lblObs, txtObs,
                btnGuardar
            });

            formEditar.ShowDialog();
        }

        private void ActualizarGrid()
        {
            dgvInventario.DataSource = null;
            dgvInventario.DataSource = lista;

            dgvInventario.Columns["ID"].HeaderText = "ID";
            dgvInventario.Columns["Nombre"].HeaderText = "Nombre del Repuesto";
            dgvInventario.Columns["Categoria"].HeaderText = "Categoría";
            dgvInventario.Columns["Proveedor"].HeaderText = "Proveedor";
            dgvInventario.Columns["CantidadActual"].HeaderText = "Cantidad Actual";
            dgvInventario.Columns["Unidad"].HeaderText = "Unidad";
            dgvInventario.Columns["CostoUnitario"].HeaderText = "Costo Unitario (L)";
            dgvInventario.Columns["ValorTotal"].HeaderText = "Valor Total (L)";
            dgvInventario.Columns["EstadoStock"].HeaderText = "Estado de Stock";
            dgvInventario.Columns["Observaciones"].HeaderText = "Observaciones";

            dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.Format = "C0";
            dgvInventario.Columns["CostoUnitario"].DefaultCellStyle.FormatProvider = new CultureInfo("es-HN");
            dgvInventario.Columns["ValorTotal"].DefaultCellStyle.Format = "C0";
            dgvInventario.Columns["ValorTotal"].DefaultCellStyle.FormatProvider = new CultureInfo("es-HN");

            foreach (DataGridViewRow row in dgvInventario.Rows)
            {
                string id = row.Cells["ID"].Value.ToString();
                if (int.TryParse(id.Split('-')[1], out int numero) && numero % 2 == 0)
                    row.DefaultCellStyle.BackColor = ColorTranslator.FromHtml("#E7E6E6");
            }

            dgvInventario.Paint += (s, ev) =>
            {
                int headerHeight = dgvInventario.ColumnHeadersHeight;
                using (Pen pen = new Pen(Color.Yellow, 3))
                {
                    float y = headerHeight - 1;
                    ev.Graphics.DrawLine(pen, 0, y, dgvInventario.Width, y);
                }
            };
        }
    }

    public class Parte
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
