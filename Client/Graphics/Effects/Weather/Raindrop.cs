/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Raindrop : Surface
    {
        float speed;
        float wind;
        float delta = 0.1f;

        public int X;
        public int Y;
        //public int terminalY;

        public Raindrop()
            : base(new Surface(32, 32)) {
            Initialize();
            Reset();
            Y = -1 * Logic.MathFunctions.Random.Next(5000 - base.Height);
        }

        void Initialize() {
            
            base.Blit(GraphicsManager.Tiles[10][6], new Point(0, 0));
            base.Transparent = true;
            //base.Surface.TransparentColor = Color.FromArgb(255, 0, 255);
            //base.Rectangle = new Rectangle(this.Width, this.Height, 0, 0);
        }

        void Reset() {
            wind = Logic.MathFunctions.Random.Next(3) / 10.0f;

            X = (int)Logic.MathFunctions.Random.Next(-1 * (int)(wind * 640), 720 - base.Width) - 128;
            Y = -64; // (int)Logic.Math.Random.Next(-1 * (int)(wind * 640), 640 - base.Width);//0 - base.Width + (Logic.Math.Random.Next(100));

            //terminalY = (int)Logic.Math.Random.Next(0, 640 - base.Width);

            speed = Logic.MathFunctions.Random.Next(100, 150);
            //base.Draw(new SdlDotNet.Graphics.Primitives.Line(0, 0, (short)(speed / 6), (short)(speed / 3)), Color.Blue);
            
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

            this.X += (int)(change / 2) * intensity;
            this.Y += (int)change * intensity;
        //    this.Y += (int)System.Math.Ceiling(change * wind);

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
