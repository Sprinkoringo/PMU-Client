namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class HidePlayersSegment : ISegment
    {
        #region Fields

        int x;
        int y;
        StoryState storyState;
        string map;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public HidePlayersSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.HidePlayers; }
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

        public void Load() {
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
        }

        public void Process(StoryState state) {
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.PlayersVisible = false;
        }

        #endregion Methods
    }
}