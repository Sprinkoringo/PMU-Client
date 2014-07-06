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

namespace Client.Logic.Maps
{
    class MapProperties
    {
        public string Name { get; set; }
        public int Right { get; set; }
        public int Left { get; set; }
        public int Up { get; set; }
        public int Down { get; set; }
        public string Music { get; set; }
        public int MaxX { get; set; }
        public int MaxY { get; set; }
        public Enums.MapMoral Moral { get; set; }
        public Enums.Weather Weather { get; set; }
        public int Darkness { get; set; }
        public bool Indoors { get; set; }
        public bool Belly { get; set; }
        public bool Recruit { get; set; }
        public bool Exp { get; set; }
        public int TimeLimit { get; set; }
        public bool Instanced { get; set; }
        public int DungeonIndex { get; set; }
        public List<MapNpcSettings> Npcs { get; set; }
        public int MinNpcs { get; set; }
        public int MaxNpcs { get; set; }
        public int NpcSpawnTime { get; set; }

        public MapProperties() {
            Npcs = new List<MapNpcSettings>();
        }
    }
}
