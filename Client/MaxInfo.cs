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