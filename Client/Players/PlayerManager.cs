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