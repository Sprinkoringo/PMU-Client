namespace Client.Logic.Missions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

    class MissionHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static MissionCollection mMissions;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static MissionCollection Missions
        {
            get { return mMissions; }
        }

        #endregion Properties

        #region Methods

        public static void InitMissionCollection()
        {
            mMissions = new MissionCollection();
        }

        public static void LoadMissionsFromPacket(string[] parse)
        {
            try {
                int n = 2;
                int max = parse[1].ToInt();
                mMissions.ClearMissions();
                if (max > 0) {
                    for (int i = 0; i < max; i++) {
                        dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, max));
                        mMissions.AddMission(i, new Mission());
                        mMissions[i].Title = parse[n];
                        n += 1;
                        ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
                    }
                    dataLoadPercent = 100;
                }
            } catch (Exception ex) {
                Exceptions.ExceptionHandler.OnException(ex);
            }
        }

        #endregion Methods
    }
}