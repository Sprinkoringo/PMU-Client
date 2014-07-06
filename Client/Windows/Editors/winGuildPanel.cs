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

namespace Client.Logic.Windows.Editors
{
    class winGuildPanel : Core.WindowCore
    {
        
        Label lblPlayer;
        Label lblGuild;
        Label lblCreate;

        TextBox txtPlayer;
        TextBox txtGuild;

        public winGuildPanel()
            : base("winGuildPanel")
        {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(174, 196);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Guild Panel";

            lblPlayer = new Label("lblPlayer");
            lblPlayer.Location = new Point(20, 0);
            lblPlayer.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblPlayer.AutoSize = true;
            
            lblPlayer.Text = "Player Name:";

            txtPlayer = new TextBox("txtPlayer");
            txtPlayer.Location = new Point(20, 20);
            txtPlayer.Size = new System.Drawing.Size(120, 20);
            txtPlayer.Font = Graphics.FontManager.LoadFont("PMU", 16);

            lblGuild = new Label("lblGuild");
            lblGuild.Location = new Point(20, 60);
            lblGuild.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblGuild.AutoSize = true;
            lblGuild.Text = "Guild Name:";

            txtGuild = new TextBox("txtGuild");
            txtGuild.Location = new Point(20, 80);
            txtGuild.Size = new System.Drawing.Size(120, 20);
            txtGuild.Font = Graphics.FontManager.LoadFont("PMU", 16);

            
            lblCreate = new Label("lblCreate");
            lblCreate.Location = new Point(40, 140);
            lblCreate.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblCreate.AutoSize = true;
            lblCreate.Text = "Create";
            lblCreate.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblCreate_Click);


            this.AddWidget(lblPlayer);
            this.AddWidget(lblGuild);
            this.AddWidget(txtPlayer);
            this.AddWidget(txtGuild);
            this.AddWidget(lblCreate);

            this.LoadComplete();
        }

        void lblCreate_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            //Messenger.MakeGuild(txtPlayer.Text, txtGuild.Text);
        }

    }
}
