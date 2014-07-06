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
using System.Text;

using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Network;

namespace Client.Logic.Windows
{
    class winSelectChar : Core.WindowCore
    {
        public Button btnChar1;
        public Button btnChar2;
        public Button btnChar3;

        Label lblDeleteCharacter;
        Label lblLoginScreen;
        Label lblNewCharacter;
        Label lblUseCharacter;

        public winSelectChar()
            : base("winSelectChar") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Login";
            this.TitleBar.CloseButton.Visible = false;
            this.BackgroundImage = Skins.SkinManager.LoadGui("Character Select");
            this.Size = this.BackgroundImage.Size;
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            btnChar1 = new Button("btnChar1");
            btnChar1.Font = Graphics.FontManager.LoadFont("PMU", 32);
            btnChar1.Location = new Point(16, 104);
            btnChar1.Size = new Size(359, 41);
            btnChar1.BackColor = Color.SteelBlue;
            btnChar1.HighlightColor = Color.SkyBlue;
            btnChar1.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnChar1.Text = "Character 1";
            btnChar1.Selected = true;
            btnChar1.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnChar1_Click);

            btnChar2 = new Button("btnChar2");
            btnChar2.Font = Graphics.FontManager.LoadFont("PMU", 32);
            btnChar2.Location = new Point(16, 145);
            btnChar2.Size = new Size(359, 41);
            btnChar2.BackColor = Color.SteelBlue;
            btnChar2.HighlightColor = Color.SkyBlue;
            btnChar2.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnChar2.Text = "Character 2";
            btnChar2.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnChar2_Click);

            btnChar3 = new Button("btnChar3");
            btnChar3.Font = Graphics.FontManager.LoadFont("PMU", 32);
            btnChar3.Location = new Point(16, 186);
            btnChar3.Size = new Size(359, 41);
            btnChar3.BackColor = Color.SteelBlue;
            btnChar3.HighlightColor = Color.SkyBlue;
            btnChar3.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnChar3.Text = "Character 3";
            btnChar3.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnChar3_Click);

            lblUseCharacter = new Label("lblUseCharacter");
            lblUseCharacter.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblUseCharacter.Location = new Point(26, 225);
            lblUseCharacter.AutoSize = true;
            lblUseCharacter.ForeColor = Color.Black;
            lblUseCharacter.Text = "Use Character";
            lblUseCharacter.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblUseCharacter_Click);

            lblDeleteCharacter = new Label("lblDeleteCharacter");
            lblDeleteCharacter.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblDeleteCharacter.Location = new Point(158, 225);
            lblDeleteCharacter.AutoSize = true;
            lblDeleteCharacter.ForeColor = Color.Black;
            lblDeleteCharacter.Text = "Delete Character";
            lblDeleteCharacter.Click += new EventHandler<MouseButtonEventArgs>(lblDeleteCharacter_Click);

            lblNewCharacter = new Label("lblNewCharacter");
            lblNewCharacter.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblNewCharacter.Location = new Point(286, 225);
            lblNewCharacter.AutoSize = true;
            lblNewCharacter.ForeColor = Color.Black;
            lblNewCharacter.Text = "New Character";
            lblNewCharacter.Click += new EventHandler<MouseButtonEventArgs>(lblNewCharacter_Click);

            lblLoginScreen = new Label("lblLoginScreen");
            lblLoginScreen.Font = Graphics.FontManager.LoadFont("PMU", 16);
            lblLoginScreen.Location = new Point(140, 345);
            lblLoginScreen.AutoSize = true;
            lblLoginScreen.ForeColor = Color.Black;
            lblLoginScreen.Text = "Back to Login Screen";
            lblLoginScreen.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblLoginScreen_Click);

            this.AddWidget(btnChar1);
            this.AddWidget(btnChar2);
            this.AddWidget(btnChar3);
            this.AddWidget(lblUseCharacter);
            this.AddWidget(lblDeleteCharacter);
            this.AddWidget(lblNewCharacter);
            this.AddWidget(lblLoginScreen);
        }

        public void SetCharName(Button button, string charName) {
            if (!string.IsNullOrEmpty(charName)) {
                button.Text = charName;
            } else {
                button.Text = "[Empty Character Slot]";
            }
        }

        void btnChar3_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            btnChar3.Selected = true;
            btnChar2.Selected = false;
            btnChar1.Selected = false;
        }

        void btnChar2_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            btnChar3.Selected = false;
            btnChar2.Selected = true;
            btnChar1.Selected = false;
        }

        void btnChar1_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            btnChar3.Selected = false;
            btnChar2.Selected = false;
            btnChar1.Selected = true;
        }

        void lblLoginScreen_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            NetworkManager.Disconnect();
            NetworkManager.Connect();
            WindowSwitcher.ShowMainMenu();
        }

        void lblUseCharacter_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            bool charSelected = false;
            if (btnChar1.Selected) {
                WindowSwitcher.GameWindow = new winGame();
                Messenger.SendUseChar(1);
                charSelected = true;
            } else if (btnChar2.Selected) {
                WindowSwitcher.GameWindow = new winGame();
                Messenger.SendUseChar(2);
                charSelected = true;
            } else if (btnChar3.Selected) {
                WindowSwitcher.GameWindow = new winGame();
                Messenger.SendUseChar(3);
                charSelected = true;
            }
            if (charSelected) {
                this.Close();
                WindowManager.AddWindow(new winLoading());
                ((winLoading)WindowSwitcher.FindWindow("winLoading")).UpdateLoadText("Logging in...");
            }
        }

        void lblNewCharacter_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            int charNum = -1;
            if (btnChar1.Selected) {
                if (btnChar1.Text == "[Empty Character Slot]") {
                    charNum = 1;
                } else {
                    MessageBox.Show("There is already a character in this slot!", "Error!");
                }
            } else if (btnChar2.Selected) {
                if (btnChar2.Text == "[Empty Character Slot]") {
                    charNum = 2;
                } else {
                    MessageBox.Show("There is already a character in this slot!", "Error!");
                }
            } else if (btnChar3.Selected) {
                if (btnChar3.Text == "[Empty Character Slot]") {
                    charNum = 3;
                } else {
                    MessageBox.Show("There is already a character in this slot!", "Error!");
                }
            }
            if (charNum != -1) {
                WindowManager.AddWindow(new winNewCharacter(charNum));
                this.Close();
            }
        }

        void lblDeleteCharacter_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //SdlDotNet.Widgets.MessageBox.Show("Delete?", "Are you sure you wish to delete this character?");
            if (MessageBox.Show("Are you sure you wish to delete this character?", "Delete Character?", MessageBoxButtons.YesNo) == SdlDotNet.Widgets.DialogResult.Yes) {
                int charNum = -1;
                if (btnChar1.Selected) {
                    charNum = 1;
                } else if (btnChar2.Selected) {
                    charNum = 2;
                } else if (btnChar3.Selected) {
                    charNum = 3;
                }
                switch (charNum) {
                    case 1: {
                            if (btnChar1.Text == "[Empty Character Slot]") {
                                charNum = -1;
                                MessageBox.Show("There is no character in this slot!", "Character Slot Empty!");
                            }
                        }
                        break;
                    case 2: {
                            if (btnChar2.Text == "[Empty Character Slot]") {
                                charNum = -1;
                                MessageBox.Show("There is no character in this slot!", "Character Slot Empty!");
                            }
                        }
                        break;
                    case 3: {
                            if (btnChar3.Text == "[Empty Character Slot]") {
                                charNum = -1;
                                MessageBox.Show("There is no character in this slot!", "Character Slot Empty!");
                            }
                        }
                        break;
                }
                if (charNum != -1) {
                    this.Close();
                    WindowManager.AddWindow(new winLoading());
                    ((Windows.winLoading)WindowManager.FindWindow("winLoading")).UpdateLoadText("Deleting Character...");
                    Messenger.SendDeleteChar(charNum);
                }
            }
            // SdlDotNet.Widgets.MessageBox.Show("Error", "There is no Character in this slot!");
        }
    }
}
