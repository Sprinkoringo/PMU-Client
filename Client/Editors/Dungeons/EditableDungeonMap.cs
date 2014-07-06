using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.Dungeons
{
    class EditableDungeonMap
    {
        public int MapNumber {get;set;}
        public string Name {get;set;}
        public int Difficulty {get;set;}
        public bool IsBadGoalMap {get;set;}
        public string GoalName {get;set;}
        public int FloorNumber{get;set;}
    }
}
