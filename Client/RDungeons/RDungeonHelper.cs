using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

namespace Client.Logic.RDungeons
{
	
	class RDungeonHelper
	{
		#region Fields

        private static int dataLoadPercent = 0;
        private static RDungeonCollection mRDungeons;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static RDungeonCollection RDungeons
        {
            get { return mRDungeons; }
        }

        #endregion Properties

        #region Methods

        public static void InitRDungeonCollection()
        {
            mRDungeons = new RDungeonCollection();
        }

        public static void LoadRDungeonsFromPacket(string[] parse)
        {
            try {
                int n = 2;
                MaxInfo.MaxRDungeons = parse[1].ToInt();
                mRDungeons.ClearRDungeons();
                if (MaxInfo.MaxRDungeons > 0) {
                    for (int i = 0; i < MaxInfo.MaxRDungeons; i++) {
                        dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxRDungeons));
                        mRDungeons.AddRDungeon(i, new RDungeon());
                        mRDungeons[i].Name = parse[n];
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
