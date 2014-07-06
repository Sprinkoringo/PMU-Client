using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using PMU.Core;

namespace Client.Logic.Graphics
{
    class SpellSheet : ICacheable
    {
        Surface sheet;
        int sizeInBytes;

        public int BytesUsed {
            get { return sizeInBytes; }
        }

        public Surface Sheet {
            get { return sheet; }
        }

        public SpellSheet(Surface surface, int sizeInBytes) {
            this.sheet = surface;
            this.sizeInBytes = sizeInBytes;
        }
    }
}
