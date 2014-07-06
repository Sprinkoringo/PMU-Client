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
