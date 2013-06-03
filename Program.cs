using System;
using System.Windows.Forms;
using System.Reflection;
using System.IO;

namespace NAS4FreeConsole
{
    static class Program
    {
        public static Messages MessageLog;
        public static Console MainForm;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            String resource1 = "NAS4FreeConsole.Resources.Renci.SshNet.dll";
            EmbeddedAssembly.Load(resource1, "Renci.SshNet.dll");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            MainForm = new Console();
            MessageLog = new Messages();
            Logger.OnMessage += MessageLog.OnMessage;
            Application.Run(MainForm);
            MessageLog.Dispose();
            MessageLog = null;
        }

        static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            return EmbeddedAssembly.Get(args.Name);
        }
    }
}
