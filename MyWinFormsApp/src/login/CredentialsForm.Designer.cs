using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    partial class CredentialsForm
    {
        private Label lblUser;
        private TextBox txtUsername;
        private Label lblPass;
        private TextBox txtPassword;
        private Button btnOk;
        private Button btnCancel;

        private void InitializeComponent()
        {
            this.lblUser = new Label();
            this.txtUsername = new TextBox();
            this.lblPass = new Label();
            this.txtPassword = new TextBox();
            this.btnOk = new Button();
            this.btnCancel = new Button();

            this.SuspendLayout();

            // CredentialsForm
            this.ClientSize = new Size(360, 180);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Credenciales";
            this.BackColor = Color.White;
            this.MaximizeBox = false;

            //usu
            this.lblUser.Text = "Usuario:";
            this.lblUser.Location = new Point(20, 20);
            this.lblUser.Size = new Size(60, 24);
            this.Controls.Add(this.lblUser);

            //usu
            this.txtUsername.Location = new Point(20, 44);
            this.txtUsername.Size = new Size(320, 24);
            this.Controls.Add(this.txtUsername);

            //contra
            this.lblPass.Text = "Contraseña:";
            this.lblPass.Location = new Point(20, 78);
            this.lblPass.Size = new Size(80, 24);
            this.Controls.Add(this.lblPass);

            //contra
            this.txtPassword.Location = new Point(20, 102);
            this.txtPassword.Size = new Size(320, 24);
            this.txtPassword.UseSystemPasswordChar = true;
            this.Controls.Add(this.txtPassword);


            this.btnOk.Text = "Aceptar";
            this.btnOk.Size = new Size(100, 32);
            this.btnOk.Location = new Point(60, 136);
            this.btnOk.FlatStyle = FlatStyle.Flat;
            this.btnOk.FlatAppearance.BorderColor = Color.Black;
            this.btnOk.FlatAppearance.BorderSize = 1;
            this.btnOk.BackColor = Color.White;
            this.btnOk.ForeColor = Color.Black;
            this.btnOk.UseVisualStyleBackColor = false;
            this.btnOk.Click += btnOk_Click;
            this.Controls.Add(this.btnOk);


            this.btnCancel.Text = "Cancelar";
            this.btnCancel.Size = new Size(100, 32);
            this.btnCancel.Location = new Point(200, 136);
            this.btnCancel.FlatStyle = FlatStyle.Flat;
            this.btnCancel.FlatAppearance.BorderColor = Color.Black;
            this.btnCancel.FlatAppearance.BorderSize = 1;
            this.btnCancel.BackColor = Color.White;
            this.btnCancel.ForeColor = Color.Black;
            this.btnCancel.UseVisualStyleBackColor = false;
            this.btnCancel.Click += btnCancel_Click;
            this.Controls.Add(this.btnCancel);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
    }
}
