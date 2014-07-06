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
    class ShortcutBar : Panel
    {
        List<ShortcutButton> buttons;

        public ShortcutBar(string name)
            : base(name) {
            this.Size = new Size(400, 60);
            this.BackColor = Color.Transparent;

            buttons = new List<ShortcutButton>();
        }

        public void AddButton(ShortcutButton button) {
            int totalWidth = 5; // Padding
            for (int i = 0; i < buttons.Count; i++) {
                totalWidth += buttons[i].Width + 5;
            }
            button.Location = new Point(this.Width - totalWidth - button.Width, 5);
            buttons.Add(button);
            this.AddWidget(button);
        }

        public override void FreeResources() {
            base.FreeResources();
            buttons.Clear();
        }
    }
}
