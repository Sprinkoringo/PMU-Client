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
using Client.Logic.Graphics;
using Client.Logic.Network;
using PMU.Sockets;
using Client.Logic.Players;
using Client.Logic.Moves;

namespace Client.Logic.Menus
{
    class mnuMoveOverwrite : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields

        const int MAX_ITEMS = 3;

        Widgets.MenuItemPicker itemPicker;
        Label lblMove1;
        Label lblMove2;
        Label lblMove3;
        Label lblMove4;

        #endregion Fields

        #region Constructors

        public mnuMoveOverwrite(string name)
            : base(name) {
            this.Size = new Size(170, 178);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblMove1 = new Label("lblMove1");
            lblMove1.AutoSize = true;
            lblMove1.Location = new Point(30, 8);
            lblMove1.Font = FontManager.LoadFont("PMU", 32);
            lblMove1.Text = PlayerManager.MyPlayer.Moves[0].MoveNum > 0 ? MoveHelper.Moves[PlayerManager.MyPlayer.Moves[0].MoveNum].Name : "----"; ;
            lblMove1.HoverColor = Color.Red;
            lblMove1.ForeColor = Color.WhiteSmoke;
            lblMove1.Click += new EventHandler<MouseButtonEventArgs>(lblMove1_Click);

            lblMove2 = new Label("lblMove2");
            lblMove2.AutoSize = true;
            lblMove2.Location = new Point(30, 38);
            lblMove2.Font = FontManager.LoadFont("PMU", 32);
            lblMove2.Text = PlayerManager.MyPlayer.Moves[1].MoveNum > 0 ? MoveHelper.Moves[PlayerManager.MyPlayer.Moves[1].MoveNum].Name : "----"; ;
            lblMove2.HoverColor = Color.Red;
            lblMove2.ForeColor = Color.WhiteSmoke;
            lblMove2.Click += new EventHandler<MouseButtonEventArgs>(lblMove2_Click);

            lblMove3 = new Label("lblMove3");
            lblMove3.AutoSize = true;
            lblMove3.Location = new Point(30, 68);
            lblMove3.Font = FontManager.LoadFont("PMU", 32);
            lblMove3.Text = PlayerManager.MyPlayer.Moves[2].MoveNum > 0 ? MoveHelper.Moves[PlayerManager.MyPlayer.Moves[2].MoveNum].Name : "----"; ;
            lblMove3.HoverColor = Color.Red;
            lblMove3.ForeColor = Color.WhiteSmoke;
            lblMove3.Click += new EventHandler<MouseButtonEventArgs>(lblMove3_Click);

            lblMove4 = new Label("lblMove4");
            lblMove4.AutoSize = true;
            lblMove4.Location = new Point(30, 98);
            lblMove4.Font = FontManager.LoadFont("PMU", 32);
            lblMove4.Text = PlayerManager.MyPlayer.Moves[3].MoveNum > 0 ? MoveHelper.Moves[PlayerManager.MyPlayer.Moves[3].MoveNum].Name : "----";
            lblMove4.HoverColor = Color.Red;
            lblMove4.ForeColor = Color.WhiteSmoke;
            lblMove4.Click += new EventHandler<MouseButtonEventArgs>(lblMove4_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblMove1);
            this.AddWidget(lblMove2);
            this.AddWidget(lblMove3);
            this.AddWidget(lblMove4);
        }

        void lblMove1_Click(object sender, MouseButtonEventArgs e) {
            SelectItem(0);
        }

        void lblMove2_Click(object sender, MouseButtonEventArgs e) {
            SelectItem(1);
        }

        void lblMove3_Click(object sender, MouseButtonEventArgs e) {
            SelectItem(2);
        }

        void lblMove4_Click(object sender, MouseButtonEventArgs e) {
            SelectItem(3);
        }

        #endregion Constructors

        #region Properties

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Properties

        #region Methods

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
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
            }
        }

        private void SelectItem(int itemNum) {
            switch (itemNum) {
                case 0: {
                        Messenger.SendPacket(TcpPacket.CreatePacket("overwritemove", "0"));
                    }
                    break;
                case 1: {
                        Messenger.SendPacket(TcpPacket.CreatePacket("overwritemove", "1"));
                    }
                    break;
                case 2: {
                        Messenger.SendPacket(TcpPacket.CreatePacket("overwritemove", "2"));
                    }
                    break;
                case 3: {
                        Messenger.SendPacket(TcpPacket.CreatePacket("overwritemove", "3"));
                    }
                    break;
            }
            MenuSwitcher.CloseAllMenus();
        }

        #endregion Methods
    }
}
