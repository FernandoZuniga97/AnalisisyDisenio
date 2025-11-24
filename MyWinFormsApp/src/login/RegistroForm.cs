using System;
using System.Windows.Forms;
using MyWinFormsApp.Database;

namespace MyWinFormsApp
{
    public partial class RegistroForm : Form
    {
        private readonly string _rolSeleccionado;

        public RegistroForm(string rolSeleccionado)
        {
            InitializeComponent();
            _rolSeleccionado = string.IsNullOrWhiteSpace(rolSeleccionado) ? "CLIENTE" : rolSeleccionado;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            // Validaciones
            if (string.IsNullOrWhiteSpace(txtUsername.Text) ||
                string.IsNullOrEmpty(txtPassword.Text) ||
                string.IsNullOrEmpty(txtConfirmPassword.Text))
            {
                MessageBox.Show("Por favor complete todos los campos.",
                              "Campos Requeridos",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                return;
            }

            if (txtPassword.Text != txtConfirmPassword.Text)
            {
                MessageBox.Show("Las contraseñas no coinciden.",
                              "Error de Verificación",
                              MessageBoxButtons.OK,
                              MessageBoxIcon.Warning);
                txtConfirmPassword.Clear();
                txtConfirmPassword.Focus();
                return;
            }

            // intentar registrar en BD
            try
            {
                int newId = DbConfig.RegistrarUsuario(txtUsername.Text.Trim(), txtPassword.Text, _rolSeleccionado);
                if (newId > 0)
                {
                    MessageBox.Show("Usuario registrado correctamente.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    //MessageBox.Show("ROL RECIBIDO = " + _rolSeleccionado);

                    MessageBox.Show("No se pudo registrar el usuario. Revise si el nombre de usuario ya existe.", "Registro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error registrando usuario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}