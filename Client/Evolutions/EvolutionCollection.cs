namespace Client.Logic.Evolutions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class EvolutionCollection
    {
        #region Fields

        private Evolution[] mEvos;

        #endregion Fields

        #region Constructors

        internal EvolutionCollection(int maxEvos)
        {
            mEvos = new Evolution[maxEvos];
        }

        #endregion Constructors

        #region Indexers

        public Evolution this[int index]
        {
            get { return mEvos[index]; }
            set { mEvos[index] = value; }
        }

        #endregion Indexers
    }
}