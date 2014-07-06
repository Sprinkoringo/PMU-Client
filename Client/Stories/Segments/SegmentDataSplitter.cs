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
