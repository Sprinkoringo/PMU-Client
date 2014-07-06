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
using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Players;
using Client.Logic.Network;

namespace Client.Logic.Widgets
{
    class ActiveTeamPanel : Panel
    {
        Label[] lblRecruitExp;
        Label[] lblRecruitHP;
        Label[] lblRecruitLevel;
        ProgressBar[] pgbRecruitExp;
        ProgressBar[] pgbRecruitHP;
        PictureBox[] picRecruit;
        PictureBox[] picStatus;
        PictureBox[] picHeldItem;
        PictureBox picActiveRecruitBorder;
        int selectedSlot;

        public event EventHandler<Events.ActiveRecruitChangedEventArgs> ActiveRecruitChanged;

        public ActiveTeamPanel(string name)
            : base(name) {
            base.BackColor = Color.Transparent;

            //base.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            base.BackgroundImage = Skins.SkinManager.LoadGui("Game Window/TeamPanel");

            selectedSlot = -1;

            picActiveRecruitBorder = new PictureBox("picActiveRecruitBorder");
            picActiveRecruitBorder.Size = new Size(48, 48);
            picActiveRecruitBorder.BackColor = Color.Transparent;
            picActiveRecruitBorder.Visible = false;
            SdlDotNet.Graphics.Surface surf = Skins.SkinManager.LoadGuiElement("Game Window", "TeamPanel\\selected.png");
            picActiveRecruitBorder.Image = surf;

            this.AddWidget(picActiveRecruitBorder);

            picRecruit = new PictureBox[4];
            picStatus = new PictureBox[4];
            picHeldItem = new PictureBox[4];
            lblRecruitLevel = new Label[4];
            lblRecruitHP = new Label[4];
            pgbRecruitHP = new ProgressBar[4];
            lblRecruitExp = new Label[4];
            pgbRecruitExp = new ProgressBar[4];

            for (int i = 0; i < 4; i++) {
                picRecruit[i] = new PictureBox("picRecruit" + i.ToString());
                picRecruit[i].Size = new Size(40, 40);
                picRecruit[i].Location = new Point(19 + i * 208, 20);
                picRecruit[i].SizeMode = ImageSizeMode.StretchImage;
                picRecruit[i].BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
                picRecruit[i].BorderColor = Color.Black;
                picRecruit[i].BorderWidth = 1;
                picRecruit[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(picRecruit_Click);

                picStatus[i] = new PictureBox("picStatus" + i.ToString());
                picStatus[i].Size = new Size(32, 16);
                picStatus[i].BackColor = Color.Transparent;
                picStatus[i].Location = new Point(59 + i * 208, 18);

                picHeldItem[i] = new PictureBox("picHeldItem" + i.ToString());
                picHeldItem[i].Size = new Size(32, 32);
                picHeldItem[i].BackColor = Color.Transparent;
                picHeldItem[i].Location = new Point(55 + i * 208, 32);
                //picHeldItem[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(picHeldItem_Click);

                lblRecruitLevel[i] = new Label("lblRecruitLevel" + i.ToString());
                lblRecruitLevel[i].Font = Logic.Graphics.FontManager.LoadFont("PMU", 16);
                lblRecruitLevel[i].Location = new Point(83 + i * 208, 14);
                lblRecruitLevel[i].AutoSize = true;

                lblRecruitHP[i] = new Label("lblRecruitHP" + i.ToString());
                lblRecruitHP[i].Font = Logic.Graphics.FontManager.LoadFont("PMU", 16);
                lblRecruitHP[i].Location = new Point(82 + i * 208, 28);
                lblRecruitHP[i].AutoSize = true;
                lblRecruitHP[i].Text = "HP:";

                pgbRecruitHP[i] = new ProgressBar("pgbRecruitHP" + i.ToString());
                pgbRecruitHP[i].Size = new Size(100, 14);
                pgbRecruitHP[i].Location = new Point(107 + i * 208, 31);
                pgbRecruitHP[i].Maximum = 100;
                pgbRecruitHP[i].Minimum = 0;

                lblRecruitExp[i] = new Label("lblRecruitExp" + i.ToString());
                lblRecruitExp[i].Font = Logic.Graphics.FontManager.LoadFont("PMU", 16);
                lblRecruitExp[i].Location = new Point(82 + i * 208, 44);
                lblRecruitExp[i].Text = "Exp:";

                pgbRecruitExp[i] = new ProgressBar("pgbRecruitExp" + i.ToString());
                pgbRecruitExp[i].Location = new Point(107 + i * 208, 47);
                pgbRecruitExp[i].Size = new Size(100, 14);
                pgbRecruitExp[i].BarColor = Color.CornflowerBlue;
                pgbRecruitExp[i].Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                pgbRecruitExp[i].Maximum = 100;
                pgbRecruitExp[i].Minimum = 0;


                this.AddWidget(picRecruit[i]);
                this.AddWidget(picStatus[i]);
                this.AddWidget(picHeldItem[i]);
                this.AddWidget(lblRecruitLevel[i]);
                this.AddWidget(lblRecruitHP[i]);
                this.AddWidget(pgbRecruitHP[i]);
                this.AddWidget(lblRecruitExp[i]);
                this.AddWidget(pgbRecruitExp[i]);
            }

        }

        void picRecruit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (ActiveRecruitChanged != null) {
                ActiveRecruitChanged(this, new Events.ActiveRecruitChangedEventArgs(Array.IndexOf(picRecruit, sender)));
            }
            //SetSelected(Array.IndexOf(picRecruit, sender));
        }

        void picHeldItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            int itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(PlayerManager.MyPlayer.Team[Array.IndexOf(picHeldItem, sender)].HeldItemSlot);
            if (itemNum > 0) {
                if ((int)Items.ItemHelper.Items[itemNum].Type < 8 || (int)Items.ItemHelper.Items[itemNum].Type == 15) {

                } else {
                    Messenger.SendUseItem(PlayerManager.MyPlayer.Team[Array.IndexOf(picHeldItem, sender)].HeldItemSlot);
                }
            }
        }

        public void DisplayRecruitData(int teamSlot) {
            Players.Recruit recruit = PlayerManager.MyPlayer.Team[teamSlot];
            if (recruit.Loaded) {
                ShowRecruitSlot(teamSlot);

                picRecruit[teamSlot].Image = Logic.Graphics.GraphicsManager.GetMugshot(recruit.Num, recruit.Form, (int)recruit.Shiny, (int)recruit.Sex).GetEmote(0);//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((recruit.Mugshot % 15) * 40, (recruit.Mugshot / 15) * 40, 40, 40));

                DisplayRecruitStatusAilment(teamSlot);
                DisplayRecruitHeldItem(teamSlot);

                string text = "Level " + recruit.Level.ToString() + "                ";

                if (recruit.Sex == Enums.Sex.Male) {
                    text += "♂";
                } else if (recruit.Sex == Enums.Sex.Female) {
                    text += "♀";
                }

                lblRecruitLevel[teamSlot].Text = text;

                DisplayRecruitHP(teamSlot);
                DisplayRecruitExp(teamSlot);

            } else {
                HideRecruitSlot(teamSlot);
            }
        }

        public void HideRecruitSlot(int teamSlot) {
            picRecruit[teamSlot].Visible = false;
            picStatus[teamSlot].Visible = false;
            picHeldItem[teamSlot].Visible = false;
            lblRecruitLevel[teamSlot].Visible = false;
            lblRecruitHP[teamSlot].Visible = false;
            lblRecruitExp[teamSlot].Visible = false;
            pgbRecruitExp[teamSlot].Visible = false;
            pgbRecruitHP[teamSlot].Visible = false;
        }

        public void ShowRecruitSlot(int slot) {
            picRecruit[slot].Visible = true;
            //picStatus[slot].Visible = true;
            //picHeldItem[slot].Visible = true;
            lblRecruitLevel[slot].Visible = true;
            lblRecruitHP[slot].Visible = true;
            lblRecruitExp[slot].Visible = true;
            pgbRecruitExp[slot].Visible = true;
            pgbRecruitHP[slot].Visible = true;
        }

        public void DisplayRecruitHP(int teamSlot) {
            pgbRecruitHP[teamSlot].Value = PlayerManager.MyPlayer.Team[teamSlot].HP * 100 / PlayerManager.MyPlayer.Team[teamSlot].MaxHP;
            pgbRecruitHP[teamSlot].Text = PlayerManager.MyPlayer.Team[teamSlot].HP + "/" + PlayerManager.MyPlayer.Team[teamSlot].MaxHP;
            if (pgbRecruitHP[teamSlot].Value < pgbRecruitHP[teamSlot].Maximum / 5) {
                pgbRecruitHP[teamSlot].BarColor = Color.Red;
            } else if (pgbRecruitHP[teamSlot].Value < pgbRecruitHP[teamSlot].Maximum / 2) {
                pgbRecruitHP[teamSlot].BarColor = Color.Yellow;
            } else {
                pgbRecruitHP[teamSlot].BarColor = Color.Green;
            }
        }

        public void DisplayRecruitExp(int teamSlot) {
            pgbRecruitExp[teamSlot].Value = PlayerManager.MyPlayer.Team[teamSlot].ExpPercent;
            pgbRecruitExp[teamSlot].Text = pgbRecruitExp[teamSlot].Percent.ToString() + "%";
            //if (teamSlot == PlayerHelper.Players.GetMyPlayer().ActiveTeamNum) {
            //    Windows.WindowSwitcher.GameWindow.lblExp.Text = GetMyPlayerRecruit(teamSlot).Exp.ToString() + "/" + GetMyPlayerRecruit(teamSlot).MaxExp.ToString();
            //}
        }

        public void DisplayRecruitLevel(int teamSlot) {
            lblRecruitLevel[teamSlot].Text = "Level " + PlayerManager.MyPlayer.Team[teamSlot].Level.ToString();
            //lblLevel.Text = Players.PlayerHelper.Players.GetMyPlayerRecruit(slot).Level.ToString();
        }

        public void DisplayRecruitStatusAilment(int teamSlot) {
            if (PlayerManager.MyPlayer.Team[teamSlot].StatusAilment != Enums.StatusAilment.OK) {
                picStatus[teamSlot].Visible = true;
                picStatus[teamSlot].Image = Tools.CropImage(Skins.SkinManager.LoadGuiElement("Game Window", "TeamPanel\\StatusAilments.png"), new Rectangle(0, ((int)PlayerManager.MyPlayer.Team[teamSlot].StatusAilment - 1) * 16, 32, 16));
                //picStatus[teamSlot].Image = surf;

            } else {
                picStatus[teamSlot].Visible = false;
            }
        }

        public void DisplayRecruitHeldItem(int teamSlot) {
            int HeldItemSlot = PlayerManager.MyPlayer.Team[teamSlot].HeldItemSlot;
            if (HeldItemSlot > 0) {
                picHeldItem[teamSlot].Image = Tools.CropImage(Logic.Graphics.GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(HeldItemSlot)].Pic - (int)(Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(HeldItemSlot)].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(HeldItemSlot)].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                picHeldItem[teamSlot].Visible = true;
            } else {
                picHeldItem[teamSlot].Visible = false;
            }
            this.RequestRedraw();
        }

        public void SetSelected(int slot) {
            if (!picActiveRecruitBorder.Visible) {
                picActiveRecruitBorder.Visible = true;
            }
            if (selectedSlot != slot) {
                for (int i = 0; i < 4; i++) {
                    if (i != slot && picRecruit[i].BorderStyle != SdlDotNet.Widgets.BorderStyle.FixedSingle) {
                        picRecruit[i].BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
                    }
                }
                picRecruit[slot].BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
                picActiveRecruitBorder.Location = new Point(15 + 208 * slot, 16);
                selectedSlot = slot;
            }
        }

    }
}
