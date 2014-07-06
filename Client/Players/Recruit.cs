namespace Client.Logic.Players
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Recruit
    {
        #region Constructors

        public Recruit() {
            Loaded = false;
            
        }

        #endregion Constructors

        #region Properties

        public int Num
        {
            get;
            set;
        }
        public int Form
        {
            get;
            set;
        }
        public Enums.Coloration Shiny
        {
            get;
            set;
        }

        public bool Loaded {
            get;
            set;
        }


        public string Name {
            get;
            set;
        }

        public int HP {
            get;
            set;
        }

        public int MaxHP
        {
            get;
            set;
        }

        public Enums.Sex Sex{
            get;
            set;
        }

        public Enums.StatusAilment StatusAilment
        {
            get;
            set;
        }

        public int HeldItemSlot { get; set; }

        public int ExpPercent { get; set; }

        public int Level { get; set; }
        
        

        #endregion Properties
    }
}