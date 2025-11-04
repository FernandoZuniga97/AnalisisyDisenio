// ...existing code...
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
            this.btnModule5.Click += ModuleButton_Click;
            this.btnModule5.Click += AnimateButton_Click;

            // panelContent
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.White;
            this.Controls.Add(this.panelContent);

            // lblContent
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
// ...existing code...