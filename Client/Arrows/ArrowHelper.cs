namespace Client.Logic.Arrows
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

    class ArrowHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static ArrowCollection mArrows;

        #endregion Fields

        #region Properties

        public static ArrowCollection Arrows
        {
            get { return mArrows; }
        }

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        #endregion Properties

        #region Methods

        public static void InitArrowCollection()
        {
            mArrows = new ArrowCollection(MaxInfo.MAX_ARROWS+2);
        }

        public static void LoadArrowsFromPacket(string[] parse)
        {
            try {
                int n = 1;
                for (int i = 0; i < MaxInfo.MAX_ARROWS; i++) {
                    dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MAX_ARROWS));
                    mArrows[i] = new Arrow();
                    mArrows[i].Name = parse[n + 1];
                    mArrows[i].Pic = parse[n + 2].ToInt();
                    mArrows[i].Range = parse[n + 3].ToInt();
                    mArrows[i].Amount = parse[n + 4].ToInt();
                    n += 5;
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