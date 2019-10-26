using System;
using System.Windows.Forms;

namespace CipherTools
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
            Application.Run(new MainMenu());
        }
        
        public static bool ExitProgram()
        {
            DialogResult ClosingMessage = MessageBox.Show("Are you sure you want to exit the program?", "Message", MessageBoxButtons.YesNo);
            // displays a message box when the user clicks on the cross in the top right.

            if (ClosingMessage == DialogResult.Yes)
            {
                Environment.Exit(0); // if they press the 'yes' button, the program closes.
            }

            return true; // if they press the 'no' button, the message box closes and the form stays open.
        }
    }
}
