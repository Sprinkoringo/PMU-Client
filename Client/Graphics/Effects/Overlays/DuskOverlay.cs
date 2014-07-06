
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using SdlDotNet.Graphics;

namespace Client.Logic.Graphics.Effects.Overlays
{
    class DuskOverlay : IOverlay
    {
        #region Fields

        Surface buffer;
        bool disposed;

        #endregion Fields

        #region Constructors

        public DuskOverlay() {
            disposed = false;
            buffer = new Surface(20 * Constants.TILE_WIDTH, 15 * Constants.TILE_HEIGHT);
            for (int x = 0; x < 20; x++) {
                for (int y = 0; y < 15; y++) {
                    buffer.Blit(GraphicsManager.Tiles[10][63], new Point(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT));
                }
            }
            buffer.AlphaBlending = true;
            buffer.Alpha = 60;
        }

        #endregion Constructors

        #region Properties

        public SdlDotNet.Graphics.Surface Buffer {
            get { return buffer; }
        }

        public bool Disposed {
            get { return disposed; }
        }

        #endregion Properties

        #region Methods

        public void FreeResources() {
            disposed = true;
            buffer.Dispose();
        }

        public void Render(Renderers.RendererDestinationData destData, int tick) {
            // We don't need to render anything as this overlay isn't animated and always remains the same
            destData.Blit(buffer, new Point(0, 0));
        }

        #endregion Methods
    }
}