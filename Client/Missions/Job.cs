using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Missions
{
    class Job
    {
        public string Title { get; set; }
        public string Summary { get; set; }
        public string GoalName { get; set; }
        public int ClientSpecies { get; set; }
        public int ClientForm { get; set; }
        public Enums.JobDifficulty Difficulty { get; set; }
        public Enums.MissionType MissionType { get; set; }
        public int Data1 { get; set; }
        public int Data2 { get; set; }
        public int RewardNum { get; set; }
        public int RewardAmount { get; set; }
        public Enums.JobStatus Accepted { get; set; }
        public bool CanSend { get; set; }

    }
}
