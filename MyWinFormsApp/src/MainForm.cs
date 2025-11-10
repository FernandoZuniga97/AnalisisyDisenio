using System;
using System.Drawing;
using System.Windows.Forms;

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

            Color start = Color.White;
            Color end = Color.LightGray;

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

        private void ToggleSubMenuExclusive(Panel panelToToggle, BoolRef flag, int targetHeight)
        {
            Panel[] allPanels = { panelSubMenu1, panelSubMenu5 };

            foreach (var p in allPanels)
            {
                if (p == null) continue;

                if (p != panelToToggle)
                {
                    p.Visible = false;
                    p.Height = 0;
                    if (p == panelSubMenu1) isSubMenu1Expanded.Value = false;
                    if (p == panelSubMenu5) isSubMenu5Expanded.Value = false;
                }
            }

            if (!flag.Value)
            {
                panelToToggle.Height = 0;
                panelToToggle.Visible = true;

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

        private void BtnMantenimiento_Click(object sender, EventArgs e)
        {
            ToggleSubMenuExclusive(panelSubMenu1, isSubMenu1Expanded, 40);
        }

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
    }
}
