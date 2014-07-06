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


namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using Graphics = Client.Logic.Graphics;

    /// <summary>
    /// Class for storing global variables.
    /// </summary>
    class Globals
    {
        #region Properties

        public static bool FoolsMode {
            get;
            set;
        }

        public static PMU.Core.Command CommandLine {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the encryption class.
        /// </summary>
        /// <value>The encryption class.</value>
        public static Security.Encryption Encryption {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the game is loaded.
        /// </summary>
        /// <value><c>true</c> if the game is loaded; otherwise, <c>false</c>.</value>
        public static bool GameLoaded {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the game screen.
        /// </summary>
        /// <value>The game screen.</value>
        public static Windows.Core.GameScreen GameScreen {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the game is running in debug mode.
        /// </summary>
        /// <value><c>true</c> if the game is running in debug mode; otherwise, <c>false</c>.</value>
        public static bool InDebugMode {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the games weather
        /// </summary>
        //internal static Enums.Weather GameWeather {
        //    get;
        //    set;
        //}


        static Enums.Weather activeWeather;
        /// <summary>
        /// Gets or Sets the active weather that will be displayed
        /// </summary>
        internal static Enums.Weather ActiveWeather {
            get { return activeWeather; }
            set {
                activeWeather = value;
                Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.SetWeather(value);
            }
        }

        /// <summary>
        /// Gets or Sets the games time
        /// </summary>
        internal static Enums.Time GameTime {
            get;
            set;
        }

        /// <summary>
        /// Gets or Sets the active time that will be displayed
        /// </summary>
        internal static Enums.Time ActiveTime {
            get;
            set;
        }

        //internal static int CameraX {
        //    get;
        //    set;
        //}

        //internal static int CameraX2 {
        //    get;
        //    set;
        //}

        //internal static int CameraY {
        //    get;
        //    set;
        //}

        //internal static int CameraY2 {
        //    get;
        //    set;
        //}

        internal static int NewPlayerX {
            get;
            set;
        }

        internal static int NewPlayerY {
            get;
            set;
        }

        internal static int NewXOffset {
            get;
            set;
        }

        internal static int NewYOffset {
            get;
            set;
        }

        internal static int NewMapX {
            get;
            set;
        }

        internal static int NewMapY {
            get;
            set;
        }

        internal static int NewMapXOffset {
            get;
            set;
        }

        internal static int NewMapYOffset {
            get;
            set;
        }

        internal static bool GettingMap {
            get;
            set;
        }

        internal static bool SavingMap {
            get;
            set;
        }

        internal static int Tick {
            get;
            set;
        }

        internal static bool RefreshLock {
            get;
            set;
        }

        internal static bool InGame {
            get;
            set;
        }

        public static string ServerStatus {
            get;
            set;
        }

        #endregion Properties


    }
}