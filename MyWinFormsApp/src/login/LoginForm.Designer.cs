using System.Windows.Forms;
using System.Drawing;

namespace MyWinFormsApp
{
    partial class LoginForm
    {
        private PictureBox pbLogo;
        private Label lblWelcome;
        private Button btnEmployee;
        private Button btnClient;
        private TableLayoutPanel layout;
        private FlowLayoutPanel flowButtons;

        // ...existing code...
        private void InitializeComponent()
        {
            // instanciar los controles antes de usarlos
            this.pbLogo = new PictureBox();
            this.lblWelcome = new Label();
            this.btnEmployee = new Button();
            this.btnClient = new Button();
            this.layout = new TableLayoutPanel();
            this.flowButtons = new FlowLayoutPanel();

            // LoginForm design
            this.SuspendLayout();
            this.ClientSize = new Size(420, 320);
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = true;
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "Inicio de sesión";

            // permitir que el formulario muestre imagen de fondo y se adapte
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.White;

            // layout
            this.layout.Dock = DockStyle.Fill;
            this.layout.RowCount = 3;
            this.layout.ColumnCount = 1;
            this.layout.ColumnStyles.Clear();
            this.layout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Percent, 55F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Absolute, 40F));
            this.layout.RowStyles.Add(new RowStyle(SizeType.Percent, 45F));
            // dejar el layout transparente para que se vea el fondo
            this.layout.BackColor = Color.Transparent;
            this.Controls.Add(this.layout);

            // pbLogo
            this.pbLogo.SizeMode = PictureBoxSizeMode.Zoom;
            this.pbLogo.Size = new Size(140, 140);
            this.pbLogo.BackColor = Color.Transparent;
            this.pbLogo.Anchor = AnchorStyles.None;
            this.layout.Controls.Add(this.pbLogo, 0, 0);

            // lblWelcome
            this.lblWelcome.Text = "Bienvenido a GICELL";
            this.lblWelcome.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            this.lblWelcome.TextAlign = ContentAlignment.MiddleCenter;
            this.lblWelcome.Dock = DockStyle.Fill;
            this.lblWelcome.BackColor = Color.Transparent;
            this.layout.Controls.Add(this.lblWelcome, 0, 1);

            // flowButtons - centrado
            this.flowButtons.FlowDirection = FlowDirection.LeftToRight;
            this.flowButtons.WrapContents = false;
            this.flowButtons.AutoSize = true;
            this.flowButtons.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            this.flowButtons.Padding = new Padding(0);
            this.flowButtons.Anchor = AnchorStyles.None;
            this.flowButtons.BackColor = Color.Transparent;
            this.layout.Controls.Add(this.flowButtons, 0, 2);

            // btnEmployee - fondo blanco y borde negro
            this.btnEmployee.Text = "Empleado";
            this.btnEmployee.Size = new Size(140, 44);
            this.btnEmployee.Margin = new Padding(10, 6, 10, 6);
            this.btnEmployee.Click += btnEmployee_Click;
            this.btnEmployee.FlatStyle = FlatStyle.Flat;
            this.btnEmployee.FlatAppearance.BorderColor = Color.Black;
            this.btnEmployee.FlatAppearance.BorderSize = 1;
            this.btnEmployee.BackColor = Color.White;
            this.btnEmployee.ForeColor = Color.Black;
            this.btnEmployee.UseVisualStyleBackColor = false;
            this.flowButtons.Controls.Add(this.btnEmployee);

            // btnClient - fondo blanco y borde negro
            this.btnClient.Text = "Cliente";
            this.btnClient.Size = new Size(140, 44);
            this.btnClient.Margin = new Padding(10, 6, 10, 6);
            this.btnClient.Click += btnClient_Click;
            this.btnClient.FlatStyle = FlatStyle.Flat;
            this.btnClient.FlatAppearance.BorderColor = Color.Black;
            this.btnClient.FlatAppearance.BorderSize = 1;
            this.btnClient.BackColor = Color.White;
            this.btnClient.ForeColor = Color.Black;
            this.btnClient.UseVisualStyleBackColor = false;
            this.flowButtons.Controls.Add(this.btnClient);

            this.ResumeLayout(false);
        }
        // ...existing code...
    }
}
// ...existing code...