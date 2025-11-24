using System;
using System.Windows.Forms;
using static MyWinFormsApp.LoginForm;

namespace MyWinFormsApp
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            using (var login = new LoginForm())
            {
                if (login.ShowDialog() == DialogResult.OK)
                {
                    // Convertimos UserRole → string "EMPLEADO" / "CLIENTE"
                    string selectedRoleString =
                        (login.SelectedRole == UserRole.Employee)
                        ? "EMPLEADO"
                        : "CLIENTE";

                    bool isEmployee = selectedRoleString == "EMPLEADO";

                    var main = new MainForm(isEmployee);
                    main.WindowState = FormWindowState.Maximized;
                    Application.Run(main);
                }
            }
        }
    }
}
