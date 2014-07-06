using System;
using System.Collections.Generic;
using System.Text;
using PMU.Core;

namespace Client.Logic.Players
{
    class PlayerCollection
    {
        ListPair<string, IPlayer> players;
        Object lockObject = new object();

        //public ListPair<string, IPlayer> Players {
        //    get { return players; }
        //}

        public PlayerCollection() {
            players = new ListPair<string, IPlayer>();
        }

        public IPlayer this[string connectionID] {
            get {
                lock (lockObject) {
                    if (players.ContainsKey(connectionID) == false) {
                        return null;
                    } else {
                        return players[connectionID];
                    }
                }
            }
            set {
                lock (lockObject) {
                    players[connectionID] = value;
                }
            }
        }

        public void Clear() {
            lock (lockObject) {
                players.Clear();
            }
        }

        public void Add(string connectionID, IPlayer player) {
            lock (lockObject) {
                if (!players.ContainsKey(connectionID)) {
                    players.Add(connectionID, player);
                } else {
                    players[connectionID] = player;
                }
            }
        }

        public void Remove(string connectionID) {
            lock (lockObject) {
                players.RemoveAtKey(connectionID);
            }
        }

        public IEnumerable<IPlayer> GetAllPlayers() {
            lock (lockObject) {
                foreach (IPlayer player in players.Values) {
                    yield return player;
                }
            }
        }
    }
}
