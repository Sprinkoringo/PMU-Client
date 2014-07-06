namespace Client.Logic.Emotions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class EmotionCollection
    {
        #region Fields

        private Emotion[] mEmotions;

        #endregion Fields

        #region Constructors

        internal EmotionCollection(int maxEmotions)
        {
            mEmotions = new Emotion[maxEmotions];
        }

        #endregion Constructors

        #region Indexers

        public Emotion this[int index]
        {
            get { return mEmotions[index]; }
            set { mEmotions[index] = value; }
        }

        #endregion Indexers
    }
}