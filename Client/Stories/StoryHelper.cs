namespace Client.Logic.Stories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class StoryHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static StoryCollection mStories;
        static Story cachedStory;
        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static StoryCollection Stories
        {
            get { return mStories; }
        }

        public static Story CachedStory {
            get { return cachedStory; }
            set { cachedStory = value; }
        }

        #endregion Properties

        #region Methods

        public static void InitStoryCollection()
        {
            mStories = new StoryCollection(MaxInfo.MaxStories+1);
        }

        public static void LoadStoriesFromPacket(string[] parse)
        {
            try {
                int n = 1;
                for (int i = 0; i <= MaxInfo.MaxStories; i++) {
                    dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxStories));
                    mStories[i] = new Story();
                    mStories[i].Name = parse[n];
                    n += 1;
                    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
                }
                dataLoadPercent = 100;
            } catch (Exception ex) {
                Exceptions.ExceptionHandler.OnException(ex);
            }
        }

        #endregion Methods
    }
}