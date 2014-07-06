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


namespace Client.Logic.Npc
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class NpcHelper
    {
        #region Fields

        private static int dataLoadPercent;
        private static NpcCollection mNpcs;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static NpcCollection Npcs
        {
            get { return mNpcs; }
        }

        #endregion Properties

        #region Methods

        public static void InitNpcArray()
        {
            mNpcs = new NpcCollection(MaxInfo.MaxNpcs);
        }

        public static void LoadNpcsFromPacket(string[] parse)
        {
            try {
                int n, temp;
                n = 1;
                for (int i = 1; i <= MaxInfo.MaxNpcs; i++) {
                    temp = Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxNpcs+1);
                    if (temp >= 100) {
                        temp = 99;
                    }
                    dataLoadPercent = temp;

                    mNpcs.AddNpc(parse[n + 1]);

                    n += 2;

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