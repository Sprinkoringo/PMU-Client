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
    class winAccountSettings : Core.WindowCore
    {
        Button btnChangePassword;
        Label lblBack;

        public winAccountSettings()
            : base("winAccountSettings") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Account Settings";
            this.TitleBar.CloseButton.Visible = false;
            this.Size = new Size(260, 200);
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Account Settings");
            this.Location = new Point(DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size).X, 5);

            btnChangePassword = new Button("btnChangePassword");
            btnChangePassword.Font = Graphics.FontManager.LoadFont("PMU", 24);
            btnChangePassword.Text = "Change Password";
            btnChangePassword.Location = new Point(65, 55);
            btnChangePassword.Size = new System.Drawing.Size(130, 32);
            btnChangePassword.Click += new EventHandler<MouseButtonEventArgs>(btnChangePassword_Click);

            lblBack = new Label("lblBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblBack.Text = "Return to Login Screen";
            lblBack.Location = new Point(45, 100);
            lblBack.AutoSize = true;
            lblBack.ForeColor = Color.Black;
            lblBack.Click +=new EventHandler<MouseButtonEventArgs>(lblBack_Click);

            this.AddWidget(btnChangePassword);
            this.AddWidget(lblBack);

            this.LoadComplete();
       }

        void btnChangePassword_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowManager.AddWindow(new winChangePassword());
       }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            WindowSwitcher.ShowMainMenu();
       }
    }
}
