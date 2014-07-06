namespace Client.Logic.Evolutions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class EvolutionHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static EvolutionCollection mEvos;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static EvolutionCollection Evolutions
        {
            get { return mEvos; }
        }

        #endregion Properties

        #region Methods

        public static void InitEvosCollection()
        {
            mEvos = new EvolutionCollection(MaxInfo.MaxEvolutions + 1);
        }

        public static void LoadEvosFromPacket(string[] parse)
        {
            try {
                int n = 1;
                for (int i = 0; i <= MaxInfo.MaxEvolutions; i++) {
                    dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxEvolutions));
                    mEvos[i] = new Evolution();
                    mEvos[i].Name = parse[n + 1];
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