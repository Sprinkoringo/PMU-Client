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

namespace Client.Logic.Windows.Editors.MapEditor
{
    class winScreenshotOptions : Window
    {
        CheckBox chkCaptureRegion;
        CheckBox chkCaptureAttributes;
        CheckBox chkCaptureMapGrid;
        Button btnTakeScreenshot;
        Button btnCancel;
        Label lblSaved;
        Timer tmrHideInfo;

        public winScreenshotOptions()
            : base("winScreenshotOptions") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Screenshot Options";
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            this.TitleBar.CloseButton.Visible = false;
            this.AlwaysOnTop = true;
            this.BackColor = Color.White;
            this.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            this.BorderWidth = 2;
            this.BorderColor = Color.Black;
            this.Size = new Size(200, 150);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            chkCaptureRegion = new CheckBox("chkCaptureRegion");
            chkCaptureRegion.BackColor = Color.Transparent;
            chkCaptureRegion.Location = new Point(5, 5);
            chkCaptureRegion.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            chkCaptureRegion.Size = new Size(200, 15);
            chkCaptureRegion.Text = "Only capture visible area";

            chkCaptureAttributes = new CheckBox("chkCaptureAttributes");
            chkCaptureAttributes.BackColor = Color.Transparent;
            chkCaptureAttributes.Location = new Point(5, 20);
            chkCaptureAttributes.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            chkCaptureAttributes.Size = new Size(200, 15);
            chkCaptureAttributes.Text = "Capture Attributes";
            chkCaptureAttributes.Checked = true;

            chkCaptureMapGrid = new CheckBox("chkCaptureMapGrid");
            chkCaptureMapGrid.BackColor = Color.Transparent;
            chkCaptureMapGrid.Location = new Point(5, 35);
            chkCaptureMapGrid.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            chkCaptureMapGrid.Size = new Size(200, 15);
            chkCaptureMapGrid.Text = "Capture Map Grid";
            chkCaptureMapGrid.Checked = true;

            btnTakeScreenshot = new Button("btnTakeScreenshot");
            btnTakeScreenshot.Size = new Size(70, 20);
            btnTakeScreenshot.Location = new Point(5, 70);
            btnTakeScreenshot.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnTakeScreenshot.Text = "Save!";
            btnTakeScreenshot.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnTakeScreenshot_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Size = new Size(70, 20);
            btnCancel.Location = new Point(80, 70);
            btnCancel.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnCancel_Click);

            lblSaved = new Label("lblSaved");
            lblSaved.AutoSize = true;
            lblSaved.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            lblSaved.Location = new Point(5, btnTakeScreenshot.Y + btnTakeScreenshot.Height + 5);
            lblSaved.Text = "Saved!";
            lblSaved.Visible = false;

            tmrHideInfo = new Timer("tmrHideInfo");
            tmrHideInfo.Interval = 2000;
            tmrHideInfo.Elapsed += new EventHandler(tmrHideInfo_Elapsed);

            this.AddWidget(chkCaptureRegion);
            this.AddWidget(chkCaptureAttributes);
            this.AddWidget(chkCaptureMapGrid);
            this.AddWidget(btnTakeScreenshot);
            this.AddWidget(btnCancel);
            this.AddWidget(lblSaved);
            this.AddWidget(tmrHideInfo);

            this.LoadComplete();
        }

        void tmrHideInfo_Elapsed(object sender, EventArgs e) {
            tmrHideInfo.Stop();
            lblSaved.Visible = false;
        }

        void chkCaptureRegion_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
        }

        void btnTakeScreenshot_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "Portable Network Graphic File|*.png|GIF File|*.gif|JPEG File|*.jpg|Bitmap File|*.bmp|Icon File|*.ico";
            sfd.AddExtension = true;
            if (sfd.ShowDialog() == SdlDotNet.Widgets.DialogResult.OK) {
                if (WindowSwitcher.GameWindow.MapViewer.ActiveMap != null && WindowSwitcher.GameWindow.MapViewer.ActiveMap.Loaded) {
                    SdlDotNet.Graphics.Surface surf = WindowSwitcher.GameWindow.MapViewer.CaptureMapImage(chkCaptureRegion.Checked, chkCaptureAttributes.Checked, chkCaptureMapGrid.Checked);
                    Graphics.SurfaceManager.SaveSurface(surf, sfd.FileName);
                    tmrHideInfo.Start();
                    lblSaved.Visible = true;  
                }
            }


        }
    }
}
