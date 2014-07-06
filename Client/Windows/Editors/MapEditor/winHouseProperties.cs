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


namespace Client.Logic.Windows.Editors.MapEditor
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Widgets;
    using Client.Logic.Maps;
    using PMU.Core;

    class winHouseProperties : Window
    {
        #region Fields

        Button btnCancel;
        Button btnOk;
        Button btnPlay;
        Button btnStop;
        ComboBox cmbMusic;
        Label lblMusic;

        HouseProperties properties;

        #endregion Fields

        #region Constructors

        public winHouseProperties()
            : base("winHouseProperties") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Music";
            this.TitleBar.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            this.TitleBar.CloseButton.Visible = true;
            this.AlwaysOnTop = true;
            this.BackColor = Color.White;
            //this.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            //this.BorderWidth = 1;
            //this.BorderColor = Color.Black;
            this.Size = new Size(500, 160);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            properties = Maps.MapHelper.ActiveMap.ExportToHouseClass();

            #region General

            lblMusic = new Label("lblMusic");
            lblMusic.Font = Logic.Graphics.FontManager.LoadFont("PMU", 22);
            lblMusic.AutoSize = true;
            lblMusic.Location = new Point(20, 10);
            lblMusic.Text = "Music";

            cmbMusic = new ComboBox("cmbMusic");
            cmbMusic.Size = new System.Drawing.Size(375, 30);
            cmbMusic.Location = new Point(25, 50);

            btnPlay = new Button("btnPlay");
            btnPlay.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnPlay.Size = new System.Drawing.Size(75, 30);
            btnPlay.Location = new Point(410, 20);
            Skins.SkinManager.LoadButtonGui(btnPlay);
            btnPlay.Text = "Play";
            btnPlay.Click += new EventHandler<MouseButtonEventArgs>(btnPlay_Click);

            btnStop = new Button("btnStop");
            btnStop.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnStop.Size = new System.Drawing.Size(75, 30);
            btnStop.Location = new Point(410, 50);
            Skins.SkinManager.LoadButtonGui(btnStop);
            btnStop.Text = "Stop";
            btnStop.Click += new EventHandler<MouseButtonEventArgs>(btnStop_Click);

            btnOk = new Button("btnOk");
            btnOk.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnOk.Size = new System.Drawing.Size(75, 30);
            btnOk.Location = new Point(20, 100);
            Skins.SkinManager.LoadButtonGui(btnOk);
            btnOk.Text = "Ok";
            btnOk.Click += new EventHandler<MouseButtonEventArgs>(btnOk_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnCancel.Size = new System.Drawing.Size(75, 30);
            btnCancel.Location = new Point(95, 100);
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            #endregion
            
            this.AddWidget(lblMusic);
            this.AddWidget(cmbMusic);
            this.AddWidget(btnPlay);
            this.AddWidget(btnStop);
            this.AddWidget(btnOk);
            this.AddWidget(btnCancel);

            this.LoadComplete();

            LoadMusic();
        }

        void btnStop_Click(object sender, MouseButtonEventArgs e) {
            // Stop the music
            Music.Music.AudioPlayer.StopMusic();
            // Play the map music
            if (!string.IsNullOrEmpty(MapHelper.ActiveMap.Music)) {
                //Music.Music.AudioPlayer.PlayMusic(MapHelper.ActiveMap.Music);
                ((Client.Logic.Music.Bass.BassAudioPlayer)Logic.Music.Music.AudioPlayer).FadeToNext(MapHelper.ActiveMap.Music, 1000);
            }
        }

        void btnPlay_Click(object sender, MouseButtonEventArgs e) {
            string song = null;
            if (cmbMusic.SelectedIndex > -1) {
                song = ((ListBoxTextItem)cmbMusic.SelectedItem).Text;
            }
            if (!string.IsNullOrEmpty(song)) {
                Music.Music.AudioPlayer.PlayMusic(song, -1, true, true);
            }
        }

        void btnOk_Click(object sender, MouseButtonEventArgs e) {
            properties.Music = (cmbMusic.SelectedItem == null || string.IsNullOrEmpty(cmbMusic.SelectedItem.TextIdentifier)) ? properties.Music : cmbMusic.SelectedItem.TextIdentifier;
            Maps.MapHelper.ActiveMap.LoadFromHouseClass(properties);
            this.Close();
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        #endregion Constructors

        void LoadMusic() {
            SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            string[] musicFiles = System.IO.Directory.GetFiles(IO.Paths.MusicPath);
            for (int i = 0; i < musicFiles.Length; i++) {
                cmbMusic.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(musicFiles[i])));
            }
            cmbMusic.SelectItem(properties.Music);
        }
    }
}
