namespace Client.Logic.Gui
{
	using System;
	using System.Collections.Generic;
	using System.Drawing;
	using System.Text;

	using Gfx = SdlDotNet.Graphics;
	using SdlDotNet.Graphics.Sprites;

	/// <summary>
	/// Standard PictureBox control. Used to display images.
	/// </summary>
	class PictureBox : Core.Control
	{
		#region Fields

		private Color mBackColor = Color.Transparent;
		private Color mBorderColor;
		private int mBorderWidth;
		private Gfx.Surface mImage = null;
		private string mImagePath;
		private SizeMode mImageSizeMode;

		#endregion Fields

		#region Constructors

		public PictureBox()
			: base()
		{
		}

		#endregion Constructors

		#region Enumerations

		public enum SizeMode
		{
			Crop,
			AutoSize,
			CenterImage
		}

		#endregion Enumerations

		#region Properties

		public Color BackColor
		{
			get { return mBackColor; }
			set {
				mBackColor = value;
				UpdateImage();
			}
		}

		public Color BorderColor
		{
			get { return mBorderColor; }
			set {
				mBorderColor = value;
				UpdateImage();
			}
		}

		public int BorderWidth
		{
			get { return mBorderWidth; }
			set {
				mBorderWidth = value;
				UpdateImage();
			}
		}

		public Gfx.Surface Image
		{
			get { return mImage; }
			set {
				mImage = value;
				UpdateImage();
			}
		}

		public string ImagePath
		{
			get { return mImagePath; }
			set {
				mImagePath = IO.IO.CreateOSPath(value);
				if (mImage != null) {
					mImage.Close();
					mImage.Dispose();
				}
				mImage = new SdlDotNet.Graphics.Surface(mImagePath);
				UpdateImage();
			}
		}

		public SizeMode ImageSizeMode
		{
			get { return mImageSizeMode; }
			set {
				mImageSizeMode = value;
				UpdateImage();
			}
		}

		#endregion Properties

		#region Methods

		public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
		{
			//int addX = 0;
			//int addY = 0;
			//if (this.Parent != null) {
			//    //addX += this.Parent.Location.X;
			//    //addY += this.Parent.Location.Y;
			//}
			base.Update(dstSrf, e);
		}

		private void Init()
		{
		}

		private void UpdateImage()
		{
			switch (mImageSizeMode) {
					case SizeMode.AutoSize: {
						this.Size = this.mImage.Size;
					}
					break;
			}

			base.Buffer.Transparent = false;
			base.Buffer.Fill(mBackColor);
			if (mImage != null) {
				switch (mImageSizeMode) {
						case SizeMode.Crop: {
							base.Buffer.Blit(mImage, new Point(mBorderWidth, mBorderWidth), new Rectangle(0, 0, this.Width, this.Height));
						}
						break;
						case SizeMode.AutoSize: {
							base.Buffer.Blit(mImage, new Point(mBorderWidth, mBorderWidth));
						}
						break;
						case SizeMode.CenterImage: {
							Point drawPoint = base.GetCenter(base.Buffer, mImage.Size);
							base.Buffer.Blit(mImage, drawPoint);
						}
						break;
				}
			}
			
			DrawBorder();
		}
		
		private void DrawBorder()
		{
			for (int i = 0; i < mBorderWidth; i++) {
				Gfx.IPrimitive border = new SdlDotNet.Graphics.Primitives.Box((short)(i), (short)(i), (short)((this.Width) - (1 + i)), (short)((this.Height - (1 + i))));
				base.Buffer.Draw(border, mBorderColor);
			}
		}

		#endregion Methods
	}
}