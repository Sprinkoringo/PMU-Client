namespace Client.Logic.Gui.Events
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class KeyDownEventArgs : EventArgs
    {
        #region Fields

        private SdlDotNet.Input.Key mKeyPressed = SdlDotNet.Input.Key.Zero;
        private string mKeyString = "";

        #endregion Fields

        #region Constructors

        public KeyDownEventArgs(SdlDotNet.Input.KeyboardEventArgs e, bool cancel)
        {
            Cancel = cancel;
            mKeyPressed = e.Key;
            mKeyString = Input.Keyboard.GetCharString(e);
        }

        #endregion Constructors

        #region Properties

        public bool Cancel
        {
            get; set;
        }

        public SdlDotNet.Input.Key Key
        {
            get { return mKeyPressed; }
        }

        public string KeyString
        {
            get { return mKeyString; }
        }

        #endregion Properties
    }
}