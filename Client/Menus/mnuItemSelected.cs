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
using PMU.Core;
using SdlDotNet.Widgets;
using Client.Logic.Network;


namespace Client.Logic.Menus
{
    class mnuItemSelected : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        bool useable;
        int itemSlot;
        Label lblHold;
        Label lblUse;
        Label lblDrop;
        Label lblSummary;
        Label lblThrow;
        NumericUpDown nudAmount;
        Widgets.MenuItemPicker itemPicker;
        int maxItems;

        public int ItemSlot {
            get { return itemSlot; }
            set {
                itemSlot = value;
                if (Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot == itemSlot) {
                    lblHold.Text = "Take";
                } else {
                    lblHold.Text = "Give";
                }
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuItemSelected(string name, int itemSlot)
            : base(name) {

            if ((int)Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].Type < 8 || (int)Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].Type == 15) {
                //cannot use item
                base.Size = new Size(165, 165);
                maxItems = 3;
                useable = false;
            } else {
                //can use item
                base.Size = new Size(165, 195);
                maxItems = 4;
                useable = true;
            }
            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(335, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            int widgetY = 8;

            //add choices
            lblHold = new Label("lblHold");
            lblHold.Size = new System.Drawing.Size(120, 32);
            lblHold.Font = FontManager.LoadFont("PMU", 32);
            //lblHold.AutoSize = true;
            lblHold.Text = "Hold";
            lblHold.Location = new Point(30, widgetY);
            lblHold.HoverColor = Color.Red;
            lblHold.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblHold_Click);
            lblHold.ForeColor = Color.WhiteSmoke;

            this.AddWidget(lblHold);
            widgetY += 30;

            if (useable) {
                lblUse = new Label("lblUse");
                lblUse.Font = FontManager.LoadFont("PMU", 32);
                lblUse.AutoSize = true;
                lblUse.Text = "Use";
                lblUse.Location = new Point(30, widgetY);
                lblUse.HoverColor = Color.Red;
                lblUse.ForeColor = Color.WhiteSmoke;
                lblUse.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblUse_Click);

                this.AddWidget(lblUse);
                widgetY += 30;
            }

            lblThrow = new Label("lblThrow");
            lblThrow.Size = new System.Drawing.Size(120, 32);
            lblThrow.Location = new Point(30, widgetY);
            lblThrow.Font = FontManager.LoadFont("PMU", 32);
            lblThrow.Text = "Throw";
            lblThrow.HoverColor = Color.Red;
            lblThrow.ForeColor = Color.WhiteSmoke;
            lblThrow.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblThrow_Click);

            widgetY += 30;

            lblSummary = new Label("lblSummary");
            lblSummary.Size = new System.Drawing.Size(120, 32);
            lblSummary.Location = new Point(30, widgetY);
            lblSummary.Font = FontManager.LoadFont("PMU", 32);
            lblSummary.Text = "Summary";
            lblSummary.HoverColor = Color.Red;
            lblSummary.ForeColor = Color.WhiteSmoke;
            lblSummary.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSummary_Click);

            widgetY += 30;

            lblDrop = new Label("lblDrop");
            lblDrop.Font = FontManager.LoadFont("PMU", 32);
            lblDrop.Size = new System.Drawing.Size(130, 32);
            lblDrop.AutoSize = false;
            //lblDrop.Text = "Drop";
            lblDrop.Location = new Point(30, widgetY);
            lblDrop.HoverColor = Color.Red;
            lblDrop.ForeColor = Color.WhiteSmoke;
            lblDrop.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblDrop_Click);

            widgetY += 32;

            if (Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].StackCap > 0) {
                lblDrop.Text = "Drop Amount:";
                nudAmount = new NumericUpDown("nudAmount");
                nudAmount.Size = new Size(120, 24);
                nudAmount.Location = new Point(32, widgetY);
                nudAmount.Maximum = Players.PlayerManager.MyPlayer.Inventory[itemSlot].Value;
                nudAmount.Minimum = 1;

                this.AddWidget(nudAmount);
            } else {
                lblDrop.Text = "Drop";
            }



            this.AddWidget(lblDrop);
            this.AddWidget(lblSummary);
            this.AddWidget(lblThrow);

            this.AddWidget(itemPicker);

            this.ItemSlot = itemSlot;

        }

        void lblSummary_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (useable) {
                SelectItem(3);
            } else {
                SelectItem(2);
            }
        }

        void lblThrow_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (useable) {
                SelectItem(2);
            } else {
                SelectItem(1);
            }
        }

        void lblDrop_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (useable) {
                SelectItem(4);
            } else {
                SelectItem(3);
            }
        }

        void lblUse_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }

        void lblHold_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == maxItems) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(maxItems);
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
                case SdlDotNet.Input.Key.Backspace: {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            if (!useable && itemNum != 0) {
                itemNum++;
            }


            switch (itemNum) {
                case 0://Hold/remove Item
                    {
                        if (Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot == itemSlot) {
                            Messenger.SendRemoveItem(itemSlot);
                        } else {
                            Messenger.SendHoldItem(itemSlot);
                        }

                    }
                    break;
                case 1: { // Use item
                        if (Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot) > 0) {
                    		  Messenger.SendUseItem(itemSlot);
                            switch (Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].Type) {
                                case Enums.ItemType.Key:
                                    CloseMenu();
                                    break;
                            }
                        }
                    }
                    break;
                case 2: { // Throw
                        if (Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot) > 0) {
                            Messenger.SendThrowItem(itemSlot);
                        }
                    }
                    break;
                case 3: { // View item summary
                        MenuSwitcher.ShowItemSummary(Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot), itemSlot, Enums.InvMenuType.Use);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 4: { // Drop item
                        if (Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].Type == Enums.ItemType.Currency || Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemSlot)].StackCap > 0) {
                            Messenger.SendDropItem(itemSlot, nudAmount.Value);
                        } else {
                            Messenger.SendDropItem(itemSlot, 0);
                        }
                    }
                    break;
            }
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");

        }
        
        

        private void CloseMenu() {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");
        }

    }
}
