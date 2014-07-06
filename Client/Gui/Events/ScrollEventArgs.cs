namespace Client.Logic.Gui.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class ScrollEventArgs : EventArgs
    {
        #region Fields

        private int mNewVal = 0;

        #endregion Fields

        #region Constructors

        internal ScrollEventArgs(int newVal)
        {
            mNewVal = newVal;
        }

        #endregion Constructors

        #region Properties

        public int NewValue
        {
            get { return mNewVal; }
        }

        #endregion Properties
    }
}