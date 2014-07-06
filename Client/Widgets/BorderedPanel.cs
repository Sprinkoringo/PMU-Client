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
using System.Drawing;
using Client.Logic.Graphics;
using SdlDotNet.Graphics;

namespace Client.Logic.Widgets
{
    class BorderedPanel : Menus.Core.MenuBase
    {
        const int VERTICAL_BORDER_WIDTH = 5;
        const int VERTICAL_BORDER_HEIGHT = 11;

        const int HORIZONTAL_BORDER_WIDTH = 14;
        const int HORIZONTAL_BORDER_HEIGHT = 5;

        Panel widgetPanel;
        PictureBox border;

        byte backgroundAlpha;

        public Panel WidgetPanel {
            get { return widgetPanel; }
        }

        public byte BackgroundAlpha {
            get { return backgroundAlpha; }
            set {
                backgroundAlpha = value;
                border.Alpha = backgroundAlpha;
            }
        }

        public BorderedPanel(string name)
            : base(name) {
            this.BackColor = Color.Transparent;

            backgroundAlpha = 150;

            widgetPanel = new Panel("widgetPanel");
            widgetPanel.BackColor = Color.Transparent;

            border = new PictureBox("border");
            border.Location = new Point(0, 0);
            border.BackColor = Color.Transparent;
            border.Alpha = backgroundAlpha;

            this.GroupedWidget = border;

            //this.AddWidget(border);
            this.AddWidget(widgetPanel);

            this.Resized += new EventHandler(BorderedPanel_Resized);
            this.LocationChanged += new EventHandler(BorderedPanel_LocationChanged);
        }

        void BorderedPanel_LocationChanged(object sender, EventArgs e) {
            border.Location = this.Location;
        }

        void BorderedPanel_Resized(object sender, EventArgs e) {
            switch (DetermineDirection()) {
                case Enums.MenuDirection.Horizontal: {
                        this.BackgroundImage = GraphicsCache.MenuHorizontalBorder.CreateStretchedSurface(this.Size);
                        border.Size = this.Size;
                        Surface imageSurf = GraphicsCache.MenuHorizontalFill.CreateStretchedSurface(this.Size);
                        imageSurf.Transparent = true;
                        border.Image = imageSurf;
                        border.Alpha = backgroundAlpha;
                        widgetPanel.Size = new Size(this.Width - HORIZONTAL_BORDER_WIDTH * 2, this.Height - HORIZONTAL_BORDER_HEIGHT * 2);
                        widgetPanel.Location = new Point(HORIZONTAL_BORDER_WIDTH, HORIZONTAL_BORDER_HEIGHT);
                    }
                    break;
                case Enums.MenuDirection.Vertical: {
                        this.BackgroundImage = GraphicsCache.MenuVerticalBorder.CreateStretchedSurface(this.Size);
                        border.Size = this.Size;
                        Surface imageSurf = GraphicsCache.MenuVerticalFill.CreateStretchedSurface(this.Size);
                        imageSurf.Transparent = true;
                        border.Image = imageSurf;
                        border.Alpha = backgroundAlpha;
                        widgetPanel.Size = new Size(this.Width - VERTICAL_BORDER_WIDTH * 2, this.Height - VERTICAL_BORDER_HEIGHT * 2);
                        widgetPanel.Location = new Point(VERTICAL_BORDER_WIDTH, VERTICAL_BORDER_HEIGHT);
                    }
                    break;
            }
        }

        public new Enums.MenuDirection MenuDirection {
            get { return DetermineDirection(); }
            set { }
        }

        Enums.MenuDirection DetermineDirection() {
            if (this.Width > this.Height) {
                return Enums.MenuDirection.Horizontal;
            } else {
                return Enums.MenuDirection.Vertical;
            }
        }

        public override void BlitToScreen(Surface destinationSurface) {
            if (this.Parent == null) {
                border.BlitToScreen(destinationSurface);
            }
            base.BlitToScreen(destinationSurface);
        }
    }
}
