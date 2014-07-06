using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Editors.Dungeons
{
    class EditableDungeon
    {
        public string Name { get; set; }
        public bool AllowsRescue { get; set; }

        public List<EditableStandardDungeonMap> StandardMaps { get; set; }
        public List<EditableRandomDungeonMap> RandomMaps { get; set; }

        public ListPair<int, string> ScriptList { get; set; }

        public EditableDungeon() {
            StandardMaps = new List<EditableStandardDungeonMap>();
            RandomMaps = new List<EditableRandomDungeonMap>();
            ScriptList = new ListPair<int, string>();
        }
    }
}
