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


namespace Client.Logic.Graphics.Renderers.Screen
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class Camera
    {
        #region Properties

        public CameraFocusObject FocusObject {
            get;
            set;
        }

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

        public int X {
            get;
            set;
        }

        public int X2 {
            get;
            set;
        }

        public int Y {
            get;
            set;
        }

        public int Y2 {
            get;
            set;
        }

        #endregion Properties

        #region Methods

        public void FocusOnSprite(Logic.Graphics.Renderers.Sprites.ISprite sprite) {
            FocusedX = sprite.X;
            FocusedY = sprite.Y;
            FocusedXOffset = sprite.Offset.X;
            FocusedYOffset = sprite.Offset.Y;
            FocusedDirection = sprite.Direction;
        }

        public void FocusOnFocusObject(CameraFocusObject focusObject) {
            FocusedX = focusObject.FocusedX;
            FocusedY = focusObject.FocusedX;
            FocusedXOffset = focusObject.FocusedXOffset;
            FocusedYOffset = focusObject.FocusedYOffset;
            FocusedDirection = focusObject.FocusedDirection;

            this.FocusObject = focusObject;
        }

        #endregion Methods
    }
}