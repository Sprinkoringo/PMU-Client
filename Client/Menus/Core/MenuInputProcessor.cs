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


namespace Client.Logic.Menus.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    using SdlInput = SdlDotNet.Input;

    class MenuInputProcessor
    {
        #region Methods

        public static void OnKeyDown(SdlInput.KeyboardEventArgs e) {
            //if (e.Key == SdlInput.Key.Escape) {
            //    if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible && Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu == false) {
            //        Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
            //        Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
            //        Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
            //    } 
            //} else if (e.Key == SdlInput.Key.F11) {
            //    Logic.Graphics.SurfaceManager.SaveSurface(SdlDotNet.Graphics.Video.Screen, IO.Paths.StartupPath + "Screenshot.png");
            //} else {
                if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                    Windows.WindowSwitcher.GameWindow.MenuManager.HandleKeyDown(e);
                }
            //}
        }

        public static void OnKeyUp(SdlInput.KeyboardEventArgs e) {
            if (e.Key == SdlInput.Key.Escape) {
                if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible && Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu == false) {
                    Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
                    Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
                    Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                }
            } else if (e.Key == SdlInput.Key.F11) {
                if (System.IO.Directory.Exists(IO.Paths.StartupPath + "Screenshots") == false) {
                    System.IO.Directory.CreateDirectory(IO.Paths.StartupPath + "Screenshots");
                }
                int openScreenshot = -1;
                for (int i = 1; i < Int32.MaxValue; i++) {
                    if (System.IO.File.Exists(IO.Paths.StartupPath + "Screenshots/Screenshot" + i + ".png") == false) {
                        openScreenshot = i;
                        break;
                    }
                }
                if (openScreenshot > -1) {
                    Logic.Graphics.SurfaceManager.SaveSurface(SdlDotNet.Graphics.Video.Screen, IO.Paths.StartupPath + "Screenshots/Screenshot" + openScreenshot + ".png");
                    ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                    if (chat != null) {
                        chat.AppendChat("Screenshot #" + openScreenshot + " saved!", System.Drawing.Color.Yellow);
                    }
                }
            } else {
                if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                    Windows.WindowSwitcher.GameWindow.MenuManager.HandleKeyUp(e);
                }
            }
        }

        #endregion Methods
    }
}