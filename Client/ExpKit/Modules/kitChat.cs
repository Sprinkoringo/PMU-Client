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
using PMU.Core;
using SdlDotNet.Widgets;
using Client.Logic.Graphics;

namespace Client.Logic.ExpKit.Modules
{
    class kitChat : Panel, IKitModule
    {
        int moduleIndex;
        Size containerSize;
        Label lblChat;
        TextBox txtCommands;
        bool enabled;
        Label lblChannel;
        ComboBox channelSelector;

        public Label Chat {
            get {
                return lblChat;
            }
        }

        public kitChat(string name)
            : base(name) {
            enabled = true;

            base.BackColor = Color.Transparent;

            lblChat = new Label("lblChat");
            //lblChat.Location = new Point(0, 0);
            //lblChat.Size = new Size(200, this.Height - 20);
            lblChat.AutoScroll = IO.Options.AutoScroll;
            lblChat.Font = FontManager.LoadFont("PMU", 16);

            lblChannel = new Label("lblChannel");
            lblChannel.Font = FontManager.LoadFont("tahoma", 15);
            lblChannel.Text = "Channel:";
            lblChannel.ForeColor = Color.WhiteSmoke;

            channelSelector = new ComboBox("channelSelector");
            

            txtCommands = new TextBox("txtCommands");
            Skins.SkinManager.LoadTextBoxGui(txtCommands);
            //txtCommands.Location = new Point(0, lblChat.Y + lblChat.Height);
            //txtCommands.Size = new Size(this.Width, 20);
            txtCommands.Font = FontManager.LoadFont("PMU", 16);
            txtCommands.KeyUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(txtCommands_KeyUp);
            //txtCommands. = FontManager.LoadFont("tahoma", 12);

            this.AddWidget(lblChat);
            this.AddWidget(txtCommands);
            this.AddWidget(lblChannel);
            this.AddWidget(channelSelector);
        }

        public void AppendChat(string text, SdlDotNet.Widgets.CharRenderOptions[] renderOptions) {
            lblChat.AppendText(text, renderOptions);
            if (lblChat.Text.Length > 10000) {
                GlyphRenderData[] newRenderOptions = new GlyphRenderData[10000];
                lblChat.CharRenderOptions.CopyTo(lblChat.CharRenderOptions.Count - 10000, newRenderOptions, 0, 10000);
                lblChat.SetText(newRenderOptions);
            }
            lblChat.ScrollToBottom();
        }

        public void AppendChat(string text, SdlDotNet.Widgets.CharRenderOptions renderOptions) {
            lblChat.AppendText(text, renderOptions);
            lblChat.ScrollToBottom();
        }

        public void AppendChat(string text, Color color) {
            AppendChat(text + "\n", new CharRenderOptions(color));
        }

        void txtCommands_KeyUp(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            if (e.Key == SdlDotNet.Input.Key.Return) {
                CommandProcessor.ProcessCommand(txtCommands.Text, (Enums.ChatChannel)Enum.Parse(typeof(Enums.ChatChannel), channelSelector.SelectedItem.TextIdentifier, true));
                txtCommands.Text = "";
            }
        }

        public void SetAutoScroll(bool set) {
            lblChat.AutoScroll = set;
        }

        public void Created(int index) {
            moduleIndex = index;
        }

        public void SwitchOut() {

        }

        public void Initialize(Size containerSize) {
            this.containerSize = containerSize;
            RecalculatePositions();
            this.RequestRedraw();
        }

        private void RecalculatePositions() {
            lblChat.Location = new Point(0, 0);
            lblChat.Size = new Size(containerSize.Width, containerSize.Height - 40 - 18);
            txtCommands.Location = new Point(0, lblChat.Y + lblChat.Height + 1);
            txtCommands.Size = new Size(containerSize.Width, 20);
            Skins.SkinManager.LoadTextBoxGui(txtCommands);

            lblChannel.Font = FontManager.LoadFont("PMU", 16);
            lblChannel.Size = new Size(75, 18);
            lblChannel.Location = new Point(0, txtCommands.Y + txtCommands.Height);

            channelSelector.Size = new Size(containerSize.Width - lblChannel.Width, 18);
            channelSelector.Location = new Point(lblChannel.Width, txtCommands.Y + txtCommands.Height);
            channelSelector.BorderColor = Color.Black;
            channelSelector.BorderWidth = 1;
            channelSelector.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;

            int selectedIndex = channelSelector.SelectedIndex;
            channelSelector.Items.Clear();
            channelSelector.Items.Add(new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "Local"));
            channelSelector.Items.Add(new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "Global"));
            channelSelector.Items.Add(new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "Guild"));
            if (Ranks.IsAllowed(Players.PlayerManager.MyPlayer, Enums.Rank.Moniter)) {
                channelSelector.Items.Add(new ListBoxTextItem(FontManager.LoadFont("PMU", 16), "Staff"));
            }
            if (selectedIndex < channelSelector.Items.Count && selectedIndex > -1) {
                channelSelector.SelectItem(selectedIndex);
            } else {
                channelSelector.SelectItem(0);
            }
            //lblCounter.Size = new Size(containerSize.Width - 10, 30);
            //btnIncrement.Location = new Point(DrawingSupport.GetCenter(containerSize, btnIncrement.Size).X - (btnIncrement.Width / 2), lblCounter.Y + lblCounter.Height + 5);
            //btnDecrement.Location = new Point(DrawingSupport.GetCenter(containerSize, btnDecrement.Size).X + (btnDecrement.Width / 2), lblCounter.Y + lblCounter.Height + 5);
        }

        public int ModuleIndex {
            get { return moduleIndex; }
        }

        public string ModuleName {
            get { return "Chat"; }
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
            get { return Enums.ExpKitModules.Chat; }
        }
    }
}
