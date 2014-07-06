using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Algorithms.Pathfinder
{
    interface IPathfinder
    {
        PathfinderResult FindPath(int startX, int startY, int endX, int endY);
    }
}
