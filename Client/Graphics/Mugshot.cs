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
    class Mugshot : ICacheable
    {
         #region Fields

        int num;
        List<Surface> sheet;
        int sizeInBytes;
        string form;
        int frameCount;

        #endregion Fields

        #region Constructors

        public Mugshot(int num, string form)
        {
            this.num = num;
            this.form = form;
            sheet = new List<Surface>();
        }

        #endregion Constructors



        #region Properties

        public int Num {
            get { return num; }
        }

        public int FrameCount
        {
            get { return frameCount; }
        }

        #endregion Properties

        public int BytesUsed {
            get { return sizeInBytes; }
        }

        public Surface GetEmote(int index)
        {
            if (index >= 0 && index < sheet.Count)
            {
                return sheet[index];
            }
            return null;
        }

        public void LoadFromData(byte[] data)
        {
            Surface sheetSurface = new Surface(data);
            for (int i = 0; i < sheetSurface.Width; i += sheetSurface.Height)
            {
                Surface emote = new Surface(sheetSurface.Height, sheetSurface.Height);
                emote.Blit(sheetSurface, new System.Drawing.Point(), new System.Drawing.Rectangle(i, 0, sheetSurface.Height, sheetSurface.Height));
                sheet.Add(emote);
            }

            this.sizeInBytes = data.Length;
        }
    }
}
