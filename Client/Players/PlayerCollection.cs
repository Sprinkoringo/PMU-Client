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
