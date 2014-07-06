namespace Client.Logic.Arrows
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class ArrowCollection
    {
        #region Fields

        private Arrow[] mArrows;

        #endregion Fields

        #region Constructors

        internal ArrowCollection(int maxArrows)
        {
            mArrows = new Arrow[maxArrows];
        }

        #endregion Constructors

        #region Indexers

        public Arrow this[int index]
        {
            get { return mArrows[index]; }
            set { mArrows[index] = value; }
        }

        #endregion Indexers
    }
}