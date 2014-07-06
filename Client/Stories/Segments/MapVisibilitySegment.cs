namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class MapVisibilitySegment : ISegment
    {
        #region Fields

        private bool visible;
        private ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public MapVisibilitySegment(bool visible) {
            Load(visible);
        }

        public MapVisibilitySegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.MapVisibility; }
        }

        public bool Visible {
            get { return visible; }
            set { visible = value; }
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

        public void Load(bool visible) {
            this.visible = visible;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.visible = parameters.GetValue("Visible").ToBool();
        }

        public void Process(StoryState state) {
           Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenVisible = this.visible;
        }

        #endregion Methods
    }
}