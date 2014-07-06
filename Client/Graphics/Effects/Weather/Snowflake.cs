using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using SdlDotNet.Graphics.Sprites;
using System.Drawing;
using SdlDotNet.Core;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Snowflake : Surface
    {
        float speed;
        float wind;
        float delta = 0.05f;

        public int X;
        public int Y;

        /// <summary>
        /// 
        /// </summary>
        public Snowflake()
            : base(new Surface(32, 32)) {
            Initialize();
            Reset();
            Y = -1 * Logic.MathFunctions.Random.Next(500 - base.Height);
        }

        void Initialize() {
            base.Blit(GraphicsManager.Tiles[10][14], new Point(0, 0));
            base.Transparent = true;
            //base.Surface.TransparentColor = Color.FromArgb(255, 0, 255);
            //base.Rectangle = new Rectangle(this.Width, this.Height, 0, 0);
        }

        void Reset() {
            wind = Logic.MathFunctions.Random.Next(3) / 10.0f;

            X = (int)Logic.MathFunctions.Random.Next(-1 * (int)(wind * 640), 640 - base.Width);
            Y = 0 - base.Width;

            speed = Logic.MathFunctions.Random.Next(50, 150);

            //base.Surface.Alpha =
            //    (byte)((150 - 50) / (speed - 50) * -255);
            //base.Surface.AlphaBlending = true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="args"></param>
        public void UpdateLocation(int intensity) {
            float change = delta * speed;

            this.Y += (int)change * intensity;
            this.X += (int)System.Math.Ceiling(change * wind) * intensity;

            if (this.Y > 480) {
                Reset();
            }
        }
        #region IDisposable
        private bool disposed;

        /// <summary>
        /// Destroys the surface object and frees its memory
        /// </summary>
        /// <param name="disposing">If true, dispose unmanaged resources</param>
        protected override void Dispose(bool disposing) {
            try {
                if (!this.disposed) {
                    if (disposing) {
                    }
                    this.disposed = true;
                }
            } finally {
                base.Dispose(disposing);
            }
        }
        #endregion IDisposable
    }
}
