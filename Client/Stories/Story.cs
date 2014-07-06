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


namespace Client.Logic.Stories
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Story
    {
        #region Constructors

        public Story()
        {
            Segments = new List<ISegment>();
            ExitAndContinue = new List<int>();
        }

        #endregion Constructors

        #region Properties

        public List<int> ExitAndContinue
        {
            get;
            set;
        }

        public bool Loaded
        {
            get;
            set;
        }

        public bool LocalStory
        {
            get;
            set;
        }

        public int MaxSegments
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        public int Revision
        {
            get;
            set;
        }

        public List<ISegment> Segments
        {
            get;
            set;
        }

        public StoryState State
        {
            get;
            set;
        }

        public int StoryStart
        {
            get;
            set;
        }

        public int Version
        {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void AppendSegment(ISegment segment)
        {
            this.Segments.Add(segment);
        }

        #endregion Methods
    }
}