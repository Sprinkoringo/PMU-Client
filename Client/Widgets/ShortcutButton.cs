using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Graphics;

namespace Client.Logic.Widgets
{
    class ShortcutButton : PictureBox
    {
        public ShortcutButton(string name)
            : base(name) {
                this.Size = new Size(50, 50);
        }
    }
}
