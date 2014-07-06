namespace Client.Logic.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;

    class Scrollbar : Core.Control
    {
        #region Fields

        private Color mBackcolor = Color.Black;
        private Gfx.Surface mBackground;
        private Rectangle mButtonDownBounds;
        private Gfx.Surface mButtonSurface;
        private Rectangle mButtonUpBounds;
        private Color mForecolor = Color.White;
        private int mMaximum = 1;
        private Rectangle mScrollBackgroundBounds;
        private Rectangle mScrollBarBounds;
        private Gfx.Surface mScrollbarBackgroundSurface;
        private Gfx.Surface mScrollbarSurface;
        private int mValue = 1;

        #endregion Fields

        #region Constructors

        //public event EventHandler<Events.ScrollEventArgs> OnScroll;
        public Scrollbar()
        {
            this.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Scrollbar_OnClick);
        }

        #endregion Constructors

        #region Properties

        public new Point Location
        {
            get { return base.Location; }
            set {
                base.Location = value;
                UpdateSurfaces();
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set {
                base.Size = value;
                UpdateSurfaces();
            }
        }

        #endregion Properties

        #region Methods

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            dstSrf.Blit(mBackground, this.ScreenLocation);

            base.Update(dstSrf, e);
        }

        public void UpdateSurfaces()
        {
            mButtonUpBounds = new Rectangle(0, 0, this.Width, 25);
            mScrollBackgroundBounds = new Rectangle(0, mButtonUpBounds.Height, this.Width, this.Height - (mButtonUpBounds.Height * 2));
            mButtonDownBounds = new Rectangle(0, mButtonUpBounds.Height + mScrollBackgroundBounds.Height, this.Width, mButtonUpBounds.Height);
            mScrollBarBounds = new Rectangle(0, mButtonUpBounds.Height, this.Width, (this.mMaximum / this.mValue) * 25);

            mBackground = new SdlDotNet.Graphics.Surface(this.Size);

            mButtonSurface = new SdlDotNet.Graphics.Surface(mButtonUpBounds.Size);
            mScrollbarBackgroundSurface = new SdlDotNet.Graphics.Surface(mScrollBackgroundBounds.Size);
            mScrollbarSurface = new SdlDotNet.Graphics.Surface(mScrollBarBounds.Size);
            mScrollbarSurface.Fill(Color.Transparent);
            //mScrollbarSurface.Transparent = true;

            mButtonSurface.Fill(mForecolor);
            Gfx.Primitives.Box border = new SdlDotNet.Graphics.Primitives.Box(new Point(0, 0), new Size(mScrollBackgroundBounds.Width - 2, mScrollBackgroundBounds.Height - 1));
            mScrollbarBackgroundSurface.Draw(border, Color.Blue);

            Gfx.Primitives.Box border2 = new SdlDotNet.Graphics.Primitives.Box(new Point(0, 0), new Size(mScrollbarSurface.Width - 2, mScrollbarSurface.Height - 2));
            mScrollbarSurface.Draw(border, Color.Red);

            mBackground.Blit(mButtonSurface);
            mBackground.Blit(mScrollbarBackgroundSurface, new Point(0, mButtonSurface.Height));
            mBackground.Blit(mScrollbarSurface, new Point(0, mButtonSurface.Height));
            mBackground.Blit(mButtonSurface, new Point(0, mButtonSurface.Height + mScrollbarBackgroundSurface.Height));
        }

        void Scrollbar_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
        }

        #endregion Methods
    }
}