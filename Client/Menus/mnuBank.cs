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
using PMU.Core;
using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuBank : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal
        {
            get;
            set;
        }

        bool loaded;
        int tempSelected;
        Label[] lblVisibleItems;
        Label lblItemCollection;
        Widgets.MenuItemPicker itemPicker;
        PictureBox picPreview;
        public int currentTen;
        Label lblItemNum;
        TextBox txtFind;
        Button btnFind;

        public List<Players.InventoryItem> BankItems
        {

            get;
            set;
        }
        
        public List<int> SortedItems {

            get;
            set;
        }


        public Widgets.BorderedPanel MenuPanel
        {
            get { return this; }
        }

        

        public mnuBank(string name, int itemSelected)
            : base(name)
        {
            base.Size = new Size(315, 400);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 32);

            currentTen = itemSelected / 10;
            
            

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblItemCollection = new Label("lblItemCollection");
            lblItemCollection.AutoSize = true;
            lblItemCollection.Font = FontManager.LoadFont("PMU", 48);
            lblItemCollection.Text = "Storage";
            lblItemCollection.Location = new Point(20, 0);
            lblItemCollection.ForeColor = Color.WhiteSmoke;

            picPreview = new PictureBox("picPreview");
            picPreview.Size = new Size(32, 32);
            picPreview.BackColor = Color.Transparent;
            picPreview.Location = new Point(255, 20);

            lblItemNum = new Label("lblItemNum");
            //lblItemNum.Size = new Size(100, 30);
            lblItemNum.AutoSize = true;
            lblItemNum.Location = new Point(182, 15);
            lblItemNum.Font = FontManager.LoadFont("PMU", 32);
            lblItemNum.BackColor = Color.Transparent;
            lblItemNum.Text = "0/0";
            lblItemNum.ForeColor = Color.WhiteSmoke;

            txtFind = new TextBox("txtFind");
            txtFind.Size = new Size(130, 20);
            txtFind.Location = new Point(32, 52);
            txtFind.Font = FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadTextBoxGui(txtFind);

            btnFind = new Button("btnFind");
            btnFind.Size = new System.Drawing.Size(40, 20);
            btnFind.Location = new Point(174, 52);
            btnFind.Font = Graphics.FontManager.LoadFont("PMU", 16);
            btnFind.Text = "Find";
            Skins.SkinManager.LoadButtonGui(btnFind);
            btnFind.Click +=new EventHandler<MouseButtonEventArgs>(btnFind_Click);

            lblVisibleItems = new Label[10];
            for (int i = 0; i < lblVisibleItems.Length; i++)
            {
                lblVisibleItems[i] = new Label("lblVisibleItems" + i);
                //lblVisibleItems[i].AutoSize = true;
                //lblVisibleItems[i].Size = new Size(200, 32);
                lblVisibleItems[i].Width = 200;
                lblVisibleItems[i].Font = FontManager.LoadFont("PMU", 32);
                lblVisibleItems[i].Location = new Point(35, (i * 30) + 72);
                //lblVisibleItems[i].HoverColor = Color.Red;
                lblVisibleItems[i].ForeColor = Color.WhiteSmoke;
                lblVisibleItems[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(bankItem_Click);
                this.AddWidget(lblVisibleItems[i]);
            }

            this.AddWidget(picPreview);
            this.AddWidget(txtFind);
            this.AddWidget(btnFind);
            this.AddWidget(lblItemCollection);
            this.AddWidget(lblItemNum);
            this.AddWidget(itemPicker);

            tempSelected = itemSelected % 10;
                //DisplayItems(currentTen * 10 + 1);
                //ChangeSelected((itemSelected - 1) % 10);
                //UpdateSelectedItemInfo();
                //loaded = true;
            lblVisibleItems[0].Text = "Loading...";
            
            
        }

        public void LoadBankItems(string[] parse)
        {
            BankItems = new List<Players.InventoryItem>();
            int maxBank = (parse.Length - 1) / 2;
            
            for (int i = 1; i <= maxBank; i++)
            {
                Players.InventoryItem invItem = new Players.InventoryItem();

                invItem.Num = parse[(i - 1) * 2 + 1].ToInt();
                invItem.Value = parse[(i - 1) * 2 + 2].ToInt();

                BankItems.Add(invItem);
                
            }
            DisplayItems(currentTen * 10);
            ChangeSelected(tempSelected);
            UpdateSelectedItemInfo();
            loaded = true;
        }

        void btnFind_Click(object sender, MouseButtonEventArgs e) {
            if (SortedItems == null) {
                if (txtFind.Text.Trim() != "") {
                    SortedItems = new List<int>();

                    for (int i = 0; i < BankItems.Count; i++) {
                        if (BankItems[i].Num > 0 && Items.ItemHelper.Items[BankItems[i].Num].Name.ToLower().Contains(txtFind.Text.ToLower())) {
                            SortedItems.Add(i);
                        }
                    }

                    btnFind.Text = "Cancel";
                }
            } else {
                SortedItems = null;
                btnFind.Text = "Find";
            }
            currentTen = 0;
            DisplayItems(currentTen * 10);
            ChangeSelected(0);
            UpdateSelectedItemInfo();
        }

        void bankItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            if (loaded)
            {
                if (SortedItems == null) {
                    if (BankItems[currentTen * 10 + Array.IndexOf(lblVisibleItems, sender)].Num > 0) {
                        ChangeSelected(Array.IndexOf(lblVisibleItems, sender));

                        mnuBankItemSelected selectedMenu = (mnuBankItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBankItemSelected");
                        if (selectedMenu != null) {
                            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                            //selectedMenu.ItemSlot = GetSelectedItemSlot();
                            //selectedMenu.ItemNum = BankItems[GetSelectedItemSlot()].Num;
                        }
                        Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", BankItems[GetSelectedItemSlot()].Num, BankItems[GetSelectedItemSlot()].Value, GetSelectedItemSlot(), Enums.InvMenuType.Take));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");

                        UpdateSelectedItemInfo();
                    }
                } else {
                    if (currentTen * 10 + Array.IndexOf(lblVisibleItems, sender) < SortedItems.Count && BankItems[SortedItems[currentTen * 10 + Array.IndexOf(lblVisibleItems, sender)]].Num > 0) {
                        ChangeSelected(Array.IndexOf(lblVisibleItems, sender));

                        mnuBankItemSelected selectedMenu = (mnuBankItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBankItemSelected");
                        if (selectedMenu != null) {
                            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                            //selectedMenu.ItemSlot = GetSelectedItemSlot();
                            //selectedMenu.ItemNum = BankItems[GetSelectedItemSlot()].Num;
                        }
                        Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", BankItems[SortedItems[GetSelectedItemSlot()]].Num, BankItems[SortedItems[GetSelectedItemSlot()]].Value, SortedItems[GetSelectedItemSlot()], Enums.InvMenuType.Take));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");

                        UpdateSelectedItemInfo();
                    }
                }
            }
        }

        public void DisplayItems(int startNum)
        {
            this.BeginUpdate();
            for (int i = 0; i < lblVisibleItems.Length; i++)
            {
                
                //withdraw menu; lists bank items
                if (SortedItems == null) {
                    if (BankItems[startNum + i].Num > 0) {
                        string itemName = Items.ItemHelper.Items[BankItems[startNum + i].Num].Name;
                        if (!string.IsNullOrEmpty(itemName)) {
                            int itemAmount = BankItems[startNum + i].Value;
                            if (itemAmount > 0) {
                                itemName += " (" + itemAmount + ")";
                            }
                            lblVisibleItems[i].Text = itemName;
                        } else {
                            lblVisibleItems[i].Text = "";
                        }

                    } else {
                        lblVisibleItems[i].Text = "-----";
                    }
                } else {
                    if (startNum + i < SortedItems.Count && BankItems[SortedItems[startNum + i]].Num > 0) {
                        string itemName = Items.ItemHelper.Items[BankItems[SortedItems[startNum + i]].Num].Name;
                        if (!string.IsNullOrEmpty(itemName)) {
                            int itemAmount = BankItems[SortedItems[startNum + i]].Value;
                            if (itemAmount > 0) {
                                itemName += " (" + itemAmount + ")";
                            }
                            lblVisibleItems[i].Text = itemName;
                        } else {
                            lblVisibleItems[i].Text = "";
                        }

                    } else {
                        lblVisibleItems[i].Text = "-----";
                    }
                }
                
            }
            this.EndUpdate();
        }

        public void UpdateVisibleItems()
        {//appears to be unused
            DisplayItems(currentTen * 10);
        }

        public void ChangeSelected(int itemNum)
        {
            itemPicker.Location = new Point(18, 89 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        private int GetSelectedItemSlot()
        {
            return itemPicker.SelectedItem + currentTen * 10;
        }

        private void UpdateSelectedItemInfo()
        {
            //withdraw; shows bank item
            if (SortedItems == null) {
                if (BankItems[GetSelectedItemSlot()].Num > 0) {
                    picPreview.Visible = true;
                    picPreview.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[BankItems[GetSelectedItemSlot()].Num].Pic - (int)(Items.ItemHelper.Items[BankItems[GetSelectedItemSlot()].Num].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[BankItems[GetSelectedItemSlot()].Num].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                } else {
                    picPreview.Visible = false;
                }
            } else {
                if (GetSelectedItemSlot() < SortedItems.Count && BankItems[SortedItems[GetSelectedItemSlot()]].Num > 0) {
                    picPreview.Visible = true;
                    picPreview.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[BankItems[SortedItems[GetSelectedItemSlot()]].Num].Pic - (int)(Items.ItemHelper.Items[BankItems[SortedItems[GetSelectedItemSlot()]].Num].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[BankItems[SortedItems[GetSelectedItemSlot()]].Num].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                } else {
                    picPreview.Visible = false;
                }
            }
                
            
            lblItemNum.Text = (currentTen + 1) + "/" + ((BankItems.Count - 1) / 10 + 1);
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (loaded)
            {
                base.OnKeyboardDown(e);
                switch (e.Key)
                {
                    case SdlDotNet.Input.Key.DownArrow:
                        {
                            if (itemPicker.SelectedItem >= 9)
                            {
                                ChangeSelected(0);
                                //DisplayItems(1);
                            }
                            else
                            {

                                ChangeSelected(itemPicker.SelectedItem + 1);
                            }
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.UpArrow:
                        {
                            if (itemPicker.SelectedItem <= 0)
                            {
                                ChangeSelected(9);
                            }
                            else
                            {
                                ChangeSelected(itemPicker.SelectedItem - 1);
                            }
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.LeftArrow:
                        {
                            //int itemSlot = (currentTen + 1) - 10;//System.Math.Max(1, GetSelectedItemSlot() - (11 - itemPicker.SelectedItem));
                            if (currentTen <= 0)
                            {
                                currentTen = ((BankItems.Count - 1) / 10);
                            }
                            else
                            {
                                currentTen--;
                            }
                            DisplayItems(currentTen * 10);
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.RightArrow:
                        {
                            //int itemSlot = currentTen + 1 + 10;
                            if (currentTen >= ((BankItems.Count - 1) / 10))
                            {
                                currentTen = 0;
                            }
                            else
                            {
                                currentTen++;
                            }
                            DisplayItems(currentTen * 10);
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.Return:
                        {
                            if (SortedItems == null) {
                                if (BankItems[GetSelectedItemSlot()].Num > 0) {
                                    Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", BankItems[GetSelectedItemSlot()].Num, BankItems[GetSelectedItemSlot()].Value, GetSelectedItemSlot(), Enums.InvMenuType.Take));
                                    Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");
                                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                                }
                            } else {
                                if (GetSelectedItemSlot() < SortedItems.Count && BankItems[SortedItems[GetSelectedItemSlot()]].Num > 0) {
                                    Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", BankItems[SortedItems[GetSelectedItemSlot()]].Num, BankItems[SortedItems[GetSelectedItemSlot()]].Value, SortedItems[GetSelectedItemSlot()], Enums.InvMenuType.Take));
                                    Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");
                                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                                }
                            }
                        }
                        break;
                    case SdlDotNet.Input.Key.Backspace:
                        {
                            if (!txtFind.Focused) {
                                Menus.MenuSwitcher.OpenBankOptions();
                                Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                            }
                        }
                        break;
                }
            }
        }

        

    }
}
