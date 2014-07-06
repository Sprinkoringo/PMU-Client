namespace Client.Logic.Emotions
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

    class EmotionHelper
    {
        #region Fields

        private static int dataLoadPercent = 0;
        private static EmotionCollection mEmotions;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static EmotionCollection Emotions
        {
            get { return mEmotions; }
        }

        #endregion Properties

        #region Methods

        public static void InitEmotionCollection()
        {
            mEmotions = new EmotionCollection(MaxInfo.MaxEmoticons);
        }

        public static void LoadEmotionsFromPacket(string[] parse)
        {
            try {
                int n;
                n = 1;
                for (int i = 0; i < MaxInfo.MaxEmoticons; i++) {
                    dataLoadPercent = System.Math.Min(Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxEmoticons), 99);
                    mEmotions[i] = new Emotion();
                    mEmotions[i].Command = parse[n + 1];
                    mEmotions[i].Pic = parse[n + 2].ToInt();
                    n += 3;
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