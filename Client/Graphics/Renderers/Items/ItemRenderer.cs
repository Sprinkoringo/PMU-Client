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
