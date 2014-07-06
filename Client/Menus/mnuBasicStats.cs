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

    using SdlDotNet.Widgets;

    class mnuBasicStats : Logic.Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        #region Fields

        Label lblExp;
        Label lblHP;
        Label lblPlayerName;
        Label lblWeather;
        Label lblBelly;
        Label lblMissionExp;
        Label lblExplorerRank;

        #endregion Fields

        #region Constructors

        public mnuBasicStats(string name)
            : base(name)
        {
            this.Size = new Size(620, 200);
            //this.MenuDirection = Enums.MenuDirection.Horizontal;
            this.Location = new Point(10, 265);

            lblPlayerName = new Label("lblPlayerName");
            lblPlayerName.Location = new Point(30, 30);
            lblPlayerName.AutoSize = true;
            lblPlayerName.Font = FontManager.LoadFont("PMU", 32);
            lblPlayerName.ForeColor = Color.Yellow;
            lblPlayerName.Text = Players.PlayerManager.MyPlayer.Name;

            lblBelly = new Label("lblBelly");
            lblBelly.Location = new Point(30, 70);
            lblBelly.AutoSize = true;
            lblBelly.Font = FontManager.LoadFont("PMU", 32);
            lblBelly.Text = "Belly: " + Players.PlayerManager.MyPlayer.Belly + "/" + Players.PlayerManager.MyPlayer.MaxBelly;
            lblBelly.ForeColor = Color.WhiteSmoke;

            lblHP = new Label("lblHP");
            lblHP.Location = new Point(195, 30);
            lblHP.AutoSize = true;
            lblHP.Font = FontManager.LoadFont("PMU", 32);
            lblHP.Text = "HP: " + Players.PlayerManager.MyPlayer.GetActiveRecruit().HP + "/" + Players.PlayerManager.MyPlayer.GetActiveRecruit().MaxHP;
            lblHP.ForeColor = Color.WhiteSmoke;

            lblExp = new Label("lblExp");
            lblExp.Location = new Point(195, 70);
            lblExp.AutoSize = true;
            lblExp.Font = FontManager.LoadFont("PMU", 32);
            lblExp.Text = "Exp: " + Players.PlayerManager.MyPlayer.Exp + "/" + Players.PlayerManager.MyPlayer.MaxExp;
            lblExp.ForeColor = Color.WhiteSmoke;

            lblMissionExp = new Label("lblMissionExp");
            lblMissionExp.Location = new Point(30, 110);
            lblMissionExp.AutoSize = true;
            lblMissionExp.Font = FontManager.LoadFont("PMU", 32);
            lblMissionExp.Text = Missions.MissionManager.RankToString(Players.PlayerManager.MyPlayer.ExplorerRank) + " Rank (" + Players.PlayerManager.MyPlayer.MissionExp + " pts.)";
            lblMissionExp.ForeColor = Color.WhiteSmoke;
            
            lblExplorerRank = new Label("lblExplorerRank");
            lblExplorerRank.Location = new Point(360, 30);
            lblExplorerRank.AutoSize = true;
            lblExplorerRank.Font = FontManager.LoadFont("PMU", 32);
            lblExplorerRank.Text = "Rank: " + Players.PlayerManager.MyPlayer.ExplorerRank;
            lblExplorerRank.ForeColor = Color.WhiteSmoke;

            lblWeather = new Label("lblWeather");
            lblWeather.Location = new Point(360, 70);
            lblWeather.AutoSize = true;
            lblWeather.Font = FontManager.LoadFont("PMU", 32);
            lblWeather.Text = "Weather: Clear";// +Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Weather.ID.ToString();
            lblWeather.ForeColor = Color.WhiteSmoke;

            this.WidgetPanel.AddWidget(lblPlayerName);
            this.WidgetPanel.AddWidget(lblBelly);
            this.WidgetPanel.AddWidget(lblHP);
            this.WidgetPanel.AddWidget(lblExp);
            this.WidgetPanel.AddWidget(lblMissionExp);
            //this.WidgetPanel.AddWidget(lblExplorerRank);
            //this.WidgetPanel.AddWidget(lblWeather);
        }

        #endregion Constructors

        #region Properties

        public Logic.Widgets.BorderedPanel MenuPanel
        {
            get { return this; }
        }

        #endregion Properties
    }
}