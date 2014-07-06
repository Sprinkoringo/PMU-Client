using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuMoves : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }
        Label[] lblAllMoves;
        Label[] lblAllMovesPP;
        Label lblMoves;



        Widgets.MenuItemPicker itemPicker;
        const int MAX_ITEMS = 3;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuMoves(string name)
            : base(name) {
            base.Size = new Size(280, 188);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 63);

            lblMoves = new Label("lblMoves");
            lblMoves.Location = new Point(20, 0);
            lblMoves.AutoSize = true;
            lblMoves.Font = FontManager.LoadFont("PMU", 48);
            lblMoves.ForeColor = Color.WhiteSmoke;
            lblMoves.Text = "Moves";

            lblAllMoves = new Label[Players.PlayerManager.MyPlayer.Moves.Length];
            lblAllMovesPP = new Label[Players.PlayerManager.MyPlayer.Moves.Length];
            for (int i = 0; i < Players.PlayerManager.MyPlayer.Moves.Length; i++) {
                lblAllMoves[i] = new Label("lblAllMoves" + i);
                lblAllMoves[i].AutoSize = true;
                lblAllMoves[i].Font = FontManager.LoadFont("PMU", 32);
                lblAllMoves[i].Location = new Point(30, (i * 30) + 48);
                //lblAllMoves[i].HoverColor = Color.Red;
                lblAllMoves[i].AllowDrop = true;
                lblAllMoves[i].MouseDown += new EventHandler<MouseButtonEventArgs>(lblAllMoves_MouseDown);
                lblAllMoves[i].DragDrop += new EventHandler<DragEventArgs>(lblAllMoves_DragDrop);
                lblAllMoves[i].Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(move_Click);
                this.AddWidget(lblAllMoves[i]);

                lblAllMovesPP[i] = new Label("lblAllMovesPP" + i);
                lblAllMovesPP[i].AutoSize = true;
                lblAllMovesPP[i].Centered = true;
                lblAllMovesPP[i].Font = FontManager.LoadFont("PMU", 32);
                lblAllMovesPP[i].Location = new Point(190, (i * 30) + 48);
                this.AddWidget(lblAllMovesPP[i]);


            }

            this.AddWidget(itemPicker);
            this.AddWidget(lblMoves);

            DisplayMoves();

            ChangeSelected(0);
        }

        void lblAllMoves_DragDrop(object sender, DragEventArgs e) {
            int oldSlot = Convert.ToInt32(e.Data.GetData(typeof(int)));
            Network.Messenger.SendSwapMoves(oldSlot, Array.IndexOf(lblAllMoves, sender));
        }

        void lblAllMoves_MouseDown(object sender, MouseButtonEventArgs e) {
            if (Windows.WindowSwitcher.GameWindow.MenuManager.FindMenu("mnuMoveSelected") == null) {
                Label label = (Label)sender;
                SdlDotNet.Graphics.Surface dragSurf = new SdlDotNet.Graphics.Surface(label.Buffer.Size);
                dragSurf.Fill(Color.Black);
                dragSurf.Blit(label.Buffer, new Point(0, 0));
                label.DoDragDrop(Array.IndexOf(lblAllMoves, sender), DragDropEffects.Copy, dragSurf);
            }
        }

        void move_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //SelectMove(Array.IndexOf(lblAllMoves, sender));
        }

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
                        SelectMove(itemPicker.SelectedItem);
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

        private void SelectMove(int moveSlot) {
            Windows.WindowSwitcher.GameWindow.MenuManager.AddMenu(new Menus.mnuMoveSelected("mnuMoveSelected", moveSlot));
            Windows.WindowSwitcher.GameWindow.MenuManager.SetActiveMenu("mnuMoveSelected");
            Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
        }

        public void DisplayMoves() {
            for (int i = 0; i < lblAllMoves.Length; i++) {
                if (Players.PlayerManager.MyPlayer.Moves[i].MoveNum > 0) {
                    lblAllMoves[i].Text = Moves.MoveHelper.Moves[Players.PlayerManager.MyPlayer.Moves[i].MoveNum].Name;
                    lblAllMovesPP[i].Text = Players.PlayerManager.MyPlayer.Moves[i].CurrentPP + "/" + Players.PlayerManager.MyPlayer.Moves[i].MaxPP;
                    if (Players.PlayerManager.MyPlayer.Moves[i].CurrentPP > 0 && !Players.PlayerManager.MyPlayer.Moves[i].Sealed)
                    {
                        lblAllMoves[i].ForeColor = Color.WhiteSmoke;
                        lblAllMovesPP[i].ForeColor = Color.WhiteSmoke;
                    }
                    else
                    {
                        lblAllMoves[i].ForeColor = Color.Red;
                        lblAllMovesPP[i].ForeColor = Color.Red;
                    }
                } else {
                    lblAllMoves[i].Text = "-----";
                    lblAllMovesPP[i].Text = "--/--";
                    lblAllMoves[i].ForeColor = Color.Gray;
                    lblAllMovesPP[i].ForeColor = Color.Gray;
                }
            }
        }

    }
}
