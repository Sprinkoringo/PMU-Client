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
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus {
    class mnuBattleLog : Widgets.BorderedPanel, Core.IMenu {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        Label lblBattleLog;
        Label lblBattleEntries;
        ListBox lstBattleEntries;

        #endregion Fields

        #region Constructors

        public mnuBattleLog(string name)
            : base(name) {
            this.Size = new Size(600, 440);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 20);

            lblBattleLog = new Label("lblBattleLog");
            lblBattleLog.Location = new Point(20, 10);
            lblBattleLog.Font = FontManager.LoadFont("PMU", 36);
            lblBattleLog.AutoSize = true;
            lblBattleLog.Text = "Battle Log";
            lblBattleLog.ForeColor = Color.WhiteSmoke;

            lstBattleEntries = new ListBox("lstBattleEntries");
            lstBattleEntries.Location = new Point(16, 50);
            lstBattleEntries.Size = new Size(this.Width - lstBattleEntries.X * 2, this.Height - lstBattleEntries.Y - 20);
            lstBattleEntries.BackColor = Color.Transparent;
            lstBattleEntries.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;

            this.AddWidget(lblBattleLog);
            this.AddWidget(lstBattleEntries);
            AddEntries();
        }

        #endregion Constructors
        #region Methods

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.Backspace: {
                        // Show the others menu when the backspace key is pressed
                        MenuSwitcher.ShowOthersMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public void AddEntries() {
            string[] messageArray = Logic.Logs.BattleLog.Messages.ToArray();
            Color[] colorArray = Logic.Logs.BattleLog.MessageColor.ToArray();

            for (int i = 0; i < messageArray.Length; i++) {
                ListBoxTextItem item = new ListBoxTextItem(FontManager.LoadFont("PMU", 16), messageArray[i]);
                item.ForeColor = colorArray[i];
                lstBattleEntries.Items.Add(item);
            }
            
        }

        #endregion Methods
    }
}
