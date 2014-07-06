using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Rain : IWeather
    {
        #region Fields

        bool disposed;
        List<Raindrop> raindrops = new List<Raindrop>();
        

        #endregion Fields

        #region Constructors

        public Rain() {
            disposed = false;
            for (int i = 0; i < 80; i++) {
                raindrops.Add(new Raindrop());
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
            for (int i = raindrops.Count - 1; i >= 0; i--) {
                raindrops[i].Dispose();
                raindrops.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < raindrops.Count; i++) {
                raindrops[i].UpdateLocation(1);
                destData.Blit(raindrops[i], new Point(raindrops[i].X, raindrops[i].Y));
            }
        }

        #endregion Methods

        public Enums.Weather ID {
            get { return Enums.Weather.Raining; }
        }
    }
}
