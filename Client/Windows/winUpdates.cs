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
using System.Text.RegularExpressions;
using Client.Logic.Skins;
using Client.Logic.Network;

namespace Client.Logic.Windows
{
    class winUpdates : Core.WindowCore
    {
        Label lblTitle;
        Label lblUpdates;
        Timer tmrUpdateNews;
        bool newsUpdated;

        public winUpdates()
            : base("winUpdates") {

            this.Windowed = true;
            //this.TitleBar.Text = "News and Updates";
            this.TitleBar.CloseButton.Visible = false;
            //this.TitleBar.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.TitleBar.BackgroundImage = SkinManager.LoadGuiElement("News", "titlebar.png");
            this.Size = new Size(390, 300);
            this.Location = new Point(5, 160);
            this.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.BackgroundImage = SkinManager.LoadGui("News");

            lblTitle = new Label("lblTitle");
            lblTitle.AutoSize = true;
            lblTitle.Font = Graphics.FontManager.LoadFont("tahoma", 16);
            lblTitle.Location = new Point(5, 5);
            lblTitle.Text = "Latest News";

            lblUpdates = new Label("lblUpdates");
            lblUpdates.AutoSize = false;
            lblUpdates.Font = Graphics.FontManager.LoadFont("tahoma", 14);
            lblUpdates.Location = new Point(5, 25);
            lblUpdates.Size = new System.Drawing.Size(this.Width - (this.Location.X * 2), 200);
            lblUpdates.AutoScroll = true;
            lblUpdates.Text = "Receiving latest news from server...";

            tmrUpdateNews = new Timer("tmrUpdateNews");
            tmrUpdateNews.Interval = 500;
            tmrUpdateNews.Elapsed += new EventHandler(tmrUpdateNews_Elapsed);
            tmrUpdateNews.Start();

            this.AddWidget(lblTitle);
            this.AddWidget(lblUpdates);
            this.AddWidget(tmrUpdateNews);
        }

        void tmrUpdateNews_Elapsed(object sender, EventArgs e) {
            if (!newsUpdated) {
                Messenger.SendRequestNews();
            } else {
                tmrUpdateNews.Stop();
            }
        }

        public void DisplayNews(string news) {
            lblUpdates.Text = news;
            //CharRenderOptions options = new CharRenderOptions(lblUpdates.ForeColor);
            //options.Bold = true;
            //lblUpdates.SetRenderOption(options, lblUpdates.Text.IndexOf("Version:"), 8);
            //lblUpdates.SetRenderOption(options, lblUpdates.Text.IndexOf("On For:"), 7);

            newsUpdated = true;
        }

        public void ClearNews() {
            lblUpdates.Text = "Receiving latest news from server...";
            newsUpdated = false;
            tmrUpdateNews.Start();
        }
    }
}
