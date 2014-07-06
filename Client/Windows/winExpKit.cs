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


using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winExpKit : Core.WindowCore
    {
        ExpKit.KitContainer kitContainer;

        public ExpKit.KitContainer KitContainer {
            get { return kitContainer; }
        }

        public winExpKit()
            : base("winExpKit") {

            //this.Location = Graphics.DrawingSupport.GetCenter(this.Size);
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 450);
            this.Location = new System.Drawing.Point(5, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 5);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = false;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Explorer Kit";
            this.TitleBar.FillColor = System.Drawing.Color.Transparent;
            this.TitleBar.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.TitleBar.BackgroundImage = Skins.SkinManager.LoadGuiElement("Game Window", "Widgets/expkitTitleBar.png");

            kitContainer = new ExpKit.KitContainer("kitContainer");
            kitContainer.Location = new System.Drawing.Point(0, 0);
            kitContainer.Size = this.Size;

            this.AddWidget(kitContainer);

            this.LoadComplete();
        }

    }
}
