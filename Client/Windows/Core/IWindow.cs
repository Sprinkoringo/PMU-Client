namespace Client.Logic.Windows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    interface IWindow
    {
        #region Methods

        void CloseWindow();

        void Keyboard_Down(SdlDotNet.Input.KeyboardEventArgs e);

        void Keyboard_Up(SdlDotNet.Input.KeyboardEventArgs e);

        void Mouse_Down(SdlDotNet.Input.MouseButtonEventArgs e);

        void Mouse_Motion(SdlDotNet.Input.MouseMotionEventArgs e);

        void Mouse_Up(SdlDotNet.Input.MouseButtonEventArgs e);

        void Tick(SdlDotNet.Graphics.Surface dstSurf, SdlDotNet.Core.TickEventArgs e);

        #endregion Methods
    }
}