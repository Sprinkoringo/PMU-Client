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

    using Client.Logic.Graphics;
    using PMU.Core;
    using SdlDotNet.Widgets;
    using Client.Logic.Widgets;
    using Client.Logic.Missions;
    using Client.Logic.Network;

    class mnuMissionBoard : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        #region Fields

        const int MAX_ITEMS = 7;

        Logic.Widgets.MenuItemPicker itemPicker;
        Label lblJobList;
        Label lblLoading;
        List<Job> jobs;
        MissionTitle[] items;

        #endregion Fields

        #region Constructors

        public mnuMissionBoard(string name, string[] parse)
            : base(name) {
            base.Size = new Size(280, 460);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(15, 10);

            itemPicker = new Logic.Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(20, 64);

            lblJobList = new Label("lblJobList");
            lblJobList.AutoSize = true;
            lblJobList.Font = FontManager.LoadFont("PMU", 48);
            lblJobList.ForeColor = Color.WhiteSmoke;
            lblJobList.Text = "Missions";
            lblJobList.Location = new Point(20, 0);

            lblLoading = new Label("lblLoading");
            lblLoading.Location = new Point(10, 50);
            lblLoading.Font = FontManager.LoadFont("PMU", 16);
            lblLoading.AutoSize = true;
            lblLoading.Text = "Loading...";
            lblLoading.ForeColor = Color.WhiteSmoke;

            items = new MissionTitle[8];
            int lastY = 58;

            for (int i = 0; i < items.Length; i++) {
                items[i] = new MissionTitle("item" + i, this.Width);
                items[i].Location = new Point(15, lastY);

                
                this.AddWidget(items[i]);

                lastY += items[i].Height + 8;

            }

            this.AddWidget(itemPicker);
            this.AddWidget(lblJobList);
            //this.AddWidget(lblLoading);

            LoadMissionsFromPacket(parse);
        }

        #endregion Constructors

        #region Properties

        public Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }
        #endregion Properties


        #region Methods

        public void LoadMissionsFromPacket(string[] parse) {
            
            int count = parse[1].ToInt();
            int n = 2;
            jobs = new List<Job>();
            for (int i = 0; i < count; i++) {
                
                    Job job = new Job();
                    job.Title = parse[n];
                    job.Summary = parse[n + 1];
                    job.GoalName = parse[n + 2];
                    job.ClientSpecies = parse[n + 3].ToInt();
                    job.ClientForm = parse[n + 4].ToInt();
                    job.MissionType = (Enums.MissionType)parse[n + 5].ToInt();
                    job.Data1 = parse[n + 6].ToInt();
                    job.Data2 = parse[n + 7].ToInt();
                    job.Difficulty = (Enums.JobDifficulty)parse[n + 8].ToInt();
                    job.RewardNum = parse[n + 9].ToInt();
                    job.RewardAmount = parse[n + 10].ToInt();
                    jobs.Add(job);
                
                n += 12;
            }
            RefreshMissionList();
        }

        public void RemoveJob(int index) {
            jobs.RemoveAt(index);
            RefreshMissionList();
        }

        public void AddJob(string[] parse) {
            if (jobs.Count >= 8) {
                jobs.RemoveAt(jobs.Count-1);
            }
            int n = 1;
            Job job = new Job();
            job.Title = parse[n];
            job.Summary = parse[n + 1];
            job.GoalName = parse[n + 2];
            job.ClientSpecies = parse[n + 3].ToInt();
            job.ClientForm = parse[n + 4].ToInt();
            job.MissionType = (Enums.MissionType)parse[n + 5].ToInt();
            job.Data1 = parse[n + 6].ToInt();
            job.Data2 = parse[n + 7].ToInt();
            job.Difficulty = (Enums.JobDifficulty)parse[n + 8].ToInt();
            job.RewardNum = parse[n + 9].ToInt();
            job.RewardAmount = parse[n + 10].ToInt();
            //job.Mugshot = parse[n + 11].ToInt();
            jobs.Insert(0, job);
            RefreshMissionList();
        }
        

        private void RefreshMissionList() {
            
            for (int i = 0; i < items.Length; i++) {
                

                if (jobs.Count > i) {
                    
                    items[i].SetJob(jobs[i]);
                } else {
                    items[i].SetJob(null);
                }
                
            }
            ReloadJobDescription();
        }

        private void ReloadJobDescription() {
            Menus.Core.IMenu mnuJobDesc = Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobDescription");
            Job job = null;
            if (itemPicker.SelectedItem < jobs.Count) {
                job = jobs[itemPicker.SelectedItem];
            }
            if (mnuJobDesc == null) {
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuJobDescription("mnuJobDescription", job, true));
            } else {
                ((mnuJobDescription)mnuJobDesc).UpdateJob(job);
            }
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(20, 64 + ((items[0].Height + 8) * itemNum));
            itemPicker.SelectedItem = itemNum;
            ReloadJobDescription();
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
            if (jobs.Count > itemNum) {
                Messenger.SendAcceptMission(itemNum);
                //Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        #endregion Methods

        #region Old

        //List<Mission> missions;
        //PictureBox picCreator;
        //Label lblCreatorName;
        //Label lblMissionName;
        //Label lblGoal;
        //Label lblDifficulty;
        //Label lblMissionCompletionEvent;
        //Label lblMissionReward;
        //Label lblSlot;

        //Button btnAccept;
        //Button btnClose;


        //public mnuMissionBoard(string name, string[] parse)
        //    : base(name) {
            

        //    base.Size = new Size(300, 300);
        //    base.MenuDirection = Enums.MenuDirection.Vertical;
        //    base.Location = Logic.Graphics.DrawingSupport.GetCenter(Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.Size);

        //    picCreator = new PictureBox("picCreator");
        //    picCreator.Size = new Size(40, 40);
        //    picCreator.Location = new Point(30, 20);
        //    picCreator.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
        //    picCreator.BorderWidth = 1;
        //    //picCreator.Image = Logic.Graphics.GraphicsManager.GetMugshot(missionInfo.CreatorMugshot).Sheet;//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((missionInfo.CreatorMugshot % 15) * 40, (missionInfo.CreatorMugshot / 15) * 40, 40, 40));

        //    lblCreatorName = new Label("lblCreatorName");
        //    lblCreatorName.Font = FontManager.LoadFont("tahoma", 16);
        //    lblCreatorName.AutoSize = true;
        //    //lblCreatorName.Text = "Client: " + missionInfo.CreatorName;
        //    lblCreatorName.ForeColor = Color.WhiteSmoke;
        //    lblCreatorName.Location = new Point(picCreator.X + picCreator.Width + 10, picCreator.Y);

        //    lblDifficulty = new Label("lblDifficulty");
        //    lblDifficulty.Font = FontManager.LoadFont("tahoma", 16);
        //    lblDifficulty.AutoSize = true;
        //    //lblDifficulty.Text = "Difficulty: " + missionInfo.Difficulty;
        //    lblDifficulty.ForeColor = Color.WhiteSmoke;
        //    lblDifficulty.Location = new Point(picCreator.X + picCreator.Width + 10, picCreator.Y + lblCreatorName.Height + 5);

        //    lblMissionName = new Label("lblMissionName");
        //    lblMissionName.Font = FontManager.LoadFont("tahoma", 14);
        //    lblMissionName.Size = new Size(this.Width - 20, 40);
        //    //lblMissionName.Text = "Name: " + missionInfo.Name;
        //    lblMissionName.ForeColor = Color.WhiteSmoke;
        //    lblMissionName.Location = new Point(picCreator.X, picCreator.Y + picCreator.Height + 10);

        //    lblGoal = new Label("lblGoal");
        //    lblGoal.Font = FontManager.LoadFont("tahoma", 14);
        //    lblGoal.Size = new Size(this.Width - 20, 40);
        //    //lblGoal.Text = "Location: " + missionInfo.Goal;
        //    lblGoal.ForeColor = Color.WhiteSmoke;
        //    lblGoal.Location = new Point(lblMissionName.X, lblMissionName.Y + lblMissionName.Height + 5);

        //    btnAccept = new Button("btnAccept");
        //    btnAccept.Font = FontManager.LoadFont("tahoma", 12);
        //    btnAccept.Size = new Size(100, 20);
        //    btnAccept.Text = "Take Job";
        //    btnAccept.Location = new Point(Logic.Graphics.DrawingSupport.GetCenterX(this.Width, btnAccept.Width) - (btnAccept.Width / 2), this.Height - 40);
        //    Skins.SkinManager.LoadButtonGui(btnAccept);
        //    //btnAccept.Click += new EventHandler<MouseButtonEventArgs>(btnAccept_Click);

        //    btnClose = new Button("btnClose");
        //    btnClose.Font = FontManager.LoadFont("tahoma", 12);
        //    btnClose.Size = new Size(100, 20);
        //    btnClose.Text = "Close";
        //    btnClose.Location = new Point(Logic.Graphics.DrawingSupport.GetCenterX(this.Width, btnClose.Width) + (btnClose.Width / 2), this.Height - 40);
        //    Skins.SkinManager.LoadButtonGui(btnClose);
        //    //btnClose.Click += new EventHandler<MouseButtonEventArgs>(btnClose_Click);

        //    lblSlot = new Label("lblSlot");
        //    lblSlot.Font = FontManager.LoadFont("tahoma", 12);
        //    lblSlot.AutoSize = true;
        //    //lblSlot.Text = "Slot: " + missionInfo.Slot;
        //    lblSlot.ForeColor = Color.WhiteSmoke;
        //    lblSlot.Location = new Point(btnAccept.X - lblSlot.Width, btnAccept.Y);

        //    lblMissionCompletionEvent = new Label("lblMissionCompletionEvent");
        //    lblMissionCompletionEvent.Font = FontManager.LoadFont("tahoma", 14);
        //    lblMissionCompletionEvent.Size = new Size(this.Width - 20, 40);
        //    //lblMissionCompletionEvent.Text = "Goal: " + missionInfo.CompletionEvent;
        //    lblMissionCompletionEvent.ForeColor = Color.WhiteSmoke;
        //    lblMissionCompletionEvent.Location = new Point(lblGoal.X, lblGoal.Y + lblGoal.Height + 5);

        //    lblMissionReward = new Label("lblMissionReward");
        //    lblMissionReward.Font = FontManager.LoadFont("tahoma", 14);
        //    lblMissionReward.Size = new Size(this.Width - 20, 40);
        //    //lblMissionReward.Text = "Reward: " + missionInfo.Reward;
        //    lblMissionReward.ForeColor = Color.WhiteSmoke;
        //    lblMissionReward.Location = new Point(lblMissionCompletionEvent.X, lblMissionCompletionEvent.Y + lblMissionCompletionEvent.Height + 5);


        //    this.AddWidget(picCreator);
        //    this.AddWidget(lblCreatorName);
        //    this.AddWidget(lblDifficulty);
        //    this.AddWidget(lblMissionName);
        //    this.AddWidget(lblGoal);
        //    this.AddWidget(btnAccept);
        //    this.AddWidget(btnClose);
        //    //this.AddWidget(lblSlot);
        //    this.AddWidget(lblMissionCompletionEvent);
        //    this.AddWidget(lblMissionReward);

        //    LoadMissionsFromPacket(parse);

        //}

        //void btnClose_Click(object sender, MouseButtonEventArgs e) {
        //    MenuSwitcher.CloseAllMenus();
        //}

        //void btnAccept_Click(object sender, MouseButtonEventArgs e) {
        //    Messenger.SendAcceptMission(missionInfo.Slot);
        //    MenuSwitcher.CloseAllMenus();
        //}

        #endregion


    }
}