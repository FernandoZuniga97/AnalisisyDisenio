using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    partial class MainForm
    {
        private Panel panelLeft;
        private Panel panelContent;
        private Button btnModule1;
        private Button btnModule2;
        private Button btnModule3;
        private Button btnModule4;
        private Button btnModule5;
        private Label lblContent;
        //submenu especifico para el boton 5
        private Panel panelSubMenu5;
        private Button btnInventario;
        //------------------------------- aqui termina el sub5
        private void InitializeComponent()
        {
            this.panelLeft = new Panel();
            this.btnModule1 = new Button();
            this.btnModule2 = new Button();
            this.btnModule3 = new Button();
            this.btnModule4 = new Button();
            this.btnModule5 = new Button();
            this.panelContent = new Panel();
            this.lblContent = new Label();
            //submenu papu
            this.panelSubMenu5 = new Panel();
            this.btnInventario = new Button();
            //-------------------------------
            // MainForm
            this.SuspendLayout();
            this.ClientSize = new Size(800, 450);
            this.Text = "GICELL - Sistema de Monitoreo de Reparaciones de Dispositivos";
            this.StartPosition = FormStartPosition.CenterScreen;
            // panel de la izquierda
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 180;
            this.panelLeft.BackColor = Color.Blue;
            this.Controls.Add(this.panelLeft);
            // panel de contenido
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.White;
            this.Controls.Add(this.panelContent);
            this.panelContent.BringToFront();
            // Buttons (dock top to stack vertically)
            var buttons = new[] { btnModule5, btnModule4, btnModule3, btnModule2, btnModule1 };
            for (int i = 0; i < buttons.Length; i++)
            {
                var b = buttons[i];
                b.Height = 60;
                b.Dock = DockStyle.Top;
                b.Margin = new Padding(0);

                // estilo: fondo blanco, borde negro
                b.FlatStyle = FlatStyle.Flat;
                b.FlatAppearance.BorderColor = Color.Black;
                b.FlatAppearance.BorderSize = 1;
                b.BackColor = Color.White;
                b.ForeColor = Color.Black;
                b.UseVisualStyleBackColor = false; // para que BackColor se aplique
                this.panelLeft.Controls.Add(b);
            }

            // btnModule1
            this.btnModule1.Text = "Registro de mantenimiento";
            this.btnModule1.Tag = "1";
            this.btnModule1.Click += ModuleButton_Click;
            this.btnModule1.Click += AnimateButton_Click; // animación al tocar

            // btnModule2
            this.btnModule2.Text = "Actualización de estado y tiempo estimado";
            this.btnModule2.Tag = "2";
            this.btnModule2.Click += ModuleButton_Click;
            this.btnModule2.Click += AnimateButton_Click;

            // btnModule3
            this.btnModule3.Text = "Reporte de reparación y tiempos de entrega";
            this.btnModule3.Tag = "3";
            this.btnModule3.Click += ModuleButton_Click;
            this.btnModule3.Click += AnimateButton_Click;

            // btnModule4
            this.btnModule4.Text = "Linea de tiempo";
            this.btnModule4.Tag = "4";
            this.btnModule4.Click += ModuleButton_Click;
            this.btnModule4.Click += AnimateButton_Click;

            // btnModule5
            this.btnModule5.Text = "Administración general";
            this.btnModule5.Tag = "5";
            this.btnModule5.Click -= ModuleButton_Click;
            this.btnModule5.Click += BtnAdministracion_Click; // manejo especial para módulo 5
            this.btnModule5.Click += AnimateButton_Click;
            // submenu del 5
            this.panelSubMenu5 = new Panel();
            this.panelSubMenu5.BackColor = Color.FromArgb(230, 230, 250); // Color lavanda claro
            this.panelSubMenu5.Visible = false;
            this.panelSubMenu5.Height = 0; // Inicialmente altura 0
            this.panelSubMenu5.Width = 180;
            this.panelSubMenu5.Location = new Point(0, btnModule5.Bottom);
            this.panelLeft.Controls.Add(this.panelSubMenu5);
            //inventario sub menu
            // Configurar el botón de inventario
            this.btnInventario = new Button();
            this.btnInventario.Text = "Inventario";
            this.btnInventario.Height = 40;
            this.btnInventario.Width = 180;
            this.btnInventario.FlatStyle = FlatStyle.Flat;
            this.btnInventario.FlatAppearance.BorderSize = 0;
            this.btnInventario.BackColor = Color.FromArgb(240, 240, 255);
            this.btnInventario.ForeColor = Color.Black;
            this.btnInventario.TextAlign = ContentAlignment.MiddleLeft;
            this.btnInventario.Padding = new Padding(20, 0, 0, 0); // Sangría para indicar que es submenú
            this.btnInventario.Click += BtnInventario_Click;
            this.panelSubMenu5.Controls.Add(this.btnInventario);
            //aqui termina el submenu del5 
            this.lblContent.AutoSize = false;
            this.lblContent.TextAlign = ContentAlignment.MiddleCenter;
            this.lblContent.Dock = DockStyle.Fill;
            this.lblContent.Font = new Font("Segoe UI", 14, FontStyle.Regular);
            this.lblContent.Text = "Seleccione un módulo";
            this.panelContent.Controls.Add(this.lblContent);

            this.ResumeLayout(false);
        }
    }
}
