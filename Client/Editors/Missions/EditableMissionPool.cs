using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Editors.Missions {
    class EditableMissionPool {

        public List<EditableMissionReward> Rewards { get; set; }
        public List<int> Enemies { get; set; }
        public List<EditableMissionClient> Clients { get; set; }

        public EditableMissionPool() {
            Rewards = new List<EditableMissionReward>();
            Clients = new List<EditableMissionClient>();
            Enemies = new List<int>();
        }
    }
}
