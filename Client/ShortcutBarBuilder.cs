using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Client.Logic.Graphics;
using Client.Logic.Stories;
using Client.Logic.Stories.Segments;
using Client.Logic.Network;

namespace Client.Logic
{
    /// <summary>
    /// Class to add buttons to a ShortcutBar
    /// </summary>
    class ShortcutBarBuilder
    {
        /// <summary>
        /// Adds the 'Show Menu' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddShowMenuButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("menuButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showmenu.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showmenu-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(menuButton_Click);
            shortcutBar.AddButton(button);
        }

        static void menuButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (Menus.MenuSwitcher.CanShowMenu()) {
                Menus.MenuSwitcher.ShowMainMenu();
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                //if (!Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                //    Windows.WindowSwitcher.GameWindow.MenuManager.Visible = true;
                //    Windows.WindowSwitcher.GameWindow.MenuManager.Focus();
                //    Menus.MenuSwitcher.ShowMainMenu();
                //} else {
                //    Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
                //    Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
                //    Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                //}
            }
        }

        /// <summary>
        /// Adds the 'Show Inventory' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddShowInvButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("invButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showinv.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showinv-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(invButton_Click);
            shortcutBar.AddButton(button);
        }

        static void invButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Menus.MenuSwitcher.ShowInventoryMenu(1);
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        /// <summary>
        /// Adds the 'Show Moves' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddShowMovesButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("showMovesButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showmoves.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showmoves-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(showMovesButton_Click);
            shortcutBar.AddButton(button);
        }

        static void showMovesButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Menus.MenuSwitcher.ShowMovesMenu();
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        /// <summary>
        /// Adds the 'Use Recovery Item' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddUseRecoveryItemButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("useRecoveryItemButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "userecoveryitem.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "userecoveryitem-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(useRecoveryItemButton_Click);
            shortcutBar.AddButton(button);
        }

        static void useRecoveryItemButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Players.Inventory inv = Players.PlayerManager.MyPlayer.Inventory;
            //for (int i = 1; i <= inv.Length; i++) {
                //if (inv[i].Num > 0) {
                    //if (Items.ItemHelper.Items[inv[i].Num].Type == Enums.ItemType.PotionAddHP) {
                        //ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                        //if (chat != null) {
                        //    chat.AppendChat("You have used a " + Items.ItemHelper.Items[inv[i].Num].Name + "!", Color.Yellow);
                        //}
                        //Messenger.SendUseItem(i);
                        //break;
                    //}
                //}
            //}
        }

        /// <summary>
        /// Adds the 'Show Online List' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddShowOnlineListButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("showOnlineListButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showonline.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showonline-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(showOnlineListButton_Click);
            shortcutBar.AddButton(button);
        }

        static void showOnlineListButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Menus.MenuSwitcher.ShowMenu(new Menus.mnuOnlineList("mnuOnlineList"));
            Network.Messenger.SendOnlineListRequest();
        }

        /// <summary>
        /// Adds the 'Show Options' button to the specified ShortcutBar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the button to</param>
        public static void AddShowOptionsButtonToBar(Widgets.ShortcutBar shortcutBar) {
            Widgets.ShortcutButton button = new Widgets.ShortcutButton("showOptionsButton");
            button.BackColor = Color.Transparent;
            button.Image = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showoptions.png");
            button.HighlightImage = Skins.SkinManager.LoadGuiElement("Game Window/ShortcutBar", "showoptions-h.png");
            button.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(showOptionsButton_Click);
            shortcutBar.AddButton(button);
        }

        static void showOptionsButton_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Menus.MenuSwitcher.ShowMenu(new Menus.mnuOptions("mnuOptions"));
        }

        /// <summary>
        /// Adds all buttons to the shortcut bar
        /// </summary>
        /// <param name="shortcutBar">The ShortcutBar to add the buttons to</param>
        public static void AssembleShortcutBarButtons(Widgets.ShortcutBar shortcutBar) {
            AddShowMenuButtonToBar(shortcutBar);
            AddShowInvButtonToBar(shortcutBar);
            AddShowMovesButtonToBar(shortcutBar);
            //AddUseRecoveryItemButtonToBar(shortcutBar);
            AddShowOnlineListButtonToBar(shortcutBar);
            AddShowOptionsButtonToBar(shortcutBar);
        }
    }
}
