namespace Client.Logic.Music
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SdlAudio = SdlDotNet.Audio;

    class MusicPlayerOld
    {
        #region Fields

        static string currentSong;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the current song.
        /// </summary>
        /// <value>The current song.</value>
        public static string CurrentSong {
            get { return currentSong; }
        }

        /// <summary>
        /// Gets or sets the music volume.
        /// </summary>
        /// <value>The music volume.</value>
        public static int Volume {
            get { return SdlAudio.MusicPlayer.Volume; }
            set { SdlAudio.MusicPlayer.Volume = value; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Fades the music in.
        /// </summary>
        /// <param name="numberOfTimes">The number of times.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        public static void FadeIn(int numberOfTimes, int milliseconds) {
            SdlAudio.MusicPlayer.FadeIn(numberOfTimes, milliseconds);
        }

        /// <summary>
        /// Fades the music in from the specified position.
        /// </summary>
        /// <param name="numberOfTimes">The number of times.</param>
        /// <param name="milliseconds">The milliseconds.</param>
        /// <param name="musicPosition">The music position.</param>
        public static void FadeInPosition(int numberOfTimes, int milliseconds, int musicPosition) {
            SdlAudio.MusicPlayer.FadeInPosition(numberOfTimes, milliseconds, musicPosition);
        }

        public static bool IsMusicPaused() {
            return SdlAudio.MusicPlayer.IsPaused;
        }

        /// <summary>
        /// Fades the music out.
        /// </summary>
        /// <param name="milliseconds">The length of the fade out.</param>
        public static void FadeOut(int milliseconds) {
            if (SdlAudio.MusicPlayer.IsFading == false && SdlAudio.MusicPlayer.IsPlaying)
                SdlAudio.MusicPlayer.Fadeout(milliseconds);
            
        }

        /// <summary>
        /// Pauses the playing music.
        /// </summary>
        public static void Pause() {
            if (SdlAudio.MusicPlayer.IsPlaying)
                SdlAudio.MusicPlayer.Pause();
        }

        /// <summary>
        /// Plays a music file continuosly.
        /// </summary>
        /// <param name="songName">The filename of the song to play. Excluding the folder path.</param>
        public static void PlayMusic(string songName) {
            //songName = FileNameConverter(IO.Paths.MusicPath, songName);
            //if (songName != currentSong) {
            //    if (IO.Options.Music == false) {
            //        StopMusic();
            //    } else if (IO.IO.FileExists(IO.Paths.MusicPath + songName)) {
            //        StopMusic();
            //        if (IO.IO.FileExists(IO.Paths.MusicPath + songName)) {
            //            SdlAudio.MusicPlayer.Load(new SdlDotNet.Audio.Music(IO.Paths.MusicPath + songName));
            //            SdlAudio.MusicPlayer.Play(true);
            //            currentSong = songName;
            //        }
            //    } else {
            //        return;
            //    }
            //}
            PlayMusic(songName, -1, false, true);
        }

        /// <summary>
        /// Plays a music file a certain amount of times.
        /// </summary>
        /// <param name="songName">The filename of the song to play. Excluding the folder path.</param>
        /// <param name="numberOfTimes">The number of times the song will be played.</param>
        public static void PlayMusic(string songName, int numberOfTimes) {
            songName = FileNameConverter(IO.Paths.MusicPath, songName);
            if (songName != currentSong) {
                StopMusic();
                if (IO.IO.FileExists(IO.Paths.MusicPath + songName)) {
                    SdlAudio.MusicPlayer.Load(new SdlDotNet.Audio.Music(IO.Paths.MusicPath + songName));
                    SdlAudio.MusicPlayer.Play(numberOfTimes);
                    currentSong = songName;
                }
            }
        }

        /// <summary>
        /// Plays the music.
        /// </summary>
        /// <param name="songName">Name of the song.</param>
        /// <param name="numberOfTimes">The number of times.</param>
        /// <param name="ignoreMusicSetting">if set to <c>true</c> [ignore music setting].</param>
        /// <param name="ignoreIfPlaying">if set to <c>true</c> [ignore if playing].</param>
        public static void PlayMusic(string songName, int numberOfTimes, bool ignoreMusicSetting, bool ignoreIfPlaying) {
            if (!string.IsNullOrEmpty(songName)) {
                PlayMusicDirect(IO.Paths.MusicPath + FileNameConverter(IO.Paths.MusicPath, songName), numberOfTimes, ignoreMusicSetting, ignoreIfPlaying);
            }
        }

        public static void PlayMusicDirect(string fullPath, int numberOfTimes, bool ignoreMusicSetting, bool ignoreIfPlaying) {
            if (ignoreMusicSetting == false) {
                if (IO.Options.Music == false) {
                    StopMusic();
                    return;
                }
            }
            if (string.IsNullOrEmpty(fullPath)) {
                return;
            }
            fullPath = System.IO.Path.GetDirectoryName(fullPath) + "/" + FileNameConverter(System.IO.Path.GetDirectoryName(fullPath) + "/",  System.IO.Path.GetFileName(fullPath));
            if (ignoreIfPlaying) {
                if (currentSong == fullPath) {
                    return;
                }
            }
            if (IO.IO.FileExists(fullPath)) {
                StopMusic();
                SdlAudio.MusicPlayer.Load(new SdlDotNet.Audio.Music(fullPath));
                if (numberOfTimes > -1) {
                    SdlAudio.MusicPlayer.Play(numberOfTimes);
                } else {
                    SdlAudio.MusicPlayer.Play(true);
                }
                currentSong = fullPath;
            }
        }

        /// <summary>
        /// Plays a sound effect.
        /// </summary>
        /// <param name="soundEffectName">Name of the sound effect.</param>
        public static void PlaySoundEffect(string soundEffectName) {
            if (IO.Options.Sound) {
                soundEffectName = FileNameConverter(IO.Paths.SfxPath, soundEffectName);
                if (IO.IO.FileExists(IO.Paths.SfxPath + soundEffectName)) {
                    SdlAudio.Sound sound = new SdlDotNet.Audio.Sound(IO.Paths.SfxPath + soundEffectName);
                    sound.Play();
                }
            }
        }

        /// <summary>
        /// Resumes playback.
        /// </summary>
        public static void Resume() {
            if (SdlAudio.MusicPlayer.IsPaused)
                SdlAudio.MusicPlayer.Resume();
        }

        /// <summary>
        /// Sets the position of the music.
        /// </summary>
        /// <param name="musicPosition">The music position.</param>
        public static void SetPosition(double musicPosition) {
            SdlAudio.MusicPlayer.Position(musicPosition);
        }

        /// <summary>
        /// Stops the music.
        /// </summary>
        public static void StopMusic() {
            if (SdlAudio.MusicPlayer.IsPlaying) {
                SdlAudio.MusicPlayer.Stop();
                currentSong = null;
            }
        }

        public static string FileNameConverter(string directory, string fileToTest) {
            if (System.IO.File.Exists(directory + fileToTest)) {
                return fileToTest;
            } else if (System.IO.File.Exists(directory + System.IO.Path.ChangeExtension(fileToTest, ".ogg"))) {
                return System.IO.Path.ChangeExtension(fileToTest, ".ogg");
            } else {
                return fileToTest;
            }
        }

        public static string FindMusicFile(string folder, string songName) {
            string[] extensions = new string[] { ".mp3", ".ogg", ".wav" };
            for (int i = 0; i < extensions.Length; i++) {
                if (System.IO.File.Exists(folder + songName + extensions[i])) {
                    return folder + songName + extensions[i];
                }
            }
            return null;
        }

        public static void RunTest() {
            //SdlAudio.Sound sound1 = new SdlAudio.Sound(IO.Paths.MusicPath + "PMD2) Temporal Tower.ogg");
            //SdlAudio.Sound sound2 = new SdlAudio.Sound(IO.Paths.MusicPath + "PMD2) Spacial Distortion (Versus Palkia).ogg");
            //sound1.Play();
            //sound2.Play();
        }

        public static bool IsSongDownloaded(string songName) {
            string newSongName = FileNameConverter(IO.Paths.MusicPath, songName);
            return IO.IO.FileExists(IO.Paths.MusicPath + newSongName);
        }

        #endregion Methods
    }
}