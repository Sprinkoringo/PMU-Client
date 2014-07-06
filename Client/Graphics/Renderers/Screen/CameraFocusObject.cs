using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Graphics.Renderers.Screen
{
    class CameraFocusObject
    {
        public int FocusedX {
            get;
            set;
        }

        public int FocusedXOffset {
            get;
            set;
        }

        public int FocusedY {
            get;
            set;
        }

        public int FocusedYOffset {
            get;
            set;
        }

        public Enums.Direction FocusedDirection {
            get;
            set;
        }

        public void Process(int tick) {

        }
    }
}
