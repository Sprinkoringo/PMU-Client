namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class DeleteFNPCSegment : ISegment
    {
        #region Fields

        string id;
        StoryState storyState;
        Enums.Direction direction;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public DeleteFNPCSegment(string id) {
            Load(id);
        }

        public DeleteFNPCSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.DeleteFNPC; }
        }

        public string ID {
            get { return id; }
            set { id = value; }
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

        public void Load(string id) {
            this.id = id;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("ID"));
        }

        public void Process(StoryState state) {
            this.storyState = state;
            for (int i = state.FNPCs.Count - 1; i >= 0; i--) {
                if (state.FNPCs[i].ID == id) {
                    state.FNPCs.RemoveAt(i);
                }
            }
        }

        #endregion Methods
    }
}