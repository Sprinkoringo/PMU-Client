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
