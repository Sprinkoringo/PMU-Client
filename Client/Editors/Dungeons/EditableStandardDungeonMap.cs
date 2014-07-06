using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.Dungeons
{
    class EditableStandardDungeonMap
    {
        public int MapNum {get;set;}
        public Enums.JobDifficulty Difficulty {get;set;}
        public bool IsBadGoalMap {get;set;}
    }
}
