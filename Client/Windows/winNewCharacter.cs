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

using System.Drawing;
using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Windows
{
    class winNewCharacter : Core.WindowCore
    {
        Label lblName;
        TextBox txtName;
        Label lblCreateChar;
        Label lblBack;
        RadioButton optMale;
        RadioButton optFemale;
        int charSlot;

        public winNewCharacter(int charSlot)
            : base("winNewCharacter") {
            this.charSlot = charSlot;

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "New Character";
            this.TitleBar.CloseButton.Visible = false;
            //this.BackgroundImage = Skins.SkinManager.LoadGui("New Character"); - We should have a better GUI for this.
            //this.Size = this.BackgroundImage.Size;
            this.Size = new Size(400, 200);
            this.Location = new Point(DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size).X, 5);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblName.Location = new Point(40, 40);
            lblName.AutoSize = true;
            lblName.Text = "Name:";

            txtName = new TextBox("txtName");
            txtName.Size = new System.Drawing.Size(177, 16);
            txtName.Location = new Point(40, 70);

            lblCreateChar = new Label("lblCreateChar");
            lblCreateChar.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblCreateChar.Location = new Point(40, 130);
            lblCreateChar.AutoSize = true;
            lblCreateChar.Text = "Create Character";
            lblCreateChar.Click +=new EventHandler<MouseButtonEventArgs>(lblCreateChar_Click);

            lblBack = new Label("btnBack");
            lblBack.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblBack.Location = new Point(147, 130);
            lblBack.AutoSize = true;
            lblBack.Text = "Back to Login Screen";
            lblBack.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblBack_Click);

            optMale = new RadioButton("optMale");
            optMale.BackColor = Color.Transparent;
            optMale.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optMale.Location = new Point(240, 50);
            optMale.Size = new System.Drawing.Size(95, 17);
            optMale.Text = "Male";
            optMale.Checked = true;

            optFemale = new RadioButton("optFemale");
            optFemale.BackColor = Color.Transparent;
            optFemale.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optFemale.Location = new Point(240, 70);
            optFemale.Size = new System.Drawing.Size(95, 17);
            optFemale.Text = "Female";
            optFemale.Checked = false;

            this.AddWidget(lblName);
            this.AddWidget(txtName);
            this.AddWidget(lblCreateChar);
            this.AddWidget(lblBack);
            this.AddWidget(optMale);
            this.AddWidget(optFemale);
            this.LoadComplete();
        }

        void lblBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            Messenger.SendCharListRequest();
        }

        //This is made not to decide rather the player is male or female, but only for sending the new name.
        void lblCreateChar_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string name = txtName.Text;
            Enums.Sex sex = Enums.Sex.Male;
            if (optFemale.Checked) sex = Enums.Sex.Female;
            winLoading loading = new winLoading();
            loading.Show();
            loading.UpdateLoadText("Sending Character...");
            Messenger.SendNewChar(name, sex, charSlot);
            this.Close();
        }
    }
}
