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