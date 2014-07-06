namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class SaySegment : ISegment
    {
        #region Fields

        private int pauseLocation;
        ListPair<string, string> parameters;
        private int speaker;
        private int speed;
        StoryState storyState;
        private string text;

        #endregion Fields

        #region Constructors

        public SaySegment(string text, int speaker, int speed, int pauseLocation) {
            Load(text, speaker, speed, pauseLocation);
        }

        public SaySegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.Say; }
        }

        public int PauseLocation {
            get { return pauseLocation; }
            set { pauseLocation = value; }
        }


        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public int Speaker {
            get { return speaker; }
            set { speaker = value; }
        }

        public int Speed {
            get { return speed; }
            set { speed = value; }
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public bool UsesSpeechMenu {
            get { return true; }
        }

        #endregion Properties

        #region Methods

        public void Load(string text, int speaker, int speed, int pauseLocation) {
            this.text = text;
            this.speaker = speaker;
            this.speed = speed;
            this.pauseLocation = pauseLocation;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            Load(parameters.GetValue("Text"), parameters.GetValue("Mugshot").ToInt(-1), parameters.GetValue("Speed").ToInt(1), parameters.GetValue("PauseLocation").ToInt(-1));
        }

        public void Process(StoryState state) {
            Menus.MenuSwitcher.ShowBlankMenu();
            Components.SpokenTextMenu textMenu;
            IMenu menuToFind = Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("story-spokenTextMenu");
            if (menuToFind != null) {
                textMenu = (Components.SpokenTextMenu)menuToFind;
            } else {
                textMenu = new Components.SpokenTextMenu("story-spokenTextMenu", Windows.WindowSwitcher.GameWindow.MapViewer.Size);
            }
            textMenu.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(textMenu_Click);
            textMenu.KeyDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(textMenu_KeyDown);
            textMenu.DisplayText(StoryProcessor.ReplaceVariables(text), speaker);
            Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(textMenu, true);
            Windows.WindowSwitcher.GameWindow.MenuManager.BlockInput = false;

            this.storyState = state;

            if (Windows.WindowSwitcher.GameWindow.BattleLog.Visible) {
                Windows.WindowSwitcher.GameWindow.BattleLog.Hide();
            }

            state.Pause();

            textMenu.Click -= new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(textMenu_Click);
            textMenu.KeyDown -= new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(textMenu_KeyDown);

            if (state.NextSegment == null || !state.NextSegment.UsesSpeechMenu) {
                Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(textMenu);
            }
        }

        void textMenu_KeyDown(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            if (e.Key == SdlDotNet.Input.Key.Return) {
                storyState.Unpause();
            }
        }

        void textMenu_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            storyState.Unpause();
        }

        #endregion Methods
    }
}