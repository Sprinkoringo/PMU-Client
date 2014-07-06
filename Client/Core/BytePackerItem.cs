namespace Client.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BytePackerItem
    {
        #region Constructors

        public BytePackerItem(int highestRangeValue, int value) {
            this.HighestRangeValue = highestRangeValue;
            this.Value = value;
        }

        #endregion Constructors

        #region Properties

        public int HighestRangeValue {
            get;
            set;
        }

        public int Value {
            get;
            set;
        }

        #endregion Properties
    }
}