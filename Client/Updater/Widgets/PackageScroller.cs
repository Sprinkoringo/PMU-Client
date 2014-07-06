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
using System.Drawing;
using PMU.Updater.Linker;

namespace Client.Logic.Updater.Widgets
{
    class PackageScroller : Panel
    {
        List<PackageButton> buttons;
        VScrollBar vScroll;
        int maxVisibleButtons;
        bool locked;

        public List<PackageButton> Buttons {
            get { return buttons; }
        }

        public bool Locked {
            get { return locked; }
            set { locked = value; }
        }

        public event EventHandler<PackageButtonSelectedEventArgs> PackageButtonSelected;

        public PackageScroller(string name)
            : base(name) {

            this.Size = new Size(212, 300);
            this.BackColor = Color.White;
            vScroll = new VScrollBar("vScroll");
            vScroll.BackColor = Color.Transparent;
            vScroll.Size = new Size(12, this.Height);
            vScroll.Location = new Point(this.Width - vScroll.Width, 0);
            vScroll.Minimum = 1;
            vScroll.Value = 1;
            vScroll.ValueChanged += new EventHandler<ValueChangedEventArgs>(vScroll_ValueChanged);

            buttons = new List<PackageButton>();
            maxVisibleButtons = (this.Height / PackageButton.BUTTON_HEIGHT) - 1;

            this.AddWidget(vScroll);
        }

        void vScroll_ValueChanged(object sender, ValueChangedEventArgs e) {
            VerifyButtons();
        }

        void VerifyButtons() {
            int lastY = 0;
            for (int i = 0; i < buttons.Count; i++) {
                if (IsButtonVisible(buttons[i])) {
                    buttons[i].Location = new Point(0, lastY * PackageButton.BUTTON_HEIGHT);
                    buttons[i].Visible = true;
                    lastY++;
                } else {
                    buttons[i].Visible = false;
                }
            }
        }

        public void AddPackage(IPackageInfo package) {
            PackageButton button = new PackageButton("button" + buttons.Count, package);
            button.Click += new EventHandler<MouseButtonEventArgs>(button_Click);
            buttons.Add(button);
            vScroll.Maximum = System.Math.Max(1, buttons.Count - maxVisibleButtons);
            base.AddWidget(button);
            VerifyButtons();
        }

        void button_Click(object sender, MouseButtonEventArgs e) {
            if (!locked) {
                for (int i = 0; i < buttons.Count; i++) {
                    if (buttons[i] != (PackageButton)sender) {
                        buttons[i].Selected = false;
                    } else {
                        buttons[i].Selected = true;
                    }
                }
            }
            if (PackageButtonSelected != null) {
                PackageButtonSelected(this, new PackageButtonSelectedEventArgs((PackageButton)sender));
            }
        }

        public void DeselectAll() {
            for (int i = 0; i < buttons.Count; i++) {
                buttons[i].Selected = false;
            }
        }

        public void ScrollToButton(int index) {
            DeselectAll();
            vScroll.Value = index - (maxVisibleButtons - 1);
            buttons[index].Selected = true;
            if (PackageButtonSelected != null) {
                PackageButtonSelected(this, new PackageButtonSelectedEventArgs(buttons[index]));
            }
        }

        bool IsButtonVisible(PackageButton button) {
            int index = buttons.IndexOf(button)+1;
            if (index >= vScroll.Value && index <= vScroll.Value + maxVisibleButtons) {
                return true;
            } else {
                return false;
            }
        }
    }
}
