using System;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    public partial class LoginForm : Form
    {
        public enum UserRole { Employee, Client }
        public UserRole SelectedRole { get; private set; } = UserRole.Client;
        public string Username { get; private set; }

        public LoginForm()
        {
            InitializeComponent();
            LoadLogoSafe();
            LoadBackgroundSafe();
        }
        //------
        // ...existing code...
        private void LoadBackgroundSafe()
        {
            try
            {
                string[] candidates =
                {
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "fon.jpg"),
           // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "fondo_1.JPG"),
           // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "back.jpg"),
            Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "login", "Image", "fon.jpg"),
           // Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "login", "Image", "back.jpg")
        };

                string found = null;
                foreach (var c in candidates)
                {
                    if (File.Exists(c))
                    {
                        found = c;
                        break;
                    }
                }

                if (found == null)
                {
                    // DEBUG: rutas probadas (descomentar si quieres verlas)
                    // MessageBox.Show("No se encontró fondo. Rutas probadas:\n" + string.Join("\n", candidates));
                    return;
                }

                // cargar de forma segura (evita bloqueo del archivo)
                using (var img = Image.FromFile(found))
                {
                    var bmp = new Bitmap(img); // copia en memoria
                    if (this.BackgroundImage != null) { this.BackgroundImage.Dispose(); this.BackgroundImage = null; }
                    this.BackgroundImage = bmp;
                    this.BackgroundImageLayout = ImageLayout.Stretch;
                }

                // asegurar transparencia de controles
                if (this.layout != null) this.layout.BackColor = Color.Transparent;
                if (this.pbLogo != null) this.pbLogo.BackColor = Color.Transparent;
                if (this.lblWelcome != null) this.lblWelcome.BackColor = Color.Transparent;
                if (this.flowButtons != null) this.flowButtons.BackColor = Color.Transparent;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error cargando fondo:\n{ex.Message}\nRuta probada: {Environment.CurrentDirectory}", "Fondo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }


        private void LoadLogoSafe()
        {
            try
            {
                //ruta de la pinche imagen
                var imgPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Image", "logo_g.jpg");

                //ruta alternativa por si acaso
                if (!File.Exists(imgPath))
                {
                    var alt = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "src", "login", "Image", "logo_g.jpg");
                    if (File.Exists(alt)) imgPath = alt;
                }

                if (File.Exists(imgPath))
                {
                    pbLogo.Image = Image.FromFile(imgPath);
                }
                else
                {
                    // opcional: mostrar aviso en modo desarrollo
                    // MessageBox.Show($"No se encontró {imgPath}");
                }
            }
            catch
            {
                // dejar placeholder si falla
            }
        }

        private void btnEmployee_Click(object sender, EventArgs e)
        {
            ShowCredentialsAndClose(UserRole.Employee);
        }

        private void btnClient_Click(object sender, EventArgs e)
        {
            ShowCredentialsAndClose(UserRole.Client);
        }

        private void ShowCredentialsAndClose(UserRole role)
        {
            using (var cred = new CredentialsForm(role.ToString()))
            {
                var res = cred.ShowDialog();
                if (res == DialogResult.OK)
                {
                    SelectedRole = role;
                    Username = cred.Username;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
        }
    }
}
//continuar otro dia con mas paciencia