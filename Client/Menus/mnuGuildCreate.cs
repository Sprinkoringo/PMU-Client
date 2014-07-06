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
using PMU.Core;
using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuGuildCreate : Widgets.BorderedPanel, Core.IMenu {


        public bool Modal {
            get;
            set;
        }
        #region Fields

        Label lblGuild;
        Label lblName;
        TextBox txtName;
        Label lblMembers;
        ListBox lbxMembers;
        Button btnOK;
        Button btnCancel;

        #endregion Fields

        #region Constructors

        public mnuGuildCreate(string name, string[] parse)
            : base(name) {
            this.Size = new Size(280, 280);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(180, 80);

            lblGuild = new Label("lblGuild");
            lblGuild.Location = new Point(20, 0);
            lblGuild.AutoSize = true;
            lblGuild.Font = FontManager.LoadFont("PMU", 48);
            lblGuild.Text = "Register Guild";
            lblGuild.ForeColor = Color.WhiteSmoke;

            lblName = new Label("lblName");
            lblName.Location = new Point(10, 50);
            lblName.AutoSize = true;
            lblName.Font = FontManager.LoadFont("PMU", 16);
            lblName.Text = "Guild Name:";
            lblName.ForeColor = Color.WhiteSmoke;

            txtName = new TextBox("txtName");
            txtName.Size = new Size(180, 24);
            txtName.Location = new Point(20, 70);
            txtName.Font = FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadTextBoxGui(txtName);

            lblMembers = new Label("lblMembers");
            lblMembers.Location = new Point(10, 100);
            lblMembers.AutoSize = true;
            lblMembers.Font = FontManager.LoadFont("PMU", 16);
            lblMembers.Text = "Party Members: (Will become co-leaders)";
            lblMembers.ForeColor = Color.WhiteSmoke;

            lbxMembers = new ListBox("lbxMembers");
            lbxMembers.Location = new Point(20, 120);
            lbxMembers.Size = new Size(240, 80);

            btnOK = new Button("btnOK");
            btnOK.Size = new System.Drawing.Size(60, 32);
            btnOK.Location = new Point(60, 210);
            btnOK.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnOK.Text = "OK";
            Skins.SkinManager.LoadButtonGui(btnOK);
            btnOK.Click +=new EventHandler<MouseButtonEventArgs>(btnOK_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Size = new System.Drawing.Size(60, 32);
            btnCancel.Location = new Point(150, 210);
            btnCancel.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnCancel.Text = "Cancel";
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);
            
            this.AddWidget(lblGuild);
            this.AddWidget(lblName);
            this.AddWidget(txtName);
            this.AddWidget(lblMembers);
            this.AddWidget(lbxMembers);
            this.AddWidget(btnOK);
            this.AddWidget(btnCancel);

            LoadPartyFromPacket(parse);
        }

        #endregion Constructors

        void btnOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Messenger.MakeGuild(txtName.Text);
            MenuSwitcher.CloseAllMenus();
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            MenuSwitcher.CloseAllMenus();
        }

        #region Methods

        void LoadPartyFromPacket(string[] parse) {

            int count = parse[1].ToInt();

            for (int i = 0; i < count; i++) {
                ListBoxTextItem lbiName = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), parse[i + 2]);
                lbxMembers.Items.Add(lbiName);
            }

        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
