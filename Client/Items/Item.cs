namespace Client.Logic.Items
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Item
    {
        #region Properties

        public string Name { get; set; }
        public string Desc { get; set; }
        public int Pic { get; set; }
        public Enums.ItemType Type { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int Data3 { get; set; }
        public int Price { get; set; }
        public int StackCap { get; set; }
        public bool Bound { get; set; }
        public bool Loseable { get; set; }
        public int Rarity { get; set; }

        public int AttackReq { get; set; }
        public int DefenseReq { get; set; }
        public int SpAtkReq { get; set; }
        public int SpDefReq { get; set; }
        public int SpeedReq { get; set; }
        public int ScriptedReq { get; set; }

        public int AddHP { get; set; }
        public int AddPP { get; set; }
        public int AddAttack { get; set; }
        public int AddDefense { get; set; }
        public int AddSpAtk { get; set; }
        public int AddSpDef { get; set; }
        public int AddSpeed { get; set; }
        public int AddEXP { get; set; }
        public int AttackSpeed { get; set; }
        public int RecruitBonus { get; set; }

        #endregion Properties
        
        public Item() {
        	Name = "";
        	Desc = "";
        }
    }
}