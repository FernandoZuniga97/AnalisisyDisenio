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
<<<<<<< HEAD

=======
>>>>>>> 75d0ebd033c6ed8a21008acfe3817165ce35ef52
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
<<<<<<< HEAD
=======

>>>>>>> 75d0ebd033c6ed8a21008acfe3817165ce35ef52
        }
    }
}

