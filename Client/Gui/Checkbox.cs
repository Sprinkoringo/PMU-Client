namespace Client.Logic.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;

    class Checkbox : Core.Control
    {
        #region Fields

        private bool mAntiAlias = false;
        private Color mBackColor = Color.White;
        private Gfx.Surface mBackground;
        private bool mChecked = false;
        private Rectangle mCheckedBoxBounds;
        private Color mForeColor = Color.Black;
        private string mText = "";

        #endregion Fields

        #region Constructors

        public Checkbox()
            : base()
        {
            Init();
        }

        #endregion Constructors

        #region Properties

        public bool AntiAlias
        {
            get { return mAntiAlias; }
            set {
                mAntiAlias = value;
                UpdateBackground();
            }
        }

        public Color BackColor
        {
            get { return mBackColor; }
            set { mBackColor = value; }
        }

        public bool Checked
        {
            get { return mChecked; }
            set {
                mChecked = value;
                UpdateBackground();
            }
        }

        public new Point Location
        {
            get { return base.Location; }
            set {
                base.Location = value;
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set {
                base.Size = value;
                UpdateBackground();
            }
        }

        public string Text
        {
            get { return mText; }
            set {
                mText = value;
                UpdateBackground();
            }
        }

        #endregion Properties

        #region Methods

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            base.Update(dstSrf, e);
        }

        void Checkbox_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (base.PointInBounds(e.Position)) {
                this.Checked = !this.Checked;
            }
        }

        private void Init()
        {
            base.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(Checkbox_OnClick);
        }

        private void UpdateBackground()
        {
            mBackground = new SdlDotNet.Graphics.Surface(base.Size);
            base.Buffer.Fill(mBackColor);
            if (mBackColor.A != 0) {
                mBackground.Fill(mBackColor);
            } else {
                mBackground.Transparent = true;
                mBackground.TransparentColor = Color.Transparent;
                mBackground.Fill(Color.Transparent);
            }
            mCheckedBoxBounds = new Rectangle(new Point(2, 2), new Size(this.Height - 4, this.Height - 4));
            Gfx.Primitives.Box box = new SdlDotNet.Graphics.Primitives.Box(mCheckedBoxBounds.Location, mCheckedBoxBounds.Size);
            mBackground.Draw(box, Color.Black);
            if (mChecked) {
                Gfx.Surface filled = new SdlDotNet.Graphics.Surface(box.Size);
                filled.Fill(Color.Black);
                mBackground.Blit(filled, box.Location);
                filled.Close();
                filled.Dispose();
            }
            if (mText != "") {
                Gfx.Font font = new Gfx.Font(IO.IO.CreateOSPath("Fonts\\PMU.ttf"), this.Height);
                mBackground.Blit(font.Render(mText, mForeColor, mAntiAlias), new Point(20, -4));
                font.Close();
            }
            base.Buffer.Blit(mBackground, new Point(0, 0));
        }

        #endregion Methods
    }
}