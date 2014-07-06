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
