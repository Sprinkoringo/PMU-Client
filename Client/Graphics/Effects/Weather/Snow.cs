using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Snow : IWeather
    {
        #region Fields

        bool disposed;

        List<Snowflake> snowflakes = new List<Snowflake>();

        #endregion Fields

        #region Constructors

        public Snow() {
            disposed = false;

            for (int i = 0; i < 50; i++) {
                snowflakes.Add(new Snowflake());
            }
        }

        #endregion Constructors

        #region Properties

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            for (int i = snowflakes.Count - 1; i >= 0; i--) {
                snowflakes[i].Dispose();
                snowflakes.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < snowflakes.Count; i++) {
                snowflakes[i].UpdateLocation(1);
                destData.Blit(snowflakes[i], new Point(snowflakes[i].X, snowflakes[i].Y));
            }
        }

        #endregion Methods

        public Enums.Weather ID {
            get { return Enums.Weather.Snowing; }
        }
    }
}
