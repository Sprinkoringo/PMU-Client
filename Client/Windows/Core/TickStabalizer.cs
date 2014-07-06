namespace Client.Logic.Windows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class TickStabalizer
    {
        #region Fields

        private static float lastVal = 0;

        #endregion Fields

        #region Methods

        public static bool CanUpdate(SdlDotNet.Core.TickEventArgs e)
        {
            float val = 1.0f / 1000 * e.TicksElapsed * Constants.FRAME_RATE;
            val += lastVal;
            if (val > 1) {
                lastVal = 0;
                return true;
            } else {
                lastVal = val;
                return false;
            }
        }

        #endregion Methods
    }
}