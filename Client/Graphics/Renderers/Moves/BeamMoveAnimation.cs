namespace Client.Logic.Graphics.Renderers.Moves {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class BeamMoveAnimation : IMoveAnimation {
        #region Constructors

        public BeamMoveAnimation(int X1, int Y1, Enums.Direction dir, int distance) {
            StartX = X1;
            StartY = Y1;
            Direction = dir;
            Distance = distance;
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

        public int RenderLoops {
            get;
            set;
        }

        public Enums.MoveAnimationType AnimType {
            get { return Enums.MoveAnimationType.Beam; }
        }

        public int StartX {
            get;
            set;
        }

        public int StartY {
            get;
            set;
        }

        public int Distance {
            get;
            set;
        }

        public Enums.Direction Direction {
            get;
            set;
        }

        #endregion Properties


    }
}