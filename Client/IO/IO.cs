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