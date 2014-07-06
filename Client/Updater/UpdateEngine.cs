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

using PMU.Updater;
using PMU.Updater.Linker;
using PMU.Net;

namespace Client.Logic.Updater
{
    class UpdateEngine
    {
        UpdateClient updater;
        IUpdateCheckResult lastCheckResult;

        public readonly string UpdateURL = IO.Options.UpdateAddress;

        public UpdateClient Updater {
            get { return updater; }
        }

        public IUpdateCheckResult LastCheckResult {
            get { return lastCheckResult; }
        }

        public UpdateEngine(string packageListKey) {
            updater = new UpdateClient(packageListKey, System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetExecutingAssembly().Location) + "\\");//System.Windows.Forms.Application.StartupPath + "\\");
        }

        public bool CheckForUpdates() {
            //if (NetTools.IsConnected()) {
            // Load the installed package list
            updater.LoadInstalledPackageList();
            updater.InstallationComplete += new EventHandler(updater_InstallationComplete);
            lastCheckResult = updater.CheckForUpdates(UpdateURL);
            return (lastCheckResult.PackagesToUpdate.Count != 0);
            //} else {
            //    return false;
            //}
        }

        public void PerformUpdate(IUpdateCheckResult result) {
            if (result.PackagesToUpdate.Count != 0) {
                updater.PerformUpdate(result);
            }
        }

        //public void PerformUpdate() {
        //    // Load the installed package list
        //    updater.LoadInstalledPackageList();
        //    updater.InstallationComplete += new EventHandler(updater_InstallationComplete);
        //    updater.PackageDownloadStart += new EventHandler<PackageDownloadStartEventArgs>(updater_PackageDownloadStart);
        //    IUpdateCheckResult result = updater.CheckForUpdates("http://www.pmuniverse.net/PMU/Updates/Map%20Editor%203.1/UpdateData.xml");
        //    if (result.PackagesToUpdate.Count != 0) {
        //        updater.PerformUpdate(result);
        //    } else {
        //        System.Windows.Forms.MessageBox.Show("No updates found!");
        //        Environment.Exit(0);
        //    }
        //}

        void updater_InstallationComplete(object sender, EventArgs e) {
            //System.Windows.Forms.MessageBox.Show("Update complete!");
            //Environment.Exit(0);
        }
    }
}
