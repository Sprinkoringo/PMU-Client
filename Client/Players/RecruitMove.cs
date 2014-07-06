using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Players
{
    class RecruitMove
    {
        

        #region Constructors

        public RecruitMove() {
            MoveNum = -1;
            CurrentPP = -1;
            MaxPP = -1;
        }

        #endregion Constructors

        #region Properties

        public int CurrentPP
        {
            get;
            set;
        }

        public int MaxPP
        {
            get;
            set;
        }

        public int MoveNum
        {
            get;
            set;
        }

        public bool Sealed { get; set; }

        #endregion Properties
    }
}
