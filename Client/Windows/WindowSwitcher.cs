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