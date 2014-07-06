namespace Client.Logic.Maps
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    [Serializable]
    class MapNpc : Graphics.Renderers.Sprites.ISprite
    {
        public MapNpc() {
            Location = new System.Drawing.Point();
            Offset = new System.Drawing.Point();
            VolatileStatus = new List<int>();
        }

        #region Properties

        public Logic.Graphics.SpriteSheet SpriteSheet {
            get;
            set;
        }

        public int AttackTimer {
            get;
            set;
        }
        public int TotalAttackTime { get; set; }

        public bool Attacking {
            get;
            set;
        }

        //public bool Big {
        //    get;
        //    set;
        //}

        public Enums.Direction Direction {
            get;
            set;
        }

        public bool Enemy {
            get;
            set;
        }

        public int EmotionNum {
            get;
            set;
        }

        public int EmotionTime {
            get;
            set;
        }

        public int EmotionVar {
            get;
            set;
        }

        public int HP {
            get;
            set;
        }

        public string Map {
            get;
            set;
        }

        public int MaxHP {
            get;
            set;
        }

        public Enums.MovementSpeed MovementSpeed {
            get;
            set;
        }

        
        public Enums.StatusAilment StatusAilment { get; set; }

        public List<int> VolatileStatus
        {
            get;
            set;
        }

        public int Num {
            get;
            set;
        }

        public int Target {
            get;
            set;
        }

        #endregion Properties

        public int Sprite {
            get;
            set;
        }

        public int Form { get; set; }
        public Enums.Coloration Shiny { get; set; }
        public Enums.Sex Sex { get; set; }

        public int IdleTimer { get; set; }
        public int IdleFrame { get; set; }
        public int LastWalkTime { get; set; }
        public int WalkingFrame { get; set; }

        public System.Drawing.Point Offset {
            get;
            set;
        }

        public System.Drawing.Point Location {
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

        public bool Leaving { get; set; }

        public bool ScreenActive { get; set; }

        public int SleepTimer {
            get;
            set;
        }

        public int SleepFrame {
            get;
            set;
        }

        public Graphics.Renderers.Sprites.SpeechBubble CurrentSpeech { get; set; }

        public Graphics.Renderers.Sprites.Emoticon CurrentEmote { get; set; }
    }
}