namespace Client.Logic.Graphics.Renderers.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class NormalMoveAnimation : IMoveAnimation
    {
        #region Constructors

        public NormalMoveAnimation(int targetX, int targetY)
        {
            StartX = targetX;
            StartY = targetY;
        }

        #endregion Constructors

        #region Properties

        public bool Active
        {
            get;
            set;
        }

        public int AnimationIndex
        {
            get;
            set;
        }

        public int CompletedLoops
        {
            get;
            set;
        }

        public int Frame
        {
            get;
            set;
        }

        public int FrameLength
        {
            get;
            set;
        }

        public int MoveTime
        {
            get;
            set;
        }

        public int RenderLoops
        {
            get;
            set;
        }

        public Enums.MoveAnimationType AnimType
        {
            get { return Enums.MoveAnimationType.Normal; }
        }

        public int StartX
        {
            get;
            set;
        }

        public int StartY
        {
            get;
            set;
        }

        #endregion Properties

        
    }
}