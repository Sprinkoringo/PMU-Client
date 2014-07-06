using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Stories.Segments
{
    class HideBackgroundSegment : ISegment
    {
        #region Fields

        private Enums.PadlockState state;
        private ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public HideBackgroundSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.HideBackground; }
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
            if (Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground != null) {
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground = null;
            }
        }

        #endregion Methods
    }
}
