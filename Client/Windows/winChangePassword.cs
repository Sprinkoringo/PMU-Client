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

namespace Client.Logic.Windows
{
    class winChangePassword : Core.WindowCore
    {
        Label lblBack;
        Label lblName;
        Label lblPassword;
        Label lblNewPassword;
        Label lblRetypePassword;
        Label lblChangePassword;
        TextBox txtName;
        TextBox txtPassword;
        TextBox txtNewPassword;
        TextBox txtRetypePassword;

        public winChangePassword()
            : base("winChangePassword") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Change Password";
            this.TitleBar.CloseButton.Visible = false;
            this.Size = new Size(280, 360);
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Change Password");
            this.Location = new Point(DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size).X, 5);

            lblBack = new Label("lblBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblBack.Text = "Back to Account Settings";
            lblBack.Location = new Point(45, 300);
            lblBack.AutoSize = true;
            lblBack.ForeColor = Color.Black;
            lblBack.Click += new EventHandler<MouseButtonEventArgs>(lblBack_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblName.Location = new Point(60, 57);
            lblName.AutoSize = true;
            lblName.ForeColor = Color.Black;
            lblName.Text = "Enter your Account Name";

            lblPassword = new Label("lblPassword");
            lblPassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblPassword.Location = new Point(60, 103);
            lblPassword.AutoSize = true;
            lblPassword.ForeColor = Color.Black;
            lblPassword.Text = "Enter your current Password";

            lblNewPassword = new Label("lblNewPassword");
            lblNewPassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblNewPassword.Location = new Point(60, 149);
            lblNewPassword.AutoSize = true;
            lblNewPassword.ForeColor = Color.Black;
            lblNewPassword.Text = "Enter your new Password";

            lblRetypePassword = new Label("lblRetypePassword");
            lblRetypePassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblRetypePassword.Location = new Point(60, 195);
            lblRetypePassword.AutoSize = true;
            lblRetypePassword.ForeColor = Color.Black;
            lblRetypePassword.Text = "Reenter your new Password";

            lblChangePassword = new Label("lblChangePassword");
            lblChangePassword.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblChangePassword.Location = new Point(70, 241);
            lblChangePassword.AutoSize = true;
            lblChangePassword.ForeColor = Color.Black;
            lblChangePassword.Text = "Change your Password!";
            lblChangePassword.Click += new EventHandler<MouseButtonEventArgs>(lblChangePassword_Click);

            txtName = new TextBox("txtName");
            txtName.Size = new System.Drawing.Size(165, 16);
            txtName.Location = new Point(60, 78);

            txtPassword = new TextBox("txtPassword");
            txtPassword.Size = new System.Drawing.Size(165, 16);
            txtPassword.Location = new Point(60, 124);

            txtNewPassword = new TextBox("txtNewPassword");
            txtNewPassword.Size = new System.Drawing.Size(165, 16);
            txtNewPassword.Location = new Point(60, 170);

            txtRetypePassword = new TextBox("txtRetypePassword");
            txtRetypePassword.Size = new System.Drawing.Size(165, 16);
            txtRetypePassword.Location = new Point(60, 216);

            this.AddWidget(lblBack);
            this.AddWidget(lblName);
            this.AddWidget(lblPassword);
            this.AddWidget(lblNewPassword);
            this.AddWidget(lblRetypePassword);
            this.AddWidget(lblChangePassword);
            this.AddWidget(txtName);
            this.AddWidget(txtPassword);
            this.AddWidget(txtNewPassword);
            this.AddWidget(txtRetypePassword);

            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowAccountSettings();
        }

        void lblChangePassword_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!string.IsNullOrEmpty(txtName.Text) && !string.IsNullOrEmpty(txtNewPassword.Text) && !string.IsNullOrEmpty(txtRetypePassword.Text)) {
                if (txtNewPassword.Text == txtRetypePassword.Text) {
                    Network.Messenger.SendPasswordChange(txtName.Text, txtPassword.Text, txtNewPassword.Text);
                }
            }
        }
    }
}
