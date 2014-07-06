using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuInventory : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        Enums.InvMenuType mode;
        Label[] lblVisibleItems;
        Label lblInventory;
        Widgets.MenuItemPicker itemPicker;
        PictureBox picPreview;
        public int currentTen;
        Label lblItemNum;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }



        public mnuInventory(string name, Enums.InvMenuType menuType, int itemSelected)
            : base(name) {
            base.Size = new Size(315, 360);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblInventory = new Label("lblInventory");
            lblInventory.AutoSize = true;
            lblInventory.Font = FontManager.LoadFont("PMU", 48);
            lblInventory.Text = "Inventory";
            lblInventory.ForeColor = Color.WhiteSmoke;
            lblInventory.Location = new Point(20, 0);

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
            lblItemNum.ForeColor = Color.WhiteSmoke;
            lblItemNum.Text = "0/" + ((MaxInfo.MaxInv - 1) / 10 + 1);

            lblVisibleItems = new Label[10];
            for (int i = 0; i < lblVisibleItems.Length; i++) {
                lblVisibleItems[i] = new Label("lblVisibleItems" + i);
                //lblVisibleItems[i].AutoSize = true;
                lblVisibleItems[i].Size = new Size(200, 32);
                lblVisibleItems[i].Font = FontManager.LoadFont("PMU", 32);
                lblVisibleItems[i].Location = new Point(35, (i * 30) + 48);
                //lblVisibleItems[i].HoverColor = Color.Red;
                lblVisibleItems[i].ForeColor = Color.WhiteSmoke;
                lblVisibleItems[i].AllowDrop = true;
                lblVisibleItems[i].DragDrop += new EventHandler<DragEventArgs>(lblVisibleItems_DragDrop);
                lblVisibleItems[i].MouseDown += new EventHandler<MouseButtonEventArgs>(lblVisibleItems_MouseDown);
                lblVisibleItems[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(inventoryItem_Click);
                this.AddWidget(lblVisibleItems[i]);
            }

            this.AddWidget(picPreview);
            this.AddWidget(itemPicker);
            this.AddWidget(lblInventory);
            this.AddWidget(lblItemNum);

            mode = menuType;
            currentTen = (itemSelected - 1) / 10;
            DisplayItems(currentTen * 10 + 1);
            ChangeSelected((itemSelected - 1) % 10);
            UpdateSelectedItemInfo();
        }

        void lblVisibleItems_DragDrop(object sender, DragEventArgs e) {
            if (mode == Enums.InvMenuType.Use) {
                int oldSlot = Convert.ToInt32(e.Data.GetData(typeof(int)));
                Network.Messenger.SendSwapInventoryItems(oldSlot, Array.IndexOf(lblVisibleItems, sender) + 1 + (currentTen * 10));
            }
        }

        void lblVisibleItems_MouseDown(object sender, MouseButtonEventArgs e) {
            if (mode == Enums.InvMenuType.Use) {
                if (Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuItemSelected") == null) {
                    Label label = (Label)sender;
                    SdlDotNet.Graphics.Surface dragSurf = new SdlDotNet.Graphics.Surface(label.Buffer.Size);
                    dragSurf.Fill(Color.Black);
                    dragSurf.Blit(label.Buffer, new Point(0, 0));
                    label.DoDragDrop(Array.IndexOf(lblVisibleItems, sender) + 1 + (currentTen * 10), DragDropEffects.Copy, dragSurf);
                }
            }
        }

        void inventoryItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (Players.PlayerManager.MyPlayer.GetInvItemNum(currentTen * 10 + 1 + Array.IndexOf(lblVisibleItems, sender)) > 0) {
                ChangeSelected(Array.IndexOf(lblVisibleItems, sender));


                if (mode == Enums.InvMenuType.Store)
                {
                    mnuBankItemSelected selectedMenu = (mnuBankItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBankItemSelected");
                    if (selectedMenu != null) {
                        Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                        //selectedMenu.ItemSlot = GetSelectedItemSlot();
                        //selectedMenu.ItemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot());

                    }

                    Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()), Players.PlayerManager.MyPlayer.Inventory[GetSelectedItemSlot()].Value, GetSelectedItemSlot(), Enums.InvMenuType.Store));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");



                    
                }
                else if (mode == Enums.InvMenuType.Use)
                {

                    // Don't select the item, interferes with drag & drop
                    //mnuItemSelected selectedMenu = (mnuItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuItemSelected");
                    //if (selectedMenu != null) {
                    //    Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                    //    //selectedMenu.ItemSlot = GetSelectedItemSlot();
                    //    //ChangeSelected(Array.IndexOf(lblVisibleItems, sender));
                    //}
                    //Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuItemSelected("mnuItemSelected", currentTen * 10 + 1 + Array.IndexOf(lblVisibleItems, sender)));
                    //Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuItemSelected");
                    ////ChangeSelected(Array.IndexOf(lblVisibleItems, sender));

                } else if (mode == Enums.InvMenuType.Sell) {
                		mnuShopItemSelected selectedMenu = (mnuShopItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShopItemSelected");
                    if (selectedMenu != null)
                    {
                        Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                        //selectedMenu.ItemSlot = GetSelectedItemSlot();
                        //ChangeSelected(Array.IndexOf(lblVisibleItems, sender));
                    }
                        Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopItemSelected("mnuShopItemSelected", Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()), GetSelectedItemSlot(), Enums.InvMenuType.Sell));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopItemSelected");
                        //ChangeSelected(Array.IndexOf(lblVisibleItems, sender));
                    
                	
                }
                UpdateSelectedItemInfo();
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        public void DisplayItems(int startNum) {
            //System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
            //watch.Start();
            this.BeginUpdate();
            for (int i = 0; i < lblVisibleItems.Length; i++) {
                if (Players.PlayerManager.MyPlayer.GetInvItemNum(startNum + i) > 0) {
                    if (Players.PlayerManager.MyPlayer.IsEquiped(startNum + i)) { // Check if the item is equiped
                        // If it is equiped, set the labels forecolor to yellow, if it isn't yellow already
                        if (lblVisibleItems[i].ForeColor != Color.Yellow) {
                            lblVisibleItems[i].ForeColor = Color.Yellow;
                        }
                    } else {
                        // If it isn't equiped, set the labels' forecolor to black, if it isn't black already
                        if (lblVisibleItems[i].ForeColor != Color.WhiteSmoke) {
                            lblVisibleItems[i].ForeColor = Color.WhiteSmoke;
                        }
                    }
                    
                    string itemName = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(startNum + i)].Name;
                    if (!string.IsNullOrEmpty(itemName)) {
                        int itemAmount = Players.PlayerManager.MyPlayer.GetInvItemAmount(startNum + i);
                        if (itemAmount > 0) {
                            itemName += " (" + itemAmount + ")";
                        }
                        if (Players.PlayerManager.MyPlayer.GetInvItemSticky(startNum + i)) {
                            itemName = "x" + itemName;
                        }
                        lblVisibleItems[i].Text = itemName;
                    } else {
                        lblVisibleItems[i].Text = "";
                    }
                } else {
                    if (lblVisibleItems[i].ForeColor != Color.WhiteSmoke) {
                        lblVisibleItems[i].ForeColor = Color.WhiteSmoke;
                    }
                    lblVisibleItems[i].Text = "----";
                }
            }
            this.EndUpdate();
            //watch.Stop();
            //Console.WriteLine(watch.Elapsed.ToString());
        }

        public void UpdateVisibleItem(int itemNum) {//appears to be unused
        	this.BeginUpdate();
        	if (itemNum/10 != currentTen) return;
        	
        	int itemIndex = itemNum - currentTen*10 - 1;
        	if (Players.PlayerManager.MyPlayer.GetInvItemNum(itemNum) > 0) {
                    if (Players.PlayerManager.MyPlayer.IsEquiped(itemNum)) { // Check if the item is equiped
                        // If it is equiped, set the labels forecolor to yellow, if it isn't yellow already
                        if (lblVisibleItems[itemIndex].ForeColor != Color.Yellow) {
                            lblVisibleItems[itemIndex].ForeColor = Color.Yellow;
                        }
                    } else {
                        // If it isn't equiped, set the labels' forecolor to black, if it isn't black already
                        if (lblVisibleItems[itemIndex].ForeColor != Color.WhiteSmoke) {
                            lblVisibleItems[itemIndex].ForeColor = Color.WhiteSmoke;
                        }
                    }
                    
                    string itemName = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(itemNum)].Name;
                    if (!string.IsNullOrEmpty(itemName)) {
                        int itemAmount = Players.PlayerManager.MyPlayer.GetInvItemAmount(itemNum);
                        if (itemAmount > 0) {
                            itemName += " (" + itemAmount + ")";
                        }
                        if (Players.PlayerManager.MyPlayer.GetInvItemSticky(itemNum)) {
                            itemName = "x" + itemName;
                        }
                        lblVisibleItems[itemIndex].Text = itemName;
                    } else {
                        lblVisibleItems[itemIndex].Text = "";
                    }
                } else {
                    if (lblVisibleItems[itemIndex].ForeColor != Color.WhiteSmoke) {
                        lblVisibleItems[itemIndex].ForeColor = Color.WhiteSmoke;
                    }
                    lblVisibleItems[itemIndex].Text = "----";
                }
        	this.EndUpdate();
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 63 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        private int GetSelectedItemSlot() {
            return itemPicker.SelectedItem + currentTen * 10 + 1;
        }

        public void UpdateSelectedItemInfo() {
            if (Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()) > 0) {
                picPreview.Visible = true;
                picPreview.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot())].Pic - (int)(Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot())].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot())].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
            } else {
                picPreview.Visible = false;
            }
            lblItemNum.Text = (currentTen + 1) + "/" + ((MaxInfo.MaxInv - 1) / 10 + 1);
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem >= 9) {
                            ChangeSelected(0);
                            //DisplayItems(1);
                        } else {

                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        UpdateSelectedItemInfo();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem <= 0) {
                            ChangeSelected(9);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        UpdateSelectedItemInfo();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.LeftArrow: {
                        //int itemSlot = (currentTen + 1) - 10;//System.Math.Max(1, GetSelectedItemSlot() - (11 - itemPicker.SelectedItem));
                        if (currentTen <= 0) {
                            currentTen = ((MaxInfo.MaxInv - 1) / 10);
                        } else {
                            currentTen--;
                        }
                        DisplayItems(currentTen * 10 + 1);
                        UpdateSelectedItemInfo();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow: {
                        //int itemSlot = currentTen + 1 + 10;
                        if (currentTen >= ((MaxInfo.MaxInv - 1) / 10)) {
                            currentTen = 0;
                        } else {
                            currentTen++;
                        }
                        DisplayItems(currentTen * 10 + 1);
                        UpdateSelectedItemInfo();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {

                        if (Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()) > 0)
                        {
                            if (mode == Enums.InvMenuType.Store)
                            {
                                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankItemSelected("mnuBankItemSelected", Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()), Players.PlayerManager.MyPlayer.Inventory[GetSelectedItemSlot()].Value, GetSelectedItemSlot(), Enums.InvMenuType.Store));
                                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankItemSelected");

                            }
                            else if (mode == Enums.InvMenuType.Use)
                            {

                                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuItemSelected("mnuItemSelected", GetSelectedItemSlot()));
                                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuItemSelected");
                            } else if (mode == Enums.InvMenuType.Sell)
                            {
                                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopItemSelected("mnuShopItemSelected", Players.PlayerManager.MyPlayer.GetInvItemNum(GetSelectedItemSlot()), GetSelectedItemSlot(), Enums.InvMenuType.Sell));
                                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopItemSelected");
                            }
                            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {

                        if (mode == Enums.InvMenuType.Store)
                        {

                            Menus.MenuSwitcher.OpenBankOptions();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                        } else if (mode == Enums.InvMenuType.Use){
                            Menus.MenuSwitcher.ShowMainMenu();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                        }else if (mode == Enums.InvMenuType.Sell){
                        	
                        	Menus.MenuSwitcher.OpenShopOptions();
                        	Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                        }
                    }
                    break;
            }
        }



    }
}
