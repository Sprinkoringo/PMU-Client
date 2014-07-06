namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;

    class mnuMapInfo : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields

        Label lblMapName;

        #endregion Fields

        #region Constructors

        public mnuMapInfo(string name)
            : base(name) {
            this.Size = new Size(420, 90);
            this.MenuDirection = Enums.MenuDirection.Horizontal;
            this.Location = new Point(190, 60);

            lblMapName = new Label("lblMapName");
            lblMapName.Size = new Size(this.Width, this.Height);
            //lblMapName.AutoSize = true;
            //lblMapName.Location = new Point(0, 0);
            lblMapName.Centered = true;
            lblMapName.Font = FontManager.LoadFont("PMU", 48);
            if (Windows.WindowSwitcher.GameWindow.MapViewer.ActiveMap != null) {
                lblMapName.Text = Windows.WindowSwitcher.GameWindow.MapViewer.ActiveMap.Name;
            } else {
                lblMapName.Text = "Unknown Location";
            }
            lblMapName.ForeColor = Color.WhiteSmoke;

            this.AddWidget(lblMapName);
        }

        #endregion Constructors

        #region Properties

        public Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Properties
    }
}