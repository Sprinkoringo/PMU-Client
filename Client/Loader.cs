namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Windows;
    using Client.Logic.Graphics;
    using Client.Logic.Network;
    using System.IO;
    using System.Threading;
    using System.Text.RegularExpressions;
using PMU.Core;
    using Microsoft.Win32;

    /// <summary>
    /// Loader that will load the game data.
    /// </summary>
    public class Loader
    {
        #region Methods

        /// <summary>
        /// Checks the folders to see if they exist.
        /// </summary>
        public static void CheckFolders() {
            IO.IO.CheckFolders();
        }

        /// <summary>
        /// Initializes the loader.
        /// </summary>
        [STAThread]
        public static void InitLoader(string[] args) {
            Globals.CommandLine = PMU.Core.CommandProcessor.ParseCommand(System.Environment.CommandLine);
            Globals.GameLoaded = false;

            Logic.Globals.GameScreen = new Client.Logic.Windows.Core.GameScreen();
            AppDomain.CurrentDomain.UnhandledException += new UnhandledExceptionEventHandler(CurrentDomain_UnhandledException);

#if DEBUG
            if (Globals.CommandLine.ContainsCommandArg("-debug")) {
                Logic.Globals.InDebugMode = true;
            }
#endif

            IO.IO.Init();
            if (IsRunningUnderMaintenanceMode(Globals.CommandLine)) {
                RunMaintenanceMode(Globals.CommandLine);
            } else {
                RunGame();
            }
        }

        static void RunGame() {
#if !DEBUG
            // Naming a Mutex makes it available computer-wide. Use a name that's
            // unique to your company and application (e.g., include your URL).
            using (Mutex mutex = new Mutex(false, "Pokemon Mystery Universe (www.pmuniverse.net)")) {
                // Wait a few seconds if contended, in case another instance
                // of the program is still in the process of shutting down.

                if (!mutex.WaitOne(TimeSpan.FromSeconds(3), false)) {
                    Exceptions.ExceptionHandler.OnException(new Exception("Another instance of PMU is already running!"));
                    return;
                }
                InitializeGame();
            }
#else
            InitializeGame();
#endif
        }

        private static void InitializeGame() {
            // Create filetype associations
            CheckFileAssociations();

            Sdl.SdlLoader.InitializeSdl();
        }

        static void CheckFileAssociations() {
            RegistryKey key = Registry.ClassesRoot.OpenSubKey(".pmuskn");
            if (key == null) {
                // The association doesn't exist, restart as admin in maintenance mode
                if (!Client.Logic.Security.Admin.IsAdmin()) {
                    if (Client.Logic.Security.Admin.IsVistaOrHigher()) {
                        Process process = Client.Logic.Security.Admin.StartProcessElevated(PMU.Core.Environment.StartupPath, "-createfileassociations");
                        process.WaitForExit();
                    }
                } else {
                    Process process = Process.Start(PMU.Core.Environment.StartupPath, "-createfileassociations");
                    process.WaitForExit();
                }
            }
        }

        static void RunMaintenanceMode(Command command) {
            if (command.ContainsCommandArg("-installskin")) {
                // We are trying to install a new skin
                string skinPackagePath = command["-installskin"];
                if (!string.IsNullOrEmpty(skinPackagePath) && File.Exists(skinPackagePath)) {
                    bool installed = Skins.SkinManager.InstallSkin(skinPackagePath);
                    if (installed) {
                        System.Windows.Forms.MessageBox.Show("The skin has been installed!", "Installation completed!");
                    } else {
                        System.Windows.Forms.MessageBox.Show("The selected file is not a valid skin package.", "Invalid Package");
                    }
                }
            } else if (command.ContainsCommandArg("-createfileassociations")) {
                // Create associations for the skin loader
                RegistryKey RegKey = Registry.ClassesRoot.CreateSubKey(".pmuskn");
                RegKey.SetValue("", "PMU.Skin.Loader");
                RegKey.Close();

                RegKey = Registry.ClassesRoot.CreateSubKey("PMU.Skin.Loader");
                RegKey.SetValue("", "Pokémon Mystery Universe Skin Package");
                RegKey.Close();

                RegKey = Registry.ClassesRoot.CreateSubKey("PMU.Skin.Loader" + "\\DefaultIcon");
                RegKey.SetValue("", @"C:\Program Files\Pokemon Mystery Universe\Client\pmuicon.ico" + "," + "0");
                RegKey.Close();

                RegKey = Registry.ClassesRoot.CreateSubKey("PMU.Skin.Loader" + "\\" + "Shell" + "\\" + "Open");
                RegKey = RegKey.CreateSubKey("Command");
                RegKey.SetValue("", "\"" + PMU.Core.Environment.StartupPath + "\" -installskin \"%1\"");
                RegKey.Close();
            }
        }

        static bool IsRunningUnderMaintenanceMode(Command command) {
            if (command.ContainsCommandArg("-installskin")) {
                return true;
            } else if (command.ContainsCommandArg("-createfileassociations")) {
                return true;
            }

            return false;
        }

        static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e) {
            Logic.Exceptions.ErrorBox.ShowDialog("Error", ((Exception)e.ExceptionObject).Message, ((Exception)e.ExceptionObject).ToString());
        }

        //static void CurrentDomain_FirstChanceException(object sender, System.Runtime.ExceptionServices.FirstChanceExceptionEventArgs e) {
        //    //System.Windows.Forms.MessageBox.Show(e.Exception.ToString());
        //}

        /// <summary>
        /// Loads the game data.
        /// </summary>
        public static void LoadData() {
           
            // Load the main font
            Graphics.FontManager.InitFonts();

            IO.Options.Initialize();
            IO.Options.LoadOptions();
            IO.ControlLoader.LoadControls();

            // Load the initial skin
            Skins.SkinManager.ChangeActiveSkin(IO.Options.ActiveSkin);

            if (Globals.InDebugMode) {
                // Init the debug controls
                Globals.GameScreen.InitControls();
            }

            SdlDotNet.Widgets.Widgets.Initialize(
                SdlDotNet.Graphics.Video.Screen,
                SdlDotNet.Widgets.Widgets.ResourceDirectory,
                IO.Paths.FontPath + "tahoma.ttf",
                12
                );
            //SdlDotNet.Widgets.Settings.DefaultFont = ;
            //SdlDotNet.Widgets.WindowManager.Initialize(SdlDotNet.Graphics.Video.Screen);
            //SdlDotNet.Widgets.WindowManager.WindowSwitcherEnabled = false;
            GraphicsCache.LoadCache();
            Input.InputProcessor.Initialize();
            // Switch to the loading window
            SdlDotNet.Widgets.WindowManager.AddWindow(new winLoading());
            ((winLoading)WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Checking for updates...");
            DoUpdateCheck();
        }

        private static void DoUpdateCheck() {
#if !DEBUG
            Updater.UpdateEngine updateEngine = new Updater.UpdateEngine("clientpackagekey7wf8ysdch");
            Thread updateCheckThread = new Thread(new ThreadStart(delegate()
            {
                try {
                    DeleteToDeleteFiles();
                    if (updateEngine.CheckForUpdates()) {
                        WindowSwitcher.FindWindow("winLoading").Visible = false;
                        SdlDotNet.Widgets.WindowManager.AddWindow(new Updater.winUpdater(updateEngine));
                        Windows.WindowSwitcher.UpdaterWindow.AlwaysOnTop = true;
                    } else {
                        PostUpdateLoad();
                    }
                } catch (Exception ex) {
                    PostUpdateLoad();
                }
            }));
            updateCheckThread.Start();
#else
            PostUpdateLoad();
#endif
        }

        private static void PostUpdateLoad() {
            Music.Music.Initialize();
            Skins.SkinManager.PlaySkinMusic();
            winLoading winLoading = WindowSwitcher.FindWindow("winLoading") as winLoading;
            winLoading.UpdateLoadText("Loading game...");
            CheckFolders();
            LoadGuis();
            LoadGraphics();

            // TODO: Add encryption key here
            Logic.Globals.Encryption = new Client.Logic.Security.Encryption();
            winLoading.UpdateLoadText("Connecting to server...");
            // Load TCP and connect to the server
            NetworkManager.InitializeTcp();
            NetworkManager.InitializePacketSecurity();
            NetworkManager.Connect();
            winLoading.Close();
            Windows.WindowSwitcher.ShowMainMenu();
            Globals.GameLoaded = true;
        }

        private static void DeleteToDeleteFiles() {
            string[] files = Directory.GetFiles(System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location), "*ToDelete", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++) {
                try {
                    File.Delete(files[i]);
                } catch { }
            }
        }

        /// <summary>
        /// Loads the game graphics.
        /// </summary>
        public static void LoadGraphics() {
            Graphics.GraphicsManager.Initialize();

            //if (IO.IO.FileExists("GFX\\BigSpells.pmugfx")) {
            //    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Loading graphics... [Big Spells]");
            //    Graphics.GraphicsManager.BigSpells = Graphics.SurfaceManager.LoadSurface(IO.IO.GetGfxPath("BigSpells.pmugfx"));
            //    Graphics.GraphicsManager.BigSpells.TransparentColor = Graphics.GraphicsManager.BigSpells.GetPixel(new Point(0, 0));
            //    Graphics.GraphicsManager.BigSpells.Transparent = true;
            //}
            //if (IO.IO.FileExists("GFX\\Arrows.pmugfx")) {
            //    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Loading graphics... [Arrows]");
            //    Graphics.GraphicsManager.Arrows = Graphics.SurfaceManager.LoadSurface(IO.IO.GetGfxPath("Arrows.pmugfx"));
            //    Graphics.GraphicsManager.Arrows.TransparentColor = Graphics.GraphicsManager.Arrows.GetPixel(new Point(0, 0));
            //    Graphics.GraphicsManager.Arrows.Transparent = true;
            //}
            //if (IO.IO.FileExists("GFX\\BigSprites.pmugfx")) {
            //    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Loading graphics... [Big Sprites]");
            //    Graphics.GraphicsManager.BigSprites = Graphics.SurfaceManager.LoadSurface(IO.IO.GetGfxPath("BigSprites.pmugfx"));
            //    Graphics.GraphicsManager.BigSprites.TransparentColor = Graphics.GraphicsManager.BigSprites.GetPixel(new Point(0, 0));
            //    Graphics.GraphicsManager.BigSprites.Transparent = true;
            //}

            if (IO.IO.FileExists("GFX\\Items\\Items.png")) {
                Graphics.GraphicsManager.Items = Graphics.SurfaceManager.LoadSurface(IO.IO.GetGfxPath("Items\\Items.png"));
                Graphics.GraphicsManager.Items.Transparent = true;
            }
            //if (IO.IO.FileExists("GFX\\Spells.pmugfx")) {
            //    ((Windows.winLoading)Windows.WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Loading graphics... [Spells]");
            //    Graphics.GraphicsManager.Spells = Graphics.SurfaceManager.LoadSurface(IO.IO.GetGfxPath("Spells.pmugfx"));
            //    Graphics.GraphicsManager.Spells.TransparentColor = Graphics.GraphicsManager.Spells.GetPixel(new Point(0, 0));
            //    Graphics.GraphicsManager.Spells.Transparent = true;
            //}

            for (int i = 0; i < Graphics.GraphicsManager.MAX_TILES; i++) {
                if (IO.IO.FileExists(IO.Paths.GfxPath + "Tiles\\Tiles" + i.ToString() + ".tile")) {
                    Graphics.GraphicsManager.LoadTilesheet(i);
                }
            }

        }

#if DEBUG
        /// <summary>
        /// Encrypts all png graphics
        /// </summary>
        private static void DoGraphicConversions() {
            //if (IO.IO.FileExists("GFX\\Arrows.png")) {
            //    System.IO.File.WriteAllBytes(IO.IO.GetGfxPath("Arrows.pmugfx"), Graphics.SurfaceManager.EncryptSurface(IO.IO.GetGfxPath("Arrows.png")));
            //    System.IO.File.Delete(IO.IO.GetGfxPath("Arrows.png"));
            //}
            //if (IO.IO.FileExists("GFX\\BigSpells.png")) {
            //    System.IO.File.WriteAllBytes(IO.IO.GetGfxPath("BigSpells.pmugfx"), Graphics.SurfaceManager.EncryptSurface(IO.IO.GetGfxPath("BigSpells.png")));
            //    System.IO.File.Delete(IO.IO.GetGfxPath("BigSpells.png"));
            //}
            //if (IO.IO.FileExists("GFX\\BigSprites.png")) {
            //    System.IO.File.WriteAllBytes(IO.IO.GetGfxPath("BigSprites0.pmugfx"), Graphics.SurfaceManager.EncryptSurface(IO.IO.GetGfxPath("BigSprites.png")));
            //    System.IO.File.Delete(IO.IO.GetGfxPath("BigSprites.png"));
            //}
            //if (IO.IO.FileExists("GFX\\Spells.png")) {
            //    System.IO.File.WriteAllBytes(IO.IO.GetGfxPath("Spells.pmugfx"), Graphics.SurfaceManager.EncryptSurface(IO.IO.GetGfxPath("Spells.png")));
            //    System.IO.File.Delete(IO.IO.GetGfxPath("Spells.png"));
            //}
            //for (int i = 0; i < Graphics.GraphicsManager.MAX_TILES; i++) {
            //    if (IO.IO.FileExists(IO.Paths.GfxPath + "Tiles" + i.ToString() + ".png")) {
            //        System.IO.File.WriteAllBytes(IO.Paths.GfxPath + "Tiles" + i.ToString() + ".pmugfx", Graphics.SurfaceManager.EncryptSurface(IO.Paths.GfxPath + "Tiles" + i.ToString() + ".png"));
            //        System.IO.File.Delete(IO.Paths.GfxPath + "Tiles" + i.ToString() + ".png");
            //    }
            //}
            string[] files = System.IO.Directory.GetFiles(IO.Paths.GfxPath + "Sprites/", "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++) {
                if (files[i].EndsWith(".png")) {
                    System.IO.File.WriteAllBytes(System.IO.Path.ChangeExtension(files[i], ".pmugfx"), Graphics.SurfaceManager.EncryptSurface(files[i]));
                    System.IO.File.Delete(files[i]);
                }
            }
            files = System.IO.Directory.GetFiles(IO.Paths.GfxPath + "Spells/", "*", SearchOption.TopDirectoryOnly);
            for (int i = 0; i < files.Length; i++) {
                if (files[i].EndsWith(".png")) {
                    System.IO.File.WriteAllBytes(System.IO.Path.ChangeExtension(files[i], ".pmugfx"), Graphics.SurfaceManager.EncryptSurface(files[i]));
                    System.IO.File.Delete(files[i]);
                }
            }
            files = System.IO.Directory.GetFiles(IO.Paths.GfxPath + "Mugshots/", "*", SearchOption.TopDirectoryOnly);


            for (int i = 0; i < files.Length; i++) {
                if (files[i].EndsWith(".png")) {
                    string path = System.IO.Path.GetDirectoryName(files[i]) + "/" + System.IO.Path.GetFileNameWithoutExtension(files[i]) + ".pmugfx";

                    SdlDotNet.Graphics.Surface mugshotSurface = new SdlDotNet.Graphics.Surface(files[i]);
                    mugshotSurface.ReplaceColor(Color.FromArgb(255, 254, 254, 254), Color.FromArgb(254, 254, 254, 254));
                    mugshotSurface.ReplaceColor(Color.FromArgb(255, 255, 255, 255), Color.FromArgb(254, 255, 255, 255));
                    SurfaceManager.SaveSurface(mugshotSurface, files[i]);
                    
                    System.IO.File.WriteAllBytes(path, Graphics.SurfaceManager.EncryptSurface(files[i]));
                    System.IO.File.Delete(files[i]);
                }
            }

        }
#endif

        /// <summary>
        /// Loads the core GUI's.
        /// </summary>
        public static void LoadGuis() {
            //Graphics.GuiManager.LoadGui(Graphics.GuiManager.Menu.Menu1);
            //Graphics.GuiManager.LoadGui(Graphics.GuiManager.Menu.Menu2);
        }

        #endregion Methods
    }
}