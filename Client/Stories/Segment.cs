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