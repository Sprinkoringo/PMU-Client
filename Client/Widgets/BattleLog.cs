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
