namespace Client.Logic.Npc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Npc
    {
        #region Constructors

        public Npc()
        {
            ItemDrops = new NpcDrop[MaxInfo.MAX_NPC_DROPS];
        }

        #endregion Constructors

        #region Properties

        public string AIScript
        {
            get;
            set;
        }

        public string AttackSay
        {
            get;
            set;
        }

        public Enums.NpcBehavior Behavior
        {
            get;
            set;
        }

        public NpcDrop[] ItemDrops
        {
            get;
            set;
        }

        public int Species { get; set; }
        public bool SpawnsAtDay { get; set; }
        public bool SpawnsAtNight { get; set; }
        public bool SpawnsAtDawn { get; set; }
        public bool SpawnsAtDusk { get; set; }


        public int[] Moves { get; set; }

        public string Name
        {
            get;
            set;
        }

        public int ShinyChance
        {
            get;
            set;
        }

        public int Size
        {
            get;
            set;
        }

        public int Form
        {
            get;
            set;
        }

        #endregion Properties
    }
}