namespace Client.Logic.Gui.Textbox
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class CharOptions
    {
        #region Constructors

        public CharOptions(Color foreColor)
        {
            ForeColor = foreColor;
        }

        #endregion Constructors

        #region Properties

        public bool Bold
        {
            get; set;
        }

        public Color ForeColor
        {
            get; set;
        }

        public bool Italic
        {
            get; set;
        }

        public bool Underline
        {
            get; set;
        }

        #endregion Properties
    }
}