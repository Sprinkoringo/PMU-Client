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
using System.Drawing;

namespace Client.Logic.Graphics.Renderers.Sprites
{
    interface ISprite
    {
        //Enums.Size Size { get; set; }
        int Sprite { get; set; }
        int Form { get; set; }
        Enums.Sex Sex { get; set; }
        Enums.Coloration Shiny { get; set; }
        Enums.Direction Direction { get; set; }
        bool Attacking { get; set; }
        Point Offset { get; set; }
        Point Location { get; set; }
        int AttackTimer { get; set; }
        int TotalAttackTime { get; set; }
        int X { get; set; }
        int Y { get; set; }
        Enums.MovementSpeed MovementSpeed { get; set; }
        int LastWalkTime { get; set; }
        int WalkingFrame { get; set; }
        //bool Confused { get; set; }
        Enums.StatusAilment StatusAilment{ get; set; }
        List<int> VolatileStatus { get; set; }
        int SleepTimer { get; set; }
        int SleepFrame { get; set; }
        bool Leaving { get; set; }
        bool ScreenActive { get; set; }
        
        

        int IdleTimer { get; set; }
        int IdleFrame { get; set; }

        SpriteSheet SpriteSheet { get; set; }

        SpeechBubble CurrentSpeech { get; set; }

        Emoticon CurrentEmote { get; set; }

        //int WalkAnimationFrame { get; set; }
        //int WalkAnimationTimer { get; set; }
        //int AttackAnimationFrame { get; set; }
        //int AttackAnimationTimer { get; set; }
    }
}
