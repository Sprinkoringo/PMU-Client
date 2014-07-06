using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuChangeWeather : Widgets.BorderedPanel, Core.IMenu {
        public bool Modal {
            get;
            set;
        }

        //Label lblAddTile;

        Label lblAddTile;
        ListBox lstSound;
        Label lblPrice;
        Button btnAccept;
        Button btnCancel;
        int price;

        public mnuChangeWeather(string name, int price)
            : base(name) {
            this.price = price;

            this.Size = new Size(250, 250);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = Client.Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

            lblAddTile = new Label("lblAddTile");
            lblAddTile.Location = new Point(25, 15);
            lblAddTile.AutoSize = false;
            lblAddTile.Size = new System.Drawing.Size(this.Width - lblAddTile.X * 2, 20);
            lblAddTile.Text = "Choose a weather condition:";
            lblAddTile.ForeColor = Color.WhiteSmoke;

            lstSound = new ListBox("lstSound");
            lstSound.Location = new Point(lblAddTile.X, lblAddTile.Y + lblAddTile.Height);
            lstSound.Size = new Size(180, 120);
            
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
                //string[] sfxFiles = System.IO.Directory.GetFiles(IO.Paths.SfxPath);
                for (int i = 1; i < 12; i++) {
                    if ((Enums.Weather)i != Enums.Weather.DiamondDust) {
                        lstSound.Items.Add(new ListBoxTextItem(font, ((Enums.Weather)i).ToString()));
                    }
                }
                lstSound.SelectedIndex = 0;

            lblPrice = new Label("lblPrice");
            lblPrice.Location = new Point(lblAddTile.X, lstSound.Y + lstSound.Height + 10);
            lblPrice.AutoSize = false;
            lblPrice.Size = new System.Drawing.Size(180, 30);
            lblPrice.Text = "Changing the weather will cost " + price + " " + Items.ItemHelper.Items[1].Name + ".";
            lblPrice.ForeColor = Color.WhiteSmoke;

            btnAccept = new Button("btnAccept");
            btnAccept.Location = new Point(lblAddTile.X, lblPrice.Y + lblPrice.Height + 10);
            btnAccept.Size = new Size(80, 30);
            btnAccept.Text = "Set Weather";
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
            this.AddWidget(lstSound);
            this.AddWidget(lblPrice);
            this.AddWidget(btnAccept);
            this.AddWidget(btnCancel);
        }


        void btnAccept_Click(object sender, MouseButtonEventArgs e) {
            int weather = lstSound.SelectedIndex;
            if (weather == -1) return;
            weather++;
            if (weather > 5) weather++;
            Messenger.SendWeatherRequest(weather);
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

        void lstSound_ItemSelected(object sender, EventArgs e) {
            Music.Music.AudioPlayer.PlaySoundEffect(((ListBoxTextItem)lstSound.SelectedItems[0]).Text);
        }
    }
}
