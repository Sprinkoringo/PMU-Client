using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Events
{
    class ActiveRecruitChangedEventArgs : EventArgs
    {
        int newSlot;

        public int NewSlot {
            get { return newSlot; }
        }
        public ActiveRecruitChangedEventArgs(int newSlot) {
            this.newSlot = newSlot;
        }
    }
}
