namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class HideImageSegment : ISegment
    {
        #region Fields

        string imageID;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public HideImageSegment(string imageID) {
            Load(imageID);
        }

        public HideImageSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.MapVisibility; }
        }

        public string ImageID {
            get { return imageID; }
            set { imageID = value; }
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

        public void Load(string imageID) {
            this.imageID = imageID;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.imageID = parameters.GetValue("ImageID");
        }

        public void Process(StoryState state) {
            int index = -1;
            for (int i = 0; i < Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.Count; i++) {
                if (Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays[i].ImageID == imageID) {
                    index = i;
                    break;
                }
            }
            if (index > -1) {
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.RemoveAt(index);
            }
        }

        #endregion Methods
    }
}