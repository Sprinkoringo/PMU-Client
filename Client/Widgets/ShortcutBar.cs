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
