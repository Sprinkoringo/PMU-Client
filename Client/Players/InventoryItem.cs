namespace Client.Logic.Players
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class InventoryItem
    {
        #region Properties

        public int Num {
            get;
            set;
        }

        public int Value {
            get;
            set;
        }

        public bool Sticky
        {
            get;
            set;
        }

        #endregion Properties
    }
}