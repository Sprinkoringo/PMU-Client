using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuHelpPage : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        Label lblHelpPage;
        Label lblPageNumber;
        Button btnShowHelp;
        PictureBox picHelpPage;
        string helpFolder;
        int page;


        #endregion Fields

        #region Constructors

        public mnuHelpPage(string name, string helpFolder, int page)
            : base(name) {
            this.Size = new Size(620, 420);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            this.page = page;
            this.helpFolder = helpFolder;

            lblHelpPage = new Label("lblHelpTopics");
            lblHelpPage.Location = new Point(40, 5);
            lblHelpPage.Font = FontManager.LoadFont("PMU", 36);
            lblHelpPage.AutoSize = true;
            lblHelpPage.Text = helpFolder;
            lblHelpPage.ForeColor = Color.WhiteSmoke;

            lblPageNumber = new Label("lblPageNumber");
            lblPageNumber.AutoSize = true;
            lblPageNumber.Font = FontManager.LoadFont("PMU", 36);
            lblPageNumber.Text = "Page 1";
            lblPageNumber.Location = new Point(this.Width - lblPageNumber.Width - 40, 5);
            lblPageNumber.ForeColor = Color.WhiteSmoke;

            picHelpPage = new PictureBox("picHelpPage");
            picHelpPage.SizeMode = ImageSizeMode.StretchImage;
            picHelpPage.Location = new Point(50, 50);
            picHelpPage.Size = new Size(this.Width - (picHelpPage.X * 2), this.Height - picHelpPage.Y -20);
            picHelpPage.BackColor = Color.Green;

            //lstHelpTopics = new ListBox("lstHelpTopics");
            //lstHelpTopics.Location = new Point(10, 50);
            //lstHelpTopics.Size = new Size(this.Width - lstHelpTopics.X * 2, this.Height - lstHelpTopics.Y - 10);
            //lstHelpTopics.BackColor = Color.Transparent;
            //lstHelpTopics.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;

            this.AddWidget(lblHelpPage);
            this.AddWidget(picHelpPage);
            this.AddWidget(lblPageNumber);

            LoadHelpPage(this.page);
        }

        void LoadHelpPage(int page) {
            if (System.IO.File.Exists(IO.Paths.StartupPath + "Help/" + helpFolder + "/" + "page" + (page + 1).ToString() + ".png")) {
                picHelpPage.Image = SurfaceManager.LoadSurface(IO.Paths.StartupPath + "Help/" + helpFolder + "/" + "page" + (page + 1).ToString() + ".png", true, false);
                lblPageNumber.Text = "Page " + (page + 1).ToString();
                lblPageNumber.Location = new Point(this.Width - lblPageNumber.Width - 40, 5);
            }
        }

        #endregion Constructors
        #region Methods

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.LeftArrow: {
                        if (page > 0) {
                            page--;
                            LoadHelpPage(page);
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow: {
                        if (System.IO.File.Exists(IO.Paths.StartupPath + "Help/" + helpFolder + "/" + "page" + (page + 1).ToString() + ".png")) {
                            page++;
                            LoadHelpPage(page);
                            Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                        }
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        // Show the otherackspace key is pressed
                        MenuSwitcher.ShowHelpMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
