using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.Windows.Core
{
    class WindowCore : Window
    {
        public WindowCore(string name)
            : base(name) {

            this.ShowInWindowSwitcher = false;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            this.TitleBar.BackColor = Color.Blue;
            this.TitleBar.FillColor = Color.White;
            //this.AlwaysOnTop = true;
            this.BackColor = Color.White;
            this.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            this.BorderWidth = 2;
            this.BorderColor = Color.Black;

        }
    }
}
