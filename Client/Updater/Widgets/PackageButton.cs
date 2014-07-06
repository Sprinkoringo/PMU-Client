using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet.Widgets;
using PMU.Updater.Linker;
using System.Drawing;
using SdlDotNet.Graphics;

namespace Client.Logic.Updater.Widgets
{
    class PackageButton : Button
    {
        public const int BUTTON_HEIGHT = 50;
        IPackageInfo attachedPackage;
        Surface installedSurf;
        bool installed;

        public IPackageInfo AttachedPackage {
            get { return attachedPackage; }
            set { attachedPackage = value; }
        }

        public PackageButton(string name, IPackageInfo attachedPackage)
            : base(name) {
            this.attachedPackage = attachedPackage;
            this.Size = new Size(200, 50);
            base.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            base.BackColor = Color.SteelBlue;
            base.HighlightType = SdlDotNet.Widgets.HighlightType.Color;
            base.HighlightColor = Color.LightSteelBlue;

            installedSurf = Graphics.SurfaceManager.LoadSurface(IO.Paths.GfxPath + "Updater\\package-installed.png");

            base.Text = attachedPackage.FullID;

            base.Paint += new EventHandler(PackageButton_Paint);
        }

        public bool Installed {
            get { return installed; }
            set { 
                installed = value;
                RequestRedraw();
            }
        }

        void PackageButton_Paint(object sender, EventArgs e) {
            if (installed) {
                if (installedSurf != null) {
                    base.Buffer.Blit(installedSurf, new Point(5, (this.Height / 2) - (installedSurf.Height / 2)));
                }
            }
        }
    }
}
