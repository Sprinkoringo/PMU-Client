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
using PMU.Core;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuAdventureLog : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal
        {
            get;
            set;
        }

        #region Fields

        Panel pnlGeneral;
        Panel pnlDungeons;
        Panel pnlPokedex;
        Label lblAdventureLog;
        Label lblPage;
        int page;
        bool loaded;

        #region Page1
        Label lblGeneral;
        Label lblPlayerName;
        Label lblRank;
        Label lblPlayTime;
        Label lblDungeonsCompleted;
        Label lblMissionsCompleted;
        //Label lblRescuesCompleted;
        #endregion

        #region Page2
        Label lblDungeons;
        Label lblEntered;
        Label lblCompleted;
        ListBox lbxDungeonList;

        #endregion

        #region Page3
        Label lblPokedex;
        Label lblDefeated;
        Label lblJoined;
        ListBox lbxPokedex;

        #endregion

        #endregion Fields

        #region Constructors

        public mnuAdventureLog(string name)
            : base(name)
        {
            this.Size = new Size(200, 260);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            pnlGeneral = new Panel("pnlGeneral");
            pnlGeneral.Size = new System.Drawing.Size(200, 220);
            pnlGeneral.Location = new Point(0, 40);
            pnlGeneral.BackColor = Color.Transparent;
            pnlGeneral.Visible = false;

            pnlDungeons = new Panel("pnlDungeons");
            pnlDungeons.Size = new System.Drawing.Size(200, 220);
            pnlDungeons.Location = new Point(0, 40);
            pnlDungeons.BackColor = Color.Transparent;
            pnlDungeons.Visible = false;

            pnlPokedex = new Panel("pnlPokedex");
            pnlPokedex.Size = new System.Drawing.Size(200, 220);
            pnlPokedex.Location = new Point(0, 40);
            pnlPokedex.BackColor = Color.Transparent;
            pnlPokedex.Visible = false;

            lblAdventureLog = new Label("lblAdventureLog");
            lblAdventureLog.Location = new Point(20, 0);
            lblAdventureLog.Font = FontManager.LoadFont("PMU", 48);
            lblAdventureLog.AutoSize = true;
            lblAdventureLog.Text = "Profile";
            lblAdventureLog.ForeColor = Color.WhiteSmoke;

            page = 0;
            lblPage = new Label("lblPage");
            lblPage.Location = new Point(140, 20);
            lblPage.Font = FontManager.LoadFont("PMU", 16);
            lblPage.AutoSize = true;
            lblPage.Text = "Loading...";
            lblPage.ForeColor = Color.WhiteSmoke;



            #region General
            lblGeneral = new Label("lblGeneral");
            lblGeneral.Location = new Point(20, 10);
            lblGeneral.Font = FontManager.LoadFont("PMU", 32);
            lblGeneral.AutoSize = true;
            lblGeneral.Text = "General";
            lblGeneral.ForeColor = Color.WhiteSmoke;

            lblPlayerName = new Label("lblPlayerName");
            lblPlayerName.Location = new Point(20, 40);
            lblPlayerName.Font = FontManager.LoadFont("PMU", 16);
            lblPlayerName.AutoSize = true;
            //lblPlayerName.Text = "Name: ";
            lblPlayerName.ForeColor = Color.WhiteSmoke;

            lblRank = new Label("lblRank");
            lblRank.Location = new Point(20, 60);
            lblRank.Font = FontManager.LoadFont("PMU", 16);
            lblRank.AutoSize = true;
            //lblRank.Text = "Rank: ";
            lblRank.ForeColor = Color.WhiteSmoke;

            lblPlayTime = new Label("lblPlayTime");
            lblPlayTime.Location = new Point(20, 80);
            lblPlayTime.Font = FontManager.LoadFont("PMU", 16);
            lblPlayTime.AutoSize = true;
            //lblPlayTime.Text = "Play Time: ";
            lblPlayTime.ForeColor = Color.WhiteSmoke;

            lblDungeonsCompleted = new Label("lblDungeonsCompleted");
            lblDungeonsCompleted.Location = new Point(20, 100);
            lblDungeonsCompleted.Font = FontManager.LoadFont("PMU", 16);
            lblDungeonsCompleted.AutoSize = true;
            //lblDungeonsCompleted.Text = "Dungeon Victories: ";
            lblDungeonsCompleted.ForeColor = Color.WhiteSmoke;

            lblMissionsCompleted = new Label("lblMissionsCompleted");
            lblMissionsCompleted.Location = new Point(20, 120);
            lblMissionsCompleted.Font = FontManager.LoadFont("PMU", 16);
            lblMissionsCompleted.AutoSize = true;
            //lblMissionsCompleted.Text = "Missions Completed: ";
            lblMissionsCompleted.ForeColor = Color.WhiteSmoke;

            //lblRescuesCompleted = new Label("lblRescuesCompleted");
            //lblRescuesCompleted.Location = new Point(20, 140);
            //lblRescuesCompleted.Font = FontManager.LoadFont("PMU", 16);
            //lblRescuesCompleted.AutoSize = true;
            //lblRescuesCompleted.Text = "Successful Rescues: ";
            //lblRescuesCompleted.ForeColor = Color.WhiteSmoke;
            #endregion

            #region Dungeons
            lblDungeons = new Label("lblDungeons");
            lblDungeons.Location = new Point(20, 10);
            lblDungeons.Font = FontManager.LoadFont("PMU", 32);
            lblDungeons.AutoSize = true;
            lblDungeons.Text = "Dungeons";
            lblDungeons.ForeColor = Color.WhiteSmoke;

            lblEntered = new Label("lblEntered");
            lblEntered.Location = new Point(20, 40);
            lblEntered.Font = FontManager.LoadFont("PMU", 16);
            lblEntered.AutoSize = true;
            lblEntered.Text = "Red = Entered";
            lblEntered.ForeColor = Color.Red;

            lblCompleted = new Label("lblCompleted");
            lblCompleted.Location = new Point(100, 40);
            lblCompleted.Font = FontManager.LoadFont("PMU", 16);
            lblCompleted.AutoSize = true;
            lblCompleted.Text = "Blue = Completed";
            lblCompleted.ForeColor = Color.Cyan;

            lbxDungeonList = new ListBox("lstDungeonList");
            lbxDungeonList.Location = new Point(10, 60);
            lbxDungeonList.Size = new Size(pnlDungeons.Width - lbxDungeonList.X * 2, pnlDungeons.Height - lbxDungeonList.Y - 10);
            lbxDungeonList.BackColor = Color.Transparent;
            lbxDungeonList.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

            #endregion

            #region Pokedex
            lblPokedex = new Label("lblPokedex");
            lblPokedex.Location = new Point(20, 10);
            lblPokedex.Font = FontManager.LoadFont("PMU", 32);
            lblPokedex.AutoSize = true;
            lblPokedex.Text = "Recruit List";
            lblPokedex.ForeColor = Color.WhiteSmoke;

            lblDefeated = new Label("lblDefeated");
            lblDefeated.Location = new Point(20, 40);
            lblDefeated.Font = FontManager.LoadFont("PMU", 16);
            lblDefeated.AutoSize = true;
            lblDefeated.Text = "Red = Defeated";
            lblDefeated.ForeColor = Color.Red;

            lblJoined = new Label("lblJoined");
            lblJoined.Location = new Point(100, 40);
            lblJoined.Font = FontManager.LoadFont("PMU", 16);
            lblJoined.AutoSize = true;
            lblJoined.Text = "Blue = Joined";
            lblJoined.ForeColor = Color.Cyan;

            lbxPokedex = new ListBox("lstPokedex");
            lbxPokedex.Location = new Point(10, 60);
            lbxPokedex.Size = new Size(pnlPokedex.Width - lbxPokedex.X * 2, pnlPokedex.Height - lbxPokedex.Y - 10);
            lbxPokedex.BackColor = Color.Transparent;
            lbxPokedex.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

            #endregion


            #region Addwidget
            this.AddWidget(lblAdventureLog);
            this.AddWidget(lblPage);

            #region General
            pnlGeneral.AddWidget(lblGeneral);
            pnlGeneral.AddWidget(lblPlayerName);
            pnlGeneral.AddWidget(lblRank);
            pnlGeneral.AddWidget(lblPlayTime);
            pnlGeneral.AddWidget(lblDungeonsCompleted);
            pnlGeneral.AddWidget(lblMissionsCompleted);
            //pnlGeneral.AddWidget(lblRescuesCompleted);
            #endregion General
            #region Dungeons
            pnlDungeons.AddWidget(lblDungeons);
            pnlDungeons.AddWidget(lblEntered);
            pnlDungeons.AddWidget(lblCompleted);
            pnlDungeons.AddWidget(lbxDungeonList);
            #endregion Dungeons
            #region Pokedex
            pnlPokedex.AddWidget(lblPokedex);
            pnlPokedex.AddWidget(lblDefeated);
            pnlPokedex.AddWidget(lblJoined);
            pnlPokedex.AddWidget(lbxPokedex);
            #endregion Pokedex

            this.AddWidget(pnlGeneral);
            this.AddWidget(pnlDungeons);
            this.AddWidget(pnlPokedex);
            #endregion AddWidget
            
        }

        #endregion Constructors
        #region Methods

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (!loaded) return;
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                case SdlDotNet.Input.Key.Backspace:
                    {
                        // Show the others menu when the backspace key is pressed
                        MenuSwitcher.ShowOthersMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.LeftArrow:
                    {
                        // change pages
                        SwitchToPage((page + 5) % 3);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow:
                    {
                        // change pages
                        SwitchToPage((page + 1) % 3);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
            }
        }

        public Widgets.BorderedPanel MenuPanel
        {
            get { return this; }
        }

        public void SwitchToPage(int newPage)
        {
            page = newPage;
            switch (page)
            {
                case 0:
                    {
                        pnlDungeons.Visible = false;
                        pnlPokedex.Visible = false;
                        pnlGeneral.Visible = true;
                    }
                    break;
                case 1:
                    {
                        pnlPokedex.Visible = false;
                        pnlGeneral.Visible = false;
                        pnlDungeons.Visible = true;
                    }
                    break;
                case 2:
                    {
                        pnlGeneral.Visible = false;
                        pnlDungeons.Visible = false;
                        pnlPokedex.Visible = true;
                    }
                    break;

            }

            lblPage.Text = "Page " + (page + 1);

        }

        public void LoadAdventureLogFromPacket(string[] parse)
        {
            int n;

            //General
            lblPlayerName.Text = "Name: " + Players.PlayerManager.MyPlayer.Name;
            lblRank.Text = "Rank: " + Missions.MissionManager.RankToString(Players.PlayerManager.MyPlayer.ExplorerRank) + " Rank (" + Players.PlayerManager.MyPlayer.MissionExp + " Pts.)";
            lblPlayTime.Text = "Play Time: " + (parse[1].ToInt() / 60).ToString().PadLeft(2, '0') + " : " + (parse[1].ToInt() % 60).ToString().PadLeft(2, '0');
            lblDungeonsCompleted.Text = "Dungeon Victories: " + parse[2];
            lblMissionsCompleted.Text = "Missions Completed: " + parse[3];
            //lblRescuesCompleted.Text = "Successful Rescues: " + parse[4];

            n = 5;
            //Dungeons
            for (int i = 0; i < parse[n].ToInt(); i++)
            {
                ListBoxTextItem item = new ListBoxTextItem(FontManager.LoadFont("PMU", 16), parse[n + i*2 + 1]);
                if (parse[n + i*2 + 2].ToInt() > 0)
                {
                    item.ForeColor = Color.Cyan;
                }
                else
                {
                    item.ForeColor = Color.Red;
                }
                lbxDungeonList.Items.Add(item);
                
            }

            n += lbxDungeonList.Items.Count * 2;

            for (int i = 1; i <= MaxInfo.TotalPokemon; i++)
            {
                ListBoxTextItem item;

                if (parse[n + 1].ToInt() <= 0)
                {
                    item = new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "#" + i + ": -----");
                    item.ForeColor = Color.Gray;
                }
                else
                {
                    item = new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "#" + i + ": " + Pokedex.PokemonHelper.Pokemon[i-1].Name);
                    if (parse[n + 1].ToInt() == 1)
                    {
                        item.ForeColor = Color.Red;
                    }
                    else if (parse[n + 1].ToInt() == 2)
                    {
                        item.ForeColor = Color.Cyan;
                    }
                    else
                    {
                        item.ForeColor = Color.Black;
                    }
                }

                lbxPokedex.Items.Add(item);
                n++;
            }

            SwitchToPage(0);
            loaded = true;
        }

        #endregion Methods
    }
}
