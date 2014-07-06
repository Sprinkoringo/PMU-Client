using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Music
{
    class AudioHelper
    {
        public static string FindMusicFile(string folder, string songName) {
            string[] extensions = new string[] { ".mp3", ".ogg", ".wav" };
            for (int i = 0; i < extensions.Length; i++) {
                if (System.IO.File.Exists(folder + songName + extensions[i])) {
                    return folder + songName + extensions[i];
                }
            }
            return null;
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
    }
}
