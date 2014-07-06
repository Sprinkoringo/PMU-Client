namespace Client.Logic.Graphics.Renderers.Moves {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class TileMoveAnimation : IMoveAnimation {
        #region Constructors

        public TileMoveAnimation(int targetX, int targetY, Enums.MoveRange rangeType, Enums.Direction dir, int range) {
            StartX = targetX;
            StartY = targetY;
            RangeType = rangeType;
            Direction = dir;
            Range = range;
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
            get { return Enums.MoveAnimationType.Tile; }
        }

        public int StartX {
            get;
            set;
        }

        public int StartY {
            get;
            set;
        }

        public Enums.MoveRange RangeType {
            get;
            set;
        }

        public int Range {
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