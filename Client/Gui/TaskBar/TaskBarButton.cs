namespace Client.Logic.Gui.TaskBar
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;

    class TaskBarButton : Core.Control
    {
        #region Fields

        private Gfx.Surface mBackgroundSurf;
        private Windows.Core.Window mWindow;

        #endregion Fields

        #region Constructors

        public TaskBarButton(Windows.Core.Window parentWindow)
        {
            mWindow = parentWindow;
            this.Size = new System.Drawing.Size(140, 15);
            base.AddEvents();
            this.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(TaskBarButton_OnClick);
        }

        #endregion Constructors

        #region Properties

        public Windows.Core.Window AttachedWindow
        {
            get { return mWindow; }
        }

        #endregion Properties

        #region Methods

        public override void Close()
        {
            mBackgroundSurf.Close();
            base.RemoveEvents();
            this.OnClick -= new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(TaskBarButton_OnClick);
            base.Close();
        }

        public Gfx.Surface Render()
        {
            Init();
            return base.Buffer;
        }

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            base.Update(dstSrf, e);
        }

        private void Init()
        {
            mBackgroundSurf = new SdlDotNet.Graphics.Surface(IO.IO.CreateOSPath("Skins\\" + Globals.ActiveSkin + "\\General\\TaskBar\\taskbarbutton.png"));
            if (mWindow.TaskBarText != "") {
                Gfx.Font font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
                Gfx.Surface textSurf = font.Render(mWindow.TaskBarText, Color.White);
                //textSurf = textSurf.CreateStretchedSurface(new Size(130, 12));
                mBackgroundSurf.Blit(textSurf, GetCenter(mBackgroundSurf, textSurf.Size), new Rectangle(0, 0, 125, 14));
                string stateString = "?";
                switch (mWindow.WindowState) {
                    case Client.Logic.Windows.WindowManager.WindowState.Normal:
                        stateString = "^";
                        break;
                    case Client.Logic.Windows.WindowManager.WindowState.Minimized:
                        stateString = "v";
                        break;
                    case Client.Logic.Windows.WindowManager.WindowState.Maximized:
                        stateString = "[]";
                        break;
                }
                mBackgroundSurf.Blit(font.Render(stateString, Color.White), new Point(this.Width - font.SizeText(stateString).Width - 1, 0));
                font.Close();
            }
            base.Buffer.Blit(mBackgroundSurf, new Point(0, 0));
        }

        void TaskBarButton_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (mWindow.WindowState == Client.Logic.Windows.WindowManager.WindowState.Minimized) {
                mWindow.WindowState = Client.Logic.Windows.WindowManager.WindowState.Normal;
                Windows.WindowManager.BringWindowToFront(mWindow);
            } else if (mWindow.WindowState == Client.Logic.Windows.WindowManager.WindowState.Normal) {
                if (Windows.WindowManager.IsWindowTopMost(mWindow)) {
                    mWindow.WindowState = Client.Logic.Windows.WindowManager.WindowState.Minimized;
                } else {
                    mWindow.BringToFront();
                }
            } else if (mWindow.WindowState == Client.Logic.Windows.WindowManager.WindowState.Maximized) {
                mWindow.BringToFront();
            }
        }

        #endregion Methods
    }
}