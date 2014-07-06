using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace Client.Logic.Stories.Components
{
    class ScreenImageOverlay
    {
        string file;
        string imageID;
        int x;
        int y;
        Surface surface;

        public string File {
            get { return file; }
        }

        public string ImageID {
            get { return imageID; }
        }

        public int X {
            get { return x; }
        }

        public int Y {
            get { return y; }
        }

        public Surface Surface {
            get { return surface; }
        }

        public ScreenImageOverlay(string file, string imageID, int x, int y) {
            this.file = file;
            this.imageID = imageID;
            this.x = x;
            this.y = y;
        }

        public void LoadImage() {
            surface = Logic.Graphics.SurfaceManager.LoadSurface(IO.Paths.StoryDataPath + "Images/" + file);
        }
    }
}
