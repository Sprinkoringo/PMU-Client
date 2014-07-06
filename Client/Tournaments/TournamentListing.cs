using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Tournaments
{
    class TournamentListing
    {
        string tournamentID;
        string name;
        string mainAdmin;

        public string Name {
            get { return name; }
        }

        public string TournamentID {
            get { return tournamentID; }
        }

        public string MainAdmin {
            get { return this.mainAdmin; }
        }

        public TournamentListing(string name, string tournamentID, string mainAdmin) {
            this.name = name;
            this.tournamentID = tournamentID;
            this.mainAdmin = mainAdmin;
        }
    }
}
