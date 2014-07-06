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
