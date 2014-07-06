using System;
using System.Collections.Generic;
using System.Text;

using System.Drawing;
using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winCredits : Core.WindowCore
    {
        Label lblTeam;
        Label lblBack;
        Label lblProgramming;
        Label lblPikablu;
        Label lblSprinko;
        Label lblDarkmazer;
        Label lblRocket;
        Label lblGraphics;
        Label lblSprinkoAgain;
        Label lblFlare;
        Label lblWolfLink;
        Label lblHayarotle;
        Label lblRhaenn;
        Label lblTerranariko;
        Label lblKirk;

        public winCredits()
            : base("WinCredits") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Credits";
            this.TitleBar.CloseButton.Visible = false;
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Credits");
            //this.Size = this.BackgroundImage.Size;
            this.BackColor = Color.White;
            this.Size = new Size(400, 400);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            lblTeam = new Label("lblTeam");
            lblTeam.Font = Graphics.FontManager.LoadFont("PMU", 30);
            lblTeam.AutoSize = true;
            lblTeam.Location = new Point(30, 20);
            lblTeam.Text = "The Pokemon Mystery Universe 7 Team!";
            lblTeam.BackColor = Color.GreenYellow;

            lblBack = new Label("lblBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblBack.AutoSize = true;
            lblBack.Location = new Point(0, 330);
            lblBack.Text = "Return to Login Screen";
            lblBack.BackColor = Color.Blue;
            lblBack.Click +=new EventHandler<MouseButtonEventArgs>(lblBack_Click);

            lblProgramming = new Label("lblProgramming");
            lblProgramming.Font = Graphics.FontManager.LoadFont("PMU", 25);
            lblProgramming.AutoSize = true;
            lblProgramming.Location = new Point(0, 80);
            lblProgramming.Text = "Programming:";
            lblProgramming.BackColor = Color.Silver;

            lblPikablu = new Label("lblPikablu");
            lblPikablu.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblPikablu.AutoSize = true;
            lblPikablu.Location = new Point(120, 85);
            lblPikablu.Text = "Pikablu";
            lblPikablu.BackColor = Color.Yellow;

            lblDarkmazer = new Label("lblDarkmazer");
            lblDarkmazer.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblDarkmazer.AutoSize = true;
            lblDarkmazer.Location = new Point(175, 85);
            lblDarkmazer.Text = "Darkmazer";
            // TODO: Add Forecolors for the Credits.
            //lblDarkmazer.BackColor = Color.Black; -For the Darkness, as always.
            //lblDarkmazer.ForeColor = Color.White; -Even in the deepest of darkness, there's always light...

            lblSprinko = new Label("lblSprinko");
            lblSprinko.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblSprinko.AutoSize = true;
            lblSprinko.Location = new Point(250, 85);
            lblSprinko.Text = "Sprinko";
            lblSprinko.BackColor = Color.LightBlue;

            lblRocket = new Label("lblRocket");
            lblRocket.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblRocket.AutoSize = true;
            lblRocket.Location = new Point(310, 85);
            lblRocket.Text = "Rocket";
            lblRocket.BackColor = Color.Orange;

            lblGraphics = new Label("lblGraphics");
            lblGraphics.Font = Graphics.FontManager.LoadFont("PMU", 25);
            lblGraphics.AutoSize = true;
            lblGraphics.Location = new Point(0, 140);
            lblGraphics.Text = "Graphics:";
            lblGraphics.BackColor = Color.Brown;

            lblSprinkoAgain = new Label("lblSprinkoAgain");
            lblSprinkoAgain.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblSprinkoAgain.AutoSize = true;
            lblSprinkoAgain.Location = new Point(90, 145);
            lblSprinkoAgain.Text = "Sprinko";
            lblSprinkoAgain.BackColor = Color.SkyBlue;

            lblFlare = new Label("lblFlare");
            lblFlare.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblFlare.AutoSize = true;
            lblFlare.Location = new Point(130, 185);
            lblFlare.Text = "Flare";
            //lblFlare.BackColor = Color.Black; -Shiny, shiny!
            //lblFlare.ForeColor = Color.Silver; -Isn't Silver shiny though? Doesn't it count?

            lblWolfLink = new Label("lblWolfLink");
            lblWolfLink.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblWolfLink.AutoSize = true;
            lblWolfLink.Location = new Point(160, 145);
            lblWolfLink.Text = "Wolf Link";
            lblWolfLink.BackColor = Color.Tan;

            lblRhaenn = new Label("lblRhaenn");
            lblRhaenn.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblRhaenn.AutoSize = true;
            lblRhaenn.Location = new Point(200, 185);
            lblRhaenn.Text = "Rhaenn";
            lblRhaenn.BackColor = Color.MediumBlue;

            lblHayarotle = new Label("lblHayarotle");
            lblHayarotle.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblHayarotle.AutoSize = true;
            lblHayarotle.Location = new Point(245, 145);
            lblHayarotle.Text = "Hayarotle";
            lblHayarotle.BackColor = Color.Red;

            lblTerranariko = new Label("lblTerranariko");
            lblTerranariko.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblTerranariko.AutoSize = true;
            lblTerranariko.Location = new Point(270, 185);
            lblTerranariko.Text = "Terranariko";
            lblTerranariko.BackColor = Color.Pink;

            lblKirk = new Label("lblKirk");
            lblKirk.Font = Graphics.FontManager.LoadFont("PMU", 20);
            lblKirk.AutoSize = true;
            lblKirk.Location = new Point(90, 225);
            lblKirk.Text = "Kirk";
            lblKirk.BackColor = Color.Transparent;

            this.AddWidget(lblTeam);
            this.AddWidget(lblBack);
            this.AddWidget(lblProgramming);
            this.AddWidget(lblPikablu);
            this.AddWidget(lblSprinko);
            this.AddWidget(lblDarkmazer);
            this.AddWidget(lblRocket);
            this.AddWidget(lblGraphics);
            this.AddWidget(lblSprinkoAgain);
            this.AddWidget(lblFlare);
            this.AddWidget(lblWolfLink);
            this.AddWidget(lblHayarotle);
            this.AddWidget(lblRhaenn);
            this.AddWidget(lblTerranariko);
            this.AddWidget(lblKirk);
            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowMainMenu();
        }
    }
}
