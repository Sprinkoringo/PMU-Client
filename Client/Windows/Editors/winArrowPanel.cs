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
using Client.Logic.Network;
using PMU.Sockets;
using PMU.Core;

namespace Client.Logic.Windows.Editors
{
    class winArrowPanel : Core.WindowCore
    {
        #region Fields

        int itemNum = -1;
        int currentTen = 0;

        Panel pnlArrowList;
        Panel pnlArrowEditor;

        ListBox lbxArrowList;
        ListBoxTextItem lbiArrow;
        //Button btnAddNew; (Can implement later...)
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        Label lblName;
        TextBox txtName;
        Label lblSprite;
        HScrollBar hsbPic;
        PictureBox pic;
        Label lblRange;
        HScrollBar hsbRange;
        Label lblAmount;
        HScrollBar hsbAmount;

        #endregion Fields

        #region Constructors
        public winArrowPanel()
            : base("winArrowPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Arrow Panel";

            pnlArrowList = new Panel("pnlArrowList");
            pnlArrowList.Size = new System.Drawing.Size(200, 230);
            pnlArrowList.Location = new Point(0, 0);
            pnlArrowList.BackColor = Color.White;
            pnlArrowList.Visible = true;

            pnlArrowEditor = new Panel("pnlArrowEditor");
            pnlArrowEditor.Size = new System.Drawing.Size(230, 261);
            pnlArrowEditor.Location = new Point(0, 0);
            pnlArrowEditor.BackColor = Color.White;
            pnlArrowEditor.Visible = false;


            lbxArrowList = new ListBox("lbxArrowList");
            lbxArrowList.Location = new Point(10, 10);
            lbxArrowList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                lbiArrow = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Arrows.ArrowHelper.Arrows[(i + 1) + 10 * currentTen].Name);
                lbxArrowList.Items.Add(lbiArrow);
            }


            btnBack = new Button("btnBack");
            btnBack.Location = new Point(10, 160);
            btnBack.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnBack.Size = new System.Drawing.Size(64, 16);
            btnBack.Visible = true;
            btnBack.Text = "<--";
            btnBack.Click += new EventHandler<MouseButtonEventArgs>(btnBack_Click);

            btnForward = new Button("btnForward");
            btnForward.Location = new Point(126, 160);
            btnForward.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnForward.Size = new System.Drawing.Size(64, 16);
            btnForward.Visible = true;
            btnForward.Text = "-->";
            btnForward.Click += new EventHandler<MouseButtonEventArgs>(btnForward_Click);


            btnEdit = new Button("btnEdit");
            btnEdit.Location = new Point(10, 190);
            btnEdit.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEdit.Size = new System.Drawing.Size(64, 16);
            btnEdit.Visible = true;
            btnEdit.Text = "Edit";
            btnEdit.Click += new EventHandler<MouseButtonEventArgs>(btnEdit_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Location = new Point(126, 190);
            btnCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnCancel.Size = new System.Drawing.Size(64, 16);
            btnCancel.Visible = true;
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            //btnAddNew = new Button("btnAddNew");
            //btnAddNew.Location = new Point();
            //btnAddNew.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            //btnAddNew.Size = new System.Drawing.Size(64, 16);
            //btnAddNew.Visible = true;
            //btnAddNew.Text = "Add New";
            //btnAddNew.Click += new EventHandler<MouseButtonEventArgs>(btnAddNew_Click);


            btnEditorCancel = new Button("btnEditorCancel");
            btnEditorCancel.Location = new Point(120, 215);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(20, 215);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Arrow Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 16);
            txtName.Font = Graphics.FontManager.LoadFont("Tahoma", 12);

            lblSprite = new Label("lblSprite");
            lblSprite.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSprite.Text = "Arrow Sprite:";
            lblSprite.AutoSize = true;
            lblSprite.Location = new Point(10, 36);

            pic = new PictureBox("pic");
            pic.Location = new Point(10, 48);
            pic.Size = new Size(32, 32);

            hsbPic = new HScrollBar("hsbPic");
            hsbPic.Maximum = MaxInfo.MAX_ARROWS;
            hsbPic.Location = new Point(10, 90);
            hsbPic.Size = new Size(200, 12);
            hsbPic.ValueChanged +=new EventHandler<ValueChangedEventArgs>(hsbPic_ValueChanged);

            lblRange = new Label("lblRange");
            lblRange.AutoSize = true;
            lblRange.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblRange.Location = new Point(10, 115);
            lblRange.Text = "Range: -1";

            hsbRange = new HScrollBar("hsbRange");
            hsbRange.Maximum = 50;
            hsbRange.Location = new Point(10, 140);
            hsbRange.Size = new Size(200, 12);
            hsbRange.ValueChanged += new EventHandler<ValueChangedEventArgs>(hsbRange_ValueChanged);

            lblAmount = new Label("lblAmount");
            lblAmount.AutoSize = true;
            lblAmount.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblAmount.Location = new Point(10, 165);
            lblAmount.Text = "Amount: -1";

            hsbAmount = new HScrollBar("hsbAmount");
            hsbAmount.Maximum = 15;
            hsbAmount.Location = new Point(10, 190);
            hsbAmount.Size = new Size(200, 12);
            hsbAmount.ValueChanged += new EventHandler<ValueChangedEventArgs>(hsbAmount_ValueChanged);

            pnlArrowList.AddWidget(lbxArrowList);
            pnlArrowList.AddWidget(btnBack);
            pnlArrowList.AddWidget(btnForward);
            //pnlArrowList.AddWidget(btnAddNew);
            pnlArrowList.AddWidget(btnEdit);
            pnlArrowList.AddWidget(btnCancel);

            pnlArrowEditor.AddWidget(lblName);
            pnlArrowEditor.AddWidget(txtName);
            pnlArrowEditor.AddWidget(lblSprite);
            pnlArrowEditor.AddWidget(pic);
            pnlArrowEditor.AddWidget(hsbPic);
            pnlArrowEditor.AddWidget(lblRange);
            pnlArrowEditor.AddWidget(hsbRange);
            pnlArrowEditor.AddWidget(lblAmount);
            pnlArrowEditor.AddWidget(hsbAmount);
            pnlArrowEditor.AddWidget(btnEditorCancel);
            pnlArrowEditor.AddWidget(btnEditorOK);

            this.AddWidget(pnlArrowList);
            this.AddWidget(pnlArrowEditor);

            RefreshArrowList();
            this.LoadComplete();
        }
        #endregion

        #region Methods

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshArrowList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
        	if (currentTen < ((MaxInfo.MAX_ARROWS - 1) / 10)) {
                currentTen++;
            }
            RefreshArrowList();
        }

        public void RefreshArrowList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MAX_ARROWS) {
                    ((ListBoxTextItem)lbxArrowList.Items[i]).Text = ((i + 10 * currentTen) + 1 + ": " + Arrows.ArrowHelper.Arrows[i + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxArrowList.Items[i]).Text = "---";
                }
            }
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxArrowList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxArrowList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    itemNum = index[0].ToInt() - 1;
                    Messenger.SendEditArrow(itemNum);
                    pnlArrowList.Visible = false;
                }
            }
        }

        public void DisplayArrowData() {
            // First, get the arrow instance based on the stored arrow index
            Arrows.Arrow arrow = Arrows.ArrowHelper.Arrows[itemNum];
            // Update the widgets with the arrow data
            txtName.Text = arrow.Name;
            //pic.Image = Tools.CropImage(Logic.Graphics.GraphicsManager.Arrows, new Rectangle(0, arrow.Pic * 32, 32, 32));
            hsbAmount.Value = arrow.Amount;
            hsbRange.Value = arrow.Range;

            pnlArrowEditor.Visible = true;
            this.Size = new System.Drawing.Size(pnlArrowEditor.Width, pnlArrowEditor.Height);
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            itemNum = -1;
            pnlArrowEditor.Visible = false;
            pnlArrowList.Visible = true;
            this.Size = new System.Drawing.Size(pnlArrowList.Width, pnlArrowList.Height);

        }

        void hsbRange_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (lblRange.Text != "Range: " + e.NewValue.ToString()) {
                lblRange.Text = "Range: " + e.NewValue.ToString();
            }
        }

        void hsbAmount_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (lblAmount.Text != "Amount: " + e.NewValue.ToString()) {
                lblAmount.Text = "Amount: " + e.NewValue.ToString();
            }
        }

        void hsbPic_ValueChanged(object sender, ValueChangedEventArgs e) {
            //pic.Image = Tools.CropImage(Logic.Graphics.GraphicsManager.Arrows, new Rectangle(0, hsbPic.Value * 32, 32, 32));
        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Arrows.Arrow arrowToSend = new Arrows.Arrow();
            arrowToSend.Name = txtName.Text;
            arrowToSend.Pic = hsbPic.Value;
            arrowToSend.Range = hsbRange.Value;
            arrowToSend.Amount = hsbAmount.Value;
            Messenger.SendSaveArrow(itemNum, arrowToSend);
            pnlArrowEditor.Visible = false;
            pnlArrowList.Visible = true;
            this.Size = new System.Drawing.Size(pnlArrowList.Width, pnlArrowList.Height);

        }
        #endregion Methods
    }
}
