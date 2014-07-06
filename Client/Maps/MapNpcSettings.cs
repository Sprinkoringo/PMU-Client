using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Maps
{
    [Serializable]
    class MapNpcSettings
    {
        public int NpcNum { get; set; }
        public int SpawnX { get; set; }
        public int SpawnY { get; set; }
        public int MinLevel { get; set; }
        public int MaxLevel { get; set; }

        public int AppearanceRate { get; set; }

        public Enums.StatusAilment StartStatus { get; set; }
        public int StartStatusCounter { get; set; }
        public int StartStatusChance { get; set; }

        public MapNpcSettings() {
            // If the NpcNum -2, the server will not save this MapNpc
            NpcNum = -2;
        }

        public override bool Equals(object obj) {
            if (!(obj is MapNpcSettings)) return false;
            MapNpcSettings npc = obj as MapNpcSettings;

            if (NpcNum != npc.NpcNum) return false;
            if (SpawnX != npc.SpawnX) return false;
            if (SpawnY != npc.SpawnY) return false;
            if (MinLevel != npc.MinLevel) return false;
            if (MaxLevel != npc.MaxLevel) return false;
            if (AppearanceRate != npc.AppearanceRate) return false;
            if (StartStatus != npc.StartStatus) return false;
            if (StartStatusCounter != npc.StartStatusCounter) return false;
            if (StartStatusChance != npc.StartStatusChance) return false;
            return true;
        }
    }
}
