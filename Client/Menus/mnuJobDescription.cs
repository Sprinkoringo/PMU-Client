namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;
    using Client.Logic.Missions;
    using Client.Logic.Network;

    class mnuJobDescription : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        #region Fields

        Label lblDescription;
        PictureBox picCreator;
        Label lblCreatorName;
        Label lblNullTitle;
        Label lblTitle;
        Label lblSummary;
        Label lblObjective;
        Label lblGoal;
        Label lblDifficulty;
        PictureBox picReward;
        Label lblReward;
        Label lblPressEnter;


        #endregion Fields

        #region Constructors

        public mnuJobDescription(string name, Job job, bool missionBoardView)
            : base(name) {

            base.Size = new Size(300, 460);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            if (missionBoardView) {
                base.Location = new Point(305, 10);
            } else {
                base.Location = new Point(160, 10);
            }

            lblDescription = new Label("lblDescription");
            lblDescription.Font = FontManager.LoadFont("PMU", 48);
            lblDescription.AutoSize = true;
            lblDescription.Text = "Description";
            lblDescription.ForeColor = Color.WhiteSmoke;
            lblDescription.Location = new Point(20, 0);

            lblNullTitle = new Label("lblNullTitle");
            lblNullTitle.Font = FontManager.LoadFont("PMU", 32);
            lblNullTitle.Size = new Size(this.Width - 20, 40);
            lblNullTitle.ForeColor = Color.Gray;
            lblNullTitle.Text = "-----";
            lblNullTitle.Location = new Point(20, 50);

            picCreator = new PictureBox("picCreator");
            picCreator.Size = new Size(40, 40);
            picCreator.Location = new Point(30, 50);
            picCreator.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            picCreator.BorderWidth = 1;

            lblTitle = new Label("lblTitle");
            lblTitle.Font = FontManager.LoadFont("PMU", 16);
            lblTitle.AutoSize = true;
            lblTitle.ForeColor = Color.WhiteSmoke;
            lblTitle.Location = new Point(picCreator.X + picCreator.Width + 10, picCreator.Y);

            lblCreatorName = new Label("lblCreatorName");
            lblCreatorName.Font = FontManager.LoadFont("PMU", 16);
            lblCreatorName.AutoSize = true;
            lblCreatorName.ForeColor = Color.WhiteSmoke;
            lblCreatorName.Location = new Point(picCreator.X + picCreator.Width + 10, picCreator.Y + 20);

            lblSummary = new Label("lblSummary");
            lblSummary.Font = FontManager.LoadFont("PMU", 16);
            lblSummary.Size = new Size(this.Width - 50, 120);
            lblSummary.ForeColor = Color.WhiteSmoke;
            lblSummary.Location = new Point(lblNullTitle.X, picCreator.Y + picCreator.Height + 4);

            lblObjective = new Label("lblObjective");
            lblObjective.Font = FontManager.LoadFont("PMU", 16);
            lblObjective.Size = new Size(this.Width - 20, 40);
            lblObjective.ForeColor = Color.WhiteSmoke;
            lblObjective.Location = new Point(lblNullTitle.X, lblNullTitle.Y + lblNullTitle.Height + 5);

            lblGoal = new Label("lblGoal");
            lblGoal.Font = FontManager.LoadFont("PMU", 16);
            lblGoal.Size = new Size(this.Width - 20, 40);
            lblGoal.ForeColor = Color.WhiteSmoke;
            lblGoal.Location = new Point(lblNullTitle.X, lblSummary.Y + lblSummary.Height + 5);

            lblDifficulty = new Label("lblDifficulty");
            lblDifficulty.Font = FontManager.LoadFont("PMU", 16);
            lblDifficulty.AutoSize = true;
            lblDifficulty.ForeColor = Color.WhiteSmoke;
            lblDifficulty.Location = new Point(lblNullTitle.X, lblGoal.Y + 20);

            picReward = new PictureBox("picReward");
            picReward.Size = new Size(32, 32);
            picReward.BackColor = Color.Transparent;
            picReward.Location = new Point(lblNullTitle.X, lblDifficulty.Y + 24);
            

            lblReward = new Label("lblMissionReward");
            lblReward.Font = FontManager.LoadFont("PMU", 16);
            lblReward.Size = new Size(this.Width - 20, 40);
            lblReward.ForeColor = Color.WhiteSmoke;
            lblReward.Location = new Point(lblNullTitle.X + 32, lblDifficulty.Y + 20);

            lblPressEnter = new Label("lblPressEnter");
            lblPressEnter.Font = FontManager.LoadFont("PMU", 32);
            lblPressEnter.Size = new Size(this.Width - 20, 40);
            lblPressEnter.ForeColor = Color.WhiteSmoke;
            lblPressEnter.Text = "Press Enter to take this job.";
            lblPressEnter.Location = new Point(lblNullTitle.X, this.Height - 50);

            this.AddWidget(lblDescription);
            this.AddWidget(picCreator);
            this.AddWidget(lblTitle);
            this.AddWidget(lblCreatorName);
            this.AddWidget(lblNullTitle);
            this.AddWidget(lblSummary);
            //this.AddWidget(lblObjective);
            this.AddWidget(lblGoal);
            this.AddWidget(lblDifficulty);
            this.AddWidget(picReward);
            this.AddWidget(lblReward);
            if (missionBoardView) {
                this.AddWidget(lblPressEnter);
            }

            UpdateJob(job);
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

        public void UpdateJob(Job job) {
            if (job == null) {
                lblDifficulty.Visible = false;
                picCreator.Visible = false;
                lblCreatorName.Visible = false;
                lblNullTitle.Visible = true;
                lblTitle.Visible = false;
                lblSummary.Visible = false;
                lblGoal.Visible = false;
                picReward.Visible = false;
                lblReward.Visible = false;
                lblPressEnter.Visible = false;
            } else {
                lblDifficulty.Visible = true;
                lblDifficulty.Text = "Difficulty: " + MissionManager.DifficultyToString(job.Difficulty) + " (" + MissionManager.DetermineMissionExpReward(job.Difficulty) + " Explorer Points)";
                picCreator.Visible = true;
                picCreator.Image = Logic.Graphics.GraphicsManager.GetMugshot(job.ClientSpecies, job.ClientForm, 0, 0).GetEmote(0);//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((this.creatorMugshot % 15) * 40, (this.creatorMugshot / 15) * 40, 40, 40));
                lblTitle.Visible = true;
                lblTitle.Text = "Title: " + job.Title;
                lblCreatorName.Visible = true;
                lblCreatorName.Text = "From: " + Pokedex.PokemonHelper.Pokemon[job.ClientSpecies-1].Name;
                lblNullTitle.Visible = false;
                lblSummary.Visible = true;
                lblSummary.Text = "Summary: \n" + job.Summary;
                
                lblGoal.Visible = true;
                lblGoal.Text = "Place: " + job.GoalName;
                picReward.Visible = true;
                picReward.Image = Tools.CropImage(GraphicsManager.Items, new Rectangle((Items.ItemHelper.Items[job.RewardNum].Pic - (int)(Items.ItemHelper.Items[job.RewardNum].Pic / 6) * 6) * Constants.TILE_WIDTH, (int)(Items.ItemHelper.Items[job.RewardNum].Pic / 6) * Constants.TILE_WIDTH, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                lblReward.Visible = true;
                if (Items.ItemHelper.Items[job.RewardNum].StackCap > 0) {
                    lblReward.Text = "Reward:\n" + job.RewardAmount + " " + Items.ItemHelper.Items[job.RewardNum].Name;
                } else {
                    lblReward.Text = "Reward:\n" + Items.ItemHelper.Items[job.RewardNum].Name;
                }
                lblPressEnter.Visible = true;
            }
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.Backspace: {
                        Menus.MenuSwitcher.ShowJobListMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        Menus.MenuSwitcher.ShowJobListMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        #endregion Methods
    }
}