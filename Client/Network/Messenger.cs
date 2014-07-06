namespace Client.Logic.Network
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;
    using PMU.Sockets;

    /// <summary>
    /// Handles sending common packets to the server.
    /// </summary>
    internal class Messenger
    {
        #region Methods

        public static void SendRequestNews() {
            SendPacket(TcpPacket.CreatePacket("requestnews"));
        }

        #region Logging In

        /// <summary>
        /// Sends the player's login information to the server.
        /// </summary>
        /// <param name="account">The account.</param>
        /// <param name="password">The password.</param>
        public static void SendLogin(string account, string password) {
            //string hashedPass = Security.Hash.GenerateMD5Hash(password).Trim();
            string mac = PMU.Net.NetTools.GetMacAddress();
            SendPacket(TcpPacket.CreatePacket("login", account, password, Constants.CLIENT_VERSION.ToString(),
                            Constants.SEC_CODE1, Constants.SEC_CODE2, Constants.SEC_CODE3, Constants.SEC_CODE4,
                            System.Environment.OSVersion.VersionString, System.Environment.Version.ToString(), Constants.CLIENT_EDITION,
                            mac ?? "", MathFunctions.Rand(1, 99999).ToString()), false, true);
        }

        public static void SendCharListRequest() {
            SendPacket(TcpPacket.CreatePacket("charlistrequest"));
        }

        /// <summary>
        /// Sends the character that the player will use to the server.
        /// </summary>
        /// <param name="charNum">The character num.</param>
        public static void SendUseChar(int charNum) {
            SendPacket(TcpPacket.CreatePacket("usechar", charNum.ToString()));
        }

        #endregion Logging In

        #region Account Editing

        //gatglasses

        public static void SendCreateAccountRequest(string account, string password) {
            SendPacket(TcpPacket.CreatePacket("createaccount", account, password));
        }

        public static void SendNewChar(string name, Enums.Sex sex, int charSlot) {
            SendPacket(TcpPacket.CreatePacket("addchar", name, ((int)sex).ToString(), charSlot.ToString()));
        }

        public static void SendDeleteChar(int charSlot) {
            SendPacket(TcpPacket.CreatePacket("delchar", charSlot.ToString()));
        }

        public static void SendDeleteAccount(string account, string password) {
            string hashedPass = Security.Hash.GenerateMD5Hash(password).Trim();
            SendPacket(TcpPacket.CreatePacket("deleteaccount", account, hashedPass));
        }

        public static void SendPasswordChange(string account, string oldPass, string newPass) {
            SendPacket(TcpPacket.CreatePacket("passchange", account, oldPass, newPass));
        }

        #endregion Account Editing

        #region Movement

        public static void SendPlayerCriticalMove() {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null) {
                
            } else {
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
            }
            Players.PlayerManager.MyPlayer.MovementPacketCache.AddPacket(TcpPacket.CreatePacket("critmove", ((int)Players.PlayerManager.MyPlayer.Direction).ToString(), ((int)Players.PlayerManager.MyPlayer.MovementSpeed).ToString()));
            NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
            Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
            Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            //SendPacket();
        }

        public static void SendPlayerMove() {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null) {
                if (Globals.Tick > Players.PlayerManager.MyPlayer.LastMovementCacheSend + Constants.MovementClusteringFrquency && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                    NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                    Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                    Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
                }
            } else {
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
            }
            Players.PlayerManager.MyPlayer.MovementPacketCache.AddPacket(TcpPacket.CreatePacket("playermove", ((int)Players.PlayerManager.MyPlayer.Direction).ToString(), ((int)Players.PlayerManager.MyPlayer.MovementSpeed).ToString()));
            //SendPacket();
        }

        public static void SendPlayerDir() {
            SendPacket(TcpPacket.CreatePacket("playerdir", ((int)Players.PlayerManager.MyPlayer.Direction).ToString()));
        }

        /// <summary>
        /// Sends a "requestnewmap" packet to the server.
        /// </summary>
        /// <param name="cancel">cancel or not</param>
        public static void SendPlayerRequestNewMap(bool cancel) {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            } 
            SendPacket(TcpPacket.CreatePacket("requestnewmap", ((int)Players.PlayerManager.MyPlayer.Direction).ToString(), cancel.ToIntString()));
        }

        /// <summary>
        /// Sends a response to the "checkformap" packet
        /// </summary>
        /// <param name="val">"yes" or "no</param>
        public static void SendNeedMapResponse(bool val) {
            SendPacket(TcpPacket.CreatePacket("needmap", val.ToIntString()));
        }

        public static void SendNeedMapResponse(bool[] results) {
            TcpPacket packet = new TcpPacket("needmapseamless");
            packet.AppendParameter(results.Length);
            for (int i = 0; i < results.Length; i++) {
                packet.AppendParameter(results[i].ToIntString());
            }
            SendPacket(packet);
        }

        public static void SendRefresh() {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            }
            SendPacket(TcpPacket.CreatePacket("refresh"));
        }

        public static void SendMapLoaded() {
            SendPacket(TcpPacket.CreatePacket("maploaded"));
        }

        #endregion Movement

        #region Attacking

        public static void SendAttack() {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            }
            SendPacket(TcpPacket.CreatePacket("attack"));
        }
        #endregion Attacking

        #region Attacking - Moves
        public static void SendForgetMove(int moveSlot) {
            SendPacket(TcpPacket.CreatePacket("forgetspell", moveSlot.ToString()));
        }

        public static void SendShiftMove(int moveSlot, bool shiftUp) {
            SendPacket(TcpPacket.CreatePacket("shiftspell", moveSlot.ToString(), shiftUp.ToString()));
        }

        public static void SendSwapMoves(int oldMoveSlot, int newMoveSlot) {
            SendPacket(TcpPacket.CreatePacket("swapmoves", oldMoveSlot, newMoveSlot));
        }

        public static void SendUseMove(int moveSlot) {
            SendPacket(TcpPacket.CreatePacket("cast", moveSlot.ToString()));
        }
        #endregion Attacking - Moves

        #region Messages

        public static void BroadcastMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("broadcastmsg", text));
        }

        public static void SendMapMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("saymsg", text));
        }

        public static void EmoteMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("emotemsg", text));
        }

        public static void GlobalMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("globalmsg", text));
        }

        public static void AdminMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("adminmsg", text));
        }

        public static void GuildMsg(string text) {
            SendPacket(TcpPacket.CreatePacket("guildmsg", text));
        }

        public static void PlayerMsg(string msgto, string text) {
            SendPacket(TcpPacket.CreatePacket("playermsg", msgto, text));
        }

        public static void MapMsg(string text) {//server doesn't accept this packet...
            SendPacket(TcpPacket.CreatePacket("mapmsg", text));
        }

        #endregion Messages

        #region Recruits
        public static void SendActiveCharSwap(int slot) {
            SendPacket(TcpPacket.CreatePacket("requestactivecharswap", slot.ToString()));
        }

        public static void SendSwitchLeader(int oldSlot) {
            SendPacket(TcpPacket.CreatePacket("switchleader", oldSlot.ToString()));
        }

        public static void SendAddToTeam(int teamSlot, int recruitIndex) {
            SendPacket(TcpPacket.CreatePacket("addtoteam", teamSlot.ToString(), recruitIndex.ToString()));
        }

        public static void SendRemoveFromTeam(int slot) {
            SendPacket(TcpPacket.CreatePacket("removefromteam", slot.ToString()));
        }

        public static void SendStandbyFromTeam(int slot) {
            SendPacket(TcpPacket.CreatePacket("standby", slot.ToString()));
        }

        public static void SendReleaseRecruit(int recruitIndex) {
            SendPacket(TcpPacket.CreatePacket("releaserecruit", recruitIndex.ToString()));
        }

        public static void SendChangeRecruitName(int slot, string newName) {
            SendPacket(TcpPacket.CreatePacket("changerecruitname", slot.ToString(), newName));
        }
        #endregion Recruits

        #region Missions

        public static void SendAcceptMission(int missionSlot) {
            SendPacket(TcpPacket.CreatePacket("acceptmission", missionSlot.ToString()));
        }

        public static void SendStartMission(int jobSlot) {
            SendPacket(TcpPacket.CreatePacket("startmission", jobSlot.ToString()));
        }

        public static void SendCancelJob(int jobSlot) {
            SendPacket(TcpPacket.CreatePacket("canceljob", jobSlot.ToString()));
        }

        public static void SendDeleteJob(int jobSlot) {
            SendPacket(TcpPacket.CreatePacket("deletejob", jobSlot.ToString()));
        }

        public static void SendSendMission(int jobSlot, string name) {
            SendPacket(TcpPacket.CreatePacket("sendmission", jobSlot.ToString(), name));
        }

        #endregion Missions

        #region Misc

        public static void SendPing() {
            SendPacket(TcpPacket.CreatePacket("ping"));
        }

        public static void SendSearch(int targetX, int targetY) {
            SendPacket(TcpPacket.CreatePacket("search", targetX.ToString(), targetY.ToString()));
        }

        public static void SendPickupItem() {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            }
            SendPacket(TcpPacket.CreatePacket("mapgetitem"));
        }

        public static void SendDropItem(int item, int amount) {
            if (Players.PlayerManager.MyPlayer.MovementPacketCache != null && Players.PlayerManager.MyPlayer.MovementPacketCache.Packets.Count > 0) {
                NetworkManager.SendData(Players.PlayerManager.MyPlayer.MovementPacketCache);
                Players.PlayerManager.MyPlayer.MovementPacketCache = new PacketList();
                Players.PlayerManager.MyPlayer.LastMovementCacheSend = Globals.Tick;
            }
            SendPacket(TcpPacket.CreatePacket("mapdropitem", item.ToString(), amount.ToString()));
        }

        public static void SendCheckArrow(int index) {
            SendPacket(TcpPacket.CreatePacket("checkarrows", index.ToString()));
        }

        public static void SendCheckEmoticons(int index) {
            SendPacket(TcpPacket.CreatePacket("checkemoticons", index.ToString()));
        }

        public static void SendOnlineListRequest() {
            SendPacket(TcpPacket.CreatePacket("onlinelist"));
        }

        public static void SendAdventureLogRequest()
        {
            SendPacket(TcpPacket.CreatePacket("adventurelog"));
        }

        #endregion Misc

        #region Scripting

        public static void SendHotScript(int index) {
            SendPacket(TcpPacket.CreatePacket("hotscript" + index.ToString()));
        }

        public static void SendQuestionResult(bool answer) {
            SendPacket(TcpPacket.CreatePacket("questionresult", answer.ToString()));
        }

        public static void SendStoryScript(int scriptNum, bool paused) {
            SendPacket(TcpPacket.CreatePacket("runstoryscript", scriptNum.ToString(), paused.ToIntString()));
        }

        public static void SendCheckCommands(string command) {
            SendPacket(TcpPacket.CreatePacket("checkcommands", command));
        }

        public static void SendReloadScripts() {
            SendPacket(TcpPacket.CreatePacket("reloadscripts"));
        }

        #endregion Scripting

        #region Tile Checks

        public static void SendBuySprite() {
            SendPacket(TcpPacket.CreatePacket("buysprite"));
        }

        public static void SendBuyHouse() {
            SendPacket(TcpPacket.CreatePacket("buyhouse"));
        }

        #endregion Tile Checks

        #region Story

        public static void SendNeedStory(string start, int storyNum) {
            SendPacket(TcpPacket.CreatePacket("needstory", start, storyNum.ToString()));
        }

        public static void SendUpdateSegment(int segment) {
            SendPacket(TcpPacket.CreatePacket("updatesegment", segment.ToString()));
        }

        public static void SendAction() {
            SendPacket(TcpPacket.CreatePacket("actonaction"));
        }

        public static void SendChapterComplete() {
            SendPacket(TcpPacket.CreatePacket("chaptercomplete"));
        }

        public static void SendStoryLoadingComplete() {
            SendPacket(TcpPacket.CreatePacket("storyloadingcomplete"));
        }

        #endregion Story

        #region Editors

        #region RDungeons

        public static void SendEditRDungeon(int rdungeonNum) {




            SendPacket(TcpPacket.CreatePacket("editrdungeon", rdungeonNum.ToString()));
        }

        public static void SendAddRDungeon() {




            SendPacket(TcpPacket.CreatePacket("addnewrdungeon"));
        }

        public static void SendSaveRDungeon(Logic.Editors.RDungeons.EditableRDungeon rdungeon) {

            TcpPacket packet = new TcpPacket("saverdungeon");
            packet.AppendParameter(rdungeon.RDungeonIndex.ToString());

            packet.AppendParameters(rdungeon.DungeonName, ((int)rdungeon.Direction).ToString(), rdungeon.MaxFloors.ToString(),
                rdungeon.Recruitment.ToIntString(), rdungeon.Exp.ToIntString(), rdungeon.WindTimer.ToString(), rdungeon.DungeonIndex.ToString());
            for (int i = 0; i < rdungeon.MaxFloors; i++) {
                if (i >= rdungeon.Floors.Count) {
                    rdungeon.Floors.Add(new Logic.Editors.RDungeons.EditableRDungeonFloor());
                }
                //Generator Options
                packet.AppendParameters(rdungeon.Floors[i].TrapMin.ToString(), rdungeon.Floors[i].TrapMax.ToString(),
                                        rdungeon.Floors[i].ItemMin.ToString(), rdungeon.Floors[i].ItemMax.ToString(),
                                        rdungeon.Floors[i].Intricacy.ToString(),
                                        rdungeon.Floors[i].RoomWidthMin.ToString(), rdungeon.Floors[i].RoomWidthMax.ToString(),
                                        rdungeon.Floors[i].RoomLengthMin.ToString(), rdungeon.Floors[i].RoomLengthMax.ToString(),
                                        rdungeon.Floors[i].HallTurnMin.ToString(), rdungeon.Floors[i].HallTurnMax.ToString(),
                                        rdungeon.Floors[i].HallVarMin.ToString(), rdungeon.Floors[i].HallVarMax.ToString(),
                                        rdungeon.Floors[i].WaterFrequency.ToString(), rdungeon.Floors[i].Craters.ToString(),
                                        rdungeon.Floors[i].CraterMinLength.ToString(), rdungeon.Floors[i].CraterMaxLength.ToString(),
                                        rdungeon.Floors[i].CraterFuzzy.ToIntString(),
                                        rdungeon.Floors[i].MinChambers.ToString(), rdungeon.Floors[i].MaxChambers.ToString());
                packet.AppendParameters(rdungeon.Floors[i].Darkness, (int)rdungeon.Floors[i].GoalType, rdungeon.Floors[i].GoalMap, rdungeon.Floors[i].GoalX, rdungeon.Floors[i].GoalY);
                packet.AppendParameter(rdungeon.Floors[i].Music);
                //Terrain
                packet.AppendParameters(rdungeon.Floors[i].StairsX.ToString(), rdungeon.Floors[i].StairsSheet.ToString(),
                                        rdungeon.Floors[i].mGroundX.ToString(), rdungeon.Floors[i].mGroundSheet.ToString(),
                                        rdungeon.Floors[i].mTopLeftX.ToString(), rdungeon.Floors[i].mTopLeftSheet.ToString(),
                                        rdungeon.Floors[i].mTopCenterX.ToString(), rdungeon.Floors[i].mTopCenterSheet.ToString(),
                                        rdungeon.Floors[i].mTopRightX.ToString(), rdungeon.Floors[i].mTopRightSheet.ToString(),
                                        rdungeon.Floors[i].mCenterLeftX.ToString(), rdungeon.Floors[i].mCenterLeftSheet.ToString(),
                                        rdungeon.Floors[i].mCenterCenterX.ToString(), rdungeon.Floors[i].mCenterCenterSheet.ToString(),
                                        rdungeon.Floors[i].mCenterRightX.ToString(), rdungeon.Floors[i].mCenterRightSheet.ToString(),
                                        rdungeon.Floors[i].mBottomLeftX.ToString(), rdungeon.Floors[i].mBottomLeftSheet.ToString(),
                                        rdungeon.Floors[i].mBottomCenterX.ToString(), rdungeon.Floors[i].mBottomCenterSheet.ToString(),
                                        rdungeon.Floors[i].mBottomRightX.ToString(), rdungeon.Floors[i].mBottomRightSheet.ToString(),
                                        rdungeon.Floors[i].mInnerTopLeftX.ToString(), rdungeon.Floors[i].mInnerTopLeftSheet.ToString(),
                                        rdungeon.Floors[i].mInnerBottomLeftX.ToString(), rdungeon.Floors[i].mInnerBottomLeftSheet.ToString(),
                                        rdungeon.Floors[i].mInnerTopRightX.ToString(), rdungeon.Floors[i].mInnerTopRightSheet.ToString(),
                                        rdungeon.Floors[i].mInnerBottomRightX.ToString(), rdungeon.Floors[i].mInnerBottomRightSheet.ToString(),
                                        rdungeon.Floors[i].mIsolatedWallX.ToString(), rdungeon.Floors[i].mIsolatedWallSheet.ToString(),
                                        rdungeon.Floors[i].mColumnTopX.ToString(), rdungeon.Floors[i].mColumnTopSheet.ToString(),
                                        rdungeon.Floors[i].mColumnCenterX.ToString(), rdungeon.Floors[i].mColumnCenterSheet.ToString(),
                                        rdungeon.Floors[i].mColumnBottomX.ToString(), rdungeon.Floors[i].mColumnBottomSheet.ToString(),
                                        rdungeon.Floors[i].mRowLeftX.ToString(), rdungeon.Floors[i].mRowLeftSheet.ToString(),
                                        rdungeon.Floors[i].mRowCenterX.ToString(), rdungeon.Floors[i].mRowCenterSheet.ToString(),
                                        rdungeon.Floors[i].mRowRightX.ToString(), rdungeon.Floors[i].mRowRightSheet.ToString(),
                                        rdungeon.Floors[i].mGroundAltX.ToString(), rdungeon.Floors[i].mGroundAltSheet.ToString(),
                                        rdungeon.Floors[i].mGroundAlt2X.ToString(), rdungeon.Floors[i].mGroundAlt2Sheet.ToString(),
                                        rdungeon.Floors[i].mTopLeftAltX.ToString(), rdungeon.Floors[i].mTopLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mTopCenterAltX.ToString(), rdungeon.Floors[i].mTopCenterAltSheet.ToString(),
                                        rdungeon.Floors[i].mTopRightAltX.ToString(), rdungeon.Floors[i].mTopRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mCenterLeftAltX.ToString(), rdungeon.Floors[i].mCenterLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mCenterCenterAltX.ToString(), rdungeon.Floors[i].mCenterCenterAltSheet.ToString(),
                                        rdungeon.Floors[i].mCenterCenterAlt2X.ToString(), rdungeon.Floors[i].mCenterCenterAlt2Sheet.ToString(),
                                        rdungeon.Floors[i].mCenterRightAltX.ToString(), rdungeon.Floors[i].mCenterRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mBottomLeftAltX.ToString(), rdungeon.Floors[i].mBottomLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mBottomCenterAltX.ToString(), rdungeon.Floors[i].mBottomCenterAltSheet.ToString(),
                                        rdungeon.Floors[i].mBottomRightAltX.ToString(), rdungeon.Floors[i].mBottomRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mInnerTopLeftAltX.ToString(), rdungeon.Floors[i].mInnerTopLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mInnerBottomLeftAltX.ToString(), rdungeon.Floors[i].mInnerBottomLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mInnerTopRightAltX.ToString(), rdungeon.Floors[i].mInnerTopRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mInnerBottomRightAltX.ToString(), rdungeon.Floors[i].mInnerBottomRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mIsolatedWallAltX.ToString(), rdungeon.Floors[i].mIsolatedWallAltSheet.ToString(),
                                        rdungeon.Floors[i].mColumnTopAltX.ToString(), rdungeon.Floors[i].mColumnTopAltSheet.ToString(),
                                        rdungeon.Floors[i].mColumnCenterAltX.ToString(), rdungeon.Floors[i].mColumnCenterAltSheet.ToString(),
                                        rdungeon.Floors[i].mColumnBottomAltX.ToString(), rdungeon.Floors[i].mColumnBottomAltSheet.ToString(),
                                        rdungeon.Floors[i].mRowLeftAltX.ToString(), rdungeon.Floors[i].mRowLeftAltSheet.ToString(),
                                        rdungeon.Floors[i].mRowCenterAltX.ToString(), rdungeon.Floors[i].mRowCenterAltSheet.ToString(),
                                        rdungeon.Floors[i].mRowRightAltX.ToString(), rdungeon.Floors[i].mRowRightAltSheet.ToString(),
                                        rdungeon.Floors[i].mWaterX.ToString(), rdungeon.Floors[i].mWaterSheet.ToString(),
                                        rdungeon.Floors[i].mWaterAnimX.ToString(), rdungeon.Floors[i].mWaterAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopLeftX.ToString(), rdungeon.Floors[i].mShoreTopLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopRightX.ToString(), rdungeon.Floors[i].mShoreTopRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomRightX.ToString(), rdungeon.Floors[i].mShoreBottomRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomLeftX.ToString(), rdungeon.Floors[i].mShoreBottomLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreDiagonalForwardX.ToString(), rdungeon.Floors[i].mShoreDiagonalForwardSheet.ToString(),
                                        rdungeon.Floors[i].mShoreDiagonalBackX.ToString(), rdungeon.Floors[i].mShoreDiagonalBackSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopX.ToString(), rdungeon.Floors[i].mShoreTopSheet.ToString(),
                                        rdungeon.Floors[i].mShoreRightX.ToString(), rdungeon.Floors[i].mShoreRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomX.ToString(), rdungeon.Floors[i].mShoreBottomSheet.ToString(),
                                        rdungeon.Floors[i].mShoreLeftX.ToString(), rdungeon.Floors[i].mShoreLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreVerticalX.ToString(), rdungeon.Floors[i].mShoreVerticalSheet.ToString(),
                                        rdungeon.Floors[i].mShoreHorizontalX.ToString(), rdungeon.Floors[i].mShoreHorizontalSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopLeftX.ToString(), rdungeon.Floors[i].mShoreInnerTopLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopRightX.ToString(), rdungeon.Floors[i].mShoreInnerTopRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomRightX.ToString(), rdungeon.Floors[i].mShoreInnerBottomRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomLeftX.ToString(), rdungeon.Floors[i].mShoreInnerBottomLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopX.ToString(), rdungeon.Floors[i].mShoreInnerTopSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerRightX.ToString(), rdungeon.Floors[i].mShoreInnerRightSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomX.ToString(), rdungeon.Floors[i].mShoreInnerBottomSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerLeftX.ToString(), rdungeon.Floors[i].mShoreInnerLeftSheet.ToString(),
                                        rdungeon.Floors[i].mShoreSurroundedX.ToString(), rdungeon.Floors[i].mShoreSurroundedSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopLeftAnimX.ToString(), rdungeon.Floors[i].mShoreTopLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopRightAnimX.ToString(), rdungeon.Floors[i].mShoreTopRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomRightAnimX.ToString(), rdungeon.Floors[i].mShoreBottomRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomLeftAnimX.ToString(), rdungeon.Floors[i].mShoreBottomLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreDiagonalForwardAnimX.ToString(), rdungeon.Floors[i].mShoreDiagonalForwardAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreDiagonalBackAnimX.ToString(), rdungeon.Floors[i].mShoreDiagonalBackAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreTopAnimX.ToString(), rdungeon.Floors[i].mShoreTopAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreRightAnimX.ToString(), rdungeon.Floors[i].mShoreRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreBottomAnimX.ToString(), rdungeon.Floors[i].mShoreBottomAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreLeftAnimX.ToString(), rdungeon.Floors[i].mShoreLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreVerticalAnimX.ToString(), rdungeon.Floors[i].mShoreVerticalAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreHorizontalAnimX.ToString(), rdungeon.Floors[i].mShoreHorizontalAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopLeftAnimX.ToString(), rdungeon.Floors[i].mShoreInnerTopLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopRightAnimX.ToString(), rdungeon.Floors[i].mShoreInnerTopRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomRightAnimX.ToString(), rdungeon.Floors[i].mShoreInnerBottomRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomLeftAnimX.ToString(), rdungeon.Floors[i].mShoreInnerBottomLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerTopAnimX.ToString(), rdungeon.Floors[i].mShoreInnerTopAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerRightAnimX.ToString(), rdungeon.Floors[i].mShoreInnerRightAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerBottomAnimX.ToString(), rdungeon.Floors[i].mShoreInnerBottomAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreInnerLeftAnimX.ToString(), rdungeon.Floors[i].mShoreInnerLeftAnimSheet.ToString(),
                                        rdungeon.Floors[i].mShoreSurroundedAnimX.ToString(), rdungeon.Floors[i].mShoreSurroundedAnimSheet.ToString());
                packet.AppendParameters(((int)rdungeon.Floors[i].GroundTile.Type).ToString(),
                                rdungeon.Floors[i].GroundTile.Data1.ToString(),
                                rdungeon.Floors[i].GroundTile.Data2.ToString(),
                                rdungeon.Floors[i].GroundTile.Data3.ToString(),
                                rdungeon.Floors[i].GroundTile.String1,
                                rdungeon.Floors[i].GroundTile.String2,
                                rdungeon.Floors[i].GroundTile.String3,
                                
                                ((int)rdungeon.Floors[i].HallTile.Type).ToString(),
                                rdungeon.Floors[i].HallTile.Data1.ToString(),
                                rdungeon.Floors[i].HallTile.Data2.ToString(),
                                rdungeon.Floors[i].HallTile.Data3.ToString(),
                                rdungeon.Floors[i].HallTile.String1,
                                rdungeon.Floors[i].HallTile.String2,
                                rdungeon.Floors[i].HallTile.String3,
                                
                                ((int)rdungeon.Floors[i].WaterTile.Type).ToString(),
                                rdungeon.Floors[i].WaterTile.Data1.ToString(),
                                rdungeon.Floors[i].WaterTile.Data2.ToString(),
                                rdungeon.Floors[i].WaterTile.Data3.ToString(),
                                rdungeon.Floors[i].WaterTile.String1,
                                rdungeon.Floors[i].WaterTile.String2,
                                rdungeon.Floors[i].WaterTile.String3,
                                
                                ((int)rdungeon.Floors[i].WallTile.Type).ToString(),
                                rdungeon.Floors[i].WallTile.Data1.ToString(),
                                rdungeon.Floors[i].WallTile.Data2.ToString(),
                                rdungeon.Floors[i].WallTile.Data3.ToString(),
                                rdungeon.Floors[i].WallTile.String1,
                                rdungeon.Floors[i].WallTile.String2,
                                rdungeon.Floors[i].WallTile.String3,
                                
                                rdungeon.Floors[i].NpcSpawnTime.ToString(),
                                rdungeon.Floors[i].NpcMin.ToString(),
                                rdungeon.Floors[i].NpcMax.ToString());
                
                packet.AppendParameter(rdungeon.Floors[i].Items.Count);
                for (int item = 0; item < rdungeon.Floors[i].Items.Count; item++) {
                    packet.AppendParameters(rdungeon.Floors[i].Items[item].ItemNum.ToString(),
                	                        rdungeon.Floors[i].Items[item].MinAmount.ToString(),
                	                        rdungeon.Floors[i].Items[item].MaxAmount.ToString(),
                	                        rdungeon.Floors[i].Items[item].AppearanceRate.ToString(),
                	                        rdungeon.Floors[i].Items[item].StickyRate.ToString(),
                	                        rdungeon.Floors[i].Items[item].Tag,
                	                        rdungeon.Floors[i].Items[item].Hidden.ToIntString(),
                	                        rdungeon.Floors[i].Items[item].OnGround.ToIntString(),
                	                        rdungeon.Floors[i].Items[item].OnWater.ToIntString(),
                	                        rdungeon.Floors[i].Items[item].OnWall.ToIntString());
                }
                
                packet.AppendParameter(rdungeon.Floors[i].Npcs.Count);
                for (int npc = 0; npc < rdungeon.Floors[i].Npcs.Count; npc++) {
                    packet.AppendParameters(rdungeon.Floors[i].Npcs[npc].NpcNum.ToString(),
                	                        rdungeon.Floors[i].Npcs[npc].MinLevel.ToString(),
                	                        rdungeon.Floors[i].Npcs[npc].MaxLevel.ToString(),
                                            rdungeon.Floors[i].Npcs[npc].AppearanceRate.ToString(),
                                            ((int)rdungeon.Floors[i].Npcs[npc].StartStatus).ToString(),
                                            rdungeon.Floors[i].Npcs[npc].StartStatusCounter.ToString(),
                                            rdungeon.Floors[i].Npcs[npc].StartStatusChance.ToString());
                }
                
                packet.AppendParameter(rdungeon.Floors[i].SpecialTiles.Count);
                for (int trap = 0; trap < rdungeon.Floors[i].SpecialTiles.Count; trap++) {
                    packet.AppendParameters(((int)rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Type).ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Data1.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Data2.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Data3.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.String1,
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.String2,
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.String3,
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Ground.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.GroundSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.GroundAnim.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.GroundAnimSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Mask.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.MaskSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Anim.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.AnimSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Mask2.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Mask2Set.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.M2Anim.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.M2AnimSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Fringe.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.FringeSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.FAnim.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.FAnimSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Fringe2.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.Fringe2Set.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.F2Anim.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.F2AnimSet.ToString(),
                	                        rdungeon.Floors[i].SpecialTiles[trap].SpecialTile.RDungeonMapValue.ToString(),
                                            rdungeon.Floors[i].SpecialTiles[trap].AppearanceRate.ToString());
                }
                
                packet.AppendParameter(rdungeon.Floors[i].Weather.Count);
                for (int weather = 0; weather < rdungeon.Floors[i].Weather.Count; weather++) {
                    packet.AppendParameters(((int)rdungeon.Floors[i].Weather[weather]).ToString());
                }

                packet.AppendParameter(rdungeon.Floors[i].Chambers.Count);
                for (int chamber = 0; chamber < rdungeon.Floors[i].Chambers.Count; chamber++)
                {
                    packet.AppendParameters(rdungeon.Floors[i].Chambers[chamber].ChamberNum.ToString(),
                        rdungeon.Floors[i].Chambers[chamber].String1,
                        rdungeon.Floors[i].Chambers[chamber].String2,
                        rdungeon.Floors[i].Chambers[chamber].String3);
                }
            }
            packet.FinalizePacket();

            SendPacket(packet);
        }
        #endregion
        #region Maps
        public static void SendSaveMap(Maps.Map map) {
            Globals.SavingMap = true;
            TcpPacket packet = new TcpPacket("mapdata");
            int x, y;
            if (map.MaxX == 0)
                map.MaxX = 19;
            if (map.MaxY == 0)
                map.MaxY = 14;

            packet.AppendParameters(map.MapID,
                map.Name.Trim(),
                map.Revision.ToString(),
                ((int)map.Moral).ToString(),
                map.Up.ToString(),
                map.Down.ToString(),
                map.Left.ToString(),
                map.Right.ToString(),
                map.Music,
                map.Indoors.ToIntString(),
                ((int)map.Weather).ToString(),
                map.MaxX.ToString(),
                map.MaxY.ToString(),
                map.Darkness.ToString(),
                map.HungerEnabled.ToIntString(),
                map.RecruitEnabled.ToIntString(),
                map.ExpEnabled.ToIntString(),
                map.TimeLimit.ToString(),
                map.MinNpcs.ToString(),
                map.MaxNpcs.ToString(),
                map.NpcSpawnTime.ToString(),
                map.Instanced.ToIntString()
                );

            for (y = 0; y <= map.MaxY; y++) {
                for (x = 0; x <= map.MaxX; x++) {
                    int maxX = map.Tile.GetUpperBound(0);
                    int maxY = map.Tile.GetUpperBound(1);
                    if (x > maxX || y > maxY) {
                        packet.AppendParameters("0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "",
                            "",
                            "",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0",
                            "0");
                    } else {
                        packet.AppendParameters(map.Tile[x, y].Ground.ToString(),
                            map.Tile[x, y].GroundAnim.ToString(),
                            map.Tile[x, y].Mask.ToString(),
                            map.Tile[x, y].Anim.ToString(),
                            map.Tile[x, y].Mask2.ToString(),
                            map.Tile[x, y].M2Anim.ToString(),
                            map.Tile[x, y].Fringe.ToString(),
                            map.Tile[x, y].FAnim.ToString(),
                            map.Tile[x, y].Fringe2.ToString(),
                            map.Tile[x, y].F2Anim.ToString(),
                            ((int)map.Tile[x, y].Type).ToString(),
                            map.Tile[x, y].Data1.ToString(),
                            map.Tile[x, y].Data2.ToString(),
                            map.Tile[x, y].Data3.ToString(),
                            map.Tile[x, y].String1,
                            map.Tile[x, y].String2,
                            map.Tile[x, y].String3,
                            map.Tile[x, y].RDungeonMapValue.ToString(),
                            map.Tile[x, y].GroundSet.ToString(),
                            map.Tile[x, y].GroundAnimSet.ToString(),
                            map.Tile[x, y].MaskSet.ToString(),
                            map.Tile[x, y].AnimSet.ToString(),
                            map.Tile[x, y].Mask2Set.ToString(),
                            map.Tile[x, y].M2AnimSet.ToString(),
                            map.Tile[x, y].FringeSet.ToString(),
                            map.Tile[x, y].FAnimSet.ToString(),
                            map.Tile[x, y].Fringe2Set.ToString(),
                            map.Tile[x, y].F2AnimSet.ToString());
                    }
                }
            }

            packet.AppendParameter(map.Npc.Count);

            for (x = 0; x < map.Npc.Count; x++) {
                packet.AppendParameters(map.Npc[x].NpcNum,
                    map.Npc[x].SpawnX,
                    map.Npc[x].SpawnY,
                    map.Npc[x].MinLevel,
                    map.Npc[x].MaxLevel,
                    map.Npc[x].AppearanceRate,
                    (int)map.Npc[x].StartStatus,
                    map.Npc[x].StartStatusCounter,
                    map.Npc[x].StartStatusChance);
            }

            packet.FinalizePacket();

            SendPacket(packet);
        }

        public static void SendScriptedTileInfoRequest(int tile) {
            SendPacket(TcpPacket.CreatePacket("scriptedtileinforequest", tile));
        }

        public static void SendScriptedSignInfoRequest(int tile) {
            SendPacket(TcpPacket.CreatePacket("scriptedsigninforequest", tile));
        }

        public static void SendMobilityInfoRequest(int mobility) {
            SendPacket(TcpPacket.CreatePacket("mobilityinforequest", mobility));
        }

        #endregion
        #region Items
        public static void SendSaveItem(int itemNum, Items.Item item) {
            TcpPacket packet = new TcpPacket("saveitem");

            packet.AppendParameters(
                itemNum.ToString(),
                item.Name,
                item.Desc,
                item.Pic.ToString(),
                ((int)item.Type).ToString(),
                item.Data1.ToString(),
                item.Data2.ToString(),
                item.Data3.ToString(),
                item.Price.ToString(),
                item.StackCap.ToString(),
                item.Bound.ToIntString(),
                item.Loseable.ToIntString(),
                item.Rarity.ToString(),
                item.AttackReq.ToString(),
                item.DefenseReq.ToString(),
                item.SpAtkReq.ToString(),
                item.SpDefReq.ToString(),
                item.SpeedReq.ToString(),
                item.ScriptedReq.ToString(),
                item.AddHP.ToString(),
                item.AddPP.ToString(),
                item.AddAttack.ToString(),
                item.AddDefense.ToString(),
                item.AddSpAtk.ToString(),
                item.AddSpDef.ToString(),
                item.AddSpeed.ToString(),
                item.AddEXP.ToString(),
                item.AttackSpeed.ToString(),
                item.RecruitBonus.ToString()
                );

            packet.FinalizePacket();

            SendPacket(packet);

        }

        public static void SendEditItem(int itemNum) {
            TcpPacket packet = new TcpPacket("edititem");
            packet.AppendParameters(
                itemNum.ToString());

            packet.FinalizePacket();

            SendPacket(packet);
        }
        #endregion
        #region Stories
        public static void SendSaveStory(int storyNum, Logic.Editors.Stories.EditableStory storyToSend) {
            TcpPacket packet = new TcpPacket("savestory");
            packet.AppendParameters(storyNum.ToString(),
                                    storyToSend.Name.Trim(),
                                    storyToSend.StoryStart.ToString(),
                                    storyToSend.Segments.Count.ToString());
            for (int i = 0; i < storyToSend.Segments.Count; i++) {
                packet.AppendParameters(storyToSend.Segments[i].Parameters.Count.ToString(),
                                        ((int)storyToSend.Segments[i].Action).ToString());
                for (int z = 0; z < storyToSend.Segments[i].Parameters.Count; z++) {
                    packet.AppendParameters(storyToSend.Segments[i].Parameters.KeyByIndex(z),
                                            storyToSend.Segments[i].Parameters.ValueByIndex(z));
                }
            }
            packet.AppendParameter(storyToSend.ExitAndContinue.Count.ToString());
            for (int i = 0; i < storyToSend.ExitAndContinue.Count; i++) {
                packet.AppendParameters(storyToSend.ExitAndContinue[i].ToString());
            }
            packet.FinalizePacket();
            SendPacket(packet);
        }
        #endregion

        //Evolutions

        public static void SendEditEvo(int evoNum) {
            SendPacket(TcpPacket.CreatePacket("editevo", evoNum.ToString()));
        }

        public static void SendSaveEvo(int evoNum, Evolutions.Evolution evo, int maxBranches) {
            TcpPacket packet = new TcpPacket("saveevo");

            packet.AppendParameters(evoNum.ToString(), evo.Name, evo.Species.ToString(), maxBranches.ToString());

            for (int i = 0; i < maxBranches; i++) {
                if (i >= evo.Branches.Count) {
                    evo.Branches.Add(new Evolutions.EvolutionBranch());
                }
                packet.AppendParameters(evo.Branches[i].Name,
                                        evo.Branches[i].NewSpecies.ToString(),
                                        evo.Branches[i].ReqScript.ToString(),
                                        evo.Branches[i].Data1.ToString(),
                                        evo.Branches[i].Data2.ToString(),
                                        evo.Branches[i].Data3.ToString());
            }

            packet.FinalizePacket();

            SendPacket(packet);
        }

        //Npcs
        //Shops
        public static void SendEditShop(int shopNum) {

            SendPacket(TcpPacket.CreatePacket("editshop", shopNum.ToString()));
        }

        public static void SendSaveShop(int shopNum, Shops.Shop shop) {
            TcpPacket packet = new TcpPacket("saveshop");
            packet.AppendParameters(
                shopNum.ToString(), shop.Name, shop.JoinSay, shop.LeaveSay);
            for (int i = 0; i < MaxInfo.MAX_TRADES; i++) {
                packet.AppendParameters(shop.Items[i].GiveItem, shop.Items[i].GiveValue, shop.Items[i].GetItem);
            }
            packet.FinalizePacket();

            SendPacket(packet);
        }
        //Moves

        public static void SendEditMove(int moveNum) {
            SendPacket(TcpPacket.CreatePacket("editmove", moveNum.ToString()));
        }

        public static void SendSaveMove(int moveNum, string[] move) {
            TcpPacket packet = new TcpPacket("savemove");

            packet.AppendParameter(moveNum.ToString());

            foreach (string i in move) {

                packet.AppendParameter(i);
            }

            packet.FinalizePacket();

            SendPacket(packet);

        }

        //Missions
        public static void SendEditMission(int missionRank) {
            SendPacket(TcpPacket.CreatePacket("editmission", missionRank.ToString()));
        }

        public static void SendSaveMission(int missionRank, Editors.Missions.EditableMissionPool missionPool) {
            
            
            
            TcpPacket packet = new TcpPacket("savemission");

            packet.AppendParameter(missionRank.ToString());

            packet.AppendParameter(missionPool.Clients.Count);
            foreach (Logic.Editors.Missions.EditableMissionClient missionClient in missionPool.Clients) {

                packet.AppendParameters(missionClient.DexNum, missionClient.FormNum);
            }

            packet.AppendParameter(missionPool.Enemies.Count);
            for (int i = 0; i < missionPool.Enemies.Count; i++) {
                packet.AppendParameters(missionPool.Enemies[i]);
            }

            packet.AppendParameter(missionPool.Rewards.Count);
            foreach (Logic.Editors.Missions.EditableMissionReward missionReward in missionPool.Rewards) {

                packet.AppendParameters(missionReward.ItemNum, missionReward.ItemAmount);
                packet.AppendParameters(missionReward.ItemTag);
            }

            packet.FinalizePacket();

            SendPacket(packet);
        }


        public static void SendEditDungeon(int dungeonNum) {
            SendPacket(TcpPacket.CreatePacket("editdungeon", dungeonNum.ToString()));
        }

        public static void SendSaveDungeon(int dungeonNum, Logic.Editors.Dungeons.EditableDungeon dungeonToSend) {
            TcpPacket packet = new TcpPacket("savedungeon");

            packet.AppendParameter(dungeonNum);
            packet.AppendParameters(dungeonToSend.Name, dungeonToSend.AllowsRescue.ToIntString());

            packet.AppendParameter(dungeonToSend.ScriptList.Count);
            for (int i = 0; i < dungeonToSend.ScriptList.Count; i++) {
                packet.AppendParameters(
                    dungeonToSend.ScriptList.KeyByIndex(i).ToString(),
                    dungeonToSend.ScriptList.ValueByIndex(i));
            }

            packet.AppendParameter(dungeonToSend.StandardMaps.Count);
            for (int i = 0; i < dungeonToSend.StandardMaps.Count; i++) {
                packet.AppendParameters(
                    ((int)dungeonToSend.StandardMaps[i].Difficulty).ToString(),
                    dungeonToSend.StandardMaps[i].IsBadGoalMap.ToIntString(),
                    dungeonToSend.StandardMaps[i].MapNum.ToString());
            }

            packet.AppendParameter(dungeonToSend.RandomMaps.Count);
            for (int i = 0; i < dungeonToSend.RandomMaps.Count; i++)
            {
                packet.AppendParameters(
                    ((int)dungeonToSend.RandomMaps[i].Difficulty).ToString(),
                    dungeonToSend.RandomMaps[i].IsBadGoalMap.ToIntString(),
                    dungeonToSend.RandomMaps[i].RDungeonIndex.ToString(),
                    dungeonToSend.RandomMaps[i].RDungeonFloor.ToString());
            }

            packet.FinalizePacket();
            SendPacket(packet);
        }

        public static void SendAddDungeon() {
            SendPacket(TcpPacket.CreatePacket("addnewdungeon"));
        }


        //Arrows
        public static void SendEditArrow(int itemNum) {

            SendPacket(TcpPacket.CreatePacket("editarrow", itemNum.ToString()));
        }
        public static void SendSaveArrow(int itemNum, Arrows.Arrow arrow) {
            TcpPacket packet = new TcpPacket("savearrow");

            packet.AppendParameters(
                itemNum.ToString(),
                arrow.Name,
                arrow.Pic.ToString(),
                arrow.Range.ToString(),
                arrow.Amount.ToString()
                );

            packet.FinalizePacket();

            SendPacket(packet);
        }
        //Emotions
        public static void SendEditEmotion(int itemNum) {

            SendPacket(TcpPacket.CreatePacket("editemoticon", itemNum.ToString()));
        }
        public static void SendSaveEmotion(int itemNum, Emotions.Emotion emotion) {
            TcpPacket packet = new TcpPacket("saveemoticon");

            packet.AppendParameters(
                itemNum.ToString(),
                emotion.Command,
                emotion.Pic.ToString()
                );

            packet.FinalizePacket();

            SendPacket(packet);
        }
        // NPCs
        public static void SendEditNpc(int npcNum) {


            SendPacket(TcpPacket.CreatePacket("editnpc", npcNum.ToString()));
        }

        public static void SendSaveNpc(int npcNum, Logic.Editors.NPCs.EditableNPC npc) {
            TcpPacket packet = new TcpPacket("savenpc");

            packet.AppendParameter(npcNum);

            packet.AppendParameters(
                npc.Name,
                npc.AttackSay,
                npc.Form.ToString(),
                npc.Species.ToString(),
                npc.ShinyChance.ToString(),
                ((int)npc.Behavior).ToString(),
                npc.RecruitRate.ToString(),
                npc.AIScript,
                npc.SpawnsAtDawn.ToIntString(),
                npc.SpawnsAtDay.ToIntString(),
                npc.SpawnsAtDusk.ToIntString(),
                npc.SpawnsAtNight.ToIntString()
            );

            for (int i = 0; i < npc.Moves.Length; i++) {
                packet.AppendParameter(npc.Moves[i]);
            }
            for (int i = 0; i < npc.Drops.Length; i++) {
                packet.AppendParameters(
                    npc.Drops[i].ItemNum.ToString(),
                    npc.Drops[i].ItemValue.ToString(),
                    npc.Drops[i].Chance.ToString(),
                    npc.Drops[i].Tag
                    );
            }

            packet.FinalizePacket();

            SendPacket(packet);
        }
        //Scripts

        // Stories
        public static void SendEditStory(int storyNum) {
            SendPacket(TcpPacket.CreatePacket("editstory", storyNum.ToString()));
        }

        #endregion Editors

        #region Live Map Editor

        public static void SendTilePlacedData(int startX, int startY, int endX, int endY, Enums.LayerType layer, int layerSet, int layerTile) {
            
            for (int i = startX; i <= endX; i++) {
                for (int j = startY; j <= endY; j++) {
                    SendTilePlacedData(i, j, layer, layerSet, layerTile);
                }
            }
        }

        public static void SendTilePlacedData(int x, int y, Enums.LayerType layer, int layerSet, int layerTile) {
            SendPacket(TcpPacket.CreatePacket("mapeditortileplaced", x.ToString(), y.ToString(),
                ((int)layer).ToString(), layerSet.ToString(), layerTile.ToString()));
        }

        public static void SendAttributePlacedData(int x, int y, Enums.TileType tileType, int data1, int data2, int data3,
            string string1, string string2, string string3, int dungeonValue) {
            SendPacket(TcpPacket.CreatePacket("mapeditorattribplaced", x.ToString(), y.ToString(),
                ((int)tileType).ToString(), data1.ToString(), data2.ToString(), data3.ToString(),
                string1, string2, string3, dungeonValue.ToString()));
        }

        public static void SendExitMapEditor() {
            SendPacket(TcpPacket.CreatePacket("exitmapeditor"));
        }

        public static void SendLayerFillData(Enums.LayerType layer, int layerSet, int layerTile) {
            SendPacket(TcpPacket.CreatePacket("mapeditorfilllayer", ((int)layer).ToString(), layerSet.ToString(),
                layerTile.ToString()));
        }

        public static void SendAttributeFillData(Enums.TileType tileType, int data1, int data2, int data3,
            string string1, string string2, string string3, int dungeonValue) {
            SendPacket(TcpPacket.CreatePacket("mapeditorfillattribute",
               ((int)tileType).ToString(), data1.ToString(), data2.ToString(), data3.ToString(),
               string1, string2, string3, dungeonValue.ToString()));
        }


        #endregion

        #region OnlineList

        public static void PlayersOnline() {
            SendPacket(TcpPacket.CreatePacket("whosonline"));
        }

        public static void OnlineList() {
            SendPacket(TcpPacket.CreatePacket("onlinelist"));
        }

        public static void PlayerInfoRequest(string name) {
            SendPacket(TcpPacket.CreatePacket("playerinforequest", name));
        }

        public static void GetStats() {
            SendPacket(TcpPacket.CreatePacket("getstats"));
        }

        #endregion OnlineList

        #region Admin Commands

        public static void SetAccess(string name, int access) {
            SendPacket(TcpPacket.CreatePacket("setaccess", name, access.ToString()));
        }

        public static void SetMotd(string motd) {
            SendPacket(TcpPacket.CreatePacket("setmotd", motd));
        }

        public static void RestartServer() {
            SendPacket(TcpPacket.CreatePacket("restartserver"));
        }

        public static void ServerUpdateCheck() {
            SendPacket(TcpPacket.CreatePacket("serverupdatecheck"));
        }

        public static void SetSprite(int spriteNum) {
            SendPacket(TcpPacket.CreatePacket("setsprite", spriteNum.ToString()));
        }

        public static void SetPlayerSprite(string player, int spriteNum) {
            SendPacket(TcpPacket.CreatePacket("setplayersprite", player, spriteNum.ToString()));
        }

        public static void MapRespawn() {
            SendPacket(TcpPacket.CreatePacket("maprespawn"));
        }

        public static void KickPlayer(string player) {
            SendPacket(TcpPacket.CreatePacket("kickplayer", player));
        }

        public static void BanPlayer(string player) {
            SendPacket(TcpPacket.CreatePacket("banplayer", player));
        }

        public static void BanList() {
            SendPacket(TcpPacket.CreatePacket("banlist"));
        }

        public static void ClearOwner() {
            SendPacket(TcpPacket.CreatePacket("clearowner"));
        }

        public static void DestroyBanList() {
            SendPacket(TcpPacket.CreatePacket("banlistdestroy"));
        }


        public static void WarpTo(int map) {
            SendPacket(TcpPacket.CreatePacket("warpto", map.ToString()));
        }

        public static void WarpToMe(string player) {
            SendPacket(TcpPacket.CreatePacket("warptome", player));
        }

        public static void WarpMeTo(string player) {
            SendPacket(TcpPacket.CreatePacket("warpmeto", player));
        }

        public static void WarpLoc(int x, int y) {
            SendPacket(TcpPacket.CreatePacket("warploc", x.ToString(), y.ToString()));
        }

        public static void ArrowHit(int n, int z, int x, int y) {
            SendPacket(TcpPacket.CreatePacket("arrowhit", n.ToString(), z.ToString(), x.ToString(), y.ToString()));
        }

        public static void SavePlayer() {
            SendPacket(TcpPacket.CreatePacket("saveplayer"));
        }

        public static void Solid() {
            SendPacket(TcpPacket.CreatePacket("solid"));
        }

        public static void Weather(int weatherNum) {
            SendPacket(TcpPacket.CreatePacket("weather", weatherNum.ToString()));
        }

        public static void MapReportRequest() {
            SendPacket(TcpPacket.CreatePacket("mapreportrequest"));
        }

        #endregion Admin Commands

        #region Friends List

        public static void RequestFriendsList() {
            SendPacket(TcpPacket.CreatePacket("sendfriendslist"));
        }

        public static void AddFriend(string name) {
            SendPacket(TcpPacket.CreatePacket("addfriend", name));
        }

        public static void RemoveFriend(string name) {
            SendPacket(TcpPacket.CreatePacket("removefriend", name));
        }

        #endregion Friends List

        #region Guild

        //Guild Change Access ~obsolete?

        public static void GuildDisown(int index) {
            SendPacket(TcpPacket.CreatePacket("guilddisown", index));
        }

        public static void MakeGuild(string guild) {
            SendPacket(TcpPacket.CreatePacket("makeguild", guild));
        }

        public static void MakeGuildMember(string player) {
            SendPacket(TcpPacket.CreatePacket("guildmember", player));
        }

        public static void GuildPromote(int index) {
            SendPacket(TcpPacket.CreatePacket("guildpromote", index));
        }

        public static void GuildStepDown() {
            SendPacket(TcpPacket.CreatePacket("guildleave"));
        }

        public static void GuildDemote(int index) {
            SendPacket(TcpPacket.CreatePacket("guilddemote", index));
        }

        #endregion Guild

        #region Items

        public static void SendUseItem(int itemSlot) {
            SendPacket(TcpPacket.CreatePacket("useitem", itemSlot.ToString()));
        }

        public static void SendThrowItem(int itemSlot) {
            SendPacket(TcpPacket.CreatePacket("throwitem", itemSlot.ToString()));
        }

        public static void SendHoldItem(int itemSlot) {
            SendPacket(TcpPacket.CreatePacket("holditem", itemSlot.ToString()));
        }

        public static void SendRemoveItem(int itemSlot) {
            SendPacket(TcpPacket.CreatePacket("removeitem", itemSlot.ToString()));
        }

        public static void SendSwapInventoryItems(int oldInvSlot, int newInvSlot) {
            SendPacket(TcpPacket.CreatePacket("swapinvitems", oldInvSlot, newInvSlot));
        }

        #endregion Items

        #region Shops

        public static void RequestShop() {

            SendPacket(TcpPacket.CreatePacket("shoprequest"));
        }

        public static void LeaveShop() {

            SendPacket(TcpPacket.CreatePacket("shopleave"));
        }

        public static void TradeRequest(int amount, int itemSlot) {
            SendPacket(TcpPacket.CreatePacket("traderequest", amount.ToString(), itemSlot.ToString()));
        }

        public static void SellItem(int amount, int itemNum) {
            SendPacket(TcpPacket.CreatePacket("sellitem", amount.ToString(), itemNum.ToString()));
        }

        public static void SendRecallMove(int move) {
            SendPacket(TcpPacket.CreatePacket("moverecall", move.ToString()));
        }




        #endregion Shops

        #region Bank

        public static void BankDeposit(int slot, int amount) {
            SendPacket(TcpPacket.CreatePacket("bankdeposit", slot.ToString(), amount.ToString()));
        }

        public static void BankWithdraw(int slot, int amount) {
            SendPacket(TcpPacket.CreatePacket("bankwithdraw", slot.ToString(), amount.ToString()));
        }

        public static void BankWithdrawMenu() {
            SendPacket(TcpPacket.CreatePacket("bankwithdrawmenu"));
        }

        #endregion Bank

        //Custom Menus

        #region Housing

        public static void SendHouseVisitRequest(string ownerName) {
            SendPacket(TcpPacket.CreatePacket("housevisitrequest", ownerName));
        }

        public static void SendWeatherRequest(int weather) {
            SendPacket(TcpPacket.CreatePacket("weatherrequest", weather));
        }

        public static void SendDarknessRequest(int darkness) {
            SendPacket(TcpPacket.CreatePacket("darknessrequest", darkness));
        }

        public static void SendExpansionRequest(int maxX, int maxY) {
            SendPacket(TcpPacket.CreatePacket("expansionrequest", maxX, maxY));
        }

        public static void SendAddShopRequest(int price) {
            SendPacket(TcpPacket.CreatePacket("addshoprequest", price));
        }

        public static void SendAddSoundRequest(string text1) {
            SendPacket(TcpPacket.CreatePacket("addsoundrequest", text1));
        }

        public static void SendAddNoticeRequest(string text1, string text2, string text3) {
            SendPacket(TcpPacket.CreatePacket("addnoticerequest", text1, text2, text3));
        }

        public static void SendAddSignRequest(string text1, string text2, string text3) {
            SendPacket(TcpPacket.CreatePacket("addsignrequest", text1, text2, text3));
        }

        #endregion Housing

        #region Tournament

        public static void SendJoinTournament(string tournamentID) {
            SendPacket(TcpPacket.CreatePacket("jointournament", tournamentID));
        }

        public static void SendSpectateTournament(string tournamentID) {
            SendPacket(TcpPacket.CreatePacket("spectatetournament", tournamentID));
        }

        public static void SendSaveTournamentRules(Tournaments.TournamentRules rules) {
            TcpPacket packet = new TcpPacket("savetournamentrules");

            packet.AppendParameters(
                rules.SleepClause.ToIntString(),
                rules.AccuracyClause.ToIntString(),
                rules.SpeciesClause.ToIntString(),
                rules.FreezeClause.ToIntString(),
                rules.OHKOClause.ToIntString(),
                rules.SelfKOClause.ToIntString());

            SendPacket(packet);
        }

        public static void SendViewTournamentRules(string tournamentID) {
            SendPacket(TcpPacket.CreatePacket("viewtournamentrules", tournamentID));
        }

        #endregion

        public static void SendPacket(IPacket packet) {
            NetworkManager.SendData(packet);
        }

        public static void SendPacket(IPacket packet, bool compress, bool encrypt) {
            NetworkManager.SendData(packet, compress, encrypt);
        }

        #endregion Methods
    }
}