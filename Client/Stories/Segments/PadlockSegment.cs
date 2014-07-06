using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Stories.Segments
{
    class PadlockSegment : ISegment
    {
        #region Fields

        private Enums.PadlockState state;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public PadlockSegment(Enums.PadlockState state) {
            Load(state);
        }

        public PadlockSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.Padlock; }
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
            this.state = (Enums.PadlockState)(parameters.GetValue("State").ToInt(0));
        }

        public void Process(StoryState state) {
            Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("actonaction"));
        }

        #endregion Methods
    }
}
