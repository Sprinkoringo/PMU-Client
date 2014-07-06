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


namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Widgets;
    using Client.Logic.Network;

    partial class winGame
    {
        #region Fields

        StatLabel lblStats;
        ActiveTeamPanel pnlTeam;
        MapViewer mapViewer;
        Menus.Core.MenuManager menuManager;
        ShortcutBar shortcutBar;
        BattleLog battleLog;
        internal bool inMapEditor;
        int menuManagerKeyDownTick;
        int tickSinceLastPing;

        #endregion Fields

        #region Constructors

        public winGame() {
            //this.Windowed = false;
            //this.Size = SdlDotNet.Graphics.Video.Screen.Size;
            //this.Location = new System.Drawing.Point(0, 0);
            //this.BackColor = Color.SteelBlue;

            pnlTeam = new ActiveTeamPanel("pnlTeam");
            pnlTeam.Size = new Size(SdlDotNet.Graphics.Video.Screen.Width/* - 50*/, 100);
            pnlTeam.Location = new Point(SdlDotNet.Widgets.DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, pnlTeam.Size).X, 0);

            lblStats = new StatLabel("lblStats");
            lblStats.Size = new System.Drawing.Size(400, 30);
            lblStats.Location = new Point(pnlTeam.X + 10, 70);

            mapViewer = new Widgets.MapViewer("mapViewer");
            mapViewer.Size = new Size(640, 480);
            mapViewer.Location = new Point(SdlDotNet.Graphics.Video.Screen.Width - mapViewer.Width, pnlTeam.Y + pnlTeam.Height + 1);
            mapViewer.ActiveMap = null;
            mapViewer.BackColor = Color.Gray;
            mapViewer.KeyRepeatInterval = 1;
            //mapViewer.BeforeKeyDown += new EventHandler<SdlDotNet.Widgets.BeforeKeyDownEventArgs>(mapViewer_BeforeKeyDown);
            mapViewer.KeyDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(mapViewer_KeyDown);
            mapViewer.KeyUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(mapViewer_KeyUp);
            mapViewer.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(mapViewer_Click);
            mapViewer.MouseMotion += new EventHandler<SdlDotNet.Input.MouseMotionEventArgs>(mapViewer_MouseMotion);

            menuManager = new Menus.Core.MenuManager("menuManager", mapViewer);
            menuManager.Location = mapViewer.Location;
            menuManager.Size = mapViewer.Size;
            menuManager.Visible = false;
            menuManager.KeyDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(menuManager_KeyDown);
            menuManager.KeyUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(menuManager_KeyUp);
            menuManager.KeyRepeatInterval = 0;
            menuManager.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(menuManager_Click);

            battleLog = new BattleLog("battleLog");
            battleLog.Size = new System.Drawing.Size(mapViewer.Width - 10, 100);
            battleLog.Location = new Point(mapViewer.X + 5, mapViewer.Y + mapViewer.Height - battleLog.Height);
            battleLog.Visible = false;

            shortcutBar = new ShortcutBar("shortcutBar");
            shortcutBar.Location = new Point(SdlDotNet.Graphics.Video.Screen.Width - shortcutBar.Width, mapViewer.Y + mapViewer.Height);
            ShortcutBarBuilder.AssembleShortcutBarButtons(shortcutBar);

            pnlTeam.Hide();
            lblStats.Hide();
            mapViewer.Hide();
            menuManager.Hide();
            shortcutBar.Hide();

            SdlDotNet.Widgets.Screen.AddWidget(pnlTeam);
            SdlDotNet.Widgets.Screen.AddWidget(mapViewer);
            SdlDotNet.Widgets.Screen.AddWidget(lblStats);
            SdlDotNet.Widgets.Screen.AddWidget(battleLog);
            SdlDotNet.Widgets.Screen.AddWidget(menuManager);
            SdlDotNet.Widgets.Screen.AddWidget(shortcutBar);

            InitMapEditorWidgets();

            // Add the events
            pnlTeam.ActiveRecruitChanged += new EventHandler<Events.ActiveRecruitChangedEventArgs>(pnlTeam_ActiveRecruitChanged);

            //this.LoadComplete();

            SdlDotNet.Core.Events.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(winGame_Tick);
            //base.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(winGame_Tick);
        }

        public void ShowWidgets() {
            pnlTeam.Show();
            lblStats.Show();
            mapViewer.Show();
            shortcutBar.Show();
        }

        int lastSaveTick = 0;
        void winGame_Tick(object sender, SdlDotNet.Core.TickEventArgs e) {
            if (Globals.InGame) {
                if (IO.Options.AutoSaveSpeed > 0) {
                    if (e.Tick > lastSaveTick + (100 * 60000 / IO.Options.AutoSaveSpeed)) {
                        Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("checkcommands", "/save"));
                        lastSaveTick = e.Tick;
                    }
                }
                if (IO.Options.Ping) {
                    if (e.Tick > tickSinceLastPing + 1000 && !MessageProcessor.PingStopwatch.IsRunning) {
                        MessageProcessor.PingStopwatch.Reset();
                        MessageProcessor.PingStopwatch.Start();
                        Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("ping"));
                        tickSinceLastPing = e.Tick;
                    }
                }

                if (Players.PlayerManager.MyPlayer.MovementPacketCache != null) {
                    if (Globals.Tick > Players.PlayerManager.MyPlayer.LastMovementCacheSend + Constants.MovementClusteringFrquency && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                        NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                        Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                        Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
                    }
                }
            }
        }

        void menuManager_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (menuManager.OpenMenus.Count == 0) {
                if (!menuManager.BlockInput) {
                    mapViewer.Focus();
                }
            } else {
                menuManager.Focus();
            }
        }

        //public override void BlitToScreen(SdlDotNet.Graphics.Surface destinationSurface) {
        //    base.BlitToScreen(destinationSurface);
        //    if (menuManager.Visible) {
        //        menuManager.BlitToScreen(destinationSurface);
        //    }
        //}

        void mapViewer_KeyUp(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            Input.InputProcessor.OnKeyUp(e);
            if (menuManager.Visible && !(menuManager.Visible && !menuManager.BlockInput)) {
                Input.InputProcessor.Attacking = false;
                Input.InputProcessor.MoveUp = false;
                Input.InputProcessor.MoveDown = false;
                Input.InputProcessor.MoveLeft = false;
                Input.InputProcessor.MoveRight = false;
            }
        }

        void menuManager_KeyDown(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            Menus.Core.MenuInputProcessor.OnKeyDown(e);
            menuManagerKeyDownTick = Globals.Tick;
            Input.InputProcessor.Attacking = false;
            Input.InputProcessor.MoveUp = false;
            Input.InputProcessor.MoveDown = false;
            Input.InputProcessor.MoveLeft = false;
            Input.InputProcessor.MoveRight = false;
        }

        void menuManager_KeyUp(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            Menus.Core.MenuInputProcessor.OnKeyUp(e);

            if (menuManager.Visible && !(menuManager.Visible && !menuManager.BlockInput)) {
                Input.InputProcessor.Attacking = false;
                Input.InputProcessor.MoveUp = false;
                Input.InputProcessor.MoveDown = false;
                Input.InputProcessor.MoveLeft = false;
                Input.InputProcessor.MoveRight = false;
            }
        }


        void mapViewer_KeyDown(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            if (Globals.Tick < menuManagerKeyDownTick + 200) {
                return;
            }
            if (!menuManager.Visible || (menuManager.Visible && !menuManager.BlockInput)) {
                Input.InputProcessor.OnKeyDown(e);
            } else if (menuManager.Visible && !menuManager.BlockInput) {
                Input.InputProcessor.Attacking = false;
                Input.InputProcessor.MoveUp = false;
                Input.InputProcessor.MoveDown = false;
                Input.InputProcessor.MoveLeft = false;
                Input.InputProcessor.MoveRight = false;
                Menus.Core.MenuInputProcessor.OnKeyDown(e);
            } else {
                Input.InputProcessor.Attacking = false;
                Input.InputProcessor.MoveUp = false;
                Input.InputProcessor.MoveDown = false;
                Input.InputProcessor.MoveLeft = false;
                Input.InputProcessor.MoveRight = false;
            }
        }

        void mapViewer_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (inMapEditor) {
                Editor_mapViewer_Click(sender, e);
            } else {
                if (Globals.InGame && Maps.MapHelper.ActiveMap != null) {
                    int newX = (e.RelativePosition.X / Constants.TILE_WIDTH) + Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
                    int newY = (e.RelativePosition.Y / Constants.TILE_HEIGHT) + Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
                    if ((Ranks.IsAllowed(Players.PlayerManager.MyPlayer, Enums.Rank.Mapper) || Maps.MapHelper.ActiveMap.MapID.StartsWith("h-")) && (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.LeftShift) && e.MouseEventArgs.Button == SdlDotNet.Input.MouseButton.SecondaryButton)) {
                        Messenger.WarpLoc(newX, newY);
                        return;
                    } else {
                        Messenger.SendSearch(newX, newY);
                    }
                }
            }
        }

        #endregion Constructors

        #region Properties

        public ActiveTeamPanel ActiveTeam {
            get { return pnlTeam; }
        }

        public StatLabel StatLabel {
            get { return lblStats; }
        }

        public MapViewer MapViewer {
            get { return mapViewer; }
        }

        public Menus.Core.MenuManager MenuManager {
            get { return menuManager; }
        }

        public BattleLog BattleLog {
            get { return battleLog; }
        }

        #endregion Properties

        #region Methods

        void pnlTeam_ActiveRecruitChanged(object sender, Events.ActiveRecruitChangedEventArgs e) {
            Messenger.SendActiveCharSwap(e.NewSlot);
        }

        public void EnableMapEditorWidgets(Enums.MapEditorLimitTypes limitType, bool liveMode) {
            this.limiter = limitType;
            this.inLiveMode = liveMode;
            mapEditor_Menu.Visible = true;
            btnAttributes.Visible = true;
            btnTerrain.Visible = true;
            tilesetViewer.Visible = true;
            EnforceMapEditorLimits();
        }

        public void DisableMapEditorWidgets() {
            mapEditor_Menu.Visible = false;
            pnlAttributes.Visible = false;
            btnAttributes.Visible = false;
            btnTerrain.Visible = false;
            tilesetViewer.Visible = false;
            this.inLiveMode = false;
            this.HideAutoHiddenPanels();
            Messenger.SendExitMapEditor();
        }

        public void AddToBattleLog(string text, Color color) {
            if (!battleLog.Visible && !menuManager.Visible && inMapEditor == false) {
                battleLog.Visible = true;
            }
            battleLog.tmrHide.Start();
            battleLog.AddLog(text, color);
        }

        #endregion Methods
    }
}