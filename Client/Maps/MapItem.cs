#region Header

/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 27/09/2009
 * Time: 11:47 AM
 *
 */

#endregion Header

namespace Client.Logic.Maps
{
	using System;

	/// <summary>
	/// Description of MapItem.
	/// </summary>
   [Serializable]
	internal class MapItem
	{
		#region Constructors

		public MapItem()
		{
		}

		#endregion Constructors

		#region Properties

		public bool Sticky
		{
			get; set;
		}

		public int Num
		{
			get; set;
		}

		public int Value
		{
			get; set;
		}

		public int X
		{
			get; set;
		}

		public int Y
		{
			get; set;
		}

		#endregion Properties
	}
}