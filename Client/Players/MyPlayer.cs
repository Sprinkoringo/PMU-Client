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
using Client.Logic.Missions;
using Client.Logic.Network;
using System.Drawing;
using Client.Logic.Windows;

namespace Client.Logic.Players
{
    class MyPlayer : IPlayer
    {
        public Logic.Graphics.SpriteSheet SpriteSheet {
            get;
            set;
        }

        public int Sprite {
            get;
            set;
        }
        public int Form { get; set; }
        public Enums.Coloration Shiny { get; set; }
        
        public Enums.Sex Sex {
            get {
                return GetActiveRecruit().Sex;
            }
            set {
                GetActiveRecruit().Sex = value;
            }
        }

        

        public PlayerType PlayerType {
            get { return Players.PlayerType.My; }
        }

        public int IdleTimer { get; set; }
        public int IdleFrame { get; set; }
        public int LastWalkTime { get; set; }
        public int WalkingFrame { get; set; }

        public bool SwitchingSeamlessMaps { get; set; }

        public string Name {
            get;
            set;
        }

        public string MapID {
            get;
            set;
        }

        public int X {
            get { return Location.X; }
            set { Location = new Point(value, Location.Y); }
        }

        public int Y {
            get { return Location.Y; }
            set { Location = new Point(Location.X, value); }
        }

        public int TargetX {
            get;
            set;
        }

        public int TargetY {
            get;
            set;
        }

        public int LastMovement { get; set; }
        public Enums.MovementSpeed StoryMovementSpeed { get; set; }
        public Algorithms.Pathfinder.PathfinderResult StoryPathfinderResult { get; set; }

        public Point Location {
            get;
            set;
        }

        public bool Leaving { get; set; }

        public bool ScreenActive { get; set; }

        public int SleepTimer {
            get;
            set;
        }

        public int SleepFrame {
            get;
            set;
        }

        public string Guild {
            get;
            set;
        }

        public Enums.GuildRank GuildAccess {
            get;
            set;
        }

        public string Status {
            get;
            set;
        }

        

        public bool Hunted {
            get;
            set;
        }

        public bool Dead {
            get;
            set;
        }

        public Enums.Rank Access {
            get;
            set;
        }

        public Enums.Direction Direction {
            get;
            set;
        }

        public bool MovementLocked {
            get;
            set;
        }

        public bool Solid {
            get;
            set;
        }

        public string ID {
            get;
            set;
        }

        public int Atk { get; set; }
        public int Def { get; set; }
        public int Spd { get; set; }
        public int SpAtk { get; set; }
        public int SpDef { get; set; }
        public ulong Exp { get; set; }
        public ulong MaxExp { get; set; }
        public int Belly { get; set; }
        public int MaxBelly { get; set; }
        public bool Confused { get; set; }
        public Enums.StatusAilment StatusAilment {
            get {
                return GetActiveRecruit().StatusAilment;
            }
            set {
                GetActiveRecruit().StatusAilment = value;
            }
        }
        public List<int> VolatileStatus {
            get;
            set;
        }

        public Enums.MovementSpeed SpeedLimit { get; set; }
        public Inventory Inventory { get; set; }
        public int Level {
            get {
                return GetActiveRecruit().Level;
            }
            set {
                GetActiveRecruit().Level = value;
            }
        }
        public bool[] Mobility {
            get;
            set;
        }


        public int TimeMultiplier {
            get;
            set;
        }


        public int Darkness {
            get;
            set;
        }

        //public int WeaponSlot { get; set; }
        //public int ArmorSlot { get; set; }
        //public int ShieldSlot { get; set; }
        //public int LegsSlot { get; set; }
        //public int NecklaceSlot { get; set; }
        //public int RingSlot { get; set; }
        //public int HelmetSlot { get; set; }

        int activeTeamNum;

        public int ActiveTeamNum {
            get {
                return activeTeamNum;
            }
            set {
                activeTeamNum = value;
                Windows.WindowSwitcher.GameWindow.ActiveTeam.SetSelected(value);
            }
        }

        public int PauseTimer { get; set; }
        public int AttackTimer { get; set; }
        public int TotalAttackTime { get; set; }
        public bool Attacking { get; set; }

        public int GetTimer { get; set; }
        public int TalkTimer { get; set; }
        public int TempMuteTimer { get; set; }

        public Recruit[] Team { get; set; }

        public RecruitMove[] Moves { get; set; }

        public Enums.MovementSpeed MovementSpeed { get; set; }
        public Point Offset { get; set; }

        private JobList jobList;
        public int MissionExp { get; set; }
        public Enums.ExplorerRank ExplorerRank { get; set; }
        public Parties.PartyData Party { get; set; }

        public List<Friend> FriendsList { get; set; }

        public Maps.DungeonRoom CurrentRoom { get; set; }

        public PlayerPet[] Pets { get; set; }

        public PacketList MovementPacketCache { get; set; }
        public int LastMovementCacheSend { get; set; }

        public MyPlayer() {
            // Initialize the job list
            jobList = new JobList();
            // Initialize the move list
            Moves = new RecruitMove[MaxInfo.MAX_PLAYER_MOVES];
            for (int i = 0; i < Moves.Length; i++) {
                Moves[i] = new RecruitMove();
            }
            // Initialize the player team
            Team = new Recruit[MaxInfo.MAX_ACTIVETEAM];
            Pets = new PlayerPet[MaxInfo.MAX_ACTIVETEAM];
            // Initialize the inventory and bank
            Inventory = new Inventory(MaxInfo.MaxInv);
            FriendsList = new List<Friend>();
            Mobility = new bool[16];
            TimeMultiplier = 1000;
            VolatileStatus = new List<int>();
            MapGoals = new List<MissionGoal>();
            ScreenActive = true;

            TargetX = -1;
            TargetY = -1;
        }

        public List<MissionGoal> MapGoals {
            get;
            set;
        }

        public JobList JobList {
            get { return jobList; }
        }

        public Graphics.Renderers.Sprites.SpeechBubble CurrentSpeech { get; set; }

        public Graphics.Renderers.Sprites.Emoticon CurrentEmote { get; set; }

        public Recruit GetActiveRecruit() {
            return Team[ActiveTeamNum];
        }

        public int GetInvItemNum(int invSlot) {
            if (invSlot > 0) {
                return Inventory[invSlot].Num;
            } else {
                return 0;
            }
        }

        public int GetInvItemAmount(int invSlot) {
            if (invSlot > -1) {
                return Inventory[invSlot].Value;
            } else {
                return 0;
            }
        }

        public bool GetInvItemSticky(int invSlot) {
            if (invSlot > -1) {
                return Inventory[invSlot].Sticky;
            } else {
                return false;
            }
        }

        public bool IsEquiped(int invSlot) {
            for (int i = 0; i < 4; i++) {
                if (Team[i] != null && Team[i].HeldItemSlot == invSlot) {
                    return true;

                }

            }

            return false;
        }

        public void UseMove(int moveSlot) {
            if (Moves[moveSlot].MoveNum > 0  && MovementSpeed != Enums.MovementSpeed.Slip) {

                int attackSpeed = 0;
                //if (GetActiveRecruit().HeldItemSlot > 0) {
                //    attackSpeed = Items.ItemHelper.Items[Players.PlayerManager.MyPlayer.GetInvItemNum(Players.PlayerManager.MyPlayer.GetActiveRecruit().HeldItemSlot)].AttackSpeed;
                //} else {
                attackSpeed = Logic.Moves.MoveHelper.Moves[Moves[moveSlot].MoveNum].HitTime;
                //}

                attackSpeed = attackSpeed * Players.PlayerManager.MyPlayer.TimeMultiplier / 1000;


                if (Players.PlayerManager.MyPlayer.AttackTimer < Globals.Tick && Players.PlayerManager.MyPlayer.Attacking == false) {
                    Messenger.SendUseMove(moveSlot);
                    Attacking = true;
                    AttackTimer = Globals.Tick + attackSpeed;
                    TotalAttackTime = attackSpeed;
                    if (Logic.Moves.MoveHelper.Moves[Moves[moveSlot].MoveNum].HitFreeze) {
                        PauseTimer = Globals.Tick + attackSpeed;
                    }
                    // no more having to cast while standing

                }
            } else {
                ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                if (chat != null) {
                    chat.AppendChat("There is no move here!", Color.Red);
                }
            }
        }

        public void ForgetMove(int moveSlot) {
            //Dim CstSplWalk As String
            //            Dim CstSplWalk2 As String
            if (Moves[moveSlot].MoveNum > 0) {

                Messenger.SendForgetMove(moveSlot);


            } else {
                // TODO: AddText("There is no move here!");
            }
        }

        public void ShiftMove(int moveSlot, bool shiftUp) {
            //Dim CstSplWalk As String
            //            Dim CstSplWalk2 As String
            //if (Moves[moveSlot].MoveNum > 0)
            //{
            if (moveSlot < 1 && shiftUp || moveSlot > 2 && !shiftUp) {
                //tell the player it can't be done in some way (maybe beep at him...)
            } else {
                Messenger.SendShiftMove(moveSlot, shiftUp);
            }

            //}
            //else
            //{
            // shifting empty move slots is allowed
            //}
        }

        public void SwapMoves(int oldMoveSlot, int newMoveSlot) {
            Messenger.SendSwapMoves(oldMoveSlot, newMoveSlot);
        }

        //public Enums.Size Size {
        //    get;
        //    set;
        //}

        public void CharSwap(int slot) {//may require timing restrictions?
            if (slot == ActiveTeamNum) {
                //(insert nickname) is already in!
            } else {
                Messenger.SendActiveCharSwap(slot);
            }
        }

        public void LeaderSwap(int slot)//may require timing restrictions?
        {
            if (slot == 0) {
                //That Pokémon is already leader!
            } else {
                Messenger.SendSwitchLeader(slot);
            }
        }

        public void SendHome(int slot)//may require timing restrictions?
        {
            if (slot == 0) {
                //The leader cannot be sent home!
            } else {
                Messenger.SendRemoveFromTeam(slot);
            }
        }

        public void SetCurrentRoom() {
            if (MapID.StartsWith("rd")) {
                CurrentRoom = GetTargetRoom();

                if (PlayerManager.MyPlayer.CurrentRoom.Width < 3) {
                    if (PlayerManager.MyPlayer.CurrentRoom.Height > 2) {
                        CurrentRoom.Y = Y - 1;
                        CurrentRoom.Height = 2;
                    }
                }
                if (PlayerManager.MyPlayer.CurrentRoom.Height < 3) {
                    if (PlayerManager.MyPlayer.CurrentRoom.Width > 2) {
                        CurrentRoom.X = X - 1;
                        CurrentRoom.Width = 2;
                    }
                }
            } else {
                if (PlayerManager.MyPlayer.CurrentRoom == null) {
                    CurrentRoom = new Maps.DungeonRoom(0, 0, 0, 0);
                }
                CurrentRoom.X = 0;
                CurrentRoom.Y = 0;
                CurrentRoom.Width = Maps.MapHelper.ActiveMap.MaxX;
                CurrentRoom.Height = Maps.MapHelper.ActiveMap.MaxY;
            }
        }

        public Maps.DungeonRoom GetTargetRoom() {
            Maps.Map map = Maps.MapHelper.ActiveMap;
            int targetX = X;
            int targetY = Y;

            int x1 = targetX;
            int y1 = targetY;
            int blockedCount = 0;


            int leftXDistance = -1;
            int rightXDistance = -1;

            int upYDistance = -1;
            int downYDistance = -1;

            int roomStartX = -1;
            int roomWidth = -1;

            int roomStartY = -1;
            int roomHeight = -1;

            while (true) {
                // Keep going left until we've hit a wall...
                x1--;
                blockedCount = 0;
                if (x1 < 0) {
                    roomStartX = 0;
                    break;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY - 1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY + 1)) {
                    blockedCount++;
                }
                if (blockedCount == 3 || blockedCount == 2) {
                    // This means that a hallway was found between blocks!
                    leftXDistance = x1;
                    roomStartX = x1;
                    break;
                }
            }
            x1 = targetX;
            while (true) {
                // Keep going right until we've hit a wall...
                x1++;
                blockedCount = 0;
                if (x1 > map.MaxX) {
                    roomWidth = map.MaxX - roomStartX;
                    break;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY - 1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, x1, targetY + 1)) {
                    blockedCount++;
                }
                if (blockedCount == 3 || blockedCount == 2) {
                    // This means that a hallway was found between blocks!
                    rightXDistance = x1;
                    roomWidth = (x1 - targetX) + (targetX - roomStartX);
                    break;
                }
            }

            while (true) {
                // Keep going up until we've hit a wall...
                y1--;
                blockedCount = 0;
                if (y1 < 0) {
                    roomStartY = 0;
                    break;
                }
                if (GameProcessor.IsBlocked(map, targetX, y1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, targetX - 1, y1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, targetX + 1, y1)) {
                    blockedCount++;
                }
                if (blockedCount == 3 || blockedCount == 2) {
                    // This means that a hallway was found between blocks!
                    upYDistance = y1;
                    roomStartY = y1;
                    break;
                }
            }
            y1 = targetY;
            while (true) {
                // Keep going down until we've hit a wall...
                y1++;
                blockedCount = 0;
                if (y1 > map.MaxY) {
                    roomHeight = map.MaxY - roomStartY;
                    break;
                }
                if (GameProcessor.IsBlocked(map, targetX, y1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, targetX - 1, y1)) {
                    blockedCount++;
                }
                if (GameProcessor.IsBlocked(map, targetX + 1, y1)) {
                    blockedCount++;
                }
                if (blockedCount == 3 || blockedCount == 2) {
                    // This means that a hallway was found between blocks!
                    downYDistance = y1;
                    roomHeight = (y1 - targetY) + (targetY - roomStartY);
                    break;
                }
            }

            return new Maps.DungeonRoom(roomStartX, roomStartY, roomWidth, roomHeight);
        }

    }
}
