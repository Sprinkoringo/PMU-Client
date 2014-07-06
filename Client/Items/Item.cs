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