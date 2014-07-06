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


namespace Client.Logic.Graphics
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	using SdlDotNet.Graphics;

	/// <summary>
	/// Class used for managing the game fonts.
	/// </summary>
	public class FontManager
	{
        #region Properties

		/// <summary>
		/// Gets or Sets the font used as the main map font
		/// </summary>
		public static Font GameFont {
			get;
			set;
		}

        public static Font GameFontSmall {
            get;
            set;
        }

		#endregion Properties

		#region Methods

		/// <summary>
		/// Initializes the textbox font.
		/// </summary>
		public static void InitFonts()
		{
			GameFont = LoadFont("PMU.ttf", 24);
            GameFontSmall = LoadFont("PMU.ttf", 16);
		}

		/// <summary>
		/// Loads a font.
		/// </summary>
		/// <param name="fontName">Filename of the font to load.</param>
		/// <param name="pointSize">Size of the font.</param>
		/// <returns></returns>
		public static Font LoadFont(string fontName, int pointSize)
		{
			if (fontName.EndsWith(".ttf") == false)
				fontName += ".ttf";
			return new Font(IO.Paths.FontPath + fontName, pointSize);
		}

		#endregion Methods
	}
}