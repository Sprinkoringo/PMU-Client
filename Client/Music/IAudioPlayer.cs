using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Music
{
    interface IAudioPlayer : IDisposable
    {
        string CurrentSong { get; }
        void Pause();
        void Resume();
        bool IsMusicPaused();
        void PlayMusic(string songName);
        void PlayMusic(string songName, int numberOfTimes);
        void PlayMusic(string songName, int numberOfTimes, bool ignoreMusicSetting, bool ignoreIfPlaying);
        void StopMusic();
        void PlaySoundEffect(string soundEffect);
        void FadeOut(int milliseconds);
    }
}
