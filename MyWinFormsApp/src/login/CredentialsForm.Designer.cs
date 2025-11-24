using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    partial class CredentialsForm
    {
        private Panel mainCard;
        private Panel avatarCircle;
        private PictureBox logoPicture;
        private Panel userBorder;
        private TextBox txtUsername;
        private Label userIcon;
        private Panel passBorder;
        private TextBox txtPassword;
        private Label passIcon;
        private Button btnOk;
        private Button btnCancel;
        private CheckBox chkRemember;
        private LinkLabel lnkForgot;
        private Panel contentPanel;
        private Panel leftPanel;
        private Panel rightPanel;
        private Label footerLabel;

        private void InitializeComponent()
        {
            // Form
            this.SuspendLayout();
            this.ClientSize = new Size(760, 360); // horizontal
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Credenciales";
            this.BackColor = Color.FromArgb(0x10, 0x28, 0x44); //azul fondo oscuro
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.DoubleBuffered = true;

            // Main card (tarjeta centrada, horizontal)
            int cardW = 700;
            int cardH = 300;
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
            int gap = 12;

            // Title
            var title = new Label()
            {
                Text = "Inicio de sesión",
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

            try { this.txtUsername.PlaceholderText = "Ingrese su usuario"; } catch { }
            this.userBorder.Controls.Add(this.txtUsername);

            y += this.userBorder.Height + gap;

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

            y += this.passBorder.Height + 16;

            // Login button (degradado)
            this.btnOk = new Button()
            {
                Text = "INICIAR SESIÓN",
                Size = new Size(controlWidth, 42),
                Location = new Point(leftPadding, y),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
            };
            this.btnOk.FlatAppearance.BorderSize = 0;
            this.btnOk.Paint += BtnOk_Paint;
            this.btnOk.Click += btnOk_Click;
            this.rightPanel.Controls.Add(this.btnOk);

            y += this.btnOk.Height + 16;
            var dp = new Panel()
            {
                Size = new Size(controlWidth, 40),
                Location = new Point(leftPadding, y),
                BackColor = Color.Transparent,
                Anchor = AnchorStyles.Left | AnchorStyles.Right
            };

            //recuerdame
            this.chkRemember = new CheckBox()
            {
                Text = "Recuérdame",
                AutoSize = true,
                Location = new Point(4, 10),
                Font = new Font("Segoe UI", 9F),
                BackColor = Color.Transparent
            };
            dp.Controls.Add(this.chkRemember);

            // olvidaste la contraseña
            this.lnkForgot = new LinkLabel()
            {
                Text = "Registra un usuario",
                AutoSize = true,
                Font = new Font("Segoe UI", 9F),
                Location = new Point(dp.Width - 160, 12),
                Size = new Size(160, 20),
                LinkColor = Color.FromArgb(0x0F, 0x5F, 0x7A),
                ActiveLinkColor = Color.FromArgb(0x11, 0x7A, 0xA1),
                LinkBehavior = LinkBehavior.HoverUnderline
            };

            // Add click handler to open RegistroForm
            this.lnkForgot.Click += (s, e) =>
            {
                var registroForm = new RegistroForm(this.RoleSelected);
                registroForm.ShowDialog(this);
            };

            dp.Controls.Add(this.lnkForgot);
            this.rightPanel.Controls.Add(dp);
            //----------------------------------
            y += dp.Height + 20;
            this.footerLabel = new Label()
            {
                Text = "Copyright © 2025 GICELL.",
                AutoSize = false,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.FromArgb(80, 80, 80),
                Font = new Font("Segoe UI", 9F),
                Size = new Size(controlWidth, 30),
                Location = new Point(leftPadding, y),
                BackColor = Color.Transparent
            };
            this.rightPanel.Controls.Add(this.footerLabel);

            // Ensure proper Z-order
            dp.BringToFront();
            this.lnkForgot.BringToFront();
            this.footerLabel.BringToFront();



            // Cancel button (aligned bottom-right of card)
            this.btnCancel = new Button()
            {
                Text = "Cancelar",
                Size = new Size(88, 32),
                Location = new Point(this.mainCard.Width - 16 - 88, this.mainCard.Height - 16 - 32),
                FlatStyle = FlatStyle.Flat,
                BackColor = Color.White,
                ForeColor = Color.Black,
            };
            this.btnCancel.FlatAppearance.BorderColor = Color.Black;
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);

            this.mainCard.Controls.Add(this.btnCancel);

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