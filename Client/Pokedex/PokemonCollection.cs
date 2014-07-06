using System;
    using System.Collections.Generic;
    using System.Text;

namespace Client.Logic.Pokedex
{
	/// <summary>
	/// Description of PokemonCollection.
	/// </summary>
	class PokemonCollection
	{
		#region Fields

        private PMU.Core.ListPair<int, Pokemon> mPokemon;

        #endregion Fields

        #region Constructors

        internal PokemonCollection()
        {
            mPokemon = new PMU.Core.ListPair<int, Pokemon>();
        }

        #endregion Constructors

        #region Indexers

        public Pokemon this[int index]
        {
            get { return mPokemon[index]; }
            set { mPokemon[index] = value; }
        }

        #endregion Indexers

        #region Methods

        public void AddPokemon(int index, Pokemon pokemonToAdd)
        {
            mPokemon.Add(index, pokemonToAdd);
        }

        public void ClearPokemon()
        {
            mPokemon.Clear();
        }

        #endregion Methods
	}
}
