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

namespace Client.Logic.ExpKit
{
    class KitContainer : Panel
    {
        ModuleSwitcher moduleSwitcher;
        IKitModule activeModule;
        Button btnRight;
        Button btnLeft;
        Label lblModuleName;

        public ModuleSwitcher ModuleSwitcher {
            get { return moduleSwitcher; }
        }

        public new Size Size {
            get { return base.Size; }
            set {
                base.Size = value;
                RecalculateWidgetPositions();
            }
        }

        public IKitModule ActiveModule {
            get {
                return activeModule;
            }
        }

        public KitContainer(string name)
            : base(name) {
            base.BackColor = Color.FromArgb(32, 69, 79);

            moduleSwitcher = new ModuleSwitcher();

            btnRight = new Button("btnRight");
            btnRight.Size = new Size(30, 20);
            btnRight.Text = "->";
            Skins.SkinManager.LoadButtonGui(btnRight);
            btnRight.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnRight_Click);

            btnLeft = new Button("btnLeft");
            btnLeft.Size = new Size(30, 20);
            btnLeft.Text = "<-";
            Skins.SkinManager.LoadButtonGui(btnLeft);
            btnLeft.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLeft_Click);

            lblModuleName = new Label("lblModuleName");
            lblModuleName.BackColor = Color.Transparent;
            lblModuleName.AutoSize = true;
            lblModuleName.Location = new Point(0, 0);
            lblModuleName.ForeColor = Color.WhiteSmoke;

            this.AddWidget(btnRight);
            this.AddWidget(btnLeft);
            this.AddWidget(lblModuleName);

            //SetActiveModule(0);
        }

        void btnLeft_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (moduleSwitcher.AvailableKitModules.IndexOf(activeModule) - 1 >= 0) {
                SetActiveModule(moduleSwitcher.AvailableKitModules.IndexOf(activeModule) - 1);
            }
        }

        void btnRight_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (moduleSwitcher.AvailableKitModules.IndexOf(activeModule) + 1 < moduleSwitcher.AvailableKitModules.Count) {
                try {
                    SetActiveModule(moduleSwitcher.AvailableKitModules.IndexOf(activeModule) + 1);
                } catch (Exception ex) {
                    throw new Exception(ex.Message + " [Module: " + ((Enums.ExpKitModules)moduleSwitcher.AvailableKitModules.IndexOf(activeModule) + 1).ToString() + "]");
                }
            }
        }

        private void RecalculateWidgetPositions() {
            btnRight.Location = new Point(this.Width - btnRight.Width, 0);
            btnLeft.Location = new Point(this.Width - btnRight.Width - btnLeft.Width, 0);
        }

        public void SetActiveModule(Enums.ExpKitModules module) {
            for (int i = 0; i < moduleSwitcher.AvailableKitModules.Count; i++) {
                if (moduleSwitcher.AvailableKitModules[i].ModuleID == module) {
                    SetActiveModule(i);
                    break;
                }
            }
        }

        public void SetActiveModule(int index) {
            if (activeModule != null) {
                activeModule.SwitchOut();
                activeModule.ModulePanel.Hide();
                //this.RemoveWidget(activeModule.ModulePanel.Name);
            }
            activeModule = moduleSwitcher.GetAvailableKitModule(index);
            Network.Messenger.SendPacket(PMU.Sockets.TcpPacket.CreatePacket("activekitmodule", ((int)(activeModule.ModuleID)).ToString()));
            activeModule.Initialize(new Size(this.Width, this.Height - btnLeft.Height));
            lblModuleName.Text = activeModule.ModuleName + " [" + (index + 1) + "/" + moduleSwitcher.AvailableKitModules.Count + "]";
            if (this.ContainsWidget(activeModule.ModulePanel.Name) == false) {
                this.AddWidget(activeModule.ModulePanel);
            }
            activeModule.ModulePanel.Show();
        }

        public override void OnTick(SdlDotNet.Core.TickEventArgs e) {
            base.OnTick(e);
            if (activeModule != null) {
                if (activeModule.ModulePanel.Location.X != 0 || activeModule.ModulePanel.Location.Y != btnLeft.Height) {
                    activeModule.ModulePanel.Location = new Point(0, btnLeft.Height);
                }
                if (activeModule.ModulePanel.Size.Width != this.Width || activeModule.ModulePanel.Size.Height != this.Height - btnLeft.Height) {
                    activeModule.ModulePanel.Size = new Size(this.Width, this.Height - btnLeft.Height);
                }
                //activeModule.ModulePanel.BlitToScreen(destinationSurface);
            }
            //if (activeModule != null) {
            //    activeModule.ModulePanel.OnTick(e);
            //}
        }

        public override void OnMouseDown(SdlDotNet.Widgets.MouseButtonEventArgs e) {
            base.OnMouseDown(e);
            //if (activeModule != null) {
            //    activeModule.ModulePanel.OnMouseDown(new MouseButtonEventArgs(e));
            //}
        }

        public override void OnMouseMotion(SdlDotNet.Input.MouseMotionEventArgs e) {
            base.OnMouseMotion(e);
            //if (activeModule != null) {
            //    activeModule.ModulePanel.OnMouseMotion(e);
            //}
        }

        public override void OnMouseUp(SdlDotNet.Widgets.MouseButtonEventArgs e) {
            base.OnMouseUp(e);
            //if (activeModule != null) {
            //    activeModule.ModulePanel.OnMouseUp(new MouseButtonEventArgs(e));
            //}
        }

    }
}
