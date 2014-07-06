namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using PMU.Core;

    class PlayMusicSegment : ISegment
    {
        #region Fields

        bool honorSettings;
        bool loop;
        StoryState storyState;
        string file;
        ListPair<string, string> parameters;

        #endregion Fields

        #region Constructors

        public PlayMusicSegment(string file, bool honorSettings, bool loop) {
            Load(file, honorSettings, loop);
        }

        public PlayMusicSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.PlayMusic; }
        }

        public string File {
            get { return file; }
            set { file = value; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public bool HonorSettings {
            get { return honorSettings; }
            set { honorSettings = value; }
        }

        public bool Loop {
            get { return loop; }
            set { loop = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string file, bool honorSettings, bool loop) {
            this.file = file;
            this.honorSettings = honorSettings;
            this.loop = loop;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("File"), parameters.GetValue("HonorSettings").ToBool(), parameters.GetValue("Loop").ToBool());
        }

        public void Process(StoryState state) {
            this.storyState = state;
            string fileToPlay = null;
            if (file == "%mapmusic%") {
                fileToPlay = Maps.MapHelper.ActiveMap.Music;
            } else {
                fileToPlay = file;
            }
            if (!string.IsNullOrEmpty(fileToPlay)) {
                Music.Music.AudioPlayer.PlayMusic(fileToPlay, this.loop ? -1 : 1, !honorSettings, true);
            }
        }

        #endregion Methods
    }
}