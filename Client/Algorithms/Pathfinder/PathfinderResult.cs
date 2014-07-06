using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Algorithms.Pathfinder
{
    class PathfinderResult
    {
        private List<Enums.Direction> path;
        private int tileCount;
        private bool isPath;

        public List<Enums.Direction> Path {
            get { return path; }
        }

        public int TileCount {
            get { return tileCount; }
        }

        public bool IsPath {
            get { return isPath; }
        }

        int currentItem = 0;

        public Enums.Direction GetNextItem() {
            currentItem++;
            return path[currentItem - 1];
        }

        internal PathfinderResult(List<Enums.Direction> path, bool isPath) {
            this.path = path;
            tileCount = path.Count;
            this.isPath = isPath;
        }
    }
}
