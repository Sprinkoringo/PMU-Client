using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Missions
{
    class JobList
    {
        List<Job> jobs;

        public JobList() {
            jobs = new List<Job>();
        }

        public List<Job> Jobs {
            get {
                return jobs;
            }
        }
    }
}
