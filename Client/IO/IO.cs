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

    /// <summary>
    /// 
    /// </summary>
    public class IO
    {
        #region Methods

        /// <summary>
        /// Initializes the IO class
        /// </summary>
        public static void Init() {
            Paths.Initialize();
        }

        /// <summary>
        /// Checks if the game folders exist, and creates the ones that dont exist.
        /// </summary>
        public static void CheckFolders() {
            CreateDir(Paths.MapPath);
            CreateDir(Paths.GfxPath);
            CreateDir(Paths.SfxPath);
            CreateDir(Paths.MusicPath);
            CreateDir(Paths.SkinPath);
            CreateDir(Paths.FontPath);
        }

        /// <summary>
        /// Creates a directory.
        /// </summary>
        /// <param name="dir">The directory.</param>
        public static void CreateDir(string dir) {
            if (DirExists(dir) == false) {
                System.IO.Directory.CreateDirectory(Paths.CreateOSPath(dir));
            }
        }

        /// <summary>
        /// Checks if a directory exists.
        /// </summary>
        /// <param name="dir">The directory to check.</param>
        /// <returns>True if the directory exists; otherwise, false.</returns>
        public static bool DirExists(string dir) {
            return System.IO.Directory.Exists(Paths.CreateOSPath(dir));
        }

        /// <summary>
        /// Checks if a file exists.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>True if the file exists; otherwise, false.</returns>
        public static bool FileExists(string fileName) {
            return System.IO.File.Exists(Paths.CreateOSPath(fileName));
        }

        /// <summary>
        /// Gets the full path of a file in the GFX directory.
        /// </summary>
        /// <param name="filename">The filename. (without directory name)</param>
        /// <returns></returns>
        public static string GetGfxPath(string filename) {
            return Paths.CreateOSPath(Paths.GfxPath + filename);
        }

        #endregion Methods
    }
}