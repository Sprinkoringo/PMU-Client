namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class ShowImageSegment : ISegment
    {
        #region Fields

        string imageID;
        ListPair<string, string> parameters;
        int x;
        int y;
        StoryState storyState;
        string file;

        #endregion Fields

        #region Constructors

        public ShowImageSegment(string file, string imageID, int x, int y) {
            Load(file, imageID, x, y);
        }

        public ShowImageSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ShowImage; }
        }

        public string File {
            get { return file; }
            set { file = value; }
        }


        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public string ImageID {
            get { return imageID; }
            set { imageID = value; }
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

        public void Load(string file, string imageID, int x, int y) {
            this.file = file;
            this.imageID = imageID;
            this.x = x;
            this.y = y;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("File"), parameters.GetValue("ImageID"), parameters.GetValue("X").ToInt(), parameters.GetValue("Y").ToInt());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            state.ResetWaitEvent();

            Components.ScreenImageOverlay imageOverlay = new Components.ScreenImageOverlay(file, imageID, x, y);
            imageOverlay.LoadImage();
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.ScreenImageOverlays.Add(imageOverlay);
        }

        #endregion Methods
    }
}