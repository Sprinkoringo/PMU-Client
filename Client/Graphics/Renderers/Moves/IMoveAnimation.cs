namespace Client.Logic.Graphics.Renderers.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    interface IMoveAnimation
    {
        #region Properties

        bool Active {
            get;
            set;
        }

        int AnimationIndex {
            get;
            set;
        }

        int CompletedLoops {
            get;
            set;
        }

        int Frame {
            get;
            set;
        }

        int FrameLength {
            get;
            set;
        }

        int MoveTime {
            get;
            set;
        }

        int RenderLoops {
            get;
            set;
        }

        int StartX {
            get;
            set;
        }

        int StartY {
            get;
            set;
        }

        Enums.MoveAnimationType AnimType {
            get;
        }

        #endregion
    }
}