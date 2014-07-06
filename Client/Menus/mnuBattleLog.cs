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
