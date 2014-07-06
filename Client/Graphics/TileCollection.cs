namespace Client.Logic.Graphics
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using PMU.Core;

    using Gfx = SdlDotNet.Graphics;
    using SdlDotNet.Graphics;

    /// <summary>
    /// Description of TileCollection.
    /// </summary>
    public class TileCollection
    {
        #region Fields

        Size size;
        List<Bitmap> tiles;
        int tilesetNumber;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes an instance of the tiles collection class and loads the tiles
        /// </summary>
        /// <param name="filePath">The path of the full tilesheet</param>
        /// <param name="tileWidth">The width of each tile</param>
        /// <param name="tileHeight">The height of each tile</param>
        public TileCollection(int tilesetNumber, string filePath, int tileWidth, int tileHeight) {
            this.tilesetNumber = tilesetNumber;
            tiles = new List<Bitmap>();
            using (Bitmap fullSurface = (Bitmap)Image.FromFile(filePath))
            {
                size = fullSurface.Size;
                Point blitPoint = new Point(0, 0);
                for (int tileY = 0; tileY * tileHeight < fullSurface.Height; tileY++)
                {
                    for (int tileX = 0; tileX * tileWidth < fullSurface.Width; tileX++)
                    {
                        Bitmap tile = new Bitmap(tileWidth, tileHeight, System.Drawing.Imaging.PixelFormat.Format32bppArgb);
                        using (Graphics gr = Graphics.FromImage(tile))
                        {
                            gr.DrawImage(fullSurface, 0, 0, new Rectangle(tileX * tileWidth, tileY * tileHeight, tileWidth, tileHeight), GraphicsUnit.Pixel);
                        }
                        //tile.TransparentColor = transparentColor;
                        //tile.Transparent = true;
                        this.tiles.Add(tile);
                    }
                }
            }
        }

        #endregion Constructors

        #region Properties

        public Size Size {
            get { return size; }
        }

        public int Count {
            get { return tiles.Count; }
        }

        public int TilesetNumber {
            get { return tilesetNumber; }
        }

        public List<Bitmap> Tiles {
            get { return tiles; }
        }

        #endregion Properties

        #region Indexers

        /// <summary>
        /// Gets a tile from this tile collection
        /// </summary>
        public Bitmap this[int tileNum] {
            get {
                if (tileNum > -1 && tileNum < tiles.Count) {
                    return tiles[tileNum];
                } else {
                    return tiles[0];
                }
            }
        }

        #endregion Indexers

        #region Methods

        public void ConvertToPMUTile(string destinationPath) {
            // File format:
            // [tileset-width(4)][tileset-height(4)][tile-count(4)]
            // [tileposition-1(4)][tilesize-1(4)][tileposition-2(4)][tilesize-2(4)][tileposition-n(n*4)][tilesize-n(n*4)]
            // [tile-1(variable)][tile-2(variable)][tile-n(variable)]
            int position = 0;
            using (System.IO.FileStream stream = new System.IO.FileStream(destinationPath, System.IO.FileMode.Create, System.IO.FileAccess.Write)) {
                // Write tileset width
                stream.Write(ByteArray.IntToByteArray(size.Width), 0, 4);
                position += 4;
                // Write tileset height
                stream.Write(ByteArray.IntToByteArray(size.Height), 0, 4);
                position += 4;
                // Write the number of tiles
                stream.Write(ByteArray.IntToByteArray(tiles.Count), 0, 4);
                position += 4;

                // Calculate the size of the tile information header
                int tileInfoHeaderSize = 0;
                // Each tile uses 4 bytes for it's position
                tileInfoHeaderSize += 4;
                // And 4 bytes for the tile size
                tileInfoHeaderSize += 4;
                // Multiply that by the number of tiles...
                tileInfoHeaderSize *= tiles.Count;

                int tileDataStart = 0;
                // Add the current position
                tileDataStart += position;
                // Add the size of the tile info header
                tileDataStart += tileInfoHeaderSize;

                // Write header information about each tile
                int tileDataPosition = tileDataStart;
                for (int i = 0; i < tiles.Count; i++) {
                    int tileSize = GetSurfaceSize(tiles[i]);
                    // Write the position of the tile
                    stream.Write(ByteArray.IntToByteArray(tileDataPosition), 0, 4);
                    // Write the size of the tile
                    stream.Write(ByteArray.IntToByteArray(tileSize), 0, 4);
                    // Add the tile size to the position
                    tileDataPosition += tileSize;
                }

                // Now that all header is written, write tile data
                for (int i = 0; i < tiles.Count; i++) {
                    using (System.Drawing.Image img = tiles[i]) {
                        using (System.IO.MemoryStream ms = new System.IO.MemoryStream()) {
                            img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                            byte[] encryptedBytes = ms.ToArray();
                            stream.Write(encryptedBytes, 0, encryptedBytes.Length);
                        }
                    }
                }
            }
        }

        private int GetSurfaceSize(System.Drawing.Image img)
        {
            using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
            {
                img.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray().Length;
            }
        }

        #endregion Methods
    }
}