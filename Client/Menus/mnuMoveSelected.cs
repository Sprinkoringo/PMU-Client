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
    class mnuMoveSelected : Widgets.BorderedPanel, Core.IMenu
    {
        int moveSlot;
        Label lblUse;
        Label lblShiftUp;
        Label lblShiftDown;
        Label lblForget;
        Widgets.MenuItemPicker itemPicker;
        const int MAX_ITEMS = 3;

        public int MoveSlot {
            get { return moveSlot; }
            set {
                moveSlot = value;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuMoveSelected(string name, int moveSlot)
            : base(name) {

            base.Size = new Size(165, 155);
            base.MenuDirection = Enums.MenuDirection.Horizontal;
            base.Location = new Point(300, 34);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 23);

            lblUse = new Label("lblUse");
            lblUse.Font = FontManager.LoadFont("PMU", 32);
            lblUse.AutoSize = true;
            lblUse.Text = "Use";
            lblUse.Location = new Point(30, 8);
            lblUse.HoverColor = Color.Red;
            lblUse.ForeColor = Color.WhiteSmoke;
            lblUse.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblUse_Click);

            lblShiftUp = new Label("lblShiftUp");
            lblShiftUp.Font = FontManager.LoadFont("PMU", 32);
            lblShiftUp.AutoSize = true;
            lblShiftUp.Text = "Shift Up";
            lblShiftUp.Location = new Point(30, 38);
            lblShiftUp.HoverColor = Color.Red;
            lblShiftUp.ForeColor = Color.WhiteSmoke;
            lblShiftUp.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblShiftUp_Click);

            lblShiftDown = new Label("lblShiftDown");
            lblShiftDown.Font = FontManager.LoadFont("PMU", 32);
            lblShiftDown.AutoSize = true;
            lblShiftDown.Text = "Shift Down";
            lblShiftDown.Location = new Point(30, 68);
            lblShiftDown.HoverColor = Color.Red;
            lblShiftDown.ForeColor = Color.WhiteSmoke;
            lblShiftDown.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblShiftDown_Click);

            lblForget = new Label("lblForget");
            lblForget.Font = FontManager.LoadFont("PMU", 32);
            lblForget.AutoSize = true;
            lblForget.Text = "Forget";
            lblForget.Location = new Point(30, 98);
            lblForget.HoverColor = Color.Red;
            lblForget.ForeColor = Color.WhiteSmoke;
            lblForget.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblForget_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblUse);
            this.AddWidget(lblShiftUp);
            this.AddWidget(lblShiftDown);
            this.AddWidget(lblForget);

            this.MoveSlot = moveSlot;

        }



        void lblUse_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(0, moveSlot);
        }

        void lblShiftUp_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(1, moveSlot);
        }

        void lblShiftDown_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(2, moveSlot);
        }

        void lblForget_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(3, moveSlot);
        }

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 23 + (30 * itemNum));
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
                        SelectItem(itemPicker.SelectedItem, moveSlot);
                        
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum, int moveSlot) {
            switch (itemNum) {
                case 0: { // Use move
                        Players.PlayerManager.MyPlayer.UseMove(moveSlot);
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case 1: { // Shift Up
                        if (moveSlot > 0) {
                            Players.PlayerManager.MyPlayer.ShiftMove(moveSlot, true);
                            CloseMenu();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep6.wav");
                        }
                    }
                    break;
                case 2: { // Shift Down
                        if (moveSlot < 3) {
                            Players.PlayerManager.MyPlayer.ShiftMove(moveSlot, false);
                            CloseMenu();
                            Music.Music.AudioPlayer.PlaySoundEffect("beep6.wav");
                        }
                    }
                    break;
                case 3: { // Forget move
                        Players.PlayerManager.MyPlayer.ForgetMove(moveSlot);
                        CloseMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
            }
        }

        private void CloseMenu() {
            Windows.WindowSwitcher.GameWindow.MenuManager.RemoveMenu(this);
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMoves");
            
        }



        public bool Modal {
            get;
            set;
        }
    }
}
