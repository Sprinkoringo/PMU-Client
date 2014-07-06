using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.Dungeons
{
    class EditableRandomDungeonMap
    {
        public int RDungeonIndex { get; set; }
        public int RDungeonFloor { get; set; }
        public Enums.JobDifficulty Difficulty { get; set; }
        public bool IsBadGoalMap { get; set; }
    }
}
