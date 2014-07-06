using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using System.Drawing;
using SdlDotNet.Core;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Diamond : Surface
    {
        float speed;
        float wind;
        float delta = 0.05f;

        public int X;
        public int Y;

        /// <summary>
        /// 
        /// </summary>
        public Diamond()
            : base(new Surface(32, 32))
        {
            Initialize();
            Reset();
            Y = -1 * Logic.MathFunctions.Random.Next(500 - base.Height);
        }

        void Initialize()
        {
            base.Blit(GraphicsManager.Tiles[10][197], new Point(0, 0));
            base.Transparent = true;
            //base.Surface.TransparentColor = Color.FromArgb(255, 0, 255);
            //base.Rectangle = new Rectangle(this.Width, this.Height, 0, 0);
        }

        void Reset()
        {
            wind = Logic.MathFunctions.Random.Next(3) / 30.0f;

            X = (int)Logic.MathFunctions.Random.Next(-1 * (int)(wind * 640), 640 - base.Width);
            Y = 0 - base.Width;

            speed = Logic.MathFunctions.Random.Next(40, 120);

            
            base.AlphaBlending = true;
            base.Alpha = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        public void UpdateLocation()
        {
            float change = delta * speed;

            //base.Alpha = (byte)((this.Y % 64)/ 4 /* 16 + 1*/);
            
            switch ((this.Y % 64) / 4)
            {
                case 0:
                    {
                        base.Alpha = 255;
                    }
                    break;
                case 15: case 1:
                    {
                        base.Alpha = 192;
                    }
                    break;
                case 14: case 2:
                    {
                        base.Alpha = 128;
                    }
                    break;
                default:
                    {
                        base.Alpha = 0;
                    }
                    break;

            }
            
            //base.Alpha = (byte)System.Math.Max(0, (System.Math.Abs(this.Y % 64 - 32) - 24)* 32 - 1);
            this.Y += (int)change;
            this.X += (int)System.Math.Ceiling(change * wind);

            if (this.Y > 480)
            {
                Reset();
            }
        }
        #region IDisposable
        private bool disposed;

        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">If true, dispose unmanaged resources</param>
        protected override void Dispose(bool disposing)
        {
            try
            {
                if (!this.disposed)
                {
                    if (disposing)
                    {
                    }
                    this.disposed = true;
                }
            }
            finally
            {
                base.Dispose(disposing);
            }
        }
        #endregion IDisposable
    }
}
