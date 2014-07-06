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