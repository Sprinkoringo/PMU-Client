namespace Client.Logic.Input
{
    using System;

    using SdlInput = SdlDotNet.Input;
    using Client.Logic.Players;
    using Client.Logic.Network;
    using System.Drawing;
    using Client.Logic.Windows;

    /// <summary>
    /// Description of InputProcessor.
    /// </summary>
    internal class InputProcessor
    {
        #region Constructors

        public InputProcessor() {
        }

        #endregion Constructors

        public static int SelectedMove {
            get;
            set;
        }

        public static bool MoveUp { get; set; }
        public static bool MoveDown { get; set; }
        public static bool MoveLeft { get; set; }
        public static bool MoveRight { get; set; }
        public static bool Attacking { get; set; }
        public static bool Pickup { get; set; }

        #region Methods

        public static void Initialize() {
            SelectedMove = -1;
        }

        
        public static void OnKeyDown(SdlInput.KeyboardEventArgs e) {
            if (e.Key == IO.ControlLoader.UpKey) {
                MoveUp = true;
                MovePlayer(Enums.Direction.Up, false);
            } else if (e.Key == IO.ControlLoader.DownKey) {
                MoveDown = true;
                MovePlayer(Enums.Direction.Down, false);
            } else if (e.Key == IO.ControlLoader.LeftKey) {
                MoveLeft = true;
                MovePlayer(Enums.Direction.Left, false);
            } else if (e.Key == IO.ControlLoader.RightKey) {
                MoveRight = true;
                MovePlayer(Enums.Direction.Right, false);
            } else if (e.Key == SdlInput.Key.Return) {
                if (!Pickup) {
                    PickupItem();
                    Pickup = true;
                }
                //} else if (e.Key == IO.ControlLoader.AttackKey) {
                //    GameProcessor.CheckAttack();
            } else if (e.Key == SdlInput.Key.W) {
                if (PlayerManager.MyPlayer.Moves[0].MoveNum > -1) {
                    SelectedMove = 0;
                }
            } else if (e.Key == SdlInput.Key.A) {
                if (PlayerManager.MyPlayer.Moves[1].MoveNum > -1) {
                    SelectedMove = 1;
                }
            } else if (e.Key == SdlInput.Key.S) {
                if (PlayerManager.MyPlayer.Moves[2].MoveNum > -1) {
                    SelectedMove = 2;
                }
            } else if (e.Key == SdlInput.Key.D) {
                if (PlayerManager.MyPlayer.Moves[3].MoveNum > -1) {
                    SelectedMove = 3;
                }
            } else if (e.Key == SdlInput.Key.F) {
                
                if (SelectedMove > -1 && SelectedMove < 4) {
                    PlayerManager.MyPlayer.UseMove(SelectedMove);
                } else {
                    Attacking = true;
                    GameProcessor.CheckAttack();
                }
            } else if (e.Key == SdlInput.Key.Z) {
                int itemNum = 0;
                if (PlayerManager.MyPlayer.Team[1] != null) {
                    itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(PlayerManager.MyPlayer.Team[0].HeldItemSlot);
                }
                if (itemNum > 0) {
                    if ((int)Items.ItemHelper.Items[itemNum].Type < 8 || (int)Items.ItemHelper.Items[itemNum].Type == 15) {

                    } else {
                        if (SdlInput.Keyboard.IsKeyPressed(SdlInput.Key.LeftControl)) {
                            Messenger.SendThrowItem(PlayerManager.MyPlayer.Team[0].HeldItemSlot);
                        } else {
                            GameProcessor.CheckUseItem(0, PlayerManager.MyPlayer.Team[0].HeldItemSlot);
                        }
                    }
                }
            } else if (e.Key == SdlInput.Key.X) {
                int itemNum = 0;
                if (PlayerManager.MyPlayer.Team[1] != null) {
                    itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(PlayerManager.MyPlayer.Team[1].HeldItemSlot);
                }
                if (itemNum > 0) {
                    if ((int)Items.ItemHelper.Items[itemNum].Type < 8 || (int)Items.ItemHelper.Items[itemNum].Type == 15) {

                    } else {
                        if (SdlInput.Keyboard.IsKeyPressed(SdlInput.Key.LeftControl)) {
                            Messenger.SendThrowItem(PlayerManager.MyPlayer.Team[1].HeldItemSlot);
                        } else {
                            GameProcessor.CheckUseItem(1, PlayerManager.MyPlayer.Team[1].HeldItemSlot);
                        }
                    }
                }
            } else if (e.Key == SdlInput.Key.C) {
                int itemNum = 0;
                if (PlayerManager.MyPlayer.Team[1] != null) {
                    itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(PlayerManager.MyPlayer.Team[2].HeldItemSlot);
                }
                if (itemNum > 0) {
                    if ((int)Items.ItemHelper.Items[itemNum].Type < 8 || (int)Items.ItemHelper.Items[itemNum].Type == 15) {

                    } else {
                        if (SdlInput.Keyboard.IsKeyPressed(SdlInput.Key.LeftControl)) {
                            Messenger.SendThrowItem(PlayerManager.MyPlayer.Team[2].HeldItemSlot);
                        } else {
                            GameProcessor.CheckUseItem(2, PlayerManager.MyPlayer.Team[2].HeldItemSlot);
                        }
                    }
                }
            } else if (e.Key == SdlInput.Key.V) {
                int itemNum = 0;
                if (PlayerManager.MyPlayer.Team[1] != null) {
                    itemNum = Players.PlayerManager.MyPlayer.GetInvItemNum(PlayerManager.MyPlayer.Team[3].HeldItemSlot);
                }
                if (itemNum > 0) {
                    if ((int)Items.ItemHelper.Items[itemNum].Type < 8 || (int)Items.ItemHelper.Items[itemNum].Type == 15) {

                    } else {
                        if (SdlInput.Keyboard.IsKeyPressed(SdlInput.Key.LeftControl)) {
                            Messenger.SendThrowItem(PlayerManager.MyPlayer.Team[3].HeldItemSlot);
                        } else {
                            GameProcessor.CheckUseItem(3, PlayerManager.MyPlayer.Team[3].HeldItemSlot);
                        }
                    }
                }
            } else if (e.Key == SdlInput.Key.F11) {
                if (System.IO.Directory.Exists(IO.Paths.StartupPath + "Screenshots") == false) {
                    System.IO.Directory.CreateDirectory(IO.Paths.StartupPath + "Screenshots");
                }
                int openScreenshot = -1;
                for (int i = 1; i < Int32.MaxValue; i++) {
                    if (System.IO.File.Exists(IO.Paths.StartupPath + "Screenshots/Screenshot" + i + ".png") == false) {
                        openScreenshot = i;
                        break;
                    }
                }
                if (openScreenshot > -1) {
                    Logic.Graphics.SurfaceManager.SaveSurface(SdlDotNet.Graphics.Video.Screen, IO.Paths.StartupPath + "Screenshots/Screenshot" + openScreenshot + ".png");
                    ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                    if (chat != null) {
                        chat.AppendChat("Screenshot #" + openScreenshot + " saved!", System.Drawing.Color.Yellow);
                    }
                }

            } else if (e.Key == SdlInput.Key.F1) {
                if (Ranks.IsAllowed(PlayerManager.MyPlayer, Enums.Rank.Moniter)) {
                    Windows.Editors.EditorManager.AdminPanel.Show();
                    SdlDotNet.Widgets.WindowManager.BringWindowToFront(Windows.Editors.EditorManager.AdminPanel);
                }
                /*
                } else if (e.Key == SdlInput.Key.F2) {
                    Players.Inventory inv = Players.PlayerManager.MyPlayer.Inventory;
                    for (int i = 1; i <= inv.Length; i++) {
                        if (inv[i].Num > 0) {
                            if (Items.ItemHelper.Items[inv[i].Num].Type == Enums.ItemType.PotionAddHP) {
                                ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                                if (chat != null) {
                                    chat.AppendChat("You have used a " + Items.ItemHelper.Items[inv[i].Num].Name + "!", Color.Yellow);
                                }
                                Messenger.SendUseItem(i);
                                break;
                            }
                        }
                    }
                } else if (e.Key == SdlInput.Key.F3) {
                    Players.Inventory inv = Players.PlayerManager.MyPlayer.Inventory;
                    for (int i = 1; i <= inv.Length; i++) {
                        if (inv[i].Num > 0) {
                            if (Items.ItemHelper.Items[inv[i].Num].Type == Enums.ItemType.PotionAddPP) {
                                ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                                if (chat != null) {
                                    chat.AppendChat("You have used a " + Items.ItemHelper.Items[inv[i].Num].Name + "!", Color.Yellow);
                                }
                                Messenger.SendUseItem(i);
                                break;
                            }
                        }
                    }
                */
            }
            //else if (e.Key == SdlInput.Key.F4) {
            //    if (Ranks.IsAllowed(PlayerManager.MyPlayer, Enums.Rank.Moniter)) {
            //        Windows.Editors.EditorManager.GuildPanel.Show();
            //        SdlDotNet.Widgets.WindowManager.BringWindowToFront(Windows.Editors.EditorManager.GuildPanel);
            //    }
            //}
            else if (e.Key == SdlInput.Key.F9) {
                Menus.MenuSwitcher.ShowMenu(new Menus.mnuOnlineList("mnuOnlineList"));
                Network.Messenger.SendOnlineListRequest();
            } else if (e.Key == SdlInput.Key.F10) {
                Menus.MenuSwitcher.ShowMenu(new Menus.mnuBattleLog("mnuBattleLog"));
            } else if (e.Key == SdlInput.Key.Tab) {

            } else if (e.Key == SdlInput.Key.One) {
                Messenger.SendActiveCharSwap(0);
            } else if (e.Key == SdlInput.Key.Two) {
                Messenger.SendActiveCharSwap(1);
            } else if (e.Key == SdlInput.Key.Three) {
                Messenger.SendActiveCharSwap(2);
            } else if (e.Key == SdlInput.Key.Four) {
                Messenger.SendActiveCharSwap(3);
            }
        }

        public static void OnKeyUp(SdlInput.KeyboardEventArgs e) {
            if (e.Key == IO.ControlLoader.UpKey) {
                MoveUp = false;
            } else if (e.Key == IO.ControlLoader.DownKey) {
                MoveDown = false;
            } else if (e.Key == IO.ControlLoader.LeftKey) {
                MoveLeft = false;
            } else if (e.Key == IO.ControlLoader.RightKey) {
                MoveRight = false;
            } else if (e.Key == IO.ControlLoader.AttackKey) {
                Attacking = false;
            }
            switch (e.Key) {
                case SdlInput.Key.Escape: {
                        if (!Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                            Windows.WindowSwitcher.GameWindow.MenuManager.Visible = true;
                            Windows.WindowSwitcher.GameWindow.MenuManager.Focus();
                            Menus.MenuSwitcher.ShowMainMenu();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                        } else {
                            if (Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu == false) {
                                Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
                                Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
                                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                            }
                        }
                    }
                    break;
                //case SdlInput.Key.F5: {
                //        if (!Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                //            Windows.WindowSwitcher.GameWindow.MenuManager.Visible = true;
                //            Windows.WindowSwitcher.GameWindow.MenuManager.Focus();
                //            Menus.MenuSwitcher.ShowGuildMenu();
                //            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                //        }
                //    }
                //    break;
                case SdlInput.Key.Home: {
                        TurnPlayer();
                    }
                    break;
                case SdlInput.Key.End: {
                    if (Globals.Tick > PlayerManager.MyPlayer.GetTimer + 250) {
                        PlayerManager.MyPlayer.GetTimer = Globals.Tick;
                        Messenger.SendRefresh();
                    }
                    }
                    break;
                case SdlInput.Key.Return: {
                        if (PlayerManager.MyPlayer.Y - 1 > -1 && PlayerManager.MyPlayer.X >= 0 && PlayerManager.MyPlayer.X <= Maps.MapHelper.ActiveMap.MaxX && PlayerManager.MyPlayer.Y >= 0 && PlayerManager.MyPlayer.Y <= Maps.MapHelper.ActiveMap.MaxY) {
                            if (Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].Type == Enums.TileType.Sign && PlayerManager.MyPlayer.Direction == Enums.Direction.Up) {
                                ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                                if (chat != null) {
                                    chat.AppendChat("The sign reads:\n", new SdlDotNet.Widgets.CharRenderOptions(Color.Black));
                                    if (!string.IsNullOrEmpty(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String1.Trim())) {
                                        chat.AppendChat(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String1.Trim() + "\n", new SdlDotNet.Widgets.CharRenderOptions(Color.Gray));
                                    }
                                    if (!string.IsNullOrEmpty(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String2.Trim())) {
                                        chat.AppendChat(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String2.Trim() + "\n", new SdlDotNet.Widgets.CharRenderOptions(Color.Gray));
                                    }
                                    if (!string.IsNullOrEmpty(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String3.Trim())) {
                                        chat.AppendChat(Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.X, PlayerManager.MyPlayer.Y - 1].String3.Trim() + "\n", new SdlDotNet.Widgets.CharRenderOptions(Color.Gray));
                                    }
                                }
                            }
                        }
                        Pickup = false;
                    }
                    break;
            }
        }

        public static void MovePlayer(Enums.Direction dir, bool lastMoved) {
            if (!Globals.GettingMap && !PlayerManager.MyPlayer.MovementLocked && !Stories.StoryProcessor.loadingStory && !PlayerManager.MyPlayer.Dead) {
                if (PlayerManager.MyPlayer.Confused) {
                    dir = (Enums.Direction)(Logic.MathFunctions.Random.Next(0, 4));
                }
                //dir = Enums.Direction.Up;
                //if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.TurnKey)) {
                //    Players.PlayerManager.MyPlayer.Direction = dir;
                //    Messenger.SendPlayerDir();
                //} else 
                if (GameProcessor.CanMove(dir)) {
                    MyPlayer player = PlayerManager.MyPlayer;
                    
                    if (Globals.RefreshLock == false) {
                        if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.RunKey) || SdlInput.Keyboard.IsKeyPressed(SdlInput.Key.RightShift)) {
                            player.MovementSpeed = PlayerManager.MyPlayer.SpeedLimit;
                            
                        } else {
                            player.MovementSpeed = Enums.MovementSpeed.Walking;
                            
                            if (player.MovementSpeed > PlayerManager.MyPlayer.SpeedLimit) {
                                player.MovementSpeed = PlayerManager.MyPlayer.SpeedLimit;
                            }
                        }

                        if (Maps.MapHelper.ActiveMap.Tile[player.Location.X, player.Location.Y].Type == Enums.TileType.Slippery && lastMoved) {
                            player.MovementSpeed = Enums.MovementSpeed.Slip;
                        }
                        if (Maps.MapHelper.ActiveMap.Tile[player.Location.X, player.Location.Y].Type == Enums.TileType.Slow) {
                            int mobilityList = Maps.MapHelper.ActiveMap.Tile[player.Location.X, player.Location.Y].Data1;
                            bool slow = false;
                            for (int i = 0; i < 16; i++) {
                                if (mobilityList % 2 == 1 && !PlayerManager.MyPlayer.Mobility[i]) {
                                    slow = true;
                                    break;
                                }
                                mobilityList /= 2;
                            }
                            if (slow && player.MovementSpeed > (Enums.MovementSpeed)Maps.MapHelper.ActiveMap.Tile[player.Location.X, player.Location.Y].Data2) {
                                player.MovementSpeed = (Enums.MovementSpeed)Maps.MapHelper.ActiveMap.Tile[player.Location.X, player.Location.Y].Data2;
                            }
                        }

                        int x = player.X;
                        int y = player.Y;
                        switch (player.Direction) {
                            case Enums.Direction.Up:
                                if (GameProcessor.CheckLocked(dir)) {
                                    Messenger.SendPlayerCriticalMove();
                                    player.MovementLocked = true;
                                } else {
                                    Messenger.SendPlayerMove();
                                }
                                y -= 1;

                                if (y < 0 && Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Up)) {
                                    Maps.Map oldActive = Maps.MapHelper.Maps[Enums.MapID.Active];
                                    Maps.MapHelper.Maps[Enums.MapID.Active] = Maps.MapHelper.Maps[Enums.MapID.Up];
                                    Maps.MapHelper.Maps[Enums.MapID.Down] = oldActive;
                                    player.MapID = Maps.MapHelper.Maps[Enums.MapID.Active].MapID;
                                    player.Y = Maps.MapHelper.Maps[Enums.MapID.Active].MaxY + 1;

                                    Maps.MapHelper.Maps[Enums.MapID.BottomLeft] = Maps.MapHelper.Maps[Enums.MapID.Left];
                                    Maps.MapHelper.Maps[Enums.MapID.Left] = Maps.MapHelper.Maps[Enums.MapID.TopLeft];

                                    Maps.MapHelper.Maps[Enums.MapID.BottomRight] = Maps.MapHelper.Maps[Enums.MapID.Right];
                                    Maps.MapHelper.Maps[Enums.MapID.Right] = Maps.MapHelper.Maps[Enums.MapID.TopRight];

                                    

                                    Maps.MapHelper.HandleMapDone();
                                }

                                if (Maps.MapHelper.ActiveMap.Tile[player.X, player.Y - 1].Type == Enums.TileType.Warp) {
                                    Globals.GettingMap = true;
                                }
                                if (Globals.GettingMap == false) {
                                    player.Offset = new System.Drawing.Point(player.Offset.X, Constants.TILE_HEIGHT);
                                    player.Y -= 1;
                                } else {
                                    player.MovementSpeed = Enums.MovementSpeed.Standing;
                                }
                                break;
                            case Enums.Direction.Down:
                                if (GameProcessor.CheckLocked(dir)) {
                                    Messenger.SendPlayerCriticalMove();
                                    player.MovementLocked = true;
                                } else {
                                    Messenger.SendPlayerMove();
                                }
                                y += 1;

                                if (y > Maps.MapHelper.Maps[Enums.MapID.Active].MaxY && Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Down)) {
                                    Maps.Map oldActive = Maps.MapHelper.Maps[Enums.MapID.Active];

                                    Maps.MapHelper.Maps[Enums.MapID.Active] = Maps.MapHelper.Maps[Enums.MapID.Down];
                                    Maps.MapHelper.Maps[Enums.MapID.Up] = oldActive;
                                    player.MapID = Maps.MapHelper.Maps[Enums.MapID.Active].MapID;
                                    player.Y = -1;

                                    Maps.MapHelper.Maps[Enums.MapID.TopLeft] = Maps.MapHelper.Maps[Enums.MapID.Left];
                                    Maps.MapHelper.Maps[Enums.MapID.Left] = Maps.MapHelper.Maps[Enums.MapID.BottomLeft];

                                    Maps.MapHelper.Maps[Enums.MapID.TopRight] = Maps.MapHelper.Maps[Enums.MapID.Right];
                                    Maps.MapHelper.Maps[Enums.MapID.Right] = Maps.MapHelper.Maps[Enums.MapID.BottomRight];

                                    


                                   Maps.MapHelper.HandleMapDone();
                                }

                                if (Maps.MapHelper.ActiveMap.Tile[player.X, player.Y + 1].Type == Enums.TileType.Warp) {
                                    Globals.GettingMap = true;
                                }
                                if (Globals.GettingMap == false) {
                                    player.Offset = new System.Drawing.Point(player.Offset.X, Constants.TILE_HEIGHT * -1);
                                    player.Y += 1;
                                } else {
                                    player.MovementSpeed = Enums.MovementSpeed.Standing;
                                }
                                break;
                            case Enums.Direction.Left:
                                if (GameProcessor.CheckLocked(dir)) {
                                    Messenger.SendPlayerCriticalMove();
                                    player.MovementLocked = true;
                                } else {
                                    Messenger.SendPlayerMove();
                                }
                                x -= 1;

                                if (x < 0 && Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Left)) {
                                    Maps.Map oldActive = Maps.MapHelper.Maps[Enums.MapID.Active];
                                    Maps.MapHelper.Maps[Enums.MapID.Active] = Maps.MapHelper.Maps[Enums.MapID.Left];

                                    player.MapID = Maps.MapHelper.Maps[Enums.MapID.Active].MapID;

                                    Maps.MapHelper.Maps[Enums.MapID.TopRight] = Maps.MapHelper.Maps[Enums.MapID.Up];
                                    Maps.MapHelper.Maps[Enums.MapID.BottomRight] = Maps.MapHelper.Maps[Enums.MapID.Down];

                                    Maps.MapHelper.Maps[Enums.MapID.Up] = Maps.MapHelper.Maps[Enums.MapID.TopLeft];
                                    Maps.MapHelper.Maps[Enums.MapID.Down] = Maps.MapHelper.Maps[Enums.MapID.BottomLeft];

                                    Maps.MapHelper.Maps[Enums.MapID.Right] = oldActive;

                                    player.X = Maps.MapHelper.Maps[Enums.MapID.Active].MaxX + 1;


                                    Maps.MapHelper.HandleMapDone();
                                }

                                if (Maps.MapHelper.ActiveMap.Tile[player.X - 1, player.Y].Type == Enums.TileType.Warp) {
                                    Globals.GettingMap = true;
                                }
                                if (Globals.GettingMap == false) {
                                    player.Offset = new System.Drawing.Point(Constants.TILE_WIDTH, player.Offset.Y);
                                    player.X -= 1;
                                } else {
                                    player.MovementSpeed = Enums.MovementSpeed.Standing;
                                }
                                break;
                            case Enums.Direction.Right:
                                if (GameProcessor.CheckLocked(dir)) {
                                    Messenger.SendPlayerCriticalMove();
                                    player.MovementLocked = true;
                                } else {
                                    Messenger.SendPlayerMove();
                                }
                                x += 1;

                                if (x > Maps.MapHelper.Maps[Enums.MapID.Active].MaxX && Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Right)) {
                                    Maps.Map oldActive = Maps.MapHelper.Maps[Enums.MapID.Active];

                                    Maps.MapHelper.Maps[Enums.MapID.Active] = Maps.MapHelper.Maps[Enums.MapID.Right];

                                    player.MapID = Maps.MapHelper.Maps[Enums.MapID.Active].MapID;

                                    Maps.MapHelper.Maps[Enums.MapID.TopLeft] = Maps.MapHelper.Maps[Enums.MapID.Up];
                                    Maps.MapHelper.Maps[Enums.MapID.BottomLeft] = Maps.MapHelper.Maps[Enums.MapID.Down];

                                    Maps.MapHelper.Maps[Enums.MapID.Up] = Maps.MapHelper.Maps[Enums.MapID.TopRight];
                                    Maps.MapHelper.Maps[Enums.MapID.Down] = Maps.MapHelper.Maps[Enums.MapID.BottomRight];

                                    Maps.MapHelper.Maps[Enums.MapID.Left] = oldActive;

                                    player.X = -1;

                                    Maps.MapHelper.HandleMapDone();
                                }

                                if (Maps.MapHelper.ActiveMap.Tile[player.X + 1, player.Y].Type == Enums.TileType.Warp) {
                                    Globals.GettingMap = true;
                                }
                                if (Globals.GettingMap == false) {
                                    player.Offset = new System.Drawing.Point(Constants.TILE_WIDTH * -1, player.Offset.Y);
                                    player.X += 1;
                                } else {
                                    player.MovementSpeed = Enums.MovementSpeed.Standing;
                                }
                                break;
                        }

                        Logic.Graphics.Renderers.Screen.ScreenRenderer.DeactivateOffscreenSprites();

                        if (player.ID == PlayerManager.MyPlayer.ID) {
                            //PlayerManager.MyPlayer.SetCurrentRoom();
                        }
                        if (player.MovementSpeed > Enums.MovementSpeed.Standing && player.WalkingFrame == -1) {
                            player.WalkingFrame = 0;
                            player.LastWalkTime = Globals.Tick;
                        }
                    }
                }
            }
        }

        private static void TurnPlayer() {
            if (!Globals.GettingMap && !PlayerManager.MyPlayer.MovementLocked && !Stories.StoryProcessor.loadingStory && !PlayerManager.MyPlayer.Dead
                && PlayerManager.MyPlayer.MovementSpeed == Enums.MovementSpeed.Standing) {
                if (PlayerManager.MyPlayer.Confused) {
                    Players.PlayerManager.MyPlayer.Direction = (Enums.Direction)(Logic.MathFunctions.Random.Next(0, 4));
                } else if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.UpKey)) {
                    Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Up;
                } else if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.DownKey)) {
                    Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Down;
                } else if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.LeftKey)) {
                    Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Left;
                } else if (SdlInput.Keyboard.IsKeyPressed(IO.ControlLoader.RightKey)) {
                    Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Right;
                } else {
                    switch (Players.PlayerManager.MyPlayer.Direction) {
                        case Enums.Direction.Up: {
                                Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Right;
                            }
                            break;
                        case Enums.Direction.Down: {
                                Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Left;
                            }
                            break;
                        case Enums.Direction.Left: {
                                Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Up;
                            }
                            break;
                        case Enums.Direction.Right: {
                                Players.PlayerManager.MyPlayer.Direction = Enums.Direction.Down;
                            }
                            break;
                    }
                }
            }

            Messenger.SendPlayerDir();
        }

        public static void PickupItem() {
            if (Globals.Tick > PlayerManager.MyPlayer.GetTimer + 250) {
                PlayerManager.MyPlayer.GetTimer = Globals.Tick;
                Messenger.SendPickupItem();
            }
        }

        public void VerifyKeys() {
            //if (SdlInput.Keyboard.IsKeyPressed(AttackKey) == false
        }

        #endregion Methods
    }
}