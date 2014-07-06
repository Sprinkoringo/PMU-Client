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

using Client.Logic.Graphics;

using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuAddNotice : Widgets.BorderedPanel, Core.IMenu {
        public bool Modal {
            get;
            set;
        }

        Label lblAddTile;

        Label lblAddTile2;
        TextBox txtHouse1;
        TextBox txtHouse2;
        ListBox lstSound;
        Label lblPrice;
        Button btnAccept;
        Button btnCancel;
        int price;
        int wordPrice;

        public mnuAddNotice(string name, int price, int wordPrice)
            : base(name) {
            this.price = price;
            this.wordPrice = wordPrice;

            this.Size = new Size(250, 350);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = Client.Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

            lblAddTile = new Label("lblAddTile");
            lblAddTile.Location = new Point(25, 15);
            lblAddTile.AutoSize = false;
            lblAddTile.Size = new System.Drawing.Size(this.Width - lblAddTile.X * 2, 40);
            lblAddTile.Text = "Write what you want your notice to say:";
            lblAddTile.ForeColor = Color.WhiteSmoke;

            txtHouse1 = new TextBox("txtHouse1");
            txtHouse1.Location = new Point(lblAddTile.X, lblAddTile.Y + lblAddTile.Height + 10);
            txtHouse1.Size = new Size(this.Width - (lblAddTile.X * 2), 16);
            Skins.SkinManager.LoadTextBoxGui(txtHouse1);
            txtHouse1.TextChanged += new EventHandler(txtHouse_TextChanged);

            txtHouse2 = new TextBox("txtHouse2");
            txtHouse2.Location = new Point(txtHouse1.X, txtHouse1.Y + txtHouse1.Height + 10);
            txtHouse2.Size = new Size(this.Width - (txtHouse1.X * 2), 16);
            Skins.SkinManager.LoadTextBoxGui(txtHouse2);
            txtHouse2.TextChanged += new EventHandler(txtHouse_TextChanged);

            lblAddTile2 = new Label("lblAddTile2");
            lblAddTile2.Location = new Point(txtHouse2.X, txtHouse2.Y + txtHouse2.Height + 10);
            lblAddTile2.AutoSize = false;
            lblAddTile2.Size = new System.Drawing.Size(this.Width - lblAddTile2.X * 2, 20);
            lblAddTile2.Text = "Choose a sound to play:";
            lblAddTile2.ForeColor = Color.WhiteSmoke;

            lstSound = new ListBox("lstSound");
            lstSound.Location = new Point(lblAddTile2.X, lblAddTile2.Y + lblAddTile2.Height);
            lstSound.Size = new Size(180, 120);
            
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
                lstSound.Items.Add(new ListBoxTextItem(font, "None"));
                string[] sfxFiles = System.IO.Directory.GetFiles(IO.Paths.SfxPath);
                for (int i = 0; i < sfxFiles.Length; i++) {
                    lstSound.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(sfxFiles[i])));
                }

                lstSound.ItemSelected += new EventHandler(lstSound_ItemSelected);

            lblPrice = new Label("lblPrice");
            lblPrice.Location = new Point(lblAddTile.X, lstSound.Y + lstSound.Height + 10);
            lblPrice.AutoSize = false;
            lblPrice.Size = new System.Drawing.Size(120, 30);
            lblPrice.Text = "Placing this tile will cost " + ((txtHouse1.Text.Length + txtHouse2.Text.Length) * wordPrice + price) + " " + Items.ItemHelper.Items[1].Name + ".";
            lblPrice.ForeColor = Color.WhiteSmoke;

            btnAccept = new Button("btnAccept");
            btnAccept.Location = new Point(lblAddTile.X, lblPrice.Y + lblPrice.Height + 10);
            btnAccept.Size = new Size(80, 30);
            btnAccept.Text = "Place Notice";
            btnAccept.Font = FontManager.LoadFont("tahoma", 10);
            Skins.SkinManager.LoadButtonGui(btnAccept);
            btnAccept.Click += new EventHandler<MouseButtonEventArgs>(btnAccept_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Location = new Point(btnAccept.X + btnAccept.Width, lblPrice.Y + lblPrice.Height + 10);
            btnCancel.Size = new Size(80, 30);
            btnCancel.Text = "Cancel";
            btnCancel.Font = FontManager.LoadFont("tahoma", 10);
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            this.AddWidget(lblAddTile);
            this.AddWidget(lblAddTile2);
            this.AddWidget(txtHouse1);
            this.AddWidget(txtHouse2);
            this.AddWidget(lstSound);
            this.AddWidget(lblPrice);
            this.AddWidget(btnAccept);
            this.AddWidget(btnCancel);
        }

        void txtHouse_TextChanged(object sender, EventArgs e) {
            lblPrice.Text = "Placing this tile will cost " + ((txtHouse1.Text.Length + txtHouse2.Text.Length) * wordPrice + price) + " " + Items.ItemHelper.Items[1].Name + ".";
        }

        void btnAccept_Click(object sender, MouseButtonEventArgs e) {
            String sound = "";
            if (lstSound.SelectedItems.Count > 0) sound = ((ListBoxTextItem)lstSound.SelectedItems[0]).Text;
            Messenger.SendAddNoticeRequest(txtHouse1.Text, txtHouse2.Text, sound);
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        }

        void lstSound_ItemSelected(object sender, EventArgs e) {
            Music.Music.AudioPlayer.PlaySoundEffect(((ListBoxTextItem)lstSound.SelectedItems[0]).Text);
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }
    }
}
