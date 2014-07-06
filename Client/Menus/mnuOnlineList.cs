using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;
using PMU.Core;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuOnlineList : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        Label lblOnlineList;
        Label lblLoading;
        Label lblTotal;
        ListBox lstOnlinePlayers;

        #endregion Fields

        #region Constructors

        public mnuOnlineList(string name)
            : base(name) {
            this.Size = new Size(185, 220);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            lblOnlineList = new Label("lblOnlineList");
            lblOnlineList.Location = new Point(20, 0);
            lblOnlineList.Font = FontManager.LoadFont("PMU", 36);
            lblOnlineList.AutoSize = true;
            lblOnlineList.Text = "Online List";
            lblOnlineList.ForeColor = Color.WhiteSmoke;

            lblLoading = new Label("lblLoading");
            lblLoading.Location = new Point(10, 50);
            lblLoading.Font = FontManager.LoadFont("PMU", 16);
            lblLoading.AutoSize = true;
            lblLoading.Text = "Loading...";
            lblLoading.ForeColor = Color.WhiteSmoke;

            lblTotal = new Label("lblTotal");
            lblTotal.Location = new Point(10, 34);
            lblTotal.Font = FontManager.LoadFont("PMU", 16);
            lblTotal.AutoSize = true;
            lblTotal.ForeColor = Color.WhiteSmoke;

            lstOnlinePlayers = new ListBox("lstOnlinePlayers");
            lstOnlinePlayers.Location = new Point(10, 50);
            lstOnlinePlayers.Size = new Size(this.Width - lstOnlinePlayers.X * 2, this.Height - lstOnlinePlayers.Y - 10);
            lstOnlinePlayers.BackColor = Color.Transparent;
            lstOnlinePlayers.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
                       
            this.AddWidget(lblOnlineList);
            this.AddWidget(lblLoading);
            this.AddWidget(lblTotal);
            this.AddWidget(lstOnlinePlayers);
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

        public void AddOnlinePlayers(string[] parse) {
            lblLoading.Visible = false;
            int count = parse[1].ToInt();

            int n = 2;

            for (int i = 0; i < count; i++) {
                ListBoxTextItem item = new ListBoxTextItem(FontManager.LoadFont("PMU", 16), parse[i+n]);
                item.ForeColor = Color.WhiteSmoke;
                lstOnlinePlayers.Items.Add(item);
            }

            lblTotal.Text = count + " Players Online";
        }

        #endregion Methods
    }
}
