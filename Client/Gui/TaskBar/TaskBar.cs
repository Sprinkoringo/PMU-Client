namespace Client.Logic.Gui.TaskBar
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;

    class TaskBar : Core.Control
    {
        #region Fields

        private List<TaskBarButton> mButtons;
        private Gfx.Surface mTaskBarStartSurf;

        #endregion Fields

        #region Constructors

        public TaskBar()
            : base()
        {
            this.Size = new Size(SdlDotNet.Graphics.Video.Screen.Width, 20);
            mButtons = new List<TaskBarButton>();
            Init();
        }

        #endregion Constructors

        #region Methods

        public void AddButton(TaskBarButton buttonToAdd)
        {
            buttonToAdd.Parent = this;
            mButtons.Add(buttonToAdd);
            Init();
        }

        public void Redraw()
        {
            Init();
        }

        public void RemoveButton(Windows.Core.Window windowToRemove)
        {
            TaskBarButton button = FindWindowButton(windowToRemove);
            if (button != null) {
                button.Close();
                mButtons.Remove(button);
                Init();
            }
        }

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            base.Update(dstSrf, e);
        }

        private TaskBarButton FindWindowButton(Windows.Core.Window windowToFind)
        {
            for (int i = 0; i < mButtons.Count; i++) {
                if (mButtons[i].AttachedWindow == windowToFind) {
                    return mButtons[i];
                }
            }
            return null;
        }

        private void Init()
        {
            base.Buffer.Fill(Color.Transparent);
            mTaskBarStartSurf = new SdlDotNet.Graphics.Surface(IO.IO.CreateOSPath("Skins\\" + Globals.ActiveSkin + "\\General\\TaskBar\\taskbar.png"));
            mTaskBarStartSurf.Transparent = true;
            mTaskBarStartSurf.TransparentColor = Color.Transparent;
            base.Buffer.Blit(mTaskBarStartSurf, new Point(0, 0));
            int lastX = mTaskBarStartSurf.Width;
            for (int i = 0; i < mButtons.Count; i++) {
                mButtons[i].Location = new Point(lastX, 3);
                base.Buffer.Blit(mButtons[i].Render(), new Point(lastX, 3));
                lastX += mButtons[i].Size.Width;
            }
        }

        #endregion Methods
    }
}