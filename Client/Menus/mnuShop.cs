using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using PMU.Core;
using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    /// <summary>
    /// Description of mnuShop.
    /// </summary>
    class mnuShop : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        bool loaded;
        int tempSelected;
        Label[] lblVisibleItems;
        Label[] lblVisiblePrices;
        Label lblItemCollection;
        Widgets.MenuItemPicker itemPicker;
        PictureBox picPreview;
        public int currentTen;
        Label lblItemNum;
        public List<Shops.ShopItem> ShopItems {

            get;
            set;
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }



        public mnuShop(string name, int itemSelected)
            : base(name) {
            base.Size = new Size(421, 360);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            currentTen = itemSelected / 10;



            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblItemCollection = new Label("lblItemCollection");
            lblItemCollection.AutoSize = true;
            lblItemCollection.Font = FontManager.LoadFont("PMU", 48);
            lblItemCollection.Text = "Shop";
            lblItemCollection.Location = new Point(20, 0);
            lblItemCollection.ForeColor = Color.WhiteSmoke;

            picPreview = new PictureBox("picPreview");
            picPreview.Size = new Size(32, 32);
            picPreview.BackColor = Color.Transparent;
            picPreview.Location = new Point(361, 20);

            lblItemNum = new Label("lblItemNum");
            //lblItemNum.Size = new Size(100, 30);
            lblItemNum.AutoSize = true;
            lblItemNum.Location = new Point(288, 15);
            lblItemNum.Font = FontManager.LoadFont("PMU", 32);
            lblItemNum.BackColor = Color.Transparent;
            lblItemNum.Text = "0/0";
            lblItemNum.ForeColor = Color.WhiteSmoke;


            lblVisibleItems = new Label[10];
            lblVisiblePrices = new Label[10];
            for (int i = 0; i < lblVisibleItems.Length; i++) {
                lblVisibleItems[i] = new Label("lblVisibleItems" + i);
                //lblVisibleItems[i].AutoSize = true;
                lblVisibleItems[i].Size = new Size(200, 32);
                //lblVisibleItems[i].Width = 200;
                lblVisibleItems[i].Font = FontManager.LoadFont("PMU", 32);
                lblVisibleItems[i].Location = new Point(35, (i * 30) + 48);
                //lblVisibleItems[i].HoverColor = Color.Red;
                lblVisibleItems[i].ForeColor = Color.WhiteSmoke;
                lblVisibleItems[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(shopItem_Click);
                this.AddWidget(lblVisibleItems[i]);

                lblVisiblePrices[i] = new Label("lblVisiblePrices" + i);
                //lblVisiblePrices[i].AutoSize = true;
                lblVisiblePrices[i].Size = new Size(200, 32);
                //lblVisiblePrices[i].Width = 200;
                lblVisiblePrices[i].Font = FontManager.LoadFont("PMU", 32);
                lblVisiblePrices[i].Location = new Point(240, (i * 30) + 48);
                //lblVisiblePrices[i].HoverColor = Color.Red;
                lblVisiblePrices[i].ForeColor = Color.WhiteSmoke;
                lblVisiblePrices[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(shopPrice_Click);
                this.AddWidget(lblVisiblePrices[i]);
            }

            this.AddWidget(picPreview);
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

        public void LoadShopItems(string[] parse) {
            ShopItems = new List<Shops.ShopItem>();

            if (parse[3].ToInt() <= 0) {
                lblVisibleItems[0].Text = "Nothing";
                return;
            }


            int amount = parse[3].ToInt();
            int n = 4;

            for (int i = 0; i < amount; i++) {
                Shops.ShopItem shopItem = new Shops.ShopItem();
                shopItem.GiveItem = parse[n].ToInt();
                shopItem.GiveValue = parse[n + 1].ToInt();
                shopItem.GetItem = parse[n + 2].ToInt();

                ShopItems.Add(shopItem);

                n += 3;
            }

            //int length = parse.Length/3 - 3;
            //for (int i = 0; i <= length; i++)
            //{
            //    Shops.ShopItem shopItem = new Shops.ShopItem();

            //    shopItem.GiveItem = parse[i*3 + 3].ToInt();
            //    shopItem.GiveValue = parse[i*3 + 4].ToInt();
            //    shopItem.GetItem = parse[i*3 + 5].ToInt();


            //    ShopItems.Add(shopItem);

            //}
            DisplayItems(currentTen * 10);
            ChangeSelected(tempSelected);
            UpdateSelectedItemInfo();
            loaded = true;
        }

        void shopPrice_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (loaded) {
                if (ShopItems[currentTen * 10 + Array.IndexOf(lblVisiblePrices, sender)].GetItem > 0) {
                    ChangeSelected(Array.IndexOf(lblVisiblePrices, sender));

                    mnuShopItemSelected selectedMenu = (mnuShopItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShopItemSelected");
                    if (selectedMenu != null) {
                        Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                        //selectedMenu.ItemSlot = GetSelectedItemSlot();
                        //selectedMenu.ItemNum = BankItems[GetSelectedItemSlot()].Num;
                    }
                    Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopItemSelected("mnuShopItemSelected", ShopItems[GetSelectedItemSlot()].GetItem, GetSelectedItemSlot(), Enums.InvMenuType.Buy));
                    Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopItemSelected");

                    UpdateSelectedItemInfo();
                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                }
            }

        }

        void shopItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (loaded) {
                if (ShopItems[currentTen * 10 + Array.IndexOf(lblVisibleItems, sender)].GetItem > 0) {
                    ChangeSelected(Array.IndexOf(lblVisibleItems, sender));

                    mnuShopItemSelected selectedMenu = (mnuShopItemSelected)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShopItemSelected");
                    if (selectedMenu != null) {
                        Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(selectedMenu);
                        //selectedMenu.ItemSlot = GetSelectedItemSlot();
                        //selectedMenu.ItemNum = BankItems[GetSelectedItemSlot()].Num;
                    }
                    Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopItemSelected("mnuShopItemSelected", ShopItems[GetSelectedItemSlot()].GetItem, GetSelectedItemSlot(), Enums.InvMenuType.Buy));
                    Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopItemSelected");

                    UpdateSelectedItemInfo();
                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                }
            }
        }

        private delegate void DisplayItemsDelegate(int startNum);
        public void DisplayItems(int startNum) {
            //if (!InvokeRequired) {
            //    Invoke(new DisplayItemsDelegate(DisplayItems), startNum);
            //} else { 
                for (int i = 0; i < lblVisibleItems.Length; i++) {

                    //shop menu; lists items and their prices
                    if (startNum + i >= ShopItems.Count) {
                        lblVisibleItems[i].Text = "";
                        lblVisiblePrices[i].Text = "";
                    } else if (ShopItems[startNum + i].GetItem > 0) {
                        string getItem = Items.ItemHelper.Items[ShopItems[startNum + i].GetItem].Name;

                        string giveItem = Items.ItemHelper.Items[ShopItems[startNum + i].GiveItem].Name + "x" + ShopItems[startNum + i].GiveValue.ToString();
                        if (!string.IsNullOrEmpty(getItem)) {

                            lblVisibleItems[i].Text = getItem;
                            lblVisiblePrices[i].Text = giveItem;
                        } else {
                            lblVisibleItems[i].Text = "----";
                            lblVisiblePrices[i].Text = "";
                        }

                    } else {
                        lblVisibleItems[i].Text = "----";
                        lblVisiblePrices[i].Text = "";
                    }

                }
            //}
                RequestRedraw();
        }

        public void UpdateVisibleItems() {//appears to be unused
            DisplayItems(currentTen * 10);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 63 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;

        }

        private int GetSelectedItemSlot() {
            return itemPicker.SelectedItem + currentTen * 10;
        }

        private void UpdateSelectedItemInfo() {
            //withdraw; shows bank item
            if (ShopItems[GetSelectedItemSlot()].GetItem > 0) {
                picPreview.Visible = true;
                picPreview.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[ShopItems[GetSelectedItemSlot()].GetItem].Pic - (int)(Items.ItemHelper.Items[ShopItems[GetSelectedItemSlot()].GetItem].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[ShopItems[GetSelectedItemSlot()].GetItem].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
            } else {
                picPreview.Visible = false;
            }


            lblItemNum.Text = (currentTen + 1) + "/" + ((ShopItems.Count - 1) / 10 + 1);
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            if (ShopItems != null && ShopItems.Count == 0 && e.Key == SdlDotNet.Input.Key.Backspace) {
                Menus.MenuSwitcher.OpenShopOptions();
                Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
            }
            if (loaded) {
                base.OnKeyboardDown(e);
                switch (e.Key) {
                    case SdlDotNet.Input.Key.DownArrow: {
                            if (itemPicker.SelectedItem >= 9 || currentTen * 10 + itemPicker.SelectedItem >= ShopItems.Count - 1) {
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
                            if (currentTen * 10 + itemPicker.SelectedItem > ShopItems.Count) {
                                ChangeSelected(ShopItems.Count - currentTen * 10 - 1);
                            }
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.LeftArrow: {
                            //int itemSlot = (currentTen + 1) - 10;//System.Math.Max(1, GetSelectedItemSlot() - (11 - itemPicker.SelectedItem));
                            if (currentTen <= 0) {
                                currentTen = ((ShopItems.Count - 1) / 10);
                            } else {
                                currentTen--;
                            }
                            if (currentTen * 10 + itemPicker.SelectedItem >= ShopItems.Count) {
                                ChangeSelected(ShopItems.Count - currentTen * 10 - 1);
                            }
                            DisplayItems(currentTen * 10);
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.RightArrow: {
                            //int itemSlot = currentTen + 1 + 10;
                            if (currentTen >= ((ShopItems.Count - 1) / 10)) {
                                currentTen = 0;
                            } else {
                                currentTen++;
                            }
                            if (currentTen * 10 + itemPicker.SelectedItem >= ShopItems.Count) {
                                ChangeSelected(ShopItems.Count - currentTen * 10 - 1);
                            }
                            DisplayItems(currentTen * 10);
                            UpdateSelectedItemInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                        break;
                    case SdlDotNet.Input.Key.Return: {
                            if (ShopItems[GetSelectedItemSlot()].GetItem > 0) {
                                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopItemSelected("mnuShopItemSelected", ShopItems[GetSelectedItemSlot()].GetItem, GetSelectedItemSlot(), Enums.InvMenuType.Buy));
                                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopItemSelected");
                                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                            }
                        }
                        break;
                    case SdlDotNet.Input.Key.Backspace: {
                            Menus.MenuSwitcher.OpenShopOptions();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                        }
                        break;
                }
            }
        }
    }
}
