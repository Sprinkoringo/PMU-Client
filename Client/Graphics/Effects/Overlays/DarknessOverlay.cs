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


namespace Client.Logic.Graphics.Effects.Overlays {
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Graphics;

    class DarknessOverlay : IOverlay {
        #region Fields

        Surface buffer;
        bool disposed;

        int range;

        #endregion Fields

        
        #region Constructors

        public DarknessOverlay() {
            disposed = false;
            buffer = new Surface(20 * Constants.TILE_WIDTH, 15 * Constants.TILE_HEIGHT);
            for (int x = 0; x < 20; x++) {
                for (int y = 0; y < 15; y++) {
                    buffer.Blit(GraphicsManager.Tiles[10][45], new Point(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT));
                }
            }
            buffer.AlphaBlending = true;
            buffer.Alpha = 120;
        }

        public DarknessOverlay(int newRange) {
            disposed = false;
            range = newRange;
            buffer = new Surface(40 * Constants.TILE_WIDTH, 30 * Constants.TILE_HEIGHT);
            Surface holepart = new Surface(14 * Constants.TILE_WIDTH, 14 * Constants.TILE_HEIGHT);
            for (int x = 0; x < 14; x++) {
                for (int y = 0; y < 14; y++) {
                    holepart.Blit(GraphicsManager.Tiles[10][210 + x + 14 * y], new Point(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT));
                }
            }

            Surface hole = new Surface(28 * Constants.TILE_WIDTH, 28 * Constants.TILE_HEIGHT);
            hole.Blit(holepart, new Point(0,0));
            hole.Blit(holepart.CreateFlippedHorizontalSurface(), new Point(hole.Width / 2, -2));
            hole.Blit(holepart.CreateFlippedVerticalSurface(), new Point(0, hole.Height / 2));
            hole.Blit(holepart.CreateFlippedVerticalSurface().CreateFlippedHorizontalSurface(), new Point(hole.Width / 2, hole.Height / 2 - 2));
            hole = hole.CreateStretchedSurface(new Size((range) * Constants.TILE_WIDTH, (range) * Constants.TILE_HEIGHT));

            buffer.Blit(GraphicsManager.Tiles[10][45].CreateStretchedSurface(new Size(40 * Constants.TILE_WIDTH, 30 * Constants.TILE_HEIGHT)), new Point(0,0));
            buffer.Blit(hole, new Point(buffer.Width / 2 - hole.Width / 2, buffer.Height / 2 - hole.Height / 2));

            //buffer = hole;
            buffer.Transparent = true;

            buffer.AlphaBlending = true;
            buffer.Alpha = 180;
        }

        #endregion Constructors

        #region Properties

        public SdlDotNet.Graphics.Surface Buffer {
            get { return buffer; }
        }

        public bool Disposed {
            get { return disposed; }
        }

        public Point Focus { get; set; }

        public int Range { get { return range;} }

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

        public void Render(Renderers.RendererDestinationData destData, int tick, Point focus) {
            // We don't need to render anything as this overlay isn't animated and always remains the same
            destData.Blit(buffer, new Point(focus.X - buffer.Width / 2, focus.Y - buffer.Height / 2));
        }

        #endregion Methods
    }
}