using System;
using System.Windows.Forms;

namespace Sha1Check
{

    // .NET 3.5 - style delegates
    internal delegate void Action<T>(T value);
    internal delegate void Action<T1, T2>(T1 value1, T2 value2);
    internal delegate T3 Func<T1, T2, T3>(T1 value1, T2 value2);


    /// <summary>
    /// </summary>
    public static class Program
    {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Control.CheckForIllegalCrossThreadCalls = true;
            Application.Run(new MainForm());
        }

    }

}