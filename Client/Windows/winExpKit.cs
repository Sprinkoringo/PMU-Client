using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winExpKit : Core.WindowCore
    {
        ExpKit.KitContainer kitContainer;

        public ExpKit.KitContainer KitContainer {
            get { return kitContainer; }
        }

        public winExpKit()
            : base("winExpKit") {

            //this.Location = Graphics.DrawingSupport.GetCenter(this.Size);
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 450);
            this.Location = new System.Drawing.Point(5, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 5);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = false;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Explorer Kit";
            this.TitleBar.FillColor = System.Drawing.Color.Transparent;
            this.TitleBar.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.TitleBar.BackgroundImage = Skins.SkinManager.LoadGuiElement("Game Window", "Widgets/expkitTitleBar.png");

            kitContainer = new ExpKit.KitContainer("kitContainer");
            kitContainer.Location = new System.Drawing.Point(0, 0);
            kitContainer.Size = this.Size;

            this.AddWidget(kitContainer);

            this.LoadComplete();
        }

    }
}
