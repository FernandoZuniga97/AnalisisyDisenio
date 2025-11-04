using System;
using System.Windows.Forms;

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
                    bool isEmployee = login.SelectedRole == LoginForm.UserRole.Employee;
                    var main = new MainForm(isEmployee);
                    main.WindowState = FormWindowState.Maximized; // <-- maximizar después del login
                    Application.Run(main);
                }
            }
        }
    }
}