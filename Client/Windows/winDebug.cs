namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = Client.Logic.Graphics;
    using Gui = Client.Logic.Gui;
    using Client.Logic.Windows.Core;

    class winDebug : Core.Window
    {
        #region Fields

        Gui.Textbox.Textbox txtTest;
        Gui.ProgressBar progressBar;
        Gui.Button btnTest;

        #endregion Fields

        #region Constructors

        public winDebug()
        {
            this.Size = new Size(300, 300);
            this.Location = GetCenter(this.Size);
            this.ShowInTaskBar = true;
            this.TaskBarText = "Debug Window";
            this.Text = "Debug Window";

            txtTest = new Client.Logic.Gui.Textbox.Textbox();
            txtTest.Location = new Point(0, 0);
            txtTest.BackColor = Color.Black;
            txtTest.ForeColor = Color.Gray;
            txtTest.Size = new Size(154, 100);
            //txtPassword.PasswordChar = '*';
            //txtPassword.Text = IO.Options.SavedPassword;
            txtTest.FocusOnClick = true;
            
            progressBar = new Client.Logic.Gui.ProgressBar();
            progressBar.Size = new Size(200, 50);
            progressBar.Location = new Point(10, 0);
            progressBar.ProgressBarColor = Color.Blue;
            progressBar.Value = 50;
            
            btnTest = new Client.Logic.Gui.Button();
            btnTest.Location = new Point(10, 0);
            btnTest.Size = new Size(50, 50);
            btnTest.Text = "Hi";

            //this.AddControl(txtTest);
            this.AddControl(progressBar);
            progressBar.AddControl(btnTest);
        }
        
		public override void Tick(SdlDotNet.Graphics.Surface dstSurf, SdlDotNet.Core.TickEventArgs e)
		{
			base.Tick(dstSurf, e);
		}
        
        #endregion Constructors
    }
}