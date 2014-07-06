namespace Client.Logic.Moves
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Move
    {
        #region Properties

        public string Name {
            get;
            set;
        }

        public Enums.MoveRange RangeType {
            get;
            set;
        }

        public int Range {
            get;
            set;
        }

        public Enums.MoveTarget TargetType {
            get;
            set;
        }


        public int HitTime {
            get;
            set;
        }


        public bool HitFreeze {
            get;
            set;
        }

        #endregion Properties
    }
}