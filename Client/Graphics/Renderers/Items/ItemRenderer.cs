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
using Client.Logic.Items;
using Client.Logic.Maps;
using Client.Logic.Graphics.Renderers.Screen;

namespace Client.Logic.Graphics.Renderers.Items
{
    class ItemRenderer
    {
        public static void DrawMapItem(RendererDestinationData destData, Map map, Enums.MapID targetMapID, int itemSlot) {
            Item item = ItemHelper.Items[map.MapItems[itemSlot].Num];

            Rectangle cropRect = new Rectangle((item.Pic - (item.Pic / 6) * 6) * Constants.TILE_WIDTH,
                                               (item.Pic / 6) * Constants.TILE_HEIGHT, Constants.TILE_WIDTH, Constants.TILE_HEIGHT);

            int itemX= map.MapItems[itemSlot].X;
            int itemY = map.MapItems[itemSlot].Y;

            Renderers.Maps.SeamlessWorldHelper.ConvertCoordinatesToBorderless(map, targetMapID, ref itemX, ref itemY);
            Point dstPoint = new Point(ScreenRenderer.ToScreenX(itemX * Constants.TILE_WIDTH),
                                       ScreenRenderer.ToScreenY(itemY * Constants.TILE_HEIGHT));

            //Surface itemSurface = new Surface(32,32);
            //itemSurface.Blit(Graphics.GraphicsManager.Items, cropRect);

            //if (darkness != null && !darkness.Disposed) {
            //    Point darknessPoint = new Point(darkness.Buffer.Width / 2 + dstPoint.X - darkness.Focus.X, darkness.Buffer.Height / 2 + dstPoint.Y - darkness.Focus.Y);
            //    Surface darknessSurface = new Surface(32, 32);
            //    darknessSurface.Blit(darkness.Buffer, new Point(0, 0), new Rectangle(darknessPoint, new Size(Constants.TILE_WIDTH, Constants.TILE_HEIGHT)));

            //}
            //destData.Blit(itemSurface, dstPoint);
            destData.Blit(Graphics.GraphicsManager.Items, dstPoint, cropRect);

        }
    }
}
