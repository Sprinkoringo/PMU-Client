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
