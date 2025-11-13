using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    partial class MainForm
    {
        private Panel panelLeft;
        private Panel panelContent;
        private Label lblContent;
        private Panel panelSpacerTop;

        // Botones principales
        private Button btnModule1;
        private Button btnModule2;
        private Button btnModule3;
        private Button btnModule4;
        private Button btnModule5;
        private PictureBox pbProfile; // Para la foto de perfil
        private Label lblUserRole;     // Para el rol: Administrador/Empleado
        private Button btnLogout;

        // Submenús
        private Panel panelSubMenu1;
        private Panel panelSubMenu2;
        private Panel panelSubMenu3;
        private Panel panelSubMenu4;
        private Panel panelSubMenu5;

        // Submenú del módulo 1
        private Button btnDnR;
        private Button btnDnryr;
        //submenu del modulo 4
        private Button btnMante;
        private Button btnExce;

        private Button btnReparacionPorEstado; // nuevo botón de submenú

        private Button btnTiempoPromedioReparacion;


        //submenu del modulo 4
        // Submenú del módulo 5
        private Button btnInventario;
        private Button btnTipoFallas; // NUEVO BOTÓN

        private void InitializeComponent()
        {
            this.panelLeft = new Panel();
            this.panelContent = new Panel();
            this.lblContent = new Label();
            this.panelSpacerTop = new Panel();

            // Crear botones principales
            this.btnModule1 = new Button();
            this.btnModule2 = new Button();
            this.btnModule3 = new Button();
            this.btnModule4 = new Button();
            this.btnModule5 = new Button();
            this.pbProfile = new PictureBox();
            this.lblUserRole = new Label();
            this.btnLogout = new Button();
            // Crear submenús
            this.panelSubMenu1 = new Panel();
            this.panelSubMenu2 = new Panel();
            this.panelSubMenu3 = new Panel();
            this.panelSubMenu4 = new Panel();
            this.panelSubMenu5 = new Panel();

            // Crear botones de submenú
            this.btnMante = new Button();
            this.btnExce = new Button();
            this.btnDnR = new Button();
            this.btnDnryr = new Button();
            this.btnInventario = new Button();
            this.btnTipoFallas = new Button(); // NUEVO BOTÓN
            this.btnReparacionPorEstado = new Button(); // <-- agrega esto
            this.btnTiempoPromedioReparacion = new Button(); // 🔹 NUEVO


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
            this.lblContent.Text = "Bienvenido a GICELL";
            this.lblContent.Font = new Font("Segoe UI", 25, FontStyle.Bold);
            this.lblContent.ForeColor = Color.FromArgb(0, 70, 140);
            this.lblContent.BackColor = Color.White;
            this.lblContent.TextAlign = ContentAlignment.MiddleCenter;
            this.panelContent.Controls.Add(this.lblContent);
            // ======== PANEL ESPACIADOR SUPERIOR ========
            this.panelSpacerTop.Dock = DockStyle.Top;
            this.panelSpacerTop.Height = 65; // Altura del margen que quieres (15px)
            this.panelSpacerTop.BackColor = Color.FromArgb(0, 70, 140);
            this.panelLeft.Controls.Add(this.panelSpacerTop);
            //-------------------
            // ======== ÁREA DE PERFIL Y ROL (NUEVO) ========
            this.pbProfile.Dock = DockStyle.Top;
            this.pbProfile.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbProfile.Size = new Size(80, 80);
            this.pbProfile.Image = Image.FromFile(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "login", "Image", "perfil.jpg"));
            //this.pbProfile.Padding = new Padding(0, 55, 0, 0);
            this.pbProfile.Height = 110;
            this.pbProfile.BackColor = Color.FromArgb(0, 70, 140);
            this.panelLeft.Controls.Add(this.pbProfile);
            // Etiqueta de rol de usuario
            this.lblUserRole.Dock = DockStyle.Top;
            this.lblUserRole.TextAlign = ContentAlignment.MiddleCenter;
            this.lblUserRole.Height = 30;
            this.lblUserRole.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            this.lblUserRole.BackColor = Color.FromArgb(0, 70, 140);
            this.lblUserRole.ForeColor = Color.White;
            this.lblUserRole.Text = _isEmployee ? "Empleado" : "Administrador";
            this.panelLeft.Controls.Add(this.lblUserRole);
            //Cerrar boton confi
            // ======== CONFIGURACIÓN DEL BOTÓN DE CERRAR SESIÓN ========
            this.btnLogout.Text = "Cerrar Sesión";
            this.btnLogout.Dock = DockStyle.Bottom; // DockStyle.Bottom lo ancla al final
            this.btnLogout.Height = 45;
            this.btnLogout.FlatStyle = FlatStyle.Flat;
            this.btnLogout.FlatAppearance.BorderSize = 0;
            this.btnLogout.BackColor = Color.FromArgb(140, 0, 0); // Rojo oscuro para advertencia
            this.btnLogout.ForeColor = Color.White;
            this.btnLogout.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            //this.btnLogout.Click += LogoutButton_Click;
            this.panelLeft.Controls.Add(this.btnLogout);
            // ======== CONFIGURACIÓN DE BOTONES ========
            ConfigureMainButton(btnModule1, "Registro de mantenimiento", BtnMantenimiento_Click);
            ConfigureMainButton(btnModule2, "Actualización de estado", BtnActualizacion_Click);
            ConfigureMainButton(btnModule3, "Reporte de reparación", Anto3_Click);
            ConfigureMainButton(btnModule4, "Línea de tiempo", ModuleButton_Click);
            ConfigureMainButton(btnModule5, "Administración general", BtnAdministracion_Click);

            btnModule2.Tag = 2;
            // btnModule3.Tag = 3;
            btnModule4.Tag = 4;

            // ======== CONFIGURACIÓN DE SUBMENÚS ========
            ConfigureSubMenuPanel(panelSubMenu1);
            ConfigureSubMenuPanel(panelSubMenu2);
            ConfigureSubMenuPanel(panelSubMenu3);
            ConfigureSubMenuPanel(panelSubMenu4);
            ConfigureSubMenuPanel(panelSubMenu5);

            // ======== SUBMENÚ 1 ========
            ConfigureSubButton(btnDnR, "Dispositivos no reparados", BtnDnR_Click);
            ConfigureSubButton(btnDnryr, "Dispositivos reparados y no reparados", BtnDnryr_Click);
            panelSubMenu1.Controls.Add(btnDnryr);
            panelSubMenu1.Controls.Add(btnDnR);

            // ======== SUBMENÚ 2 ========
            // ======== SUBMENÚ 2 ========
            ConfigureSubButton(btnReparacionPorEstado, "Reparaciones por Estado", BtnReparacionPorEstado_Click);
            ConfigureSubButton(btnTiempoPromedioReparacion, "Tiempo promedio en reparación", BtnTiempoPromedioReparacion_Click);
            panelSubMenu2.Controls.Add(btnTiempoPromedioReparacion);
            panelSubMenu2.Controls.Add(btnReparacionPorEstado);



            // ======== SUBMENÚ 3 ========
            ConfigureSubButton(btnMante, "Mantenimiento Dispositivos", BtnMante_Click);
            ConfigureSubButton(btnExce, "Reporte de excepción", BtnExce_Click);
            panelSubMenu3.Controls.Add(btnMante);
            panelSubMenu3.Controls.Add(btnExce);
            // ======== SUBMENÚ 5 ========
            ConfigureSubButton(btnInventario, "Inventario", BtnInventario_Click);
            ConfigureSubButton(btnTipoFallas, "Tipo de fallas", BtnTipoFallas_Click); // NUEVO BOTÓN
            panelSubMenu5.Controls.Add(btnTipoFallas);
            panelSubMenu5.Controls.Add(btnInventario);
            //ORden pa usuario bb
            //panelLeft.Controls.Add(this.lblUserRole);
            //panelLeft.Controls.Add(this.pbProfile);
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
            this.panelLeft.Controls.Add(this.lblUserRole);
            this.panelLeft.Controls.Add(this.pbProfile);
            this.panelLeft.Controls.Add(this.panelSpacerTop);
            this.panelLeft.Controls.Add(this.btnLogout);
            this.ResumeLayout(false);
        }

        private void ConfigureMainButton(Button btn, string text, EventHandler action)
        {
            btn.Text = text;
            btn.Height = 65;
            btn.Dock = DockStyle.Top;
            btn.TextAlign = ContentAlignment.TopCenter;
            btn.FlatStyle = FlatStyle.Flat;
            btn.Margin = new Padding(0, 5, 0, 5);
            btn.FlatAppearance.BorderSize = 5;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 70, 140);
            btn.BackColor = Color.White;
            btn.ForeColor = Color.Black;
            // btn.FlatAppearance.BorderSize = 0;
            btn.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            // btn.Click += (sender, e) => HandleButtonClick(btn);
            // btn.Click += AnimateButton_Click;
            btn.Click += action;
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
            btn.Font = new Font("Segoe UI", 9, FontStyle.Regular);
            btn.TextAlign = ContentAlignment.TopCenter;
            //btn.Padding = new Padding(13, 7, 9, 7);
            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 2;
            btn.FlatAppearance.BorderColor = Color.FromArgb(0, 70, 140);
            btn.BackColor = Color.WhiteSmoke;
            //btn.Click += (sender, e) => HandleSubButtonClick(btn);
            //  btn.Click += AnimateButton_Click;
            btn.Click += action;
        }
    }
}
