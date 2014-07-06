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


namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Constants
    {
        #region Fields

        public const string CLIENT_EDITION = "7";
        public const int CLIENT_VERSION = 5;
        public const int FRAME_RATE = 30;
        public const int TILE_WIDTH = 32;
        public const int TILE_HEIGHT = 32;

        //public const string SEC_CODE1 = "12345678";
        //public const string SEC_CODE2 = "fsvsdfvl";
        //public const string SEC_CODE3 = "*&9694ng";
        //public const string SEC_CODE4 = "^78fvd)!";
        public const string SEC_CODE1 = "sdbsiadobascbsac874hfbnerfcwea9cvv9ehrvndcsdicnsducbsucbsdc";
        public const string SEC_CODE2 = "bfmpdgobmdfbdfvndifvsdnfpsaubcdonsicoaskcsmlrjsdicnjsodciwsncsacdassdc";
        public const string SEC_CODE3 = "blahblahblahblahblahblahblahblahblahblahblahhablahblahblahblah";
        public const string SEC_CODE4 = "4782934742639475264072365275462075256035732625421652164126432107460264732642";

        public static readonly int MovementClusteringFrquency = 10;
        
        #endregion Fields
    }
}