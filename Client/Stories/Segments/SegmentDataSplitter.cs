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


using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Stories.Segments
{
    class SegmentDataSplitter
    {
        public const char SEPERATOR_CHAR = '|';

        public static string[] SplitSegmentData(string segmentData) {
            if (segmentData.Contains(SEPERATOR_CHAR.ToString())) {
                List<string> parsed = new List<string>();
                bool startNewLine = true;
                int currentLine = -1;
                bool isInQuotes = false;
                for (int i = 0; i < segmentData.Length; i++) {
                    if (startNewLine) {
                        parsed.Add("");
                        currentLine++;
                        startNewLine = false;
                    }
                    char curChar = segmentData[i];
                    if (curChar == SEPERATOR_CHAR && isInQuotes == false) {
                        startNewLine = true;
                    } else if (curChar == '"') {
                        isInQuotes = !isInQuotes;
                    } else {
                        parsed[currentLine] += curChar;
                    }
                }
                return parsed.ToArray();
            } else {
                return new string[] { segmentData };
            }
        }
    }
}
