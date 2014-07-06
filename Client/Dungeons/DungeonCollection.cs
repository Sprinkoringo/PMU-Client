using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Dungeons
{
    class DungeonCollection
    {
        #region Fields

        private PMU.Core.ListPair<int, Dungeon> mDungeons;

        #endregion Fields

        #region Constructors

        internal DungeonCollection() {
            mDungeons = new PMU.Core.ListPair<int, Dungeon>();
        }

        #endregion Constructors

        #region Indexers

        public Dungeon this[int index] {
            get { return mDungeons[index]; }
            set { mDungeons[index] = value; }
        }

        #endregion Indexers

        #region Methods

        public void AddDungeon(int index, Dungeon RDungeonToAdd) {
            mDungeons.Add(index, RDungeonToAdd);
        }

        public void ClearDungeons() {
            mDungeons.Clear();
        }

        #endregion Methods
    }
}
