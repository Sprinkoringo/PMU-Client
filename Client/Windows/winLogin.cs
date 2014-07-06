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
using Client.Logic.Skins;
using Client.Logic.Network;

namespace Client.Logic.Windows
{
    class winLogin : Core.WindowCore
    {
        Label lblServerStatus;
        Timer tmrServerChecker;
        Button btnLogin;
        Label lblUsername;
        Label lblPassword;
        Button btnClear;
        TextBox txtUsername;
        TextBox txtPassword;
        CheckBox chkSavePassword;

        public winLogin()
            : base("winLogin") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new Size(500, 150);
            this.Location = new Point(DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size).X, 5);
            //this.TitleBar.Text = "Login";
            this.TitleBar.CloseButton.Visible = false;
            this.TitleBar.BackgroundImage = SkinManager.LoadGuiElement("Login Menu", "titlebar.png");
            this.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.BackgroundImage = SkinManager.LoadGui("Login Menu");

            btnLogin = new Button("btnLogin");
            btnLogin.Location = new Point(360, 27);
            btnLogin.Size = new System.Drawing.Size(134, 32);
            btnLogin.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnLogin.BackColor = Color.Transparent;
            btnLogin.BackgroundImage = SkinManager.LoadGuiElement("Login Menu", "buttons/login.png");
            btnLogin.HighlightType = HighlightType.Image;
            btnLogin.HighlightSurface = SkinManager.LoadGuiElement("Login Menu", "buttons/login-h.png");
            btnLogin.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLogin_Click);

            lblUsername = new Label("lblUsername");
            lblUsername.Font = Graphics.FontManager.LoadFont("Tahoma", 16);
            lblUsername.Text = "Username";
            lblUsername.AutoSize = true;
            lblUsername.Location = new Point(30, 30);

            lblPassword = new Label("lblPassword");
            lblPassword.Font = Graphics.FontManager.LoadFont("Tahoma", 16);
            lblPassword.Text = "Password";
            lblPassword.AutoSize = true;
            lblPassword.Location = new Point(30, 60);

            txtUsername = new TextBox("txtUsername");
            txtUsername.Size = new System.Drawing.Size(180, 16);
            txtUsername.Location = new Point(130, 30);
            txtUsername.Text = IO.Options.SavedAccount;

            txtPassword = new TextBox("txtPassword");
            txtPassword.Location = new Point(130, 60);
            txtPassword.Size = new System.Drawing.Size(180, 16);
            txtPassword.Text = IO.Options.SavedPassword;
            txtPassword.PasswordChar = '*';

            chkSavePassword = new CheckBox("chkSavePassword");
            chkSavePassword.Text = "Save Password?";
            chkSavePassword.Size = new System.Drawing.Size(200, 16);
            chkSavePassword.Location = new Point(130, 80);
            chkSavePassword.BackColor = Color.Transparent;
            if (!string.IsNullOrEmpty(IO.Options.SavedPassword)) {
                chkSavePassword.Checked = true;
            }

            btnClear = new Button("btnClear");
            btnClear.Location = new Point(360, 59);
            btnClear.Size = new System.Drawing.Size(134, 32);
            btnClear.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;
            btnClear.BackColor = Color.Transparent;
            btnClear.BackgroundImage = SkinManager.LoadGuiElement("Login Menu", "buttons/clear.png");
            btnClear.HighlightType = HighlightType.Image;
            btnClear.HighlightSurface = SkinManager.LoadGuiElement("Login Menu", "buttons/clear-h.png");
            btnClear.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnClear_Click);

            lblServerStatus = new Label("lblServerStatus");
            lblServerStatus.AntiAlias = true;
            lblServerStatus.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblServerStatus.ForeColor = Color.Turquoise;
            lblServerStatus.AutoSize = true;
            lblServerStatus.Text = "Checking...";
            lblServerStatus.Location = new Point(90, 107);

            tmrServerChecker = new Timer("tmrServerChecker");
            tmrServerChecker.Interval = 500;
            tmrServerChecker.Name = "serverChecker";
            tmrServerChecker.Elapsed += new EventHandler(tmrServerChecker_Elapsed);

            this.AddWidget(lblUsername);
            this.AddWidget(lblPassword);
            this.AddWidget(btnLogin);
            this.AddWidget(txtPassword);
            this.AddWidget(txtUsername);
            this.AddWidget(chkSavePassword);
            this.AddWidget(btnClear);

            this.AddWidget(lblServerStatus);
            this.AddWidget(tmrServerChecker);

            tmrServerChecker.Start();
        }

        void tmrServerChecker_Elapsed(object sender, EventArgs e) {
            if (NetworkManager.TcpClient.Socket.Connected) {
                if (lblServerStatus.Text != "Online") {
                    lblServerStatus.Text = "Online";
                }
            } else {
                if (NetworkManager.ShouldAttemptReconnect()) {
                    NetworkManager.Connect();
                }
                if (lblServerStatus.Text != "Offline") {
                    lblServerStatus.Text = "Offline";
                }
            }
        }

        void btnLogin_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (NetworkManager.TcpClient.Socket.Connected) {
                lblServerStatus.Text = "Online";
                string account = txtUsername.Text; 
                string password = txtPassword.Text;
                if (chkSavePassword.Checked) {
                    IO.Options.SavedAccount = account;
                    IO.Options.SavedPassword = password;

                    IO.Options.SaveXml();
                }
                this.Close();
                WindowManager.FindWindow("winUpdates").Close();
                WindowManager.FindWindow("winMainMenu").Close();
                WindowManager.AddWindow(new winLoading());
                ((Windows.winLoading)WindowManager.FindWindow("winLoading")).UpdateLoadText("Sending login...");
                Messenger.SendLogin(account, password);
            }
        }

        void btnClear_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }
    }
}
