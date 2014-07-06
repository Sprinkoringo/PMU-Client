namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlDotNet.Widgets;

    class mnuMainMenu : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields

        const int MAX_ITEMS = 4;

        Logic.Widgets.MenuItemPicker itemPicker;
        //Label lblGuild;
        Label lblItems;
        Label lblJobList;
        Label lblMoves;
        Label lblOthers;
        Label lblTeam;

        #endregion Fields

        #region Constructors

        public mnuMainMenu(string name)
            : base(name) {
            this.Size = new Size(135, 178);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            itemPicker = new Logic.Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblMoves = new Label("lblMoves");
            lblMoves.AutoSize = true;
            lblMoves.Location = new Point(30, 8);
            lblMoves.Font = FontManager.LoadFont("PMU", 32);
            lblMoves.Text = "Moves";
            lblMoves.HoverColor = Color.Red;
            lblMoves.ForeColor = Color.WhiteSmoke;
            lblMoves.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblMoves_Click);

            lblItems = new Label("lblItems");
            lblItems.AutoSize = true;
            lblItems.Location = new Point(30, 38);
            lblItems.Font = FontManager.LoadFont("PMU", 32);
            lblItems.Text = "Items";
            lblItems.HoverColor = Color.Red;
            lblItems.ForeColor = Color.WhiteSmoke;
            lblItems.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblItems_Click);

            lblTeam = new Label("lblTeam");
            lblTeam.AutoSize = true;
            lblTeam.Location = new Point(30, 68);
            lblTeam.Font = FontManager.LoadFont("PMU", 32);
            lblTeam.Text = "Team";
            lblTeam.HoverColor = Color.Red;
            lblTeam.ForeColor = Color.WhiteSmoke;
            lblTeam.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblTeam_Click);

            //lblGuild = new Label("lblGuild");
            //lblGuild.AutoSize = true;
            //lblGuild.Location = new Point(30, 98);
            //lblGuild.Font = FontManager.LoadFont("PMU", 32);
            //lblGuild.Text = "Guild";
            //lblGuild.HoverColor = Color.Red;
            //lblGuild.ForeColor = Color.WhiteSmoke;
            //lblGuild.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblGuild_Click);

            lblJobList = new Label("lblJobList");
            lblJobList.AutoSize = true;
            lblJobList.Location = new Point(30, 98);
            lblJobList.Font = FontManager.LoadFont("PMU", 32);
            lblJobList.Text = "Job List";
            lblJobList.HoverColor = Color.Red;
            lblJobList.ForeColor = Color.WhiteSmoke;
            lblJobList.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblJobList_Click);

            lblOthers = new Label("lblOthers");
            lblOthers.AutoSize = true;
            lblOthers.Location = new Point(30, 128);
            lblOthers.Font = FontManager.LoadFont("PMU", 32);
            lblOthers.Text = "Others";
            lblOthers.HoverColor = Color.Red;
            lblOthers.ForeColor = Color.WhiteSmoke;
            lblOthers.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblOthers_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblMoves);
            this.AddWidget(lblItems);
            this.AddWidget(lblTeam);
            //this.AddWidget(lblGuild);
            this.AddWidget(lblJobList);
            this.AddWidget(lblOthers);
        }

        void lblMoves_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        void lblItems_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }

        void lblTeam_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(2);
        }

        //void lblGuild_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
        //    SelectItem(3);
        //}

        void lblJobList_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(3);
        }

        void lblOthers_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(4);
        }

        #endregion Constructors

        #region Properties

        public Logic.Widgets.BorderedPanel MenuPanel {
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
                        MenuSwitcher.ShowMovesMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 1: {
                        MenuSwitcher.ShowInventoryMenu(1);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 2: {
                        MenuSwitcher.ShowTeamMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                //case 3: {
                //        MenuSwitcher.ShowGuildMenu();
                //        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                //    }
                //    break;
                case 3: {
                        MenuSwitcher.ShowJobListMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 4: {
                        MenuSwitcher.ShowOthersMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        #endregion Methods
    }
}