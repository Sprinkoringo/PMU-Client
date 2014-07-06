namespace Client.Logic.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MoveCollection
    {
        #region Fields

        private Move[] mMoves;

        #endregion Fields

        #region Constructors

        internal MoveCollection(int maxMoves)
        {
            mMoves = new Move[maxMoves + 1];
        }

        #endregion Constructors

        #region Indexers

        public Move this[int index]
        {
            get { return mMoves[index]; }
            set { mMoves[index] = value; }
        }

        #endregion Indexers
    }
}