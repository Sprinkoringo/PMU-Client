using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Graphics.Effects.Overlays;
using SdlDotNet.Graphics;
using System.Drawing;
using SdlDotNet.Graphics.Sprites;

namespace Client.Logic.Graphics.Effects.Weather
{
    class DiamondDust : IWeather
    {
        #region Fields

        bool disposed;

        List<Diamond> diamonds = new List<Diamond>();

        #endregion Fields

        #region Constructors

        public DiamondDust()
        {
            disposed = false;

            for (int i = 0; i < 80; i++)
            {
                diamonds.Add(new Diamond());
            }
        }

        #endregion Constructors

        #region Properties

        public bool Disposed
        {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources()
        {
            disposed = true;
            for (int i = diamonds.Count - 1; i >= 0; i--)
            {
                diamonds[i].Dispose();
                diamonds.RemoveAt(i);
            }
        }

        public void Render(Renderers.RendererDestinationData destData, int tick)
        {
            for (int i = 0; i < diamonds.Count; i++)
            {
                diamonds[i].UpdateLocation();
                destData.Blit(diamonds[i], new Point(diamonds[i].X, diamonds[i].Y));
            }
        }

        #endregion Methods

        public Enums.Weather ID
        {
            get { return Enums.Weather.DiamondDust; }
        }
    }
}
