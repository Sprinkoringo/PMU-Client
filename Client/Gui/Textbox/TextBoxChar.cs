namespace Client.Logic.Gui.Textbox
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class TextBoxChar
    {
        #region Constructors

        public TextBoxChar(string charstring, Color charColor, Size charSize)
        {
            Char = charstring;
            CharColor = charColor;
            CharSize = charSize;
        }

        public TextBoxChar(string charString, Size charSize, CharOptions options)
        {
            Char = charString;
            CharSize = charSize;
            CharColor = options.ForeColor;
            CharOptions = options;
        }

        #endregion Constructors

        #region Properties

        public string Char
        {
            get; set;
        }

        public Color CharColor
        {
            get; set;
        }

        public CharOptions CharOptions
        {
            get; set;
        }

        public Size CharSize
        {
            get; set;
        }

        #endregion Properties
    }
}