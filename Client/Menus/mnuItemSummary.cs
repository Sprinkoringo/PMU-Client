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
    class mnuItemSummary : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal
        {
            get;
            set;
        }

        Enums.InvMenuType originalMenu;
        int itemSlot;
        PictureBox picPreview;
        Label lblItem;
        Label lblRarity;
        Label lblPrice;
        Label lblDroppable;
        Label lblLoseable;
        Label lblDescription;




        public Widgets.BorderedPanel MenuPanel
        {
            get { return this; }
        }

        public mnuItemSummary(string name, int itemNum, int itemSlot, Enums.InvMenuType originalMenu)
            : base(name)
        {
            base.Size = new Size(380, 288);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            this.originalMenu = originalMenu;
            this.itemSlot = itemSlot;
            
            picPreview = new PictureBox("picPreview");
            picPreview.Size = new Size(32, 32);
            picPreview.BackColor = Color.Transparent;
            picPreview.Location = new Point(10, 10);
            picPreview.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[itemNum].Pic - (int)(Items.ItemHelper.Items[itemNum].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[itemNum].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));

            lblItem = new Label("lblItem");
            lblItem.Location = new Point(46, 10);
            lblItem.AutoSize = true;
            lblItem.Font = FontManager.LoadFont("PMU", 32);
            lblItem.Text = Items.ItemHelper.Items[itemNum].Name;
            lblItem.ForeColor = Color.WhiteSmoke;
            
            lblRarity = new Label("lblRarity");
            lblRarity.Location = new Point(20, 42);
            lblRarity.AutoSize = true;
            lblRarity.Font = FontManager.LoadFont("PMU", 16);
            lblRarity.Text = "Rarity: " + Items.ItemHelper.Items[itemNum].Rarity;
            lblRarity.ForeColor = Color.WhiteSmoke;

            int y = 42;
            y += 20;
            lblPrice = new Label("lblPrice");
            lblPrice.Location = new Point(20, y);
            lblPrice.AutoSize = true;
            lblPrice.Font = FontManager.LoadFont("PMU", 16);
            if (Items.ItemHelper.Items[itemNum].Price > 0) {
                lblPrice.Text = "Sell Price: " + Items.ItemHelper.Items[itemNum].Price;
            } else {
                lblPrice.Text = "Cannot be sold.";
            }
            lblPrice.ForeColor = Color.WhiteSmoke;

            if (Items.ItemHelper.Items[itemNum].Bound) {
                y += 20;
                lblDroppable = new Label("lblDroppable");
                lblDroppable.Location = new Point(20, y);
                lblDroppable.AutoSize = true;
                lblDroppable.Font = FontManager.LoadFont("PMU", 16);
                lblDroppable.Text = "Cannot be dropped.";
                lblDroppable.ForeColor = Color.WhiteSmoke;
            }

            if (Items.ItemHelper.Items[itemNum].Bound) {
                y += 20;
                lblLoseable = new Label("lblLoseable");
                lblLoseable.Location = new Point(20, y);
                lblLoseable.AutoSize = true;
                lblLoseable.Font = FontManager.LoadFont("PMU", 16);
                lblLoseable.Text = "Cannot be lost.";
                lblLoseable.ForeColor = Color.WhiteSmoke;
            }

            y += 30;
            lblDescription = new Label("lblDescription");
            lblDescription.Location = new Point(20, y);
            //lblDescription.AutoSize = true;
            lblDescription.Size = new Size(300, 220);
            lblDescription.Font = FontManager.LoadFont("PMU", 16);
            lblDescription.Text = Items.ItemHelper.Items[itemNum].Desc;
            lblDescription.ForeColor = Color.WhiteSmoke;
            
            this.AddWidget(picPreview);
            this.AddWidget(lblItem);
            this.AddWidget(lblRarity);
            this.AddWidget(lblPrice);
            this.AddWidget(lblDroppable);
            this.AddWidget(lblLoseable);
            this.AddWidget(lblDescription);
        }

        

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                
                case SdlDotNet.Input.Key.Return:
                    {
                        MenuBack();
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace:
                    {
                        // Show the main menu when the backspace key is pressed
                        MenuBack();
                    }
                    break;
            }
        }

        private void MenuBack()
        {
        	
        	switch (originalMenu) {
        			case Enums.InvMenuType.Use: {
        					MenuSwitcher.ShowInventoryMenu(itemSlot);
        					Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        			}
        				break;
        				case Enums.InvMenuType.Store:{
        					MenuSwitcher.ShowBankDepositMenu(itemSlot);
        					Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        				}
        				break;
        				case Enums.InvMenuType.Take: {
        					MenuSwitcher.ShowBankWithdrawMenu(itemSlot);
        					Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        				}
        				break;
        				case Enums.InvMenuType.Sell:{
        					MenuSwitcher.ShowShopSellMenu(itemSlot);
        					Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        				}
        				
        				break;
        				case Enums.InvMenuType.Buy:{
        					MenuSwitcher.ShowShopBuyMenu(itemSlot);
        					Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        				}
        				
        				break;
        				case Enums.InvMenuType.Recycle:{
        					
        				}
        				
        				break;
        	}

        }

    }
}
