namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class GoToSegmentSegment : ISegment
    {
        #region Fields

        private ListPair<string, string> parameters;
        private int segment;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public GoToSegmentSegment(int segment) {
            Load(segment);
        }

        public GoToSegmentSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.GoToSegment; }
        }

        public int Segment {
            get { return segment; }
            set { segment = value; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(int segment) {
            this.segment = segment;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("Segment").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            this.storyState.CurrentSegment = segment - 1;
        }

        #endregion Methods
    }
}