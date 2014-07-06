using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics
{
    public class TileGraphic : ICacheable
    {
        int sizeInBytes;
        Surface tile;
        int tileSet;
        int tileNum;

        public TileGraphic(int tileSet, int tileNum, Surface tile, int sizeInBytes) {
            this.tileSet = tileSet;
            this.tileNum = tileNum;
            this.tile = tile;
            this.sizeInBytes = sizeInBytes;
        }

        public Surface Tile {
            get {
                return tile;
            }
        }

        public int TileSet {
            get { return tileSet; }
        }

        public int TileNum {
            get { return tileNum; }
        }

        public int BytesUsed {
            get { return sizeInBytes; }
        }
    }
}
