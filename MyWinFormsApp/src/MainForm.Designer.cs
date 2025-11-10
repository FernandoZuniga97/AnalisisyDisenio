using System;
using System.Drawing;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    partial class MainForm
    {
        private Panel panelLeft;
        private Panel panelContent;
        private Label lblContent;

        // Botones principales
        private Button btnModule1;
        private Button btnModule2;
        private Button btnModule3;
        private Button btnModule4;
        private Button btnModule5;

        // Submenús
        private Panel panelSubMenu1;
        private Panel panelSubMenu2;
        private Panel panelSubMenu3;
        private Panel panelSubMenu4;
        private Panel panelSubMenu5;

        // Submenú del módulo 1
        private Button btnDnR;

        // Submenú del módulo 5
        private Button btnInventario;

        private void InitializeComponent()
        {
            this.panelLeft = new Panel();
            this.panelContent = new Panel();
            this.lblContent = new Label();

            // Crear botones principales
            this.btnModule1 = new Button();
            this.btnModule2 = new Button();
            this.btnModule3 = new Button();
            this.btnModule4 = new Button();
            this.btnModule5 = new Button();

            // Crear submenús
            this.panelSubMenu1 = new Panel();
            this.panelSubMenu2 = new Panel();
            this.panelSubMenu3 = new Panel();
            this.panelSubMenu4 = new Panel();
            this.panelSubMenu5 = new Panel();

            // Crear botones de submenú
            this.btnDnR = new Button();
            this.btnInventario = new Button();

            this.SuspendLayout();

            // ======== FORM ========
            this.ClientSize = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "GICELL - Sistema de Monitoreo de Reparaciones";

            // ======== PANEL IZQUIERDO ========
            this.panelLeft.Dock = DockStyle.Left;
            this.panelLeft.Width = 200;
            this.panelLeft.BackColor = Color.FromArgb(0, 70, 140);
            this.panelLeft.AutoScroll = true;
            this.Controls.Add(this.panelLeft);

            // ======== PANEL DE CONTENIDO ========
            this.panelContent.Dock = DockStyle.Fill;
            this.panelContent.BackColor = Color.WhiteSmoke;
            this.panelContent.AutoScroll = true;
            this.Controls.Add(this.panelContent);
            this.panelContent.BringToFront();

            // ======== LABEL PRINCIPAL ========
            this.lblContent.Dock = DockStyle.Fill;
            this.lblContent.Text = "Seleccione un módulo";
            this.lblContent.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            this.lblContent.TextAlign = ContentAlignment.MiddleCenter;
            this.panelContent.Controls.Add(this.lblContent);

            // ======== CONFIGURACIÓN DE BOTONES ========
            ConfigureMainButton(btnModule1, "Registro de mantenimiento", BtnMantenimiento_Click);
            ConfigureMainButton(btnModule2, "Actualización de estado", ModuleButton_Click);
            ConfigureMainButton(btnModule3, "Reporte de reparación", ModuleButton_Click);
            ConfigureMainButton(btnModule4, "Línea de tiempo", ModuleButton_Click);
            ConfigureMainButton(btnModule5, "Administración general", BtnAdministracion_Click);

            // Etiquetas de módulo (para ModuleButton_Click)
            btnModule2.Tag = 2;
            btnModule3.Tag = 3;
            btnModule4.Tag = 4;

            // ======== CONFIGURACIÓN DE SUBMENÚS ========
            ConfigureSubMenuPanel(panelSubMenu1);
            ConfigureSubMenuPanel(panelSubMenu2);
            ConfigureSubMenuPanel(panelSubMenu3);
            ConfigureSubMenuPanel(panelSubMenu4);
            ConfigureSubMenuPanel(panelSubMenu5);

            // ======== SUBMENÚ 1 ========
            ConfigureSubButton(btnDnR, "Dispositivos no reparados", BtnDnR_Click);
            panelSubMenu1.Controls.Add(btnDnR);

            // ======== SUBMENÚ 5 ========
            ConfigureSubButton(btnInventario, "Inventario", BtnInventario_Click);
            panelSubMenu5.Controls.Add(btnInventario);

            // ======== ORDEN DE CONTROLES ========
            panelLeft.Controls.Add(panelSubMenu5);
            panelLeft.Controls.Add(btnModule5);
            panelLeft.Controls.Add(panelSubMenu4);
            panelLeft.Controls.Add(btnModule4);
            panelLeft.Controls.Add(panelSubMenu3);
            panelLeft.Controls.Add(btnModule3);
            panelLeft.Controls.Add(panelSubMenu2);
            panelLeft.Controls.Add(btnModule2);
            panelLeft.Controls.Add(panelSubMenu1);
            panelLeft.Controls.Add(btnModule1);

            this.ResumeLayout(false);
        }

        // =============================
        // FUNCIONES AUXILIARES
        // =============================
        private void ConfigureMainButton(Button btn, string text, EventHandler action)
        {
            btn.Text = text;
            btn.Height = 55;
            btn.Dock = DockStyle.Top;
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            btn.Click += AnimateButton_Click; // activa animación visual
            btn.Click += action; // ejecuta acción funcional
        }

        private void ConfigureSubMenuPanel(Panel panel)
        {
            panel.Dock = DockStyle.Top;
            panel.Height = 0;
            panel.Visible = false;
            panel.BackColor = Color.FromArgb(235, 240, 255);
        }

        private void ConfigureSubButton(Button btn, string text, EventHandler action)
        {
            btn.Text = text;
            btn.Dock = DockStyle.Top;
            btn.Height = 40;
            btn.TextAlign = ContentAlignment.MiddleLeft;
            btn.Padding = new Padding(20, 0, 0, 0);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.BackColor = Color.FromArgb(220, 225, 250);
            btn.Click += AnimateButton_Click; // animación al hacer click
            btn.Click += action;
        }
    }
}
