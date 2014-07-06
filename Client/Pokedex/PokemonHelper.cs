using System;
    using System.Collections.Generic;
    using System.Text;
    using PMU.Core;

namespace Client.Logic.Pokedex
{
	
	class PokemonHelper
	{
		#region Fields

        private static int dataLoadPercent = 0;
        private static PokemonCollection mPokemon;

        #endregion Fields

        #region Properties

        public static int DataLoadPercent
        {
            get { return dataLoadPercent; }
        }

        public static PokemonCollection Pokemon
        {
            get { return mPokemon; }
        }

        #endregion Properties

        #region Methods

        public static void InitPokemonCollection()
        {
            mPokemon = new PokemonCollection();
        }

        public static void LoadPokemonFromPacket(string[] parse)
        {
            try {
                int n = 2;
                MaxInfo.TotalPokemon = parse[1].ToInt();
                mPokemon.ClearPokemon();
                if (MaxInfo.TotalPokemon > 0) {
                    for (int i = 0; i < MaxInfo.TotalPokemon; i++) {
                        dataLoadPercent = System.Math.Min(99, Logic.MathFunctions.CalculatePercent(i, MaxInfo.TotalPokemon));
                        mPokemon.AddPokemon(i, new Pokemon());
                        mPokemon[i].Name = parse[n];
                        int count = parse[n + 1].ToInt();
                        n += 2;
                        //for (int j = 0; j < count; j++) {
                        //    mPokemon[i].FormSprites.Add(parse[n + j].ToInt());
                        //}
                        //n += count;
                        ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Receiving Data... " + DataManager.AverageLoadPercent().ToString() + "%");
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
