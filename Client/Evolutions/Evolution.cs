namespace Client.Logic.Evolutions
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Evolution
    {
        
        #region Properties

        
        public string Name {get; set;}
        public int Species {get; set; }
        
        public List<EvolutionBranch> Branches { get; set; }

        #endregion Properties

        
        public Evolution() {
            //if (!splitEvo) {
            //    splitEvos = new Evolution[1];
            //    splitEvos[0] = new Evolution(true);
            //}
            Branches = new List<EvolutionBranch>();
        }
    }
}