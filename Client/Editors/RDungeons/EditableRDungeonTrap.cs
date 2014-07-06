using System;
using System.Collections.Generic;
using System.Text;
using Client.Logic.Maps;

namespace Client.Logic.Editors.RDungeons
{
    public class EditableRDungeonTrap
    {
        public Tile SpecialTile { get; set; }
        public int AppearanceRate { get; set; }

        public EditableRDungeonTrap()
        {
            SpecialTile = new Tile();
        }
    }
}
