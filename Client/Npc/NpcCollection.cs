namespace Client.Logic.Npc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class NpcCollection
    {
        #region Fields

        private List<Npc> mNpcArray;

        #endregion Fields

        #region Constructors

        internal NpcCollection(int maxNpcs)
        {
            mNpcArray = new List<Npc>(maxNpcs);
        }

        #endregion Constructors

        #region Indexers

        public Npc this[int index]
        {
            get { return mNpcArray[index-1]; }
            set { mNpcArray[index-1] = value; }
        }

        #endregion Indexers


        public void AddNpc(string name)
        {
            Npc npc = new Npc();
            npc.Name = name;
            npc.AttackSay = "";
            npc.Behavior = Enums.NpcBehavior.AttackOnSight;
            npc.ShinyChance = 0;
            for (int a = 0; a < MaxInfo.MAX_NPC_DROPS; a++) {
                npc.ItemDrops[a] = new NpcDrop();
                npc.ItemDrops[a].Chance = 0;
                npc.ItemDrops[a].ItemNum = 0;
                npc.ItemDrops[a].ItemValue = 0;
            }

            mNpcArray.Add(npc);
        }
    }
}