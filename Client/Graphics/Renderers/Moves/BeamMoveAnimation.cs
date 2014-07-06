/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


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