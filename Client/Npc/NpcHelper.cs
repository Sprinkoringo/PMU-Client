namespace Client.Logic.Npc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class NpcHelper
    {
        #region Fields

        private static int dataLoadPercent;
        private static NpcCollection mNpcs;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static NpcCollection Npcs
        {
            get { return mNpcs; }
        }

        #endregion Properties

        #region Methods

        public static void InitNpcArray()
        {
            mNpcs = new NpcCollection(MaxInfo.MaxNpcs);
        }

        public static void LoadNpcsFromPacket(string[] parse)
        {
            try {
                int n, temp;
                n = 1;
                for (int i = 1; i <= MaxInfo.MaxNpcs; i++) {
                    temp = Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxNpcs+1);
                    if (temp >= 100) {
                        temp = 99;
                    }
                    dataLoadPercent = temp;

                    mNpcs.AddNpc(parse[n + 1]);

                    n += 2;

                    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
                }
                dataLoadPercent = 100;
            } catch (Exception ex) {
                Exceptions.ExceptionHandler.OnException(ex);
            }
        }

        #endregion Methods
    }
}