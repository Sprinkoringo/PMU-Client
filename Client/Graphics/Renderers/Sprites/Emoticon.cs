using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Graphics.Renderers.Sprites {
    public class Emoticon {

        public int EmoteIndex { get; set; }

        public int EmoteSpeed { get; set; }

        public int EmoteTime { get; set; }

        public int EmoteFrame { get; set; }

        public int EmoteCycles { get; set; }

        public int CurrentCycle { get; set; }

        public Emoticon(int index, int speed, int cycles) {
            EmoteIndex = index;
            EmoteSpeed = speed;
            EmoteCycles = cycles;
        }

    }
}
