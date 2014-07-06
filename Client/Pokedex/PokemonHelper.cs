/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


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
