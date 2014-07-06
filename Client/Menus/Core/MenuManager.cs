namespace Client.Logic.Menus.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Widgets;

    using System.Threading;

    class MenuManager : Panel
    {
        #region Fields

        IMenu activeMenu;
        bool blockInput;
        List<IMenu> openMenus;
        //ReaderWriterLockSlim rwLock;

        public List<IMenu> OpenMenus {
            get { return openMenus; }
        }
        bool hasModalMenu;
        Logic.Widgets.MapViewer mapViewer;

        #endregion Fields

        #region Constructors

        public MenuManager(string name, Logic.Widgets.MapViewer mapViewer)
            : base(name) {
            this.BackColor = Color.Transparent;
            blockInput = true;
            openMenus = new List<IMenu>();
            //rwLock = new ReaderWriterLockSlim();

            this.mapViewer = mapViewer;

            //lblTest = new Label("lblTest");
            //lblTest.Location = new Point(100, 100);
            //lblTest.AutoSize = true;
            //lblTest.Font = Graphics.FontManager.LoadFont("PMU", 24);
            //lblTest.Text = "Test Menu Opened";

            //this.AddWidget(lblTest);
        }

        #endregion Constructors

        #region Properties

        public bool BlockInput {
            get { return blockInput; }
            set { blockInput = value; }
        }

        public bool HasModalMenu {
            get { return hasModalMenu; }
        }

        #endregion Properties

        #region Methods

        public void AddMenu(IMenu menu) {
            AddMenu(menu, false);
        }

        public void AddMenu(IMenu menu, bool modal) {
            if (openMenus.Contains(menu) == false) {
                menu.MenuPanel.Parent = null;

                //rwLock.EnterWriteLock();
                try {
                    openMenus.Add(menu);
                } finally {
                    //rwLock.ExitWriteLock();
                }

                menu.Modal = modal;
                if (modal) {
                    hasModalMenu = true;
                }
                menu.MenuPanel.ReadyToConfigure = true;
                //menu.MenuPanel.Alpha = 155;
                //this.AddWidget(menu.MenuPanel);
            }
        }

        public void RemoveMenu(IMenu menu) {
            int index = -1;
            //bool localLock = false;
            //if (!rwLock.IsUpgradeableReadLockHeld) {
            //    rwLock.EnterUpgradeableReadLock();
            //    localLock = true;
            //}
            try {
                index = openMenus.IndexOf(menu);
                if (index > -1) {
                    openMenus[index].MenuPanel.Close();
                    //this.RemoveWidget(menu.MenuPanel.Name);
                    if (activeMenu == menu) {
                        activeMenu = null;
                    }
                    //rwLock.EnterWriteLock();
                    try {
                        openMenus.RemoveAt(index);
                    } finally {
                        //rwLock.ExitWriteLock();
                    }
                }
            } finally {
                //if (localLock) {
                //    rwLock.ExitUpgradeableReadLock();
                //}
            }
            if (index > -1) {
                CheckForModalMenus();
            }
        }

        private void CheckForModalMenus() {
            //bool localLock = false;
            //if (!rwLock.IsReadLockHeld) {
            //    rwLock.EnterReadLock();
            //    localLock = true;
            //}
            try {
                for (int i = 0; i < openMenus.Count; i++) {
                    if (openMenus[i].Modal) {
                        hasModalMenu = true;
                        return;
                    }
                }
                hasModalMenu = false;
            } finally {
                //if (localLock) {
                //    rwLock.ExitReadLock();
                //}
            }
        }

        public void CloseOpenMenus() {
            //rwLock.EnterWriteLock();
            try {
                for (int i = openMenus.Count - 1; i >= 0; i--) {
                    openMenus[i].MenuPanel.Close();
                    //this.RemoveWidget(openMenus[i].MenuPanel.Name);
                    openMenus.RemoveAt(i);
                }
            } finally {
                //rwLock.ExitWriteLock();
            }
            hasModalMenu = false;
            activeMenu = null;
        }

        public IMenu FindMenu(string menuName) {
            //bool localLock = false;
            //if (!rwLock.IsReadLockHeld) {
            //    rwLock.EnterReadLock();
            //    localLock = true;
            //}
            try {
                for (int i = 0; i < openMenus.Count; i++) {
                    if (openMenus[i].MenuPanel.Name == menuName) {
                        return openMenus[i];
                    }
                }
                return null;
            } finally {
                //if (localLock) {
                //    rwLock.ExitReadLock();
                //}
            }
        }

        public void HandleKeyDown(SdlDotNet.Input.KeyboardEventArgs e) {
            if (activeMenu != null) {
                activeMenu.MenuPanel.OnKeyboardDown(e);
            } else {
                //rwLock.EnterUpgradeableReadLock();
                try {
                    if (openMenus.Count > 0) {
                        openMenus[0].MenuPanel.OnKeyboardDown(e);
                    }
                } finally {
                    //rwLock.ExitUpgradeableReadLock();
                }
            }
        }

        public void HandleKeyUp(SdlDotNet.Input.KeyboardEventArgs e) {
            if (activeMenu != null) {
                activeMenu.MenuPanel.OnKeyboardUp(e);
            } else {
                //rwLock.EnterUpgradeableReadLock();
                try {
                    if (openMenus.Count > 0) {
                        openMenus[0].MenuPanel.OnKeyboardUp(e);
                    }
                } finally {
                    //rwLock.ExitUpgradeableReadLock();
                }
            }
        }

        public void SetActiveMenu(IMenu menu) {
            //rwLock.EnterReadLock();
            try {
                if (openMenus.Contains(menu)) {
                    activeMenu = menu;
                }
            } finally {
                //rwLock.ExitReadLock();
            }
        }

        public void SetActiveMenu(string menuName) {
            IMenu menu = FindMenu(menuName);
            if (menu != null) {
                activeMenu = menu;
            }
        }

        public override void OnMouseMotion(SdlDotNet.Input.MouseMotionEventArgs e) {
            base.OnMouseMotion(e);
            //rwLock.EnterUpgradeableReadLock();
            try {
                foreach (IMenu menu in openMenus) {
                    if (DrawingSupport.PointInBounds(e.Position, menu.MenuPanel.Bounds)) {
                        menu.MenuPanel.OnMouseMotion(e);
                        break;
                    }
                }
            } finally {
                //rwLock.ExitUpgradeableReadLock();
            }
        }

        public override void OnMouseDown(MouseButtonEventArgs e) {
            base.OnMouseDown(e);
            //rwLock.EnterUpgradeableReadLock();
            try {
                foreach (IMenu menu in openMenus) {
                    if (DrawingSupport.PointInBounds(e.ScreenPosition, menu.MenuPanel.Bounds)) {
                        menu.MenuPanel.OnMouseDown(new MouseButtonEventArgs(e.MouseEventArgs, new Point(e.Position.X + this.X, e.Position.Y + this.Y)));
                        break;
                    }
                }
            } finally {
                //rwLock.ExitUpgradeableReadLock();
            }
        }

        public override void OnMouseUp(MouseButtonEventArgs e) {
            base.OnMouseUp(e);
            //rwLock.EnterUpgradeableReadLock();
            try {
                foreach (IMenu menu in openMenus) {
                    if (DrawingSupport.PointInBounds(e.ScreenPosition, menu.MenuPanel.Bounds)) {
                        menu.MenuPanel.OnMouseUp(new MouseButtonEventArgs(e.MouseEventArgs, new Point(e.Position.X + this.X, e.Position.Y + this.Y)));
                        break;
                    }
                }
            } finally {
                //rwLock.ExitUpgradeableReadLock();
            }
        }

        SdlDotNet.Graphics.Surface destSurf;
        public override void OnTick(SdlDotNet.Core.TickEventArgs e) {
            //rwLock.EnterUpgradeableReadLock();
            try {
                for (int i = 0; i < openMenus.Count; i++) {
                    openMenus[i].MenuPanel.OnTick(e);
                }
               
            } finally {
                //rwLock.ExitUpgradeableReadLock();
            }
            base.OnTick(e);
        }

        public override void BlitToScreen(SdlDotNet.Graphics.Surface destinationSurface) {
            base.BlitToScreen(destinationSurface);
            //rwLock.EnterReadLock();
            destSurf = destinationSurface;
            for (int i = 0; i < openMenus.Count; i++) {
                if (openMenus[i].MenuPanel.ReadyToConfigure) {
                    openMenus[i].MenuPanel.Location = new Point(this.X + openMenus[i].MenuPanel.X, this.Y + openMenus[i].MenuPanel.Y);
                    //openMenus[i].MenuPanel.RequestRedraw();
                    openMenus[i].MenuPanel.ReadyToConfigure = false;
                }
                if (destSurf != null) {
                    openMenus[i].MenuPanel.BlitToScreen(destSurf);
                }
            }
            try {
                //for (int i = 0; i < openMenus.Count; i++) {
                //    if (openMenus[i].MenuPanel.ReadyToConfigure) {
                //        openMenus[i].MenuPanel.Location = new Point(this.X + openMenus[i].MenuPanel.X, this.Y + openMenus[i].MenuPanel.Y);
                //        //openMenus[i].MenuPanel.RequestRedraw();
                //        openMenus[i].MenuPanel.ReadyToConfigure = false;
                //    }
                //    openMenus[i].MenuPanel.BlitToScreen(destinationSurface);
                //}
            } finally {
                //rwLock.ExitReadLock();
            }
            //foreach (IMenu menu in openMenus) {
            //    menu.MenuPanel.BlitToScreen(destinationSurface);//, new Point(this.X + menu.MenuPanel.X, this.Y + menu.MenuPanel.Y));
            //}
        }

        public void DrawToScreen(SdlDotNet.Graphics.Surface destinationSurface) {
            //for (int i = 0; i < openMenus.Count; i++) {
            //    if (openMenus[i].MenuPanel.ReadyToConfigure) {
            //        openMenus[i].MenuPanel.Location = new Point(this.X + openMenus[i].MenuPanel.X, this.Y + openMenus[i].MenuPanel.Y);
            //        //openMenus[i].MenuPanel.RequestRedraw();
            //        openMenus[i].MenuPanel.ReadyToConfigure = false;
            //    }
            //    openMenus[i].MenuPanel.BlitToScreen(destinationSurface);
            //}
        }

        #endregion Methods
    }
}