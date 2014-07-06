namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = Client.Logic.Graphics;
    //using Gui = Client.Logic.Gui;
    using SdlDotNet.Widgets;

    class winLoading : Core.WindowCore
    {
        #region Fields

        Label lblInfo;

        #endregion Fields

        #region Constructors

        public winLoading()
            : base("winLoading") {
            this.BackgroundImageSizeMode = ImageSizeMode.AutoSize;
            this.BackgroundImage = Skins.SkinManager.LoadGui("Loading Bar");
            this.Location = Gfx.DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);
            this.Windowed = false;
            this.Name = "winLoading";
            this.ShowInWindowSwitcher = false;

            lblInfo = new Label("lblInfo");
            lblInfo.Font = Gfx.FontManager.LoadFont("PMU", 32);
            lblInfo.Location = new Point(35, 2);
            lblInfo.AutoSize = false;
            lblInfo.AntiAlias = false;
            lblInfo.Size = new Size(230, 32);
            lblInfo.BackColor = Color.Transparent;
            lblInfo.ForeColor = Color.Black;

            this.AddWidget(lblInfo);

            this.LoadComplete();
        }

        #endregion Constructors

        #region Methods

        public void UpdateLoadText(string newText) {
            lblInfo.Text = newText;
        }

        #endregion Methods
    }
}