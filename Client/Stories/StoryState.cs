using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Client.Logic.Stories
{
    class StoryState
    {
        ManualResetEvent resetEvent;

        public bool StoryPaused { get; set; }
        public bool SpeechMenuActive { get; set; }
        public ISegment NextSegment { get; set; }
        public List<FNPCs.FNPC> FNPCs { get; set; }
        public int CurrentSegment { get; set; }

        public StoryState(ManualResetEvent resetEvent) {
            this.resetEvent = resetEvent;
            FNPCs = new List<FNPCs.FNPC>();
        }

        public void ResetWaitEvent() {
            resetEvent.Reset();
        }

        public void Pause() {
            ResetWaitEvent();
            resetEvent.WaitOne();
        }

        public void Pause(int milliseconds) {
            ResetWaitEvent(); 
            resetEvent.WaitOne(milliseconds, false);
        }

        public void Unpause() {
            resetEvent.Set();
        }
    }
}
