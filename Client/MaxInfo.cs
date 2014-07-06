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


namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    static class MaxInfo
    {
        #region Fields

        public const int MAX_ACTIVETEAM = 4;
        public const int MAX_ARROWS = 100;
        public const int MAX_MAP_NPCS = 20;
        public const int MAX_NPC_DROPS = 10;
        public const int MAX_PLAYER_ARROWS = 100;
        public const int MAX_PLAYER_MOVES = 4;
        public const int MAX_TRADES = 100;

        public static string GameName;
        public static int MaxInv;
        //public static int MaxBank;
        public static int MaxEmoticons;
        public static int MaxEvolutions;
        public static int MaxItems;
        public static int MaxMapItems;
        public static int MaxMapX;
        public static int MaxMapY;
        public static int MaxNpcs;
        public static int MaxShops;
        public static int MaxSpellAnim = 0;
        public static int MaxMoves;
        public static int MaxStories;
        public static int MaxRDungeons;
        public static int MaxDungeons;
        public static int TotalPokemon;
        public static bool Paperdoll = false;
        public static string Website = "";

        #endregion Fields
    }
}