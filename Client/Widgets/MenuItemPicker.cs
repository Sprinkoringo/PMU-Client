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

namespace Client.Logic.Widgets
{
    class MenuItemPicker : Widget
    {
        short lineLength = 10;
        int selectedItem;

        public int SelectedItem {
            get { return selectedItem; }
            set { selectedItem = value; }
        }
        public MenuItemPicker(string name)
            : base(name) {
                this.Size = new Size(30, 20);
                this.BackColor = Color.Transparent;

                base.Paint += new EventHandler(MenuItemPicker_Paint);
        }

        void MenuItemPicker_Paint(object sender, EventArgs e) {
            this.Buffer.Draw(new SdlDotNet.Graphics.Primitives.Triangle(0, 0, 0, lineLength, lineLength, (short)(lineLength / 2)), Color.WhiteSmoke, false, true);
            this.Buffer.Draw(new SdlDotNet.Graphics.Primitives.Triangle(0, 0, 0, lineLength, lineLength, (short)(lineLength / 2)), Color.Black, false, false);
        }
    }
}
