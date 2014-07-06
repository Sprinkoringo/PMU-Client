namespace Client.Logic.Gui.Textbox
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	class TextboxLine
	{
		#region Fields

		private List<TextBoxChar> mChars;
		private int mCurrentWidth;
		private Color mDefaultForeColor;
		private SdlDotNet.Graphics.Font mFont;
		private int mMaxWidth;
		private string mText = "";

		#endregion Fields

		#region Constructors

		public TextboxLine(int maxWidth, SdlDotNet.Graphics.Font font, Color defaultForeColor)
		{
			mMaxWidth = maxWidth;
			mChars = new List<TextBoxChar>();
			mFont = font;
			mDefaultForeColor = defaultForeColor;
		}

		#endregion Constructors

		#region Properties

		public List<TextBoxChar> Chars
		{
			get { return mChars; }
		}

		public int CurrentWidth
		{
			get { return mCurrentWidth; }
		}

		public SdlDotNet.Graphics.Font Font
		{
			get { return mFont; }
		}

		public int MaxWidth
		{
			get { return mMaxWidth; }
		}

		public string Text
		{
			get { return mText; }
		}

		#endregion Properties

		#region Methods

		public void AddChar(string letter)
		{
			Size letterSize = mFont.SizeText(letter);
			if (letter == "\n")
				letterSize.Width = 0;
			if (mCurrentWidth + letterSize.Width <= mMaxWidth) {
				mChars.Add(new TextBoxChar(letter, Color.Empty, letterSize));
				mCurrentWidth += letterSize.Width;
			}
		}

		public void AddChar(string letter, CharOptions options)
		{
			Size letterSize = mFont.SizeText(letter);
			if (letter == "\n")
				letterSize.Width = 0;
			if (mCurrentWidth + letterSize.Width < mMaxWidth) {
				mChars.Add(new TextBoxChar(letter, letterSize, options));
				mCurrentWidth += letterSize.Width;
			}
		}
		
		public void RemoveChar(int charNum)
		{
			if (mChars[charNum].Char != "\n") {
				mMaxWidth -= mChars[charNum].CharSize.Width;
			}
			mChars.RemoveAt(charNum);
		}

		public void InsertChar(string letter, int position, CharOptions options)
		{
			Size letterSize = mFont.SizeText(letter);
			mChars.Insert(position, new TextBoxChar(letter, Color.Empty, letterSize));
			mCurrentWidth += letterSize.Width;
		}

		public bool LineFull(int widthToTest)
		{
			return !(mCurrentWidth + widthToTest < mMaxWidth);
		}

		public bool LineFull()
		{
			return !(mCurrentWidth < mMaxWidth);
		}

		public void RecalculateWidth()
		{
			mCurrentWidth = 0;
			for (int i = 0; i < mChars.Count; i++) {
				mCurrentWidth += mChars[i].CharSize.Width;
			}
		}

		public SdlDotNet.Graphics.Surface Render()
		{
			SdlDotNet.Graphics.Surface tempSurf = new SdlDotNet.Graphics.Surface(mMaxWidth, mFont.Height);
			tempSurf.Fill(Color.Transparent);
			tempSurf.TransparentColor = Color.Transparent;
			tempSurf.Transparent = true;
			int lastX = 0;
			for (int i = 0; i < mChars.Count; i++) {
				if (mChars[i].Char != "\n") {
					if (mChars[i].CharOptions != null) {
						mFont.Bold = mChars[i].CharOptions.Bold;
						mFont.Italic = mChars[i].CharOptions.Italic;
						mFont.Underline = mChars[i].CharOptions.Underline;
					}
					Color bltColor;
					if (mChars[i].CharColor == Color.Empty)
						bltColor = mDefaultForeColor;
					else
						bltColor = mChars[i].CharColor;
					SdlDotNet.Graphics.Surface charSurf = mFont.Render(mChars[i].Char, bltColor, false);
					tempSurf.Blit(charSurf, new Point(lastX, 0));
					charSurf.Close();
					if (mFont.Bold == true) {
						mFont.Bold = false;
					}
					if (mFont.Italic == true) {
						mFont.Italic = false;
					}
					if (mFont.Underline == true) {
						mFont.Underline = false;
					}
					lastX += mChars[i].CharSize.Width;
				}
			}

			return tempSurf;
		}
		
		public SdlDotNet.Graphics.Surface RenderPassword(char passwordChar)
		{
			int charWidth = mFont.SizeText(passwordChar.ToString()).Width;
			SdlDotNet.Graphics.Surface tempSurf = new SdlDotNet.Graphics.Surface(mMaxWidth, mFont.Height);
			tempSurf.Fill(Color.Transparent);
			tempSurf.TransparentColor = Color.Transparent;
			tempSurf.Transparent = true;
			int lastX = 0;
			for (int i = 0; i < mChars.Count; i++) {
				if (mChars[i].Char != "\n") {
					if (mChars[i].CharOptions != null) {
						mFont.Bold = mChars[i].CharOptions.Bold;
						mFont.Italic = mChars[i].CharOptions.Italic;
						mFont.Underline = mChars[i].CharOptions.Underline;
					}
					Color bltColor;
					if (mChars[i].CharColor == Color.Empty)
						bltColor = mDefaultForeColor;
					else
						bltColor = mChars[i].CharColor;
					SdlDotNet.Graphics.Surface charSurf = mFont.Render(passwordChar.ToString(), bltColor, false);
					tempSurf.Blit(charSurf, new Point(lastX, 0));
					charSurf.Close();
					if (mFont.Bold == true) {
						mFont.Bold = false;
					}
					if (mFont.Italic == true) {
						mFont.Italic = false;
					}
					if (mFont.Underline == true) {
						mFont.Underline = false;
					}
					lastX += charWidth;
				}
			}

			return tempSurf;
		}

		#endregion Methods
	}
}