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
