#region Header

/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 16/09/2009
 * Time: 9:13 PM
 *
 */

#endregion Header

namespace Client.Logic.Gui
{
	using System;
	using System.Drawing;

	using Gfx = SdlDotNet.Graphics;
	using SdlDotNet.Graphics.Sprites;

	/// <summary>
	/// Description of ProgressBar.
	/// </summary>
	internal class ProgressBar : Core.Control
	{
		#region Fields

		private Color mBackColor = Color.LightBlue;
		private Gfx.Surface mBackground;
		private Color mBarColor = Color.Blue;
		private ulong mMax = 100;
		private ulong mMin = 0;
		private int mPercent = 0;
		private ulong mValue = 0;
		private string mText = "";
		private Gfx.Font mFont = null;

		#endregion Fields

		#region Constructors

		public ProgressBar()
			: base()
		{
			mBackground = new SdlDotNet.Graphics.Surface(base.Size);
		}

		#endregion Constructors

		#region Properties
		
		public new Size Size {
			get { return base.Size; }
			set {
				base.Size = value;
				if (mBackground != null) {
					mBackground.Close();
					mBackground.Dispose();
				}
				mBackground = new SdlDotNet.Graphics.Surface(base.Size);
				UpdateSurface();
			}
		}

		public ulong Maximum
		{
			get { return mMax; }
			set {
				if (mValue < mMin) {
					mValue = mMin;
				}

				if (value <= 0) {
					value = 1;
				}
				
				mMax = value;

				if (mValue > mMax) {
					mValue = mMax;
				}

				UpdateSurface();
			}
		}
		
		public ulong Minimum
		{
			get { return mMin; }
			set {
				if (value < 0) {
					value = 0;
				}

				if (value > mMax) {
					value = mMax;
				}

				if (mValue < value) {
					mValue = value;
				}

				mMin = value;
				UpdateSurface();
			}
		}

		public Color ProgressBarColor
		{
			get { return mBarColor; }
			set {
				mBarColor = value;
				UpdateSurface();
			}
		}

		public ulong Value
		{
			get { return mValue; }
			set {
				ulong oldValue = mValue;

				if (value < mMin) {
					value = mMin;
				} else if (value > mMax) {
					value = mMax;
				}
				mValue = value;

				mPercent = (int)Logic.Math.CalculatePercent(mValue, mMax);
				UpdateSurface();
			}
		}
		
		public string Text {
			get { return mText; }
			set {
				mText = value;
				if (mFont == null) {
					mFont = Logic.Graphics.FontManager.LoadFont("PMU", 12);
				}
				UpdateSurface();
			}
		}
		
		public Gfx.Font Font {
			get { return mFont; }
			set {
				mFont = value;
			}
		}
		
		public int Percent {
			get { return mPercent; }
		}

		#endregion Properties

		#region Methods

		public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			base.Update(dstSrf, e);
		}

		private void Draw3dBorder()
		{
			Gfx.Primitives.Line lineToDraw;
			int PenWidth = 1;

			lineToDraw = new SdlDotNet.Graphics.Primitives.Line(new Point(0, 0),
			                                                    new Point(base.Width - PenWidth, 0));
			mBackground.Draw(lineToDraw, Color.DarkGray);
			lineToDraw = new SdlDotNet.Graphics.Primitives.Line(new Point(0, 0),
			                                                    new Point(0, base.Height - PenWidth));
			mBackground.Draw(lineToDraw, Color.DarkGray);
			lineToDraw = new SdlDotNet.Graphics.Primitives.Line(new Point(0, base.Height - PenWidth),
			                                                    new Point(base.Width - PenWidth, base.Height - PenWidth));
			mBackground.Draw(lineToDraw, Color.FromArgb(255, 255, 255, 254));
			lineToDraw = new SdlDotNet.Graphics.Primitives.Line(new Point(base.Width - PenWidth, 0),
			                                                    new Point(base.Width - PenWidth, base.Height - PenWidth));
			mBackground.Draw(lineToDraw, Color.FromArgb(255, 255, 255, 254));
		}

		private void UpdateSurface()
		{
			mBackground.Fill(mBackColor);
			if (base.IsColorTransparent(mBackColor)) {
				mBackground.Transparent = true;
				mBackground.TransparentColor = mBackColor;
			} else {
				mBackground.Transparent = false;
			}
			decimal newWidth = (decimal)base.Width * ((decimal)mPercent / (decimal)100);
			Size barSize = new Size(System.Math.Max(0, (int)newWidth - 2), System.Math.Max(0, base.Height - 2));
			Gfx.Surface barSurface = new SdlDotNet.Graphics.Surface(barSize);
			barSurface.Fill(mBarColor);
			mBackground.Blit(barSurface, new Point(1, 1));
			barSurface.Close();
			barSurface.Dispose();
			if (mText != "" && mFont != null) {
				Gfx.Surface fontSurf = mFont.Render(mText, Color.Black, false);
				mBackground.Blit(fontSurf, GetCenter(mBackground, fontSurf.Size), new Rectangle(0, 0, this.Width, this.Height));
			}
			Draw3dBorder();

			base.Buffer.Blit(mBackground, new Point(0, 0));
		}

		#endregion Methods
	}
}