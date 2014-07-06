namespace Client.Logic
{
    using System;
    using System.Threading;

    internal sealed class Program
    {
        #region Methods

        [STAThread]
        private static void Main(string[] args) {
            //System.Windows.Forms.Application.EnableVisualStyles();
            //System.Windows.Forms.Application.SetCompatibleTextRenderingDefault(false);
            Client.Logic.Loader.InitLoader(args);
        }

        #endregion Methods
    }
}