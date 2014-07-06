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