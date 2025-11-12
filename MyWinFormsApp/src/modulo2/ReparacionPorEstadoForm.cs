using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyWinFormsApp.src.modulo2
{
    public class ReparacionPorEstadoForm : Form
    {
        private Label lblTitulo;
        private Label lblSubtitulo;
        private Label lblFecha;
        private Panel headerPanel;
        private Panel separatorLine;
        private DataGridView dgReparaciones;
        private Panel contentPanel;
        private Panel contenedorReporte;
        private Button btnAgregar;
        private Button btnEliminar;
        private Button btnEditar;
        private List<Reparacion> lista;
        private int contadorID = 18;

        public ReparacionPorEstadoForm()
        {
            Text = "Reporte de Reparaciones por Estado";
            Width = 1300;
            Height = 750;
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
                AutoScroll = true
            };

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
                SizeMode = PictureBoxSizeMode.Zoom,
                Width = 120,
                Height = 120,
                Dock = DockStyle.Left,
                Margin = new Padding(25, 0, 0, 0)
            };

            try
            {
                string[] possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src","Login","Image","logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "src","Login","Image","logo_g.jpg"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image","logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "Image","logo_g.jpg")
                };

                string foundPath = possiblePaths.FirstOrDefault(File.Exists);
                if (foundPath != null)
                {
                    using (var stream = new FileStream(foundPath, FileMode.Open, FileAccess.Read))
                    {
                        logo.Image = Image.FromStream(stream);
                    }
                }
            }
            catch { }

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
                Text = "Reparaciones Por Estado",
                Font = new Font("Segoe UI", 14),
                ForeColor = Color.White,
                TextAlign = ContentAlignment.MiddleCenter,
                Dock = DockStyle.Top,
                Height = 40
            };

            lblFecha = new Label()
            {
                Text = "Fecha: (9/11/25)",
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

            separatorLine = new Panel()
            {
                Height = 3,
                BackColor = Color.White,
                Dock = DockStyle.Top
            };

            dgReparaciones = new DataGridView()
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
                ScrollBars = ScrollBars.Both,
                GridColor = Color.White,
                CellBorderStyle = DataGridViewCellBorderStyle.SingleVertical,
            };

            dgReparaciones.ColumnHeadersDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#0070C0");
            dgReparaciones.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgReparaciones.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            dgReparaciones.ColumnHeadersDefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;

            dgReparaciones.AlternatingRowsDefaultCellStyle.BackColor = ColorTranslator.FromHtml("#F0F0F0");
            dgReparaciones.DefaultCellStyle.SelectionBackColor = Color.LightGray;
            dgReparaciones.CellPainting += (s, ev) =>
            {
                if (ev.RowIndex == -1 && ev.ColumnIndex >= 0)
                {
                    ev.Paint(ev.CellBounds, DataGridViewPaintParts.All & ~DataGridViewPaintParts.Border);
                    Rectangle rect = ev.CellBounds;
                    using (Pen whitePen = new Pen(Color.White, 1))
                        ev.Graphics.DrawLine(whitePen, rect.Right - 1, rect.Top, rect.Right - 1, rect.Bottom);
                    using (Pen yellowPen = new Pen(ColorTranslator.FromHtml("#FFC000"), 3))
                        ev.Graphics.DrawLine(yellowPen, rect.Left, rect.Bottom - 2, rect.Right, rect.Bottom - 2);
                    ev.Handled = true;
                }
            };

            btnAgregar = new Button()
            {
                Text = "Agregar Reparación",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = ColorTranslator.FromHtml("#0070C0"),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnAgregar.Click += BtnAgregar_Click;

            btnEliminar = new Button()
            {
                Text = "Eliminar Reparación",
                Dock = DockStyle.Bottom,
                Height = 40,
                BackColor = Color.Red,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 10, FontStyle.Bold)
            };
            btnEliminar.Click += BtnEliminar_Click;

            btnEditar = new Button()
            {
                Text = "Editar Reparación",
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
            contentPanel.Controls.Add(dgReparaciones);
            contenedorReporte.Controls.Add(contentPanel);
            contenedorReporte.Controls.Add(headerPanel);
            Controls.Add(contenedorReporte);

            Load += ReparacionPorEstadoForm_Load;
        }

        private void ReparacionPorEstadoForm_Load(object sender, EventArgs e)
        {
            lista = new List<Reparacion>()
            {
                new Reparacion{ ID="R-001", FechadeIngreso="1/10/2025", Dispositivo="Huawei P30 Lite", Cliente="Ana García", TecnicoAsignado="J. Martínez", DescripciondelDano="No enciende el dispositivo", EstadoReparacion="En proceso", FechaEstimadaEntrega="2/11/2025", CostoEstimado=1200, Observaciones="Se sospecha daño en la placa"},
                new Reparacion{ ID="R-002", FechadeIngreso="3/10/2025", Dispositivo="iPhone SE (2020)", Cliente="Carlos Mejía", TecnicoAsignado="C. Flores", DescripciondelDano="Botón Home sin respuesta", EstadoReparacion="Finalizada", FechaEstimadaEntrega="31/10/2025", CostoEstimado=1400, Observaciones="Touch ID funcional"},
                new Reparacion{ ID="R-003", FechadeIngreso="5/10/2025", Dispositivo="Samsung S23 Ultra", Cliente="Luis Torres", TecnicoAsignado="M. Pérez", DescripciondelDano="Revisión general y limpieza", EstadoReparacion="Retrasada", FechaEstimadaEntrega="31/10/2025", CostoEstimado=1100, Observaciones="En espera de repuesto"},
                new Reparacion{ ID="R-004", FechadeIngreso="7/10/2025", Dispositivo="Xiaomi Redmi Note 11", Cliente="Pedro López", TecnicoAsignado="L. Reyes", DescripciondelDano="Puerto de carga dañado", EstadoReparacion="En proceso", FechaEstimadaEntrega="4/11/2025", CostoEstimado=1000, Observaciones="Repuesto en stock"}
            };
            ActualizarGrid();
        }

        private void BtnAgregar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de agregar reparación (similar al formulario anterior).");
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            if (dgReparaciones.CurrentRow != null)
            {
                string id = dgReparaciones.CurrentRow.Cells["ID"].Value.ToString();
                DialogResult result = MessageBox.Show($"¿Está seguro de borrar la reparación '{id}'?",
                    "Confirmar eliminación", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if (result == DialogResult.Yes)
                {
                    lista.RemoveAt(dgReparaciones.CurrentRow.Index);
                    ActualizarGrid();
                }
            }
        }

        private void BtnEditar_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Funcionalidad de editar reparación (igual al formulario anterior).");
        }

        private void ActualizarGrid()
        {
            dgReparaciones.DataSource = null;
            dgReparaciones.DataSource = lista;

            if (dgReparaciones.Columns.Contains("FechadeIngreso"))
                dgReparaciones.Columns["FechadeIngreso"].HeaderText = "Fecha de ingreso";
            if (dgReparaciones.Columns.Contains("TecnicoAsignado"))
                dgReparaciones.Columns["TecnicoAsignado"].HeaderText = "Técnico asignado";
            if (dgReparaciones.Columns.Contains("DescripciondelDano"))
                dgReparaciones.Columns["DescripciondelDano"].HeaderText = "Descripción del daño";
            if (dgReparaciones.Columns.Contains("EstadoReparacion"))
                dgReparaciones.Columns["EstadoReparacion"].HeaderText = "Estado de reparación";
            if (dgReparaciones.Columns.Contains("FechaEstimadaEntrega"))
                dgReparaciones.Columns["FechaEstimadaEntrega"].HeaderText = "Fecha estimada de entrega";
            if (dgReparaciones.Columns.Contains("CostoEstimado"))
                dgReparaciones.Columns["CostoEstimado"].HeaderText = "Costo estimado (L.)";
            if (dgReparaciones.Columns.Contains("Observaciones"))
                dgReparaciones.Columns["Observaciones"].HeaderText = "Observaciones";

            dgReparaciones.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }
    }

    public class Reparacion
    {
        public string ID { get; set; }
        public string FechadeIngreso { get; set; }
        public string Dispositivo { get; set; }
        public string Cliente { get; set; }
        public string TecnicoAsignado { get; set; }
        public string DescripciondelDano { get; set; }
        public string EstadoReparacion { get; set; }
        public string FechaEstimadaEntrega { get; set; }
        public decimal CostoEstimado { get; set; }
        public string Observaciones { get; set; }
    }
}
