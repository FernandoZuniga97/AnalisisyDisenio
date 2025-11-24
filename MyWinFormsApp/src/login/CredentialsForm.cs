using System;
using System.Data.SqlClient;
using System.Windows.Forms;
using MyWinFormsApp.Database;

namespace MyWinFormsApp
{
    public partial class CredentialsForm : Form
    {
        public string Username => txtUsername.Text.Trim();
        public string Password => txtPassword.Text;
        public string RoleSelected { get; private set; } = "CLIENTE"; // default

        public CredentialsForm(string roleTitle = "")
        {
            InitializeComponent();
            if (!string.IsNullOrEmpty(roleTitle))
            {
                this.Text = $"GICELL - {roleTitle}";
                // normaliza: esperaremos "EMPLEADO" o "CLIENTE" en roleTitle;
                // si te pasaron "Employee"/"Client" lo convertimos:
                if (roleTitle.Equals("Employee", StringComparison.OrdinalIgnoreCase) ||
                    roleTitle.Equals("Empleado", StringComparison.OrdinalIgnoreCase))
                    RoleSelected = "EMPLEADO";
                else if (roleTitle.Equals("Client", StringComparison.OrdinalIgnoreCase) ||
                         roleTitle.Equals("Cliente", StringComparison.OrdinalIgnoreCase))
                    RoleSelected = "CLIENTE";
                else if (roleTitle.Equals("EMPLEADO", StringComparison.OrdinalIgnoreCase) ||
                         roleTitle.Equals("CLIENTE", StringComparison.OrdinalIgnoreCase))
                    RoleSelected = roleTitle.ToUpper();
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                MessageBox.Show("Ingrese usuario y contraseña.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Validar contra BD
            var auth = DbConfig.ValidarLogin(txtUsername.Text.Trim(), txtPassword.Text, RoleSelected);
            if (auth == null)
            {
                // comprobar si existe el usuario pero con otro rol -> mensaje específico
                // intentamos buscar por username únicamente
                bool existsUsername = false;
                try
                {
                    using (var conn = DbConfig.GetConnection())
                    {
                        conn.Open();
                        using (var cmd = new SqlCommand("SELECT Rol FROM UsuariosDelSistema WHERE Username = @u", conn))
                        {
                            cmd.Parameters.AddWithValue("@u", txtUsername.Text.Trim());
                            var o = cmd.ExecuteScalar();
                            if (o != null && o != DBNull.Value) existsUsername = true;
                            if (existsUsername)
                            {
                                string dbRol = o.ToString();
                                MessageBox.Show($"Esa cuenta es de rol '{dbRol}'.\nNo puedes iniciar sesión como {RoleSelected}.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                            else
                            {
                                MessageBox.Show("Usuario o contraseña incorrectos.", "Acceso denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                    }
                }
                catch
                {
                    MessageBox.Show("No se pudo validar los datos en la base de datos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                return;
            }

            // Login OK
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
