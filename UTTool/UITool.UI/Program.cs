using System;

namespace UITool.UI
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.

            Application.ThreadException += Application_ThreadException;

            //ApplicationConfiguration.Initialize();
            Application.Run(new Index());
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            MessageBox.Show((e.Exception as Exception).Message);
        }


    }
}