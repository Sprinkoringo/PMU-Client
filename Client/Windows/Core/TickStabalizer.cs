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


namespace Client.Logic.Windows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class TickStabalizer
    {
        #region Fields

        private static float lastVal = 0;

        #endregion Fields

        #region Methods

        public static bool CanUpdate(SdlDotNet.Core.TickEventArgs e)
        {
            float val = 1.0f / 1000 * e.TicksElapsed * Constants.FRAME_RATE;
            val += lastVal;
            if (val > 1) {
                lastVal = 0;
                return true;
            } else {
                lastVal = val;
                return false;
            }
        }

        #endregion Methods
    }
}