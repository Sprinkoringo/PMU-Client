using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Stories.Segments
{
    class StopMusicSegment : ISegment
    {
        #region Fields

        private Enums.PadlockState state;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public StopMusicSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.StopMusic; }
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

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
        }

        public void Process(StoryState state) {
            state.ResetWaitEvent();
            Music.Music.AudioPlayer.StopMusic();
        }

        #endregion Methods
    }
}
