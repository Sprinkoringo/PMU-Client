/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


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