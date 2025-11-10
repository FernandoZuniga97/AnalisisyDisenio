using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    partial class RegistroForm
    {
        private System.ComponentModel.IContainer components = null;
        private Panel mainCard;
        private Panel avatarCircle;
        private PictureBox logoPicture;
        private Panel userBorder;
        private TextBox txtUsername;
        private Label userIcon;
        private Panel passBorder;
        private TextBox txtPassword;
        private TextBox txtConfirmPassword;
        private Label passIcon;
        private Button btnOk;
        private Panel contentPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Panel confirmPassBorder;
        private Label confirmPassIcon;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.mainCard = new Panel();
            this.avatarCircle = new Panel();
            this.logoPicture = new PictureBox();
            this.userBorder = new Panel();
            this.txtUsername = new TextBox();
            this.userIcon = new Label();
            this.passBorder = new Panel();
            this.txtPassword = new TextBox();
            this.passIcon = new Label();
            this.confirmPassBorder = new Panel();
            this.txtConfirmPassword = new TextBox();
            this.confirmPassIcon = new Label();
            this.btnOk = new Button();
            this.contentPanel = new Panel();
            this.leftPanel = new Panel();
            this.rightPanel = new Panel();
            // Form
            this.SuspendLayout();
            this.ClientSize = new Size(760, 360); // horizontal
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Gicell - Registro";
            this.BackColor = Color.FromArgb(0x10, 0x28, 0x44); //azul fondo oscuro
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.DoubleBuffered = true;

            // Main card (tarjeta centrada, horizontal)
            int cardW = 700;
            int cardH = 350;
            this.mainCard = new Panel()
            {
                Size = new Size(cardW, cardH),
                Location = new Point((this.ClientSize.Width - cardW) / 2, (this.ClientSize.Height - cardH) / 2),
                BackColor = Color.FromArgb(240, Color.White),
            };
            this.mainCard.Paint += MainCard_Paint; // dibuja borde redondeado
            this.Controls.Add(this.mainCard);

            // contentPanel - divide tarjeta en 2 columnas
            this.contentPanel = new Panel()
            {
                Dock = DockStyle.Fill,
                Padding = new Padding(16)
            };
            this.mainCard.Controls.Add(this.contentPanel);


            int leftW = 220;
            this.leftPanel = new Panel()
            {
                Size = new Size(leftW, cardH - 32),
                Location = new Point(16, 16),
                BackColor = Color.Transparent
            };
            this.contentPanel.Controls.Add(this.leftPanel);


            int avatarSize = 140;
            this.avatarCircle = new Panel()
            {
                Size = new Size(avatarSize, avatarSize),
                Location = new Point((leftW - avatarSize) / 2, (this.leftPanel.Height - avatarSize) / 2),
                BackColor = Color.FromArgb(0x2F, 0x51, 0x60),
            };
            this.avatarCircle.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                using (Brush b = new SolidBrush(this.avatarCircle.BackColor))
                {
                    e.Graphics.FillEllipse(b, 0, 0, this.avatarCircle.Width - 1, this.avatarCircle.Height - 1);
                }
            };


            this.logoPicture = new PictureBox()
            {
                Dock = DockStyle.Fill,
                SizeMode = PictureBoxSizeMode.Zoom,
                BackColor = Color.Transparent
            };

            try
            {
                string[] possiblePaths = new[]
                {
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src","login", "Image", "logo_g.jpg"),
                    Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "src","login", "Image", "logo_g.jpg"),
                    Path.Combine(Application.StartupPath, "Image", "logo_g.jpg")
                };
                string foundPath = possiblePaths.FirstOrDefault(File.Exists);
                if (foundPath != null)
                {
                    using (var fs = new FileStream(foundPath, FileMode.Open, FileAccess.Read))
                        this.logoPicture.Image = Image.FromStream(fs);
                }
            }
            catch
            {
                // ignora bnb
            }

            this.avatarCircle.Controls.Add(this.logoPicture);
            this.leftPanel.Controls.Add(this.avatarCircle);


            this.rightPanel = new Panel()
            {
                Size = new Size(cardW - leftW - 48, cardH - 32),
                Location = new Point(leftW + 32, 16),
                BackColor = Color.Transparent
            };
            this.contentPanel.Controls.Add(this.rightPanel);

            int leftPadding = 8;
            int controlWidth = this.rightPanel.Width - leftPadding * 2;
            int y = 6;
            int gap = 8;

            // Title
            var title = new Label()
            {
                Text = "Registro de usuario",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleLeft,
                Font = new Font("Segoe UI", 14F, FontStyle.Bold),
                Size = new Size(controlWidth, 30),
                Location = new Point(leftPadding, y),
                ForeColor = Color.FromArgb(30, 30, 30)
            };
            this.rightPanel.Controls.Add(title);
            y += title.Height + gap;

            // Uusario container
            int inputH = 42;
            int iconW = 36;
            this.userBorder = new Panel()
            {
                BackColor = Color.White,
                Size = new Size(controlWidth, inputH),
                Location = new Point(leftPadding, y)
            };
            this.userBorder.Paint += InputBorder_Paint;
            this.rightPanel.Controls.Add(this.userBorder);

            this.userIcon = new Label()
            {
                Text = "👤",
                AutoSize = false,
                Size = new Size(iconW, inputH),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(8, 0),
                Font = new Font("Segoe UI Emoji", 14),
                BackColor = Color.Transparent
            };
            this.userBorder.Controls.Add(this.userIcon);

            this.txtUsername = new TextBox()
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11F),
                ForeColor = Color.FromArgb(30, 30, 30),
                BackColor = Color.White,
                Location = new Point(8 + iconW + 8, (inputH - 20) / 2),
                Size = new Size(controlWidth - (8 + iconW + 16), 20)
            };

            try { this.txtUsername.PlaceholderText = "Registre un usuario"; } catch { }
            this.userBorder.Controls.Add(this.txtUsername);

            y += this.userBorder.Height + gap;
            //y += this.passBorder.Height + gap;
            //contraseña conteiner
            this.passBorder = new Panel()
            {
                BackColor = Color.White,
                Size = new Size(controlWidth, inputH),
                Location = new Point(leftPadding, y)
            };
            this.passBorder.Paint += InputBorder_Paint;
            this.rightPanel.Controls.Add(this.passBorder);

            this.passIcon = new Label()
            {
                Text = "🔒",
                AutoSize = false,
                Size = new Size(iconW, inputH),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(8, 0),
                Font = new Font("Segoe UI Emoji", 14),
                BackColor = Color.Transparent
            };
            this.passBorder.Controls.Add(this.passIcon);
            this.txtPassword = new TextBox()
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true,
                ForeColor = Color.FromArgb(30, 30, 30),
                BackColor = Color.White,
                Location = new Point(8 + iconW + 8, (inputH - 20) / 2),
                Size = new Size(controlWidth - (8 + iconW + 16), 20)
            };
            try { this.txtPassword.PlaceholderText = "Contraseña"; } catch { }
            this.passBorder.Controls.Add(this.txtPassword);
            y += this.passBorder.Height + gap;
            //confirmar contraseña conteiner
            var confirmPassBorder = new Panel()
            {
                BackColor = Color.White,
                Size = new Size(controlWidth, inputH),
                Location = new Point(leftPadding, y)
            };
            confirmPassBorder.Paint += InputBorder_Paint;
            this.rightPanel.Controls.Add(confirmPassBorder);
            //icom pass
            var confirmPassIcon = new Label()
            {
                Text = "🔒",
                AutoSize = false,
                Size = new Size(iconW, inputH),
                TextAlign = ContentAlignment.MiddleCenter,
                Location = new Point(8, 0),
                Font = new Font("Segoe UI Emoji", 14),
                BackColor = Color.Transparent
            };
            confirmPassBorder.Controls.Add(confirmPassIcon);

            // Add confirm password textbox
            this.txtConfirmPassword = new TextBox()
            {
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 11F),
                UseSystemPasswordChar = true,
                ForeColor = Color.FromArgb(30, 30, 30),
                BackColor = Color.White,
                Location = new Point(8 + iconW + 8, (inputH - 20) / 2),
                Size = new Size(controlWidth - (8 + iconW + 16), 20)
            };
            try { this.txtConfirmPassword.PlaceholderText = "Confirmar contraseña"; } catch { }
            confirmPassBorder.Controls.Add(this.txtConfirmPassword);
            y += confirmPassBorder.Height + gap;
            y += confirmPassBorder.Height + gap;
            // Continue with the button and rest of the form...

            // Login button (degradado)
            this.btnOk = new Button()
            {
                Text = "Registrarse",
                Size = new Size(controlWidth, 42),
                Location = new Point(leftPadding, y),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Paint += BtnOk_Paint;
            this.btnOk.Click += new EventHandler(this.btnOk_Click); // Añade esta línea aquí
            this.rightPanel.Controls.Add(this.btnOk);


            var dp = new Panel()
            {
                Size = new Size(controlWidth, 40),
                Location = new Point(leftPadding, y),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };


            this.ResumeLayout(false);
            this.PerformLayout();
        }


        // Dibujar borde redondeado y ligero brillo en la tarjeta
        private void MainCard_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            if (p == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using (GraphicsPath path = RoundedRect(r, 12))
            {
                using (SolidBrush b = new SolidBrush(Color.FromArgb(240, Color.White)))
                    e.Graphics.FillPath(b, path);
                using (Pen pen = new Pen(Color.FromArgb(200, Color.White)))
                    e.Graphics.DrawPath(pen, path);
            }
        }

        private void InputBorder_Paint(object sender, PaintEventArgs e)
        {
            var p = sender as Panel;
            if (p == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, p.Width - 1, p.Height - 1);
            using (GraphicsPath path = RoundedRect(r, 8))
            {
                // Fill white background
                using (SolidBrush b = new SolidBrush(Color.White))
                    e.Graphics.FillPath(b, path);
                // Draw black border
                using (Pen pen = new Pen(Color.FromArgb(60, 60, 60), 1))
                    e.Graphics.DrawPath(pen, path);
            }
        }

        // Pintar degradado en botón LOGIN
        private void BtnOk_Paint(object sender, PaintEventArgs e)
        {
            var btn = sender as Button;
            if (btn == null) return;
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Rectangle r = new Rectangle(0, 0, btn.Width, btn.Height);
            using (LinearGradientBrush lb = new LinearGradientBrush(r, Color.FromArgb(0x0F, 0x5F, 0x7A), Color.FromArgb(0x11, 0x7A, 0xA1), 0f))
            {
                e.Graphics.FillRectangle(lb, r);
            }
            TextRenderer.DrawText(e.Graphics, btn.Text, btn.Font, r, btn.ForeColor, TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }

        // Helper: crear GraphicsPath con rectángulo redondeado
        private GraphicsPath RoundedRect(Rectangle bounds, int radius)
        {
            var path = new GraphicsPath();
            int d = radius * 2;
            path.AddArc(bounds.Left, bounds.Top, d, d, 180, 90);
            path.AddArc(bounds.Right - d, bounds.Top, d, d, 270, 90);
            path.AddArc(bounds.Right - d, bounds.Bottom - d, d, d, 0, 90);
            path.AddArc(bounds.Left, bounds.Bottom - d, d, d, 90, 90);
            path.CloseFigure();
            return path;
        }

        // Pequeña clase separator como Panel simple (evita usar control custom en otro archivo)
        private class SeparatorLabel : Panel
        {
            public SeparatorLabel() { this.Height = 1; this.BackColor = Color.FromArgb(50, Color.Gray); }
        }
    }
}