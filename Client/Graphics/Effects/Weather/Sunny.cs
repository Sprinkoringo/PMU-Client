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
