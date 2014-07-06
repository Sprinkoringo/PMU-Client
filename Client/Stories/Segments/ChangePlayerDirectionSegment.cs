namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class ChangePlayerDirectionSegment : ISegment
    {
        #region Fields

        StoryState storyState;
        Enums.Direction direction;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public ChangePlayerDirectionSegment(Enums.Direction direction) {
            Load(direction);
        }

        public ChangePlayerDirectionSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ChangePlayerDir; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public Enums.Direction Direction {
            get { return direction; }
            set { direction = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(Enums.Direction direction) {
            this.direction = direction;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load((Enums.Direction)parameters.GetValue("Direction").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            Players.PlayerManager.MyPlayer.Direction = direction;
            Network.Messenger.SendPlayerDir();
        }

        #endregion Methods
    }
}