namespace Client.Logic.Gui
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Gfx = SdlDotNet.Graphics;
    using SdlDotNet.Graphics.Sprites;

    class VScrollBar : Core.Control
    {
        #region Fields

        Gui.Button btnUp, btnDown;
        int buttonHeight = 10;
        Rectangle cursorArea; //, cursorLeft, cursorRight, cursorMiddle, cursorMidDest;
        Point cursorPos; //, cursorOffset;
        Gfx.Surface cursorSurf;
        bool inverted = false;
        bool isScrolling = false;
        Gfx.Surface mBackground;
        int max = 100;
        int step = 1;
        int value = 50;

        #endregion Fields

        #region Constructors

        public VScrollBar()
            : base()
        {
            Init();
        }

        #endregion Constructors

        #region Properties

        public bool Inverted
        {
            get { return inverted; } set { inverted = value; }
        }

        public new Point Location
        {
            get { return base.Location; }
            set {
                base.Location = value;
                Init();
            }
        }

        public int Max
        {
            get { return max; }
            set {
                max = value;
                if (max < 0)
                    max = 0;
                if (this.value > max)
                    this.value = max;
            }
        }

        public new Size Size
        {
            get { return base.Size; }
            set {
                base.Size = value;
                Init();
            }
        }

        public int Step
        {
            get { return step; } set { step = value; }
        }

        public int Value
        {
            get { return value; }
            set {
                this.value = value;
                if (this.value < 0)
                    this.value = 0;
                else if (this.value > max)
                    this.value = max;
            }
        }

        public new int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        #endregion Properties

        #region Methods

        public override void Close()
        {
            if (base.ContainsControl(btnUp)) {
                btnUp.RemoveEvents();
                btnUp.Close();
                base.RemoveControl(btnUp);
            }
            if (base.ContainsControl(btnDown)) {
                btnDown.RemoveEvents();
                btnDown.Close();
                base.RemoveControl(btnDown);
            }

            base.Close();
        }

        public override void Update(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            DrawBackground(dstSrf, e);
            DrawCursor(dstSrf, e);

            base.Update(dstSrf, e);
        }

        private void DrawBackground(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            mBackground.Fill(Color.White);
            dstSrf.Blit(mBackground, new Point(this.ScreenLocation.X, this.ScreenLocation.Y + buttonHeight));
        }

        private void DrawCursor(SdlDotNet.Graphics.Surface dstSrf, SdlDotNet.Core.TickEventArgs e)
        {
            cursorArea.Height = System.Math.Max(20, mBackground.Height-System.Math.Max(20, max/4));
            cursorArea.Width = 12;

            cursorPos.X = this.ScreenLocation.X;
            if (!isScrolling) {
                if (!inverted)
                    cursorPos.Y = (this.ScreenLocation.Y + buttonHeight) + (Height - 21 - cursorArea.Height) + ((value * 100) / max);
                else
                    cursorPos.Y = (this.ScreenLocation.Y + buttonHeight ) + (Height - 21 - cursorArea.Height) * ((max - value) / max);
            }

            cursorArea.X = (int)(cursorPos.X + this.ScreenLocation.X);
            cursorArea.Y = (int)(cursorPos.Y + this.ScreenLocation.Y);

            cursorSurf = new SdlDotNet.Graphics.Surface(cursorArea.Size);
            cursorSurf.Fill(Color.Gray);

            dstSrf.Blit(cursorSurf, cursorPos);
            //spriteBatch.Draw(cursorTex, cursorPos, cursorLeft, BackColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

            //cursorMidDest.X = (int)cursorPos.X + 3;
            //cursorMidDest.Y = (int)cursorPos.Y;
            //cursorMidDest.Width = cursorArea.Width - 6;
            //spriteBatch.Draw(cursorTex, cursorMidDest, cursorMiddle, BackColor, 0f, Vector2.Zero, SpriteEffects.None, 0f);

            //spriteBatch.Draw(cursorTex, cursorPos + new Vector2(cursorMidDest.Width, 0f), cursorRight, BackColor, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);
        }

        private void Init()
        {
            if (base.ContainsControl(btnUp)) {
                base.RemoveControl(btnUp);
            }
            btnUp = new Button();
            btnUp.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(btnUp_OnClick);
            btnUp.Location = new Point(0, 0);
            btnUp.Size = new Size(this.Width, buttonHeight);
            btnUp.Backcolor = SystemColors.Control;
            this.AddControl(btnUp);

            if (base.ContainsControl(btnDown)) {
                base.RemoveControl(btnDown);
            }
            btnDown = new Button();
            btnDown.OnClick += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(btnDown_OnClick);
            btnDown.Location = new Point(0, this.Height - buttonHeight);
            btnDown.Size = new Size(this.Width, 10);
            btnDown.Backcolor = SystemColors.Control;
            this.AddControl(btnDown);

            mBackground = new SdlDotNet.Graphics.Surface(this.Width, this.Height - (buttonHeight * 2));
            cursorSurf = new SdlDotNet.Graphics.Surface(7, 12);
            cursorSurf.Fill(Color.Gray);
        }

        void btnDown_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (!inverted) {
                if (Value < max) {
                    Value += step;
                    //if (OnChangeValue != null)
                    //    OnChangeValue(Value, null);
                }
            } else {
                if (Value > 0) {
                    Value -= step;
                    //if (OnChangeValue != null)
                    //    OnChangeValue(Value, null);
                }
            }
        }

        void btnUp_OnClick(object sender, SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (!inverted) {
                if (Value > 0) {
                    Value -= step;
                    //if (OnChangeValue != null)
                    //    OnChangeValue(Value, null);
                }
            } else {
                if (Value < max) {
                    Value += step;
                    //if (OnChangeValue != null)
                    //    OnChangeValue(Value, null);
                }
            }
        }

        #endregion Methods
    }
}