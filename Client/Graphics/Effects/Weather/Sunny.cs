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

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Sunny : IWeather
    {
        #region Fields

        Surface[] buffer;
        bool disposed;

        #endregion Fields

        #region Constructors

        public Sunny() {
            disposed = false;

            buffer = new Surface[6];

            for (int i = 0; i < buffer.Length; i++)
            {
                int height = 8;
                int divide = 2;
                buffer[i] = new Surface(20 * Constants.TILE_WIDTH, height * Constants.TILE_HEIGHT / ((int)System.Math.Pow(divide, i)));
                for (int x = 0; x < 20; x++)
                {
                    for (int y = 0; y < height; y++)
                    {

                        buffer[i].Blit(GraphicsManager.Tiles[10][7/*132 + 14 * (x % 4)*/], new Point(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT / ((int)System.Math.Pow(divide, i))));
                    }
                }
                buffer[i].AlphaBlending = true;

                buffer[i].Alpha = 32;


            }

            
        }

        #endregion Constructors

        #region Properties

        public SdlDotNet.Graphics.Surface Buffer {
            get { return buffer[0]; }
        }

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i].Dispose();
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            // We don't need to render anything as this overlay isn't animated and always remains the same
            int add = (((tick / 4) % 8) - 4)*6;
            if (add < 0)
            {
                add *= -1;
            }

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i].Alpha = (byte)(32 + add);
                destData.Blit(buffer[i], new Point(0, 0));
            }
        }

        #endregion Methods

        public Enums.Weather ID
        {
            get { return Enums.Weather.Sunny; }
        }
    }
}
