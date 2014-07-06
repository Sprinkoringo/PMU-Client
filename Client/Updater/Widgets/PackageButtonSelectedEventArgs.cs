using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Updater.Widgets
{
    class PackageButtonSelectedEventArgs:EventArgs
    {
        PackageButton packageButton;

        public PackageButtonSelectedEventArgs(PackageButton packageButton) {
            this.packageButton = packageButton;
        }

        public PackageButton PackageButton {
            get { return packageButton; }
        }
    }
}
