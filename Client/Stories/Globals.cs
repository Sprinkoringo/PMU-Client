/*
 * Created by SharpDevelop.
 * User: Pikachu
 * Date: 27/09/2009
 * Time: 11:15 PM
 * 
 */
using System;

namespace Client.Logic.Stories
{
    /// <summary>
    /// Global variables for stories
    /// </summary>
    internal class Globals
    {
        public static bool PlayersHidden { get; set; }
        public static bool NpcsHidden { get; set; }
        public static bool IsScrolling { get; set; }
        public static int ScrollCurrentX { get; set; }
        public static int ScrollCurrentY { get; set; }
        public static int ScrollEndX { get; set; }
        public static int ScrollEndY { get; set; }
        public static int LastScroll { get; set; }
        public static int ScrollSpeed { get; set; }
    }
}
