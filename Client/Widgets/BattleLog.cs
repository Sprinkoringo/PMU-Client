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
    class BattleLog : BorderedPanel
    {
        

        Label lblLog;
        public Timer tmrHide;

        public BattleLog(string name)
            : base(name) {
            lblLog = new Label("lblLog");
            lblLog.BackColor = Color.Transparent;
            lblLog.Size = new Size(this.Size.Width, this.Size.Height);
            lblLog.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblLog.Location = new Point(0, -4);

            this.Resized += new EventHandler(BattleLog_Resized);

            tmrHide = new Timer("tmrHide");
            tmrHide.Interval = 5000;
            tmrHide.Elapsed += new EventHandler(tmrHide_Elapsed);
            tmrHide.Start();

            this.AddWidget(tmrHide);
            this.WidgetPanel.AddWidget(lblLog);
        }

        void tmrHide_Elapsed(object sender, EventArgs e) {
            this.Visible = false;
            tmrHide.Stop();
        }

        void BattleLog_Resized(object sender, EventArgs e) {
            lblLog.Size = new Size(this.Size.Width, this.Size.Height);
        }

        public void AddLog(string message, Color color) {
            Logic.Logs.BattleLog.AddLog(message, color);
            string[] messageArray = Logic.Logs.BattleLog.Messages.ToArray();
            Color[] colorArray = Logic.Logs.BattleLog.MessageColor.ToArray();

            lblLog.Text = "";
            for (int i = Math.Max(messageArray.Length - Logic.Logs.BattleLog.MaxShownMessages, 0); i < messageArray.Length; i++) {
                lblLog.AppendText(messageArray[i], new CharRenderOptions(colorArray[i]));
                lblLog.AppendText("\n");
            }
        }
    }
}
