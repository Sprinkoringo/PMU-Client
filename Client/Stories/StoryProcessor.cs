using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Client.Logic.Menus.Core;
using Client.Logic.Network;

namespace Client.Logic.Stories
{
    class StoryProcessor
    {
        static Thread playbackThread;
        static Story activeStory;
        static ManualResetEvent resetEvent;
        internal static bool loadingStory;

        public static Story ActiveStory {
            get { return activeStory; }
        }

        public static void PlayStory(Story story, int startSegment) {
            loadingStory = false;
            if (activeStory == null) {
                playbackThread = new Thread(new ParameterizedThreadStart(PlayStoryCallback));
                playbackThread.Start(new object[] { story, startSegment });
            }
        }

        public static void ForceEndStory() {
            if (activeStory != null) {
                activeStory.Segments = new List<ISegment>();
                activeStory.State.Unpause();
            }
            
        }

        public static void PlayStoryCallback(Object param) {
            Object[] paramObj = param as Object[];
            Story story = paramObj[0] as Story;
            int startSegment = (int)paramObj[1];
            if (activeStory == null) {
                activeStory = story;
                resetEvent = new ManualResetEvent(false);
                story.State = new StoryState(resetEvent);
                story.State.StoryPaused = false;
                story.State.CurrentSegment = startSegment;
                if (!string.IsNullOrEmpty(story.Name) || story.LocalStory) {
                    try {
                        do {
                            if (!story.LocalStory) {
                                Messenger.SendUpdateSegment(story.State.CurrentSegment);
                            }
                            ISegment segment = story.Segments[story.State.CurrentSegment];
                            ISegment nextSegment = GetNextSegment(story, story.State.CurrentSegment);
                            story.State.NextSegment = nextSegment;
                            if (segment != null) {
                                segment.Process(story.State);
                            }
                            story.State.CurrentSegment++;
                        } while (story.State.CurrentSegment < story.Segments.Count);
                    } catch (Exception ex) {
                        SdlDotNet.Widgets.MessageBox.Show(ex.ToString(), "Error!");
                    }
                }
                if (!story.LocalStory) {
                    Messenger.SendChapterComplete();
                }
                Menus.MenuSwitcher.CloseAllMenus();
                activeStory = null;
            }
        }

        static ISegment GetNextSegment(Story story, int currentSegment) {
            if (currentSegment < story.Segments.Count - 1) {
                return story.Segments[currentSegment + 1];
            } else {
                return null;
            }
        }

        static void textMenu_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            resetEvent.Set();
        }

        public static string ReplaceVariables(string text) {
            int yellow = System.Drawing.Color.Yellow.ToArgb();
            text = text.Replace("%playername%", "[c][" + yellow + "]" + Players.PlayerManager.MyPlayer.Name + "[/c]");
            return text;
        }

    }
}
