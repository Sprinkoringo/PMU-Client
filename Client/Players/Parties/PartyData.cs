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

namespace Client.Logic.Players.Parties
{
    class PartyData
    {
        PartyMember[] members;
        public PartyMember[] Members {
            get { return members; }
        }

        public PartyData() {
            members = new PartyMember[4];
        }

        public void LoadMember(int slot, string name, int mugshot, int form, Enums.Coloration shiny, Enums.Sex gender, ulong exp, ulong maxExp, int hp, int maxHP) {
            members[slot] = new PartyMember();
            members[slot].Name = name;
            members[slot].MugshotNum = mugshot;
            members[slot].MugshotForm = form;
            members[slot].MugshotShiny = shiny;
            members[slot].MugshotGender = gender;
            members[slot].Exp = exp;
            members[slot].MaxExp = maxExp;
            members[slot].HP = hp;
            members[slot].MaxHP = maxHP;
        }

        public void ClearSlot(int slot) {
            members[slot] = null;
        }
    }
}
