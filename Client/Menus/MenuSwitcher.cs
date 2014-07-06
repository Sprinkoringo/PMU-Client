namespace Client.Logic.Menus
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class MenuSwitcher
    {
        #region Methods

        public static void ShowMainMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMapInfo("mnuMapInfo"));
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMainMenu("mnuMainMenu"));
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBasicStats("mnuBasicStats"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMainMenu");
            }
        }

        public static void ShowInventoryMenu(int itemSlot) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuInventory("mnuInventory", Enums.InvMenuType.Use, itemSlot));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");
            }
        }

        public static void ShowItemSummary(int itemNum, int itemSlot, Enums.InvMenuType originalMenu) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuItemSummary("mnuItemSummary", itemNum, itemSlot, originalMenu));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuItemSummary");
            }
        }

        public static void ShowMovesMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMoves("mnuMoves"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMoves");
            }
        }

        public static void ShowTeamMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTeam("mnuTeam"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTeam");
            }
        }

        public static void ShowGuildMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuGuild("mnuGuild"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuGuild");
            }
        }

        public static void ShowJobListMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuJobList("mnuJobList"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuJobList");
            }
        }

        public static void ShowJobSummary(Missions.Job job) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuJobDescription("mnuJobDescription", job, false));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuJobDescription");
            }
        }


        public static void ShowOthersMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuOthers("mnuOthers"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuOthers");
            }
        }

        public static void ShowHouseSelectionMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuVisitHouse("mnuVisitHouse"));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuVisitHouse");
            }
        }

        public static void ShowHouseWeatherMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuChangeWeather("mnuChangeWeather", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuChangeWeather");
            }
        }

        public static void ShowHouseDarknessMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuChangeDarkness("mnuChangeDarkness", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuChangeDarkness");
            }
        }

        public static void ShowHouseBoundsMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuChangeBounds("mnuChangeBounds", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuChangeBounds");
            }
        }

        public static void ShowHouseShopMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuAddShop("mnuAddShop", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuAddShop");
            }
        }

        public static void ShowHouseSoundMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuAddSound("mnuAddSound", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuAddSound");
            }
        }

        public static void ShowHouseNoticeMenu(int price, int wordPrice) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuAddNotice("mnuAddNotice", price, wordPrice));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuAddNotice");
            }
        }

        public static void ShowHouseSignMenu(int price) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuAddSign("mnuAddSign", price));
                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuAddSign");
            }
        }


        public static void ShowTournamentListingMenu(Tournaments.TournamentListing[] listings, Enums.TournamentListingMode mode) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTournamentListing("mnuTournamentListing", listings, mode));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTournamentListing");
            }
        }

        public static void ShowTournamentRulesEditorMenu(Tournaments.TournamentRules rules) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTournamentRulesEditor("mnuTournamentRulesEditor", rules));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTournamentRulesEditor");
            }
        }

        public static void ShowTournamentRulesViewerMenu(Tournaments.TournamentRules rules) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuTournamentRulesViewer("mnuTournamentRulesViewer", rules));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuTournamentRulesViewer");
            }
        }

        public static void OpenMissionBoard(string[] parse) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMissionBoard("mnuMissionBoard", parse));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMissionBoard");
            }
        }

        public static void ShowAssembly(string[] parse) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuAssembly("mnuAssembly", parse));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuAssembly");
            }
        }

        public static void ShowGuildCreate(string[] parse) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuGuildCreate("mnuGuildCreate", parse));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuGuildCreate");
            }
        }

        public static void ShowGuildManage(string[] parse) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuGuildManage("mnuGuildManage", parse));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuGuildManage");
            }
        }

        public static void OpenBankOptions() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBankOptions("mnuBankOptions"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBankOptions");
            }
        }

        public static void ShowBankDepositMenu(int itemSlot) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuInventory("mnuInventory", Enums.InvMenuType.Store, itemSlot));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");
            }
        }

        public static void ShowBankWithdrawMenu(int itemSlot) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuBank("mnuBank", itemSlot));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuBank");
                Network.Messenger.BankWithdrawMenu();
            }
        }

        public static void OpenShopOptions() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShopOptions("mnuShopOptions"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShopOptions");
            }
        }

        public static void ShowShopBuyMenu(int itemSlot) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Menus.mnuShopOptions menu = ((Menus.mnuShopOptions)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShopOptions"));
                if (menu != null) {
                    menu.Close(false);
                    Windows.WindowSwitcher.GameWindow.MenuManager.RemoveWidget("mnuShopOptions");
                    Windows.WindowSwitcher.GameWindow.MenuManager.RemoveWidget(menu.GroupedWidget.Name);
                    Windows.WindowSwitcher.GameWindow.MenuManager.OpenMenus.RemoveAt(Windows.WindowSwitcher.GameWindow.MenuManager.OpenMenus.IndexOf(menu));
                }
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuShop("mnuShop", itemSlot));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuShop");
                Network.Messenger.RequestShop();
            }
        }

        public static void ShowShopSellMenu(int itemSlot) {
            if (CanShowMenu()) {
                EnableMenuManager();

                Menus.mnuShopOptions menu = ((Menus.mnuShopOptions)Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuShopOptions"));
                if (menu != null) {
                    menu.Close(false);
                    Windows.WindowSwitcher.GameWindow.MenuManager.RemoveWidget("mnuShopOptions");
                    Windows.WindowSwitcher.GameWindow.MenuManager.OpenMenus.RemoveAt(Windows.WindowSwitcher.GameWindow.MenuManager.OpenMenus.IndexOf(menu));
                }

                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuInventory("mnuInventory", Enums.InvMenuType.Sell, itemSlot));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuInventory");

            }
        }

        public static void LinkShopRecallMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMoveRecall("mnuMoveRecall"));

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMoveRecall");
            }
        }

        public static void ShowHelpMenu() {
            ShowMenu(new mnuHelpTopics("mnuHelpTopics"));
        }

        public static void ShowHelpPage(string topic, int pageNumber) {
            ShowMenu(new mnuHelpPage("mnuHelpPage", topic, pageNumber));
        }

        public static void ShowMenu(Menus.Core.IMenu menu) {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();

                Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(menu);

                Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu(menu.MenuPanel.Name);
            }
        }

        public static void CloseAllMenus() {
            if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                if (!Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu) {
                    Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
                    Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
                    Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                }
            }
        }

        public static void HideMenuManager() {
            if (Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                if (!Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu) {
                    Windows.WindowSwitcher.GameWindow.MapViewer.Focus();
                    Windows.WindowSwitcher.GameWindow.MenuManager.Visible = false;
                }
            }
        }

        private static void EnableMenuManager() {
            if (!Windows.WindowSwitcher.GameWindow.MenuManager.Visible) {
                Windows.WindowSwitcher.GameWindow.MenuManager.Visible = true;
                Windows.WindowSwitcher.GameWindow.MenuManager.Focus();
            }
        }

        public static bool CanShowMenu() {
            if (Windows.WindowSwitcher.GameWindow.MenuManager.HasModalMenu) {
                return false;
            }

            return true;
        }

        public static void ShowBlankMenu() {
            if (CanShowMenu()) {
                EnableMenuManager();
                Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
            }
        }

        #endregion Methods
    }
}