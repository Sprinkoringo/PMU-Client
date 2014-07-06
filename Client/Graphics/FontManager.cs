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