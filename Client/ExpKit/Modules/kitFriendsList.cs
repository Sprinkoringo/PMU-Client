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

namespace Client.Logic.ExpKit.Modules
{
    class kitFriendsList : Panel, IKitModule
    {
        int moduleIndex;
        bool enabled;
        Size containerSize;

        List<Label> friendNames;
        List<PictureBox> friendOnlineStatus;

        public kitFriendsList(string name)
            : base(name) {
            enabled = true;

            friendNames = new List<Label>();
            friendOnlineStatus = new List<PictureBox>();

            base.BackColor = Color.Transparent;
        }


        public void SwitchOut() {
        }

        public void Initialize(Size containerSize) {
            this.containerSize = containerSize;
            UpdateList(Players.PlayerManager.MyPlayer.FriendsList);
        }

        public void UpdateList(List<Players.Friend> friends) {
            if (friends.Count < friendNames.Count) {
                int widgetsToRemove = -1;
                widgetsToRemove = friendNames.Count - friends.Count;
                widgetsToRemove *= 2;
                for (int i = widgetsToRemove - 1; i >= 0; i--) {
                    this.RemoveWidget(ChildWidgets[(ChildWidgets.Count - 1) - i].Name);
                }
                for (int i = (widgetsToRemove / 2) - 1; i >= 0; i--) {
                    friendNames.RemoveAt(i);
                    friendOnlineStatus.RemoveAt(i);
                }
            }
            for (int i = 0; i < friends.Count; i++) {
                if (friendNames.Count <= i) {
                    Label lblName = new Label("lblName" + i);
                    lblName.Location = new Point(5, i * 25);
                    lblName.AutoSize = true;
                    lblName.Font = Logic.Graphics.FontManager.LoadFont("PMU", 16);
                    lblName.ForeColor = Color.WhiteSmoke;

                    PictureBox picOnlineStatus = new PictureBox("picOnlineStatus" + i);
                    picOnlineStatus.Size = new Size(20, 20);
                    picOnlineStatus.Location = new Point(containerSize.Width - picOnlineStatus.Width - 10, lblName.Y);
                    picOnlineStatus.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

                    this.AddWidget(lblName);
                    this.AddWidget(picOnlineStatus);

                    friendNames.Add(lblName);
                    friendOnlineStatus.Add(picOnlineStatus);
                }
                friendNames[i].Text = friends[i].Name;
                if (friends[i].Online) {
                    friendOnlineStatus[i].BackColor = Color.Green;
                } else {
                    friendOnlineStatus[i].BackColor = Color.Red;
                }
            }
        }

        public int ModuleIndex {
            get { return moduleIndex; }
        }

        public string ModuleName {
            get { return "Friends List"; }
        }

        public void Created(int index) {
            moduleIndex = index;
        }

        public Panel ModulePanel {
            get { return this; }
        }


        public bool Enabled {
            get { return enabled; }
            set {
                enabled = value;
                if (EnabledChanged != null)
                    EnabledChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler EnabledChanged;


        public Enums.ExpKitModules ModuleID {
            get { return Enums.ExpKitModules.FriendsList; }
        }
    }
}
