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
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;
using Client.Logic.Network;
using PMU.Sockets;
using PMU.Core;

namespace Client.Logic.Windows.Editors
{
    /// <summary>
    /// Description of winShopPanel.
    /// </summary>
    class winShopPanel : Core.WindowCore
    {

        int shopNum = -1;
        int currentTen = 0;

        Panel pnlShopList;
        Panel pnlShopEditor;

        ListBox lbxShopList;
        ListBoxTextItem lbiItem;
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        Label lblName;
        TextBox txtName;
        Label lblJoinSay;
        TextBox txtJoinSay;
        Label lblLeaveSay;
        TextBox txtLeaveSay;
        ListBox lbxShopItems;
        ListBoxTextItem lbiShopItem;
        Label lblGiveItem;
        NumericUpDown nudGiveItem;
        Label lblGiveAmount;
        NumericUpDown nudGiveAmount;
        Label lblGetItem;
        NumericUpDown nudGetItem;
        Button btnChange;
        Button btnShiftUp;
        Button btnShiftDown;

        ListPair<int, Shops.ShopItem> shopItemList;

        public winShopPanel()
            : base("winShopPanel") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Shop Panel";

            pnlShopList = new Panel("pnlShopList");
            pnlShopList.Size = new System.Drawing.Size(200, 230);
            pnlShopList.Location = new Point(0, 0);
            pnlShopList.BackColor = Color.White;
            pnlShopList.Visible = true;

            pnlShopEditor = new Panel("pnlShopEditor");
            pnlShopEditor.Size = new System.Drawing.Size(440, 380);
            pnlShopEditor.Location = new Point(0, 0);
            pnlShopEditor.BackColor = Color.White;
            pnlShopEditor.Visible = false;


            lbxShopList = new ListBox("lbxShopList");
            lbxShopList.Location = new Point(10, 10);
            lbxShopList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Shops.ShopHelper.Shops[(i + 1) + 10 * currentTen].Name);
                lbxShopList.Items.Add(lbiItem);
            }
            lbxShopList.SelectItem(0);

            btnBack = new Button("btnBack");
            btnBack.Location = new Point(10, 160);
            btnBack.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnBack.Size = new System.Drawing.Size(64, 16);
            btnBack.Visible = true;
            btnBack.Text = "<--";
            btnBack.Click += new EventHandler<MouseButtonEventArgs>(btnBack_Click);

            btnForward = new Button("btnForward");
            btnForward.Location = new Point(126, 160);
            btnForward.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnForward.Size = new System.Drawing.Size(64, 16);
            btnForward.Visible = true;
            btnForward.Text = "-->";
            btnForward.Click += new EventHandler<MouseButtonEventArgs>(btnForward_Click);


            btnEdit = new Button("btnEdit");
            btnEdit.Location = new Point(10, 190);
            btnEdit.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEdit.Size = new System.Drawing.Size(64, 16);
            btnEdit.Visible = true;
            btnEdit.Text = "Edit";
            btnEdit.Click += new EventHandler<MouseButtonEventArgs>(btnEdit_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Location = new Point(126, 190);
            btnCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnCancel.Size = new System.Drawing.Size(64, 16);
            btnCancel.Visible = true;
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);


            btnEditorCancel = new Button("btnEditorCancel");
            btnEditorCancel.Location = new Point(340, 334);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(250, 334);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);



            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Shop Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(420, 16);
            txtName.Location = new Point(10, 16);
            //txtName.Text = "Loading...";

            lblJoinSay = new Label("lblJoinSay");
            lblJoinSay.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblJoinSay.Text = "Join Say:";
            lblJoinSay.AutoSize = true;
            lblJoinSay.Location = new Point(10, 36);

            txtJoinSay = new TextBox("txtJoinSay");
            txtJoinSay.Size = new Size(420, 16);
            txtJoinSay.Location = new Point(10, 48);

            lblLeaveSay = new Label("lblLeaveSay");
            lblLeaveSay.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblLeaveSay.Text = "Leave Say:";
            lblLeaveSay.AutoSize = true;
            lblLeaveSay.Location = new Point(10, 68);

            txtLeaveSay = new TextBox("txtLeaveSay");
            txtLeaveSay.Size = new Size(420, 16);
            txtLeaveSay.Location = new Point(10, 80);

            lbxShopItems = new ListBox("lbxShopItems");
            lbxShopItems.Location = new Point(10, 100);
            lbxShopItems.Size = new Size(220, 240);
            lbxShopItems.ItemSelected += new EventHandler(lbxShopItems_ItemSelected);

            for (int i = 0; i < MaxInfo.MAX_TRADES; i++) {
                lbiShopItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": ---");

                lbxShopItems.Items.Add(lbiShopItem);
            }
            //ListBoxTextItem lbiShopItem;

            lblGiveItem = new Label("lblGiveItem");
            lblGiveItem.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblGiveItem.Text = "Item Paid [---]:";
            lblGiveItem.AutoSize = true;
            lblGiveItem.Location = new Point(240, 100);

            nudGiveItem = new NumericUpDown("nudGiveItem");
            nudGiveItem.Size = new Size(100, 16);
            nudGiveItem.Location = new Point(240, 112);
            nudGiveItem.Maximum = MaxInfo.MaxItems;
            nudGiveItem.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudGiveItem_ValueChanged);

            lblGiveAmount = new Label("lblGiveAmount");
            lblGiveAmount.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblGiveAmount.Text = "Amount Paid: (integer)";
            lblGiveAmount.AutoSize = true;
            lblGiveAmount.Location = new Point(240, 132);

            nudGiveAmount = new NumericUpDown("nudGiveAmount");
            nudGiveAmount.Size = new Size(100, 16);
            nudGiveAmount.Maximum = 2147483647;
            nudGiveAmount.Location = new Point(240, 144);


            lblGetItem = new Label("lblGetItem");
            lblGetItem.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblGetItem.Text = "Item Bought [---]:";
            lblGetItem.AutoSize = true;
            lblGetItem.Location = new Point(240, 164);

            nudGetItem = new NumericUpDown("nudGetItem");
            nudGetItem.Size = new Size(100, 16);
            nudGetItem.Location = new Point(240, 176);
            nudGetItem.Maximum = MaxInfo.MaxItems;
            nudGetItem.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudGetItem_ValueChanged);


            btnShiftUp = new Button("btnShiftUp");
            btnShiftUp.Location = new Point(240, 206);
            btnShiftUp.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnShiftUp.Size = new System.Drawing.Size(64, 16);
            btnShiftUp.Visible = true;
            btnShiftUp.Text = "Shift Up";
            btnShiftUp.Click += new EventHandler<MouseButtonEventArgs>(btnShiftUp_Click);

            btnChange = new Button("btnChange");
            btnChange.Location = new Point(240, 226);
            btnChange.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnChange.Size = new System.Drawing.Size(64, 16);
            btnChange.Visible = true;
            btnChange.Text = "Change";
            btnChange.Click += new EventHandler<MouseButtonEventArgs>(btnChange_Click);

            btnShiftDown = new Button("btnShiftDown");
            btnShiftDown.Location = new Point(240, 246);
            btnShiftDown.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnShiftDown.Size = new System.Drawing.Size(64, 16);
            btnShiftDown.Visible = true;
            btnShiftDown.Text = "Shift Down";
            btnShiftDown.Click += new EventHandler<MouseButtonEventArgs>(btnShiftDown_Click);




            pnlShopList.AddWidget(lbxShopList);
            pnlShopList.AddWidget(btnBack);
            pnlShopList.AddWidget(btnForward);
            pnlShopList.AddWidget(btnEdit);
            pnlShopList.AddWidget(btnCancel);

            pnlShopEditor.AddWidget(lblName);
            pnlShopEditor.AddWidget(txtName);
            pnlShopEditor.AddWidget(lblJoinSay);
            pnlShopEditor.AddWidget(txtJoinSay);
            pnlShopEditor.AddWidget(lblLeaveSay);
            pnlShopEditor.AddWidget(txtLeaveSay);
            pnlShopEditor.AddWidget(lbxShopItems);
            //pnlShopEditor.AddWidget(lbiShopItem);
            pnlShopEditor.AddWidget(lblGiveItem);
            pnlShopEditor.AddWidget(nudGiveItem);
            pnlShopEditor.AddWidget(lblGiveAmount);
            pnlShopEditor.AddWidget(nudGiveAmount);
            pnlShopEditor.AddWidget(lblGetItem);
            pnlShopEditor.AddWidget(nudGetItem);
            pnlShopEditor.AddWidget(btnChange);
            pnlShopEditor.AddWidget(btnShiftUp);
            pnlShopEditor.AddWidget(btnShiftDown);



            pnlShopEditor.AddWidget(btnEditorCancel);
            pnlShopEditor.AddWidget(btnEditorOK);



            this.AddWidget(pnlShopList);
            this.AddWidget(pnlShopEditor);
        }

        public void LoadShop(string[] parse) {

            pnlShopList.Visible = false;
            pnlShopEditor.Visible = true;
            this.Size = new System.Drawing.Size(pnlShopEditor.Width, pnlShopEditor.Height);


            txtName.Text = parse[2];
            txtJoinSay.Text = parse[3];
            txtLeaveSay.Text = parse[4];

            shopItemList = new ListPair<int, Client.Logic.Shops.ShopItem>();

            for (int i = 0; i < MaxInfo.MAX_TRADES; i++) {
                Shops.ShopItem shopItem = new Shops.ShopItem();
                shopItem.GiveItem = parse[5 + i * 3].ToInt();
                shopItem.GiveValue = parse[6 + i * 3].ToInt();
                shopItem.GetItem = parse[7 + i * 3].ToInt();

                shopItemList.Add(i, shopItem);
            }

            //if (lbxShopItems.SelectedItems.Count > 0) {

            //lbxShopItems.SelectItem(lbxShopItems.SelectedItems[0].TextIdentifier);

            //}


            RefreshShopItemList();
            lbxShopItems.SelectItem(0);

            btnEdit.Text = "Edit";
        }

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshShopList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxShops / 10)) {
                currentTen++;
            }
            RefreshShopList();
        }

        public void RefreshShopList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxShops) {
                    ((ListBoxTextItem)lbxShopList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Shops.ShopHelper.Shops[(i + 1) + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxShopList.Items[i]).Text = "---";
                }
            }
        }

        void RefreshShopItemList() {
            for (int i = 0; i < MaxInfo.MAX_TRADES; i++) {
                if (shopItemList[i].GiveItem > 0 && shopItemList[i].GiveValue > 0 && shopItemList[i].GetItem > 0) {
                    ((ListBoxTextItem)lbxShopItems.Items[i]).Text = ((i + 1) + ": " + shopItemList[i].GiveValue.ToString() + " " + Items.ItemHelper.Items[shopItemList[i].GiveItem].Name + " for " + Items.ItemHelper.Items[shopItemList[i].GetItem].Name);
                } else {
                    ((ListBoxTextItem)lbxShopItems.Items[i]).Text = (i + 1) + ": ---";
                }
            }
        }

        void RefreshTransactionData() {
            if (lbxShopItems.SelectedItems.Count == 1) { 
                string[] index = ((ListBoxTextItem)lbxShopItems.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    int itemNum = index[0].ToInt() - 1;
                    nudGiveItem.Value = shopItemList[itemNum].GiveItem;
                    nudGiveAmount.Value = shopItemList[itemNum].GiveValue;
                    nudGetItem.Value = shopItemList[itemNum].GetItem;
                }
            }
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string[] index = ((ListBoxTextItem)lbxShopList.SelectedItems[0]).Text.Split(':');
            if (index[0].IsNumeric()) {
                shopNum = index[0].ToInt();
                btnEdit.Text = "Loading...";
            }

            Messenger.SendEditShop(shopNum);
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
        }

        void lbxShopItems_ItemSelected(object sender, EventArgs e) {
            RefreshTransactionData();
        }

        void btnShiftUp_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {//change, then shift up
            bool validInput = true;

            if (nudGiveItem.Value < 0 || nudGiveItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGiveItem.ForeColor = Color.Red;
            } else {
                lblGiveItem.ForeColor = Color.Black;
            }

            if (nudGiveAmount.Value < 0) {
                validInput = false;
                lblGiveAmount.ForeColor = Color.Red;
            } else {
                lblGiveAmount.ForeColor = Color.Black;
            }

            if (nudGetItem.Value < 0 || nudGetItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGetItem.ForeColor = Color.Red;
            } else {
                lblGetItem.ForeColor = Color.Black;
            }

            if (validInput && lbxShopItems.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxShopItems.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    int itemNum = index[0].ToInt() - 1;
                    if (itemNum <= 0) {
                        shopItemList[itemNum].GiveItem = nudGiveItem.Value;
                        shopItemList[itemNum].GiveValue = nudGiveAmount.Value;
                        shopItemList[itemNum].GetItem = nudGetItem.Value;
                        //lbxShopItems.SelectItem(itemNum);
                        RefreshShopItemList();
                    } else {
                        shopItemList[itemNum].GiveItem = shopItemList[itemNum - 1].GiveItem;
                        shopItemList[itemNum].GiveValue = shopItemList[itemNum - 1].GiveValue;
                        shopItemList[itemNum].GetItem = shopItemList[itemNum - 1].GetItem;
                        shopItemList[itemNum - 1].GiveItem = nudGiveItem.Value;
                        shopItemList[itemNum - 1].GiveValue = nudGiveAmount.Value;
                        shopItemList[itemNum - 1].GetItem = nudGetItem.Value;

                        lbxShopItems.SelectItem(itemNum - 1);
                        RefreshShopItemList();
                    }
                }

            }
        }

        void btnChange_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            bool validInput = true;

            if (nudGiveItem.Value < 0 || nudGiveItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGiveItem.ForeColor = Color.Red;
            } else {
                lblGiveItem.ForeColor = Color.Black;
            }

            if (nudGiveAmount.Value < 0) {
                validInput = false;
                lblGiveAmount.ForeColor = Color.Red;
            } else {
                lblGiveAmount.ForeColor = Color.Black;
            }

            if (nudGetItem.Value < 0 || nudGetItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGetItem.ForeColor = Color.Red;
            } else {
                lblGetItem.ForeColor = Color.Black;
            }

            if (validInput && lbxShopItems.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxShopItems.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    int itemNum = index[0].ToInt() - 1;
                    shopItemList[itemNum].GiveItem = nudGiveItem.Value;
                    shopItemList[itemNum].GiveValue = nudGiveAmount.Value;
                    shopItemList[itemNum].GetItem = nudGetItem.Value;
                }


                RefreshShopItemList();
            }
        }

        void btnShiftDown_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {//change, then shift down
            bool validInput = true;

            if (nudGiveItem.Value < 0 || nudGiveItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGiveItem.ForeColor = Color.Red;
            } else {
                lblGiveItem.ForeColor = Color.Black;
            }

            if (nudGiveAmount.Value < 0) {
                validInput = false;
                lblGiveAmount.ForeColor = Color.Red;
            } else {
                lblGiveAmount.ForeColor = Color.Black;
            }

            if (nudGetItem.Value < 0 || nudGetItem.Value >= MaxInfo.MaxItems) {
                validInput = false;
                lblGetItem.ForeColor = Color.Red;
            } else {
                lblGetItem.ForeColor = Color.Black;
            }
            if (validInput && lbxShopItems.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxShopItems.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    int itemNum = index[0].ToInt() - 1;
                    if (itemNum >= MaxInfo.MAX_TRADES - 1) {
                        shopItemList[itemNum].GiveItem = nudGiveItem.Value;
                        shopItemList[itemNum].GiveValue = nudGiveAmount.Value;
                        shopItemList[itemNum].GetItem = nudGetItem.Value;
                        //lbxShopItems.SelectItem(itemNum);
                        RefreshShopItemList();
                    } else {
                        shopItemList[itemNum].GiveItem = shopItemList[itemNum + 1].GiveItem;
                        shopItemList[itemNum].GiveValue = shopItemList[itemNum + 1].GiveValue;
                        shopItemList[itemNum].GetItem = shopItemList[itemNum + 1].GetItem;
                        shopItemList[itemNum + 1].GiveItem = nudGiveItem.Value;
                        shopItemList[itemNum + 1].GiveValue = nudGiveAmount.Value;
                        shopItemList[itemNum + 1].GetItem = nudGetItem.Value;

                        lbxShopItems.SelectItem(itemNum + 1);
                        RefreshShopItemList();
                    }
                }

            }
        }

        void nudGiveItem_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            if (nudGiveItem.Value > 0 && nudGiveItem.Value < MaxInfo.MaxItems) {
                lblGiveItem.Text = "Item Paid (" + Items.ItemHelper.Items[nudGiveItem.Value].Name + "):";
            } else {
                lblGiveItem.Text = "Item Paid [---]:";
            }

        }



        void nudGetItem_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            if (nudGetItem.Value > 0 && nudGetItem.Value < MaxInfo.MaxItems) {
                lblGetItem.Text = "Item Bought (" + Items.ItemHelper.Items[nudGetItem.Value].Name + "):";
            } else {
                lblGetItem.Text = "Item Bought [---]:";
            }

        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            shopNum = -1;
            pnlShopEditor.Visible = false;
            pnlShopList.Visible = true;
            this.Size = new System.Drawing.Size(pnlShopList.Width, pnlShopList.Height);

        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Shops.Shop shopToSend = new Shops.Shop();

            shopToSend.Name = txtName.Text;
            shopToSend.JoinSay = txtJoinSay.Text;
            shopToSend.LeaveSay = txtLeaveSay.Text;

            for (int j = 0; j < MaxInfo.MAX_TRADES; j++) {
                shopToSend.Items[j].GiveItem = shopItemList[j].GiveItem;
                shopToSend.Items[j].GiveValue = shopItemList[j].GiveValue;
                shopToSend.Items[j].GetItem = shopItemList[j].GetItem;
            }


            Messenger.SendSaveShop(shopNum, shopToSend);

            pnlShopEditor.Visible = false;
            pnlShopList.Visible = true;
            this.Size = new System.Drawing.Size(pnlShopList.Width, pnlShopList.Height);

        }
    }
}
