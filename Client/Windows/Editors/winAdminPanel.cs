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
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;
using Client.Logic.Network;
using PMU.Sockets;
using PMU.Core;

namespace Client.Logic.Windows.Editors
{
    class winAdminPanel : Core.WindowCore
    {
        #region Fields

        Panel pnlWeather;
        Panel pnlPlayerInfo;
        Panel pnlCommands;
        Button btnWeather;
        Button btnPlayerInfo;
        Button btnCommands;
        ComboBox cbWeather;
        Button btnApplyWeather;
        Button btnBanish;
        Button btnWarpTo;
        Button btnKick;
        Button btnSetAccess;
        ComboBox cbAccessLevel;
        Button btnRespawn;
        Button btnSetSprite;
        Button btnWarpMeTo;
        Button btnSetPlayerSprite;
        Button btnWarpToMe;
        Label lblPlayerName;
        TextBox txtPlayerName;
        Label lblSpriteNumber;
        TextBox txtSpriteNumber;
        Label lblAccessLevel;
        Label lblMapNumber;
        TextBox txtMapNumber;
        Button btnMapEditor;
        Button btnMapReport;
        Button btnSpells;
        Button btnItems;
        Button btnShops;
        Button btnEvolutions;
        Button btnStories; //And Why isn't this available for Mappers?
        Button btnNPC;
        //Button btnArrows;
        Button btnEmotion;
        Button btnRDungeons;
        Button btnMissions;
        Button btnDungeons;

        #endregion Fields

        #region Constructors

        public winAdminPanel()
            : base("winAdminPanel") {

            //this.Location = Graphics.DrawingSupport.GetCenter(this.Size);
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(354, 220);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Administration Panel";

            pnlWeather = new Panel("pnlWeather");
            pnlWeather.Size = new System.Drawing.Size(354, 220);
            pnlWeather.Location = new Point(0, 34);
            pnlWeather.BackColor = Color.White;
            pnlWeather.Visible = true;

            pnlPlayerInfo = new Panel("pnlPlayerInfo");
            pnlPlayerInfo.Size = new System.Drawing.Size(410, 500);
            pnlPlayerInfo.Location = new Point(0, 34);
            pnlPlayerInfo.BackColor = Color.White;
            pnlPlayerInfo.Visible = false;

            pnlCommands = new Panel("pnlCommands");
            pnlCommands.Size = new System.Drawing.Size(410, 348);
            pnlCommands.Location = new Point(0, 34);
            pnlCommands.BackColor = Color.White;
            pnlCommands.Visible = false;

            #region Widgets

            btnWeather = new Button("btnWeather");
            btnWeather.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnWeather.Location = new Point(27, 0);
            btnWeather.Size = new System.Drawing.Size(100, 32);
            btnWeather.Text = "Weather";
            btnWeather.Selected = true;
            btnWeather.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnWeather_Click);

            btnPlayerInfo = new Button("btnPlayerInfo");
            btnPlayerInfo.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnPlayerInfo.Location = new Point(127, 0);
            btnPlayerInfo.Size = new System.Drawing.Size(100, 32);
            btnPlayerInfo.Text = "Player Info";
            btnPlayerInfo.Selected = false;
            btnPlayerInfo.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnPlayerInfo_Click);

            btnCommands = new Button("btnCommands");
            btnCommands.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnCommands.Location = new Point(227, 0);
            btnCommands.Size = new System.Drawing.Size(100, 32);
            btnCommands.Text = "Commands";
            btnCommands.Selected = false;
            btnCommands.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnCommands_Click);
			
            
            
            cbWeather = new ComboBox("cbWeather");
            cbWeather.Location = new Point(110, 20);
            cbWeather.Size = new System.Drawing.Size(134, 16);
            cbWeather.BackColor = Color.DarkGray;
            for (int i =0; i < 13; i++) {
            	ListBoxTextItem item = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.Weather), i));
            	cbWeather.Items.Add(item);
            }
            cbWeather.SelectItem(0);

            btnApplyWeather = new Button("btnApplyWeather");
            btnApplyWeather.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnApplyWeather.Location = new Point(110, 54);
            btnApplyWeather.Size = new System.Drawing.Size(134, 32);
            btnApplyWeather.Visible = true;
            btnApplyWeather.Text = "Apply";
            btnApplyWeather.Click += new EventHandler<MouseButtonEventArgs>(btnApplyWeather_Click);

            

            btnBanish = new Button("btnBanish");
            btnBanish.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnBanish.Location = new Point(20, 20);
            btnBanish.Size = new System.Drawing.Size(134, 32);
            btnBanish.Visible = true;
            btnBanish.Text = "Ban";
            btnBanish.Click += new EventHandler<MouseButtonEventArgs>(btnBanish_Click);

            btnWarpTo = new Button("btnWarpTo");
            btnWarpTo.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnWarpTo.Location = new Point(200, 20);
            btnWarpTo.Size = new System.Drawing.Size(134, 32);
            btnWarpTo.Visible = true;
            btnWarpTo.Text = "Warp to Map";
            btnWarpTo.Click += new EventHandler<MouseButtonEventArgs>(btnWarpTo_Click);

            btnKick = new Button("btnKick");
            btnKick.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnKick.Location = new Point(20, 54);
            btnKick.Size = new System.Drawing.Size(134, 32);
            btnKick.Visible = true;
            btnKick.Text = "Kick";
            btnKick.Click += new EventHandler<MouseButtonEventArgs>(btnKick_Click);

            btnSetAccess = new Button("btnSetAccess");
            btnSetAccess.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnSetAccess.Location = new Point(200, 54);
            btnSetAccess.Size = new System.Drawing.Size(134, 32);
            btnSetAccess.Visible = true;
            btnSetAccess.Text = "Set Access";
            btnSetAccess.Click += new EventHandler<MouseButtonEventArgs>(btnSetAccess_Click);
            
            cbAccessLevel = new ComboBox("cbAccessLevel");
            cbAccessLevel.Location = new Point(174, 274);
            cbAccessLevel.Size = new System.Drawing.Size(134, 18);
            cbAccessLevel.BackColor = Color.DarkGray;
            for (int i =0; i < 7; i++) {
            	ListBoxTextItem item = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.Rank), i));
            	cbAccessLevel.Items.Add(item);
            }
            cbAccessLevel.SelectItem(0);

            btnRespawn = new Button("btnRespawn");
            btnRespawn.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRespawn.Location = new Point(20, 88);
            btnRespawn.Size = new System.Drawing.Size(134, 32);
            btnRespawn.Visible = true;
            btnRespawn.Text = "Respawn";
            btnRespawn.Click += new EventHandler<MouseButtonEventArgs>(btnRespawn_Click);

            btnSetSprite = new Button("btnSetSprite");
            btnSetSprite.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnSetSprite.Location = new Point(200, 88);
            btnSetSprite.Size = new System.Drawing.Size(134, 32);
            btnSetSprite.Visible = true;
            btnSetSprite.Text = "Set Species";
            btnSetSprite.Click += new EventHandler<MouseButtonEventArgs>(btnSetSprite_Click);

            btnWarpMeTo = new Button("btnWarpMeTo");
            btnWarpMeTo.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnWarpMeTo.Location = new Point(20, 122);
            btnWarpMeTo.Size = new System.Drawing.Size(134, 32);
            btnWarpMeTo.Visible = true;
            btnWarpMeTo.Text = "Warp Me to";
            btnWarpMeTo.Click += new EventHandler<MouseButtonEventArgs>(btnWarpMeTo_Click);

            btnSetPlayerSprite = new Button("btnSetPlayerSprite");
            btnSetPlayerSprite.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnSetPlayerSprite.Location = new Point(200, 122);
            btnSetPlayerSprite.Size = new System.Drawing.Size(134, 32);
            btnSetPlayerSprite.Visible = true;
            btnSetPlayerSprite.Text = "Set Player Species";
            btnSetPlayerSprite.Click += new EventHandler<MouseButtonEventArgs>(btnSetPlayerSprite_Click);

            btnWarpToMe = new Button("btnWarpToMe");
            btnWarpToMe.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnWarpToMe.Location = new Point(110, 156);
            btnWarpToMe.Size = new System.Drawing.Size(134, 32);
            btnWarpToMe.Visible = true;
            btnWarpToMe.Text = "Warp to Me";
            btnWarpToMe.Click += new EventHandler<MouseButtonEventArgs>(btnWarpToMe_Click);

            lblPlayerName = new Label("lblPlayerName");
            lblPlayerName.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblPlayerName.Location = new Point(20, 210);
            lblPlayerName.Size = new System.Drawing.Size(134, 32);
            lblPlayerName.Text = "Player Name:";
            lblPlayerName.Visible = true;

            txtPlayerName = new TextBox("txtPlayerName");
            txtPlayerName.Location = new Point(174, 219);
            txtPlayerName.Size = new System.Drawing.Size(134, 18);
            txtPlayerName.Visible = true;

            lblSpriteNumber = new Label("lblSpriteNumber");
            lblSpriteNumber.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblSpriteNumber.Location = new Point(20, 242);
            lblSpriteNumber.Size = new System.Drawing.Size(134, 32);
            lblSpriteNumber.Text = "Species Number:";
            lblSpriteNumber.Visible = true;

            txtSpriteNumber = new TextBox("txtSpriteNumber");
            txtSpriteNumber.Location = new Point(174, 251);
            txtSpriteNumber.Size = new System.Drawing.Size(134, 18);
            txtSpriteNumber.Visible = true;

            lblAccessLevel = new Label("lblAccessLevel");
            lblAccessLevel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblAccessLevel.Location = new Point(20, 274);
            lblAccessLevel.Size = new System.Drawing.Size(134, 32);
            lblAccessLevel.Text = "Access Level:";
            lblAccessLevel.Visible = true;

            //boxAccessLevel = new DropBox("boxAccessLevel");
            //boxAccessLevel.Location = new Point(174, 274);
            //boxAccessLevel.visible = false;

            lblMapNumber = new Label("lblMapNumber");
            lblMapNumber.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblMapNumber.Location = new Point(20, 306);
            lblMapNumber.Size = new System.Drawing.Size(134, 32);
            lblMapNumber.Text = "Map Number:";
            lblMapNumber.Visible = true;

            txtMapNumber = new TextBox("txtMapNumber");
            txtMapNumber.Location = new Point(174, 315);
            txtMapNumber.Size = new System.Drawing.Size(134, 18);
            txtMapNumber.Visible = true;

            btnMapEditor = new Button("btnMapEditor");
            btnMapEditor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnMapEditor.Location = new Point(20, 20);
            btnMapEditor.Size = new System.Drawing.Size(134, 32);
            btnMapEditor.Visible = true;
            btnMapEditor.Text = "Map Editor";
            btnMapEditor.Click += new EventHandler<MouseButtonEventArgs>(btnMapEditor_Click);

            btnMapReport = new Button("btnMapReport");
            btnMapReport.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnMapReport.Location = new Point(200, 20);
            btnMapReport.Size = new System.Drawing.Size(134, 32);
            btnMapReport.Visible = true;
            btnMapReport.Text = "Map Report";
            btnMapReport.Click += new EventHandler<MouseButtonEventArgs>(btnMapReport_Click);

            btnSpells = new Button("btnSpells");
            btnSpells.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnSpells.Location = new Point(20, 54);
            btnSpells.Size = new System.Drawing.Size(134, 32);
            btnSpells.Visible = true;
            btnSpells.Text = "Edit Moves";
            btnSpells.Click += new EventHandler<MouseButtonEventArgs>(btnSpells_Click);

            btnItems = new Button("btnItems");
            btnItems.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnItems.Location = new Point(200, 54);
            btnItems.Size = new System.Drawing.Size(134, 32);
            btnItems.Visible = true;
            btnItems.Text = "Edit Items";
            btnItems.Click += new EventHandler<MouseButtonEventArgs>(btnItems_Click);

            btnShops = new Button("btnShops");
            btnShops.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnShops.Location = new Point(20, 88);
            btnShops.Size = new System.Drawing.Size(134, 32);
            btnShops.Visible = true;
            btnShops.Text = "Edit Shops";
            btnShops.Click += new EventHandler<MouseButtonEventArgs>(btnShops_Click);

            btnEvolutions = new Button("btnEvolutions");
            btnEvolutions.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEvolutions.Location = new Point(200, 88);
            btnEvolutions.Size = new System.Drawing.Size(134, 32);
            btnEvolutions.Visible = true;
            btnEvolutions.Text = "Edit Evolutions";
            btnEvolutions.Click += new EventHandler<MouseButtonEventArgs>(btnEvolutions_Click);

            btnStories = new Button("btnStories");
            btnStories.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnStories.Location = new Point(20, 122);
            btnStories.Size = new System.Drawing.Size(134, 32);
            btnStories.Visible = true;
            btnStories.Text = "Story Editor";
            btnStories.Click += new EventHandler<MouseButtonEventArgs>(btnStories_Click);

            btnNPC = new Button("btnNPC");
            btnNPC.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnNPC.Location = new Point(200, 122);
            btnNPC.Size = new System.Drawing.Size(134, 32);
            btnNPC.Visible = true;
            btnNPC.Text = "NPC Editor";
            btnNPC.Click += new EventHandler<MouseButtonEventArgs>(btnNPC_Click);

            //btnArrows = new Button("btnArrows");
            //btnArrows.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            //btnArrows.Location = new Point(20, 156);
            //btnArrows.Size = new System.Drawing.Size(134, 32);
            //btnArrows.Visible = true;
            //btnArrows.Text = "Edit Arrows";
            //btnArrows.Click += new EventHandler<MouseButtonEventArgs>(btnArrows_Click);

            btnEmotion = new Button("btnEmotion");
            btnEmotion.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEmotion.Location = new Point(200, 156);
            btnEmotion.Size = new System.Drawing.Size(134, 32);
            btnEmotion.Visible = true;
            btnEmotion.Text = "Edit Emotions";
            btnEmotion.Click += new EventHandler<MouseButtonEventArgs>(btnEmotion_Click);

            btnRDungeons = new Button("btnRDungeons");
            btnRDungeons.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRDungeons.Location = new Point(20, 156);
            btnRDungeons.Size = new System.Drawing.Size(134, 32);
            btnRDungeons.Visible = true;
            btnRDungeons.Text = "Edit Random Dungeons";
            btnRDungeons.Click += new EventHandler<MouseButtonEventArgs>(btnRDungeons_Click);

            btnMissions = new Button("btnMissions");
            btnMissions.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnMissions.Location = new Point(20, 190);
            btnMissions.Size = new System.Drawing.Size(134, 32);
            btnMissions.Visible = true;
            btnMissions.Text = "Edit Missions";
            btnMissions.Click += new EventHandler<MouseButtonEventArgs>(btnMissions_Click);

            btnDungeons = new Button("btnDungeons");
            btnDungeons.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnDungeons.Location = new Point(200, 190);
            btnDungeons.Size = new System.Drawing.Size(134, 32);
            btnDungeons.Visible = true;
            btnDungeons.Text = "Edit Dungeons";
            btnDungeons.Click += new EventHandler<MouseButtonEventArgs>(btnDungeons_Click);

            #region Addwidget

            pnlWeather.AddWidget(cbWeather);
            pnlWeather.AddWidget(btnApplyWeather);
            pnlPlayerInfo.AddWidget(btnBanish);
            pnlPlayerInfo.AddWidget(btnWarpTo);
            pnlPlayerInfo.AddWidget(btnKick);
            pnlPlayerInfo.AddWidget(btnSetAccess);
            pnlPlayerInfo.AddWidget(btnRespawn);
            pnlPlayerInfo.AddWidget(btnSetSprite);
            pnlPlayerInfo.AddWidget(btnWarpMeTo);
            pnlPlayerInfo.AddWidget(btnSetPlayerSprite);
            pnlPlayerInfo.AddWidget(btnWarpToMe);
            pnlPlayerInfo.AddWidget(lblPlayerName);
            pnlPlayerInfo.AddWidget(txtPlayerName);
            pnlPlayerInfo.AddWidget(lblSpriteNumber);
            pnlPlayerInfo.AddWidget(txtSpriteNumber);
            pnlPlayerInfo.AddWidget(lblAccessLevel);
            pnlPlayerInfo.AddWidget(cbAccessLevel);
            pnlPlayerInfo.AddWidget(lblMapNumber);
            pnlPlayerInfo.AddWidget(txtMapNumber);
            pnlCommands.AddWidget(btnMapEditor);
            pnlCommands.AddWidget(btnMapReport);
            pnlCommands.AddWidget(btnSpells);
            pnlCommands.AddWidget(btnItems);
            pnlCommands.AddWidget(btnShops);
            pnlCommands.AddWidget(btnEvolutions);
            pnlCommands.AddWidget(btnStories);
            pnlCommands.AddWidget(btnNPC);
            //pnlCommands.AddWidget(btnArrows);
            pnlCommands.AddWidget(btnEmotion);
            pnlCommands.AddWidget(btnRDungeons);
            pnlCommands.AddWidget(btnMissions);
            pnlCommands.AddWidget(btnDungeons);

            this.AddWidget(pnlWeather);
            this.AddWidget(btnWeather);
            this.AddWidget(pnlPlayerInfo);
            this.AddWidget(btnPlayerInfo);
            this.AddWidget(pnlCommands);
            this.AddWidget(btnCommands);

            this.LoadComplete();
            #endregion Addwidget

            #endregion Widgets
        }
        #endregion Constructors

        #region Methods

        void btnWeather_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnWeather.Selected) {
                btnWeather.Selected = true;
                btnPlayerInfo.Selected = false;
                btnCommands.Selected = false;
                pnlWeather.Visible = true;
                pnlPlayerInfo.Visible = false;
                pnlCommands.Visible = false;
                btnPlayerInfo.Location = new Point(127, 0);
                btnCommands.Location = new Point(227, 0);
                this.Size = new System.Drawing.Size(354, 220);
                this.TitleBar.Text = "Administration Panel - Weather";
            }
        }

        void btnPlayerInfo_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnPlayerInfo.Selected) {
                btnWeather.Selected = false;
                btnPlayerInfo.Selected = true;
                btnCommands.Selected = false;
                pnlWeather.Visible = false;
                pnlPlayerInfo.Visible = true;
                pnlCommands.Visible = false;
                btnWeather.Location = new Point(27, 0);
                btnCommands.Location = new Point(227, 0);
                this.Size = new System.Drawing.Size(354, 410);
                this.TitleBar.Text = "Administration Panel - Player Info";
            }
        }

        void btnCommands_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnCommands.Selected) {
                btnWeather.Selected = false;
                btnPlayerInfo.Selected = false;
                btnCommands.Selected = true;
                pnlWeather.Visible = false;
                pnlPlayerInfo.Visible = false;
                pnlCommands.Visible = true;
                btnWeather.Location = new Point(27, 0);
                btnPlayerInfo.Location = new Point(127, 0);
                this.Size = new System.Drawing.Size(354, 290);
                this.TitleBar.Text = "Administration Panel - Developer Commands";
            }
        }

        

        void btnApplyWeather_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
          
        	    Messenger.Weather(cbWeather.SelectedIndex);
        }

        

        void btnBanish_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtPlayerName
            Messenger.BanPlayer(txtPlayerName.Text);
        }

        void btnWarpTo_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtMapNumber
            if (txtMapNumber.Text.IsNumeric()) {
            Messenger.WarpTo(txtMapNumber.Text.ToInt());
            }
        }

        void btnKick_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtPlayerName
            Messenger.KickPlayer(txtPlayerName.Text);
        }

        void btnSetAccess_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //To be used with BoxAccessLevel and txtPlayerName
            Messenger.SetAccess(txtPlayerName.Text, cbAccessLevel.SelectedIndex);
        }

        void btnRespawn_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //null
            Messenger.MapRespawn();
        }

        void btnSetSprite_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //something to do with txtSpriteNumber
            Messenger.SetSprite(txtSpriteNumber.Text.ToInt());
        }

        void btnWarpMeTo_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtPlayerName
            Messenger.WarpMeTo(txtPlayerName.Text);
        }

        void btnSetPlayerSprite_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtPlayerName and txtSpriteNumber
            Messenger.SetPlayerSprite(txtPlayerName.Text,txtSpriteNumber.Text.ToInt());
        }

        void btnWarpToMe_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Used with txtPlayerName
            Messenger.WarpToMe(txtPlayerName.Text);
        }

        void btnMapEditor_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditmap"));
        }

        void btnMapReport_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            if (WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.IsModuleAvailable(Enums.ExpKitModules.MapReport)) {
                WindowSwitcher.ExpKit.KitContainer.SetActiveModule(Enums.ExpKitModules.MapReport);
            }
        }

        void btnSpells_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditmove"));
        }

        void btnItems_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requestedititem"));
            return;
        }

        void btnShops_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditshop"));
            return;
        }

        void btnEvolutions_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditevo"));
            return;
        }

        void btnStories_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditstory"));
            return;
        }

        void btnNPC_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditnpc"));
            return;
        }

        void btnArrows_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditarrow"));
            return;
        }

        void btnEmotion_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditemoticon"));
            return;
        }

        void btnRDungeons_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditrdungeon"));
            return;
        }

        void btnDungeons_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditdungeon"));
            return;
        }

        void btnMissions_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendPacket(TcpPacket.CreatePacket("requesteditmission"));
            return;
        }

        #endregion Methods

    }
}
