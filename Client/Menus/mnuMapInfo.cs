/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


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