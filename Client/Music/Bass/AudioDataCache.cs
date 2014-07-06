using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Runtime.InteropServices;

namespace Client.Logic.Music.Bass
{
    class AudioDataCache
    {
        public string CachePath { get; set; }
        public FileStream CacheStream { get; set; }
        public BassAudioPlayer.DownloadCallbackDelegate CallbackDelegate { get; set; }
        public IntPtr AudioStreamPointer { get; set; }
    }
}
