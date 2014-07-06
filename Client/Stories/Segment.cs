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


namespace Client.Logic.Stories
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;
    using Client.Logic.Stories.Segments;

    class Segment
    {
        ListPair<string, string> parameters;

        public Segment() {
            parameters = new ListPair<string, string>();
        }

        #region Properties

        public Enums.StoryAction Action {
            get;
            set;
        }

        public ListPair<string, string> Parameters {
            get { return parameters; }
        }

        public void AddParameter(string paramID, string value) {
            parameters.Add(paramID, value);
        }

        public ISegment ToSpecific() {
            ISegment specific = null;
            switch (Action) {
                case Enums.StoryAction.Say:
                    specific = new SaySegment();
                    break;
                case Enums.StoryAction.AskQuestion:
                    specific = new AskQuestionSegment();
                    break;
                case Enums.StoryAction.HideBackground:
                    specific = new HideBackgroundSegment();
                    break;
                case Enums.StoryAction.HideImage:
                    specific = new HideImageSegment();
                    break;
                case Enums.StoryAction.MapVisibility:
                    specific = new MapVisibilitySegment();
                    break;
                case Enums.StoryAction.Padlock:
                    specific = new PadlockSegment();
                    break;
                case Enums.StoryAction.Pause:
                    specific = new PauseSegment();
                    break;
                case Enums.StoryAction.PlayerPadlock:
                    specific = new PlayerPadlockSegment();
                    break;
                case Enums.StoryAction.PlayMusic:
                    specific = new PlayMusicSegment();
                    break;
                case Enums.StoryAction.ShowBackground:
                    specific = new ShowBackgroundSegment();
                    break;
                case Enums.StoryAction.ShowImage:
                    specific = new ShowImageSegment();
                    break;
                case Enums.StoryAction.StopMusic:
                    specific = new StopMusicSegment();
                    break;
                case Enums.StoryAction.Warp:
                    specific = new WarpSegment();
                    break;
                case Enums.StoryAction.CreateFNPC:
                    specific = new CreateFNPCSegment();
                    break;
                case Enums.StoryAction.MoveFNPC:
                    specific = new MoveFNPCSegment();
                    break;
                case Enums.StoryAction.WarpFNPC:
                    specific = new WarpFNPCSegment();
                    break;
                case Enums.StoryAction.ChangeFNPCDir:
                    specific = new ChangeFNPCDirectionSegment();
                    break;
                case Enums.StoryAction.DeleteFNPC:
                    specific = new DeleteFNPCSegment();
                    break;
                case Enums.StoryAction.RunScript:
                    specific = new RunScriptSegment();
                    break;
                case Enums.StoryAction.HidePlayers:
                    specific = new HidePlayersSegment();
                    break;
                case Enums.StoryAction.ShowPlayers:
                    specific = new ShowPlayersSegment();
                    break;
                case Enums.StoryAction.GoToSegment:
                    specific = new GoToSegmentSegment();
                    break;
                case Enums.StoryAction.MovePlayer:
                    specific = new MovePlayerSegment();
                    break;
                case Enums.StoryAction.ChangePlayerDir:
                    specific = new ChangePlayerDirectionSegment();
                    break;
            }
            if (specific != null) {
                specific.LoadFromSegmentData(parameters);
            }
            return specific;

        }

        #endregion Properties
    }
}