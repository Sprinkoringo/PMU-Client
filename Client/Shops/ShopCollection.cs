namespace Client.Logic.Shops
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class ShopCollection
    {
        #region Fields

        private Shop[] mShops;

        #endregion Fields

        #region Constructors

        internal ShopCollection(int maxShops)
        {
            mShops = new Shop[maxShops];
        }

        #endregion Constructors

        #region Indexers

        public Shop this[int index]
        {
            get { return mShops[index]; }
            set { mShops[index] = value; }
        }

        #endregion Indexers
    }
}