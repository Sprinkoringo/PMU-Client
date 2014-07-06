using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Logic.Sdl
{
    class SdlEventHandler
    {
        public static void SdlDotNet_Core_Events_KeyboardUp(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            SdlDotNet.Widgets.WindowManager.HandleKeyboardUp(e);
        }

        public static void SdlDotNet_Core_Events_KeyboardDown(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            switch (e.Key) {
                case SdlDotNet.Input.Key.F1: {
                        //if (SdlDotNet.Graphics.Video.Screen.FullScreen == false) {
                        //    SdlDotNet.Graphics.Video.SetVideoMode(SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height, 32, false, false, true);
                        //} else {
                        //    SdlDotNet.Graphics.Video.SetVideoMode(SdlDotNet.Graphics.Video.Screen.Width, SdlDotNet.Graphics.Video.Screen.Height, 32, false, false, false);
                        //}
                    }
                    break;
                case SdlDotNet.Input.Key.Q: {
                        //Sdl.SdlCore.QuitApplication();
                    }
                    break;
            }
            SdlDotNet.Widgets.WindowManager.HandleKeyboardDown(e);
        }

        public static void SdlDotNet_Core_Events_MouseMotion(object sender, SdlDotNet.Input.MouseMotionEventArgs e) {
            SdlDotNet.Widgets.WindowManager.HandleMouseMotion(e);
        }

        public static void SdlDotNet_Core_Events_MouseButtonUp(object sender, SdlDotNet.Input.MouseButtonEventArgs e) {
            SdlDotNet.Widgets.WindowManager.HandleMouseButtonUp(e);
        }

        public static void SdlDotNet_Core_Events_MouseButtonDown(object sender, SdlDotNet.Input.MouseButtonEventArgs e) {
            SdlDotNet.Widgets.WindowManager.HandleMouseButtonDown(e);
        }

        public static void SdlDotNet_Core_Events_Quit(object sender, SdlDotNet.Core.QuitEventArgs e) {
            Music.Music.Dispose();
            Environment.Exit(0);
            SdlCore.QuitApplication();
        }

        public static void SdlDotNet_Core_Events_Tick(object sender, SdlDotNet.Core.TickEventArgs e) {
            // Only redraw the window if it isn't minimized
            //if (SdlDotNet.Graphics.Video.IsActive) {
            try { 
                if (Skins.SkinManager.ScreenBackground != null && Globals.InGame == false) {
                    SdlDotNet.Graphics.Video.Screen.Blit(Skins.SkinManager.ScreenBackground, new Point(0, 0));
                } else {
                    if (Skins.SkinManager.ActiveSkin.IngameBackground != null) {
                        SdlDotNet.Graphics.Video.Screen.Blit(Skins.SkinManager.ActiveSkin.IngameBackground, new Point(0, 0));
                    } else {
                        SdlDotNet.Graphics.Video.Screen.Fill(Color.SteelBlue);
                    }
                }
            } catch {
                SdlDotNet.Graphics.Video.Screen.Fill(Color.SteelBlue);
            }

            //try {
            // Check if the FPS isn't something absurd
            //if (!(SdlDotNet.Core.Events.Fps < 10 || SdlDotNet.Core.Events.Fps > Constants.FRAME_RATE + 10)) {
            SdlDotNet.Widgets.WindowManager.DrawWindows(e);
            //} catch { }
            //}

            //if (Graphics.FontManager.GameFont != null) {
            //    SdlDotNet.Graphics.Video.Screen.Blit(Graphics.FontManager.GameFont.Render("FPS: " + SdlDotNet.Core.Events.Fps.ToString(), Color.Black), new Point(0, 0));
            //}

            SdlDotNet.Graphics.Video.Update();
            //} else {
            //    // Game window isn't active
            //}
        }

    }
}
