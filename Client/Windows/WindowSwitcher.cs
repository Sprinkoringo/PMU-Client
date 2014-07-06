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


namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SdlDotNet.Widgets;

    class WindowSwitcher
    {
        #region Enumerations

        public enum Window
        {
            None,
            Debug,
            Loading,
            MainMenu,
            Login,
            CharSelect,
            Game
        }

        #endregion Enumerations

        #region Properties

        public static Window ActiveWindow {
            get;
            set;
        }

        public static winGame GameWindow {
            get;
            set;
        }

        public static winExpKit ExpKit {
            get;
            set;
        }

        public static Updater.winUpdater UpdaterWindow {
            get;
            set;
        }

        //public static winChat ChatWindow {
        //    get;
        //    set;
        //}

        //public static winChars CharSelectWindow
        //{
        //    get; set;
        //}

        //public static winDebug DebugWindow
        //{
        //    get; set;
        //}

        //public static winGame GameWindow
        //{
        //    get; set;
        //}

        #endregion Properties

        #region Methods

        public static SdlDotNet.Widgets.Window FindWindow(string windowName) {
            return SdlDotNet.Widgets.WindowManager.FindWindow(windowName);
        }

        public static void AddWindow(SdlDotNet.Widgets.Window window) {
            SdlDotNet.Widgets.WindowManager.AddWindow(window);
        }

        public static void ShowMainMenu() {
            SdlDotNet.Widgets.WindowManager.AddWindow(new winMainMenu());
            SdlDotNet.Widgets.WindowManager.AddWindow(new winUpdates());
            SdlDotNet.Widgets.WindowManager.AddWindow(new winLogin());
            // Now that the menus have been shown, lets play the menu music
            
        }

        public static void ShowAccountSettings() {
            SdlDotNet.Widgets.WindowManager.AddWindow(new winAccountSettings());
            //Music.Music.AudioPlayer.PlayMusic("Temporal Tower.mp3");
        }

        #endregion Methods
    }
}