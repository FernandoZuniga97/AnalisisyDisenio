using System;
using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    public partial class MainForm : Form
    {
        private bool isSubMenu5Expanded = false;
        private Control currentContent = null;

        private readonly bool _isEmployee;

        private void ConfigureModules()
        {
            // mostrar módulos 1-4 solo para empleados; módulo 5 siempre visible
            btnModule1.Visible = btnModule2.Visible = btnModule3.Visible = btnModule4.Visible = _isEmployee;
            btnModule5.Visible = true;
            // ajuste visual si es cliente
            if (!_isEmployee && panelLeft != null)
            {
                panelLeft.Width = 140;
            }
        }

        private void ModuleButton_Click(object sender, EventArgs e)
        {
            if (sender is Button btn && int.TryParse(btn.Tag?.ToString(), out int n))
            {
                lblContent.Text = $"Hola mundo desde Módulo {n}";
            }
            else
            {
                lblContent.Text = "Hola mundo";
            }
        }
        // animacion boton
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

        private void InitializeAnimation()
        {
            _animTimer = new Timer();
            _animTimer.Interval = 20;
            _animTimer.Tick += AnimTimer_Tick;
            _animButton = null;
            _animStep = 0;
        }

        // Evento que dispara la animación (hooked desde el diseñador)
        private void AnimateButton_Click(object sender, EventArgs e)
        {
            if (sender is Button b)
            {
                // iniciar animación si no está corriendo
                if (_animButton == null)
                {
                    _animButton = b;
                    _animStep = 0;
                    _animTimer.Start();
                }
            }
        }

        private void AnimTimer_Tick(object sender, EventArgs e)
        {
            if (_animButton == null)
            {
                _animTimer.Stop();
                return;
            }

            // efecto simple: flash (white -> light gray -> white)
            double t;
            if (_animStep <= _animSteps / 2) t = (double)_animStep / (_animSteps / 2);
            else t = (double)(_animSteps - _animStep) / (_animSteps / 2);

            Color start = Color.White;
            Color end = Color.LightGray;
            _animButton.BackColor = LerpColor(start, end, t);

            _animStep++;
            if (_animStep > _animSteps)
            {
                // restaurar y parar
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
        //metodos sub
        private void BtnAdministracion_Click(object sender, EventArgs e)
        {
            // Animación del submenú
            if (!isSubMenu5Expanded)
            {
                panelSubMenu5.Height = 0;
                panelSubMenu5.Visible = true;

                // Animación para expandir
                Timer timer = new Timer();
                timer.Interval = 10;
                timer.Tick += (s, args) =>
                {
                    if (panelSubMenu5.Height >= 40)
                    {
                        timer.Stop();
                        timer.Dispose();
                    }
                    else
                    {
                        panelSubMenu5.Height += 4;
                    }
                };
                timer.Start();
            }
            else
            {
                // Animación para contraer
                Timer timer = new Timer();
                timer.Interval = 10;
                timer.Tick += (s, args) =>
                {
                    if (panelSubMenu5.Height <= 0)
                    {
                        panelSubMenu5.Visible = false;
                        timer.Stop();
                        timer.Dispose();
                    }
                    else
                    {
                        panelSubMenu5.Height -= 4;
                    }
                };
                timer.Start();
            }

            isSubMenu5Expanded = !isSubMenu5Expanded;
        }

        private void BtnInventario_Click(object sender, EventArgs e)
        {
            try
            {
                if (currentContent != null)
                {
                    panelContent.Controls.Remove(currentContent);
                    currentContent.Dispose();
                    currentContent = null;
                }

                var inventarioForm = new InventarioPartesForm();
                inventarioForm.TopLevel = false;
                inventarioForm.FormBorderStyle = FormBorderStyle.None;
                inventarioForm.Dock = DockStyle.Fill;

                // IMPORTANTE: agregar AL panelContent, NO al this.Controls
                panelContent.Controls.Add(inventarioForm);
                panelContent.Tag = inventarioForm;
                currentContent = inventarioForm;

                inventarioForm.Show();
                inventarioForm.BringToFront();
                lblContent.Visible = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al cargar inventario: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
