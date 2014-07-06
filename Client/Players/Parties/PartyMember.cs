using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Players.Parties
{
    class PartyMember
    {
        public string Name { get; set; }
        public int MugshotNum { get; set; }
        public int MugshotForm { get; set; }
        public Enums.Coloration MugshotShiny { get; set; }
        public Enums.Sex MugshotGender { get; set; }
        public ulong Exp { get; set; }
        public ulong MaxExp { get; set; }
        public int HP { get; set; }
        public int MaxHP { get; set; }
    }
}
