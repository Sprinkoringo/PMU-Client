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
using Client.Logic.Tournaments;

namespace Client.Logic.Menus
{
    class mnuTournamentListing : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        Enums.TournamentListingMode mode;
        Label[] lblActiveTournaments;
        Label lblJoinTournament;
        Widgets.MenuItemPicker itemPicker;
        public int currentTen;
        Label lblItemNum;
        TournamentListing[] listings;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuTournamentListing(string name, TournamentListing[] listings, Enums.TournamentListingMode mode)
            : base(name) {

            this.listings = listings;
            this.mode = mode;

            base.Size = new Size(315, 360);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblJoinTournament = new Label("lblJoinTournament");
            lblJoinTournament.AutoSize = true;
            lblJoinTournament.Font = FontManager.LoadFont("PMU", 48);
            if (mode == Enums.TournamentListingMode.Join) {
                lblJoinTournament.Text = "Join A Tournament";
            } else if (mode == Enums.TournamentListingMode.Spectate) {
                lblJoinTournament.Text = "Spectate In A Tournament";
            }
            lblJoinTournament.ForeColor = Color.WhiteSmoke;
            lblJoinTournament.Location = new Point(20, 0);

            lblItemNum = new Label("lblItemNum");
            //lblItemNum.Size = new Size(100, 30);
            lblItemNum.AutoSize = true;
            lblItemNum.Location = new Point(182, 15);
            lblItemNum.Font = FontManager.LoadFont("PMU", 32);
            lblItemNum.BackColor = Color.Transparent;
            lblItemNum.ForeColor = Color.WhiteSmoke;
            lblItemNum.Text = "";//"0/" + ((MaxInfo.MaxInv - 1) / 10 + 1);

            lblActiveTournaments = new Label[10];
            for (int i = 0; i < lblActiveTournaments.Length; i++) {
                lblActiveTournaments[i] = new Label("lblActiveTournaments" + i);
                //lblVisibleItems[i].AutoSize = true;
                //lblVisibleItems[i].Size = new Size(200, 32);
                lblActiveTournaments[i].Width = 200;
                lblActiveTournaments[i].Font = FontManager.LoadFont("PMU", 32);
                lblActiveTournaments[i].Location = new Point(35, (i * 30) + 48);
                //lblVisibleItems[i].HoverColor = Color.Red;
                lblActiveTournaments[i].ForeColor = Color.WhiteSmoke;
                lblActiveTournaments[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblActiveTournament_Click);
                this.AddWidget(lblActiveTournaments[i]);
            }

            this.AddWidget(itemPicker);
            this.AddWidget(lblJoinTournament);
            this.AddWidget(lblItemNum);

            currentTen = 0;
            DisplayItems(currentTen * 10);
            ChangeSelected(0 % 10);
        }

        void lblActiveTournament_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (Players.PlayerManager.MyPlayer.GetInvItemNum(currentTen * 10 + Array.IndexOf(lblActiveTournaments, sender)) > 0) {
                ChangeSelected(Array.IndexOf(lblActiveTournaments, sender));

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTournamentListingSelected("mnuTournamentListingSelected", listings[GetSelectedItemSlot()], mode));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTournamentListingSelected");


                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        public void DisplayItems(int startNum) {
            this.BeginUpdate();
            for (int i = 0; i < lblActiveTournaments.Length; i++) {
                if (startNum + i >= listings.Length) {
                    break;
                }
                lblActiveTournaments[i].Text = listings[startNum + i].Name;
            }
            this.EndUpdate();
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 63 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        private int GetSelectedItemSlot() {
            return itemPicker.SelectedItem + currentTen * 10;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem >= 9 || itemPicker.SelectedItem + 1 + currentTen >= listings.Length) {
                            ChangeSelected(0);
                            //DisplayItems(1);
                        } else {

                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem <= 0) {
                            ChangeSelected(System.Math.Min(9, (listings.Length - 1) % 10));
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                        Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.LeftArrow: {
                        //int itemSlot = (currentTen + 1) - 10;//System.Math.Max(1, GetSelectedItemSlot() - (11 - itemPicker.SelectedItem));
                        if (currentTen <= 0) {
                            currentTen = ((listings.Length - 1) / 10);
                        } else {
                            currentTen--;
                        }
                        DisplayItems(currentTen * 10);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow: {
                        //int itemSlot = currentTen + 1 + 10;
                        if (currentTen >= ((listings.Length - 1) / 10)) {
                            currentTen = 0;
                        } else {
                            currentTen++;
                        }
                        DisplayItems(currentTen * 10);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTournamentListingSelected("mnuTournamentListingSelected", listings[GetSelectedItemSlot()], mode));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTournamentListingSelected");

                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {


                    }
                    break;
            }
        }

    }
}
