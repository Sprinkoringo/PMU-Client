namespace Client.Logic.Music.Bass
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Forms;
    using PMU.Core;

    public class BassAudioPlayer : IDisposable, IAudioPlayer
    {
        #region Fields

        private const uint BASS_DEVICE_DEFAULT = 2;
        private const uint BASS_POS_BYTE = 0;

        private readonly string MusicURL = IO.Options.MusicAddress;
        private readonly string SoundFXURL = IO.Options.SoundAddress;

        private bool abortMusic;
        string currentSong;
        IntPtr loadedStream = IntPtr.Zero;
        int musicPlayCount = 0;
        int musicRepeatAmount = 0;
        bool paused = false;
        ManualResetEvent pauseResetEvent;
        ManualResetEvent stopResetEvent;
        private System.Threading.Thread playbackThread;
        //string url;
        //string urlCachePath;
        //FileStream urlCacheStream;
        Delegate callbackDelegate;
        ListPair<string, AudioDataCache> audioDataCache = new ListPair<string, AudioDataCache>();

        #endregion Fields

        #region Delegates

        public delegate void DownloadCallbackDelegate(IntPtr bufferPointer, int length, IntPtr user);

        #endregion Delegates

        #region Properties

        public string CurrentSong {
            get { return currentSong; }
        }

        public string NextSong { get; set; }

        public int TimeOfNextSong { get; set; }

        #endregion Properties

        #region Methods

        public void Dispose() {
            //Get rid of the Audio object.
            StopMusic();
            Bass.BASS_Free();
        }

        //public void MusicDownloadCallback(IntPtr bufferPointer, int length, IntPtr user) {
        //    try {
        //        if (length > 0) {
        //            byte[] buffer = new byte[length];
        //            Marshal.Copy(bufferPointer, buffer, 0, length);
        //            urlCacheStream.Write(buffer, 0, length);
        //        } else if (length == 0) {
        //            urlCacheStream.Close();
        //            File.Copy(urlCachePath + ".tmp", urlCachePath, true);
        //            File.Delete(urlCachePath + ".tmp");
        //        }
        //    } catch (Exception ex) {
        //        throw new Exception(ex.Message + " @ Music Download Callback");
        //    }
        //}

        public BassAudioPlayer() {
            Bass.BASS_Init(-1, 44100, BASS_DEVICE_DEFAULT, IntPtr.Zero, 0);
            IntPtr ptr = Bass.BASS_PluginLoad("bassmidi.dll");

            pauseResetEvent = new ManualResetEvent(false);
            stopResetEvent = new ManualResetEvent(false);
        }

        public bool IsMusicPaused() {
            return paused;
        }

        //public void LoadMusicStreamFromUrl(string url) {
        //    this.url = url;

        //    if (loadedStream != IntPtr.Zero) {
        //        Bass.BASS_StreamFree(loadedStream);
        //        loadedStream = IntPtr.Zero;
        //    }


        //    urlCacheStream = new FileStream(IO.Paths.MusicPath + Path.GetFileName(url) + ".tmp", FileMode.Create);
        //    urlCachePath = IO.Paths.MusicPath + Path.GetFileName(url);
        //    this.currentSong = IO.Paths.MusicPath + Path.GetFileName(url);

        //    callbackDelegate = new DownloadCallbackDelegate(MusicDownloadCallback);
        //    loadedStream = Bass.BASS_StreamCreateURL(url, 0, 0, callbackDelegate, IntPtr.Zero);

        //    if (loadedStream == IntPtr.Zero) {
        //        // Uh oh! We couldn't play the music stream!
        //        urlCacheStream.Close();
        //        File.Delete(urlCachePath + ".tmp");
        //        urlCacheStream = null;
        //        urlCachePath = null;
        //    }

        //    musicPlayCount = 0;
        //}

        public void Pause() {
            if (!paused) {
                Bass.BASS_ChannelPause(loadedStream);
                pauseResetEvent.Reset();
                pauseResetEvent.WaitOne();
            }
        }


        public void FadeToNext(string nextSong, int milliseconds) {
            if ((currentSong == null || !currentSong.EndsWith(nextSong)) && nextSong != NextSong) {
                Bass.BASS_ChannelSlideAttribute(loadedStream, 2, 0, (uint)milliseconds);
                NextSong = nextSong;
                TimeOfNextSong = Globals.Tick + milliseconds;
            }
        }

        public void PlayNextMusic() {
            if (NextSong != null) {
                StopMusic();
                PlayMusic(NextSong, -1);
                NextSong = null;
                TimeOfNextSong = 0;
            }
        }

        public void PlayMusic(string songName) {
            PlayMusic(songName, -1);
        }

        public void PlayMusic(string songName, int numberOfTimes) {
            PlayMusic(songName, numberOfTimes, false, true);
        }

        public void PlayMusic(string songName, int numberOfTimes, bool ignoreMusicSetting, bool ignoreIfPlaying) {
            Thread musicLoadThread = new Thread(new ParameterizedThreadStart(PlayMusicBackground));
            musicLoadThread.IsBackground = true;
            musicLoadThread.Start(new object[] { songName, numberOfTimes, ignoreMusicSetting, ignoreIfPlaying });
        }

        private void PlayMusicBackground(Object param) {
            object[] args = param as object[];
            string songName = args[0] as string;
            int numberOfTimes = (int)args[1];
            bool ignoreMusicSetting = false;//(bool)args[2];
            bool ignoreIfPlaying = (bool)args[3];

            VerifySongName(ref songName);
            if (ignoreMusicSetting == false) {
                if (IO.Options.Music == false) {
                    StopMusic();
                    return;
                }
            }
            if (string.IsNullOrEmpty(songName)) {
                return;
            }
            if (ignoreIfPlaying) {
                if (currentSong == IO.Paths.MusicPath + songName) {
                    return;
                }
            }
            lock (audioUrlStreamLoaderLockObject) {
                StopMusic();
                LoadMusic(songName);
                playbackThread = Thread.CurrentThread;
            }
            currentSong = IO.Paths.MusicPath + songName;
            PlayMusicInternal(numberOfTimes);
        }

        public void StopMusic() {
            if (playbackThread != null) {
                //Stop the music.
                //abortMusic = true;
                //stopResetEvent.Reset();
                //stopResetEvent.WaitOne();

                if (loadedStream != IntPtr.Zero) {
                    currentSong = null;
                    lock (audioDataCache) {
                        for (int i = 0; i < audioDataCache.Count; i++) {
                            if (audioDataCache.ValueByIndex(i).AudioStreamPointer == loadedStream) {
                                audioDataCache.ValueByIndex(i).AudioStreamPointer = IntPtr.Zero;
                            }
                        }
                    }
                    Bass.BASS_StreamFree(loadedStream);
                    Bass.BASS_ChannelStop(loadedStream);
                    loadedStream = IntPtr.Zero;
                }
                if (playbackThread != null) {
                    playbackThread.Abort();
                    playbackThread = null;
                }
            }
        }

        private bool IsMusicDownloaded(string songName) {
            return IO.IO.FileExists(IO.Paths.MusicPath + songName);
        }

        private void LoadMusic(string songName) {
            if (File.Exists(songName)) {
                // Support for using a full file path as the song name
                loadedStream = Bass.BASS_StreamCreateFile(false, songName, 0, 0, 0, 0, 0);
            } else {
                if (IsMusicDownloaded(songName)) {
                    // We already have the song cached, load from the file
                    loadedStream = Bass.BASS_StreamCreateFile(false, IO.Paths.MusicPath + songName, 0, 0, 0, 0, 0);
                } else {
                    // We don't have the song cached, stream from the web
                    loadedStream = LoadAudioStreamFromUrl(MusicURL + songName, IO.Paths.MusicPath);
                }
            }
        }

        private void MusicThread() {
            //This is the actual routine that's playing the music.  I use a background thread to play it.
            long intLength = 0;

            //stream = new IntPtr(Un4seen.Bass.Bass.BASS_StreamCreateURL(url, 0, Un4seen.Bass.BASSFlag.BASS_DEFAULT, null, IntPtr.Zero));
            //stream = //BASS_StreamCreateFile(false, strDirs[i], 0, 0, 0, 0, 0);
            intLength = Bass.BASS_ChannelGetLength(loadedStream, (int)BASS_POS_BYTE);

            if (loadedStream != IntPtr.Zero) {
                //BASS_SetVolume(0.5f);
                Bass.BASS_ChannelPlay(loadedStream, false);

                //System.IO.FileStream fs = new System.IO.FileStream("SoundTest.ogg", System.IO.FileMode.Create);
                while (!(Bass.BASS_ChannelGetPosition(loadedStream, (int)BASS_POS_BYTE) >= intLength || abortMusic)) {
                    //byte[] buffer = new byte[65536];
                    //int len = Un4seen.Bass.Bass.BASS_ChannelGetData(stream.ToInt32(), buffer, (int)intLength);
                    //fs.Write(buffer, 0, len);
                    System.Threading.Thread.Sleep(500);
                    if (abortMusic)
                        break; // TODO: might not be correct. Was : Exit For
                }
                //fs.Close();
                //byte[] buffer = new byte[intLength];
                //Un4seen.Bass.Bass.BASS_ChannelSetPosition(stream.ToInt32(), 0);

                //System.IO.File.WriteAllBytes("SoundTest.ogg", buffer);
            }
            Bass.BASS_StreamFree(loadedStream);
            //if (booAbortMusic)
            //    break; // TODO: might not be correct. Was : Exit For
            Bass.BASS_ChannelStop(loadedStream);
            loadedStream = IntPtr.Zero;

            playbackThread = null;

            if (musicRepeatAmount == -1) {
                PlayMusic(Path.GetFileName(currentSong), -1, false, false);
            } else if (musicPlayCount < musicRepeatAmount) {
                musicPlayCount++;
                PlayMusicInternal(musicRepeatAmount);
            }
            GC.KeepAlive(this);
            stopResetEvent.Set();
        }

        private void PlayMusicInternal(int numberOfTimes) {
            if (numberOfTimes != 0 && loadedStream != IntPtr.Zero) {
                musicRepeatAmount = numberOfTimes;
                //Start up the music.
                abortMusic = false;
                //RunMusicPlaybackThread();
                MusicThread();
            }
        }

        public void Resume() {
            if (paused) {
                Bass.BASS_ChannelPlay(loadedStream, false);
                GC.KeepAlive(this);
                pauseResetEvent.Set();
                paused = true;
            }
        }

        void RunMusicPlaybackThread() {
            if (playbackThread == null) {
                playbackThread = new System.Threading.Thread(MusicThread);
                playbackThread.Name = "BackgroundMusic";
                playbackThread.IsBackground = true;
                playbackThread.Start();
            }
        }

        private List<string> Shuffle(List<string> strTempList) {
            //Shuffle a list of music randomly.
            Random rand = new Random();
            List<string> strResult = new List<string>();

            int i = 0;

            for (int j = 1; j <= strTempList.Count; j++) {
                i = rand.Next(0, strTempList.Count);
                strResult.Add(strTempList[i]);
                strTempList.RemoveAt(i);
            }
            return strResult;
        }

        private void VerifySongName(ref string songName) {
            if (!string.IsNullOrEmpty(songName)) {
                if (songName.EndsWith(".ogg") == false && songName.EndsWith(".mid") == false) {
                    songName = Path.ChangeExtension(songName, ".ogg");
                }
            }
        }

        public void FadeOut(int milliseconds) {
            Bass.BASS_ChannelSlideAttribute(loadedStream, 2, 0, (uint)milliseconds);
        }

        public void PlaySoundEffect(string soundEffect) {
            if (IO.Options.Sound) {
                Thread soundEffectThread = new Thread(new ParameterizedThreadStart(PlaySoundEffectThread));
                soundEffectThread.IsBackground = true;
                soundEffectThread.Name = "SFX Playback Thread";
                soundEffectThread.Start(Path.ChangeExtension(soundEffect, ".ogg"));
            }
        }

        private void PlaySoundEffectThread(Object paramenter) {
            string soundEffect = paramenter as string;

            IntPtr sfxStream = IntPtr.Zero;
            // Load the SFX
            if (IsSFXDownloaded(soundEffect)) {
                // We already have the song cached, load from the file
                sfxStream = Bass.BASS_StreamCreateFile(false, IO.Paths.SfxPath + soundEffect, 0, 0, 0, 0, 0);
            } else {
                // We don't have the song cached, stream from the web
                sfxStream = LoadAudioStreamFromUrl(SoundFXURL + soundEffect, IO.Paths.SfxPath);
            }

            PlaySFXStream(sfxStream);
        }

        private void PlaySFXStream(IntPtr sfxStream) {
            //This is the actual routine that's playing the SFX.
            long intLength = 0;

            if (sfxStream != IntPtr.Zero) {
                intLength = Bass.BASS_ChannelGetLength(sfxStream, (int)BASS_POS_BYTE);
                Bass.BASS_ChannelPlay(sfxStream, false);

                while (!(Bass.BASS_ChannelGetPosition(sfxStream, (int)BASS_POS_BYTE) >= intLength)) {
                    System.Threading.Thread.Sleep(500);
                }

                Bass.BASS_StreamFree(sfxStream);
                Bass.BASS_ChannelStop(sfxStream);
            }

            GC.KeepAlive(this);
        }

        Object audioUrlStreamLoaderLockObject = new object();
        public IntPtr LoadAudioStreamFromUrl(string url, string cacheDirectory) {
            lock (audioUrlStreamLoaderLockObject) {
                lock (audioDataCache) {
                    if (audioDataCache.ContainsKey(url)) {
                        return audioDataCache[url].AudioStreamPointer;
                    }
                }
                AudioDataCache dataCache = new AudioDataCache();

                dataCache.CachePath = cacheDirectory + Path.GetFileName(url);
                dataCache.CacheStream = new FileStream(dataCache.CachePath + ".tmp", FileMode.Create, FileAccess.Write);

                dataCache.CallbackDelegate = new DownloadCallbackDelegate(AudioDownloadCallback);

                IntPtr urlPtr = Marshal.StringToHGlobalUni(url);
                lock (audioDataCache) {
                    if (audioDataCache.ContainsKey(url) == false) {
                        audioDataCache.Add(url, dataCache);
                    } else {
                        dataCache.CallbackDelegate = null;
                        if (urlPtr != IntPtr.Zero) {
                            Marshal.FreeHGlobal(urlPtr);
                            urlPtr = IntPtr.Zero;
                        }
                    }
                }

                dataCache.AudioStreamPointer = Bass.BASS_StreamCreateURL(url, 0, 0, dataCache.CallbackDelegate, urlPtr);

                if (dataCache.AudioStreamPointer == IntPtr.Zero) {
                    // Uh oh! We couldn't play the sfx stream!
                    lock (audioDataCache) {
                        if (audioDataCache.ContainsKey(url)) {
                            audioDataCache.RemoveAtKey(url);
                        }
                        if (urlPtr != IntPtr.Zero) {
                            Marshal.FreeHGlobal(urlPtr);
                            urlPtr = IntPtr.Zero;
                        }
                    }
                    dataCache.CacheStream.Close();
                    File.Delete(dataCache.CachePath + ".tmp");
                    dataCache.CachePath = null;
                }

                return dataCache.AudioStreamPointer;
            }
        }

        private bool IsSFXDownloaded(string soundEffect) {
            return IO.IO.FileExists(IO.Paths.SfxPath + soundEffect);
        }

        public void AudioDownloadCallback(IntPtr bufferPointer, int length, IntPtr user) {
            try {
                AudioDataCache dataCache = null;
                string url = Marshal.PtrToStringUni(user);
                lock (audioDataCache) {
                    if (audioDataCache.ContainsKey(url)) {
                        dataCache = audioDataCache[url];
                    }
                }
                if (dataCache != null) {
                    if (length > 0) {
                        byte[] buffer = new byte[length];
                        Marshal.Copy(bufferPointer, buffer, 0, length);
                        dataCache.CacheStream.Write(buffer, 0, length);
                    } else if (length == 0) {
                        Marshal.FreeHGlobal(user);
                        dataCache.CacheStream.Close();
                        lock (audioDataCache) {
                            if (audioDataCache.ContainsKey(url)) {
                                audioDataCache.RemoveAtKey(url);
                            }
                        }
                        if (dataCache.AudioStreamPointer != IntPtr.Zero) {
                            File.Copy(dataCache.CachePath + ".tmp", dataCache.CachePath, true);
                        }
                        File.Delete(dataCache.CachePath + ".tmp");
                    }
                }
            } catch (Exception ex) {
                throw new Exception(ex.Message + " @ Audio Download Callback");
            }
        }

        #endregion Methods
    }
}