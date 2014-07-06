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


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuOthers : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        const int MAX_ITEMS = 4;

        Widgets.MenuItemPicker itemPicker;
        Label lblOthers;
        Label lblOptions;
        Label lblOnlineList;
        Label lblBattleLog;
        Label lblAdventureLog;
        Label lblHelp;

        #endregion Fields

        #region Constructors

        public mnuOthers(string name)
            : base(name) {
            this.Size = new Size(185, 230);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblOthers = new Label("lblOthers");
            lblOthers.Location = new Point(20, 0);
            lblOthers.Font = FontManager.LoadFont("PMU", 48);
            lblOthers.AutoSize = true;
            lblOthers.ForeColor = Color.WhiteSmoke;
            lblOthers.Text = "Others";


            lblOptions = new Label("lblOptions");
            lblOptions.AutoSize = true;
            lblOptions.Location = new Point(30, 48);
            lblOptions.Font = FontManager.LoadFont("PMU", 32);
            lblOptions.Text = "Options";
            lblOptions.HoverColor = Color.Red;
            lblOptions.ForeColor = Color.WhiteSmoke;
            lblOptions.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblOptions_Click);

            lblOnlineList = new Label("lblOnlineList");
            lblOnlineList.AutoSize = true;
            lblOnlineList.Location = new Point(30, 78);
            lblOnlineList.Font = FontManager.LoadFont("PMU", 32);
            lblOnlineList.Text = "Online List";
            lblOnlineList.HoverColor = Color.Red;
            lblOnlineList.ForeColor = Color.WhiteSmoke;
            lblOnlineList.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblOnlineList_Click);

            lblBattleLog = new Label("lblBattleLog");
            lblBattleLog.AutoSize = true;
            lblBattleLog.Location = new Point(30, 108);
            lblBattleLog.Font = FontManager.LoadFont("PMU", 32);
            lblBattleLog.Text = "Battle Log";
            lblBattleLog.HoverColor = Color.Red;
            lblBattleLog.ForeColor = Color.WhiteSmoke;
            lblBattleLog.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblBattleLog_Click);

            lblAdventureLog = new Label("lblAdventureLog");
            lblAdventureLog.AutoSize = true;
            lblAdventureLog.Location = new Point(30, 138);
            lblAdventureLog.Font = FontManager.LoadFont("PMU", 32);
            lblAdventureLog.Text = "Profile";
            lblAdventureLog.HoverColor = Color.Red;
            lblAdventureLog.ForeColor = Color.WhiteSmoke;
            lblAdventureLog.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblAdventureLog_Click);

            lblHelp = new Label("lblHelp");
            lblHelp.AutoSize = true;
            lblHelp.Location = new Point(30, 168);
            lblHelp.Font = FontManager.LoadFont("PMU", 32);
            lblHelp.Text = "Help";
            lblHelp.HoverColor = Color.Red;
            lblHelp.ForeColor = Color.WhiteSmoke;
            lblHelp.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblHelp_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblOthers);
            this.AddWidget(lblOptions);
            this.AddWidget(lblOnlineList);
            this.AddWidget(lblBattleLog);
            this.AddWidget(lblAdventureLog);
            this.AddWidget(lblHelp);
        }



        void lblOptions_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0);
        }

        void lblOnlineList_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1);
        }

        void lblBattleLog_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(2);
        }

        void lblAdventureLog_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(3);
        }

        void lblHelp_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(4);
        }

        #endregion Constructors

        #region Methods

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 63 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == MAX_ITEMS) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
            			Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(MAX_ITEMS);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                    	Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        // Show the main menu when the backspace key is pressed
                        MenuSwitcher.ShowMainMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            switch (itemNum) {
                case 0: {
                        Windows.WindowSwitcher.GameWindow.MenuManager.CloseOpenMenus();
                        Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuOptions("mnuOptions"));
                        Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuOptions");
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 1: {
                        MenuSwitcher.ShowMenu(new Menus.mnuOnlineList("mnuOnlineList"));
                        Network.Messenger.SendOnlineListRequest();
                    }
                    break;
                case 2: {
                        Menus.MenuSwitcher.ShowMenu(new Menus.mnuBattleLog("mnuBattleLog"));
                    }
                    break;
                case 3: {
                        MenuSwitcher.ShowMenu(new Menus.mnuAdventureLog("mnuAdventureLog"));
                        Network.Messenger.SendAdventureLogRequest();
                    }
                    break;
                case 4: { // Help menu
                    MenuSwitcher.ShowHelpMenu();
                    Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
