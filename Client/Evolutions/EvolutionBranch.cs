using System;
    using System.Collections.Generic;
    using System.Text;

namespace Client.Logic.Evolutions
{
	/// <summary>
	/// Description of EvolutionBranch.
	/// </summary>
	class EvolutionBranch
	{
		#region Properties
		
		public string Name {get; set; }
		public int NewSpecies { get; set; }
		public int ReqScript { get; set; }
		public int Data1 { get; set; }
		public int Data2 { get; set; }
		public int Data3 { get; set; }
		
		#endregion
		
		public EvolutionBranch() {
			Name = "";
		}
	}
}
