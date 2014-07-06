using System;
    using System.Collections.Generic;
    using System.Text;

namespace Client.Logic.RDungeons
{
	/// <summary>
	/// Description of RDungeonCollection.
	/// </summary>
	class RDungeonCollection
	{
		#region Fields

        private PMU.Core.ListPair<int, RDungeon> mRDungeons;

        #endregion Fields

        #region Constructors

        internal RDungeonCollection()
        {
            mRDungeons = new PMU.Core.ListPair<int, RDungeon>();
        }

        #endregion Constructors

        #region Indexers

        public RDungeon this[int index]
        {
            get { return mRDungeons[index]; }
            set { mRDungeons[index] = value; }
        }

        #endregion Indexers

        #region Methods

        public void AddRDungeon(int index, RDungeon RDungeonToAdd)
        {
            mRDungeons.Add(index, RDungeonToAdd);
        }

        public void ClearRDungeons()
        {
            mRDungeons.Clear();
        }

        #endregion Methods
	}
}
