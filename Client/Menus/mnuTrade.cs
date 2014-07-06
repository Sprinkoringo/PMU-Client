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

namespace Client.Logic.Menus
{
    class mnuTrade : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields
        ListBox lstInv;
        Button btnItemSet;
        Label lblAmount;
        NumericUpDown nudAmount;
        Label lblClient;
        Label lblClientItem;
        Label lblMyItem;
        Button btnConfirm;
        Button btnCancel;
        string tradePartner;

        bool itemSet;
        bool partnerItemSet;
        #endregion Fields

        #region Constructors

        public mnuTrade(string name, string tradePartner)
            : base(name) {
            this.tradePartner = tradePartner;

            this.Size = new Size(330, 350);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = Client.Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

            lstInv = new ListBox("lstInv");
            lstInv.Location = new Point(40, 20);
            lstInv.Size = new Size(250, 170);
            lstInv.ItemSelected += new EventHandler(lstInv_ItemSelected);

            lblAmount = new Label("lblAmount");
            lblAmount.AutoSize = true;
            lblAmount.Location = new Point(40, 194);
            lblAmount.Font = FontManager.LoadFont("PMU", 18);
            lblAmount.ForeColor = Color.WhiteSmoke;
            lblAmount.Text = "Amount:";
            lblAmount.Visible = false;

            nudAmount = new NumericUpDown("nudAmount");
            nudAmount.Size = new Size(120, 24);
            nudAmount.Location = new Point(120, 194);
            nudAmount.Maximum = Int32.MaxValue;
            nudAmount.Minimum = 1;
            nudAmount.Visible = false;

            lblClient = new Label("lblClient");
            lblClient.AutoSize = true;
            lblClient.Location = new Point(40, 220);
            lblClient.Font = FontManager.LoadFont("PMU", 18);
            lblClient.ForeColor = Color.WhiteSmoke;
            lblClient.Text = "Trading with: " + this.tradePartner;

            lblClientItem = new Label("lblClientItem");
            lblClientItem.AutoSize = true;
            lblClientItem.Location = new Point(40, 240);
            lblClientItem.Font = FontManager.LoadFont("PMU", 18);
            lblClientItem.ForeColor = Color.WhiteSmoke;
            lblClientItem.Text = "Trader's Item:";

            lblMyItem = new Label("lblMyItem");
            lblMyItem.AutoSize = true;
            lblMyItem.Location = new Point(40, 260);
            lblMyItem.Font = FontManager.LoadFont("PMU", 18);
            lblMyItem.ForeColor = Color.WhiteSmoke;
            lblMyItem.Text = "My Item:";

            btnConfirm = new Button("btnConfirm");
            btnConfirm.Size = new Size(82, 30);
            btnConfirm.Location = new Point(208, 300);
            btnConfirm.Font = FontManager.LoadFont("PMU", 18);
            btnConfirm.Text = "Confirm";
            btnConfirm.Click += new EventHandler<MouseButtonEventArgs>(btnConfirm_Click);

            btnItemSet = new Button("btnItemSet");
            btnItemSet.Size = new Size(82, 30);
            btnItemSet.Location = new Point(126, 300);
            btnItemSet.Font = FontManager.LoadFont("PMU", 18);
            btnItemSet.Text = "Set Item";
            btnItemSet.Click += new EventHandler<MouseButtonEventArgs>(btnItemSet_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Size = new Size(82, 30);
            btnCancel.Location = new Point(44, 300);
            btnCancel.Font = FontManager.LoadFont("PMU", 18);
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            this.AddWidget(lstInv);
            this.AddWidget(lblAmount);
            this.AddWidget(nudAmount);
            this.AddWidget(btnItemSet);
            this.AddWidget(lblClient);
            this.AddWidget(lblClientItem);
            this.AddWidget(btnConfirm);
            this.AddWidget(btnCancel);
            this.AddWidget(lblMyItem);

            LoadInventory();
        }
        #endregion Constructors

        #region Methods

        void LoadInventory() {
            SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            lstInv.Items.Clear();
            for (int i = 1; i < Players.PlayerManager.MyPlayer.Inventory.Length; i++) {
                if (Players.PlayerManager.MyPlayer.Inventory[i].Num > 0) {
                    string itemName = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.Inventory[i].Num].Name;
                    if (Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.Inventory[i].Num].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.Inventory[i].Num].StackCap > 0) {
                        itemName += " (" + Players.PlayerManager.MyPlayer.Inventory[i].Value + ")";
                    }
                    if (!string.IsNullOrEmpty(itemName)) {
                        if (lstInv.Items.Count <= i - 1) {
                            ListBoxTextItem item = new ListBoxTextItem(font, itemName);
                            item.Tag = i;
                            lstInv.Items.Add(item);
                        } else {
                            ((ListBoxTextItem)lstInv.Items[i - 1]).Text = itemName;
                        }
                    }
                }
            }
        }

        void lstInv_ItemSelected(object sender, EventArgs e) {
            int itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum((int)lstInv.SelectedItems[0].Tag);
            if (Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[itemNum].StackCap > 0) {
                lblAmount.Visible = true;
                nudAmount.Visible = true;
                nudAmount.Value = 1;
            } else {
                lblAmount.Visible = false;
                nudAmount.Visible = false;
            }
        }

        void btnItemSet_Click(object sender, MouseButtonEventArgs e) {
            if (itemSet == false) {
                if (lstInv.SelectedItems.Count == 1) {
                    Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("settradeitem", (int)lstInv.SelectedItems[0].Tag, nudAmount.Value));
                    //lblMyItem.Text = "My Item: " + Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.Inventory[(int)lstInv.SelectedItems[0].Tag].Num].Name;
                    itemSet = true;
                    //btnItemSet.Text = "Unset Item";
                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                } else {
                    //TODO: Allow Empty Trading
                }
            } else {
                Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("settradeitem", "-1", "0"));
                //lblMyItem.Text = "My Item: No item offered yet";
                //itemSet = false;
                //btnItemSet.Text = "Set Item";
                Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
            }
        }

        void btnConfirm_Click(object sender, MouseButtonEventArgs e) {
            if (itemSet && partnerItemSet) {
                Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("readytotrade"));
                btnConfirm.Selected = true;
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("endplayertrade"));
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        }

        public void UpdateSetItem(int itemNum, int amount) {
            if (itemNum > -1) {
        			lblMyItem.Text = "My Item: " + Items.ItemHelper.Items[itemNum].Name;
                    if (Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[itemNum].StackCap > 0) {
                        lblMyItem.Text += " (" + amount + ")";
                    }
                itemSet = true;
                btnItemSet.Text = "Unset Item";
            } else {
                lblMyItem.Text = "My Item:";
                itemSet = false;
                btnItemSet.Text = "Set Item";
            }
        }

        public void UpdatePartnersSetItem(int itemNum, int amount) {
            if (itemNum > -1) {
                lblClientItem.Text = "Trader's Item: " + Items.ItemHelper.Items[itemNum].Name;
                if (Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[itemNum].StackCap > 0) {
                    lblClientItem.Text += " (" + amount + ")";
                }
                partnerItemSet = true;
            } else {
                lblClientItem.Text = "Trader's Item:";
                partnerItemSet = false;
            }
        }

        public void UnconfirmTrade() {
            btnConfirm.Selected = false;
        }

        public void ResetTradeData() {
            lblMyItem.Text = "My Item:";
            itemSet = false;
            lblClientItem.Text = "Trader's Item:";
            partnerItemSet = false;
            btnItemSet.Text = "Set Item";
            UnconfirmTrade();
            if (lstInv.SelectedItems.Count == 1) {
                lstInv.SelectedItems.Remove(lstInv.SelectedItems[0]);
            }
            LoadInventory();
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }
        #endregion Methods
    }
}
