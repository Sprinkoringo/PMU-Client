using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuAddSign : Widgets.BorderedPanel, Core.IMenu {
        public bool Modal {
            get;
            set;
        }

        Label lblAddTile;
        TextBox txtHouse1;
        TextBox txtHouse2;
        TextBox txtHouse3;
        Label lblPrice;
        Button btnAccept;
        Button btnCancel;
        int price;

        public mnuAddSign(string name, int price)
            : base(name) {
                this.price = price;
            this.Size = new Size(250, 250);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = Client.Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

            lblAddTile = new Label("lblAddTile");
            lblAddTile.Location = new Point(25, 15);
            lblAddTile.AutoSize = false;
            lblAddTile.Size = new System.Drawing.Size(this.Width - lblAddTile.X * 2, 40);
            lblAddTile.Text = "Write the things you want your sign to say:";
            lblAddTile.ForeColor = Color.WhiteSmoke;

            txtHouse1 = new TextBox("txtHouse1");
            txtHouse1.Location = new Point(lblAddTile.X, lblAddTile.Y + lblAddTile.Height + 10);
            txtHouse1.Size = new Size(this.Width - (lblAddTile.X * 2), 16);
            Skins.SkinManager.LoadTextBoxGui(txtHouse1);
            txtHouse1.TextChanged +=new EventHandler(txtHouse_TextChanged);

            txtHouse2 = new TextBox("txtHouse2");
            txtHouse2.Location = new Point(txtHouse1.X, txtHouse1.Y + txtHouse1.Height + 10);
            txtHouse2.Size = new Size(this.Width - (txtHouse1.X * 2), 16);
            Skins.SkinManager.LoadTextBoxGui(txtHouse2);
            txtHouse2.TextChanged += new EventHandler(txtHouse_TextChanged);

            txtHouse3 = new TextBox("txtHouse3");
            txtHouse3.Location = new Point(txtHouse2.X, txtHouse2.Y + txtHouse2.Height + 10);
            txtHouse3.Size = new Size(this.Width - (txtHouse2.X * 2), 16);
            Skins.SkinManager.LoadTextBoxGui(txtHouse3);
            txtHouse3.TextChanged += new EventHandler(txtHouse_TextChanged);

            lblPrice = new Label("lblPrice");
            lblPrice.Location = new Point(lblAddTile.X, txtHouse3.Y + txtHouse3.Height + 10);
            lblPrice.AutoSize = false;
            lblPrice.Size = new System.Drawing.Size(120, 40);
            lblPrice.Text = "Placing this tile will cost " + ((txtHouse1.Text.Length + txtHouse2.Text.Length + txtHouse3.Text.Length) * price) + " " + Items.ItemHelper.Items[1].Name + ".";
            lblPrice.ForeColor = Color.WhiteSmoke;

            btnAccept = new Button("btnAccept");
            btnAccept.Location = new Point(lblAddTile.X, lblPrice.Y + lblPrice.Height + 10);
            btnAccept.Size = new Size(80, 30);
            btnAccept.Text = "Place Sign";
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
            this.AddWidget(txtHouse1);
            this.AddWidget(txtHouse2);
            this.AddWidget(txtHouse3);
            this.AddWidget(lblPrice);
            this.AddWidget(btnAccept);
            this.AddWidget(btnCancel);
        }

        void txtHouse_TextChanged(object sender, EventArgs e) {
            lblPrice.Text = "Placing this tile will cost " + ((txtHouse1.Text.Length + txtHouse2.Text.Length + txtHouse3.Text.Length) * price) + " " + Items.ItemHelper.Items[1].Name + ".";
        }

        void btnAccept_Click(object sender, MouseButtonEventArgs e) {
            Messenger.SendAddSignRequest(txtHouse1.Text, txtHouse2.Text, txtHouse3.Text);
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            MenuSwitcher.CloseAllMenus();
            Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }
    }
}
