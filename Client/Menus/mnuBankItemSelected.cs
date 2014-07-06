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
    class mnuBankItemSelected : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        int itemNum;
        int itemSlot;
        Enums.InvMenuType transaction;
        Label lblMove;
        NumericUpDown nudAmount;
        Label lblSummary;
        Widgets.MenuItemPicker itemPicker;
        const int MAX_ITEMS = 1;

        public int ItemNum {
            get { return itemNum; }
            set {
                itemNum = value;

                if (transaction == Enums.InvMenuType.Store)
                {
                    if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                    {
                        lblMove.Text = "Store Amount:";
                        nudAmount.Visible = true;
                    } else {
                        lblMove.Text = "Store";
                        nudAmount.Visible = false;
                    }
                }
                else if (transaction == Enums.InvMenuType.Take)
                {
                    if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                    {
                        lblMove.Text = "Take Amount:";
                        nudAmount.Visible = true;
                    } else {
                        lblMove.Text = "Take";
                        nudAmount.Visible = false;
                    }

                }
            }

        }

        public int ItemSlot {
            get { return itemSlot; }
            set {
                itemSlot = value;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuBankItemSelected(string name, int itemNum, int amount, int itemSlot, Enums.InvMenuType transactionType)
            : base(name) {
            transaction = transactionType;


            base.Size = new Size(185, 125);

            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(335, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblMove = new Label("lblMove");
            lblMove.Font = FontManager.LoadFont("PMU", 32);
            lblMove.AutoSize = true;
            lblMove.Text = "Store";
            lblMove.Location = new Point(30, 8);
            lblMove.HoverColor = Color.Red;
            lblMove.ForeColor = Color.WhiteSmoke;
            lblMove.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblMove_Click);

            nudAmount = new NumericUpDown("nudAmount");
            nudAmount.Size = new Size(120, 24);
            nudAmount.Location = new Point(32, 42);
            nudAmount.Maximum = amount;
            nudAmount.Minimum = 1;

            lblSummary = new Label("lblSummary");
            lblSummary.Font = FontManager.LoadFont("PMU", 32);
            lblSummary.AutoSize = true;
            lblSummary.Text = "Summary";
            lblSummary.Location = new Point(30, 58);
            lblSummary.HoverColor = Color.Red;
            lblSummary.ForeColor = Color.WhiteSmoke;
            lblSummary.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSummary_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblMove);
            this.AddWidget(nudAmount);
            this.AddWidget(lblSummary);

            this.ItemSlot = itemSlot;
            this.ItemNum = itemNum;
        }

        void lblSummary_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }


        void lblMove_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (50 * itemNum));
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
                case SdlDotNet.Input.Key.Backspace: {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        
        private void SelectItem(int selectedItem)
        {
            switch (selectedItem)
            {
                case 0:
                    { // deposit/withdraw item
                        if (itemNum > 0)
                        {
                            if (transaction == Enums.InvMenuType.Store)
                            {
                                if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                                {
                                    if (nudAmount.Value > 0)
                                    {
                                        Messenger.BankDeposit(itemSlot, nudAmount.Value);
                                    }
                                    else
                                    {
                                        //say you must store a number of items greater than 0.
                                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                                    }
                                } else {
                                    Messenger.BankDeposit(itemSlot, 0);
                                }
                            }
                            else if (transaction == Enums.InvMenuType.Take)
                            {
                                if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                                {
                                    if (nudAmount.Value > 0)
                                    {
                                        Messenger.BankWithdraw(itemSlot + 1, nudAmount.Value);
                                    }
                                    else
                                    {
                                        //say you must take a number of items greater than 0.
                                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                                    }
                                } else {
                                    Messenger.BankWithdraw(itemSlot + 1, 0);
                                }
                            }
                        }
                        CloseMenu();
                    }
                    break;
                case 1:
                    { // View item summary
                        
                        MenuSwitcher.ShowItemSummary(itemNum, itemSlot, transaction);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        private void CloseMenu() {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            if (transaction == Enums.InvMenuType.Store)
            {
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");
            }
            else if (transaction == Enums.InvMenuType.Take)
            {
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBank");
            }
        }

    }
}
