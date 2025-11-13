using System;
using System.Drawing;
using System.Windows.Forms;
using MyWinFormsApp.src.modulo4;
using MyWinFormsApp.src.modulo2;


namespace MyWinFormsApp
{
    public partial class MainForm : Form
    {
        public class BoolRef
        {
            public bool Value;
            public BoolRef(bool v) => Value = v;
        }

        private BoolRef isSubMenu1Expanded = new BoolRef(false);
        private BoolRef isSubMenu2Expanded = new BoolRef(false);
        private BoolRef isSubMenu3Expanded = new BoolRef(false);
        private BoolRef isSubMenu4Expanded = new BoolRef(false);
        private BoolRef isSubMenu5Expanded = new BoolRef(false);
        private Control currentContent = null;
        private readonly bool _isEmployee;

        private Timer _animTimer;
        private Button _animButton;
        private int _animStep;
        private const int _animSteps = 8;

        public MainForm(bool isEmployee)
        {
            _isEmployee = isEmployee;
            InitializeComponent();
            InitializeAnimation();
            ConfigureModules();
        }

        private void ConfigureModules()
        {
            btnModule1.Visible = btnModule2.Visible =
            btnModule3.Visible = btnModule4.Visible = _isEmployee;
            btnModule5.Visible = true;

            if (!_isEmployee)
                panelLeft.Width = 140;
        }

        private void ModuleButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag?.ToString(), out int n))
                lblContent.Text = $"Hola mundo desde Módulo {n}";
            else
                lblContent.Text = "Hola mundo";
        }

        private void InitializeAnimation()
        {
            _animTimer = new Timer();
            _animTimer.Interval = 20;
            _animTimer.Tick += AnimTimer_Tick;
            _animButton = null;
        }

        private void AnimateButton_Click(object sender, EventArgs e)
        {
            if (sender is Button b && _animButton == null)
            {
                _animButton = b;
                _animStep = 0;
                _animTimer.Start();
            }
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (_animButton == null)
            {
                _animTimer.Stop();
                return;
            }

            double t = (_animStep <= _animSteps / 2)
                ? (double)_animStep / (_animSteps / 2)
                : (double)(_animSteps - _animStep) / (_animSteps / 2);

            Color start = Color.LightGray;
            Color end = Color.White;

            _animButton.BackColor = LerpColor(start, end, t);

            _animStep++;
            if (_animStep > _animSteps)
            {
                _animButton.BackColor = Color.White;
                _animButton = null;
                _animTimer.Stop();
            }
        }

        private static Color LerpColor(Color a, Color b, double t)
        {
            t = Math.Max(0, Math.Min(1, t));
            int r = (int)(a.R + (b.R - a.R) * t);
            int g = (int)(a.G + (b.G - a.G) * t);
            int bl = (int)(a.B + (b.B - a.B) * t);
            return Color.FromArgb(r, g, bl);
        }
        // --- MainForm.cs: Reemplazar ToggleSubMenuExclusive ---

        // --- MainForm.cs ---

        // --- MainForm.cs: Reemplazar ToggleSubMenuExclusive ---

        private void ToggleSubMenuExclusive(Panel panelToToggle, BoolRef flag, int targetHeight)
        {
            Panel[] allPanels = { panelSubMenu1, panelSubMenu2, panelSubMenu3, panelSubMenu4, panelSubMenu5 };

            Button associatedButton = null;
            if (panelToToggle == panelSubMenu1) associatedButton = btnModule1;
            else if (panelToToggle == panelSubMenu2) associatedButton = btnModule2;
            else if (panelToToggle == panelSubMenu3) associatedButton = btnModule3;
            else if (panelToToggle == panelSubMenu4) associatedButton = btnModule4;
            else if (panelToToggle == panelSubMenu5) associatedButton = btnModule5;

            // 1. **CLAVE**: Reinicia todos los colores a Blanco ANTES de verificar el estado.
            ResetAllMainButtons();
            ResetAllSubButtons();

            // Colapsar todos los demás paneles.
            foreach (var p in allPanels)
            {
                if (p == null) continue;
                if (p != panelToToggle)
                {
                    p.Visible = false;
                    p.Height = 0;
                    // ... (Lógica de flags de submenú) ...
                }
            }

            if (!flag.Value)
            {
                // ABRIENDO: Aplica el color gris para mantener el estado "activo".
                if (associatedButton != null)
                    associatedButton.BackColor = Color.LightGray; // Gris claro para el botón principal

                panelToToggle.Height = 0;
                panelToToggle.Visible = true;
                panelToToggle.BackColor = Color.FromArgb(200, 200, 200); // Gris oscuro para el submenú

                // ... (Lógica del Timer para expandir) ...
                Timer t = new Timer { Interval = 10 };
                t.Tick += (s, e) =>
                {
                    if (panelToToggle.Height >= targetHeight)
                    {
                        t.Stop();
                        t.Dispose();
                        flag.Value = true;
                    }
                    else
                        panelToToggle.Height += 4;
                };
                t.Start();
            }
            else
            {
                // CERRANDO: Restablece solo el color del botón que se está cerrando.
                if (associatedButton != null)
                    associatedButton.BackColor = Color.White; // Vuelve a blanco el botón que se cierra

                panelToToggle.BackColor = Color.FromArgb(235, 240, 255); // Vuelve al color base el submenú

                // ... (Lógica del Timer para colapsar) ...
                Timer t = new Timer { Interval = 10 };
                t.Tick += (s, e) =>
                {
                    if (panelToToggle.Height <= 0)
                    {
                        panelToToggle.Visible = false;
                        t.Stop();
                        t.Dispose();
                        flag.Value = false;
                    }
                    else
                        panelToToggle.Height -= 4;
                };
                t.Start();
            }
        }
        //---------
        private void HandleSubButtonClick(Button selectedSubButton)
        {

            ResetAllSubButtons();
            selectedSubButton.BackColor = Color.FromArgb(200, 200, 200);
        }
        //------------
        private void HandleButtonClick(Button selectedButton)
        {

            ResetAllMainButtons();
            selectedButton.BackColor = ColorTranslator.FromHtml("#F0F0F0");
        }
        //---------------
        private void ResetAllSubButtons()
        {
            // Reestablece el color de todos los botones de submenú a su color base (WhiteSmoke)
            Color baseBtnColor = Color.WhiteSmoke;
            // Submenú 1
            btnDnR.BackColor = baseBtnColor;
            btnDnryr.BackColor = baseBtnColor;

            // Submenú 2 (si aplica)
            btnReparacionPorEstado.BackColor = baseBtnColor;

            // Submenú 3
            btnMante.BackColor = baseBtnColor;
            btnExce.BackColor = baseBtnColor;

            // Submenú 5
            btnInventario.BackColor = baseBtnColor;
            btnTipoFallas.BackColor = baseBtnColor;
        }
        //-------------
        private void ResetAllMainButtons()
        {
            // Restablece el color de todos los botones principales a Blanco
            btnModule1.BackColor = Color.White;
            btnModule2.BackColor = Color.White;
            btnModule3.BackColor = Color.White;
            btnModule4.BackColor = Color.White;
            btnModule5.BackColor = Color.White;

            // Restablece el color de fondo de todos los paneles de submenú a su color base claro
            // Este es el color Color.FromArgb(235, 240, 255) que configuraste para los paneles
            Color baseSubMenuColor = Color.FromArgb(235, 240, 255);

            panelSubMenu1.BackColor = baseSubMenuColor;
            panelSubMenu2.BackColor = baseSubMenuColor;
            panelSubMenu3.BackColor = baseSubMenuColor;
            panelSubMenu4.BackColor = baseSubMenuColor;
            panelSubMenu5.BackColor = baseSubMenuColor;
        }

        //bottones 1
        private void BtnMantenimiento_Click(object sender, EventArgs e)
        {
            ToggleSubMenuExclusive(panelSubMenu1, isSubMenu1Expanded, 80);
        }


        //sub menu de botones del modulo 1
        private void BtnDnR_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new RDdispositivosnorepaForm();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }
        //parte de 2 del asub menu
        private void BtnDnryr_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new DispositivosnRyRForm();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }
        //termina sub menu del modulo 1
        //bototones 3
        private void Anto3_Click(object sender, EventArgs e)
        {
            ToggleSubMenuExclusive(panelSubMenu3, isSubMenu3Expanded, 80);
        }
        //sub menu de botones del modulo 3
        private void BtnMante_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new ReporteMantenimiento();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }
        //parte de 2 del asub menu
        private void BtnExce_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new ReporteExcepcion();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }

        //botones 5
        private void BtnAdministracion_Click(object sender, EventArgs e)
        {
            ToggleSubMenuExclusive(panelSubMenu5, isSubMenu5Expanded, 80); // ALTURA AJUSTADA PARA DOS BOTONES
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new InventarioPartesForm();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }

        private void BtnTipoFallas_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new TipoDeFallasForm();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }

        // BOTÓN "Actualización de estado"
        private void BtnActualizacion_Click(object sender, EventArgs e)
        {
            ToggleSubMenuExclusive(panelSubMenu2, isSubMenu2Expanded, 50);
        }

        // SUBMENÚ: Reparaciones por Estado
        private void BtnReparacionPorEstado_Click(object sender, EventArgs e)
        {
            if (currentContent != null)
            {
                panelContent.Controls.Remove(currentContent);
                currentContent.Dispose();
                currentContent = null;
            }

            var form = new ReparacionPorEstadoForm();
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            panelContent.Controls.Add(form);
            currentContent = form;

            form.Show();
            lblContent.Visible = false;
        }

        private void LogoutButton_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show(
                "¿Estás seguro que deseas cerrar sesión?",
                "Confirmación de Cierre de Sesión",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question
            );

            if (result == DialogResult.Yes)
            {
                // Oculta el formulario actual (MainForm)
                this.Hide();
                var loginForm = new CredentialsForm();
                CenterToScreen();
                loginForm.Show();
                this.Close();
            }
            // Si es No, no hace nada y se mantiene en el formulario.
        }

    }

}
