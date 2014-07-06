namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class MovePlayerSegment : ISegment
    {
        #region Fields

        bool pause;
        StoryState storyState;
        int x;
        int y;
        int speed;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public MovePlayerSegment(int x, int y, int speed, bool pause) {
            Load(x, y, speed, pause);
        }

        public MovePlayerSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.MovePlayer; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public bool Pause {
            get { return pause; }
            set { pause = value; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public int Speed {
            get { return speed; }
            set { speed = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(int x, int y, int speed, bool pause) {
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.pause = pause;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;

            Load(parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt(), parameters.GetValue("Speed").ToInt(), parameters.GetValue("Pause").ToBool());
        }

        public void Process(StoryState state) {
            this.storyState = state;

            Players.MyPlayer myPlayer = Players.PlayerManager.MyPlayer;
            myPlayer.TargetX = x;
            myPlayer.TargetY = y;
            myPlayer.StoryMovementSpeed = (Enums.MovementSpeed)speed;

            if (this.pause) {
                state.StoryPaused = true;
                state.Pause();
                state.StoryPaused = false;
            }
        }

        #endregion Methods
    }
}