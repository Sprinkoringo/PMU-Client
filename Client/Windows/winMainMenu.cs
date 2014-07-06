namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = Client.Logic.Graphics;
    //using Gui = Client.Logic.Gui;
    using SdlDotNet.Widgets;
    using Client.Logic.Skins;
    using Client.Logic.Network;

    class winMainMenu : Core.WindowCore
    {
        #region Fields

        Button btnNewAccount;
        Button btnDeleteAccount;
        Button btnAccountSettings;
        Button btnOptions;
        Button btnSkins;
        Button btnHelp;
        Button btnCredits;
        Button btnSelectServer;
        Panel pnlSelectServer;
        PictureBox pixMainServer;
        Label lblMainServer;
        PictureBox pixDebugServer;
        Label lblDebugServer;
        Button btnExit;

        #endregion Fields

        #region Constructors

        public winMainMenu()
            : base("winMainMenu") {

            //this.BackColor = Color.Transparent;

            //this.TitleBar.Text = "Main Menu";
            this.Windowed = true;
            this.Location = new Point(405, 160);//Gfx.DrawingSupport.GetCenter(this.Size);
            this.Size = new Size(390, 300);
            this.TitleBar.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.TitleBar.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "titlebar.png");
            this.TitleBar.CloseButton.Visible = false;
            this.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.BackgroundImage = SkinManager.LoadGui("Main Menu");

            btnNewAccount = new Button("btnNewAccount");
            btnNewAccount.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnNewAccount.Location = new Point(252, 4);
            btnNewAccount.Size = new System.Drawing.Size(134, 32);
            btnNewAccount.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnNewAccount.Selected = false;
            btnNewAccount.BackColor = Color.Transparent;
            btnNewAccount.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/newaccount.png");
            btnNewAccount.HighlightType = HighlightType.Image;
            btnNewAccount.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/newaccount-h.png");
            btnNewAccount.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnNewAccount_Click);

            btnDeleteAccount = new Button("btnDeleteAccount");
            btnDeleteAccount.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnDeleteAccount.Location = new Point(252, 38);
            btnDeleteAccount.Size = new System.Drawing.Size(134, 32);
            btnDeleteAccount.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnDeleteAccount.Selected = false;
            btnDeleteAccount.BackColor = Color.Transparent;
            btnDeleteAccount.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/delaccount.png");
            btnDeleteAccount.HighlightType = HighlightType.Image;
            btnDeleteAccount.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/delaccount-h.png");
            btnDeleteAccount.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnDeleteAccount_Click);

            btnAccountSettings = new Button("btnAccountSettings");
            btnAccountSettings.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnAccountSettings.Location = new Point(252, 72);
            btnAccountSettings.Size = new System.Drawing.Size(134, 32);
            btnAccountSettings.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnAccountSettings.Selected = false;
            btnAccountSettings.BackColor = Color.Transparent;
            btnAccountSettings.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/accountsettings.png");
            btnAccountSettings.HighlightType = HighlightType.Image;
            btnAccountSettings.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/accountsettings-h.png");
            btnAccountSettings.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnAccountSettings_Click);

            btnOptions = new Button("btnOptions");
            btnOptions.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnOptions.Location = new Point(252, 106);
            btnOptions.Size = new System.Drawing.Size(134, 32);
            btnOptions.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnOptions.Selected = false;
            btnOptions.BackColor = Color.Transparent;
            btnOptions.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/options.png");
            btnOptions.HighlightType = HighlightType.Image;
            btnOptions.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/options-h.png");
            btnOptions.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnOptions_Click);

            btnSkins = new Button("btnSkins");
            btnSkins.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnSkins.Location = new Point(252, 140);
            btnSkins.Size = new System.Drawing.Size(134, 32);
            btnSkins.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnSkins.Selected = false;
            btnSkins.BackColor = Color.Transparent;
            btnSkins.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/skins.png");
            btnSkins.HighlightType = HighlightType.Image;
            btnSkins.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/skins-h.png");
            btnSkins.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnSkins_Click);

            btnHelp = new Button("btnHelp");
            btnHelp.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnHelp.Location = new Point(252, 174);
            btnHelp.Size = new System.Drawing.Size(134, 32);
            btnHelp.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnHelp.Selected = false;
            btnHelp.BackColor = Color.Transparent;
            btnHelp.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/help.png");
            btnHelp.HighlightType = HighlightType.Image;
            btnHelp.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/help-h.png");
            btnHelp.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnHelp_Click);

            btnCredits = new Button("btnCredits");
            btnCredits.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnCredits.Location = new Point(252, 208);
            btnCredits.Size = new System.Drawing.Size(134, 32);
            btnCredits.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnCredits.Selected = false;
            btnCredits.BackColor = Color.Transparent;
            btnCredits.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/credits.png");
            btnCredits.HighlightType = HighlightType.Image;
            btnCredits.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/credits-h.png");
            btnCredits.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnCredits_Click);

            btnSelectServer = new Button("btnSelectServer");
            btnSelectServer.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnSelectServer.Location = new Point(10, 242);
            btnSelectServer.Size = new System.Drawing.Size(134, 32);
            btnSelectServer.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnSelectServer.Selected = false;
            btnSelectServer.BackColor = Color.Transparent;
            btnSelectServer.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/serverselect.png");
            btnSelectServer.HighlightType = HighlightType.Image;
            btnSelectServer.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/serverselect-h.png");
            btnSelectServer.Click += new EventHandler<MouseButtonEventArgs>(btnSelectServer_Click);

            pnlSelectServer = new Panel("pnlSelectServer");
            pnlSelectServer.Location = new Point(0, 0);
            pnlSelectServer.Size = new Size(this.Width - btnHelp.Width - 5, this.Height - btnSelectServer.Height - 10);
            pnlSelectServer.Visible = false;
            pnlSelectServer.BackColor = Color.Transparent;

            pixMainServer = new PictureBox("pixMainServer");
            pixMainServer.Location = new Point(10, 20);
            pixMainServer.Size = new System.Drawing.Size(32, 32);
            pixMainServer.BackColor = Color.Green;
            pixMainServer.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pixMainServer.BorderWidth = 1;
            pixMainServer.Click += new EventHandler<MouseButtonEventArgs>(pixServer1_Click);

            lblMainServer = new Label("lblMainServer");
            lblMainServer.AutoSize = true;
            lblMainServer.Text = "Main Server";
            lblMainServer.Location = new Point(pixMainServer.X + pixMainServer.Width + 5, pixMainServer.Y + (pixMainServer.Height / 2) - (lblMainServer.Height / 2));
            lblMainServer.Font = Gfx.FontManager.LoadFont("PMU", 24);

#if DEBUG
            pixDebugServer = new PictureBox("pixDebugServer");
            pixDebugServer.Location = new Point(10, pixMainServer.Y + pixMainServer.Height + 10);
            pixDebugServer.Size = new System.Drawing.Size(32, 32);
            pixDebugServer.BackColor = Color.Green;
            pixDebugServer.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pixDebugServer.BorderWidth = 1;
            pixDebugServer.Click += new EventHandler<MouseButtonEventArgs>(pixDebugServer_Click);

            lblDebugServer = new Label("lblDebugServer");
            lblDebugServer.AutoSize = true;
            lblDebugServer.Text = "Debug Server";
            lblDebugServer.Location = new Point(pixDebugServer.X + pixDebugServer.Width + 5, pixDebugServer.Y + (pixDebugServer.Height / 2) - (pixDebugServer.Height / 2));
            lblDebugServer.Font = Gfx.FontManager.LoadFont("PMU", 24);
#endif

            btnExit = new Button("btnExit");
            btnExit.Font = Gfx.FontManager.LoadFont("PMU", 24);
            btnExit.Location = new Point(252, 242);
            btnExit.Size = new System.Drawing.Size(134, 32);
            btnExit.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnExit.Selected = false;
            btnExit.BackColor = Color.Transparent;
            btnExit.BackgroundImage = SkinManager.LoadGuiElement("Main Menu", "buttons/exit.png");
            btnExit.HighlightType = HighlightType.Image;
            btnExit.HighlightSurface = SkinManager.LoadGuiElement("Main Menu", "buttons/exit-h.png");
            btnExit.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnExit_Click);

            //this.Alpha = 155;
            pnlSelectServer.AddWidget(pixMainServer);
            pnlSelectServer.AddWidget(lblMainServer);
#if DEBUG
            pnlSelectServer.AddWidget(pixDebugServer);
            pnlSelectServer.AddWidget(lblDebugServer);
#endif

            this.AddWidget(btnNewAccount);
            this.AddWidget(btnDeleteAccount);
            this.AddWidget(btnAccountSettings);
            this.AddWidget(btnOptions);
            this.AddWidget(btnSkins);
            this.AddWidget(btnHelp);
            this.AddWidget(btnCredits);
            this.AddWidget(btnSelectServer);
            this.AddWidget(pnlSelectServer);
            this.AddWidget(btnExit);

            this.LoadComplete();
        }

#if DEBUG
        void pixDebugServer_Click(object sender, MouseButtonEventArgs e) {
            IO.Options.ConnectionIP = "localhost";
            IO.Options.ConnectionPort = 5001;
            IO.Options.SaveXml();

            pnlSelectServer.Visible = false;
            btnSelectServer.Selected = false;

            ((winUpdates)WindowManager.FindWindow("winUpdates")).ClearNews();
            NetworkManager.Disconnect();
            NetworkManager.Connect();
        }
#endif

        void pixServer1_Click(object sender, MouseButtonEventArgs e) {
            IO.Options.ConnectionIP = "game.pmuniverse.net";
            IO.Options.ConnectionPort = 4001;
            IO.Options.SaveXml();

            pnlSelectServer.Visible = false;
            btnSelectServer.Selected = false;

            ((winUpdates)WindowManager.FindWindow("winUpdates")).ClearNews();
            NetworkManager.Disconnect();
            NetworkManager.Connect();
        }

        #endregion Constructors

        #region Methods

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.Z: {
                        //Music.Music.AudioPlayer.FadeOut(1000);
                    }
                    break;
                //case SdlDotNet.Input.Key.X: {
                //        Music.Music.AudioPlayer.FadeIn(2, 1000);
                //    }
                //    break;
                case SdlDotNet.Input.Key.C: {
                        //Music.Music.AudioPlayer.FadeOut(10000);
                        //MessageBox.Show("Hello!", "Test!");
                        //audio.LoadStreamFromUrl(@"http://www.pmuniverse.net/PMU/PMU%207/Music/GSC)%20Wild%20Battle.mid");
                        //audio.LoadStreamFromUrl(@"http://www.pmuniverse.net/PMU/PMU%207/Music/PMD)%20Dream.ogg");
                        //audio.PlayMusic(1);
                        //audio.Audio(@"http://www.pmuniverse.net/PMU/PMU 7/Music/Anime) Because the Sky is There (Karaoke).ogg");
                        //audio.Audio(@"http://www.pmuniverse.net/PMU/PMU 7/Music/PMD) Dream.ogg");
                        //audio.PlayMusic();
                        //Music.Music.AudioPlayer.FadeInPosition(2, 1000, 100);
                    }
                    break;
                case SdlDotNet.Input.Key.F11: {
                        SdlDotNet.Graphics.Video.SetVideoMode(SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height, false, false, !SdlDotNet.Graphics.Video.Screen.FullScreen);
                    }
                    break;
            }
        }

        void btnNewAccount_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winNewAccount());
        }

        void btnDeleteAccount_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winDeleteAccount());
        }

        void btnAccountSettings_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winAccountSettings());
        }

        void btnOptions_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winOptions());
        }

        void btnSkins_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winSkinSelector());
        }

        void btnHelp_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

        }

        void btnCredits_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.FindWindow("winUpdates").Close();
            WindowManager.FindWindow("winLogin").Close();
            WindowSwitcher.AddWindow(new winCredits());
        }

        void btnSelectServer_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnSelectServer.Selected) {
                btnSelectServer.Selected = true;
                pnlSelectServer.Visible = true;
            } else {
                pnlSelectServer.Visible = false;
                btnSelectServer.Selected = false;
            }
        }

        void btnExit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Sdl.SdlCore.QuitApplication();
        }

        #endregion Methods


    }
}