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
