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
