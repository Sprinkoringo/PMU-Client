namespace Client.Logic.Stories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class StoryCollection
    {
        #region Fields

        private Story[] mStories;

        #endregion Fields

        #region Constructors

        internal StoryCollection(int maxStories)
        {
            mStories = new Story[maxStories];
        }

        #endregion Constructors

        #region Indexers

        public Story this[int index]
        {
            get { return mStories[index]; }
            set { mStories[index] = value; }
        }

        #endregion Indexers
    }
}