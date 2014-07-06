namespace Client.Logic.Missions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MissionCollection
    {
        #region Fields

        private PMU.Core.ListPair<int, Mission> mMissions;

        #endregion Fields

        #region Constructors

        internal MissionCollection()
        {
            mMissions = new PMU.Core.ListPair<int, Mission>();
        }

        #endregion Constructors

        #region Indexers

        public Mission this[int index]
        {
            get { return mMissions[index]; }
            set { mMissions[index] = value; }
        }

        #endregion Indexers

        #region Methods

        public void AddMission(int index, Mission missionToAdd)
        {
            mMissions.Add(index, missionToAdd);
        }

        public void ClearMissions()
        {
            mMissions.Clear();
        }

        #endregion Methods
    }
}