namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class ChangeFNPCDirectionSegment : ISegment
    {
        #region Fields

        string id;
        StoryState storyState;
        Enums.Direction direction;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public ChangeFNPCDirectionSegment(string id, Enums.Direction direction) {
            Load(id, direction);
        }

        public ChangeFNPCDirectionSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ChangeFNPCDir; }
        }

        public string ID {
            get { return id; }
            set { id = value; }
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

        public void Load(string id, Enums.Direction direction) {
            this.id = id;
            this.direction = direction;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("ID"), (Enums.Direction)parameters.GetValue("Direction").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            for (int i = 0; i < state.FNPCs.Count; i++) {
                if (state.FNPCs[i].ID == id) {
                    state.FNPCs[i].Direction = direction;
                }
            }
        }

        #endregion Methods
    }
}