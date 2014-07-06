namespace Client.Logic.Windows
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class WindowManager
    {
        #region Fields

        private static List<Core.Window> mOpenWindows;

        #endregion Fields

        #region Enumerations

        public enum WindowState
        {
            Normal,
            Minimized,
            Maximized
        }

        #endregion Enumerations

        #region Methods

        public static void AddWindow(Core.Window windowToAdd)
        {
            if (windowToAdd != null && mOpenWindows.Contains(windowToAdd) == false) {
                bool autoSwitch = false;
                if (mOpenWindows.Count > 0) {
                    if (mOpenWindows[mOpenWindows.Count - 1].AlwaysOnTop) {
                        autoSwitch = true;
                    }
                }
                mOpenWindows.Add(windowToAdd);
                if (autoSwitch) {
                    SwitchWindows(mOpenWindows.Count - 2, mOpenWindows.Count - 1);
                }
                if (windowToAdd.ShowInTaskBar && windowToAdd.TaskBarText != "") {
                    Globals.GameScreen.TaskBar.AddButton(new Client.Logic.Gui.TaskBar.TaskBarButton(windowToAdd));
                }
            }
        }

        public static void AddWindow(WindowSwitcher.Window windowToAdd)
        {
            AddWindow(WindowSwitcher.GetWindow(windowToAdd, true));
        }

        public static void BringWindowToFront(Core.Window window)
        {
            if (mOpenWindows.Contains(window)) {
                SwitchWindows(mOpenWindows.IndexOf(window), mOpenWindows.Count - 1);
            }
        }

        public static void Initialize()
        {
            mOpenWindows = new List<Core.Window>();
        }

        public static bool IsWindowOpen(Core.Window windowToCheck)
        {
            return mOpenWindows.Contains(windowToCheck);
        }

        public static bool IsWindowOpen(WindowSwitcher.Window windowToCheck)
        {
            return IsWindowOpen(WindowSwitcher.GetWindow(windowToCheck));
        }

        public static bool IsWindowTopMost(Core.Window window)
        {
            if (mOpenWindows.Count > 0) {
                return window == mOpenWindows[mOpenWindows.Count - 1];
            } else {
                return false;
            }
        }

        public static void Keyboard_Down(SdlDotNet.Input.KeyboardEventArgs e)
        {
            if (mOpenWindows.Count > 0) {
                for (int i = mOpenWindows.Count - 1; i >= 0; i--) {
                    if (mOpenWindows[i].WindowState != WindowState.Minimized) {
                        mOpenWindows[i].Keyboard_Down(e);
                        break;
                    }
                }
                //GetTopMostWindow().Mouse_Down(e);
            }
        }

        public static void Mouse_Down(SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (mOpenWindows.Count > 0) {
                for (int i = mOpenWindows.Count - 1; i >= 0; i--) {
                    if (mOpenWindows[i].WindowState != WindowState.Minimized && PointInBounds(e.Position, mOpenWindows[i].FullBounds)) {
                        mOpenWindows[i].Mouse_Down(e);
                        if (mOpenWindows.Count > 0) {
                            if (mOpenWindows[mOpenWindows.Count - 1].AlwaysOnTop == false && i != mOpenWindows.Count - 1) {
                                SwitchWindows(i, mOpenWindows.Count - 1);
                            }
                        }
                        break;
                    }
                }
                //GetTopMostWindow().Mouse_Down(e);
            }
        }

        public static void Mouse_Motion(SdlDotNet.Input.MouseMotionEventArgs e)
        {
            if (mOpenWindows.Count > 0) {
                //GetTopMostWindow().Mouse_Motion(e);
                for (int i = mOpenWindows.Count - 1; i >= 0; i--) {
                    //if (PointInBounds(e.Position, mOpenWindows[i].FullBounds)) {
                    mOpenWindows[i].Mouse_Motion(e);
                    //if (mOpenWindows[mOpenWindows.Count - 1].AlwaysOnTop == false && i != mOpenWindows.Count - 1) {
                    //    SwitchWindows(i, mOpenWindows.Count - 1);
                    //}
                    //break;
                    //}
                }
            }
        }

        public static void Mouse_Up(SdlDotNet.Input.MouseButtonEventArgs e)
        {
            if (mOpenWindows.Count > 0) {
                //GetTopMostWindow().Mouse_Up(e);
                for (int i = mOpenWindows.Count - 1; i >= 0; i--) {
                    if (mOpenWindows[i].WindowState != WindowState.Minimized && PointInBounds(e.Position, mOpenWindows[i].FullBounds)) {
                        mOpenWindows[i].Mouse_Up(e);
                        //if (mOpenWindows[mOpenWindows.Count - 1].AlwaysOnTop == false && i != mOpenWindows.Count - 1) {
                        //    SwitchWindows(i, mOpenWindows.Count - 1);
                        //}
                        break;
                    }
                }
            }
        }

        public static void RemoveWindow(Core.Window windowToRemove)
        {
            if (windowToRemove != null) {
                windowToRemove.CloseWindow();
                Globals.GameScreen.TaskBar.RemoveButton(windowToRemove);
                mOpenWindows.Remove(windowToRemove);
                windowToRemove = null;
            }
        }

        public static void RemoveWindow(WindowSwitcher.Window windowToRemove)
        {
            if (WindowSwitcher.GetWindow(windowToRemove, false) != null) {
                RemoveWindow(WindowSwitcher.GetWindow(windowToRemove, false));
            }
        }

        public static void RemoveWindowQuick(Core.Window windowToRemove)
        {
            if (windowToRemove != null) {
                Globals.GameScreen.TaskBar.RemoveButton(windowToRemove);
                mOpenWindows.Remove(windowToRemove);
                windowToRemove = null;
            }
        }

        public static void Tick(SdlDotNet.Graphics.Surface screen, SdlDotNet.Core.TickEventArgs e)
        {
            for (int i = 0; i < mOpenWindows.Count; i++) {
                mOpenWindows[i].Tick(screen, e);
            }
        }

        private static Core.Window GetTopMostWindow()
        {
            if (mOpenWindows.Count > 0) {
                return mOpenWindows[mOpenWindows.Count - 1];
            } else {
                return null;
            }
        }

        private static bool PointInBounds(Point pointToTest, Rectangle bounds)
        {
            if (pointToTest.X >= bounds.X && pointToTest.Y >= bounds.Y && pointToTest.X - bounds.Location.X <= bounds.Width && pointToTest.Y - bounds.Location.Y <= bounds.Height) {
                return true;
            } else {
                return false;
            }
        }

        private static void SwitchWindows(int oldSlot, int newSlot)
        {
            if (mOpenWindows.Count > oldSlot && mOpenWindows.Count > newSlot) {
                Core.Window temp = mOpenWindows[oldSlot];
                mOpenWindows[oldSlot] = mOpenWindows[newSlot];
                mOpenWindows[newSlot] = temp;
            }
        }

        #endregion Methods
    }
}