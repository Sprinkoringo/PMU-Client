using System;
	using System.Collections.Generic;
	using System.Text;
	using PMU.Core;

namespace Client.Logic.Dungeons
{
	
	class DungeonHelper
	{
		#region Fields

		private static int dataLoadPercent = 0;
		private static DungeonCollection mDungeons;

		#endregion Fields

		#region Properties

		public static int DataLoadPercent
		{
			get { return dataLoadPercent; }
		}

		public static DungeonCollection Dungeons
		{
			get { return mDungeons; }
		}

		#endregion Properties

		#region Methods

		public static void InitDungeonCollection()
		{
			mDungeons = new DungeonCollection();
		}

		public static void LoadDungeonsFromPacket(string[] parse)
		{
			try {
				int n = 2;
				MaxInfo.MaxDungeons = parse[1].ToInt();
				mDungeons.ClearDungeons();
				if (MaxInfo.MaxDungeons > 0) {
					for (int i = 0; i < MaxInfo.MaxDungeons; i++) {
						dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.MaxDungeons));
						mDungeons.AddDungeon(i, new Dungeon());
						mDungeons[i].Name = parse[n];
						n += 1;
						((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Recieving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
					}
					dataLoadPercent = 100;
				}
			} catch (Exception ex) {
				Exceptions.ExceptionHandler.OnException(ex);
			}
		}

		#endregion Methods
	}
}
