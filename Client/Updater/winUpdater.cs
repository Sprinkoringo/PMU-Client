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
using Client.Logic.Updater.Widgets;
using PMU.Updater.Linker;
using System.Threading;

namespace Client.Logic.Updater
{
    class winUpdater : Window
    {
        Label lblUpdateFound;
        PackageScroller packageScroller;
        Label lblUpdateInfo;
        Label lblStatus;
        Button btnAccept;
        Button btnDecline;
        SdlDotNet.Widgets.Timer tmrRestart;
        ProgressBar pgbDownloadProgress;

        UpdateEngine updateEngine;

        public UpdateEngine UpdateEngine {
            get { return updateEngine; }
            set { updateEngine = value; }
        }

        public winUpdater(UpdateEngine updateEngine)
            : base("winUpdater") {
            Windows.WindowSwitcher.UpdaterWindow = this;
            this.updateEngine = updateEngine;
            this.updateEngine.Updater.StatusUpdated += new EventHandler(Updater_StatusUpdated);
            this.updateEngine.Updater.PackageDownloadStart += new EventHandler<PMU.Updater.PackageDownloadStartEventArgs>(Updater_PackageDownloadStart);
            this.updateEngine.Updater.PackageInstallationComplete += new EventHandler<PMU.Updater.PackageInstallationCompleteEventArgs>(Updater_PackageInstallationComplete);
            this.updateEngine.Updater.InstallationComplete += new EventHandler(Updater_InstallationComplete);
            this.Windowed = true;
            this.TitleBar.Text = "Updater";
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);

            this.Size = new Size(400, 400);
            this.BackColor = Color.White;
            this.Location = DrawingSupport.GetCenter(WindowManager.ScreenSize, this.Size);
            lblUpdateFound = new Label("lblUpdateFound");
            lblUpdateFound.Location = new Point(10, 300);
            lblUpdateFound.AutoSize = true;
            lblUpdateFound.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            lblUpdateFound.Text = "An update has been found! Would you like to install this update?";

            packageScroller = new PackageScroller("packageScroller");
            packageScroller.Location = new Point(0, 0);

            for (int i = 0; i < updateEngine.LastCheckResult.PackagesToUpdate.Count; i++) {
                packageScroller.AddPackage(updateEngine.LastCheckResult.PackagesToUpdate[i]);
            }
            packageScroller.PackageButtonSelected += new EventHandler<PackageButtonSelectedEventArgs>(packageScroller_PackageButtonSelected);

            lblUpdateInfo = new Label("lblUpdateInfo");
            lblUpdateInfo.Size = new Size(188, 300);
            lblUpdateInfo.Location = new Point(212, 0);
            lblUpdateInfo.WordWrap = true;
            lblUpdateInfo.Font = Graphics.FontManager.LoadFont("tahoma", 12);

            lblStatus = new Label("lblStatus");
            lblStatus.Location = new Point(lblUpdateFound.Location.X, lblUpdateFound.Location.Y + lblUpdateFound.Height + 5);
            lblStatus.AutoSize = true;
            lblStatus.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            lblStatus.Visible = false;

            lblStatus = new Label("lblStatus");
            lblStatus.Location = new Point(lblUpdateFound.Location.X, lblUpdateFound.Location.Y + lblUpdateFound.Height + 5);
            lblStatus.AutoSize = true;
            lblStatus.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            lblStatus.Visible = false;

            pgbDownloadProgress = new ProgressBar("pgbDownloadProgress");
            pgbDownloadProgress.Location = new Point(lblUpdateFound.Location.X, lblUpdateFound.Location.Y + lblUpdateFound.Height + 5);
            pgbDownloadProgress.Size = new System.Drawing.Size(this.Width - (lblUpdateFound.Location.X * 2), 20);
            pgbDownloadProgress.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            pgbDownloadProgress.Visible = false;
            pgbDownloadProgress.TextStyle = ProgressBarTextStyle.Percent;

            btnAccept = new Button("btnAccept");
            btnAccept.Text = "Yes";
            btnAccept.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnAccept.Size = new Size(100, 20);
            btnAccept.Location = new Point(lblUpdateFound.Location.X, lblUpdateFound.Location.Y + lblUpdateFound.Height + 5);
            btnAccept.Click += new EventHandler<MouseButtonEventArgs>(btnAccept_Click);

            btnDecline = new Button("btnDecline");
            btnDecline.Text = "No";
            btnDecline.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnDecline.Size = new Size(100, 20);
            btnDecline.Location = new Point(lblUpdateFound.Location.X + btnAccept.Width + 5, lblUpdateFound.Location.Y + lblUpdateFound.Height + 5);
            btnDecline.Click += new EventHandler<MouseButtonEventArgs>(btnDecline_Click);

            tmrRestart = new SdlDotNet.Widgets.Timer("restartTimer");
            tmrRestart.Interval = 1000;
            tmrRestart.Elapsed += new EventHandler(tmrRestart_Elapsed);

            this.AddWidget(packageScroller);
            this.AddWidget(lblUpdateInfo);
            this.AddWidget(lblUpdateFound);
            this.AddWidget(btnAccept);
            this.AddWidget(btnDecline);
            this.AddWidget(lblStatus);
            this.AddWidget(pgbDownloadProgress);
            this.AddWidget(tmrRestart);

            this.LoadComplete();

            packageScroller.ScrollToButton(0);
            LoadPackageInfo(packageScroller.Buttons[0]);
        }

        int restartCountdown;
        void tmrRestart_Elapsed(object sender, EventArgs e) {
            restartCountdown--;
            if (restartCountdown <= 0) {
                RestartApplication();
            } else {
                UpdateStatus("Update complete! This program will restart in " + restartCountdown + "...");
            }
        }

        void RestartApplication() {
            // Restarting!
            // Get the parameters/arguments passed to program if any
            string arguments = string.Empty;
            string[] args = Environment.GetCommandLineArgs();
            for (int i = 1; i < args.Length; i++) // args[0] is always exe path/filename
                arguments += args[i] + " ";

            // Restart current application, with same arguments/parameters
            System.Diagnostics.Process.Start(System.Reflection.Assembly.GetEntryAssembly().Location, arguments);
            Sdl.SdlCore.QuitApplication();
        }

        void Updater_InstallationComplete(object sender, EventArgs e) {
            UpdateStatus("Update complete! This program will restart in 5...");
            restartCountdown = 5;
            tmrRestart.Start();
        }

        void Updater_PackageInstallationComplete(object sender, PMU.Updater.PackageInstallationCompleteEventArgs e) {
            packageScroller.Buttons[e.PackageIndex].Installed = true;
            if (updateEngine.LastCheckResult.PackagesToUpdate.Count != e.PackageIndex + 1) {
                packageScroller.ScrollToButton(e.PackageIndex + 1);
                LoadPackageInfo(packageScroller.Buttons[e.PackageIndex + 1]);
            }
        }

        void Updater_PackageDownloadStart(object sender, PMU.Updater.PackageDownloadStartEventArgs e) {
            lblUpdateFound.Text = "Package: " + e.Package.FullID;
            lblStatus.Hide();
            pgbDownloadProgress.Show();
            //UpdateStatus("Downloading...");
            e.Download.DownloadUpdate += new EventHandler<FileDownloadingEventArgs>(Download_DownloadUpdate);
            e.Download.DownloadComplete += new EventHandler<FileDownloadingEventArgs>(Download_DownloadComplete);
        }

        void Download_DownloadComplete(object sender, FileDownloadingEventArgs e) {
            pgbDownloadProgress.Hide();
            lblStatus.Show();
            UpdateStatus("Installing...");
        }

        void Download_DownloadUpdate(object sender, FileDownloadingEventArgs e) {
            pgbDownloadProgress.Value = e.Percent;
            //UpdateStatus("Downloading: " + PMU.Core.IO.Files.GetFileSize(e.Position) + "/" + PMU.Core.IO.Files.GetFileSize(e.FileSize) + " (" + e.Percent + "%)");
        }

        void btnAccept_Click(object sender, MouseButtonEventArgs e) {
            btnAccept.Visible = false;
            btnDecline.Visible = false;
            lblStatus.Visible = true;
            Thread updateThread = new Thread(new ThreadStart(delegate()
            {
                this.updateEngine.Updater.PerformUpdate(this.updateEngine.LastCheckResult);
            }));
            updateThread.Start();
        }

        void btnDecline_Click(object sender, MouseButtonEventArgs e) {
            Environment.Exit(0);
        }

        void Updater_StatusUpdated(object sender, EventArgs e) {
            UpdateStatus(this.updateEngine.Updater.Status);
        }

        void UpdateStatus(string status) {
            lblStatus.Text = status;
        }

        void packageScroller_PackageButtonSelected(object sender, PackageButtonSelectedEventArgs e) {
            LoadPackageInfo(e.PackageButton);
        }

        private void LoadPackageInfo(PackageButton packageButton) {
            lblUpdateInfo.Text = "";
            CharRenderOptions renderOptions = new CharRenderOptions(Color.Black);
            renderOptions.Bold = true;
            lblUpdateInfo.AppendText("Package: " + packageButton.AttachedPackage.FullID + "\n", renderOptions);
            lblUpdateInfo.AppendText("Name: " + packageButton.AttachedPackage.Name + "\nSize: " + PMU.Core.IO.Files.GetFileSize(packageButton.AttachedPackage.Size) +
                "\nPublish Date: " + packageButton.AttachedPackage.PublishDate.ToLongDateString() + "\n\nDescription:\n" + packageButton.AttachedPackage.Description, new CharRenderOptions(Color.Black));
        }
    }
}
