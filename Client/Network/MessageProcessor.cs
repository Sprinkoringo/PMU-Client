namespace Client.Logic.Network
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;
    using Client.Logic.Core;
    using Client.Logic.Windows;
    using PMU.Core;
    using PMU.Sockets;
    using PMU.Net;
    using Client.Logic.Players;
    using Client.Logic.Players.Parties;

    class MessageProcessor
    {
        public static System.Diagnostics.Stopwatch PingStopwatch = new System.Diagnostics.Stopwatch();
        #region Methods

        public static void HandleData(string data) {
            string[] parse = data.Split(Convert.ToChar(TcpPacket.SEP_CHAR));
            switch (parse[0].ToLower()) {
                case "cryptkey": {
                        if (parse[1].StartsWith("----") == false) {
                            NetworkManager.packetModifiers.SetKey(parse[1]);
                        } else {
                            // Encryption is disabled
                            NetworkManager.packetModifiers.SetKey(null);
                        }
                    }
                    break;
                case "news": {
                        SdlDotNet.Widgets.Window winUpdates = WindowSwitcher.FindWindow("winUpdates");
                        if (winUpdates != null) {
                            ((winUpdates)winUpdates).DisplayNews(parse[1]);
                        }
                    }
                    break;
                case "foolsmode": {
                        Globals.FoolsMode = parse[1].ToBool();
                        for (int i = 0; i < 4; i++) {
                            WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitData(i);
                        }
                    }
                    break;
                #region Log in
                case "allchars": {
                        SdlDotNet.Widgets.Window winLoading = WindowSwitcher.FindWindow("winLoading");
                        if (winLoading != null) {
                            winLoading.Close();
                        }

                        Windows.winSelectChar charWindow = WindowSwitcher.FindWindow("winSelectChar") as Windows.winSelectChar;
                        if (charWindow == null) {
                            charWindow = new winSelectChar();
                            WindowSwitcher.AddWindow(charWindow);
                        }

                        charWindow.SetCharName(charWindow.btnChar1, parse[1]);
                        charWindow.SetCharName(charWindow.btnChar2, parse[2]);
                        charWindow.SetCharName(charWindow.btnChar3, parse[3]);
                    }
                    break;

                case "maxinfo": {
                        MaxInfo.GameName = parse[1].Trim();
                        MaxInfo.MaxItems = parse[2].ToInt();
                        MaxInfo.MaxNpcs = parse[3].ToInt();
                        MaxInfo.MaxShops = parse[4].ToInt();
                        MaxInfo.MaxMoves = parse[5].ToInt();
                        MaxInfo.MaxMapItems = parse[6].ToInt();
                        MaxInfo.MaxMapX = parse[7].ToInt();
                        MaxInfo.MaxMapY = parse[8].ToInt();
                        MaxInfo.MaxEmoticons = parse[9].ToInt();
                        MaxInfo.MaxEvolutions = parse[10].ToInt();
                        MaxInfo.MaxStories = parse[11].ToInt();


                        // Initialize everything
                        Players.PlayerManager.Initialize();
                        Logs.BattleLog.Initialize();
                        Npc.NpcHelper.InitNpcArray();
                        Shops.ShopHelper.InitShopCollection();
                        Moves.MoveHelper.InitMoveCollection();
                        Emotions.EmotionHelper.InitEmotionCollection();
                        Items.ItemHelper.InitItemCollection();
                        //Arrows.ArrowHelper.InitArrowCollection();
                        Evolutions.EvolutionHelper.InitEvosCollection();
                        Stories.StoryHelper.InitStoryCollection();
                        //Missions.MissionHelper.InitMissionCollection();
                        RDungeons.RDungeonHelper.InitRDungeonCollection();
                        Dungeons.DungeonHelper.InitDungeonCollection();
                        Pokedex.PokemonHelper.InitPokemonCollection();
                        Maps.MapHelper.InitMapHelper();
                    }
                    break;
                case "myconid": {
                        PlayerManager.Players.Add(parse[1], new MyPlayer());
                        PlayerManager.MyConnectionID = parse[1];
                        PlayerManager.MyPlayer.ID = parse[1];

                        //Music.Music.AudioPlayer.StopMusic();

                        Windows.winExpKit expKitWindow = new winExpKit();
                        WindowSwitcher.ExpKit = expKitWindow;
                        expKitWindow.Visible = false;
                        WindowSwitcher.AddWindow(expKitWindow);
                    }
                    break;
                case "allitemsdata": {
                        Items.ItemHelper.LoadItemsFromPacket(parse);
                    }
                    break;
                case "allemoticonsdata": {
                        Emotions.EmotionHelper.LoadEmotionsFromPacket(parse);
                    }
                    break;
                case "allarrowsdata": {
                        //Arrows.ArrowHelper.LoadArrowsFromPacket(parse);
                    }
                    break;
                case "allnpcsdata": {
                        Npc.NpcHelper.LoadNpcsFromPacket(parse);
                    }
                    break;
                case "allshopsdata": {
                        Shops.ShopHelper.LoadShopsFromPacket(parse);
                    }
                    break;
                case "allspellsdata": {
                        Moves.MoveHelper.LoadMovesFromPacket(parse);
                    }
                    break;
                case "allevosdata": {
                        Evolutions.EvolutionHelper.LoadEvosFromPacket(parse);
                    }
                    break;
                case "allstoriesdata": {
                        Stories.StoryHelper.LoadStoriesFromPacket(parse);
                    }
                    break;
                    //no more preset missions
                //case "allmissions": {
                //        Missions.MissionHelper.LoadMissionsFromPacket(parse);
                //    }
                //    break;
                case "allrdungeons": {
                        RDungeons.RDungeonHelper.LoadRDungeonsFromPacket(parse);
                    }
                    break;
                case "alldungeons": {
                        Dungeons.DungeonHelper.LoadDungeonsFromPacket(parse);
                    }
                    break;
                case "allpokemon": {
                        Pokedex.PokemonHelper.LoadPokemonFromPacket(parse);
                    }
                    break;
                case "ingame": {
                        WindowSwitcher.FindWindow("winLoading").Close();
                        WindowSwitcher.GameWindow.ShowWidgets();
                        //WindowSwitcher.AddWindow(WindowSwitcher.GameWindow);
                        WindowSwitcher.FindWindow("winExpKit").Visible = true;
                        //WindowSwitcher.FindWindow("winChat").Visible = true;
                        Globals.InGame = true;
                    }
                    break;
                #endregion
                #region Recruitment
                case "activeteam": {
                        int n = 1;
                        for (int i = 0; i < MaxInfo.MAX_ACTIVETEAM; i++) {
                            PlayerManager.MyPlayer.Team[i] = new Recruit();
                            if (parse[n].ToLower() != "notloaded") {
                                Recruit recruit = PlayerManager.MyPlayer.Team[i];
                                recruit.Name = parse[n];
                                recruit.Num = parse[n + 1].ToInt();
                                recruit.Form = parse[n + 2].ToInt();
                                recruit.Shiny = (Enums.Coloration)parse[n + 3].ToInt();
                                recruit.Sex = (Enums.Sex)parse[n + 4].ToInt();
                                recruit.HP = parse[n + 5].ToInt();
                                recruit.MaxHP = parse[n + 6].ToInt();
                                recruit.ExpPercent = parse[n + 7].ToInt();
                                recruit.Level = parse[n + 8].ToInt();
                                recruit.StatusAilment = (Enums.StatusAilment)parse[n + 9].ToInt();
                                recruit.HeldItemSlot = parse[n + 10].ToInt();
                                recruit.Loaded = true;
                                n += 11;
                            } else {
                                n += 1;
                            }
                            WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitData(i);
                        }
                    }
                    break;
                case "playerhelditem": {
                        for (int i = 0; i < 4; i++) {
                            if (PlayerManager.MyPlayer.Team[i] != null && PlayerManager.MyPlayer.Team[i].Loaded) {
                                PlayerManager.MyPlayer.Team[i].HeldItemSlot = parse[i + 1].ToInt();
                                WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitHeldItem(i);
                            }


                        }

                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory") != null) {
                            ((Menus.mnuInventory)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory")).DisplayItems(((Menus.mnuInventory)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory")).currentTen * 10 + 1);

                        }
                        //PlayerManager.MyPlayer.ArmorSlot = parse[1].ToInt();
                        //PlayerManager.MyPlayer.WeaponSlot = parse[2].ToInt();
                        //PlayerManager.MyPlayer.HelmetSlot = parse[3].ToInt();
                        //PlayerManager.MyPlayer.ShieldSlot = parse[4].ToInt();
                        //PlayerManager.MyPlayer.LegsSlot = parse[5].ToInt();
                        //PlayerManager.MyPlayer.RingSlot = parse[6].ToInt();
                        //PlayerManager.MyPlayer.NecklaceSlot = parse[7].ToInt();

                    }
                    break;
                case "teamstatus": {
                        for (int i = 0; i < 4; i++) {
                            if (PlayerManager.MyPlayer.Team[i] != null) {
                                PlayerManager.MyPlayer.Team[i].StatusAilment = (Enums.StatusAilment)parse[i + 1].ToInt();
                                WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitStatusAilment(i);
                            }

                        }

                    }
                    break;
                case "activecharswap": {
                        PlayerManager.MyPlayer.ActiveTeamNum = parse[1].ToInt();
                        Music.Music.AudioPlayer.PlaySoundEffect("magic165.wav");
                    }
                    break;
                case "activeteamnum": {
                        PlayerManager.MyPlayer.ActiveTeamNum = parse[1].ToInt();
                    }
                    break;
                case "allrecruits": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.ShowAssembly(parse);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                #endregion
                #region Stats
                case "playerhp": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Team[parse[1].ToInt()].MaxHP = parse[2].ToInt();
                            PlayerManager.MyPlayer.Team[parse[1].ToInt()].HP = parse[3].ToInt();
                            //PlayerManager.MyPlayer.Team[PlayerManager.MyPlayer.ActiveTeamNum].HPPercent = MathFunctions.CalculatePercent(PlayerManager.MyPlayer.HP, PlayerManager.MyPlayer.MaxHP);
                            if (PlayerManager.MyPlayer.Team[parse[1].ToInt()].MaxHP > 0) {
                                WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitHP(parse[1].ToInt());
                            }
                        }
                    }
                    break;
                case "playerexp": {
                        PlayerManager.MyPlayer.MaxExp = parse[1].ToUlng();
                        PlayerManager.MyPlayer.Exp = parse[2].ToUlng();
                        PlayerManager.MyPlayer.GetActiveRecruit().ExpPercent = (int)MathFunctions.CalculatePercent(PlayerManager.MyPlayer.Exp, PlayerManager.MyPlayer.MaxExp);

                        WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitExp(PlayerManager.MyPlayer.ActiveTeamNum);

                    }
                    break;
                case "playerstatspacket": {


                        WindowSwitcher.GameWindow.StatLabel.SetStats(parse[1], parse[2], parse[3], parse[4], parse[5]);
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.MaxExp = parse[6].ToUlng();
                            PlayerManager.MyPlayer.Exp = parse[7].ToUlng();
                            PlayerManager.MyPlayer.Level = parse[8].ToInt();

                            PlayerManager.MyPlayer.GetActiveRecruit().ExpPercent = (int)MathFunctions.CalculatePercent(PlayerManager.MyPlayer.Exp, PlayerManager.MyPlayer.MaxExp);

                            WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitData(PlayerManager.MyPlayer.ActiveTeamNum);
                        }
                    }
                    break;
                case "recruitbelly": {
                        PlayerManager.MyPlayer.Belly = parse[1].ToInt();
                        PlayerManager.MyPlayer.MaxBelly = parse[2].ToInt();
                    }
                    break;
                #endregion
                #region Chat
                case "msg": {
                        ChatReceived(parse);
                        break;
                    }
                case "battlemsg": {
                        WindowSwitcher.GameWindow.AddToBattleLog(parse[1], Color.FromArgb(parse[2].ToInt()));
                    }
                    break;
                case "multibattlemsg": {
                        for (int i = 1; i < parse.Length; i++) {
                            WindowSwitcher.GameWindow.AddToBattleLog(parse[i * 2 - 1], Color.FromArgb(parse[i * 2].ToInt()));
                        }
                    }
                    break;
                case "battledivider": {
                        WindowSwitcher.GameWindow.AddToBattleLog("--------------------------------", Color.WhiteSmoke);
                    }
                    break;
                case "speechbubble": {
                        string text = parse[1];
                        string conID = parse[2];
                        IPlayer player = PlayerManager.Players[conID];
                        if (player != null) {
                            Logic.Graphics.Renderers.Sprites.SpeechBubble bubble = new Logic.Graphics.Renderers.Sprites.SpeechBubble();
                            bubble.SetBubbleText(text);
                            bubble.BubbleDisplayStart = Globals.Tick;
                            player.CurrentSpeech = bubble;
                        }
                    }
                    break;
                #endregion
                #region Maps
                case "checkformap": {
                        // Erase all players except self
                        //for (int i = 0; i < PlayerManager.Players.Count; i++) {
                        //    if (PlayerManager.Players.GetPlayerFromIndex(i).ID != PlayerManager.MyConnectionID) {
                        //        PlayerManager.Players.RemoveAt(i);
                        //    }
                        //}


                        string[] mapIDs = new string[9];
                        int[] revisions = new int[9];
                        bool[] tempChanges = new bool[9];
                        int n = 1;
                        for (int i = 0; i < 9; i++) {
                            mapIDs[i] = parse[n];

                            if (mapIDs[i] == "nm-1") {
                                // No map
                                revisions[i] = -1;
                                tempChanges[i] = false;
                                n += 1;
                            } else {
                                // A map is specified
                                revisions[i] = parse[n + 1].ToInt();
                                tempChanges[i] = parse[n + 2].ToBool();
                                n += 3;
                            }
                        }
                        //string mapID = parse[1];
                        //int revision = parse[2].ToInt();
                        //bool tempChange = parse[3].ToBool();

                        bool[] mapResults = new bool[9];
                        Enums.MapID[] mapIDEnums = new Enums.MapID[9] { Enums.MapID.TempActive, Enums.MapID.TempUp, Enums.MapID.TempDown, Enums.MapID.TempLeft, Enums.MapID.TempRight,
                                                                        Enums.MapID.TempTopLeft, Enums.MapID.TempBottomLeft, Enums.MapID.TempTopRight, Enums.MapID.TempBottomRight };

                        for (int i = 0; i < mapIDs.Length; i++) {
                            if (revisions[i] == -1) {
                                Maps.MapHelper.Maps[mapIDEnums[i]] = null;
                                mapResults[i] = false;
                            } else if (!tempChanges[i] && IO.IO.FileExists(IO.Paths.MapPath + "Map-" + mapIDs[i] + ".dat")) {
                                Maps.Map mapToTest = Maps.MapHelper.LoadMapFromFile(IO.Paths.MapPath + "Map-" + mapIDs[i] + ".dat");
                                if (mapToTest != null && mapToTest.Revision == revisions[i]) {
                                    mapResults[i] = true;
                                    mapToTest.MapID = mapIDs[i];
                                    Maps.MapHelper.Maps[mapIDEnums[i]] = mapToTest;
                                    if (i == 0) {
                                        //PlayerManager.MyPlayer.MapID = mapIDs[i];
                                    }
                                } else {
                                    mapResults[i] = false;
                                }
                            }
                        }
                        //if (!tempChange && IO.IO.FileExists(IO.Paths.MapPath + "Map-" + mapID.ToString() + ".dat")) {
                        //    Maps.Map mapToTest = Maps.MapHelper.LoadMapFromFile(IO.Paths.MapPath + "Map-" + mapID.ToString() + ".dat");
                        //    if (mapToTest != null && mapToTest.Revision == revision) {
                        //        mapToTest.MapID = mapID;
                        //        Maps.MapHelper.Maps[Enums.MapID.Temp] = mapToTest;
                        //        PlayerManager.MyPlayer.MapID = mapID;

                        //        //switch (mapToTest.Weather) {
                        //        //Globals.ActiveWeather = Globals.GameWeather;
                        //        //case Enums.Weather.None:
                        //        //Globals.ActiveTime = Globals.GameTime;
                        //        //Globals.ActiveWeather = Globals.GameWeather;
                        //        //break;
                        //        //case Enums.Weather.Raining:
                        //        //Globals.ActiveWeather = Enums.Weather.Raining;
                        //        //Globals.ActiveTime = Globals.GameTime;
                        //        //break;
                        //        //case Enums.Weather.Snowing:
                        //        //Globals.ActiveWeather = Enums.Weather.Snowing;
                        //        //Globals.ActiveTime = Globals.GameTime;
                        //        //break;
                        //        //case Enums.Weather.Thunder:
                        //        //Globals.ActiveWeather = Enums.Weather.Thunder;

                        //        //break;
                        //        //}
                        //        Globals.ActiveTime = Globals.GameTime;
                        //        // Close editors [Map + House editor]
                        //        Windows.Editors.EditorManager.CloseMapEditor();

                        //        Globals.SavingMap = false;

                        //        if (PlayerManager.MyPlayer.ActiveMission != null) {
                        //            PlayerManager.MyPlayer.ActiveMission.GoalX = -1;
                        //            PlayerManager.MyPlayer.ActiveMission.GoalY = -1;
                        //        }

                        //        Messenger.SendNeedMapResponse(false);
                        //        return;
                        //    }
                        //}
                        //if (PlayerManager.MyPlayer.ActiveMission != null) {
                        //    PlayerManager.MyPlayer.ActiveMission.GoalX = -1;
                        //    PlayerManager.MyPlayer.ActiveMission.GoalY = -1;
                        //}
                        Messenger.SendNeedMapResponse(mapResults);
                        //Messenger.SendNeedMapResponse(true);
                    }
                    break;
                case "mapdata": {
                        Maps.MapHelper.LoadMapFromPacket(parse);
                    }
                    break;
                case "tiledata": {
                        Maps.MapHelper.UpdateTile(parse);
                    }
                    break;
                case "mapitemdata": {
                        int n = 3;

                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        //if (PlayerManager.MyPlayer.SwitchingSeamlessMaps) {
                        //map = Maps.MapHelper.ActiveMap;
                        if (parse[2].ToBool()) mapSearchCounter += 9;
                        //} else {
                        //    //map = Maps.MapHelper.Maps[Enums.MapID.TempActive];
                        //}
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                            }
                        }
                        if (map != null) {
                            for (int i = 0; i < MaxInfo.MaxMapItems; i++) {
                                map.MapItems[i] = new Client.Logic.Maps.MapItem();
                                map.MapItems[i].Num = parse[n].ToInt();
                                map.MapItems[i].Value = parse[n + 1].ToInt();
                                map.MapItems[i].Sticky = parse[n + 2].ToBool();
                                map.MapItems[i].X = parse[n + 3].ToInt();
                                map.MapItems[i].Y = parse[n + 4].ToInt();
                                n += 5;
                            }
                        }
                    }
                    break;
                case "mapnpcdata": {
                        int n = 3;

                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        if (parse[2].ToBool()) mapSearchCounter += 9;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                            }
                        }
                        if (map != null) {
                            for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
                                map.MapNpcs[i] = new Client.Logic.Maps.MapNpc();
                                map.MapNpcs[i].Num = parse[n].ToInt();
                                map.MapNpcs[i].Sprite = parse[n + 1].ToInt();
                                map.MapNpcs[i].Form = parse[n+2].ToInt();
                                map.MapNpcs[i].Shiny = (Enums.Coloration)parse[n+3].ToInt();
                                map.MapNpcs[i].Sex = (Enums.Sex)parse[n+4].ToInt();
                                map.MapNpcs[i].Location = new Point(parse[n + 5].ToInt(), parse[n + 6].ToInt());
                                map.MapNpcs[i].Direction = (Enums.Direction)parse[n + 7].ToInt();
                                map.MapNpcs[i].StatusAilment = (Enums.StatusAilment)parse[n + 8].ToInt();
                                map.MapNpcs[i].Enemy = parse[n +9].ToBool();

                                //if (map.MapNpcs[i].Num > 0) {
                                //    map.MapNpcs[i].Sprite = Npc.NpcHelper.Npcs[map.MapNpcs[i].Num].Sprite;
                                //
                                //}
                                map.MapNpcs[i].ScreenActive = true;

                                n += 10;
                            }
                        }
                    }
                    break;
                case "seamlessmapchange": {
                        PlayerManager.MyPlayer.SwitchingSeamlessMaps = true;
                    }
                    break;
                case "mapdone": {
                        if (!(Maps.MapHelper.Maps[Enums.MapID.TempActive] != null && PlayerManager.MyPlayer.MapID == Maps.MapHelper.Maps[Enums.MapID.TempActive].MapID)) {
                            return;
                        }
                        
                        
                        //lock (PlayerManager.Players.Players) {
                        //    for (int i = 0; i < PlayerManager.Players.Players.Count; i++) {
                        //        if (PlayerManager.Players.Players.ValueByIndex(i).MapID != Maps.MapHelper.Maps[Enums.MapID.TempActive].MapID) {
                        //            PlayerManager.Players.Players.RemoveAt(i);
                        //        }
                        //    }
                        //}

                        bool isSameMap = false;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map tempMap = Maps.MapHelper.Maps[(Enums.MapID)(i + 9)];
                            if (tempMap != null) {
                                tempMap.Loaded = true;
                            }
                            if (i == 0) {
                                if (Maps.MapHelper.Maps[Enums.MapID.Active] == Maps.MapHelper.Maps[Enums.MapID.TempActive]) {
                                    isSameMap = true;
                                }
                            }
                            //if (i != 0) {
                            if ((i == 0 && !PlayerManager.MyPlayer.SwitchingSeamlessMaps) || (!PlayerManager.MyPlayer.SwitchingSeamlessMaps) || tempMap == null || Maps.MapHelper.ActiveMap == null) {
                                Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                            } else if (i == 0 && PlayerManager.MyPlayer.SwitchingSeamlessMaps) {
                                // Do nothing...
                            } else {
                                Maps.Map activeMap = Maps.MapHelper.ActiveMap;
                                switch ((Enums.MapID)i) {
                                    case Enums.MapID.Up: {
                                            if (tempMap.MapID == ("s" + activeMap.Up.ToString())) {
                                                Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                                            }
                                        }
                                        break;
                                    case Enums.MapID.Down: {
                                            if (tempMap.MapID == ("s" + activeMap.Down.ToString())) {
                                                Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                                            }
                                        }
                                        break;
                                    case Enums.MapID.Left: {
                                            if (tempMap.MapID == ("s" + activeMap.Left.ToString())) {
                                                Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                                            }
                                        }
                                        break;
                                    case Enums.MapID.Right: {
                                            if (tempMap.MapID == ("s" + activeMap.Right.ToString())) {
                                                Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                                            }
                                        }
                                        break;
                                    default: {
                                            Maps.MapHelper.Maps[(Enums.MapID)(i)] = tempMap;
                                        }
                                        break;
                                }
                            }
                            //}
                            //if (Maps.MapHelper.Maps[(Enums.MapID)(i)] != null) {
                            //    if (i == 0) {
                            //        if (PlayerManager.MyPlayer.MapID == Maps.MapHelper.Maps[(Enums.MapID)(i)].MapID) {
                            //            isSameMap = true;
                            //        } else {
                            //            PlayerManager.MyPlayer.MapID = Maps.MapHelper.Maps[(Enums.MapID)(i)].MapID;
                            //        }
                            //    }
                            //}
                            //Maps.MapHelper.Maps[(Enums.MapID)(i + 9)] = null;
                        }

                        if (!isSameMap) {
                            //PlayerManager.MyPlayer.MapID = Maps.MapHelper.Maps[Enums.MapID.Active].MapID;
                        }

                        Maps.MapHelper.ActiveMap.DoOverlayChecks();

                        Logic.Graphics.Effects.Overlays.ScreenOverlays.MapChangeInfoOverlay infoOverlay = Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenOverlay as Logic.Graphics.Effects.Overlays.ScreenOverlays.MapChangeInfoOverlay;
                        if (infoOverlay != null) {
                            if (infoOverlay.MinTimePassed) {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenOverlay = null;
                            }
                        }

                        PlayerManager.MyPlayer.SwitchingSeamlessMaps = false;
                        if (!isSameMap) {
                            WindowSwitcher.GameWindow.MapViewer.ActiveMap = Maps.MapHelper.ActiveMap;
                            if (PlayerManager.MyPlayer.Darkness > -2) {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(PlayerManager.MyPlayer.Darkness);
                            } else {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(Maps.MapHelper.ActiveMap.Darkness);
                            }
                            Logic.Graphics.Renderers.Screen.ScreenRenderer.DeactivateOffscreenSprites();
                            //Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetOverlay((Enums.Overlay)Maps.MapHelper.ActiveMap.Overlay);
                            ////Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetOverlay(Enums.Overlay.Sandstorm);
                            //Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetWea(Maps.MapHelper.ActiveMap.Weather);


                            PlayerManager.MyPlayer.SetCurrentRoom();
                            //Music.Music.AudioPlayer.PlayMusic(Maps.MapHelper.ActiveMap.Music);
                            ((Client.Logic.Music.Bass.BassAudioPlayer)Logic.Music.Music.AudioPlayer).FadeToNext(Maps.MapHelper.ActiveMap.Music, 1000);

                            if (Stories.StoryProcessor.ActiveStory != null && Stories.StoryProcessor.ActiveStory.Segments[Stories.StoryProcessor.ActiveStory.State.CurrentSegment].Action == Enums.StoryAction.Warp) {
                                if (Maps.MapHelper.ActiveMap.MapID == ((Stories.Segments.WarpSegment)Stories.StoryProcessor.ActiveStory.Segments[Stories.StoryProcessor.ActiveStory.State.CurrentSegment]).Map) {
                                    if (Stories.StoryProcessor.ActiveStory.State.StoryPaused) {
                                        Stories.StoryProcessor.ActiveStory.State.Unpause();
                                        Stories.StoryProcessor.ActiveStory.State.StoryPaused = false;
                                    }
                                }
                            }
                            Globals.GettingMap = false;
                            Globals.RefreshLock = false;
                            PlayerManager.MyPlayer.MovementLocked = false;
                            Client.Logic.Windows.Editors.EditorManager.CloseMapEditor();

                            //Messenger.SendMapLoaded();
                        }
                    }
                    break;
                case "weather": {
                        Enums.Weather weather = (Enums.Weather)parse[1].ToInt();
                        if (Globals.ActiveWeather != weather) {
                            switch (weather) {
                                case Enums.Weather.Ambiguous:
                                case Enums.Weather.None: {
                                        switch (Globals.ActiveWeather) {
                                            case Enums.Weather.Thunder:
                                            case Enums.Weather.Raining: {

                                                    WindowSwitcher.GameWindow.AddToBattleLog("The rain stopped.", Color.LightCyan);

                                                }
                                                break;
                                            case Enums.Weather.Snowing:
                                            case Enums.Weather.Snowstorm: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("It stopped snowing.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Hail: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The hail stopped.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.DiamondDust: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The sky returned to normal.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Cloudy: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The sky cleared up.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Fog: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The fog cleared!", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Sunny: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The sunlight faded.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Sandstorm: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The sandstorm subsided.", Color.LightCyan);
                                                }
                                                break;
                                            case Enums.Weather.Ashfall: {
                                                    WindowSwitcher.GameWindow.AddToBattleLog("The ashes settled down.", Color.LightCyan);
                                                }
                                                break;
                                        }
                                    }
                                    break;
                                case Enums.Weather.Raining: {
                                        if (Globals.ActiveWeather != Enums.Weather.Thunder) {
                                            WindowSwitcher.GameWindow.AddToBattleLog("It started to rain!", Color.LightCyan);
                                            Music.Music.AudioPlayer.PlaySoundEffect("magic617.wav");
                                        }
                                    }
                                    break;
                                case Enums.Weather.Snowing: {
                                        if (Globals.ActiveWeather != Enums.Weather.Snowstorm) {
                                            WindowSwitcher.GameWindow.AddToBattleLog("It started to snow!", Color.LightCyan);
                                            Music.Music.AudioPlayer.PlaySoundEffect("magic585.wav");
                                        }
                                    }
                                    break;
                                case Enums.Weather.Thunder: {
                                        if (Globals.ActiveWeather != Enums.Weather.Raining) {
                                            WindowSwitcher.GameWindow.AddToBattleLog("It started to rain!", Color.LightCyan);
                                            Music.Music.AudioPlayer.PlaySoundEffect("magic617.wav");
                                        }
                                    }
                                    break;
                                case Enums.Weather.Hail: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("It started to hail!", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic639.wav");
                                    }
                                    break;
                                case Enums.Weather.DiamondDust: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("The sky began to sparkle!", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic805.wav");
                                    }
                                    break;
                                case Enums.Weather.Cloudy: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("The sky became cloudy...", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic213.wav");
                                    }
                                    break;
                                case Enums.Weather.Fog: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("The fog is deep...", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic214.wav");
                                    }
                                    break;
                                case Enums.Weather.Sunny: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("The sunlight turned harsh!", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic636.wav");
                                    }
                                    break;
                                case Enums.Weather.Sandstorm: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("A sandstorm brewed!", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic618.wav");
                                    }
                                    break;
                                case Enums.Weather.Snowstorm: {
                                        if (Globals.ActiveWeather != Enums.Weather.Snowing) {
                                            WindowSwitcher.GameWindow.AddToBattleLog("It started to snow!", Color.LightCyan);
                                            Music.Music.AudioPlayer.PlaySoundEffect("magic579.wav");
                                        }
                                    }
                                    break;
                                case Enums.Weather.Ashfall: {
                                        WindowSwitcher.GameWindow.AddToBattleLog("Ashes filled the sky!", Color.LightCyan);
                                        Music.Music.AudioPlayer.PlaySoundEffect("magic487.wav");
                                    }
                                    break;
                            }

                            Globals.ActiveWeather = weather;
                            if (Maps.MapHelper.ActiveMap != null) {
                                Maps.MapHelper.ActiveMap.DoOverlayChecks();
                            }
                        }
                    }
                    break;
                case "darkness": {
                        Maps.MapHelper.ActiveMap.Darkness = parse[1].ToInt();
                        if (PlayerManager.MyPlayer != null && PlayerManager.MyPlayer.Darkness > -2) {
                            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(PlayerManager.MyPlayer.Darkness);
                        } else {
                            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(Maps.MapHelper.ActiveMap.Darkness);
                        }

                    }
                    break;
                case "mapkey": {
                        Maps.Map map = Maps.MapHelper.Maps[Enums.MapID.Active];
                        int x = parse[1].ToInt();
                        int y = parse[2].ToInt();
                        bool value = parse[3].ToBool();
                        map.Tile[x, y].DoorOpen = value;
                    }
                    break;
                case "floorchangedisplay": {
                        string dungeonName = parse[1];
                        int minDisplayTime = parse[2].ToInt();

                        Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenOverlay = new Logic.Graphics.Effects.Overlays.ScreenOverlays.MapChangeInfoOverlay(dungeonName, minDisplayTime);
                    }
                    break;
                case "spritechange": {
                        Messenger.SendPacket(TcpPacket.CreatePacket("buysprite"));
                    }
                    break;
                #endregion
                #region Npcs
                case "npchp": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();

                            map.MapNpcs[n].HP = parse[3].ToInt();
                            map.MapNpcs[n].MaxHP = parse[4].ToInt();
                        }
                    }
                    break;
                case "npcsprite": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();
                            map.MapNpcs[n].Sprite = parse[3].ToInt();
                            map.MapNpcs[n].Form = parse[4].ToInt();
                            map.MapNpcs[n].Shiny = (Enums.Coloration)parse[5].ToInt();
                            map.MapNpcs[n].Sex = (Enums.Sex)parse[6].ToInt();
                        }
                    }
                    break;
                case "npcvolatilestatus": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();
                            map.MapNpcs[n].VolatileStatus.Clear();
                            for (int i = 0; i < parse[3].ToInt(); i++) {
                                map.MapNpcs[n].VolatileStatus.Add(parse[4 + i].ToInt());
                            }
                        }

                    }
                    break;
                case "npcconfuse": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();

                            //map.MapNpcs[n].Confused = parse[3].ToBool();
                        }

                    }
                    break;
                case "npcattack": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();

                            map.MapNpcs[n].Attacking = true;
                            map.MapNpcs[n].AttackTimer = Globals.Tick + 1000;
                            map.MapNpcs[n].TotalAttackTime = 1000;
                        }

                    }
                    break;
                case "npcxy": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {

                            int n = parse[2].ToInt();
                            map.MapNpcs[n].X = parse[3].ToInt();
                            map.MapNpcs[n].Y = parse[4].ToInt();
                        }
                    }
                    break;
                case "npcdir": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();
                            map.MapNpcs[n].Direction = (Enums.Direction)parse[3].ToInt();
                            map.MapNpcs[n].Offset = new Point(0, 0);
                            map.MapNpcs[n].MovementSpeed = Enums.MovementSpeed.Standing;
                        }
                    }
                    break;
                case "npcstatus": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            int n = parse[2].ToInt();

                            map.MapNpcs[n].StatusAilment = (Enums.StatusAilment)parse[3].ToInt();
                        }

                    }
                    break;
                case "nm": {
                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[2]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null && map.Loaded) {
                            ByteUnpacker unpacker = new ByteUnpacker();
                            unpacker.AddRange(MaxInfo.MAX_MAP_NPCS); // Map npcs
                            unpacker.AddRange(4); // Direction
                            unpacker.AddRange(7); // Movement speed

                            unpacker.UnpackByte(parse[1].ToInt());
                            //int dataByte = parse[1].ToInt();
                            //int mapNpcNum = dataByte % 16;
                            //int directionNum = (dataByte % 64) / 16;
                            //int speed = dataByte / 64;

                            int i = unpacker.UnpackedItems[0].Value;
                            //int i = parse[1].ToInt();
                            //int x = parse[2].ToInt();
                            //int y = parse[3].ToInt();
                            Enums.Direction dir = (Enums.Direction)unpacker.UnpackedItems[1].Value;//(Enums.Direction)directionNum;//parse[2].ToInt();
                            Enums.MovementSpeed n = (Enums.MovementSpeed)unpacker.UnpackedItems[2].Value;//(Enums.MovementSpeed)speed;//parse[3].ToInt();

                            //Maps.MapHelper.ActiveMap.MapNpcs[i].X = x;
                            //Maps.MapHelper.ActiveMap.MapNpcs[i].Y = y;
                            map.MapNpcs[i].Direction = dir;
                            map.MapNpcs[i].Offset = new Point(0, 0);
                            map.MapNpcs[i].MovementSpeed = n;

                            bool seen = Logic.Graphics.Renderers.Screen.ScreenRenderer.CanBeSeen(map.MapNpcs[i].X, map.MapNpcs[i].Y, targetMapID);
                            
                            switch (map.MapNpcs[i].Direction) {
                                case Enums.Direction.Up:
                                    map.MapNpcs[i].Y--;
                                    map.MapNpcs[i].Offset = new Point(map.MapNpcs[i].Offset.X, Constants.TILE_HEIGHT);
                                    break;
                                case Enums.Direction.Down:
                                    map.MapNpcs[i].Y++;
                                    map.MapNpcs[i].Offset = new Point(map.MapNpcs[i].Offset.X, Constants.TILE_HEIGHT * -1);
                                    break;
                                case Enums.Direction.Left:
                                    map.MapNpcs[i].X--;
                                    map.MapNpcs[i].Offset = new Point(Constants.TILE_WIDTH, map.MapNpcs[i].Offset.Y);
                                    break;
                                case Enums.Direction.Right:
                                    map.MapNpcs[i].X++;
                                    map.MapNpcs[i].Offset = new Point(Constants.TILE_WIDTH * -1, map.MapNpcs[i].Offset.Y);
                                    break;
                            }

                            if (seen != Logic.Graphics.Renderers.Screen.ScreenRenderer.CanBeSeen(map.MapNpcs[i].X, map.MapNpcs[i].Y, targetMapID)) {
                                map.MapNpcs[i].Leaving = true;
                            }

                            if (map.MapNpcs[i].WalkingFrame == -1) {
                                map.MapNpcs[i].LastWalkTime = Globals.Tick;
                                map.MapNpcs[i].WalkingFrame = 0;
                            }
                        }
                    }
                    break;
                case "spawnnpc": {
                        int n = parse[2].ToInt();

                        Maps.Map map = null;
                        int mapSearchCounter = 0;
                        Enums.MapID targetMapID = Enums.MapID.Active;
                        for (int i = 0; i < 9; i++) {
                            Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                            if (testMap != null && testMap.MapID == parse[1]) {
                                map = testMap;
                                targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                break;
                            }
                        }
                        if (map != null) {
                            map.MapNpcs[n] = new Maps.MapNpc();
                            map.MapNpcs[n].Num = parse[3].ToInt();
                            map.MapNpcs[n].Sprite = parse[4].ToInt();
                            map.MapNpcs[n].Form = parse[5].ToInt();
                            map.MapNpcs[n].Shiny = (Enums.Coloration)parse[6].ToInt();
                            map.MapNpcs[n].Sex = (Enums.Sex)parse[7].ToInt();
                            map.MapNpcs[n].X = parse[8].ToInt();
                            map.MapNpcs[n].Y = parse[9].ToInt();
                            map.MapNpcs[n].Direction = (Enums.Direction)parse[10].ToInt();
                            map.MapNpcs[n].StatusAilment = (Enums.StatusAilment)parse[11].ToInt();
                            map.MapNpcs[n].Enemy = parse[12].ToBool();
                            map.MapNpcs[n].ScreenActive = true;

                            // Client use only
                            //if (map.MapNpcs[n].Num > 0) {
                            //    map.MapNpcs[n].Sprite = Npc.NpcHelper.Npcs[map.MapNpcs[n].Num].Sprite;
                            //}
                            map.MapNpcs[n].Offset = new Point();
                            map.MapNpcs[n].MovementSpeed = Enums.MovementSpeed.Standing;

                            Logic.Graphics.Renderers.Screen.ScreenRenderer.DeactivateOffscreenNpcs();
                        }
                    }
                    break;
                case "npcactive": {
                        //int n = parse[1].ToInt();

                        for (int i = 0; i < 9; i++) {
                            if (Maps.MapHelper.Maps[(Enums.MapID)i] != null && Maps.MapHelper.Maps[(Enums.MapID)i].Loaded) {
                                if (Maps.MapHelper.Maps[(Enums.MapID)i].MapID == parse[1]) {
                                    Maps.MapHelper.Maps[(Enums.MapID)i].MapNpcs[parse[2].ToInt()].ScreenActive = parse[3].ToBool();
                                }
                            }
                        }

                        //Maps.MapHelper.ActiveMap.MapNpcs[n].ScreenActive = true;
                    }
                    break;
                case "npcdead": {
                        int n = parse[1].ToInt();

                        Maps.MapHelper.ActiveMap.MapNpcs[n] = new Maps.MapNpc();
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].Num = 0;
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].X = 0;
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].Y = 0;
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].Direction = 0;

                        // Client use only
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].Offset = new Point();
                        //Maps.MapHelper.ActiveMap.MapNpcs[n].MovementSpeed = Enums.MovementSpeed.Standing;
                    }
                    break;
                #endregion
                #region Items
                case "spawnitem": {
                        int slot = parse[1].ToInt();

                        Maps.MapHelper.ActiveMap.MapItems[slot] = new Client.Logic.Maps.MapItem();
                        Maps.MapHelper.ActiveMap.MapItems[slot].Num = parse[2].ToInt();
                        Maps.MapHelper.ActiveMap.MapItems[slot].Value = parse[3].ToInt();
                        Maps.MapHelper.ActiveMap.MapItems[slot].Sticky = parse[4].ToBool();
                        Maps.MapHelper.ActiveMap.MapItems[slot].X = parse[5].ToInt();
                        Maps.MapHelper.ActiveMap.MapItems[slot].Y = parse[6].ToInt();

                    }
                    break;
                case "updateitem": {
                        int n = parse[1].ToInt();

                        Items.ItemHelper.Items[n].Name = parse[2];
                        Items.ItemHelper.Items[n].Desc = parse[3];
                        Items.ItemHelper.Items[n].Pic = parse[4].ToInt();
                        Items.ItemHelper.Items[n].Type = (Enums.ItemType)parse[5].ToInt();
                        Items.ItemHelper.Items[n].Data1 = parse[6].ToInt();
                        Items.ItemHelper.Items[n].Data2 = parse[7].ToInt();
                        Items.ItemHelper.Items[n].Data3 = parse[8].ToInt();
                        Items.ItemHelper.Items[n].Price = parse[9].ToInt();
                        Items.ItemHelper.Items[n].StackCap = parse[10].ToInt();
                        Items.ItemHelper.Items[n].Bound = parse[11].ToBool();
                        Items.ItemHelper.Items[n].Loseable = parse[12].ToBool();
                        Items.ItemHelper.Items[n].Rarity = parse[13].ToInt();
                        Items.ItemHelper.Items[n].AttackReq = parse[14].ToInt();
                        Items.ItemHelper.Items[n].DefenseReq = parse[15].ToInt();
                        Items.ItemHelper.Items[n].SpAtkReq = parse[16].ToInt();
                        Items.ItemHelper.Items[n].SpDefReq = parse[17].ToInt();
                        Items.ItemHelper.Items[n].SpeedReq = parse[18].ToInt();
                        Items.ItemHelper.Items[n].ScriptedReq = parse[19].ToInt();
                        Items.ItemHelper.Items[n].AddHP = parse[20].ToInt();
                        Items.ItemHelper.Items[n].AddPP = parse[21].ToInt();
                        Items.ItemHelper.Items[n].AddAttack = parse[22].ToInt();
                        Items.ItemHelper.Items[n].AddDefense = parse[23].ToInt();
                        Items.ItemHelper.Items[n].AddSpAtk = parse[24].ToInt();
                        Items.ItemHelper.Items[n].AddSpDef = parse[25].ToInt();
                        Items.ItemHelper.Items[n].AddSpeed = parse[26].ToInt();
                        Items.ItemHelper.Items[n].AddEXP = parse[27].ToInt();
                        Items.ItemHelper.Items[n].AttackSpeed = parse[28].ToInt();
                        Items.ItemHelper.Items[n].RecruitBonus = parse[29].ToInt();


                        if (WindowSwitcher.FindWindow("winItemPanel") != null) {
                            ((Windows.Editors.winItemPanel)WindowSwitcher.FindWindow("winItemPanel")).RefreshItemList();
                        }
                    }
                    break;
                case "openbank": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.OpenBankOptions();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "playerbank": {
                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank") != null) {
                            ((Menus.mnuBank)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank")).LoadBankItems(parse);
                        }
                    }
                    break;
                case "playerbankupdate": {

                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank") != null) {
                            ((Menus.mnuBank)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank")).BankItems[parse[1].ToInt() - 1].Num = parse[2].ToInt();
                            ((Menus.mnuBank)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank")).BankItems[parse[1].ToInt() - 1].Value = parse[3].ToInt();
                            ((Menus.mnuBank)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank")).DisplayItems(((Menus.mnuBank)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuBank")).currentTen * 10);
                        }
                    }
                    break;
                case "openshop": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.OpenShopOptions();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "trade": {
                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShop") != null) {
                            ((Menus.mnuShop)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShop")).LoadShopItems(parse);
                        }
                    }
                    break;
                case "moverecallmenu": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.LinkShopRecallMenu();
                        ((Menus.mnuMoveRecall)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoveRecall")).LoadRecallMoves(parse);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");

                    }
                    break;
                #endregion
                #region Players
                case "leftmap": {
                    PlayerManager.Players.Remove(parse[1]);
                        //lock (PlayerManager.Players.Players) {
                        //    if (PlayerManager.Players.Players.ContainsKey(parse[1])) {
                        //        PlayerManager.Players.Players.RemoveAtKey(parse[1]);
                        //    }
                        //}
                    }
                    break;
                case "playerdata": {
                        string id = parse[1];
                        IPlayer player = new GenericPlayer();
                        player.Name = parse[2];
                        player.Sprite = parse[3].ToInt();
                        player.Form = parse[4].ToInt();
                        player.Shiny = (Enums.Coloration)parse[5].ToInt();
                        player.Sex = (Enums.Sex)parse[6].ToInt();
                        player.MapID = parse[7];
                        player.X = parse[8].ToInt();
                        player.Y = parse[9].ToInt();
                        player.Direction = (Enums.Direction)parse[10].ToInt();
                        player.Access = (Enums.Rank)parse[11].ToInt();
                        player.Hunted = parse[12].ToBool();
                        player.Dead = parse[13].ToBool();
                        player.Guild = parse[14];
                        player.GuildAccess = (Enums.GuildRank)parse[15].ToInt();
                        player.Status = parse[16];
                        //player.Confused = parse[14].ToBool();
                        player.StatusAilment = (Enums.StatusAilment)parse[18].ToInt();
                        for (int i = 0; i < parse[19].ToInt(); i++) {
                            player.VolatileStatus.Add(parse[20 + i].ToInt());
                        }
                        // Make sure they aren't walking
                        player.MovementSpeed = Enums.MovementSpeed.Standing;
                        player.Offset = new Point();
                        player.ScreenActive = true;
                        
                        PlayerManager.Players.Add(id, player);
                        Logic.Graphics.Renderers.Screen.ScreenRenderer.DeactivateOffscreenPlayers();
                    }
                    break;
                case "playerguild": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.Guild = parse[2];
                            player.GuildAccess = (Enums.GuildRank)parse[3].ToInt();
                        }
                    }
                    break;
                case "myplayerdata": {
                        PlayerManager.MyPlayer.ActiveTeamNum = parse[1].ToInt();
                        PlayerManager.MyPlayer.Name = parse[2];
                        PlayerManager.MyPlayer.Sprite = parse[3].ToInt();
                        PlayerManager.MyPlayer.Form = parse[4].ToInt();
                        PlayerManager.MyPlayer.Shiny = (Enums.Coloration)parse[5].ToInt();
                        PlayerManager.MyPlayer.Sex = (Enums.Sex)parse[6].ToInt();
                        PlayerManager.MyPlayer.MapID = parse[7];
                        PlayerManager.MyPlayer.X = parse[8].ToInt();
                        PlayerManager.MyPlayer.Y = parse[9].ToInt();
                        PlayerManager.MyPlayer.Direction = (Enums.Direction)parse[10].ToInt();
                        PlayerManager.MyPlayer.Access = (Enums.Rank)parse[11].ToInt();
                        PlayerManager.MyPlayer.Hunted = parse[12].ToBool();
                        PlayerManager.MyPlayer.Dead = parse[13].ToBool();
                        PlayerManager.MyPlayer.Guild = parse[14];
                        PlayerManager.MyPlayer.GuildAccess = (Enums.GuildRank)parse[15].ToInt();
                        PlayerManager.MyPlayer.Solid = parse[16].ToBool();
                        PlayerManager.MyPlayer.Status = parse[17];
                        PlayerManager.MyPlayer.Confused = parse[18].ToBool();
                        PlayerManager.MyPlayer.StatusAilment = (Enums.StatusAilment)parse[19].ToInt();
                        PlayerManager.MyPlayer.SpeedLimit = (Enums.MovementSpeed)parse[20].ToInt();
                        int mobility = parse[21].ToInt();
                        for (int i = 0; i < 16; i++) {
                            if (mobility % 2 == 1) {
                                PlayerManager.MyPlayer.Mobility[i] = true;
                            } else {
                                PlayerManager.MyPlayer.Mobility[i] = false;
                            }
                            mobility /= 2;
                        }
                        PlayerManager.MyPlayer.TimeMultiplier = parse[22].ToInt();
                        
                        for (int i = 0; i < parse[24].ToInt(); i++) {
                            PlayerManager.MyPlayer.VolatileStatus.Add(parse[25 + i].ToInt());
                        }

                        // Make sure they aren't walking
                        PlayerManager.MyPlayer.MovementSpeed = Enums.MovementSpeed.Standing;
                        PlayerManager.MyPlayer.Offset = new Point();

                        if (PlayerManager.MyPlayer.Darkness != parse[23].ToInt()) {
                            PlayerManager.MyPlayer.Darkness = parse[23].ToInt();
                            if (PlayerManager.MyPlayer.Darkness > -2) {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(PlayerManager.MyPlayer.Darkness);
                            } else if (Maps.MapHelper.ActiveMap != null) {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(Maps.MapHelper.ActiveMap.Darkness);
                            }
                        }
                    }
                    break;
                case "playeractive": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.ScreenActive = parse[2].ToBool();
                        }
                    }
                    break;
                case "playermove": {
                        string id = parse[1];
                        string mapID = parse[2];
                        int x = parse[3].ToInt();
                        int y = parse[4].ToInt();
                        Enums.Direction dir = (Enums.Direction)parse[5].ToInt();
                        Enums.MovementSpeed n = (Enums.MovementSpeed)parse[6].ToInt();

                        IPlayer player = PlayerManager.Players[id];

                        if (player != null) {
                            if (!(dir < Enums.Direction.Up || dir > Enums.Direction.Right)) {

                                Maps.Map map = null;
                                int mapSearchCounter = 0;
                                Enums.MapID targetMapID = Enums.MapID.Active;
                                for (int i = 0; i < 9; i++) {
                                    Maps.Map testMap = Maps.MapHelper.Maps[(Enums.MapID)(i + mapSearchCounter)];
                                    if (testMap != null && testMap.MapID == mapID) {
                                        map = testMap;
                                        targetMapID = (Enums.MapID)(i + mapSearchCounter);
                                        break;
                                    }
                                }
                                if (map != null) {

                                    if (Logic.Graphics.Renderers.Screen.ScreenRenderer.CanBeSeen(player.X, player.Y, targetMapID)) {
                                        player.Leaving = true;
                                    }

                                }

                                player.MapID = mapID;
                                player.X = x;
                                player.Y = y;
                                player.Direction = dir;

                                player.Offset = new Point();
                                player.MovementSpeed = n;

                                switch (player.Direction) {
                                    case Enums.Direction.Up:
                                        player.Offset = new Point(player.Offset.X, Constants.TILE_HEIGHT);
                                        break;
                                    case Enums.Direction.Down:
                                        player.Offset = new Point(player.Offset.X, Constants.TILE_HEIGHT * -1);
                                        break;
                                    case Enums.Direction.Left:
                                        player.Offset = new Point(Constants.TILE_WIDTH, player.Offset.Y);
                                        break;
                                    case Enums.Direction.Right:
                                        player.Offset = new Point(Constants.TILE_WIDTH * -1, player.Offset.Y);
                                        break;
                                }
                                if (player.WalkingFrame == -1) {
                                    player.LastWalkTime = Globals.Tick;
                                    player.WalkingFrame = 0;
                                }
                            }
                        }
                    }
                    break;
                case "attack": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];

                        if (player != null) {
                            player.Attacking = true;
                            player.AttackTimer = Globals.Tick + 1000;
                            player.TotalAttackTime = 1000;
                        }
                    }
                    break;
                case "playerdir": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];

                        if (player != null) {
                            player.Direction = (Enums.Direction)parse[2].ToInt();

                            player.MovementSpeed = Enums.MovementSpeed.Standing;
                            player.Offset = new Point();
                        }
                    }
                    break;
                case "myplayerguild": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Guild = parse[1];
                            PlayerManager.MyPlayer.GuildAccess = (Enums.GuildRank)parse[2].ToInt();
                        }
                    }
                    break;
                case "volatilestatus": {
                    if (PlayerManager.MyPlayer != null) {
                        PlayerManager.MyPlayer.VolatileStatus.Clear();
                        for (int i = 0; i < parse[1].ToInt(); i++) {
                            PlayerManager.MyPlayer.VolatileStatus.Add(parse[2 + i].ToInt());
                        }
                    }
                    }
                    break;
                case "emote": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.CurrentEmote = new Client.Logic.Graphics.Renderers.Sprites.Emoticon(parse[1].ToInt(), parse[2].ToInt(), parse[3].ToInt());
                        }
                    }
                    break;
                case "confusion": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Confused = parse[1].ToBool();
                        }
                    }
                    break;
                case "playervolatilestatus": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.VolatileStatus.Clear();
                            for (int i = 0; i < parse[2].ToInt(); i++) {
                                player.VolatileStatus.Add(parse[3 + i].ToInt());
                            }
                        }
                    }
                    break;
                case "playeremote": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.CurrentEmote = new Client.Logic.Graphics.Renderers.Sprites.Emoticon(parse[2].ToInt(), parse[3].ToInt(), parse[4].ToInt());
                        }
                    }
                    break;
                case "playersprite": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.Sprite = parse[2].ToInt();
                            player.Form = parse[2].ToInt();
                            player.Shiny = (Enums.Coloration)parse[3].ToInt();
                            player.Sex = (Enums.Sex)parse[4].ToInt();
                        }
                    }
                    break;
                case "playerdead": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.Dead = parse[2].ToBool();
                        }
                    }
                    break;
                case "playerhunted": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.Hunted = parse[2].ToBool();
                        }
                    }
                    break;
                    break;
                case "statusailment": {
                        PlayerManager.MyPlayer.StatusAilment = (Enums.StatusAilment)parse[1].ToInt();
                        WindowSwitcher.GameWindow.ActiveTeam.DisplayRecruitStatusAilment(PlayerManager.MyPlayer.ActiveTeamNum);
                    }
                    break;
                case "playerstatused": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.StatusAilment = (Enums.StatusAilment)parse[2].ToInt();
                        }
                    }
                    break;
                case "speedlimit": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.SpeedLimit = (Enums.MovementSpeed)parse[1].ToInt();
                        }
                    }
                    break;
                case "sprite": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Sprite = parse[1].ToInt();
                            PlayerManager.MyPlayer.Form = parse[2].ToInt();
                            PlayerManager.MyPlayer.Shiny = (Enums.Coloration)parse[3].ToInt();
                            PlayerManager.MyPlayer.Sex = (Enums.Sex)parse[4].ToInt();
                        }

                    }
                    break;
                case "dead": {

                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Dead = parse[1].ToBool();
                        }
                    }
                    break;
                case "hunted": {

                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.Hunted = parse[1].ToBool();
                        }
                    }
                    break;
                case "mobility": {
                        if (PlayerManager.MyPlayer != null) {
                            int mobility = parse[1].ToInt();
                            for (int i = 0; i < 16; i++) {
                                if (mobility % 2 == 1) {
                                    PlayerManager.MyPlayer.Mobility[i] = true;
                                } else {
                                    PlayerManager.MyPlayer.Mobility[i] = false;
                                }
                                mobility /= 2;
                            }
                        }
                    }
                    break;
                case "timemultiplier": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.TimeMultiplier = parse[1].ToInt();
                        }

                    }
                    break;
                case "selfdarkness": {
                        if (PlayerManager.MyPlayer != null && parse[1].ToInt() != PlayerManager.MyPlayer.Darkness) {
                            PlayerManager.MyPlayer.Darkness = parse[1].ToInt();

                            if (PlayerManager.MyPlayer.Darkness > -2) {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(PlayerManager.MyPlayer.Darkness);
                            } else {
                                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetDarkness(Maps.MapHelper.ActiveMap.Darkness);
                            }
                        }

                    }
                    break;
                case "visibility": {
                        if (PlayerManager.MyPlayer != null) {
                            PlayerManager.MyPlayer.ScreenActive = parse[1].ToBool();
                        }

                    }
                    break;
                case "playerinv": {


                        MyPlayer myPlayer = PlayerManager.MyPlayer;
                        if (MaxInfo.MaxInv != parse[1].ToInt()) {
                            MaxInfo.MaxInv = parse[1].ToInt();
                            myPlayer.Inventory = new Inventory(MaxInfo.MaxInv);
                        }
                        int n = 2;
                        for (int i = 1; i <= MaxInfo.MaxInv; i++) {
                            myPlayer.Inventory[i].Num = parse[n].ToInt();
                            myPlayer.Inventory[i].Value = parse[n + 1].ToInt();
                            myPlayer.Inventory[i].Sticky = parse[n + 2].ToBool();

                            n += 3;
                        }


                    }
                    break;
                case "playerinvupdate": {

                        PlayerManager.MyPlayer.Inventory[parse[1].ToInt()].Num = parse[2].ToInt();
                        PlayerManager.MyPlayer.Inventory[parse[1].ToInt()].Value = parse[3].ToInt();
                        PlayerManager.MyPlayer.Inventory[parse[1].ToInt()].Sticky = parse[4].ToBool();

                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory") != null) {
                            ((Menus.mnuInventory)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory")).UpdateVisibleItem(parse[1].ToInt());
                            ((Menus.mnuInventory)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuInventory")).UpdateSelectedItemInfo();
                        }
                    }
                    break;
                case "playerxy": {
                        string id = parse[1];
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            player.X = parse[2].ToInt();
                            player.Y = parse[3].ToInt();

                            player.MovementSpeed = Enums.MovementSpeed.Standing;
                            player.Offset = new Point();

                            if (id == PlayerManager.MyConnectionID) {
                                if (Stories.StoryProcessor.ActiveStory != null && Stories.StoryProcessor.ActiveStory.Segments[Stories.StoryProcessor.ActiveStory.State.CurrentSegment].Action == Enums.StoryAction.Warp) {
                                    if (Maps.MapHelper.ActiveMap.MapID == ((Stories.Segments.WarpSegment)Stories.StoryProcessor.ActiveStory.Segments[Stories.StoryProcessor.ActiveStory.State.CurrentSegment]).Map) {
                                        if (Stories.StoryProcessor.ActiveStory.State.StoryPaused) {
                                            Stories.StoryProcessor.ActiveStory.State.Unpause();
                                            Stories.StoryProcessor.ActiveStory.State.StoryPaused = false;
                                        }
                                    }
                                }

                                Globals.GettingMap = false;
                            }
                        }
                    }
                    break;
                case "movementlock": {
                    PlayerManager.MyPlayer.MovementLocked = parse[1].ToBool();
                    }
                    break;
                case "friendslist": {
                        int count = parse[1].ToInt();
                        PlayerManager.MyPlayer.FriendsList.Clear();
                        int n = 2;
                        for (int i = 0; i < count; i++) {
                            Friend friend = new Friend();
                            friend.Name = parse[n];
                            friend.Online = parse[n + 1].ToBool();
                            PlayerManager.MyPlayer.FriendsList.Add(friend);

                            n += 2;
                        }
                        if (Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule.ModuleID == Enums.ExpKitModules.FriendsList) {
                            ExpKit.Modules.kitFriendsList kitFriendsList = (ExpKit.Modules.kitFriendsList)Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule;
                            kitFriendsList.UpdateList(PlayerManager.MyPlayer.FriendsList);
                        }
                    }
                    break;
                #endregion
                #region Party
                case "partymemberdata": {
                        int slot = parse[1].ToInt();
                        if (PlayerManager.MyPlayer.Party == null) {
                            PlayerManager.MyPlayer.Party = new PartyData();
                        }
                        PlayerManager.MyPlayer.Party.LoadMember(slot, parse[2], parse[3].ToInt(),
                             parse[4].ToInt(), (Enums.Coloration)parse[5].ToInt(), (Enums.Sex)parse[6].ToInt(), parse[7].ToUlng(), parse[8].ToUlng(),
                                         parse[9].ToInt(), parse[10].ToInt());
                        if (Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule.ModuleID == Enums.ExpKitModules.Party) {
                            ExpKit.Modules.kitParty kitParty = (ExpKit.Modules.kitParty)Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule;
                            kitParty.DisplayPartyMemberData(slot);
                        }
                    }
                    break;
                case "clearpartyslot": {
                        int slot = parse[1].ToInt();
                        if (PlayerManager.MyPlayer.Party == null) {
                            PlayerManager.MyPlayer.Party = new PartyData();
                        }
                        PlayerManager.MyPlayer.Party.ClearSlot(slot);
                        if (Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule.ModuleID == Enums.ExpKitModules.Party) {
                            ExpKit.Modules.kitParty kitParty = (ExpKit.Modules.kitParty)Windows.WindowSwitcher.ExpKit.KitContainer.ActiveModule;
                            kitParty.DisplayPartyMemberData(slot);
                        }
                        break;
                    }
                case "disbandparty": {
                        PlayerManager.MyPlayer.Party = new PartyData();
                        break;
                    }
                #endregion
                #region Guild
                case "createguild": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.ShowGuildCreate(parse);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "guildmenu": {
                        //PlayerManager.MyPlayer.MovementLocked = false;
                        Menus.MenuSwitcher.ShowGuildManage(parse);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "fullguildupdate": {
                        Menus.mnuGuildManage menu = (Menus.mnuGuildManage)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuGuildManage");
                        if (menu != null) {
                            menu.LoadGuildFromPacket(parse);
                        }
                    }
                    break;
                case "guildupdate": {
                    Menus.mnuGuildManage menu = (Menus.mnuGuildManage)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuGuildManage");
                        if (menu != null) {
                            menu.UpdateMember(parse[1].ToInt(), (Enums.GuildRank)parse[2].ToInt());
                        }
                    }
                    break;
                case "guildadd": {
                        Menus.mnuGuildManage menu = (Menus.mnuGuildManage)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuGuildManage");
                        if (menu != null) {
                            menu.AddMember(parse[1]);
                        }
                    }
                    break;
                case "guildremove": {
                        Menus.mnuGuildManage menu = (Menus.mnuGuildManage)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuGuildManage");
                        if (menu != null) {
                            menu.RemoveMember(parse[1].ToInt());
                        }
                    }
                    break;
                #endregion
                #region Moves
                case "moves": {
                        int n = 1;

                        for (int i = 0; i < MaxInfo.MAX_PLAYER_MOVES; i++) {
                            Players.RecruitMove move = new Players.RecruitMove();
                            move.MoveNum = parse[n].ToInt();
                            move.CurrentPP = parse[n + 1].ToInt();
                            move.MaxPP = parse[n + 2].ToInt();
                            move.Sealed = parse[n + 3].ToBool();
                            if (PlayerManager.MyPlayer != null) {
                                PlayerManager.MyPlayer.Moves[i] = move;
                            }

                            n += 4;
                        }
                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoves") != null) {
                            ((Menus.mnuMoves)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoves")).DisplayMoves();
                        }
                    }
                    break;
                case "scriptspellanim": {
                        Logic.Graphics.Renderers.Moves.IMoveAnimation animation;

                        switch ((Enums.MoveAnimationType)parse[1].ToInt()) {
                            case Enums.MoveAnimationType.Normal: {
                                    animation = new Logic.Graphics.Renderers.Moves.NormalMoveAnimation(parse[5].ToInt(), parse[6].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.Overlay: {
                                    animation = new Logic.Graphics.Renderers.Moves.OverlayMoveAnimation();
                                }
                                break;
                            case Enums.MoveAnimationType.Tile: {
                                    animation = new Logic.Graphics.Renderers.Moves.TileMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), (Enums.MoveRange)parse[7].ToInt(), (Enums.Direction)parse[8].ToInt(), parse[9].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.Arrow: {
                                    animation = new Logic.Graphics.Renderers.Moves.ArrowMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), (Enums.Direction)parse[7].ToInt(), parse[8].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.Throw: {
                                    animation = new Logic.Graphics.Renderers.Moves.ThrowMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), parse[7].ToInt(), parse[8].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.Beam: {
                                    animation = new Logic.Graphics.Renderers.Moves.BeamMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), (Enums.Direction)parse[7].ToInt(), parse[8].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.ItemArrow: {
                                    animation = new Logic.Graphics.Renderers.Moves.ItemArrowMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), (Enums.Direction)parse[7].ToInt(), parse[8].ToInt());
                                }
                                break;
                            case Enums.MoveAnimationType.ItemThrow: {
                                    animation = new Logic.Graphics.Renderers.Moves.ItemThrowMoveAnimation(parse[5].ToInt(), parse[6].ToInt(), parse[7].ToInt(), parse[8].ToInt());
                                }
                                break;
                            default: {
                                    animation = null;
                                }
                                break;
                        }

                        if (animation != null) {
                            animation.AnimationIndex = parse[2].ToInt();
                            animation.RenderLoops = parse[4].ToInt();
                            animation.FrameLength = parse[3].ToInt();
                            animation.Active = true;
                            Logic.Graphics.Renderers.Moves.MoveRenderer.ActiveAnimations.Add(animation);
                        }
                    }
                    break;
                case "moveppupdate": {
                        int slot = parse[1].ToInt();
                        PlayerManager.MyPlayer.Moves[slot].CurrentPP = parse[2].ToInt();
                        PlayerManager.MyPlayer.Moves[slot].MaxPP = parse[3].ToInt();

                        if (WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoves") != null) {
                            ((Menus.mnuMoves)WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoves")).DisplayMoves();
                        }
                    }
                    break;
                case "openmoveforgetmenu": {
                        Menus.MenuSwitcher.ShowMenu(new Menus.mnuMoveOverwrite("mnuMoveOverwrite"));
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }

                    break;
                #endregion



                #region Misc
                case "sound": {
                        Music.Music.AudioPlayer.PlaySoundEffect(parse[1]);/*
                        switch (parse[1].ToLower()) {
                            case "attack":
                                Music.Music.AudioPlayer.PlaySoundEffect("sword.wav");
                                break;
                            case "critical":
                                Music.Music.AudioPlayer.PlaySoundEffect("critical.wav");
                                break;
                            case "miss":
                                Music.Music.AudioPlayer.PlaySoundEffect("miss.wav");
                                break;
                            case "key":
                                Music.Music.AudioPlayer.PlaySoundEffect("key.wav");
                                break;
                            case "magic":
                                Music.Music.AudioPlayer.PlaySoundEffect("magic" + parse[2] + ".wav");
                                break;
                            case "warp":
                                Music.Music.AudioPlayer.PlaySoundEffect("warp.wav");
                                break;
                            case "pain":
                                Music.Music.AudioPlayer.PlaySoundEffect("pain.wav");
                                break;
                            case "soundattribute":
                                Music.Music.AudioPlayer.PlaySoundEffect(parse[2]);
                                break;
                        }*/
                    }
                    break;
                case "music": {
                        //Music.Music.AudioPlayer.PlayMusic(parse[1]);
                        ((Client.Logic.Music.Bass.BassAudioPlayer)Logic.Music.Music.AudioPlayer).FadeToNext(parse[1], 1000);
                    }
                    break;
                case "fademusic": {
                        Music.Music.AudioPlayer.FadeOut(parse[1].ToInt());
                    }
                    break;
                case "plainmsg": {
                        string msg = parse[1];
                        Enums.PlainMsgType type = (Enums.PlainMsgType)parse[2].ToInt();

                        SdlDotNet.Widgets.MessageBox.Show(msg, "----");

                        SdlDotNet.Widgets.Window winLoading = WindowSwitcher.FindWindow("winLoading");
                        if (winLoading != null) {
                            winLoading.Close();
                        }
                        switch (type) {
                            case Enums.PlainMsgType.MainMenu: {
                                    WindowSwitcher.ShowMainMenu();
                                }
                                break;
                            case Enums.PlainMsgType.Chars: {
                                    Messenger.SendCharListRequest();
                                }
                                break;
                            case Enums.PlainMsgType.NewChar: {
                                    Messenger.SendCharListRequest();
                                }
                                break;
                        }
                    }
                    break;
                case "onlinelist": {
                        

                        Menus.mnuOnlineList onlineList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuOnlineList") as Menus.mnuOnlineList;
                        if (onlineList != null) {
                            onlineList.AddOnlinePlayers(parse);
                            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                        }

                        
                        
                    }
                    break;
                case "gametime": {
                        Globals.GameTime = (Enums.Time)parse[1].ToInt();
                        if (Maps.MapHelper.ActiveMap != null) {
                            Maps.MapHelper.ActiveMap.DoOverlayChecks();
                        }
                    }
                    break;
                case "adventurelog": {
                        Menus.mnuAdventureLog adventureLog = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuAdventureLog") as Menus.mnuAdventureLog;
                        if (adventureLog != null) {
                            adventureLog.LoadAdventureLogFromPacket(parse);
                        }
                    }
                    break;
                case "focusonpoint": {
                        int x = parse[1].ToInt();
                        int y = parse[2].ToInt();

                        if (x == -1 && y == -1) {
                            Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.FocusObject = null;
                        } else {
                            Logic.Graphics.Renderers.Screen.CameraFocusObject focusObject = new Logic.Graphics.Renderers.Screen.CameraFocusObject();
                            focusObject.FocusedX = x;
                            focusObject.FocusedY = y;
                            focusObject.FocusedXOffset = 0;
                            focusObject.FocusedYOffset = 0;
                            focusObject.FocusedDirection = Enums.Direction.Up;
                            Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.FocusOnFocusObject(focusObject);
                        }
                    }
                    break;
                case "serverstatus": {
                        Globals.ServerStatus = parse[1];
                    }
                    break;
                case "ping": {
                        PingStopwatch.Stop();
                        Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.RecentPing = (int)PingStopwatch.ElapsedMilliseconds;
                    }
                    break;
                #endregion
                #region Editors
                #region Map Editor
                case "editmap": {
                        Maps.MapHelper.ActiveMap.Tile = Maps.MapHelper.ActiveMap.OriginalTiles;
                        Windows.Editors.EditorManager.OpenMapEditor();
                    }
                    break;
                case "mapreport": {
                        int n = 2;
                        int maxMaps = parse[1].ToInt();
                        String[] MapName;
                        MapName = new String[maxMaps];
                        for (int i = 1; i <= maxMaps; i++) {
                            MapName[i - 1] = parse[n];

                            n += 1;
                        }

                        if (Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.IsModuleAvailable(Enums.ExpKitModules.MapReport)) {
                            ExpKit.Modules.kitMapReport kitMapReport = (ExpKit.Modules.kitMapReport)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindAvailableKitModule(Enums.ExpKitModules.MapReport);
                            kitMapReport.LoadAllMapNames(MapName);
                        }
                    }
                    break;
                case "mapnameupdated": {
                        if (Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.IsModuleAvailable(Enums.ExpKitModules.MapReport)) {
                            ExpKit.Modules.kitMapReport kitMapReport = (ExpKit.Modules.kitMapReport)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindAvailableKitModule(Enums.ExpKitModules.MapReport);
                            kitMapReport.UpdateMapName(parse[1].ToInt(), parse[2]);
                        }
                    }
                    break;
                case "maplatestproperties": {
                        int n = 3;
                        Maps.MapHelper.ActiveMap.Instanced = parse[1].ToBool();
                        int npcs = parse[2].ToInt();
                        for (int i = 0; i < npcs; i++) {
                            Maps.MapNpcSettings npc = new Maps.MapNpcSettings();
                            npc.NpcNum = parse[n].ToInt();
                            npc.SpawnX = parse[n + 1].ToInt();
                            npc.SpawnY = parse[n + 2].ToInt();
                            npc.MinLevel = parse[n + 3].ToInt();
                            npc.MaxLevel = parse[n + 4].ToInt();
                            npc.AppearanceRate = parse[n + 5].ToInt();
                            npc.StartStatus = (Enums.StatusAilment)parse[n + 6].ToInt();
                            npc.StartStatusCounter = parse[n + 7].ToInt();
                            npc.StartStatusChance = parse[n + 8].ToInt();

                            Maps.MapHelper.ActiveMap.Npc[i] = npc;

                            n += 9;
                        }
                    }
                    break;
                case "mapeditortileplaced": {
                        WindowSwitcher.GameWindow.SetMapLayer(parse[1].ToInt(), parse[2].ToInt(), (Enums.LayerType)parse[3].ToInt(), parse[4].ToInt(), parse[5].ToInt());
                    }
                    break;
                case "mapeditorattribplaced": {
                        WindowSwitcher.GameWindow.SetMapAttribute(parse[1].ToInt(), parse[2].ToInt(),
                            (Enums.TileType)parse[3].ToInt(), parse[4].ToInt(), parse[5].ToInt(), parse[6].ToInt(),
                            parse[7], parse[8], parse[9], parse[10].ToInt());
                    }
                    break;
                case "mapeditorlayerfill": {
                        WindowSwitcher.GameWindow.FillLayer((Enums.LayerType)parse[1].ToInt(), parse[2].ToInt(), parse[3].ToInt());
                    }
                    break;
                case "mapeditorattribfill": {
                        WindowSwitcher.GameWindow.FillAttributes((Enums.TileType)parse[1].ToInt(), parse[2].ToInt(), parse[3].ToInt(), parse[4].ToInt(),
                            parse[5], parse[6], parse[7], parse[8].ToInt());
                    }
                    break;
                case "scriptedattributename": {
                        WindowSwitcher.GameWindow.lbl1.Text = "Script " + parse[1];
                    }
                    break;
                case "scriptedsignattributename": {
                        WindowSwitcher.GameWindow.lbl1.Text = "Script " + parse[1];
                    }
                    break;
                case "mobilityname": {
                        WindowSwitcher.GameWindow.lbl1.Text = "Mobility Flag: " + parse[1];
                    }
                    break;
                #endregion
                #region Random Dungeon Editor
                case "rdungeoneditor": {
                        Windows.Editors.EditorManager.RDungeonPanel.Show();
                    }
                    break;
                case "rdungeonadded": {
                        MaxInfo.MaxRDungeons++;
                        RDungeons.RDungeonHelper.RDungeons.AddRDungeon(parse[1].ToInt(), new RDungeons.RDungeon());
                        RDungeons.RDungeonHelper.RDungeons[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winRDungeonPanel") != null) {
                            ((Windows.Editors.winRDungeonPanel)WindowSwitcher.FindWindow("winRDungeonPanel")).RefreshRDungeonList();
                        }
                    }
                    break;
                case "rdungeonupdate": {

                        RDungeons.RDungeonHelper.RDungeons[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winRDungeonPanel") != null) {
                            ((Windows.Editors.winRDungeonPanel)WindowSwitcher.FindWindow("winRDungeonPanel")).RefreshRDungeonList();
                        }
                    }
                    break;
                case "editrdungeon": {
                        if (WindowSwitcher.FindWindow("winRDungeonPanel") == null) {
                            Windows.Editors.EditorManager.RDungeonPanel.Show();
                        }
                        ((Windows.Editors.winRDungeonPanel)WindowSwitcher.FindWindow("winRDungeonPanel")).LoadRDungeon(parse);
                    }
                    break;
                #endregion
                #region Dungeon Editor
                case "dungeoneditor": {
                        Windows.Editors.EditorManager.DungeonPanel.Show();
                    }
                    break;
                case "dungeonadded": {
                        MaxInfo.MaxDungeons++;
                        Dungeons.DungeonHelper.Dungeons.AddDungeon(parse[1].ToInt(), new Dungeons.Dungeon());
                        Dungeons.DungeonHelper.Dungeons[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winDungeonPanel") != null) {
                            ((Windows.Editors.winDungeonPanel)WindowSwitcher.FindWindow("winDungeonPanel")).RefreshDungeonList();
                        }
                    }
                    break;
                case "updatedungeon": {
                        Dungeons.DungeonHelper.Dungeons[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winDungeonPanel") != null) {
                            ((Windows.Editors.winDungeonPanel)WindowSwitcher.FindWindow("winDungeonPanel")).RefreshDungeonList();
                        }
                    }
                    break;
                case "editdungeon": {
                        if (WindowSwitcher.FindWindow("winDungeonPanel") == null) {
                            Windows.Editors.EditorManager.DungeonPanel.Show();
                        }
                        ((Windows.Editors.winDungeonPanel)WindowSwitcher.FindWindow("winDungeonPanel")).LoadDungeon(parse);

                    }
                    break;
                #endregion
                #region Mission Editor
                case "missioneditor": {
                        Windows.Editors.EditorManager.MissionPanel.Show();
                    }
                    break;
                case "updatemission": {
                        Dungeons.DungeonHelper.Dungeons[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winMissionPanel") != null) {
                            ((Windows.Editors.winMissionPanel)WindowSwitcher.FindWindow("winMissionPanel")).RefreshMissionList();
                        }
                    }
                    break;
                case "editmission": {
                        ((Windows.Editors.winMissionPanel)WindowSwitcher.FindWindow("winMissionPanel")).LoadMission(parse);
                    }
                    break;
                #endregion
                #region Item Editor
                case "itemeditor": {
                        Windows.Editors.EditorManager.ItemPanel.Show();
                    }
                    break;
                case "edititem": {
                    if (WindowSwitcher.FindWindow("winItemPanel") == null) {
                        Windows.Editors.EditorManager.ItemPanel.Show();
                    }
                    ((Windows.Editors.winItemPanel)WindowSwitcher.FindWindow("winItemPanel")).LoadItem(parse[1].ToInt());
                    }
                    break;
                #endregion
                #region Move Editor
                case "moveeditor": {
                        Windows.Editors.EditorManager.MovePanel.Show();
                    }
                    break;
                case "editmove": {
                    if (WindowSwitcher.FindWindow("winMovePanel") == null) {
                        Windows.Editors.EditorManager.MovePanel.Show();
                    }
                        ((Windows.Editors.winMovePanel)WindowSwitcher.FindWindow("winMovePanel")).LoadMove(parse);

                    }
                    break;
                case "updatespell": {
                        Moves.MoveHelper.Moves[parse[1].ToInt()].Name = parse[2];
                        Moves.MoveHelper.Moves[parse[1].ToInt()].RangeType = (Enums.MoveRange)parse[3].ToInt();
                        Moves.MoveHelper.Moves[parse[1].ToInt()].Range = parse[4].ToInt();
                        Moves.MoveHelper.Moves[parse[1].ToInt()].TargetType = (Enums.MoveTarget)parse[5].ToInt();
                        Moves.MoveHelper.Moves[parse[1].ToInt()].HitTime = parse[6].ToInt();
                        Moves.MoveHelper.Moves[parse[1].ToInt()].HitFreeze = parse[7].ToBool();
                        if (WindowSwitcher.FindWindow("winMovePanel") != null) {
                            ((Windows.Editors.winMovePanel)WindowSwitcher.FindWindow("winMovePanel")).RefreshMoveList();
                        }
                    }
                    break;
                #endregion

                #region Shop Editor
                case "shopeditor": {
                        Windows.Editors.EditorManager.ShopPanel.Show();
                    }
                    break;
                case "editshop": {
                        ((Windows.Editors.winShopPanel)WindowSwitcher.FindWindow("winShopPanel")).LoadShop(parse);


                    }
                    break;
                case "updateshop": {
                        Shops.ShopHelper.Shops[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winShopPanel") != null) {
                            ((Windows.Editors.winShopPanel)WindowSwitcher.FindWindow("winShopPanel")).RefreshShopList();
                        }
                    }
                    break;
                #endregion

                #region Arrow Editor
                case "arroweditor": {
                        Windows.Editors.EditorManager.ArrowPanel.Show();
                    }
                    break;
                case "editarrow": {
                        // Obtain the arrow index
                        int arrowNum = parse[1].ToInt();

                        // Update the in-memory arrow data to use most recent data from the server
                        Arrows.ArrowHelper.Arrows[arrowNum].Name = parse[2];
                        Arrows.ArrowHelper.Arrows[arrowNum].Pic = parse[3].ToInt();
                        Arrows.ArrowHelper.Arrows[arrowNum].Amount = parse[4].ToInt();
                        Arrows.ArrowHelper.Arrows[arrowNum].Range = parse[5].ToInt();

                        // Display this data on the arrow editor window
                        Windows.Editors.winArrowPanel arrowWindow = WindowSwitcher.FindWindow("winArrowPanel") as Windows.Editors.winArrowPanel;
                        if (arrowWindow != null) {
                            arrowWindow.DisplayArrowData();
                        }
                    }
                    break;
                case "updatearrow": {
                        // Obtain the arrow index
                        int arrowNum = parse[1].ToInt();

                        // Update the in-memory arrow data to use most recent data from the server
                        Arrows.ArrowHelper.Arrows[arrowNum].Name = parse[2];
                        Arrows.ArrowHelper.Arrows[arrowNum].Pic = parse[3].ToInt();
                        Arrows.ArrowHelper.Arrows[arrowNum].Range = parse[4].ToInt();
                        Arrows.ArrowHelper.Arrows[arrowNum].Amount = parse[5].ToInt();

                        // Display this data on the arrow editor window, if it's open
                        Windows.Editors.winArrowPanel arrowWindow = WindowSwitcher.FindWindow("winArrowPanel") as Windows.Editors.winArrowPanel;
                        if (arrowWindow != null) {
                            arrowWindow.RefreshArrowList();
                        }
                    }
                    break;
                #endregion
                #region Emoticons Editor
                case "emoticoneditor": {
                        Windows.Editors.EditorManager.EmotionPanel.Show();
                    }
                    break;
                case "editemoticon": {
                        // Optain the emoticon index
                        int emoteNum = parse[1].ToInt();

                        // Update the in-memory emoticon data to use most recent data from the server
                        Emotions.EmotionHelper.Emotions[emoteNum].Command = parse[2];
                        Emotions.EmotionHelper.Emotions[emoteNum].Pic = parse[3].ToInt();

                        // Display this data on the emoticon editor window
                        Windows.Editors.winEmotionPanel emoteWindow = WindowSwitcher.FindWindow("winEmotionPanel") as Windows.Editors.winEmotionPanel;
                        if (emoteWindow != null) {
                            emoteWindow.DisplayEmotionData();
                        }
                    }
                    break;
                case "updateemoticon": {
                        // Obtain the emote index
                        int emoteNum = parse[1].ToInt();

                        // Update the in-memory emote data to use most recent data from the server
                        Emotions.EmotionHelper.Emotions[emoteNum].Command = parse[2];
                        Emotions.EmotionHelper.Emotions[emoteNum].Pic = parse[3].ToInt();

                        // Display this data on the emote editor window, if it's open
                        Windows.Editors.winEmotionPanel emotionWindow = WindowSwitcher.FindWindow("winEmotionPanel") as Windows.Editors.winEmotionPanel;
                        if (emotionWindow != null) {
                            emotionWindow.RefreshEmoteList();
                        }
                    }
                    break;
                #endregion
                #region NPC Editor
                case "npceditor": {
                        Windows.Editors.EditorManager.NPCPanel.Show();
                    }
                    break;
                case "editnpc": {
                    if (WindowSwitcher.FindWindow("winNPCPanel") == null) {
                        Windows.Editors.EditorManager.NPCPanel.Show();
                    }
                    Windows.Editors.EditorManager.NPCPanel.LoadNPC(parse);
                    }
                    break;
                case "npcadded":
                    {
                        Npc.NpcHelper.Npcs.AddNpc(parse[1]);
                        if (WindowSwitcher.FindWindow("winNPCPanel") != null)
                        {
                            ((Windows.Editors.winNPCPanel)WindowSwitcher.FindWindow("winNPCPanel")).RefreshNPCList();
                        }
                    }
                    break;
                case "updatenpc":
                    {
                        int npcNum = parse[1].ToInt();
                        Npc.NpcHelper.Npcs[npcNum].Name = parse[2];
                        //Npc.NpcHelper.Npcs[npcNum].Sprite = parse[3].ToInt();
                    }
                    break;
                #endregion
                #region House Editor
                case "edithouse": {
                        Windows.Editors.EditorManager.OpenHouseEditor();
                    }
                    break;
                #endregion
                #region Story Edtior
                case "storyeditor": {
                        Windows.Editors.EditorManager.StoryPanel.Show();
                    }
                    break;
                case "editstory": {
                        Windows.Editors.EditorManager.StoryPanel.LoadStory(parse);
                    }
                    break;
                case "updatestoryname": {
                        Stories.StoryHelper.Stories[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winStoryPanel") != null) {
                            ((Windows.Editors.winStoryPanel)WindowSwitcher.FindWindow("winStoryPanel")).RefreshStoryList();
                        }
                    }
                    break;
                #endregion
                #region Evolution Editor
                case "evoeditor": {
                        Windows.Editors.EditorManager.EvolutionPanel.Show();
                    }
                    break;
                case "editevo": {
                        ((Windows.Editors.winEvolutionPanel)WindowSwitcher.FindWindow("winEvolutionPanel")).LoadEvo(parse);
                    }
                    break;
                case "updateevo": {


                        Evolutions.EvolutionHelper.Evolutions[parse[1].ToInt()].Name = parse[2];
                        if (WindowSwitcher.FindWindow("winEvolutionPanel") != null) {
                            ((Windows.Editors.winEvolutionPanel)WindowSwitcher.FindWindow("winEvolutionPanel")).RefreshEvoList();
                        }
                    }
                    break;
                #endregion

                #region Scripts
                case "scripteditstart": {
                        // Because Windows Forms and SDL don't like playing together.
                        System.Threading.Thread t = new System.Threading.Thread(new System.Threading.ThreadStart(RunScriptEditor));
                        t.SetApartmentState(System.Threading.ApartmentState.STA); //Set the thread to STA
                        t.Start();
                        while (Windows.Editors.EditorManager.ScriptEditor == null) {
                        }
                    }
                    break;
                case "scriptfilelist": {
                        int filesCount = parse[1].ToInt();
                        List<string> files = new List<string>(filesCount);
                        int n = 2;
                        for (int i = 0; i < filesCount; i++) {
                            files.Add(parse[n]);
                            n += 1;
                        }
                        Windows.Editors.EditorManager.ScriptEditor.SetFileList(files);
                    }
                    break;
                case "scriptfiledata":
                case "scripteditdata": {
                        Windows.Editors.ScriptEditor.ScriptFileTab tab = new Windows.Editors.ScriptEditor.ScriptFileTab();
                        tab.SetDocumentFile(parse[1]);
                        tab.SetDocumentText(parse[2]);
                        Windows.Editors.EditorManager.ScriptEditor.AddDocumentTab(tab);
                    }
                    break;
                case "scriptsyntax": {
                        if (System.IO.Directory.Exists(IO.Paths.StartupPath + "Script/") == false) {
                            System.IO.Directory.CreateDirectory(IO.Paths.StartupPath + "Script/");
                        }
                        System.IO.File.WriteAllText(IO.Paths.StartupPath + "Script/CSharp.syn", parse[1]);
                    }
                    break;
                case "scriptclasses": {
                        int filesCount = parse[1].ToInt();
                        List<string> files = new List<string>(filesCount);
                        int n = 2;
                        for (int i = 0; i < filesCount; i++) {
                            files.Add(parse[n]);
                            n += 1;
                        }
                        Windows.Editors.EditorManager.ScriptEditor.SetClassesList(files);
                    }
                    break;
                case "scriptmethods": {
                        int filesCount = parse[1].ToInt();
                        List<string> files = new List<string>(filesCount);
                        int n = 2;
                        for (int i = 0; i < filesCount; i++) {
                            files.Add(parse[n]);
                            n += 1;
                        }
                        Windows.Editors.EditorManager.ScriptEditor.SetMethodsList(files);
                    }
                    break;
                case "scriptparam": {
                        Windows.Editors.EditorManager.ScriptEditor.SetScriptParameterInfo(parse[1]);
                    }
                    break;
                case "scriptediterrors": {
                        int errorsCount = parse[1].ToInt();
                        List<string> errors = new List<string>(errorsCount);
                        int n = 2;
                        for (int i = 0; i < errorsCount; i++) {
                            string errorString = parse[n] + ", line: " + parse[n + 1] + ", error: " + parse[n + 3];
                            errors.Add(errorString);
                            n += 4;
                        }
                        Windows.Editors.EditorManager.ScriptEditor.SetErrorsList(errors);
                    }
                    break;
                #endregion
                #endregion
                #region ExpKit
                case "kitmodules": {
                        int count = parse[1].ToInt();
                        int n = 2;
                        WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.DisableAllModules();
                        for (int i = 0; i < count; i++) {
                            WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule((Enums.ExpKitModules)parse[n].ToInt()).Enabled = true;
                            n += 1;
                        }
                        WindowSwitcher.ExpKit.KitContainer.SetActiveModule(0);
                    }
                    break;
                case "setactivekitmodule": {
                        Enums.ExpKitModules module = (Enums.ExpKitModules)parse[1].ToInt();
                        WindowSwitcher.ExpKit.KitContainer.SetActiveModule(module);
                    }
                    break;
                #endregion
                #region Housing
                case "visithouse": {
                        Menus.MenuSwitcher.ShowHouseSelectionMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "changehouseweather": {
                        Menus.MenuSwitcher.ShowHouseWeatherMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "changehousedarkness": {
                        Menus.MenuSwitcher.ShowHouseDarknessMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "changehousebounds": {
                        Menus.MenuSwitcher.ShowHouseBoundsMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "addhouseshop": {
                        Menus.MenuSwitcher.ShowHouseShopMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "addhousesound": {
                        Menus.MenuSwitcher.ShowHouseSoundMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "addhousenotice": {
                        Menus.MenuSwitcher.ShowHouseNoticeMenu(parse[1].ToInt(), parse[2].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "addhousesign": {
                    Menus.MenuSwitcher.ShowHouseSignMenu(parse[1].ToInt());
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                #endregion
                #region Missions
                case "missionboard": {
                        Menus.MenuSwitcher.OpenMissionBoard(parse);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case "missionremoved": {
                        Menus.Core.IMenu mnuMissionBoard = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMissionBoard");
                        if (mnuMissionBoard != null) {
                            ((Menus.mnuMissionBoard)mnuMissionBoard).RemoveJob(parse[1].ToInt());
                            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                        }
                        
                    }
                    break;
                case "missionadded": {
                        Menus.Core.IMenu mnuMissionBoard = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMissionBoard");
                        if (mnuMissionBoard != null) {
                            ((Menus.mnuMissionBoard)mnuMissionBoard).AddJob(parse);
                            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                        }
                    }
                    break;
                case "joblist": {
                        int n = 2;
                        int jobCount = parse[1].ToInt();
                        // Clear the job list first
                        PlayerManager.MyPlayer.JobList.Jobs.Clear();
                        for (int i = 0; i < jobCount; i++) {
                            Missions.Job job = new Missions.Job();
                            job.Title = parse[n];
                            job.Summary = parse[n+1];
                            job.GoalName = parse[n+2];
                            job.ClientSpecies = parse[n+3].ToInt();
                            job.ClientForm = parse[n + 4].ToInt();
                            job.MissionType = (Enums.MissionType)parse[n + 5].ToInt();
                            job.Data1 = parse[n + 6].ToInt();
                            job.Data2 = parse[n + 7].ToInt();
                            job.Difficulty = (Enums.JobDifficulty)parse[n + 8].ToInt();
                            job.RewardNum = parse[n + 9].ToInt();
                            job.RewardAmount = parse[n + 10].ToInt();
                            //job.Mugshot = parse[n + 11].ToInt();
                            
                            job.Accepted = (Enums.JobStatus)parse[n + 12].ToInt();
                            job.CanSend = parse[n + 13].ToBool();

                            PlayerManager.MyPlayer.JobList.Jobs.Add(job);
                            n += 14;
                        }

                        Menus.Core.IMenu mnuJobList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobList");
                        if (mnuJobList != null) {
                            ((Menus.mnuJobList)mnuJobList).DisplayItems();
                        }

                    }
                    break;
                case "newjob": {

                    int n = 1;
                        Missions.Job job = new Missions.Job();
                        job.Title = parse[n];
                        job.Summary = parse[n + 1];
                        job.GoalName = parse[n + 2];
                        job.ClientSpecies = parse[n + 3].ToInt();
                        job.ClientForm = parse[n + 4].ToInt();
                        job.MissionType = (Enums.MissionType)parse[n + 5].ToInt();
                        job.Data1 = parse[n + 6].ToInt();
                        job.Data2 = parse[n + 7].ToInt();
                        job.Difficulty = (Enums.JobDifficulty)parse[n + 8].ToInt();
                        job.RewardNum = parse[n + 9].ToInt();
                        job.RewardAmount = parse[n + 10].ToInt();
                        //job.Mugshot = parse[n + 11].ToInt();

                        job.Accepted = (Enums.JobStatus)parse[n + 12].ToInt();
                        job.CanSend = parse[n + 13].ToBool();

                        PlayerManager.MyPlayer.JobList.Jobs.Add(job);

                        Menus.Core.IMenu mnuJobList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobList");
                        if (mnuJobList != null) {
                            ((Menus.mnuJobList)mnuJobList).DisplayItems();
                        }
                    }
                    break;
                case "acceptjob": {
                        int slot = parse[1].ToInt();
                        PlayerManager.MyPlayer.JobList.Jobs[slot].Accepted = (Enums.JobStatus)parse[2].ToInt();

                        for (int i = PlayerManager.MyPlayer.MapGoals.Count - 1; i >= 0; i--) {
                            if (PlayerManager.MyPlayer.JobList.Jobs[slot].Accepted != Enums.JobStatus.Taken && PlayerManager.MyPlayer.MapGoals[i].JobListSlot == slot) {
                                PlayerManager.MyPlayer.MapGoals.RemoveAt(i);
                            }
                        }

                        Menus.Core.IMenu mnuJobList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobList");
                        if (mnuJobList != null) {
                            ((Menus.mnuJobList)mnuJobList).DisplayItems();
                        }
                    }
                    break;
                case "jobsends": {
                        int slot = parse[1].ToInt();
                        PlayerManager.MyPlayer.JobList.Jobs[slot].CanSend = parse[2].ToBool();


                        Menus.Core.IMenu mnuJobList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobList");
                        if (mnuJobList != null) {
                            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuJobList");
                            ((Menus.mnuJobList)mnuJobList).DisplayItems();
                        }
                    }
                    break;
                case "deletejob": {

                        PlayerManager.MyPlayer.JobList.Jobs.RemoveAt(parse[1].ToInt());

                        Menus.Core.IMenu mnuJobList = WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuJobList");
                        if (mnuJobList != null) {
                            ((Menus.mnuJobList)mnuJobList).DisplayItems();
                        }
                    }
                    break;
                case "missiongoal": {
                        int count = parse[1].ToInt();
                        int n = 2;
                        PlayerManager.MyPlayer.MapGoals.Clear();
                        for (int i = 0; i < count; i++) {
                            Missions.MissionGoal goal = new Missions.MissionGoal();

                            goal.JobListSlot = parse[n].ToInt();
                            goal.GoalX = parse[n + 1].ToInt();
                            goal.GoalY = parse[n + 2].ToInt();

                            PlayerManager.MyPlayer.MapGoals.Add(goal);
                            n += 3;
                        }
                    }
                    break;
                //case "openjoblist": {
                //        Menus.MenuSwitcher.ShowJobListMenu();
                //        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                //    }
                //    break;
                //case "missiondescription": {
                //        Menus.mnuJobDescription jobDescription = new Menus.mnuJobDescription("mnuJobDescription",
                //            parse[1],
                //            parse[2],
                //            (Enums.JobDifficulty)parse[3].ToInt(),
                //            parse[4],
                //            parse[5].ToInt(),
                //            parse[6],
                //            parse[7]
                //            );
                //        Menus.MenuSwitcher.ShowMenu(jobDescription);
                //        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                //    }
                //    break;
                //case "canceljob": {
                //        PlayerManager.MyPlayer.ActiveMission = null;
                //    }
                //    break;
                //case "missioncode": {
                //        Menus.mnuWonderMailCodeViewer codeViewer = new Menus.mnuWonderMailCodeViewer("mnuWonderMailCodeViewer",
                //            parse[1],
                //            parse[2]
                //            );
                //        Menus.MenuSwitcher.ShowMenu(codeViewer);
                //    }
                //    break;
                case "missionexp": {
                        PlayerManager.MyPlayer.MissionExp = parse[1].ToInt();
                        PlayerManager.MyPlayer.ExplorerRank = (Enums.ExplorerRank)parse[2].ToInt();
                    }
                    break;
                #endregion
                #region Custom Menus
                case "custommenu": {
                        SdlDotNet.Widgets.WidgetCollection widgets = new SdlDotNet.Widgets.WidgetCollection();
                        CustomMenus.CustomMenuOptions options = new CustomMenus.CustomMenuOptions();
                        options.Name = parse[1];
                        options.Size = new Size(parse[3].ToInt(), parse[4].ToInt());
                        int pictureBoxCount = parse[5].ToInt();
                        int labelCount = parse[6].ToInt();
                        int textboxCount = parse[7].ToInt();
                        int n = 8;
                        for (int i = 0; i < pictureBoxCount; i++) {
                            SdlDotNet.Widgets.PictureBox picture = new SdlDotNet.Widgets.PictureBox("pictureBox" + i);
                            picture.ImageLocation = parse[n];
                            picture.Location = new Point(parse[n + 1].ToInt(), parse[n + 2].ToInt());
                            widgets.AddWidget(picture.Name, picture);
                            n += 3;
                        }
                        for (int i = 0; i < labelCount; i++) {
                            SdlDotNet.Widgets.Label label = new SdlDotNet.Widgets.Label("label" + i);
                            label.AutoSize = false;
                            label.Location = new Point(parse[n].ToInt(), parse[n + 1].ToInt());
                            label.Size = new Size(parse[n + 2].ToInt(), parse[n + 3].ToInt());
                            label.Font = Logic.Graphics.FontManager.LoadFont(parse[n + 5], parse[n + 6].ToInt());
                            label.ForeColor = Color.FromArgb(parse[n + 7].ToInt());
                            label.Text = parse[n + 4];
                            widgets.AddWidget(label.Name, label);
                            n += 8;
                        }
                        for (int i = 0; i < textboxCount; i++) {
                            SdlDotNet.Widgets.TextBox textBox = new SdlDotNet.Widgets.TextBox("textBox" + i);
                            textBox.Location = new Point(parse[n].ToInt(), parse[n + 1].ToInt());
                            textBox.Size = new Size(parse[n + 2].ToInt(), 14);
                            textBox.Text = parse[n + 3];

                            widgets.AddWidget(textBox.Name, textBox);
                            n += 4;
                        }

                        CustomMenus.CustomMenu menu = new CustomMenus.CustomMenu(options, widgets);
                        menu.Location = Logic.Graphics.DrawingSupport.GetCenter(WindowSwitcher.GameWindow.MenuManager.Size, menu.Size);
                        Menus.MenuSwitcher.ShowMenu(menu);
                    }
                    break;
                #endregion
                #region Story
                case "storycheck": {
                        Messenger.SendPacket(TcpPacket.CreatePacket("needstory", "yes", parse[1]));
                    }
                    break;
                case "updatestory": {
                        Client.Logic.Stories.Story story = new Client.Logic.Stories.Story();
                        story.LocalStory = false;

                        story.Name = parse[2];
                        story.Revision = parse[3].ToInt();
                        story.StoryStart = parse[4].ToInt();
                        int segments = parse[5].ToInt();

                        int n = 6;
                        for (int i = 0; i < segments; i++) {
                            Stories.Segment segment = new Stories.Segment();
                            int paramCount = parse[n].ToInt();
                            segment.Action = (Enums.StoryAction)(parse[n + 1].ToInt());
                            n += 2;
                            for (int a = 0; a < paramCount; a++) {
                                segment.AddParameter(parse[n], parse[n + 1]);
                                n += 2;
                            }
                            
                            story.AppendSegment(segment.ToSpecific());
                        }

                        Stories.StoryHelper.CachedStory = story;
                    }
                    break;
                case "runstory": {
                        Client.Logic.Stories.Story story = new Client.Logic.Stories.Story();
                        story.LocalStory = false;

                        story.Name = parse[2];
                        story.Revision = parse[3].ToInt();
                        story.StoryStart = parse[4].ToInt();
                        int segments = parse[5].ToInt();

                        int n = 6;
                        for (int i = 0; i < segments; i++) {
                            Stories.Segment segment = new Stories.Segment();
                            int paramCount = parse[n].ToInt();
                            segment.Action = (Enums.StoryAction)(parse[n + 1].ToInt());
                            n += 2;
                            for (int a = 0; a < paramCount; a++) {
                                segment.AddParameter(parse[n], parse[n + 1]);
                                n += 2;
                            }
                            story.AppendSegment(segment.ToSpecific());
                        }

                        Stories.StoryHelper.CachedStory = story;

                        Messenger.SendStoryLoadingComplete();
                        Stories.StoryProcessor.PlayStory(Stories.StoryHelper.CachedStory, 0);
                    }
                    break;
                case "startstory": {
                        Messenger.SendStoryLoadingComplete();
                        Stories.StoryProcessor.PlayStory(Stories.StoryHelper.CachedStory, parse[2].ToInt());
                    }
                    break;
                case "askquestion": {
                        Stories.Story story = new Stories.Story();
                        story.Name = "Ask Question";
                        story.LocalStory = true;

                        string[] choices = new string[parse[4].ToInt()];
                        int n = 5;
                        for (int i = 0; i < choices.Length; i++) {
                            choices[i] = parse[n];

                            n += 1;
                        }
                        story.AppendSegment(new Stories.Segments.AskQuestionSegment(parse[1], parse[2].ToInt(), -1, -1, choices));
                        //story.AppendSegment(new SaySegment("Test", 5, 0, 0));
                        //story.AppendSegment(new AskQuestionSegment("Hello?", 5, 1, 1));
                        //story.AppendSegment(new PauseSegment(5000));
                        //story.AppendSegment(new SaySegment("Test - Segment 3!", 26, 0 ,0));
                        //story.AppendSegment(new MapVisibilitySegment(false));
                        //story.AppendSegment(new PauseSegment(5000));
                        //story.AppendSegment(new MapVisibilitySegment(true));

                        Stories.StoryProcessor.PlayStory(story, 0);
                    }
                    break;
                case "loadingstory": {
                        Stories.StoryProcessor.loadingStory = true;
                    }
                    break;
                case "breakstory": {
                    Stories.StoryProcessor.ForceEndStory();
                    }
                    break;
                #endregion
                #region Trading
                case "opentrademenu": {
                        string tradePartner = parse[1];
                        Menus.MenuSwitcher.ShowMenu(new Menus.mnuTrade("mnuTrade", tradePartner));
                    }
                    break;
                case "tradesetitemupdate": {
                        Menus.mnuTrade menu = (Menus.mnuTrade)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuTrade");
                        if (menu != null) {
                            if (parse[3].ToBool()) {
                                if (!string.IsNullOrEmpty(parse[1])) {
                                    menu.UpdateSetItem(parse[1].ToInt(), parse[2].ToInt());
                                } else {
                                    menu.UpdateSetItem(-1, 0);
                                }
                            } else {
                                if (!string.IsNullOrEmpty(parse[1])) {
                                    menu.UpdatePartnersSetItem(parse[1].ToInt(), parse[2].ToInt());
                                } else {
                                    menu.UpdatePartnersSetItem(-1, 0);
                                }
                            }
                        }
                    }
                    break;
                case "tradecomplete": {
                        Menus.mnuTrade menu = (Menus.mnuTrade)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuTrade");
                        if (menu != null) {
                            menu.ResetTradeData();
                        }
                    }
                    break;
                case "unconfirmtrade": {
                        Menus.mnuTrade menu = (Menus.mnuTrade)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuTrade");
                        if (menu != null) {
                            menu.UnconfirmTrade();
                        }
                    }
                    break;
                case "endtrade": {
                        Menus.mnuTrade menu = (Menus.mnuTrade)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuTrade");
                        if (menu != null) {
                            Menus.MenuSwitcher.CloseAllMenus();
                        }
                    }
                    break;
                #endregion
                #region Pets
                case "activepets": {
                        string id = parse[1];
                        int n = 2;
                        IPlayer player = PlayerManager.Players[id];
                        if (player != null) {
                            for (int i = 1; i < MaxInfo.MAX_ACTIVETEAM; i++) {
                                if (parse[n] != "-1") {
                                    player.Pets[i] = new PlayerPet(i, player);
                                    player.Pets[i].X = player.X;
                                    player.Pets[i].Y = player.Y - 1;
                                    player.Pets[i].Sprite = parse[n].ToInt();
                                    //TODO: add forme/shiny/gender data
                                    n += 1;
                                } else {
                                    player.Pets[i] = null;
                                    n += 1;
                                }
                            }
                        }
                    }
                    break;
                #endregion
                #region Tournament
                case "tournamentlisting": {
                        Tournaments.TournamentListing[] listings = new Tournaments.TournamentListing[parse[1].ToInt()];
                        int n = 2;
                        for (int i = 0; i < listings.Length; i++) {
                            listings[i] = new Tournaments.TournamentListing(
                                parse[n],
                                parse[n + 1],
                                parse[n + 2]);
                            n += 3;
                        }
                        Menus.MenuSwitcher.ShowTournamentListingMenu(listings, Enums.TournamentListingMode.Join);
                    }
                    break;
                case "tournamentspectatelisting": {
                        Tournaments.TournamentListing[] listings = new Tournaments.TournamentListing[parse[1].ToInt()];
                        int n = 2;
                        for (int i = 0; i < listings.Length; i++) {
                            listings[i] = new Tournaments.TournamentListing(
                                parse[n],
                                parse[n + 1],
                                parse[n + 2]);
                            n += 3;
                        }
                        Menus.MenuSwitcher.ShowTournamentListingMenu(listings, Enums.TournamentListingMode.Spectate);
                    }
                    break;
                case "tournamentruleseditor": {
                        Tournaments.TournamentRules rules = new Tournaments.TournamentRules();
                        rules.SleepClause = parse[1].ToBool();
                        rules.AccuracyClause = parse[2].ToBool();
                        rules.SpeciesClause = parse[3].ToBool();
                        rules.FreezeClause = parse[4].ToBool();
                        rules.OHKOClause = parse[5].ToBool();
                        rules.SelfKOClause = parse[6].ToBool();

                        Menus.MenuSwitcher.ShowTournamentRulesEditorMenu(rules);
                    }
                    break;
                case "tournamentrules": {
                        Tournaments.TournamentRules rules = new Tournaments.TournamentRules();
                        rules.SleepClause = parse[1].ToBool();
                        rules.AccuracyClause = parse[2].ToBool();
                        rules.SpeciesClause = parse[3].ToBool();
                        rules.FreezeClause = parse[4].ToBool();
                        rules.OHKOClause = parse[5].ToBool();
                        rules.SelfKOClause = parse[6].ToBool();

                        Menus.MenuSwitcher.ShowTournamentRulesViewerMenu(rules);
                    }
                    break;
                #endregion



            }
        }


        private static void ChatReceived(string[] parse) {
            string text = parse[1].Replace("\\", "/") + "\n";
            List<SdlDotNet.Widgets.CharRenderOptions> renderOptions = new List<SdlDotNet.Widgets.CharRenderOptions>();
            Color color;
            if (parse[2].ToInt() == -1) {
                color = Color.FromArgb(255, 255, 255, 254);
            } else {
                color = Color.FromArgb(parse[2].ToInt());
            }
            bool foundName = text.Contains(":");
            bool nameEnded = false;
            for (int i = 0; i < text.Length; i++) {
                SdlDotNet.Widgets.CharRenderOptions options = new SdlDotNet.Widgets.CharRenderOptions(color);
                if (foundName) {
                    if (nameEnded == false) {
                        options.Bold = true;
                        if (text[i] == ':') {
                            nameEnded = true;
                        }
                    }
                }
                renderOptions.Add(options);
            }
            SdlDotNet.Widgets.CharRenderOptions[] newRenderOptions = ParseText(renderOptions.ToArray(), ref text);
            ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
            if (chat != null) {
                chat.AppendChat(text, newRenderOptions);
            }
        }

        private static void RunScriptEditor() {
            System.Windows.Forms.Application.EnableVisualStyles();
            System.Windows.Forms.Application.Run(Windows.Editors.EditorManager.ScriptEditor = new Windows.Editors.ScriptEditor.frmScriptEditor());
        }

        public static SdlDotNet.Widgets.CharRenderOptions[] ParseText(SdlDotNet.Widgets.CharRenderOptions[] renderOptions, ref string text) {
            List<SdlDotNet.Widgets.CharRenderOptions> newRenderOptions = new List<SdlDotNet.Widgets.CharRenderOptions>(text.Length);
            for (int i = 0; i < text.Length; i++) {
                if (renderOptions != null && i < renderOptions.Length) {
                    newRenderOptions.Add(renderOptions[i]);
                } else {
                    newRenderOptions.Add(new SdlDotNet.Widgets.CharRenderOptions(Color.Black));
                }
            }
            //int index = text.IndexOf("[b]");
            //while (index > -1) {
            //    text = text.Remove(index, 3);
            //    newRenderOptions.RemoveRange(index, 3);
            //    int endIndex = text.IndexOf("[/b]");
            //    if (endIndex > -1) {
            //        text = text.Remove(endIndex, 4);
            //        newRenderOptions.RemoveRange(endIndex, 4);
            //        for (int i = index; i < endIndex; i++) {
            //            newRenderOptions[i].Bold = true;
            //        }
            //    }

            //    index = text.IndexOf("[b]");
            //}

            //index = text.IndexOf("[i]");
            //while (index > -1) {
            //    text = text.Remove(index, 3);
            //    newRenderOptions.RemoveRange(index, 3);
            //    int endIndex = text.IndexOf("[/i]");
            //    if (endIndex > -1) {
            //        text = text.Remove(endIndex, 4);
            //        newRenderOptions.RemoveRange(endIndex, 4);
            //        for (int i = index; i < endIndex; i++) {
            //            newRenderOptions[i].Italic = true;
            //        }
            //    }

            //    index = text.IndexOf("[i]");
            //}

            //index = text.IndexOf("[u]");
            //while (index > -1) {
            //    text = text.Remove(index, 3);
            //    newRenderOptions.RemoveRange(index, 3);
            //    int endIndex = text.IndexOf("[/u]");
            //    if (endIndex > -1) {
            //        text = text.Remove(endIndex, 4);
            //        newRenderOptions.RemoveRange(endIndex, 4);
            //        for (int i = index; i < endIndex; i++) {
            //            newRenderOptions[i].Underline = true;
            //        }
            //    }

            //    index = text.IndexOf("[u]");
            //}

            int index = text.IndexOf("[c]");
            while (index > -1) {
                int colorStartIndex = text.IndexOf('[', index + 3);
                int colorEndIndex = text.IndexOf(']', colorStartIndex);

                if (colorStartIndex > -1 && colorEndIndex > -1) {

                    string colorValue = text.Substring(colorStartIndex + 1, (colorEndIndex - colorStartIndex) - 1);
                    Color color = Color.FromArgb(colorValue.ToInt());
                    text = text.Remove(index, 3);
                    newRenderOptions.RemoveRange(index, 3);

                    text = text.Remove(colorStartIndex - 3, colorValue.Length + 2);
                    newRenderOptions.RemoveRange(colorStartIndex - 3, colorValue.Length + 2);

                    int endIndex = text.IndexOf("[/c]");
                    if (endIndex > -1) {
                        text = text.Remove(endIndex, 4);
                        newRenderOptions.RemoveRange(endIndex, 4);
                        for (int i = index; i < endIndex; i++) {
                            newRenderOptions[i].ForeColor = color;
                        }
                    }

                }
                index = text.IndexOf("[c]");
            }

            return newRenderOptions.ToArray();
        }

        #endregion Methods
    }
}