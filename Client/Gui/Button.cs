namespace Client.Logic.Gui
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;

    /// <summary>
    /// Standard Button control.
    /// </summary>
    class Button : Core.Control
    {
        #region Fields

        private Color mBackcolor;
        private Gfx.Surface mBackgroundTexture;
        private Color mBorderColor;
        private int mBorderWidth = 1;
        private Gfx.Font mFont;
        private Color mForecolor;
        private Color mHoverColor;
        private string mText = "button";
        private Size mTextSize;
        private Gfx.Surface mTexture;
        private Skins.Core.ButtonTheme mTheme;
        private int mXOffset;
        private int mYOffset;

        #endregion Fields

        #region Constructors

        public Button()
            : base()
        {
            mFont = new SdlDotNet.Graphics.Font(IO.IO.CreateOSPath("Fonts\\PMU.ttf"), 12);
            Init();
        }

        public Button(Gfx.Font font)
            : base()
        {
            mFont = font;
            Init();
        }

        #endregion Constructors

        #region Properties

        public Color Backcolor
        {
            get { return mBackcolor; }
            set {
                mBackcolor = value;
                UpdateTexture();
            }
        }

        public Color BorderColor
        {
            get { return mBorderColor; }
            set {
                mBorderColor = value;
                UpdateTexture();
            }
        }

        public int BorderWidth
        {
            get { return mBorderWidth; }
            set {
                mBorderWidth = value;
                UpdateTexture();
            }
        }

        public Gfx.Font Font
        {
            get { return mFont; }
            set {
                mFont = value;
                UpdateTexture();
            }
        }

        public Color Forecolor
        {
            get { return mForecolor; }
            set {
                mForecolor = value;
                UpdateTexture();
            }
        }

        public Color HoverColor
        {
            get { return mHoverColor; }
            set {
                mHoverColor = value;
                UpdateTexture();
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set {
            	if (mTexture != null) {
            		mTexture.Close();
            		mTexture.Dispose();
            	}
                mTexture = new SdlDotNet.Graphics.Surface(value);
                if (mBackgroundTexture != null) {
                	mBackgroundTexture.Close();
                	mBackgroundTexture.Dispose();
                }
                mBackgroundTexture = new SdlDotNet.Graphics.Surface(new Size(value.Width - 2, value.Height - 2));
                mBackgroundTexture.Alpha = 0;
                mBackgroundTexture.AlphaBlending = true;
                base.Size = value;
                UpdateTexture();
            }
        }

        public string Text
        {
            get { return mText; }
            set {
                mText = value;
                mTextSize = mFont.SizeText(mText);
                UpdateTexture();
            }
        }

        public int TextXOffset
        {
            get { return mXOffset; }
            set {
                mXOffset = value;
                UpdateTexture();
            }
        }

        public int TextYOffset
        {
            get { return mYOffset; }
            set {
                mYOffset = value;
                UpdateTexture();
            }
        }

        #endregion Properties

        #region Methods

        public void LoadFromTheme(Skins.Core.ButtonTheme theme)
        {
            mForecolor = theme.ForeColor;
            mBackcolor = theme.BackColor;
            mBorderColor = theme.BorderColor;
            mHoverColor = theme.HoverColor;
            mTheme = theme;
            UpdateTexture();
        }

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            int addX = this.Location.X;
            int addY = this.Location.Y;
            if (this.Parent != null) {
                addX += this.Parent.Location.X;
                addY += this.Parent.Location.Y;
            }
            if (SdlDotNet.Input.Mouse.IsButtonPressed(SdlDotNet.Input.MouseButton.PrimaryButton) == false) {
                if (base.PointInBounds(SdlDotNet.Input.Mouse.MousePosition)) {
                    if (this.Focused == false) {
                        if (mBackgroundTexture.Alpha < 150) {
                            mBackgroundTexture.Alpha += 30;
                            UpdateTexture();
                        }
                    } else {
                        if (mBackgroundTexture.Alpha < 150) {
                            mBackgroundTexture.Alpha = 150;
                            UpdateTexture();
                        }
                    }
                } else {
                    if (this.Focused == false) {
                        if (mBackgroundTexture.Alpha != 0) {
                            mBackgroundTexture.Alpha = 0;
                            UpdateTexture();
                        }
                    } else {
                        if (mBackgroundTexture.Alpha < 150) {
                            mBackgroundTexture.Alpha = 150;
                            UpdateTexture();
                        }
                    }
                }
            } else {
                if (mBackgroundTexture.Alpha != 0) {
                    mBackgroundTexture.Alpha = 0;
                    UpdateTexture();
                }
            }

            base.Buffer.Blit(mTexture, new Point(0, 0));
            base.Update(dstSrf, e);
        }

        private void Init()
        {
            //mForecolor = Color.Black;
            //mBackcolor = SystemColors.Control;
            //mHoverColor = Color.SteelBlue;
            //mBorderColor = Color.Black;
            mTexture = new SdlDotNet.Graphics.Surface(this.Size);
            mTexture.Fill(mBackcolor);
        }

        private void UpdateTexture()
        {
            if (mBackcolor.A != 0) {
                mTexture.Fill(mBackcolor);
            } else {
                mTexture.Transparent = true;
                mTexture.TransparentColor = Color.Transparent;
                mTexture.Fill(Color.Transparent);
            }
            mBackgroundTexture.Fill(mHoverColor);
            mTexture.Blit(mBackgroundTexture, new Point(1, 1));
            mTexture.Blit(mFont.Render(mText, mForecolor), new Point(((mTexture.Width / 2) - (mTextSize.Width / 2)) + mXOffset, ((mTexture.Height / 2) - (mTextSize.Height / 2)) + mYOffset));
            for (int i = 0; i < mBorderWidth; i++) {
                Gfx.IPrimitive border = new Gfx.Primitives.Box((short)(i + 1), (short)(i + 1), (short)(this.Width - (2 + i)), (short)(this.Height - (2 + i)));
                mTexture.Draw(border, mBorderColor);
            }
        }

        #endregion Methods
    }
}