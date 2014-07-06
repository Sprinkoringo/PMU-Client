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
using Client.Logic.Network;


namespace Client.Logic.Menus
{
    class mnuJobSelected : Widgets.BorderedPanel, Core.IMenu
    {
        int jobSlot;
        Label lblAccept;
        Label lblDescription;
        Label lblDelete;
        Label lblSend;
        TextBox txtSend;
        Widgets.MenuItemPicker itemPicker;
        int maxItems;

        public int JobSlot {
            get { return jobSlot; }
            set {
                jobSlot = value;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuJobSelected(string name, int jobSlot)
            : base(name) {
                int size = 95;
                maxItems = 1;
                if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Finished ||
                    Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Failed) {
                } else {
                    size += 30;
                    maxItems++;
                }
                if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].CanSend) {
                    size += 60;
                    maxItems++;
                }
                base.Size = new Size(180, size);
            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(300, 34);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            int locY = 8;

            if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Finished ||
                Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Failed) {
                
            } else {
                lblAccept = new Label("lblAccept");
                lblAccept.Font = FontManager.LoadFont("PMU", 32);
                lblAccept.AutoSize = true;
                if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Obtained ||
                    Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Suspended) {
                    lblAccept.Text = "Accept";
                } else if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Taken) {
                    lblAccept.Text = "Cancel";
                }
                lblAccept.Location = new Point(30, locY);
                lblAccept.HoverColor = Color.Red;
                lblAccept.ForeColor = Color.WhiteSmoke;
                lblAccept.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblAccept_Click);

                this.AddWidget(lblAccept);

                locY += 30;
            }

            

            lblDescription = new Label("lblDescription");
            lblDescription.Font = FontManager.LoadFont("PMU", 32);
            lblDescription.AutoSize = true;
            lblDescription.Text = "Description";
            lblDescription.Location = new Point(30, locY);
            lblDescription.HoverColor = Color.Red;
            lblDescription.ForeColor = Color.WhiteSmoke;
            lblDescription.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblDescription_Click);
            locY += 30;

            lblDelete = new Label("lblDelete");
            lblDelete.Font = FontManager.LoadFont("PMU", 32);
            lblDelete.AutoSize = true;
            lblDelete.Text = "Delete";
            lblDelete.Location = new Point(30, locY);
            lblDelete.HoverColor = Color.Red;
            lblDelete.ForeColor = Color.WhiteSmoke;
            lblDelete.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblDelete_Click);
            locY += 30;

            if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].CanSend) {

                lblSend = new Label("lblSend");
                lblSend.Font = FontManager.LoadFont("PMU", 32);
                lblSend.AutoSize = true;
                lblSend.Text = "Send to:";
                lblSend.Location = new Point(30, locY);
                lblSend.HoverColor = Color.Red;
                lblSend.ForeColor = Color.WhiteSmoke;
                lblSend.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSend_Click);
                locY += 40;

                txtSend = new TextBox("txtSend");
                txtSend.Size = new Size(120, 24);
                txtSend.Location = new Point(32, locY);
                txtSend.Font = FontManager.LoadFont("PMU", 16);
                Skins.SkinManager.LoadTextBoxGui(txtSend);

                this.AddWidget(lblSend);
                this.AddWidget(txtSend);

            }


            this.AddWidget(itemPicker);
            this.AddWidget(lblDescription);
            this.AddWidget(lblDelete);

            this.jobSlot = jobSlot;

        }



        void lblAccept_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0, jobSlot);
        }

        void lblDescription_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1, jobSlot);
        }

        void lblDelete_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(2, jobSlot);
        }

        void lblSend_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(3, jobSlot);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == maxItems) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
            			Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(maxItems);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                    	Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem, jobSlot);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum, int jobSlot) {
            if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Finished ||
                Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Failed) {
                itemNum++;
            }
            switch (itemNum) {
                case 0: { // Accept

                        if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Obtained ||
                            Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Suspended) {
                            Messenger.SendStartMission(jobSlot);
                        } else if (Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot].Accepted == Enums.JobStatus.Taken) {
                            Messenger.SendCancelJob(jobSlot);
                        }
                        MenuSwitcher.ShowJobListMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 1: { // Description
                    MenuSwitcher.ShowJobSummary(Players.PlayerManager.MyPlayer.JobList.Jobs[jobSlot]);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 2: { // Delete
                        Messenger.SendDeleteJob(jobSlot);
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 3: { // Send
                        Messenger.SendSendMission(jobSlot, txtSend.Text);
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        private void CloseMenu() {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuJobList");
        }



        public bool Modal {
            get;
            set;
        }
    }
}
