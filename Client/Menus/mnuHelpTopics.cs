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
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuHelpTopics : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        #region Fields

        Label lblHelpTopics;
        ListBox lstHelpTopics;
        Button btnShowHelp;

        #endregion Fields

        #region Constructors

        public mnuHelpTopics(string name)
            : base(name) {
            this.Size = new Size(185, 220);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            lblHelpTopics = new Label("lblHelpTopics");
            lblHelpTopics.Location = new Point(20, 0);
            lblHelpTopics.Font = FontManager.LoadFont("PMU", 36);
            lblHelpTopics.AutoSize = true;
            lblHelpTopics.Text = "Help Topics";
            lblHelpTopics.ForeColor = Color.WhiteSmoke;

            lstHelpTopics = new ListBox("lstHelpTopics");
            lstHelpTopics.Location = new Point(10, 50);
            lstHelpTopics.Size = new Size(this.Width - lstHelpTopics.X * 2, this.Height - lstHelpTopics.Y - 10 - 30);
            lstHelpTopics.BackColor = Color.Transparent;
            lstHelpTopics.BorderStyle = SdlDotNet.Widgets.BorderStyle.None;

            btnShowHelp = new Button("btnShowHelp");
            btnShowHelp.Location = new Point(10, lstHelpTopics.Y + lstHelpTopics.Height + 5);
            btnShowHelp.Size = new Size(100, 30);
            btnShowHelp.Text = "Load Topic";
            Skins.SkinManager.LoadButtonGui(btnShowHelp);
            btnShowHelp.Click += new EventHandler<MouseButtonEventArgs>(btnShowHelp_Click);

            this.AddWidget(lblHelpTopics);
            this.AddWidget(lstHelpTopics);
            this.AddWidget(btnShowHelp);

            LoadHelpTopics();
        }

        void btnShowHelp_Click(object sender, MouseButtonEventArgs e) {
            if (lstHelpTopics.SelectedItems.Count > 0) {
                MenuSwitcher.ShowHelpPage(((ListBoxTextItem)lstHelpTopics.SelectedItems[0]).Text, 0);
                Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
            }
        }

        #endregion Constructors
        #region Methods

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.Backspace: {
                        // Show the others menu when the backspace key is pressed
                        MenuSwitcher.ShowOthersMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        public void LoadHelpTopics() {
            string[] dirs = System.IO.Directory.GetDirectories(IO.Paths.StartupPath + "Help");
            for (int i = 0; i < dirs.Length; i++) {
                ListBoxTextItem lbi = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), System.IO.Path.GetFileNameWithoutExtension(dirs[i]));
                lbi.ForeColor = Color.WhiteSmoke;
                lstHelpTopics.Items.Add(lbi);
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
