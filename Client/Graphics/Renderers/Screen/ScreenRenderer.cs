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
using SdlDotNet.Graphics;
using System.Drawing;
using Client.Logic.Players;
using Client.Logic.Graphics.Renderers.Maps;
using Client.Logic.Graphics.Renderers.Items;
using Client.Logic.Graphics.Renderers.Npcs;
using Client.Logic.Graphics.Renderers.Players;
using Client.Logic.Algorithms.Pathfinder;

namespace Client.Logic.Graphics.Renderers.Screen
{
    class ScreenRenderer
    {
        public static int MapYOffset;
        public static int MapXOffset;

        static ScreenRenderOptions renderOptions;

        public static ScreenRenderOptions RenderOptions {
            get { return renderOptions; }
        }

        static Camera camera;

        static System.Diagnostics.Stopwatch RenderStopwatch = new System.Diagnostics.Stopwatch();

        public static Camera Camera {
            get { return camera; }
        }

        public static void Initialize() {
            camera = new Camera();
            Moves.MoveRenderer.Initialize();
            Sprites.SpriteRenderer.Initialize();
            renderOptions = new ScreenRenderOptions();
        }

        public static void RenderScreen(RendererDestinationData destData) {
            try {
                //System.Diagnostics.Debug.WriteLine("Elapsed " + (SdlDotNet.Core.Timer.TicksElapsed - Globals.Tick));
                Globals.Tick = SdlDotNet.Core.Timer.TicksElapsed;
                //dstSurf.Fill(Color.Blue);

                //Maps.Map renderOptions.Map = Maps.MapHelper.renderOptions.Map;

                //Windows.WindowSwitcher.GameWindow.mInput.VerifyKeys();

                if (renderOptions.Map != null && renderOptions.Map.Loaded) {
                    //Players.Player player = Players.PlayerHelper.Players.GetMyPlayer();
                    if (camera.FocusObject == null) {
                        camera.FocusOnSprite(PlayerManager.MyPlayer);
                    } else {
                        camera.FocusObject.Process(Globals.Tick);

                        camera.FocusOnFocusObject(camera.FocusObject);
                    }

                    // Get the camera coordinates
                    camera.X = GetScreenLeft() - 1;
                    camera.Y = GetScreenTop();
                    camera.X2 = GetScreenRight();
                    camera.Y2 = GetScreenBottom() + 1;
                    // Verify that the coordinates aren't outside the map bounds
                    if (camera.X < 0 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Left)) {
                        camera.X = 0;
                        camera.X2 = 20;
                    } else if (camera.X2 > renderOptions.Map.MaxX + 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Right)) {
                        camera.X = renderOptions.Map.MaxX - 19;
                        camera.X2 = renderOptions.Map.MaxX + 1;
                    }
                    if (camera.Y < 0 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Up)) {
                        camera.Y = 0;
                        camera.Y2 = 15;
                    } else if (camera.Y2 > renderOptions.Map.MaxY + 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Down)) {
                        camera.Y = renderOptions.Map.MaxY - 14;
                        camera.Y2 = renderOptions.Map.MaxY + 1;
                    }

                    MapXOffset = camera.FocusedXOffset;
                    MapYOffset = camera.FocusedYOffset;

                    if (camera.FocusedY - 6 <= 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Up)) {
                        int subAmount = 6;
                        if (camera.FocusedDirection == Enums.Direction.Up) {
                            subAmount--;
                        }
                        if (camera.FocusedY - subAmount <= 1 || renderOptions.Map.MaxY == 14) {
                            MapYOffset = 0;
                        }
                    } else if (camera.FocusedY + 10 > renderOptions.Map.MaxY + 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Down)) {
                        int plusAmount = 9;
                        if (camera.FocusedDirection == Enums.Direction.Down) {
                            plusAmount--;
                        }
                        if (camera.FocusedY + plusAmount > renderOptions.Map.MaxY + 1 || renderOptions.Map.MaxY == 14) {
                            MapYOffset = 0;
                        }
                    }

                    if (camera.FocusedX - 9 <= 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Left)) {
                        int subAmount = 9;
                        if (camera.FocusedDirection == Enums.Direction.Left) {
                            subAmount--;
                        }
                        if (camera.FocusedX - subAmount <= 1 || renderOptions.Map.MaxX == 19) {
                            MapXOffset = 0;
                        }
                    } else if (camera.FocusedX + 11 > renderOptions.Map.MaxX + 1 && !SeamlessWorldHelper.IsMapSeamless(Enums.MapID.Right)) {
                        int plusAmount = 10;
                        if (camera.FocusedDirection == Enums.Direction.Left) {
                            plusAmount++;
                        }
                        if (camera.FocusedX + plusAmount > renderOptions.Map.MaxX + 1 || renderOptions.Map.MaxX == 19) {
                            MapXOffset = 0;
                        }
                    }

                    //MapRenderer.DrawGroundTiles(destData, renderOptions.Map, renderOptions.DisplayAnimation, camera.X, camera.X2, camera.Y, camera.Y2);
                    MapRenderer.DrawGroundTilesSeamless(destData, renderOptions.Map, renderOptions.DisplayAnimation, camera.X, camera.X2, camera.Y, camera.Y2);

                    
                    //prepare focus for relative cutoff of items
                    if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {
                        renderOptions.Darkness.Focus = new Point(ToTileX(PlayerManager.MyPlayer.X) + PlayerManager.MyPlayer.Offset.X + Constants.TILE_WIDTH / 2, ToTileY(PlayerManager.MyPlayer.Y) + PlayerManager.MyPlayer.Offset.Y + Constants.TILE_HEIGHT / 2);
                    }

                    #region Map Items/Npcs/Players
                    // Draw the items on the map
                    for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                        Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                        if (testMap != null && testMap.Loaded) {
                            for (int i = 0; i < MaxInfo.MaxMapItems; i++) {
                                if (testMap.MapItems[i] != null && testMap.MapItems[i].Num > 0 && CanBeSeen(testMap.MapItems[i].X, testMap.MapItems[i].Y, (Enums.MapID)mapCounter)) {
                                    ItemRenderer.DrawMapItem(destData, testMap, (Enums.MapID)mapCounter, i);
                                }
                            }
                        }
                    }

                    if (Input.InputProcessor.SelectedMove > -1) {
                        switch (Input.InputProcessor.SelectedMove) {
                            case 0: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.W)) {
                                        if (PlayerManager.MyPlayer.Moves[0].MoveNum > 0) {
                                            Moves.MoveRenderer.RenderMoveTargettingDisplay(destData, PlayerManager.MyPlayer,
                                                Logic.Moves.MoveHelper.Moves[PlayerManager.MyPlayer.Moves[0].MoveNum]);
                                        }
                                    } else {
                                        Input.InputProcessor.SelectedMove = -1;
                                    }
                                }
                                break;
                            case 1: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.A)) {
                                        if (PlayerManager.MyPlayer.Moves[1].MoveNum > 0) {
                                            Moves.MoveRenderer.RenderMoveTargettingDisplay(destData, PlayerManager.MyPlayer,
                                                Logic.Moves.MoveHelper.Moves[PlayerManager.MyPlayer.Moves[1].MoveNum]);
                                        }
                                    } else {
                                        Input.InputProcessor.SelectedMove = -1;
                                    }
                                }
                                break;
                            case 2: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.S)) {
                                        if (PlayerManager.MyPlayer.Moves[2].MoveNum > 0) {
                                            Moves.MoveRenderer.RenderMoveTargettingDisplay(destData, PlayerManager.MyPlayer,
                                                Logic.Moves.MoveHelper.Moves[PlayerManager.MyPlayer.Moves[2].MoveNum]);
                                        }
                                    } else {
                                        Input.InputProcessor.SelectedMove = -1;
                                    }
                                }
                                break;
                            case 3: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.D)) {
                                        if (PlayerManager.MyPlayer.Moves[3].MoveNum > 0) {
                                            Moves.MoveRenderer.RenderMoveTargettingDisplay(destData, PlayerManager.MyPlayer,
                                                Logic.Moves.MoveHelper.Moves[PlayerManager.MyPlayer.Moves[3].MoveNum]);
                                        }
                                    } else {
                                        Input.InputProcessor.SelectedMove = -1;
                                    }
                                }
                                break;
                        }
                    }

                    if (Stories.Globals.NpcsHidden == false) {
                        // Blit out the npcs
                        for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                            Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                            if (testMap != null && testMap.Loaded) {
                                for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                                    
                                    if ((CanBeSeen(testMap.MapNpcs[i].Location, (Enums.MapID)mapCounter) || testMap.MapNpcs[i].Leaving) && testMap.MapNpcs[i].Num > 0 && testMap.MapNpcs[i].ScreenActive) {


                                        

                                        NpcRenderer.DrawNpc(destData, testMap, (Enums.MapID)mapCounter, i);

                                        



                                        if (testMap.MapNpcs[i].StatusAilment != Enums.StatusAilment.OK) {
                                            NpcRenderer.DrawNpcStatus(destData, testMap, (Enums.MapID)mapCounter, i, (int)testMap.MapNpcs[i].StatusAilment);

                                        }

                                        
                                        //if (testMap.MapNpcs[i].Confused) {
                                        //    NpcRenderer.DrawNpcStatus(destData, testMap, (Enums.MapID)mapCounter, i, 6);
                                        //}
                                        if (testMap.MapNpcs[i].VolatileStatus.Count > 0) {
                                            NpcRenderer.DrawNpcVolatileStatus(destData, testMap, (Enums.MapID)mapCounter, i, testMap.MapNpcs[i].VolatileStatus);
                                        }

                                        // TODO: DrawNpcEmotion
                                    }
                                    
                                }
                            }
                        }
                    }

                    if (Stories.StoryProcessor.ActiveStory != null) {
                        if (Stories.StoryProcessor.ActiveStory.State != null) {

                            // Process MoveFNPC segment
                            for (int i = 0; i < Stories.StoryProcessor.ActiveStory.State.FNPCs.Count; i++) {
                                if (Stories.StoryProcessor.ActiveStory.State.FNPCs[i].MapID == renderOptions.Map.MapID ||
                                    Stories.StoryProcessor.ActiveStory.State.FNPCs[i].MapID == "-2" && renderOptions.Map.MapID.StartsWith("i")) {

                                    Stories.FNPCs.FNPC fNPC = Stories.StoryProcessor.ActiveStory.State.FNPCs[i];

                                    if (fNPC.TargetX > -1 || fNPC.TargetY > -1) {
                                        if (fNPC.Offset.X == 0 && fNPC.Offset.Y == 0) {
                                            if (fNPC.PathfinderResult == null) {
                                                fNPC.PathfinderResult = Logic.Maps.MapHelper.ActiveMap.Pathfinder.FindPath(fNPC.X, fNPC.Y, fNPC.TargetX, fNPC.TargetY);
                                            }
                                            PathfinderResult result = fNPC.PathfinderResult;
                                            if (result.IsPath) {
                                                fNPC.Direction = result.GetNextItem();
                                                fNPC.Offset = new Point(0, 0);
                                                fNPC.MovementSpeed = Enums.MovementSpeed.Walking;

                                                switch (fNPC.Direction) {
                                                    case Enums.Direction.Up:
                                                        fNPC.Y--;
                                                        fNPC.Offset = new Point(fNPC.Offset.X, Constants.TILE_HEIGHT);
                                                        break;
                                                    case Enums.Direction.Down:
                                                        fNPC.Y++;
                                                        fNPC.Offset = new Point(fNPC.Offset.X, Constants.TILE_HEIGHT * -1);
                                                        break;
                                                    case Enums.Direction.Left:
                                                        fNPC.X--;
                                                        fNPC.Offset = new Point(Constants.TILE_WIDTH, fNPC.Offset.Y);
                                                        break;
                                                    case Enums.Direction.Right:
                                                        fNPC.X++;
                                                        fNPC.Offset = new Point(Constants.TILE_WIDTH * -1, fNPC.Offset.Y);
                                                        break;
                                                }

                                                fNPC.LastMovement = Globals.Tick;

                                                if (fNPC.X == fNPC.TargetX && fNPC.Y == fNPC.TargetY) {
                                                    fNPC.TargetX = -2;
                                                    fNPC.TargetY = -2;
                                                    fNPC.PathfinderResult = null;
                                                }
                                            }
                                        }
                                    } else if (fNPC.TargetX == -2 && fNPC.TargetY == -2) {
                                        if (fNPC.Offset.X == 0 && fNPC.Offset.Y == 0) {
                                            fNPC.TargetX = -1;
                                            fNPC.TargetY = -1;
                                            if (Stories.StoryProcessor.ActiveStory.State.StoryPaused) {
                                                Stories.StoryProcessor.ActiveStory.State.Unpause();
                                            }
                                        }
                                    }

                                    GameProcessor.ProcessSpriteMovement(Stories.StoryProcessor.ActiveStory.State.FNPCs[i]);
                                    Sprites.SpriteRenderer.DrawSprite(destData, renderOptions.Map, Enums.MapID.Active, Stories.StoryProcessor.ActiveStory.State.FNPCs[i]);
                                }
                            }

                            // Process MovePlayer segment
                            MyPlayer myPlayer = PlayerManager.MyPlayer;
                            if (myPlayer.TargetX > -1 || myPlayer.TargetY > -1) {
                                // We still need to move the player
                                if (myPlayer.Offset.X == 0 && myPlayer.Offset.Y == 0) {
                                    if (myPlayer.StoryPathfinderResult == null) {
                                        myPlayer.StoryPathfinderResult = Logic.Maps.MapHelper.ActiveMap.Pathfinder.FindPath(myPlayer.X, myPlayer.Y, myPlayer.TargetX, myPlayer.TargetY);
                                    }
                                    PathfinderResult result = myPlayer.StoryPathfinderResult;
                                    if (result.IsPath) {
                                        myPlayer.Direction = result.GetNextItem();
                                        myPlayer.Offset = new Point(0, 0);
                                        myPlayer.MovementSpeed = myPlayer.StoryMovementSpeed;

                                        Network.Messenger.SendPlayerMove();
                                        switch (myPlayer.Direction) {
                                            case Enums.Direction.Up:
                                                myPlayer.Y--;
                                                myPlayer.Offset = new Point(myPlayer.Offset.X, Constants.TILE_HEIGHT);
                                                break;
                                            case Enums.Direction.Down:
                                                myPlayer.Y++;
                                                myPlayer.Offset = new Point(myPlayer.Offset.X, Constants.TILE_HEIGHT * -1);
                                                break;
                                            case Enums.Direction.Left:
                                                myPlayer.X--;
                                                myPlayer.Offset = new Point(Constants.TILE_WIDTH, myPlayer.Offset.Y);
                                                break;
                                            case Enums.Direction.Right:
                                                myPlayer.X++;
                                                myPlayer.Offset = new Point(Constants.TILE_WIDTH * -1, myPlayer.Offset.Y);
                                                break;
                                        }

                                        myPlayer.LastMovement = Globals.Tick;

                                        if (myPlayer.X == myPlayer.TargetX && myPlayer.Y == myPlayer.TargetY) {
                                            myPlayer.TargetX = -2;
                                            myPlayer.TargetY = -2;
                                            myPlayer.StoryPathfinderResult = null;
                                        }
                                    }
                                }
                            } else if (myPlayer.TargetX == -2 && myPlayer.TargetY == -2) {
                                if (myPlayer.Offset.X == 0 && myPlayer.Offset.Y == 0) {
                                    myPlayer.TargetX = -1;
                                    myPlayer.TargetY = -1;
                                    if (Stories.StoryProcessor.ActiveStory.State.StoryPaused) {
                                        Stories.StoryProcessor.ActiveStory.State.Unpause();
                                    }
                                }
                            }
                        }
                    }

                    // Draw the arrows and players
                    if (Stories.Globals.PlayersHidden == false) {
                        foreach (IPlayer player in PlayerManager.Players.GetAllPlayers()) {

                            for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                                Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                                if (testMap != null && testMap.Loaded) {

                                    if (player.MapID == testMap.MapID) {
                                        if ((CanBeSeen(player.Location, (Enums.MapID)mapCounter) || player.Leaving) && player.ScreenActive || player == PlayerManager.MyPlayer) {
                                            PlayerRenderer.DrawPlayer(destData, player, testMap, (Enums.MapID)mapCounter);
#if DEBUG
                                            // TODO: Pet system is a WIP
                                            //for (int n = 1; n < MaxInfo.MAX_ACTIVETEAM; n++) {
                                            //    if (player.Pets[n] != null) {
                                            //        player.Pets[n].Update();
                                            //        GameProcessor.ProcessSpriteMovement(player.Pets[n]);
                                            //        Sprites.SpriteRenderer.DrawSprite(destData, player.Pets[n]);
                                            //    }
                                            //}
#endif
                                            if (!player.Dead) {
                                                if (player.StatusAilment != Enums.StatusAilment.OK) {
                                                    PlayerRenderer.DrawPlayerStatus(destData, testMap, (Enums.MapID)mapCounter, player, (int)player.StatusAilment);

                                                }
                                                //if (player.Confused) {
                                                //    PlayerRenderer.DrawPlayerStatus(destData, testMap, (Enums.MapID)mapCounter, player, 6);
                                                //}
                                                if (player.VolatileStatus.Count > 0 && player.CurrentEmote == null) {
                                                    PlayerRenderer.DrawPlayerVolatileStatus(destData, testMap, (Enums.MapID)mapCounter, player, player.VolatileStatus);
                                                }
                                            } else {
                                                PlayerRenderer.DrawPlayerStatus(destData, testMap, (Enums.MapID)mapCounter, player, 6);
                                            }

                                            if (player.CurrentEmote != null) {
                                                player.CurrentEmote.EmoteTime++;
                                                if (player.CurrentEmote.EmoteTime > player.CurrentEmote.EmoteSpeed) {
                                                    player.CurrentEmote.EmoteFrame++;
                                                    player.CurrentEmote.EmoteTime = 0;
                                                }

                                                if (player.CurrentEmote.EmoteFrame >= 12) {
                                                    player.CurrentEmote.CurrentCycle++;
                                                    player.CurrentEmote.EmoteFrame = 0;
                                                }
                                                if (player.CurrentEmote.CurrentCycle >= player.CurrentEmote.EmoteCycles) {
                                                    player.CurrentEmote = null;
                                                } else {
                                                    PlayerRenderer.DrawPlayerEmote(destData, testMap, (Enums.MapID)mapCounter, player);
                                                }
                                            }
                                        }
                                    }

                                }
                            }
                        }
                    } else {
                        PlayerRenderer.DrawPlayer(destData, PlayerManager.MyPlayer, renderOptions.Map, Enums.MapID.Active);
                    }
                    //draw confusion
                    //if (Stories.Globals.PlayersHidden == false) {
                    //    for (int i = 0; i < PlayerManager.Players.Count; i++) {
                    //        if (PlayerManager.Players.GetPlayerFromIndex(i).MapID == renderOptions.Map.MapID) {
                    //            if (IsInSight(PlayerManager.Players.GetPlayerFromIndex(i).Location)) {
                    //                PlayerRenderer.DrawPlayer(destSurf, PlayerManager.Players.GetPlayerFromIndex(i), renderOptions.Map);
                    //            }

                    //        }
                    //    }
                    //} else {
                    //    //PlayerRenderer.DrawPlayer(destSurf, PlayerManager.MyPlayer, renderOptions.Map);
                    //    
                    //}

                    // Render all active move animations
                    for (int i = Renderers.Moves.MoveRenderer.ActiveAnimations.Count - 1; i >= 0; i--) {
                        Renderers.Moves.IMoveAnimation animation = Renderers.Moves.MoveRenderer.ActiveAnimations[i];
                        if (animation.Active) {
                            Renderers.Moves.MoveRenderer.RenderMoveAnimation(destData, animation, Screen.ScreenRenderer.ToTilePoint(new Point(animation.StartX, animation.StartY)));
                        } else {
                            // Remove the animation if it is finished
                            Renderers.Moves.MoveRenderer.ActiveAnimations.RemoveAt(i);
                        }
                    }



                    #endregion

                    MapRenderer.DrawFringeTilesSeamless(destData, renderOptions.Map, renderOptions.DisplayAnimation, camera.X, camera.X2, camera.Y, camera.Y2);

                    if (renderOptions.DisplayMapGrid) {
                        MapRenderer.DrawMapGrid(destData, renderOptions.Map, camera.X, camera.X2, camera.Y, camera.Y2);
                    }

                    if (renderOptions.DisplayAttributes) {
                        MapRenderer.DrawAttributes(destData, renderOptions.Map, camera.X, camera.X2, camera.Y, camera.Y2);
                    }

                    if (renderOptions.DisplayDungeonValues) {
                        MapRenderer.DrawDungeonValues(destData, renderOptions.Map, camera.X, camera.X2, camera.Y, camera.Y2);
                    }

                    #region Player/Npc Names
                    // Draw the player names
                    if (Stories.Globals.PlayersHidden == false) {
                        if (IO.Options.PlayerName) {
                            foreach (IPlayer player in PlayerManager.Players.GetAllPlayers()) {

                                for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                                    Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                                    if (testMap != null && testMap.Loaded) {

                                        if (player.MapID == testMap.MapID && player.ScreenActive || player == PlayerManager.MyPlayer) {
                                            if (CanBeIdentified(player.Location, (Enums.MapID)mapCounter)) {
                                                PlayerRenderer.DrawPlayerGuild(destData, testMap, (Enums.MapID)mapCounter, player);
                                                PlayerRenderer.DrawPlayerName(destData, testMap, (Enums.MapID)mapCounter, player);
                                            }
                                        }

                                    }
                                }
                            }
                        }
                    } else {
                        PlayerRenderer.DrawPlayerName(destData, renderOptions.Map, Enums.MapID.Active, PlayerManager.MyPlayer);
                        PlayerRenderer.DrawPlayerGuild(destData, renderOptions.Map, Enums.MapID.Active, PlayerManager.MyPlayer);
                    }
                    if (Stories.Globals.NpcsHidden == false) {
                        // Draw npc names
                        if (IO.Options.NpcName) {
                            for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                                Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                                if (testMap != null && testMap.Loaded) {
                                    for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                                        if (testMap.MapNpcs[i].Num > 0 && testMap.MapNpcs[i].ScreenActive) {
                                            if (CanBeIdentified(testMap.MapNpcs[i].Location, (Enums.MapID)mapCounter)) {
                                                NpcRenderer.DrawMapNpcName(destData, testMap, (Enums.MapID)mapCounter, i);
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                    #endregion

                    #region Player/Npc bars
                    // Draw the NPC HP Bars
                    if (IO.Options.NpcBar) {
                        for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                            Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                            if (testMap != null && testMap.Loaded) {
                                for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                                    if (CanBeIdentified(renderOptions.Map.MapNpcs[i].Location, (Enums.MapID)mapCounter)) {
                                        NpcRenderer.DrawNpcBars(destData, renderOptions.Map, (Enums.MapID)mapCounter, i);
                                    }
                                }
                            }
                        }
                    }

                    // Draw the player bar
                    if (IO.Options.PlayerBar) {
                        PlayerRenderer.DrawPlayerBar(destData);
                    }

                    #endregion

                    if (Stories.Globals.PlayersHidden == false) {
                        foreach (IPlayer player in PlayerManager.Players.GetAllPlayers()) {

                            for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                                Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                                if (testMap != null && testMap.Loaded) {

                                    if (player.MapID == testMap.MapID) {
                                        if ((CanBeSeen(player.Location, (Enums.MapID)mapCounter) || player.Leaving) && player.ScreenActive || player == PlayerManager.MyPlayer) {
                                            Sprites.SpriteRenderer.DrawSpeechBubble(destData, testMap, (Enums.MapID)mapCounter, player, Globals.Tick);
                                        }
                                    }
                                }
                            }
                        }
                    }
                    //Sprites.SpriteRenderer.ProcessSpeechBubbles(destData, Globals.Tick);

                    #region Movement Processing
                    //if (PlayerManager.MyPlayer.TempMuteTimer < Globals.Tick) {
                    //    PlayerManager.MyPlayer.TempMuteTimer = Globals.Tick + 3000;
                    //    Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("checkcommands", "/rstart 22 2"));
                    //}
                    // Process player movements (actually move them)
                    foreach (IPlayer player in PlayerManager.Players.GetAllPlayers()) {

                        for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                            Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                            if (testMap != null && testMap.Loaded) {

                                if (player.MapID == testMap.MapID) {
                                    if ((CanBeSeen(player.Location, (Enums.MapID)mapCounter) || player.Leaving) && player.ScreenActive || player == PlayerManager.MyPlayer) {
                                        GameProcessor.ProcessSpriteMovement(player);
                                        break;
                                    }
                                }

                            }
                        }
                    }

                    for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                        Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                        if (testMap != null && testMap.Loaded) {

                            for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                                if (testMap.MapNpcs[i].Num > 0 && testMap.MapNpcs[i].ScreenActive) {
                                    if (CanBeSeen(testMap.MapNpcs[i].Location, (Enums.MapID)mapCounter) || testMap.MapNpcs[i].Leaving) {
                                        GameProcessor.ProcessSpriteMovement(testMap.MapNpcs[i]);
                                    }
                                }
                            }
                        }
                    }

                    #endregion

                    if (renderOptions.Weather != null && !renderOptions.Weather.Disposed) {
                        renderOptions.Weather.Render(destData, Globals.Tick);
                    }

                    if (renderOptions.Overlay != null && !renderOptions.Overlay.Disposed) {
                        renderOptions.Overlay.Render(destData, Globals.Tick);
                    }

                    if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {
                        /*if (PlayerManager.MyPlayer.MovementSpeed != Enums.MovementSpeed.Standing) {
                            switch (PlayerManager.MyPlayer.Direction) {
                                case Enums.Direction.Up:
                                    spot = new System.Drawing.Point(spot.X, spot.Y + GameProcessor.DetermineSpeed(PlayerManager.MyPlayer.MovementSpeed));
                                    break;
                                case Enums.Direction.Down:
                                    spot = new System.Drawing.Point(spot.X, spot.Y - GameProcessor.DetermineSpeed(PlayerManager.MyPlayer.MovementSpeed));
                                    break;
                                case Enums.Direction.Left:
                                    spot = new System.Drawing.Point(spot.X + GameProcessor.DetermineSpeed(PlayerManager.MyPlayer.MovementSpeed), spot.Y);
                                    break;
                                case Enums.Direction.Right:
                                    spot = new System.Drawing.Point(spot.X - GameProcessor.DetermineSpeed(PlayerManager.MyPlayer.MovementSpeed), spot.Y);
                                    break;
                            }
                        }*/
                        renderOptions.Darkness.Render(destData, Globals.Tick, renderOptions.Darkness.Focus);
                    }

                    if (renderOptions.DisplayLocation && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                        TextRenderer.DrawText(destData, "Char Loc: X: " + PlayerManager.MyPlayer.Location.X + " Y: " + PlayerManager.MyPlayer.Location.Y, Color.Yellow, Color.Black, 12, 30);
                        // TODO: Draw Cursor Location
                        TextRenderer.DrawText(destData, "Map: " + PlayerManager.MyPlayer.MapID, Color.Yellow, Color.Black, 12, 48);
                    }

                    if (IO.Options.Ping && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                        TextRenderer.DrawText(destData, "Ping: " + renderOptions.RecentPing + "ms", Color.Yellow, Color.Black, 12, 66);
                    }

                    if (IO.Options.FPS && Windows.WindowSwitcher.GameWindow.inMapEditor == false) {
                        TextRenderer.DrawText(destData, "FPS: " + renderOptions.RecentFPS, Color.Yellow, Color.Black, 12, 84);
                    }

                    if (Windows.WindowSwitcher.GameWindow.inMapEditor) {
                        TextRenderer.DrawText(destData, "Char Loc: X: " + PlayerManager.MyPlayer.Location.X + " Y: " + PlayerManager.MyPlayer.Location.Y, Color.Yellow, Color.Black, 12, 12);
                        // TODO: Draw Cursor Location
                        TextRenderer.DrawText(destData, "Map: " + PlayerManager.MyPlayer.MapID, Color.Yellow, Color.Black, 12, 30);
                        TextRenderer.DrawText(destData, "Selected Tile: " + Windows.WindowSwitcher.GameWindow.GetSelectedTileNumber(), Color.Yellow, Color.Black, 12, 58);
                    }

                    if (!Network.NetworkManager.TcpClient.Socket.Connected) {
                        Globals.ServerStatus = "You have been disconnected from the server!";
                    }

                    if (!string.IsNullOrEmpty(Globals.ServerStatus)) {
                        TextRenderer.DrawText(destData, Globals.ServerStatus, Color.Red, Color.Black, 12, 48);
                    }
                    MapRenderer.DrawMapName(destData, renderOptions.Map);

                    if (!Windows.WindowSwitcher.GameWindow.inMapEditor) {
                        //PlayerRenderer.DrawMiniBars(destData);
                    }

                    if (Input.InputProcessor.SelectedMove > -1) {
                        bool canDisplay = false;
                        switch (Input.InputProcessor.SelectedMove) {
                            case 0: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.W)) {
                                        canDisplay = true;
                                    }
                                }
                                break;
                            case 1: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.A)) {
                                        canDisplay = true;
                                    }
                                }
                                break;
                            case 2: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.S)) {
                                        canDisplay = true;
                                    }
                                }
                                break;
                            case 3: {
                                    if (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.D)) {
                                        canDisplay = true;
                                    }
                                }
                                break;
                        }
                        if (canDisplay) {
                            RecruitMove move = Logic.Players.PlayerManager.MyPlayer.Moves[Input.InputProcessor.SelectedMove];
                            string displayString = Logic.Moves.MoveHelper.Moves[move.MoveNum].Name + "   " + move.CurrentPP + "/" + move.MaxPP;
                            Color textColor;
                            if (move.CurrentPP == 0 || move.Sealed) {
                                textColor = Color.Red;
                            } else {
                                textColor = Color.WhiteSmoke;
                            }
                            TextRenderer.DrawText(destData, displayString, textColor, Color.Black, new Point(10, 30));
                        }
                    }

                    if (Globals.GettingMap) {
                        TextRenderer.DrawText(destData, "Loading Map...", Color.Blue, Color.Black, 5, 5);
                    }

                    if (Globals.SavingMap) {
                        TextRenderer.DrawText(destData, "Saving Map...", Color.Blue, Color.Black, 5, 5);
                    }

                    if (Stories.StoryProcessor.loadingStory) {
                        TextRenderer.DrawText(destData, "Loading story...", Color.Blue, Color.Black, 5, 5);
                    }

                    if (renderOptions.ScreenVisible == false) {
                        destData.Surface.Fill(Color.Black);
                    }
                }

                if (renderOptions.ScreenOverlay != null) {
                    renderOptions.ScreenOverlay.Render(destData, Globals.Tick);
                }

                if (renderOptions.StoryBackground != null) {
                    destData.Blit(renderOptions.StoryBackground, new Point(0, 0));
                }

                if (renderOptions.ScreenImageOverlays.Count > 0) {
                    for (int i = 0; i < renderOptions.ScreenImageOverlays.Count; i++) {
                        destData.Blit(renderOptions.ScreenImageOverlays[i].Surface, new Point(renderOptions.ScreenImageOverlays[i].X, renderOptions.ScreenImageOverlays[i].Y));
                    }
                }
                //if (e.Tick > mLastAnim + 250) {
                //    mMapAnim = !mMapAnim;
                //    mLastAnim = e.Tick;
                //}

                if (IO.Options.FPS) {
                    renderOptions.RecentRenders++;
                    if (renderOptions.RecentRenders >= 10) {
                        RenderStopwatch.Stop();
                        if (RenderStopwatch.Elapsed.TotalSeconds > 0) {
                            renderOptions.RecentFPS = (int)(1 / (RenderStopwatch.Elapsed.TotalSeconds / 10));
                        }
                        RenderStopwatch.Reset();
                        RenderStopwatch.Start();
                        renderOptions.RecentRenders = 0;
                    }
                }
            } catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Inner Rendering:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        public static int ToScreenX(int x) {
            return x - (Camera.X * 32) - MapXOffset;
        }

        public static int ToScreenY(int y) {
            return y - (Camera.Y * 32) - MapYOffset;
        }

        public static int ToTileX(int x) {
            return (x * Constants.TILE_WIDTH) - (Camera.X * Constants.TILE_WIDTH) - MapXOffset;
        }

        public static int ToTileY(int y) {
            return (y * Constants.TILE_HEIGHT) - (Camera.Y * Constants.TILE_HEIGHT) - MapYOffset;
        }

        public static Point ToTilePoint(Point point) {
            point.X = ToTileX(point.X);
            point.Y = ToTileY(point.Y);
            return point;
        }


        public static bool WillBeSeen(Point location, Enums.MapID targetMapID) {//checks to see if just out of range
            return WillBeSeen(location.X, location.Y, targetMapID);
        }

        public static bool WillBeSeen(int x, int y, Enums.MapID targetMapID) {

            int upDistance = 0, downDistance = 0, leftDistance = 0, rightDistance = 0,
                farUp, farDown, farLeft, farRight;

            switch (PlayerManager.MyPlayer.Direction) {
                case Enums.Direction.Up: {
                        upDistance = 9;
                        downDistance = 1;
                        leftDistance = 2;
                        rightDistance = 2;
                    }
                    break;
                case Enums.Direction.Down: {
                        upDistance = 1;
                        downDistance = 9;
                        leftDistance = 2;
                        rightDistance = 2;
                    }
                    break;
                case Enums.Direction.Left: {
                        upDistance = 2;
                        downDistance = 2;
                        leftDistance = 9;
                        rightDistance = 1;
                    }
                    break;
                case Enums.Direction.Right: {
                        upDistance = 2;
                        downDistance = 2;
                        leftDistance = 1;
                        rightDistance = 9;
                    }
                    break;
            }


            if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {
                farUp = PlayerManager.MyPlayer.Y - renderOptions.Darkness.Range / 2;
                farDown = PlayerManager.MyPlayer.Y + renderOptions.Darkness.Range / 2;
                farLeft = PlayerManager.MyPlayer.X - renderOptions.Darkness.Range / 2;
                farRight = PlayerManager.MyPlayer.X + renderOptions.Darkness.Range / 2;

            } else {
                farUp = Camera.Y;
                farDown = Camera.Y2;
                farLeft = Camera.X;
                farRight = Camera.X2;
            }

            SeamlessWorldHelper.ConvertCoordinatesToBorderless(renderOptions.Map, targetMapID, ref x, ref y);

            if (x < farLeft - leftDistance || x > farRight + rightDistance || y < farUp - upDistance || y > farDown + downDistance) return false;

            //if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {

            //    int distance = (int)System.Math.Floor(System.Math.Sqrt(System.Math.Pow(PlayerManager.MyPlayer.X - x, 2) + System.Math.Pow(PlayerManager.MyPlayer.Y - y, 2)));
            //    if (distance * 2 > renderOptions.Darkness.Range + 1) return false;
            //}

            return true;
        }

        public static bool CanBeSeen(Point location, Enums.MapID targetMapID) {
            return CanBeSeen(location.X, location.Y, targetMapID);
        }

        public static bool CanBeSeen(int x, int y, Enums.MapID targetMapID) {
            SeamlessWorldHelper.ConvertCoordinatesToBorderless(renderOptions.Map, targetMapID, ref x, ref y);

            if (x < Camera.X || x > Camera.X2 || y < Camera.Y || y > Camera.Y2) return false;

            if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {
                int distance = (int)System.Math.Floor(2 * System.Math.Sqrt(System.Math.Pow(PlayerManager.MyPlayer.X - x, 2) + System.Math.Pow(PlayerManager.MyPlayer.Y - y, 2)));
                if (distance > renderOptions.Darkness.Range) return false;
            }

            return true;
        }

        public static bool CanBeIdentified(Point location, Enums.MapID targetMapID) {
            return CanBeIdentified(location.X, location.Y, targetMapID);
        }

        public static bool CanBeIdentified(int x, int y, Enums.MapID targetMapID) {
            SeamlessWorldHelper.ConvertCoordinatesToBorderless(renderOptions.Map, targetMapID, ref x, ref y);

            if (x < Camera.X || x > Camera.X2 || y < Camera.Y || y > Camera.Y2) return false;

            if (renderOptions.Darkness != null && !renderOptions.Darkness.Disposed) {
                int distance = (int)System.Math.Ceiling(2 * System.Math.Sqrt(System.Math.Pow(PlayerManager.MyPlayer.X - x, 2) + System.Math.Pow(PlayerManager.MyPlayer.Y - y, 2)));
                if (distance > renderOptions.Darkness.Range - 1) return false;
            }

            return true;
        }


        public static void DeactivateOffscreenSprites() {
            DeactivateOffscreenPlayers();
            DeactivateOffscreenNpcs();
        }

        public static void DeactivateOffscreenPlayers() {
            foreach (IPlayer player in PlayerManager.Players.GetAllPlayers()) {

                for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
                    Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

                    if (testMap != null && testMap.Loaded) {

                        if (testMap.MapID == PlayerManager.MyPlayer.MapID && mapCounter != 0) {
                            continue;
                        }

                        if (player.MapID == testMap.MapID) {
                            if (!WillBeSeen(player.Location, (Enums.MapID)mapCounter) && player != PlayerManager.MyPlayer) {
                                player.ScreenActive = false;
                            }
                        }

                    }
                }
            }
        }

        public static void DeactivateOffscreenNpcs() {
            for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                if (renderOptions.Map.MapNpcs[i].ScreenActive) {
                    if (!WillBeSeen(renderOptions.Map.MapNpcs[i].Location, Enums.MapID.Active)) {
                        renderOptions.Map.MapNpcs[i].ScreenActive = false;
                    }
                }
            }

            //for (int mapCounter = 0; mapCounter < 9; mapCounter++) {
            //    Logic.Maps.Map testMap = Logic.Maps.MapHelper.Maps[(Enums.MapID)mapCounter];

            //    if (testMap != null && testMap.Loaded) {

            //        if (testMap.MapID == PlayerManager.MyPlayer.MapID && mapCounter != 0) {
            //            continue;
            //        }
            //        for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
            //            if (testMap.MapNpcs[i].ScreenActive) {
            //                if (!WillBeSeen(testMap.MapNpcs[i].Location, (Enums.MapID)mapCounter)) {
            //                    testMap.MapNpcs[i].ScreenActive = false;
            //                }
            //            }
            //        }

            //    }
            //}
        }

        public static int GetScreenLeft() {
            return camera.FocusedX - 9;
        }

        public static int GetScreenTop() {
            return camera.FocusedY - 7;
        }

        public static int GetScreenRight() {
            return camera.FocusedX + 11;
        }

        public static int GetScreenBottom() {
            return camera.FocusedY + 8;
        }
    }
}
