using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Graphics
{
    class TileCache
    {
        Tileset[] tileSets;

        public TileCache(int maxTileSets) {
            tileSets = new Tileset[maxTileSets];
        }

        public void AddTileset(Tileset tileSet) {
            tileSets[tileSet.TilesetNumber] = tileSet;
        }

        public Tileset this[int index] {
            get { return tileSets[index]; }
        }
    }
}
