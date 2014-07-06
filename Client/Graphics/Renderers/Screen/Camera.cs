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