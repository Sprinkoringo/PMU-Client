namespace Client.Logic.Shops
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class ShopHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static ShopCollection mShops;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static ShopCollection Shops
        {
            get { return mShops; }
        }

        #endregion Properties

        #region Methods

        public static void InitShopCollection()
        {
            mShops = new ShopCollection(MaxInfo.MaxShops + 1);
        }

        public static void LoadShopsFromPacket(string[] parse)
        {
            try {
                int n = 1;
                for (int i = 1; i <= MaxInfo.MaxShops; i++) {
                    dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxShops));
                    mShops[i] = new Shop();
                    mShops[i].Name = parse[n + 1];
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