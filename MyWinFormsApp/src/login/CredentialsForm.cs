using System;
using System.Windows.Forms;

namespace MyWinFormsApp
{
    public partial class CredentialsForm : Form
    {
        public string Username => txtUsername.Text.Trim();
        public string Password => txtPassword.Text;

        public CredentialsForm(string roleTitle = "")
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(roleTitle))
                this.Text = $"Credenciales";
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validación mínima: acepta cualquier par no vacío.
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
