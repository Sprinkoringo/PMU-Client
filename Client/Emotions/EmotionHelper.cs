/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


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