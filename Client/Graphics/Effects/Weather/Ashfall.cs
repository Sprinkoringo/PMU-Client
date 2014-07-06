using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class Ashfall : IWeather
    {
        #region Fields

        bool disposed;

        List<Ash> ashes = new List<Ash>();

        #endregion Fields

        #region Constructors

        public Ashfall() {
            disposed = false;

            for (int i = 0; i < 40; i++) {
                ashes.Add(new Ash());
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
            for (int i = ashes.Count - 1; i >= 0; i--) {
                ashes[i].Dispose();
                ashes.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            for (int i = 0; i < ashes.Count; i++) {
                ashes[i].UpdateLocation();
                destData.Blit(ashes[i], new Point(ashes[i].X, ashes[i].Y));
            }
        }

        #endregion Methods

        public Enums.Weather ID {
            get { return Enums.Weather.Ashfall; }
        }
    }
}
