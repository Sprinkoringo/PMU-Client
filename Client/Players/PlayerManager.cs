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


namespace Client.Logic.Players
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class PlayerManager
    {
        #region Fields

        static bool loggedIn = false;
        static string myConnectionID = "";
        static PlayerCollection players;
        static MyPlayer myPlayer;

        #endregion Fields

        #region Properties

        public static string MyConnectionID {
            get { return myConnectionID; }
            set { myConnectionID = value; }
        }

        public static PlayerCollection Players {
            get { return players; }
        }

        #endregion Properties

        #region Methods

        public static void Initialize() {
            players = new PlayerCollection();
        }

        //public static bool IsPlaying(int index) {
        //    return (!string.IsNullOrEmpty(players[index].Name));
        //}

        public static bool IsPlaying(string id) {
            foreach (IPlayer player in players.GetAllPlayers()) { 
                if (player.ID == id) {
                    return true;
                }
            }
            return false;
        }

        public static MyPlayer MyPlayer {
            get {
                if (players == null) {
                    return null;
                }
                if (myPlayer == null) {
                    myPlayer = players[myConnectionID] as MyPlayer;
                }
                return myPlayer;
            }
        }

        #endregion Methods
    }
}