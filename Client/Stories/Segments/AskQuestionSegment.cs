/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


namespace Client.Logic.Stories.Segments
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Client.Logic.Menus.Core;

    using PMU.Core;

    class AskQuestionSegment : ISegment
    {
        #region Fields

        private int segmentOnNo;
        private ListPair<string, string> parameters;
        private string question;
        private int segmentOnYes;
        private int mugshot;
        StoryState storyState;
        string[] options;

        #endregion Fields

        #region Constructors

        public AskQuestionSegment(string text, int speaker, int segmentOnYes, int segmentOnNo, string[] options) {
            Load(text, segmentOnYes, segmentOnNo, speaker, options);
        }

        public AskQuestionSegment() {
        }

        #endregion Constructors

        #region Properties

        public Enums.StoryAction Action {
            get { return Enums.StoryAction.AskQuestion; }
        }

        public int SegmentOnNo {
            get { return segmentOnNo; }
            set { segmentOnNo = value; }
        }

        public ListPair<string, string> Parameters
        {
            get { return parameters; }
        }

        public string Question {
            get { return question; }
            set { question = value; }
        }

        public int SegmentOnYes {
            get { return segmentOnYes; }
            set { segmentOnYes = value; }
        }

        public int Mugshot {
            get { return mugshot; }
            set { mugshot = value; }
        }

        public bool UsesSpeechMenu {
            get { return false; }
        }

        #endregion Properties

        #region Methods

        public void Load(string question, int segmentOnYes, int segmentOnNo, int mugshot, string[] options) {
            this.question = question;
            this.segmentOnYes = segmentOnYes;
            this.segmentOnNo = segmentOnNo;
            this.mugshot = mugshot;
            this.options = options;
        }

        public void LoadFromSegmentData(ListPair<string, string> parameters)
        {
            this.parameters = parameters;
            //this code never gets reached
            //if (parameters.Count > 4) {
            //    string[] choices = new string[data[4].ToInt()];
            //    int n = 5;
            //    for (int i = 0; i < choices.Length; i++) {
            //        choices[i] = data[n];

            //        n += 1;
            //    }
            //    Load(parameters.GetValue("Question"), parameters.GetValue("SegmentOnYes").ToInt(-1), parameters.GetValue("SegmentOnNo").ToInt(-1), parameters.GetValue("Mugshot").ToInt(-1), choices);
            //} else {
                Load(parameters.GetValue("Question"), parameters.GetValue("SegmentOnYes").ToInt(-1), parameters.GetValue("SegmentOnNo").ToInt(-1), parameters.GetValue("Mugshot").ToInt(-1), new string[] { "Yes", "No" });
            //}
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
            textMenu.DisplayText(StoryProcessor.ReplaceVariables(question), mugshot);
            Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(textMenu, true);

            Components.OptionSelectionMenu optionMenu = new Components.OptionSelectionMenu("story-optionSelectionMenu", Windows.WindowSwitcher.GameWindow.MapViewer.Size, this.options);
            optionMenu.OptionSelected += new Components.OptionSelectionMenu.OptionSelectedDelegate(optionMenu_OptionSelected);
            Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(optionMenu, true);

            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu(optionMenu);
            Windows.WindowSwitcher.GameWindow.MenuManager.BlockInput = true;

            this.storyState = state;

            state.Pause();

            optionMenu.OptionSelected -= new Components.OptionSelectionMenu.OptionSelectedDelegate(optionMenu_OptionSelected);
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(optionMenu);

            //if (state.NextSegment == null || !state.NextSegment.UsesSpeechMenu) {
                Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(textMenu);
            //}
        }

        void optionMenu_OptionSelected(string option) {
            bool segmentSet = false;
            if (option == "Yes") {
                if (segmentOnYes > -1) {
                    this.storyState.CurrentSegment = segmentOnYes - 2;
                    segmentSet = true;
                }
            } else if (option == "No") {
                if (segmentOnNo > -1) {
                    this.storyState.CurrentSegment = segmentOnNo - 2;
                    segmentSet = true;
                }
            }
            if (!segmentSet) {
                Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("questionresult", option));
            }
            storyState.Unpause();
        }

        #endregion Methods
    }
}