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


namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using PMU.Core;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;

    class mnuAssembly : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        int maxItems;

        public int currentTen;

        int[] team;
        List<Players.Recruit> recruitList;
        List<int> recruitIndex;
        List<int> SortedRecruits;

        Logic.Widgets.MenuItemPicker itemPicker;
        PictureBox picMugshot;
        Label lblName;
        Label lblLevel;
        Label lblAssembly;
        Label lblRecruitNum;
        Label[] lblAllRecruits;
        TextBox txtFind;
        Button btnFind;


        #endregion Fields

        public Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }



        #region Constructors

        public mnuAssembly(string name, string[] parse)
            : base(name) {
            this.Size = new Size(315, 450);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 20);

            currentTen = 0;

            team = new int[4];

            recruitList = new List<Players.Recruit>();
            recruitIndex = new List<int>();

            itemPicker = new Logic.Widgets.MenuItemPicker("itemPicker");


            lblAssembly = new Label("lblAssembly");
            lblAssembly.AutoSize = true;
            lblAssembly.Font = FontManager.LoadFont("PMU", 48);
            lblAssembly.Text = "Assembly";
            lblAssembly.Location = new Point(20, 0);
            lblAssembly.ForeColor = Color.WhiteSmoke;

            lblRecruitNum = new Label("lblRecruitNum");
            //lblItemNum.Size = new Size(100, 30);
            lblRecruitNum.AutoSize = true;
            lblRecruitNum.Location = new Point(222, 14);
            lblRecruitNum.Font = FontManager.LoadFont("PMU", 32);
            lblRecruitNum.BackColor = Color.Transparent;
            lblRecruitNum.Text = "0/0";
            lblRecruitNum.ForeColor = Color.WhiteSmoke;

            txtFind = new TextBox("txtFind");
            txtFind.Size = new Size(130, 20);
            txtFind.Location = new Point(32, 48);
            txtFind.Font = FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadTextBoxGui(txtFind);

            btnFind = new Button("btnFind");
            btnFind.Size = new System.Drawing.Size(40, 20);
            btnFind.Location = new Point(174, 48);
            btnFind.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnFind.Text = "Find";
            Skins.SkinManager.LoadButtonGui(btnFind);
            btnFind.Click += new EventHandler<MouseButtonEventArgs>(btnFind_Click);

            picMugshot = new PictureBox("picMugshot");
            picMugshot.Size = new Size(40, 40);
            picMugshot.BackColor = Color.Transparent;
            picMugshot.Location = new Point(35, 76);

            lblName = new Label("lblName");
            lblName.AutoSize = true;
            lblName.Centered = true;
            lblName.Font = FontManager.LoadFont("PMU", 16);
            lblName.ForeColor = Color.WhiteSmoke;
            lblName.Location = new Point(75, 76);

            lblLevel = new Label("lblLevel");
            lblLevel.AutoSize = true;
            lblLevel.Centered = true;
            lblLevel.Font = FontManager.LoadFont("PMU", 16);
            lblLevel.ForeColor = Color.WhiteSmoke;
            lblLevel.Location = new Point(75, 96);

            lblAllRecruits = new Label[10];
            for (int i = 0; i < 10; i++) {


                lblAllRecruits[i] = new Label("lblAllRecruits" + i);
                //lblAllRecruits[i].AutoSize = true;
                //lblAllRecruits[i].Centered = true;
                lblAllRecruits[i].Width = 200;
                lblAllRecruits[i].Font = FontManager.LoadFont("PMU", 32);
                
                lblAllRecruits[i].Location = new Point(35, (i * 30) + 114);
                //lblAllRecruits[i].HoverColor = Color.Red;
                //lblAllRecruits[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(assemblyItem_Click);
                this.AddWidget(lblAllRecruits[i]);


            }

            LoadRecruitsFromPacket(parse);

            this.AddWidget(itemPicker);
            this.AddWidget(txtFind);
            this.AddWidget(btnFind);
            this.AddWidget(lblAssembly);
            this.AddWidget(lblRecruitNum);
            this.AddWidget(picMugshot);
            this.AddWidget(lblName);
            this.AddWidget(lblLevel);

            FindMaxItems();
            DisplayRecruitList();
            UpdateSelectedRecruitInfo();
            ChangeSelected(0);
        }

        #endregion Constructors

        public void LoadRecruitsFromPacket(string[] parse) {
            int recruitCount = parse[1].ToInt();
            team[0] = parse[2].ToInt();
            team[1] = parse[3].ToInt();
            team[2] = parse[4].ToInt();
            team[3] = parse[5].ToInt();
            int n = 6;
            for (int i = 0; i < recruitCount; i++) {
                recruitIndex.Add(parse[n].ToInt());
                Players.Recruit recruit = new Players.Recruit();
                recruit.Num = parse[n + 1].ToInt();
                recruit.Sex = (Enums.Sex)parse[n + 2].ToInt();
                recruit.Form = parse[n + 3].ToInt();
                recruit.Shiny = (Enums.Coloration)parse[n + 4].ToInt();
                recruit.Level = parse[n + 5].ToInt();
                recruit.Name = parse[n + 6];
                recruitList.Add(recruit);
                n += 7;
            }

        }


        void btnFind_Click(object sender, MouseButtonEventArgs e) {
            
            if (SortedRecruits == null) {
                if (txtFind.Text.Trim() != "") {
                    SortedRecruits = new List<int>();

                    for (int i = 0; i < recruitList.Count; i++) {
                        if (recruitList[i].Name.ToLower().Contains(txtFind.Text.ToLower())) {
                            SortedRecruits.Add(i);
                        }
                    }

                    btnFind.Text = "Cancel";
                }
            } else {
                SortedRecruits = null;
                btnFind.Text = "Find";
            }
            currentTen = 0;
            ChangeSelected(0);
            FindMaxItems();
            DisplayRecruitList();
            UpdateSelectedRecruitInfo();
            
        }

        void assemblyItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectRecruit(Array.IndexOf(lblAllRecruits, sender) + currentTen * 10);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 130 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public void UpdateSelectedRecruitInfo() {
            if (SortedRecruits == null) {
                if (itemPicker.SelectedItem + currentTen * 10 >= 0 && itemPicker.SelectedItem + currentTen * 10 < recruitList.Count) {
                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    Players.Recruit recruit = recruitList[itemPicker.SelectedItem + currentTen * 10];
                    picMugshot.Image = Logic.Graphics.GraphicsManager.GetMugshot(recruit.Num, recruit.Form, (int)recruit.Shiny, (int)recruit.Sex).GetEmote(0);//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot % 15) * 40, (recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot / 15) * 40, 40, 40));
                    watch.Stop();
                    lblName.Text = recruitList[itemPicker.SelectedItem + currentTen * 10].Name;
                    string levelString = "Lv. " + recruitList[itemPicker.SelectedItem + currentTen * 10].Level + "  ";
                    if (recruitList[itemPicker.SelectedItem + currentTen * 10].Sex == Enums.Sex.Male) {
                        levelString += "(♂)";
                    } else if (recruitList[itemPicker.SelectedItem + currentTen * 10].Sex == Enums.Sex.Female) {
                        levelString += "(♀)";
                    }
                    lblLevel.Text = (levelString);

                    lblRecruitNum.Text = (currentTen + 1) + "/" + ((recruitList.Count - 1) / 10 + 1);
                }
            } else {
                if (itemPicker.SelectedItem + currentTen * 10 >= 0 && itemPicker.SelectedItem + currentTen * 10 < SortedRecruits.Count) {
                    System.Diagnostics.Stopwatch watch = new System.Diagnostics.Stopwatch();
                    watch.Start();
                    //picMugshot.BlitToBuffer(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot % 15) * 40, (recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot / 15) * 40, 40, 40));
                    Players.Recruit recruit = recruitList[SortedRecruits[itemPicker.SelectedItem + currentTen * 10]];
                    picMugshot.Image = Logic.Graphics.GraphicsManager.GetMugshot(recruit.Num, recruit.Form, (int)recruit.Shiny, (int)recruit.Sex).GetEmote(0);//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot % 15) * 40, (recruitList[itemPicker.SelectedItem + currentTen * 10].Mugshot / 15) * 40, 40, 40));
                    watch.Stop();
                    lblName.Text = recruitList[SortedRecruits[itemPicker.SelectedItem + currentTen * 10]].Name;
                    string levelString = "Lv. " + recruitList[SortedRecruits[itemPicker.SelectedItem + currentTen * 10]].Level + "  ";
                    if (recruitList[SortedRecruits[itemPicker.SelectedItem + currentTen * 10]].Sex == Enums.Sex.Male) {
                        levelString += "(♂)";
                    } else if (recruitList[SortedRecruits[itemPicker.SelectedItem + currentTen * 10]].Sex == Enums.Sex.Female) {
                        levelString += "(♀)";
                    }
                    lblLevel.Text = (levelString);

                    lblRecruitNum.Text = (currentTen + 1) + "/" + ((SortedRecruits.Count - 1) / 10 + 1);
                } else {
                    picMugshot.Image = Logic.Graphics.GraphicsManager.GetMugshot(0, 0, 0, 0).GetEmote(0);
                    lblName.Text = "---";
                    lblLevel.Text = "---";
                    lblRecruitNum.Text = "0/0";
                }
            }
        }

        public void DisplayRecruitList() {
            //this.BeginUpdate();
            for (int i = 0; i < 10; i++) {

                if (SortedRecruits == null) {
                    if ((i + currentTen * 10) < recruitList.Count) {
                        if (recruitIndex[i + currentTen * 10] == team[0]) {
                            lblAllRecruits[i].ForeColor = Color.Cyan;
                        } else if (recruitIndex[i + currentTen * 10] == team[1] || recruitIndex[i + currentTen * 10] == team[2] || recruitIndex[i + currentTen * 10] == team[3]) {
                            lblAllRecruits[i].ForeColor = Color.Yellow;
                        } else {
                            lblAllRecruits[i].ForeColor = Color.WhiteSmoke;
                        }
                        if (!string.IsNullOrEmpty(recruitList[i + currentTen * 10].Name)) {
                            lblAllRecruits[i].Text = recruitList[i + currentTen * 10].Name;
                        } else {
                            lblAllRecruits[i].Text = "No Name";
                        }
                        if (lblAllRecruits[i].Visible == false) {
                            lblAllRecruits[i].Visible = true;
                        }
                    } else {
                        lblAllRecruits[i].Visible = false;
                    }
                } else {
                    if ((i + currentTen * 10) < SortedRecruits.Count) {
                        if (recruitIndex[SortedRecruits[i + currentTen * 10]] == team[0]) {
                            lblAllRecruits[i].ForeColor = Color.Cyan;
                        } else if (recruitIndex[SortedRecruits[i + currentTen * 10]] == team[1] || recruitIndex[SortedRecruits[i + currentTen * 10]] == team[2] || recruitIndex[SortedRecruits[i + currentTen * 10]] == team[3]) {
                            lblAllRecruits[i].ForeColor = Color.Yellow;
                        } else {
                            lblAllRecruits[i].ForeColor = Color.WhiteSmoke;
                        }
                        if (!string.IsNullOrEmpty(recruitList[SortedRecruits[i + currentTen * 10]].Name)) {
                            lblAllRecruits[i].Text = recruitList[SortedRecruits[i + currentTen * 10]].Name;
                        } else {
                            lblAllRecruits[i].Text = "No Name";
                        }
                        if (lblAllRecruits[i].Visible == false) {
                            lblAllRecruits[i].Visible = true;
                        }
                    } else {
                        if (i == 0) {
                            lblAllRecruits[i].Text = "None";
                            lblAllRecruits[i].ForeColor = Color.Gray;
                            if (lblAllRecruits[i].Visible == false) {
                                lblAllRecruits[i].Visible = true;
                            }
                        } else {
                            lblAllRecruits[i].Visible = false;
                        }
                    }
                }
            }
            this.RequestRedraw();
        }

        public void FindMaxItems() {
            if (SortedRecruits == null) {
                if (recruitList.Count < (currentTen + 1) * 10) {
                    maxItems = recruitList.Count % 10 - 1;
                } else {
                    maxItems = 9;
                }
            } else {
                if (SortedRecruits.Count < (currentTen + 1) * 10) {
                    maxItems = SortedRecruits.Count % 10 - 1;
                } else {
                    maxItems = 9;
                }
            }
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            //if (Loaded) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (maxItems > -1) {
                            if (itemPicker.SelectedItem >= maxItems) {
                                ChangeSelected(0);

                            } else {

                                ChangeSelected(itemPicker.SelectedItem + 1);
                            }
                            UpdateSelectedRecruitInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (maxItems > -1) {
                            if (itemPicker.SelectedItem <= 0) {
                                ChangeSelected(maxItems);
                            } else {
                                ChangeSelected(itemPicker.SelectedItem - 1);
                            }
                            UpdateSelectedRecruitInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.LeftArrow: {
                        if (maxItems > -1) {
                            if (currentTen <= 0) {
                                if (SortedRecruits == null) {
                                    currentTen = ((recruitList.Count - 1) / 10);
                                } else {
                                    currentTen = ((SortedRecruits.Count - 1) / 10);
                                }
                            } else {
                                currentTen--;
                            }
                            FindMaxItems();
                            if (itemPicker.SelectedItem > maxItems) {
                                ChangeSelected(maxItems);
                            }
                            DisplayRecruitList();
                            UpdateSelectedRecruitInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow: {
                        if (maxItems > -1) {
                            if (SortedRecruits == null) {
                                if (currentTen >= ((recruitList.Count - 1) / 10)) {
                                    currentTen = 0;
                                } else {
                                    currentTen++;
                                }
                            } else {
                                if (currentTen >= ((SortedRecruits.Count - 1) / 10)) {
                                    currentTen = 0;
                                } else {
                                    currentTen++;
                                }
                            }
                            FindMaxItems();
                            if (itemPicker.SelectedItem > maxItems) {
                                ChangeSelected(maxItems);
                            }
                            DisplayRecruitList();
                            UpdateSelectedRecruitInfo();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectRecruit(itemPicker.SelectedItem + currentTen * 10);
                    }
                    break;
            }
            //}
        }

        private void SelectRecruit(int slot) {
            int activeTeamStatus;
            if (SortedRecruits == null) {
                if (recruitIndex[slot] == team[0]) {
                    activeTeamStatus = 0;
                } else if (recruitIndex[slot] == team[1]) {
                    activeTeamStatus = 1;
                } else if (recruitIndex[slot] == team[2]) {
                    activeTeamStatus = 2;
                } else if (recruitIndex[slot] == team[3]) {
                    activeTeamStatus = 3;
                } else {
                    activeTeamStatus = -1;
                }
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuRecruitSelected("mnuRecruitSelected", recruitIndex[slot], activeTeamStatus));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuRecruitSelected");
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            } else if (SortedRecruits.Count > slot) {
                if (recruitIndex[SortedRecruits[slot]] == team[0]) {
                    activeTeamStatus = 0;
                } else if (recruitIndex[SortedRecruits[slot]] == team[1]) {
                    activeTeamStatus = 1;
                } else if (recruitIndex[SortedRecruits[slot]] == team[2]) {
                    activeTeamStatus = 2;
                } else if (recruitIndex[SortedRecruits[slot]] == team[3]) {
                    activeTeamStatus = 3;
                } else {
                    activeTeamStatus = -1;
                }
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuRecruitSelected("mnuRecruitSelected", recruitIndex[SortedRecruits[slot]], activeTeamStatus));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuRecruitSelected");
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }


    }

}
