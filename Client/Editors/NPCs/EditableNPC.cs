using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Editors.NPCs
{
    class EditableNPC
    {
        #region Constructors

        public EditableNPC() {
            Moves = new int[4];
            Drops = new EditableNpcDrop[MaxInfo.MAX_NPC_DROPS];
        }

        #endregion Constructors

        #region Properties

        public string Name {
            get;
            set;
        }

        public string AttackSay {
            get;
            set;
        }

        public int Form {
            get;
            set;
        }

        public int Species {
            get;
            set;
        }

        public bool SpawnsAtDawn { get; set; }
        public bool SpawnsAtDay { get; set; }
        public bool SpawnsAtDusk { get; set; }
        public bool SpawnsAtNight { get; set; }

        public int ShinyChance { get; set; }
        public Enums.NpcBehavior Behavior { get; set; }
        public int RecruitRate { get; set; }

        public int[] Moves { get; set; }
        public EditableNpcDrop[] Drops { get; set; }

        public string AIScript { get; set; }

        #endregion Properties
    }
}
