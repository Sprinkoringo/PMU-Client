namespace Client.Logic.Graphics.Renderers.Moves {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class ThrowMoveAnimation : IMoveAnimation {
        #region Constructors

        public ThrowMoveAnimation(int X1, int Y1, int DX, int DY) {
            StartX = X1;
            StartY = Y1;
            XChange = DX;
            YChange = DY;
            TotalMoveTime = Globals.Tick;
        }

        #endregion Constructors

        #region Properties

        public bool Active {
            get;
            set;
        }

        public int AnimationIndex {
            get;
            set;
        }

        public int CompletedLoops {
            get;
            set;
        }

        public int Frame {
            get;
            set;
        }

        public int FrameLength {
            get;
            set;
        }

        public int MoveTime {
            get;
            set;
        }

        public int TotalMoveTime {
            get;
            set;
        }

        public int RenderLoops {
            get;
            set;
        }

        public Enums.MoveAnimationType AnimType {
            get { return Enums.MoveAnimationType.Throw; }
        }

        public int StartX {
            get;
            set;
        }

        public int StartY {
            get;
            set;
        }

        public int XChange {
            get;
            set;
        }

        public int YChange {
            get;
            set;
        }


        #endregion Properties


    }
}