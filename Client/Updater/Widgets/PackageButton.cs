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
