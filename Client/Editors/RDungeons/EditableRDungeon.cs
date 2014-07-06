using System;
    using System.Collections.Generic;
    using System.Text;

namespace Client.Logic.Editors.RDungeons
{
	/// <summary>
	/// Description of EditableRDungeon.
	/// </summary>
	class EditableRDungeon
	{
		public string DungeonName { get; set; }
        public Enums.Direction Direction { get; set; }
        public int MaxFloors { get; set; }
        public bool Recruitment { get; set; }
        public bool Exp { get; set; }
        public int WindTimer { get; set; }
        public int DungeonIndex { get; set; }

        public List<EditableRDungeonFloor> Floors { get; set; }
        
        

        

        public int RDungeonIndex;

        public EditableRDungeon(int rDungeonIndex) {
            RDungeonIndex = rDungeonIndex;
            DungeonName = "";
            Floors = new List<EditableRDungeonFloor>();
            
        }
	}
}
