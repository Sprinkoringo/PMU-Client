namespace Client.Logic.Gui
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using Gfx = SdlDotNet.Graphics;
	using SdlDotNet.Graphics.Sprites;

	/// <summary>
	/// Standard Label control. Used to display text.
	/// </summary>
	class Label : Core.Control
	{
		#region Fields

		string[] lines;
		private bool mAntiAlias = false;
		private bool mAutoSize;
		private Color mBackColor = Color.Transparent;
		private Color mBorderColor;
		private int mBorderWidth;
		private Gfx.Font mFont;
		private Color mForeColor;
		private bool mMultiLine;
		private string mText = "";
		private Skins.Core.LabelTheme mTheme;
		private bool mWordwrap;
		private int mXOffset = 0;
		private int mYOffset = 0;
		string[] splitChars = { "\n" };
		private bool mCentered = false;

		// show text
		// seperate lines from string (newline character slows down creation of TextSprite)
		TextSprite[] textSprites;

		#endregion Fields

		#region Constructors

		public Label()
			: base()
		{
			Init();
			mFont = Logic.Graphics.FontManager.LoadFont("PMU.ttf", 12);
		}

		public Label(Gfx.Font font)
			: base()
		{
			mFont = font;
			Init();
		}
		
		public Label(string fontName, int pointSize)
			: base() {
			mFont = Logic.Graphics.FontManager.LoadFont(fontName, pointSize);
			Init();
		}

		#endregion Constructors

		#region Properties

		public bool AntiAlias
		{
			get { return mAntiAlias; }
			set {
				mAntiAlias = value;
				UpdateTextBuffer();
			}
		}

		public bool Autosize
		{
			get { return mAutoSize; }
			set {
				mAutoSize = value;
			}
		}

		public Color Backcolor
		{
			get { return mBackColor; }
			set {
				mBackColor = value;
				if (mBackColor.A == 0) {
					mBackColor = Color.Transparent;
				}
			}
		}

		public Color BorderColor
		{
			get { return mBorderColor; }
			set {
				mBorderColor = value;
				UpdateTextBuffer();
			}
		}

		public int BorderWidth
		{
			get { return mBorderWidth; }
			set {
				mBorderWidth = value;
				UpdateTextBuffer();
			}
		}
		
		public bool Centered {
			get { return mCentered; }
			set {
				mCentered = value;
				UpdateTextBuffer();
			}
		}

		public Gfx.Font Font
		{
			get { return mFont; }
			set { mFont = value; }
		}

		public Color ForeColor
		{
			get { return mForeColor; }
			set {
				if (value == Color.White) {
					value = Color.FromArgb(255, 255, 255, 254);
				}
				mForeColor = value;
				UpdateTextBuffer();
			}
		}

		public new Size Size
		{
			get { return base.Size; }
			set {
				base.Size = value;
				if (base.Buffer.Size != this.Size) {
					base.ReinitBuffer();
				}
			}
		}

		public string Text
		{
			get { return mText; }
			set {
				if (mText != value) {
					mText = value;
					mText = mText.Replace("\t", "     ");
					if (mText.Contains("\n")) {
						mMultiLine = true;
					} else {
						mMultiLine = false;
					}
					if (mMultiLine) {
						SetText(mText);
					} else {
						if (mAutoSize) {
							Size textSize = this.Font.SizeText(mText);
							this.Size = new Size(textSize.Width + 2, textSize.Height);
						}
					}

					UpdateTextBuffer();
				}
			}
		}

		public int TextXOffset
		{
			get { return mXOffset; }
			set {
				mXOffset = value;
			}
		}

		public int TextYOffset
		{
			get { return mYOffset; }
			set { mYOffset = value; }
		}

		public bool Wordwrap
		{
			get { return mWordwrap; }
			set { mWordwrap = value; }
		}

		#endregion Properties

		#region Methods

		public override void Close()
		{
			base.Close();
		}

		public void LoadFromTheme(Skins.Core.LabelTheme theme)
		{
			mForeColor = theme.Forecolor;
			mBackColor = theme.Backcolor;
			mTheme = theme;
		}

		public Gfx.Surface Render()
		{
			UpdateTextBuffer();
			return base.Buffer;
		}

		public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			base.Update(dstSrf, e);
		}

		private void DrawBorder()
		{
			for (int i = 0; i < mBorderWidth; i++) {
				//IPrimitive border = new SdlDotNet.Graphics.Primitives.Box((short)(addX + i), (short)(addY + i), (short)((this.Width + addX) - (1 + i)), (short)((this.Height + addY) - (1 + i)));
				Gfx.IPrimitive border = new SdlDotNet.Graphics.Primitives.Box((short)(i), (short)(i), (short)((this.Width) - (1 + i)), (short)((this.Height - (1 + i))));
				base.Buffer.Draw(border, mBorderColor);
			}
		}

		private void Init()
		{
			mWordwrap = true;
			//mForecolor = Color.White;
			//mBackColor = Color.Transparent;
			mAutoSize = true;
		}

		private void SetText(string newText)
		{
			lines = newText.Split(splitChars, new StringSplitOptions());
			int width = this.Width;
			int curY = 0;
			int startY = curY;
			int biggestWidth = 0;
			textSprites = new TextSprite[lines.Length];
			for (int i = 0; i < lines.Length; i++) {
				if (mBackColor.A == 0) {
					textSprites[i] = new TextSprite(lines[i], mFont, mForeColor);
				} else {
					textSprites[i] = new TextSprite(lines[i], mFont, mForeColor, mBackColor);
				}
				if (mWordwrap && mAutoSize == false) {
					//textSprites[i].AntiAlias = false;
					textSprites[i].TextWidth = width - 10;
				}
				textSprites[i].Transparent = true;
				if (textSprites[i].Width > biggestWidth)
					biggestWidth = textSprites[i].Width;
				textSprites[i].X = 0;//(int)((width - textSprites[i].Width) / 2);
				textSprites[i].Y = curY;
				textSprites[i].AntiAlias = false;
				curY += textSprites[i].Height;
			}
			if (mAutoSize)
				this.Size = new Size(biggestWidth, curY);
		}

		private void UpdateTextBuffer()
		{
			try {
				base.Buffer.Fill(mBackColor);
				int addX = mXOffset;
				int addY = mYOffset;
				if (mText != "") {
					if (mMultiLine) {
						for (int i = 0; i < lines.Length; i++) {
							base.Buffer.Blit(textSprites[i].Surface, new Point(textSprites[i].X + addX, textSprites[i].Y + addY), new Rectangle(0, 0, this.Width, this.Height));
						}
					} else {
						if (mCentered) {
							if (mBackColor.A == 0) {
								Gfx.Surface fontSurf = mFont.Render(mText, mForeColor, mAntiAlias);
								base.Buffer.Blit(fontSurf, GetCenter(base.Buffer, fontSurf.Size), new Rectangle(0, 0, this.Width, this.Height));
							} else {
								base.Buffer.Blit(mFont.Render(mText, mForeColor, mBackColor, mAntiAlias), new Point(addX, addY), new Rectangle(0, 0, this.Width, this.Height));
							}
						} else  {
							if (mBackColor.A == 0) {
								base.Buffer.Blit(mFont.Render(mText, mForeColor, mAntiAlias), new Point(addX, addY), new Rectangle(0, 0, this.Width, this.Height));
							} else {
								base.Buffer.Blit(mFont.Render(mText, mForeColor, mBackColor, mAntiAlias), new Point(addX, addY), new Rectangle(0, 0, this.Width, this.Height));
							}
						}
					}
				}

				DrawBorder();
			} catch {}
		}

		#endregion Methods
	}
}