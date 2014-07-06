/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 28/09/2009
 * Time: 1:39 PM
 * 
 */
using System;
using Client.Logic.Network;
using Client.Logic.Players;
using PMU.Sockets;

namespace Client.Logic
{
    /// <summary>
    /// Description of GameProcessor.
    /// </summary>
    internal class GameProcessor
    {

        #region Movement
        public static bool CanMove(Enums.Direction dirToTest) {
            Maps.Map map = Maps.MapHelper.ActiveMap;
            Players.MyPlayer player = Players.PlayerManager.MyPlayer;

            Enums.Direction dir = player.Direction;
            int x, y;

            //Make sure they aren't asleep, or frozen
            if (player.StatusAilment == Enums.StatusAilment.Freeze || player.StatusAilment == Enums.StatusAilment.Sleep) {

                return false;
            }

            // Make sure they aren't trying to move when they are already moving
            if (player.MovementSpeed != Enums.MovementSpeed.Standing || Globals.GettingMap) {
                return false;
            }



            player.Direction = dirToTest;

            //make sure they aren't exceeding speed limit
            if (player.SpeedLimit == Enums.MovementSpeed.Standing && map.Tile[player.Location.X, player.Location.Y].Type != Enums.TileType.Slippery) {
                Messenger.SendPlayerDir();
                return false;
            }



            //test against post-attack cooldown
            if (!(Players.PlayerManager.MyPlayer.PauseTimer < Globals.Tick)) {
                Messenger.SendPlayerDir();
                return false;
            }

            x = player.Location.X;
            y = player.Location.Y;



            switch (dirToTest) {
                case Enums.Direction.Up:
                    x = player.Location.X;
                    y = player.Location.Y - 1;

                    player.Direction = Enums.Direction.Up;
                    if (y < 0) {
                        if (map.Up > 0 && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                            if (Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Up)) {
                                return true;
                            } else {
                                Messenger.SendPlayerRequestNewMap(false);
                                Globals.GettingMap = true;
                                return false;
                            }
                        } else {
                            return false;
                        }
                    }
                    break;
                case Enums.Direction.Down:
                    x = player.Location.X;
                    y = player.Location.Y + 1;

                    if (y > map.MaxY) {
                        if (map.Down > 0 && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                            if (Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Down)) {
                                return true;
                            } else {
                                Messenger.SendPlayerRequestNewMap(false);
                                Globals.GettingMap = true;
                                return false;
                            }
                        } else {
                            return false;
                        }
                    }
                    break;
                case Enums.Direction.Left:
                    x = player.Location.X - 1;
                    y = player.Location.Y;

                    if (x < 0) {
                        if (map.Left > 0 && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                            if (Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Left)) {
                                return true;
                            } else {
                                Messenger.SendPlayerRequestNewMap(false);
                                Globals.GettingMap = true;
                                return false;
                            }
                        } else {
                            return false;
                        }
                    }
                    break;
                case Enums.Direction.Right:
                    x = player.Location.X + 1;
                    y = player.Location.Y;

                    if (x > map.MaxX) {
                        if (map.Right > 0 && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                            if (Logic.Graphics.Renderers.Maps.SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Right)) {
                                return true;
                            } else {
                                Messenger.SendPlayerRequestNewMap(false);
                                Globals.GettingMap = true;
                                return false;
                            }
                        } else {
                            return false;
                        }
                    }
                    break;
            }

            if (x < 0 || x > map.MaxX || y < 0 || y > map.MaxY) {
                return false;
            }

            Maps.Tile tile = map.Tile[x, y];

            switch (tile.Type) {
                case Enums.TileType.Blocked:
                case Enums.TileType.Sign:
                case Enums.TileType.ScriptedSign:
                    if (dir != dirToTest) {
                        Messenger.SendPlayerDir();
                    }
                    return false;
                case Enums.TileType.Warp: {
                        if (Windows.WindowSwitcher.GameWindow.inMapEditor) {
                            if (dir != dirToTest) {
                                Messenger.SendPlayerDir();
                            }
                            return false;
                        }
                    }
                    break;
                case Enums.TileType.LevelBlock:
                    if (player.Level > tile.Data1) {
                        return true;
                    } else {
                        if (dir != dirToTest) {
                            Messenger.SendPlayerDir();
                        }
                        return false;
                    }
                case Enums.TileType.Story:
                    Messenger.SendPacket(TcpPacket.CreatePacket("isstory", x.ToString(), y.ToString()));
                    if (dir != dirToTest) {
                        Messenger.SendPlayerDir();
                    }
                    return true;
                case Enums.TileType.SpriteBlock:
                    if (tile.Data1 == 1) {
                        if (player.Sprite == tile.Data2 || player.Sprite == tile.Data3) {
                            return true;
                        } else {
                            if (dir != dirToTest) {
                                Messenger.SendPlayerDir();
                            }
                            return false;
                        }
                    } else if (tile.Data1 == 2) {
                        if (player.Sprite != tile.Data2 || player.Sprite != tile.Data3) {
                            return true;
                        } else {
                            if (dir != dirToTest) {
                                Messenger.SendPlayerDir();
                            }
                            return false;
                        }
                    } else {
                        if (dir != dirToTest) {
                            Messenger.SendPlayerDir();
                        }
                        return false;
                    }
                case Enums.TileType.MobileBlock: {
                        int mobilityList = tile.Data1;
                        for (int i = 0; i < 16; i++) {
                            if (mobilityList % 2 == 1 && !player.Mobility[i]) {
                                if (dir != dirToTest) {
                                    Messenger.SendPlayerDir();
                                }
                                return false;
                            }
                            mobilityList /= 2;
                        }
                        // use mobility
                        return true;


                    }

                //if (tile.Data1 == player.GetActiveRecruit().Class) {
                //    return true;
                //} else if (tile.Data2 == player.GetActiveRecruit().Class) {
                //    return true;
                //} else if (tile.Data3 == player.GetActiveRecruit().Class) {
                //    return true;
                //} else {
                //    if (dir != dirToTest) {
                //        Tcp.Messenger.SendPlayerDir();
                //    }
                //    return false;
                //}
                case Enums.TileType.Key:
                case Enums.TileType.Door:
                    if (tile.DoorOpen == false) {
                        if (dir != dirToTest) {
                            Messenger.SendPlayerDir();
                        }
                        return false;
                    }
                    break;
                case Enums.TileType.LinkShop:
                case Enums.TileType.Assembly:
                case Enums.TileType.Guild:
                case Enums.TileType.Bank:
                case Enums.TileType.Shop:
                    //player.MovementLocked = true;
                    return true;

            }

            foreach (IPlayer playerToTest in PlayerManager.Players.GetAllPlayers()) {
                if (playerToTest.MapID == map.MapID && playerToTest.ScreenActive) {
                    if (playerToTest.Location.X == x && playerToTest.Location.Y == y) {
                        if (player.Solid == true) {
                            if (dir != dirToTest) {
                                Messenger.SendPlayerDir();
                            }
                            return false;
                        }
                    }
                }
            }

            for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                if (map.MapNpcs[i].Num > 0 && map.MapNpcs[i].ScreenActive) {
                    if (map.MapNpcs[i].Location.X == x && map.MapNpcs[i].Location.Y == y) {
                        if (dir != dirToTest) {
                            Messenger.SendPlayerDir();
                        }
                        return false;
                    }
                }
            }

            if (dir != dirToTest) {
                Messenger.SendPlayerDir();
            }
            return true;
        }

        public static bool CheckLocked(Enums.Direction dirToTest) {
            Maps.Map map = Maps.MapHelper.ActiveMap;
            Players.MyPlayer player = Players.PlayerManager.MyPlayer;

            Enums.Direction dir = player.Direction;
            int x = 0, y = 0;

            

            switch (dirToTest) {
                case Enums.Direction.Up:
                    x = player.Location.X;
                    y = player.Location.Y - 1;

                    break;
                case Enums.Direction.Down:
                    x = player.Location.X;
                    y = player.Location.Y + 1;

                    
                    break;
                case Enums.Direction.Left:
                    x = player.Location.X - 1;
                    y = player.Location.Y;

                    break;
                case Enums.Direction.Right:
                    x = player.Location.X + 1;
                    y = player.Location.Y;

                    break;
            }

            if (x < 0 || x > map.MaxX || y < 0 || y > map.MaxY) {
                return false;
            }

            Maps.Tile tile = map.Tile[x, y];

            switch (tile.Type) {
                case Enums.TileType.Story:
                case Enums.TileType.LinkShop:
                case Enums.TileType.Assembly:
                case Enums.TileType.Guild:
                case Enums.TileType.Bank:
                case Enums.TileType.Shop:
                case Enums.TileType.MissionBoard:
                case Enums.TileType.RDungeonGoal:
                case Enums.TileType.Evolution:
                case Enums.TileType.SpriteChange:
                case Enums.TileType.Warp:
                case Enums.TileType.Scripted:
                    
                    return true;

            }

            return false;
        }

        //public static void ProcessMovement(int index) {
        //    // Check if player is walking, and if so process moving them over
        //    Players.Player player = Players.PlayerHelper.Players[index];
        //    if (player.MovementSpeed == Enums.MovementSpeed.Walking) {
        //        switch (player.Direction) {
        //            case Enums.Direction.Up:
        //                player.Offset = new System.Drawing.Point(player.Offset.X, player.Offset.Y - Constants.WALK_SPEED);
        //                break;
        //            case Enums.Direction.Down:
        //                player.Offset = new System.Drawing.Point(player.Offset.X, player.Offset.Y + Constants.WALK_SPEED)
        //                break;
        //            case Enums.Direction.Left:
        //                player.Offset = new System.Drawing.Point(player.Offset.X - Constants.WALK_SPEED, player.Offset.Y);
        //                break;
        //            case Enums.Direction.Right:
        //                player.Offset = new System.Drawing.Point(player.Offset.X + Constants.WALK_SPEED, player.Offset.Y);
        //                break;
        //        }

        //        // Check if completed walking over to the next tile
        //        if ((player.Offset.X == 0) && (player.Offset.Y == 0)) {
        //            player.MovementSpeed = Enums.MovementSpeed.Standing;
        //        }
        //    } else if (player.MovementSpeed == Enums.MovementSpeed.Running) {
        //        switch (player.Direction) {
        //            case Enums.Direction.Up:
        //                player.Offset = new System.Drawing.Point(player.Offset.X, player.Offset.Y - Constants.RUN_SPEED);
        //                break;
        //            case Enums.Direction.Down:
        //                player.Offset = new System.Drawing.Point(player.Offset.X, player.Offset.Y + Constants.RUN_SPEED)
        //                break;
        //            case Enums.Direction.Left:
        //                player.Offset = new System.Drawing.Point(player.Offset.X - Constants.RUN_SPEED, player.Offset.Y);
        //                break;
        //            case Enums.Direction.Right:
        //                player.Offset = new System.Drawing.Point(player.Offset.X + Constants.RUN_SPEED, player.Offset.Y);
        //                break;
        //        }

        //        // Check if completed walking over to the next tile
        //        if ((player.Offset.X == 0) && (player.Offset.Y == 0)) {
        //            player.MovementSpeed = Enums.MovementSpeed.Standing;
        //        }
        //    }
        //}

        //public static void ProcessNpcMovement(Maps.Map activeMap, int npcSlot) {
        //    // Check if npc is walking, and if so process moving them over
        //    if (activeMap.MapNpcs[npcSlot].Moving == Enums.MovementSpeed.Walking) {
        //        switch (activeMap.MapNpcs[npcSlot].Direction) {
        //            case Enums.Direction.Up:
        //                activeMap.MapNpcs[npcSlot].YOffset -= Constants.WALK_SPEED;
        //                break;
        //            case Enums.Direction.Down:
        //                activeMap.MapNpcs[npcSlot].YOffset += Constants.WALK_SPEED;
        //                break;
        //            case Enums.Direction.Left:
        //                activeMap.MapNpcs[npcSlot].XOffset -= Constants.WALK_SPEED;
        //                break;
        //            case Enums.Direction.Right:
        //                activeMap.MapNpcs[npcSlot].XOffset += Constants.WALK_SPEED;
        //                break;
        //        }

        //        if ((activeMap.MapNpcs[npcSlot].Offset.X == 0) && activeMap.MapNpcs[npcSlot].Offset.Y == 0) {
        //            activeMap.MapNpcs[npcSlot].Moving = Enums.MovementSpeed.Standing;
        //        }
        //    }
        //}

        public static void ProcessSpriteMovement(Graphics.Renderers.Sprites.ISprite sprite) {
            // Check if sprite is walking, and if so process moving them over
            if (sprite.MovementSpeed != Enums.MovementSpeed.Standing) {
                //if (sprite.Offset.X > 0) {
                //    sprite.Direction = Enums.Direction.Left;
                //} else if (sprite.Offset.X < 0) {
                //    sprite.Direction = Enums.Direction.Right;
                //} else if (sprite.Offset.Y > 0) {
                //    sprite.Direction = Enums.Direction.Up;
                //} else if (sprite.Offset.X < 0) {
                //    sprite.Direction = Enums.Direction.Down;
                //}
                switch (sprite.Direction) {
                    case Enums.Direction.Up:
                        sprite.Offset = new System.Drawing.Point(sprite.Offset.X, sprite.Offset.Y - DetermineSpeed(sprite.MovementSpeed));
                        if (sprite.Offset.Y < 0) sprite.Offset = new System.Drawing.Point(0, 0);
                        break;
                    case Enums.Direction.Down:
                        sprite.Offset = new System.Drawing.Point(sprite.Offset.X, sprite.Offset.Y + DetermineSpeed(sprite.MovementSpeed));
                        if (sprite.Offset.Y > 0) sprite.Offset = new System.Drawing.Point(0, 0);
                        break;
                    case Enums.Direction.Left:
                        sprite.Offset = new System.Drawing.Point(sprite.Offset.X - DetermineSpeed(sprite.MovementSpeed), sprite.Offset.Y);
                        if (sprite.Offset.X < 0) sprite.Offset = new System.Drawing.Point(0, 0);
                        break;
                    case Enums.Direction.Right:
                        sprite.Offset = new System.Drawing.Point(sprite.Offset.X + DetermineSpeed(sprite.MovementSpeed), sprite.Offset.Y);
                        if (sprite.Offset.X > 0) sprite.Offset = new System.Drawing.Point(0, 0);
                        break;
                }

                // Check if completed walking over to the next tile
                if ((sprite.Offset.X == 0 && sprite.Offset.Y == 0)
                    /*|| sprite.Offset.X > Constants.TILE_WIDTH || sprite.Offset.X < -Constants.TILE_WIDTH
                    || sprite.Offset.Y > Constants.TILE_HEIGHT || sprite.Offset.Y < -Constants.TILE_HEIGHT*/) {
                    Enums.MovementSpeed speed = sprite.MovementSpeed;
                    sprite.MovementSpeed = Enums.MovementSpeed.Standing;
                    sprite.Leaving = false;
                    if (sprite == PlayerManager.MyPlayer) {
                        if (Maps.MapHelper.Maps[Enums.MapID.Active].Tile[PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y].Type == Enums.TileType.Slippery) {
                            int mobilityList = Maps.MapHelper.ActiveMap.Tile[PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y].Data1;
                            bool slip = false;
                            for (int i = 0; i < 16; i++) {
                                if (mobilityList % 2 == 1 && !PlayerManager.MyPlayer.Mobility[i]) {
                                    slip = true;
                                    break;
                                }
                                mobilityList /= 2;
                            }
                            if (speed > Enums.MovementSpeed.Walking) slip = true;
                            if (slip) {
                                if (speed != Enums.MovementSpeed.Slip) {
                                    int distance = GetSlipperyDistance(PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y, PlayerManager.MyPlayer.Direction);
                                    //if (distance > 12) {
                                    //    Music.Music.AudioPlayer.PlaySoundEffect("magic779.wav");
                                    //} else 
                                    if (distance > 6) {
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic762.wav");
                                    } else if (distance > 0) {
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic628.wav");
                                    }
                                }
                                Input.InputProcessor.MovePlayer(PlayerManager.MyPlayer.Direction, true);
                            }
                        } else {
                            if (Input.InputProcessor.MoveUp) {
                                Input.InputProcessor.MovePlayer(Enums.Direction.Up, false);
                            } else if (Input.InputProcessor.MoveDown) {
                                Input.InputProcessor.MovePlayer(Enums.Direction.Down, false);
                            } else if (Input.InputProcessor.MoveLeft) {
                                Input.InputProcessor.MovePlayer(Enums.Direction.Left, false);
                            } else if (Input.InputProcessor.MoveRight) {
                                Input.InputProcessor.MovePlayer(Enums.Direction.Right, false);
                            }
                        }
                        if (Maps.MapHelper.Maps[Enums.MapID.Active].Tile[PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y].Type == Enums.TileType.Scripted) {
                            PlayerManager.MyPlayer.CurrentEmote = new Client.Logic.Graphics.Renderers.Sprites.Emoticon(13, 2, 1);
                        }
                    }
                }
            } else {
                sprite.WalkingFrame = -1;
            }
            if (Input.InputProcessor.Attacking && !PlayerManager.MyPlayer.Attacking) {
                CheckAttack();
            }
        }

        public static int DetermineSpeed(Enums.MovementSpeed speed) {
            switch (speed) {
                case (Enums.MovementSpeed.Standing): {
                        return 0;
                    }
                case (Enums.MovementSpeed.SuperSlow): {
                        return 2;
                    }
                case (Enums.MovementSpeed.Slow): {
                        return 3;
                    }
                case (Enums.MovementSpeed.Walking): {
                        return 4;
                    }
                case (Enums.MovementSpeed.Running): {
                        return 8;
                    }
                case (Enums.MovementSpeed.Fast): {
                        return 16;
                    }
                case (Enums.MovementSpeed.SuperFast): {
                        return 24;
                    }
                case (Enums.MovementSpeed.Slip): {
                        return 32;
                    }
                default: {
                        return 4;
                    }
            }
        }

        public static bool IsBlocked(Maps.Map map, int x, int y) {
            switch (map.Tile[x, y].Type) {
                case Enums.TileType.Blocked:
                case Enums.TileType.Sign:
                case Enums.TileType.ScriptedSign:
                case Enums.TileType.LevelBlock:
                case Enums.TileType.SpriteBlock:
                case Enums.TileType.MobileBlock:
                    return true;
                default:
                    return false;
            }
        }

        public static int GetSlipperyDistance(int x, int y, Enums.Direction dir) {
            int checkedX = x;
            int checkedY = y;
            for (int i = 1; i < 100; i++) {
                switch (dir) {
                    case Enums.Direction.Up: {
                        checkedY--;
                        }
                        break;
                    case Enums.Direction.Down: {
                        checkedY++;
                        }
                        break;
                    case Enums.Direction.Left: {
                        checkedX--;
                        }
                        break;
                    case Enums.Direction.Right: {
                        checkedX++;
                        }
                        break;
                }
                if (checkedX >= 0 && checkedX <= Maps.MapHelper.ActiveMap.MaxX && checkedY >= 0 && checkedY <= Maps.MapHelper.ActiveMap.MaxY
                    && Maps.MapHelper.ActiveMap.Tile[checkedX, checkedY].Type == Enums.TileType.Slippery) {
                    
                } else {
                    return i - 1;
                    break;
                }
            }
            return 100;
        }

        #endregion


        #region Attacking

        public static void CheckAttack() {
            if (Players.PlayerManager.MyPlayer.Dead || Players.PlayerManager.MyPlayer.MovementSpeed == Enums.MovementSpeed.Slip ||
                Maps.MapHelper.Maps[Enums.MapID.Active].Tile[PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y].Type == Enums.TileType.Slippery) {
                return;
            }
            int attackSpeed = 0;
            //if (Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot > 0)
            //{

            //    attackSpeed = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot)].AttackSpeed;
            //} else {
                attackSpeed = 1000;
            //}

            attackSpeed = attackSpeed * Players.PlayerManager.MyPlayer.TimeMultiplier / 1000;

            if (Players.PlayerManager.MyPlayer.AttackTimer < Globals.Tick && Players.PlayerManager.MyPlayer.Attacking == false) {
                Players.PlayerManager.MyPlayer.Attacking = true;
                Players.PlayerManager.MyPlayer.AttackTimer = Globals.Tick + attackSpeed;
                Players.PlayerManager.MyPlayer.TotalAttackTime = attackSpeed;
                //Players.PlayerManager.MyPlayer.PauseTimer = Globals.Tick + attackSpeed;
                Messenger.SendAttack();
            }
        }

        public static void CheckUseItem(int teamSlot, int itemSlot) {
            if (Players.PlayerManager.MyPlayer.Dead || Players.PlayerManager.MyPlayer.MovementSpeed == Enums.MovementSpeed.Slip ||
                Maps.MapHelper.Maps[Enums.MapID.Active].Tile[PlayerManager.MyPlayer.Location.X, PlayerManager.MyPlayer.Location.Y].Type == Enums.TileType.Slippery) {
                return;
            }
            int attackSpeed = 0;
            //if (Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot > 0)
            //{

            //    attackSpeed = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot)].AttackSpeed;
            //} else {
            attackSpeed = 1000;
            //}

            attackSpeed = attackSpeed * Players.PlayerManager.MyPlayer.TimeMultiplier / 1000;

            if (Players.PlayerManager.MyPlayer.AttackTimer < Globals.Tick && Players.PlayerManager.MyPlayer.Attacking == false) {
                Players.PlayerManager.MyPlayer.Attacking = true;
                Players.PlayerManager.MyPlayer.AttackTimer = Globals.Tick + attackSpeed;
                Players.PlayerManager.MyPlayer.TotalAttackTime = attackSpeed;
                Players.PlayerManager.MyPlayer.PauseTimer = Globals.Tick + attackSpeed;
                Messenger.SendUseItem(PlayerManager.MyPlayer.Team[teamSlot].HeldItemSlot);
            }
        }

        #endregion
    }
}
