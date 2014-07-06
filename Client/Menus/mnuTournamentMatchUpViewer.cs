namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using SdlDotNet.Widgets;
    using System.Drawing;
    using Client.Logic.Graphics;

    class mnuTournamentMatchUpViewer : Client.Logic.Widgets.BorderedPanel, Core.IMenu
    {

        Label lblLeftArrow;
        Label lblRightArrow;
        PictureBox pbxPlayerOneMugshot;
        PictureBox pbxPlayerTwoMugshot;
        Label lblVSLabel;
        Label lblPlayerOneName;
        Label lblPlayerTwoName;


        #region Properties

        public Client.Logic.Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }

        #endregion Properties

        public mnuTournamentMatchUpViewer(string name)
            : base(name) {

            this.Size = new System.Drawing.Size(300, 400);

            lblLeftArrow = new Label("lblLeftArrow");
            lblLeftArrow.AutoSize = false;
            lblLeftArrow.Size = new System.Drawing.Size(50, 35);
            lblLeftArrow.Font = FontManager.LoadFont("PMU", 32);
            lblLeftArrow.Text = "<";
            lblLeftArrow.Location = new Point(5);

            lblRightArrow = new Label("lblRightArrow");
            lblRightArrow.AutoSize = false;
            lblRightArrow.Size = new System.Drawing.Size(50, 35);
            lblRightArrow.Font = FontManager.LoadFont("PMU", 32);
            lblRightArrow.Text = ">";
            lblRightArrow.Location = new Point(this.Width - lblRightArrow.Width - 5);

            pbxPlayerOneMugshot = new PictureBox("pbxPlayerOneMugshot");
            pbxPlayerOneMugshot.Size = new Size(40, 40);
            pbxPlayerOneMugshot.Location = new Point(lblLeftArrow.X + lblLeftArrow.Width + 5, 5);

            lblVSLabel = new Label("lblVSLabel");
            lblVSLabel.Font = FontManager.LoadFont("PMU", 32);
            lblVSLabel.AutoSize = false;
            lblVSLabel.Size = new Size(100, 35);
            lblVSLabel.Location = new Point(pbxPlayerOneMugshot.X + pbxPlayerOneMugshot.Width + 5, pbxPlayerOneMugshot.Y + pbxPlayerOneMugshot.Height - lblVSLabel.Height);

            pbxPlayerTwoMugshot = new PictureBox("pbxPlayerTwoMugshot");
            pbxPlayerTwoMugshot.Size = new Size(40, 40);
            pbxPlayerTwoMugshot.Location = new Point(lblVSLabel.X + lblVSLabel.Width + 5, pbxPlayerOneMugshot.Y);

            lblPlayerOneName = new Label("lblPlayerOneName");
            lblPlayerOneName.Font = FontManager.LoadFont("PMU", 24);
            lblPlayerOneName.Location = new Point(pbxPlayerOneMugshot.X, pbxPlayerOneMugshot.Y + pbxPlayerOneMugshot.Height + 5);

            lblPlayerTwoName = new Label("lblPlayerTwoName");
            lblPlayerTwoName.Font = FontManager.LoadFont("PMU", 24);
            lblPlayerTwoName.Location = new Point(pbxPlayerTwoMugshot.X, pbxPlayerTwoMugshot.Y + pbxPlayerTwoMugshot.Height + 5);

        }

        public void LoadMatchUpsFromPacket(string[] parse) {
            int n;
        }
    }
}