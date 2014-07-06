namespace Client.Logic.Players
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Drawing;
    using Client.Logic.Missions;

    //class Player : Logic.Graphics.Renderers.Sprites.ISprite
    //{
    //    #region Fields

    //    private int mArmorSlot = -1;
    //    private int mHelmetSlot = -1;
    //    private int mIndex = -1;
    //    private int mLegsSlot = -1;
    //    private int mNecklaceSlot = -1;
    //    private int mRingSlot = -1;
    //    private int mShieldSlot = -1;
    //    private int mWeaponSlot = -1;
    //    int activeTeamNum;
    //    Inventory inventory;
    //    JobList jobList;

    //    #endregion Fields

    //    #region Constructors

    //    public Player(int index) {
    //        mIndex = index;
    //        inventory = new Inventory(index, MaxInfo.MAX_INV);
    //        jobList = new JobList();
    //        Bank = new InventoryItem[MaxInfo.MAX_BANK];
    //        Arrow = new PlayerArrow[MaxInfo.MAX_PLAYER_ARROWS];
    //        Team = new Recruit[MaxInfo.MAX_ACTIVETEAM];
    //        ActiveTeamNum = 0;
    //        // CustomMenuManager = new CustomMenus.MenuManager();
    //        FriendsList = new List<Friend>();
    //        //            for (int i = 0; i < MaxInfo.MAX_ACTIVETEAM; i++) {
    //        //                Team[i] = new Recruit(index, i);
    //        //            }
    //        Location = new Point();
    //        Offset = new Point();
    //    }

    //    #endregion Constructors

    //    #region Properties

    //    public Enums.Rank Access {
    //        get;
    //        set;
    //    }

    //    public JobList JobList {
    //        get { return jobList; }
    //    }

    //    public MissionGoal ActiveMission {
    //        get;
    //        set;
    //    }

    //    public int ActiveTeamNum {
    //        get { return activeTeamNum; }
    //        set {
    //            activeTeamNum = value;
    //            Windows.WindowSwitcher.GameWindow.ActiveTeam.SetSelected(activeTeamNum);
    //        }
    //    }

    //    public int ArmorSlot {
    //        get { return mArmorSlot; }
    //        set { mArmorSlot = value; }
    //    }

    //    public PlayerArrow[] Arrow {
    //        get;
    //        set;
    //    }

    //    public int AttackTimer {
    //        get;
    //        set;
    //    }

    //    public bool Attacking {
    //        get;
    //        set;
    //    }

    //    public InventoryItem[] Bank {
    //        get;
    //        set;
    //    }

    //    public Enums.Direction Direction {
    //        get;
    //        set;
    //    }

    //    public int EmotionNum {
    //        get;
    //        set;
    //    }

    //    public int EmotionTime {
    //        get;
    //        set;
    //    }

    //    public int EmotionVar {
    //        get;
    //        set;
    //    }

    //    // TODO: Create custom menus
    //    //public CustomMenus.MenuManager CustomMenuManager {get;set;}
    //    public List<Friend> FriendsList {
    //        get;
    //        set;
    //    }

    //    public int GuildAccess {
    //        get;
    //        set;
    //    }

    //    public string GuildName {
    //        get;
    //        set;
    //    }

    //    public int HelmetSlot {
    //        get { return mHelmetSlot; }
    //        set { mHelmetSlot = value; }
    //    }

    //    public int Index {
    //        get { return mIndex; }
    //        set { mIndex = value; }
    //    }

    //    public Inventory Inventory {
    //        get { return inventory; }
    //        set { inventory = value; }
    //    }

    //    public int LegsSlot {
    //        get { return mLegsSlot; }
    //        set { mLegsSlot = value; }
    //    }

    //    public bool LevelUp {
    //        get;
    //        set;
    //    }

    //    public int LevelUpTimer {
    //        get;
    //        set;
    //    }

    //    public string Map {
    //        get;
    //        set;
    //    }

    //    public int MapGetTimer {
    //        get;
    //        set;
    //    }

    //    public Moves.MoveAnimation[] MoveAnim {
    //        get;
    //        set;
    //    }

    //    public int MoveNum {
    //        get;
    //        set;
    //    }

    //    public Enums.MovementSpeed MovementSpeed {
    //        get;
    //        set;
    //    }

    //    public string Name {
    //        get;
    //        set;
    //    }

    //    public int NecklaceSlot {
    //        get { return mNecklaceSlot; }
    //        set { mNecklaceSlot = value; }
    //    }

    //    public bool PK {
    //        get;
    //        set;
    //    }

    //    public int RingSlot {
    //        get { return mRingSlot; }
    //        set { mRingSlot = value; }
    //    }

    //    public int ShieldSlot {
    //        get { return mShieldSlot; }
    //        set { mShieldSlot = value; }
    //    }

    //    public bool Solid {
    //        get;
    //        set;
    //    }

    //    public string Status {
    //        get;
    //        set;
    //    }

    //    public Recruit[] Team {
    //        get;
    //        set;
    //    }

    //    public bool UsedMove {
    //        get;
    //        set;
    //    }

    //    public int WeaponSlot {
    //        get { return mWeaponSlot; }
    //        set { mWeaponSlot = value; }
    //    }

    //    #endregion Properties

    //    #region Methods

    //    public int GetInvItemNum(int invSlot) {
    //        if (invSlot > -1) {
    //            return Inventory[invSlot].Num;
    //        } else {
    //            return 0;
    //        }
    //    }

    //    public int GetInvItemAmount(int invSlot) {
    //        if (invSlot > -1) {
    //            return Inventory[invSlot].Value;
    //        } else {
    //            return 0;
    //        }
    //    }

    //    public bool IsEquiped(int invSlot) {
    //        if (ArmorSlot == invSlot) {
    //            return true;
    //        } else if (HelmetSlot == invSlot) {
    //            return true;
    //        } else if (LegsSlot == invSlot) {
    //            return true;
    //        } else if (NecklaceSlot == invSlot) {
    //            return true;
    //        } else if (RingSlot == invSlot) {
    //            return true;
    //        } else if (ShieldSlot == invSlot) {
    //            return true;
    //        } else if (WeaponSlot == invSlot) {
    //            return true;
    //        } else {
    //            return false;
    //        }
    //    }

    //    //public Recruit GetActiveRecruit() {
    //    //    if (mIndex == PlayerManager.MyIndex) {
    //    //        return Team[ActiveTeamNum];
    //    //    } else {
    //    //        if (Team[0] == null) {
    //    //            Team[0] = new Recruit(mIndex, 0);
    //    //        }
    //    //        return Team[0];
    //    //    }
    //    //}

    //    public void CharSwap(int slot) {//may require timing restrictions?
    //        if (slot == activeTeamNum) {
    //            //(insert nickname) is already in!
    //        } else {
    //            Tcp.Messenger.SendActiveCharSwap(slot);
    //        }
    //    }

    //    public void LeaderSwap(int slot)//may require timing restrictions?
    //    {
    //        if (slot == 0) {
    //            //That pokemon is already leader!
    //        } else {
    //            Tcp.Messenger.SendSwitchLeader(slot);
    //        }
    //    }

    //    public void SendHome(int slot)//may require timing restrictions?
    //    {
    //        if (slot == 0) {
    //            //The leader cannot be sent home!
    //        } else {
    //            Tcp.Messenger.SendRemoveFromTeam(slot);
    //        }
    //    }


    //    #endregion Methods

    //    public Enums.Size Size {
    //        get {
    //            return Enums.Size.Normal;
    //        }
    //        set {
    //            // TODO: Add Size variable to recruits
    //        }
    //    }

    //    public int Sprite {
    //        get {
    //            return GetActiveRecruit().Sprite;
    //        }
    //        set {
    //            GetActiveRecruit().Sprite = value;
    //        }
    //    }

    //    public System.Drawing.Point Offset {
    //        get;
    //        set;
    //    }

    //    public System.Drawing.Point Location {
    //        get;
    //        set;
    //    }

    //    public int X {
    //        get {
    //            return Location.X;
    //        }
    //        set {
    //            Location = new System.Drawing.Point(value, Location.Y);
    //        }
    //    }

    //    public int Y {
    //        get {
    //            return Location.Y;
    //        }
    //        set {
    //            Location = new System.Drawing.Point(Location.X, value);
    //        }
    //    }
    //}
}