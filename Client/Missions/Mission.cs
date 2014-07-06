namespace Client.Logic.Missions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Mission
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

        //public Mission(string missionName, string newSummary, string goal, int species, int form, JobDifficulty difficulty, int itemNum, int amount) {
        //    this.Title = missionName;
        //    this.Summary = newSummary;
        //    this.GoalName = goal;
        //    this.ClientSpecies = species;
        //    this.ClientForm = form;
        //    this.Difficulty = difficulty;
        //    this.RewardNum = itemNum;
        //    this.RewardAmount = amount;
        //}
    }
}