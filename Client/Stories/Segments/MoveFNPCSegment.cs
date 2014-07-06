namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class MoveFNPCSegment : ISegment
    {
        #region Fields

        string id;
        bool pause;
        StoryState storyState;
        int x;
        int y;
        int speed;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public MoveFNPCSegment(string id, int x, int y, int speed, bool pause) {
            Load(id, x, y, speed, pause);
        }

        public MoveFNPCSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.MoveFNPC; }
        }

        public string ID {
            get { return id; }
            set { id = value; }
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

        public void Load(string id, int x, int y, int speed, bool pause) {
            this.id = id;
            this.x = x;
            this.y = y;
            this.speed = speed;
            this.pause = pause;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;



            Load(parameters.GetValue("ID"), parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt(),
                parameters.GetValue("Speed").ToInt(), parameters.GetValue("Pause").ToBool());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            for (int i = 0; i < state.FNPCs.Count; i++) {
                if (state.FNPCs[i].ID == id) {
                    state.FNPCs[i].TargetX = x;
                    state.FNPCs[i].TargetY = y;
                }
            }

            if (this.pause) {
                state.StoryPaused = true;
                state.Pause();
                state.StoryPaused = false;
            }
        }

        #endregion Methods
    }
}