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


namespace Client.Logic.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Paths
    {
        #region Fields

        /// <summary>
        /// Directory seperator character used by the OS.
        /// </summary>
        static char dirChar = System.IO.Path.DirectorySeparatorChar;
        static string fontPath;
        static string gfxPath;
        static string mapPath;
        static string musicPath;
        static string sfxPath;
        static string skinPath;
        static string startupPath;
        static string storyDataPath;

        #endregion Fields

        #region Properties

        public static char DirChar {
            get { return dirChar; }
        }

        public static string FontPath {
            get { return fontPath; }
        }

        public static string StoryDataPath {
            get { return storyDataPath; }
        }

        public static string GfxPath {
            get { return gfxPath; }
        }

        public static string MapPath {
            get { return mapPath; }
        }

        public static string MusicPath {
            get { return musicPath; }
        }

        public static string SfxPath {
            get { return sfxPath; }
        }

        public static string SkinPath {
            get { return skinPath; }
        }

        public static string StartupPath {
            get { return startupPath; }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Creates a file path in the format used by the host OS.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>A file path in the format used by the host OS</returns>
        public static string CreateOSPath(string fileName) {
            if (Environment.OSVersion.Platform == PlatformID.Unix) {
                if (fileName.Contains("\\"))
                    fileName = fileName.Replace('\\', dirChar);
            } else if (Environment.OSVersion.Platform == PlatformID.Win32NT) {
                if (fileName.Contains("/"))
                    fileName = fileName.Replace('/', dirChar);
            }
            if (fileName.StartsWith(StartupPath) == false) {
                fileName = StartupPath + fileName;
            }
            return fileName;
        }

        /// <summary>
        /// Initializes this class
        /// </summary>
        public static void Initialize() {
            Paths.startupPath = System.Windows.Forms.Application.StartupPath;
            //#if DEBUG
            if (/*Globals.InDebugMode &&*/ Globals.CommandLine.ContainsCommandArg("-overridepath")) {
                int index = Globals.CommandLine.FindCommandArg("-overridepath");
                Paths.startupPath = Globals.CommandLine.CommandArgs[index + 1];
            }
            //#endif
            Paths.startupPath = System.IO.Path.GetFullPath(Paths.startupPath);
            if (Paths.startupPath.EndsWith(dirChar.ToString()) == false)
                Paths.startupPath += dirChar;

            Paths.gfxPath = Paths.StartupPath + "GFX" + dirChar;
            Paths.skinPath = Paths.StartupPath + "Skins" + dirChar;
            Paths.fontPath = Paths.StartupPath + "Fonts" + dirChar;
            Paths.mapPath = Paths.StartupPath + "MapData" + dirChar;
            Paths.musicPath = Paths.StartupPath + "Music" + dirChar;
            Paths.sfxPath = Paths.StartupPath + "SFX" + dirChar;
            Paths.storyDataPath = Paths.StartupPath + "Story" + dirChar;
        }

        #endregion Methods
    }
}