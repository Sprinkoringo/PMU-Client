

namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;

    class mnuShopOptions : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields

        const int MAX_ITEMS = 1;

        Logic.Widgets.MenuItemPicker itemPicker;
        Label lblBuy;
        Label lblSell;

        #endregion Fields

        public mnuShopOptions(string name)
            : base(name) {
            this.Size = new Size(155, 88);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            itemPicker = new Logic.Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblBuy = new Label("lblBuy");
            lblBuy.AutoSize = true;
            lblBuy.Location = new Point(30, 8);
            lblBuy.Font = FontManager.LoadFont("PMU", 32);
            lblBuy.Text = "Buy";
            lblBuy.HoverColor = Color.Red;
            lblBuy.ForeColor = Color.WhiteSmoke;
            lblBuy.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblBuy_Click);

            lblSell = new Label("lblSell");
            lblSell.AutoSize = true;
            lblSell.Location = new Point(30, 38);
            lblSell.Font = FontManager.LoadFont("PMU", 32);
            lblSell.Text = "Sell";
            lblSell.HoverColor = Color.Red;
            lblSell.ForeColor = Color.WhiteSmoke;
            lblSell.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSell_Click);


            this.AddWidget(itemPicker);
            this.AddWidget(lblBuy);
            this.AddWidget(lblSell);
        }

        void lblBuy_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        void lblSell_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }


        public Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }


        #region Methods

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;

        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == MAX_ITEMS) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(MAX_ITEMS);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem);
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            switch (itemNum) {
                case 0: {
                        MenuSwitcher.ShowShopBuyMenu(0);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 1: {
                        MenuSwitcher.ShowShopSellMenu(1);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        public void Close(bool message) {
            if (message) {
                this.Close();
            } else {
                base.Close();
            }
        }

        public override void Close() {
            Network.Messenger.LeaveShop();
            base.Close();
        }
        #endregion Methods
    }
}
