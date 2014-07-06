namespace Client.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class BytePacker
    {
        #region Fields

        List<BytePackerItem> items;

        #endregion Fields

        #region Constructors

        public BytePacker() {
            items = new List<BytePackerItem>();
        }

        #endregion Constructors

        #region Properties

        public List<BytePackerItem> Items {
            get { return items; }
        }

        #endregion Properties

        #region Methods

        public void AddItem(int highestRangeValue, int value) {
            items.Add(new BytePackerItem(highestRangeValue, value));
        }

        public int PackItems() {
            int baseNumber = 1;
            int packedNumber = 0;
            for (int i = 0; i < items.Count; i++) {
                packedNumber += items[i].Value * baseNumber;
                baseNumber *= items[i].HighestRangeValue;
            }
            return packedNumber;
        }

        #endregion Methods
    }
}