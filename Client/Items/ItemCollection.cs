namespace Client.Logic.Items
{
	using System;
	using System.Collections.Generic;
	using System.Text;

	class ItemCollection
	{
		#region Fields

		private PMU.Core.ListPair<int, Item> mItems;

		#endregion Fields

		#region Constructors

		internal ItemCollection(int maxItems)
		{
			mItems = new PMU.Core.ListPair<int, Item>();
		}

		#endregion Constructors

		#region Indexers

		public Item this[int index]
		{
			get { return mItems[index]; }
			set {
				mItems[index] = value;
			}
		}

		#endregion Indexers
		
		public void AddItem(int index, Item value) {
			mItems.Add(index, value);
		}
	}
}