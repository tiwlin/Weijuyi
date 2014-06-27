using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace UploadTool
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Login());

            UserModel user = new UserModel();
            Login loginForm = new Login(user);
            loginForm.ShowDialog();

            if (loginForm.DialogResult == DialogResult.OK)
            {
                Application.Run(new Upload(user));
            }

        }
    }
}
