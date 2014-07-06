namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MathFunctions
    {
        static Random random = new Random();

        public static Random Random {
            get { return random; }
        }

        #region Methods

        public static int CalculatePercent(int currentValue, int maxValue)
        {
            if (maxValue == 0) {
                return 0;
            }
            return currentValue * 100 / maxValue;
        }
        
        public static ulong CalculatePercent(ulong currentValue, ulong maxValue)
        {
            if (maxValue == 0) {
                return 0;
            }
            return currentValue * 100 / maxValue;
        }

        public static int RoundToMultiple(int number, int multiple)
        {
            double d = number / multiple;
            d = System.Math.Round(d, 0);
            return Convert.ToInt32(d * multiple);
        }

        #endregion Methods
    }
}