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

namespace Client.Logic.Stories.FNPCs
{
    class FNPC : Logic.Graphics.Renderers.Sprites.ISprite
    {
        public Logic.Graphics.SpriteSheet SpriteSheet {
            get;
            set;
        }

        public int IdleTimer { get; set; }
        public int IdleFrame { get; set; }
        public int LastWalkTime { get; set; }
        public int WalkingFrame { get; set; }

        public int Sprite {
            get;
            set;
        }
        public int Form { get; set; }
        public Enums.Coloration Shiny { get; set; }
        public Enums.Sex Sex { get; set; }

        public Enums.Direction Direction {
            get;
            set;
        }

        public bool Attacking {
            get;
            set;
        }

        public int TotalAttackTime { get; set; }

        public System.Drawing.Point Offset {
            get;
            set;
        }

        public System.Drawing.Point Location {
            get;
            set;
        }

        public int AttackTimer {
            get;
            set;
        }

        public int X {
            get {
                return Location.X;
            }
            set {
                Location = new System.Drawing.Point(value, Location.Y);
            }
        }

        public int Y {
            get {
                return Location.Y;
            }
            set {
                Location = new System.Drawing.Point(Location.X, value);
            }
        }

        public Enums.MovementSpeed MovementSpeed {
            get;
            set;
        }

        public Enums.StatusAilment StatusAilment {
            get;
            set;
        }

        public List<int> VolatileStatus
        {
            get;
            set;
        }
        public bool Leaving { get; set; }

        public bool ScreenActive {
            get { return true; }
            set { }
        }

        public int SleepTimer {
            get;
            set;
        }

        public int SleepFrame {
            get;
            set;
        }

        public string MapID {
            get;
            set;
        }

        public string ID {
            get;
            set;
        }

        public int TargetX {
            get;
            set;
        }

        public int TargetY {
            get;
            set;
        }

        public int LastMovement {
            get;
            set;
        }

        public Graphics.Renderers.Sprites.SpeechBubble CurrentSpeech { get; set; }

        public Graphics.Renderers.Sprites.Emoticon CurrentEmote { get; set; }

        public Algorithms.Pathfinder.PathfinderResult PathfinderResult {
            get;
            set;
        }

        public FNPC() {
            TargetX = -1;
            TargetY = -1;
            VolatileStatus = new List<int>();
        }
    }
}
