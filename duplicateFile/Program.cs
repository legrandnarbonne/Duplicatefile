using SimpleLogger;
using System;
using System.Windows.Forms;

namespace duplicateFile
{
    internal static class Program
    {
        /// <summary>
        ///     Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        private static void Main(string[] args)
        {
            SimpleLog.SetLogFile(".\\Log", "Log_");
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm(args));
        }
    }
}