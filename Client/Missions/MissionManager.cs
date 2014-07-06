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

namespace Client.Logic.Missions
{
    static class MissionManager
    {


        public static int DetermineMissionExpReward(Enums.JobDifficulty difficulty) {
            switch (difficulty) {
                case Enums.JobDifficulty.E:
                    return 10;
                case Enums.JobDifficulty.D:
                    return 15;
                case Enums.JobDifficulty.C:
                    return 20;
                case Enums.JobDifficulty.B:
                    return 30;
                case Enums.JobDifficulty.A:
                    return 50;
                case Enums.JobDifficulty.S:
                    return 70;
                case Enums.JobDifficulty.Star:
                    return 100;
                case Enums.JobDifficulty.TwoStar:
                    return 200;
                case Enums.JobDifficulty.ThreeStar:
                    return 300;
                case Enums.JobDifficulty.FourStar:
                    return 400;
                case Enums.JobDifficulty.FiveStar:
                    return 500;
                case Enums.JobDifficulty.SixStar:
                    return 700;
                case Enums.JobDifficulty.SevenStar:
                    return 1000;
                case Enums.JobDifficulty.EightStar:
                    return 1300;
                case Enums.JobDifficulty.NineStar:
                    return 1500;
                default:
                    return 0;
            }
        }

        public static int DetermineMissionExpRequirement(Enums.ExplorerRank rank) {
            switch (rank) {
                case Enums.ExplorerRank.Normal:
                    return 0;
                case Enums.ExplorerRank.Bronze:
                    return 100;
                case Enums.ExplorerRank.Silver:
                    return 300;
                case Enums.ExplorerRank.Gold:
                    return 1600;
                case Enums.ExplorerRank.Diamond:
                    return 3200;
                case Enums.ExplorerRank.Super:
                    return 5000;
                case Enums.ExplorerRank.Ultra:
                    return 7500;
                case Enums.ExplorerRank.Hyper:
                    return 10500;
                case Enums.ExplorerRank.Master:
                    return 13500;
                case Enums.ExplorerRank.MasterX:
                    return 17000;
                case Enums.ExplorerRank.MasterXX:
                    return 21000;
                case Enums.ExplorerRank.MasterXXX:
                    return 25000;
                case Enums.ExplorerRank.Guildmaster:
                    return 100000;
                default:
                    return -1;
            }
        }


        public static string RankToString(Enums.ExplorerRank rank) {
            switch (rank) {
                case Enums.ExplorerRank.Normal:
                case Enums.ExplorerRank.Bronze:
                case Enums.ExplorerRank.Silver:
                case Enums.ExplorerRank.Gold:
                case Enums.ExplorerRank.Diamond:
                case Enums.ExplorerRank.Super:
                case Enums.ExplorerRank.Ultra:
                case Enums.ExplorerRank.Hyper:
                case Enums.ExplorerRank.Master:
                case Enums.ExplorerRank.Guildmaster:
                    return rank.ToString();
                case Enums.ExplorerRank.MasterX:
                    return "Master*";
                case Enums.ExplorerRank.MasterXX:
                    return "Master**";
                case Enums.ExplorerRank.MasterXXX:
                    return "Master***";
                default:
                    return "???";
            }
        }

        public static string DifficultyToString(Enums.JobDifficulty difficulty) {
            switch (difficulty) {
                case Enums.JobDifficulty.E:
                case Enums.JobDifficulty.D:
                case Enums.JobDifficulty.C:
                case Enums.JobDifficulty.B:
                case Enums.JobDifficulty.A:
                case Enums.JobDifficulty.S:
                    return difficulty.ToString();
                case Enums.JobDifficulty.Star:
                    return "*1";
                case Enums.JobDifficulty.TwoStar:
                    return "*2";
                case Enums.JobDifficulty.ThreeStar:
                    return "*3";
                case Enums.JobDifficulty.FourStar:
                    return "*4";
                case Enums.JobDifficulty.FiveStar:
                    return "*5";
                case Enums.JobDifficulty.SixStar:
                    return "*6";
                case Enums.JobDifficulty.SevenStar:
                    return "*7";
                case Enums.JobDifficulty.EightStar:
                    return "*8";
                case Enums.JobDifficulty.NineStar:
                    return "*9";
                default:
                    return "?";
            }
        }
    }
}
