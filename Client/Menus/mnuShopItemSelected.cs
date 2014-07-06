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
    
    class mnuShopItemSelected : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal
        {
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

        public int ItemNum
        {
            get { return itemNum; }
            set
            {
                itemNum = value;

                if (transaction == Enums.InvMenuType.Buy)
                {
                    if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                    {
                        lblMove.Text = "Buy Amount:";
                        nudAmount.Visible = true;
                    }
                    else
                    {
                        lblMove.Text = "Buy";
                        nudAmount.Visible = false;
                    }
                }
                else if (transaction == Enums.InvMenuType.Sell)
                {
                    if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                    {
                        lblMove.Text = "Sell Amount:";
                        nudAmount.Visible = true;
                    }
                    else
                    {
                        lblMove.Text = "Sell";
                        nudAmount.Visible = false;
                    }

                }
            }

        }

        public int ItemSlot
        {
            get { return itemSlot; }
            set
            {
                itemSlot = value;
            }
        }

        public Widgets.BorderedPanel MenuPanel
        {
            get { return this; }
        }
        
        
        public mnuShopItemSelected(string name, int itemNum, int itemSlot, Enums.InvMenuType transactionType)
            : base(name)
        {
            transaction = transactionType;
            
            
            base.Size = new Size(185, 125);
            
            base.MenuDirection = Enums.MenuDirection.Horizontal;
            if (transaction == Enums.InvMenuType.Buy) {
            base.Location = new Point(435, 40);
            } else {
                
                base.Location = new Point(335, 40);
            }

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
            nudAmount.Font = FontManager.LoadFont("PMU", 16);
            nudAmount.Minimum = 1;
            nudAmount.Maximum = Int32.MaxValue;

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
        
        void lblSummary_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(1);
        }


        void lblMove_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(0);
        }

        public void ChangeSelected(int itemNum)
        {
            itemPicker.Location = new Point(18, 23 + (50 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                case SdlDotNet.Input.Key.DownArrow:
                    {
                        if (itemPicker.SelectedItem == MAX_ITEMS)
                        {
                            ChangeSelected(0);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow:
                    {
                        if (itemPicker.SelectedItem == 0)
                        {
                            ChangeSelected(MAX_ITEMS);
                        }
                        else
                        {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return:
                    {
                        SelectItem(itemPicker.SelectedItem);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace:
                    {
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
                    { // buy/sell item
                        if (itemNum > 0)
                        {
                            if (transaction == Enums.InvMenuType.Buy)
                            {
                                if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                                {
                                    if (nudAmount.Value > 0)
                                    {
                                        Messenger.TradeRequest(nudAmount.Value, itemSlot);
                                    }
                                    else
                                    {
                                        //say you must buy a number of items greater than 0.
                                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                                    }
                                }
                                else
                                {
                                    Messenger.TradeRequest(0, itemSlot);
                                }
                            }
                            else if (transaction == Enums.InvMenuType.Sell)
                            {
                                if (Items.ItemHelper.Items[itemNum].StackCap > 0 || Items.ItemHelper.Items[itemNum].Type == Enums.ItemType.Currency)
                                {
                                    if (nudAmount.Value > 0)
                                    {
                                        Messenger.SellItem(nudAmount.Value, itemNum);
                                    }
                                    else
                                    {
                                        //say you must sell a number of items greater than 0.
                                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                                    }
                                }
                                else
                                {
                                    Messenger.SellItem(0, itemNum);
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

        private void CloseMenu()
        {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            if (transaction == Enums.InvMenuType.Buy)
            {
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShop");
            }
            else if (transaction == Enums.InvMenuType.Sell)
            {
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");
            }
        }
        
    }
}
