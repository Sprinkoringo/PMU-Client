using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Stories.Segments
{
    class PlayerPadlockSegment : ISegment
    {
        #region Fields

        private Enums.PadlockState state;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public PlayerPadlockSegment(Enums.PadlockState state) {
            Load(state);
        }

        public PlayerPadlockSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.PlayerPadlock; }
        }

        public Enums.PadlockState State {
            get { return state; }
            set { state = value; }
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

        public void Load(Enums.PadlockState state) {
            this.state = state;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.state = (Enums.PadlockState)Enum.Parse(typeof(Enums.PadlockState), parameters.GetValue("MovementState"));
        }

        public void Process(StoryState state) {
            switch (this.state) {
                case Enums.PadlockState.Lock: {
                        Players.PlayerManager.MyPlayer.MovementLocked = true;
                    }
                    break;
                case Enums.PadlockState.Unlock: {
                        Players.PlayerManager.MyPlayer.MovementLocked = false;
                    }
                    break;
            }
        }

        #endregion Methods
    }
}
