using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Maps
{
    class DungeonRoom
    {
         #region Fields

        int height;
        int width;
        int x;
        int y;

        #endregion Fields

        #region Constructors

        public DungeonRoom(int x, int y, int width, int height) {
            this.x = x;
            this.y = y;
            this.width = width;
            this.height = height;
        }

        #endregion Constructors

        #region Properties

        public int Height {
            get { return height; }
            set { height = value; }
        }

        public int Width {
            get { return width; }
            set { width = value; }
        }

        public int X {
            get { return x; }
            set { x = value; }
        }

        public int Y {
            get { return y; }
            set { y = value; }
        }

        #endregion Properties

        public bool IsInRoom(int x, int y) {
            return true;
            return (
               x >= this.x &&
               y >= this.y &&
               x - this.x <= this.width &&
               y - this.y <= this.height
               );
        }
    }
}
