namespace Client.Logic.Shops
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Shop
    {
        #region Constructors

        public Shop()
        {
            Items = new ShopItem[MaxInfo.MAX_TRADES];
            for (int i = 0; i < MaxInfo.MAX_TRADES; i++) {
            	Items[i] = new ShopItem();
            }
        }

        #endregion Constructors

        #region Properties

        public ShopItem[] Items
        {
            get; set;
        }

        public bool FixesItems
        {
            get; set;
        }

        public string JoinSay
        {
            get; set;
        }

        public string LeaveSay
        {
            get; set;
        }

        public string Name
        {
            get; set;
        }

        #endregion Properties
    }
}