using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Music
{
    class Music
    {
        static IAudioPlayer audioPlayer;
        public static IAudioPlayer AudioPlayer {
            get {
                return audioPlayer;
            }
        }

        public static void Initialize() {
            audioPlayer = new Bass.BassAudioPlayer();
        }

        public static void Dispose() {
            audioPlayer.Dispose();
        }
    }
}
