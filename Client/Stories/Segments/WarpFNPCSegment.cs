namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class WarpFNPCSegment : ISegment
    {
        #region Fields

        string id;
        StoryState storyState;
        int x;
        int y;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public WarpFNPCSegment(string id, int x, int y) {
            Load(id, x, y);
        }

        public WarpFNPCSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.WarpFNPC; }
        }

        public string ID {
            get { return id; }
            set { id = value; }
        }

        public ListPair<string, string> Parameters {
            get { return parameters; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string id, int x, int y) {
            this.id = id;
            this.x = x;
            this.y = y;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("ID"), parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            for (int i = 0; i < state.FNPCs.Count; i++) {
                if (state.FNPCs[i].ID == id) {
                    state.FNPCs[i].X = x;
                    state.FNPCs[i].Y = y;
                }
            }
        }

        #endregion Methods
    }
}