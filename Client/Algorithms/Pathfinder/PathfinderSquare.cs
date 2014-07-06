using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Algorithms.Pathfinder
{
    class PathfinderSquare
    {
        public int DistanceSteps { get; set; }
        public bool IsPath { get; set; }
        public Enums.TileType TileType { get; set; }
        public string SessionID { get; set; }
    }
}
