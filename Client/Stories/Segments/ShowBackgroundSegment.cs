namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class ShowBackgroundSegment : ISegment
    {
        #region Fields

        string file;
        ListPair<string, string> parameters;
        StoryState storyState;

        #endregion Fields

        #region Constructors

        public ShowBackgroundSegment(string file) {
            Load(file);
        }

        public ShowBackgroundSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.ShowBackground; }
        }

        public string File {
            get { return file; }
            set { file = value; }
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

        public void Load(string file) {
            this.file = file;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            this.file = parameters.GetValue("File");
        }

        public void Process(StoryState state) {
            if (Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground != null) {
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground.Dispose();
            }
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.StoryBackground = Logic.Graphics.SurfaceManager.LoadSurface(IO.Paths.StoryDataPath + "Backgrounds/" + file);
        }

        #endregion Methods
    }
}