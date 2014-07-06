using System;
    using System.Collections.Generic;
    using System.Text;
    using Client.Logic.Maps;

namespace Client.Logic.Editors.RDungeons
{
	class EditableRDungeonItem
    {
        public int ItemNum { get; set; }
    	public int MinAmount { get; set; }
    	public int MaxAmount { get; set; }
    	public int AppearanceRate { get; set; }
    	public int StickyRate { get; set; }
    	public string Tag { get; set; }
    	public bool Hidden { get; set; }
    	public bool OnGround { get; set; }
    	public bool OnWater { get; set; }
    	public bool OnWall { get; set; }
    	
    	public EditableRDungeonItem() {
    		Tag = "";
    	}

        public override bool Equals(object obj) {
            if (!(obj is EditableRDungeonItem)) return false;
            EditableRDungeonItem item = obj as EditableRDungeonItem;

            if (ItemNum != item.ItemNum) return false;
            if (MinAmount != item.MinAmount) return false;
            if (MaxAmount != item.MaxAmount) return false;
            if (AppearanceRate != item.AppearanceRate) return false;
            if (StickyRate != item.StickyRate) return false;
            if (Tag != item.Tag) return false;
            if (Hidden != item.Hidden) return false;
            if (OnGround != item.OnGround) return false;
            if (OnWater != item.OnWater) return false;
            if (OnWall != item.OnWall) return false;
            return true;
        } 
    }
}
