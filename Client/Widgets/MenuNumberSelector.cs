using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet.Widgets;
using SdlDotNet.Graphics;

namespace Client.Logic.Widgets
{
    class MenuNumberSelector : Widget
    {
        Label lblLeftArrow;
        Label lblRightArrow;
        Label lblSelectedNumber;

        SdlDotNet.Graphics.Font font;

        public SdlDotNet.Graphics.Font Font {
            get {
                CheckFont();
                return font;
            }
            set {
                font = value;
            }
        }

        public MenuNumberSelector(string name)
            : base(name, true) {
                base.Paint += new EventHandler(MenuNumberSelector_Paint);
        }

        void MenuNumberSelector_Paint(object sender, EventArgs e) {
        }

        private void CheckFont() {
            if (font == null) {
                font = new Font(SdlDotNet.Widgets.Widgets.DefaultFontPath, SdlDotNet.Widgets.Widgets.DefaultFontSize);
            }
        }
    }
}
