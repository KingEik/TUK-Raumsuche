using System;
using System.Collections.Generic;
using System.Threading;
using System.Windows.Forms;

namespace TUK_Raumsuche
{
    static class Program
    {
        /// <summary>
        /// Der Haupteinstiegspunkt für die Anwendung.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(Program.CurrentDomain_UnhandledException);
            Application.ThreadException += new ThreadExceptionEventHandler(Program.CurrentDomain_ThreadException);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Main());
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            Exception ex = e.ExceptionObject as Exception;
            logExeption(ex);
        }

        private static void CurrentDomain_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            Exception exception = e.Exception;
            logExeption(exception);
        }

        private static void logExeption(Exception e)
        {
            Logger.error(e.GetType().Name, e.Message, e.StackTrace);
        }
    }
}
