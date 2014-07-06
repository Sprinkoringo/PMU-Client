namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class PauseSegment : ISegment
    {
        #region Fields

        private int length;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public PauseSegment(int length) {
            Load(length);
        }

        public PauseSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.Pause; }
        }

        public int Length {
            get { return length; }
            set { length = value; }
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

        public void Load(int length) {
            this.length = length;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.length = parameters.GetValue("Length").ToInt(0);
        }

        public void Process(StoryState state) {
            state.Pause(length);
        }

        #endregion Methods
    }
}