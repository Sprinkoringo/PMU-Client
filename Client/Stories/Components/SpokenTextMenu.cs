using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;

namespace Client.Logic.Stories.Components
{
    class SpokenTextMenu : Widgets.BorderedPanel, Menus.Core.IMenu
    {
        Label lblText;
        PictureBox picSpeaker;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }

        public SpokenTextMenu(string name, Size storyBounds)
            : base(name) {

            this.Size = new System.Drawing.Size(storyBounds.Width - 10, 100);
            this.Location = new Point(5, storyBounds.Height - this.Height);

            lblText = new Label("lblText");
            lblText.BackColor = Color.Transparent;
            lblText.ForeColor = Color.WhiteSmoke;
            lblText.AutoSize = false;
            lblText.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblText.Location = new Point(15, 10);
            lblText.Size = new System.Drawing.Size(this.WidgetPanel.Width - lblText.Location.X, this.WidgetPanel.Height - lblText.Location.Y);

            picSpeaker = new PictureBox("picSpeaker");
            picSpeaker.Size = new Size(40, 40);
            picSpeaker.Location = new Point(10, DrawingSupport.GetCenter(WidgetPanel.Height, 40));
            picSpeaker.BorderWidth = 1;
            picSpeaker.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

            this.WidgetPanel.AddWidget(lblText);
            this.WidgetPanel.AddWidget(picSpeaker);
        }

        public void DisplayText(string text) {
            DisplayText(text, -1);
        }

        public void DisplayText(string text, int mugshot) {
            bool displayMugshot = false;
            if (mugshot > -1) {
                Logic.Graphics.Mugshot mugshotImg = Logic.Graphics.GraphicsManager.GetMugshot(mugshot, 0, 0, 0);
                if (mugshotImg != null) {
                    displayMugshot = true;
                }
            }
            if (displayMugshot) {
                picSpeaker.Image = Logic.Graphics.GraphicsManager.GetMugshot(mugshot, 0, 0, 0).GetEmote(0);//Tools.CropImage(Logic.Graphics.GraphicsManager.Speakers, new Rectangle((mugshot % 15) * 40, (mugshot / 15) * 40, 40, 40));
                lblText.Location = new Point(picSpeaker.X + picSpeaker.Width + 10, 10);
                lblText.Size = new System.Drawing.Size(this.WidgetPanel.Width - lblText.Location.X, this.WidgetPanel.Height - lblText.Location.Y);
                picSpeaker.Show();
            } else {
                picSpeaker.Hide();
                lblText.Location = new Point(15, 10);
                lblText.Size = new System.Drawing.Size(this.WidgetPanel.Width - lblText.Location.X, this.WidgetPanel.Height - lblText.Location.Y);
            }
            CharRenderOptions[] renderOptions = new CharRenderOptions[text.Length];
            for (int i = 0; i < renderOptions.Length; i++) {
                renderOptions[i] = new CharRenderOptions(Color.WhiteSmoke);
            }
            renderOptions = Network.MessageProcessor.ParseText(renderOptions, ref text);
            lblText.SetText(text, renderOptions);
        }

        public override void Close() {
            base.Close();
            if (this.ParentContainer != null) {
                this.ParentContainer.RemoveWidget(this.GroupedWidget.Name);
            }
        }
    }
}
