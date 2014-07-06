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
