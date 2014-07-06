namespace Client.Logic.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ByteUnpacker
    {
        #region Fields

        List<int> items;
        List<BytePackerItem> unpackedItems;

        #endregion Fields

        #region Constructors

        public ByteUnpacker() {
            items = new List<int>();
        }

        #endregion Constructors

        #region Properties

        public List<int> Items {
            get { return items; }
        }

        public List<BytePackerItem> UnpackedItems {
            get { return unpackedItems; }
        }

        #endregion Properties

        #region Methods

        public void AddRange(int highestRangeValue) {
            items.Add(highestRangeValue);
        }

        public List<BytePackerItem> UnpackByte(int packedValue) {
            unpackedItems = new List<BytePackerItem>();
            int baseNumber = 1;
            for (int i = 0; i < items.Count; i++) {
                unpackedItems.Add(new BytePackerItem(items[i], (packedValue % (items[i] * baseNumber)) / baseNumber));
                baseNumber *= items[i];
            }
            return unpackedItems;
        }

        #endregion Methods
    }
}