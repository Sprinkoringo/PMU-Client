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