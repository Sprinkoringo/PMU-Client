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
using Client.Logic.Graphics;
using SdlDotNet.Graphics;

namespace Client.Logic.Windows.Editors
{
    class winEvolutionPanel : Core.WindowCore
    {
        #region Fields
        int evoNum = -1;
        int currentTen = 0;
        Evolutions.Evolution evolution;

        Panel pnlEvoList;
        Panel pnlEvoEditor;

        ListBox lbxEvoList;
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        PictureBox picSprite;
        PictureBox picNewSprite;
        Label lblArrow;
        Label lblName;
        TextBox txtName;
        Label lblSprite;
        NumericUpDown nudSpecies;

        Label lblMaxBranchEvos;
        NumericUpDown nudMaxBranchEvos;

        Button btnEvoBranchLoad;
        Button btnEvoBranchSave;
        Label lblSaveLoadMessage;
        Label lblEvoNum;
        NumericUpDown nudEvoNum;
        Label lblNewName;
        TextBox txtNewName;
        Label lblNewSprite;
        NumericUpDown nudNewSpecies;
        Label lblReqScript;
        NumericUpDown nudReqScript;

        Label lblData1;
        NumericUpDown nudData1;
        Label lblData2;
        NumericUpDown nudData2;
        Label lblData3;
        NumericUpDown nudData3;



        #endregion

        #region Constructors
        public winEvolutionPanel()
            : base("winEvolutionPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Evolution Panel";

            pnlEvoList = new Panel("pnlEvoList");
            pnlEvoList.Size = new System.Drawing.Size(200, 230);
            pnlEvoList.Location = new Point(0, 0);
            pnlEvoList.BackColor = Color.White;
            pnlEvoList.Visible = true;

            pnlEvoEditor = new Panel("pnlEvoEditor");
            pnlEvoEditor.Size = new System.Drawing.Size(360, 400);
            pnlEvoEditor.Location = new Point(0, 0);
            pnlEvoEditor.BackColor = Color.White;
            pnlEvoEditor.Visible = false;


            lbxEvoList = new ListBox("lbxEvoList");
            lbxEvoList.Location = new Point(10, 10);
            lbxEvoList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {

                lbxEvoList.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Evolutions.EvolutionHelper.Evolutions[i].Name));
            }
            lbxEvoList.SelectItem(0);

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
            btnEditorCancel.Location = new Point(100, 354);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(10, 354);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);

            //PictureBox picSprite;
            picSprite = new PictureBox("picSprite");
            picSprite.Size = new Size(32, 32);
            picSprite.BackColor = Color.Transparent;
            picSprite.Image = new SdlDotNet.Graphics.Surface(32, 32);
            picSprite.X = 175 - picSprite.Width;
            picSprite.Y = 120 - picSprite.Height / 2;

            //PictureBox picNewSprite;
            picNewSprite = new PictureBox("picNewSprite");
            picNewSprite.Size = new Size(32, 32);
            picNewSprite.BackColor = Color.Transparent;
            picNewSprite.Image = new SdlDotNet.Graphics.Surface(32, 32);
            picNewSprite.X = 185;
            picNewSprite.Y = 120 - picNewSprite.Height / 2;

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 16);

            lblSprite = new Label("lblSprite");
            lblSprite.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSprite.Text = "Species:";
            lblSprite.AutoSize = true;
            lblSprite.Location = new Point(60, 36);

            nudSpecies = new NumericUpDown("nudSpecies");
            nudSpecies.Minimum = 0;
            nudSpecies.Maximum = Int32.MaxValue;
            nudSpecies.Size = new Size(80, 20);
            nudSpecies.Location = new Point(60, 50);
            nudSpecies.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudSpecies.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudSpecies_ValueChanged);



            lblMaxBranchEvos = new Label("lblMaxBranchEvos");
            lblMaxBranchEvos.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblMaxBranchEvos.Text = "Max Branch Evos:";
            lblMaxBranchEvos.AutoSize = true;
            lblMaxBranchEvos.Location = new Point(200, 36);

            nudMaxBranchEvos = new NumericUpDown("nudMaxBranchEvos");
            nudMaxBranchEvos.Minimum = 0;
            nudMaxBranchEvos.Maximum = Int32.MaxValue;
            nudMaxBranchEvos.Size = new Size(80, 20);
            nudMaxBranchEvos.Location = new Point(200, 50);
            nudMaxBranchEvos.Font = Graphics.FontManager.LoadFont("tahoma", 10);



            lblEvoNum = new Label("lblEvoNum");
            lblEvoNum.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblEvoNum.Text = "Evolution Branch #:";
            lblEvoNum.AutoSize = true;
            lblEvoNum.Location = new Point(30, 170);

            nudEvoNum = new NumericUpDown("nudEvoNum");
            nudEvoNum.Minimum = 1;
            nudEvoNum.Maximum = Int32.MaxValue;
            nudEvoNum.Size = new Size(80, 20);
            nudEvoNum.Location = new Point(30, 184);
            nudEvoNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);

            btnEvoBranchLoad = new Button("btnEvoBranchLoad");
            btnEvoBranchLoad.Location = new Point(150, 180);
            btnEvoBranchLoad.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEvoBranchLoad.Size = new System.Drawing.Size(80, 16);
            btnEvoBranchLoad.Visible = true;
            btnEvoBranchLoad.Text = "Load Branch";
            btnEvoBranchLoad.Click += new EventHandler<MouseButtonEventArgs>(btnEvoBranchLoad_Click);

            btnEvoBranchSave = new Button("btnEvoBranchSave");
            btnEvoBranchSave.Location = new Point(250, 180);
            btnEvoBranchSave.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEvoBranchSave.Size = new System.Drawing.Size(80, 16);
            btnEvoBranchSave.Visible = true;
            btnEvoBranchSave.Text = "Save Branch";
            btnEvoBranchSave.Click += new EventHandler<MouseButtonEventArgs>(btnEvoBranchSave_Click);

            lblSaveLoadMessage = new Label("lblSaveLoadMessage");
            lblSaveLoadMessage.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSaveLoadMessage.Text = "---";
            lblSaveLoadMessage.AutoSize = true;
            lblSaveLoadMessage.Location = new Point(70, 212);

            lblArrow = new Label("lblArrow");
            lblArrow.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblArrow.Text = "->";
            lblArrow.AutoSize = true;
            lblArrow.Location = new Point(176, 120);

            lblNewName = new Label("lblNewName");
            lblNewName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNewName.Text = "New Name:";
            lblNewName.AutoSize = true;
            lblNewName.Location = new Point(10, 234);

            txtNewName = new TextBox("txtNewName");
            txtNewName.Size = new Size(200, 16);
            txtNewName.Location = new Point(10, 250);

            lblNewSprite = new Label("lblNewSprite");
            lblNewSprite.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNewSprite.Text = "New Species:";
            lblNewSprite.AutoSize = true;
            lblNewSprite.Location = new Point(60, 270);

            nudNewSpecies = new NumericUpDown("nudNewSpecies");
            nudNewSpecies.Minimum = 0;
            nudNewSpecies.Maximum = Int32.MaxValue;
            nudNewSpecies.Size = new Size(80, 20);
            nudNewSpecies.Location = new Point(60, 286);
            nudNewSpecies.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudNewSpecies.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudNewSpecies_ValueChanged);


            lblReqScript = new Label("lblReqScript");
            lblReqScript.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblReqScript.Text = "Requirement Script:";
            lblReqScript.AutoSize = true;
            lblReqScript.Location = new Point(200, 270);

            nudReqScript = new NumericUpDown("nudReqScript");
            nudReqScript.Minimum = 0;
            nudReqScript.Maximum = Int32.MaxValue;
            nudReqScript.Size = new Size(80, 20);
            nudReqScript.Location = new Point(200, 286);
            nudReqScript.Font = Graphics.FontManager.LoadFont("tahoma", 10);


            lblData1 = new Label("lblData1");
            lblData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData1.Text = "Data 1:";
            lblData1.AutoSize = true;
            lblData1.Location = new Point(30, 306);

            nudData1 = new NumericUpDown("nudData1");
            nudData1.Minimum = Int32.MinValue;
            nudData1.Maximum = Int32.MaxValue;
            nudData1.Size = new Size(80, 20);
            nudData1.Location = new Point(30, 320);
            nudData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

            lblData2 = new Label("lblData2");
            lblData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData2.Text = "Data 2:";
            lblData2.AutoSize = true;
            lblData2.Location = new Point(140, 306);

            nudData2 = new NumericUpDown("nudData2");
            nudData2.Minimum = Int32.MinValue;
            nudData2.Maximum = Int32.MaxValue;
            nudData2.Size = new Size(80, 20);
            nudData2.Location = new Point(140, 320);
            nudData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

            lblData3 = new Label("lblData3");
            lblData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData3.Text = "Data 3:";
            lblData3.AutoSize = true;
            lblData3.Location = new Point(250, 306);

            nudData3 = new NumericUpDown("nudData3");
            nudData3.Minimum = Int32.MinValue;
            nudData3.Maximum = Int32.MaxValue;
            nudData3.Size = new Size(80, 20);
            nudData3.Location = new Point(250, 320);
            nudData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);





            #region AddWidget

            pnlEvoList.AddWidget(lbxEvoList);
            pnlEvoList.AddWidget(btnBack);
            pnlEvoList.AddWidget(btnForward);
            //pnlEvoList.AddWidget(btnAddNew);
            pnlEvoList.AddWidget(btnEdit);
            pnlEvoList.AddWidget(btnCancel);


            pnlEvoEditor.AddWidget(picSprite);
            pnlEvoEditor.AddWidget(picNewSprite);
            pnlEvoEditor.AddWidget(lblName);
            pnlEvoEditor.AddWidget(txtName);
            pnlEvoEditor.AddWidget(lblSprite);

            pnlEvoEditor.AddWidget(nudSpecies);

            pnlEvoEditor.AddWidget(lblMaxBranchEvos);
            pnlEvoEditor.AddWidget(nudMaxBranchEvos);
            //
            pnlEvoEditor.AddWidget(lblEvoNum);
            pnlEvoEditor.AddWidget(nudEvoNum);
            pnlEvoEditor.AddWidget(btnEvoBranchLoad);
            pnlEvoEditor.AddWidget(btnEvoBranchSave);
            pnlEvoEditor.AddWidget(lblSaveLoadMessage);
            pnlEvoEditor.AddWidget(lblArrow);
            pnlEvoEditor.AddWidget(lblNewName);
            pnlEvoEditor.AddWidget(txtNewName);
            pnlEvoEditor.AddWidget(lblNewSprite);
            pnlEvoEditor.AddWidget(nudNewSpecies);
            pnlEvoEditor.AddWidget(lblReqScript);
            pnlEvoEditor.AddWidget(lblData1);
            pnlEvoEditor.AddWidget(lblData2);
            pnlEvoEditor.AddWidget(lblData3);
            pnlEvoEditor.AddWidget(nudReqScript);
            pnlEvoEditor.AddWidget(nudData1);
            pnlEvoEditor.AddWidget(nudData2);
            pnlEvoEditor.AddWidget(nudData3);

            pnlEvoEditor.AddWidget(btnEditorCancel);
            pnlEvoEditor.AddWidget(btnEditorOK);


            this.AddWidget(pnlEvoList);
            this.AddWidget(pnlEvoEditor);

            this.LoadComplete();

            #endregion


        }

        #endregion

        #region Methods

        public void LoadEvo(string[] parse) {


            evolution = new Evolutions.Evolution();
            evolution.Name = parse[1];
            evolution.Species = parse[2].ToInt();

            for (int i = 0; i < parse[3].ToInt(); i++) {
                evolution.Branches.Add(new Evolutions.EvolutionBranch());
                evolution.Branches[i].Name = parse[4 + i * 6];
                evolution.Branches[i].NewSpecies = parse[5 + i * 6].ToInt();
                evolution.Branches[i].ReqScript = parse[6 + i * 6].ToInt();
                evolution.Branches[i].Data1 = parse[7 + i * 6].ToInt();
                evolution.Branches[i].Data2 = parse[8 + i * 6].ToInt();
                evolution.Branches[i].Data3 = parse[9 + i * 6].ToInt();

            }

            pnlEvoList.Visible = false;
            pnlEvoEditor.Visible = true;
            this.Size = new System.Drawing.Size(pnlEvoEditor.Width, pnlEvoEditor.Height);

            txtName.Text = evolution.Name;
            nudSpecies.Value = evolution.Species;
            nudMaxBranchEvos.Value = evolution.Branches.Count;


            btnEdit.Text = "Edit";



        }


        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshEvoList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxEvolutions / 10)) {
                currentTen++;
            }
            RefreshEvoList();
        }

        public void RefreshEvoList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxEvolutions) {
                    ((ListBoxTextItem)lbxEvoList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Evolutions.EvolutionHelper.Evolutions[i + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxEvoList.Items[i]).Text = "---";
                }
            }
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string[] index = ((ListBoxTextItem)lbxEvoList.SelectedItems[0]).Text.Split(':');
            if (index[0].IsNumeric()) {

                evoNum = index[0].ToInt() - 1;
                btnEdit.Text = "Loading...";


                Messenger.SendEditEvo(index[0].ToInt() - 1);

            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            evoNum = -1;
            evolution = null;
            pnlEvoEditor.Visible = false;
            pnlEvoList.Visible = true;
            this.Size = new System.Drawing.Size(pnlEvoList.Width, pnlEvoList.Height);

        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            evolution.Name = txtName.Text;
            evolution.Species = nudSpecies.Value;

            Messenger.SendSaveEvo(evoNum, evolution, nudMaxBranchEvos.Value);

            evoNum = -1;
            evolution = null;
            pnlEvoEditor.Visible = false;
            pnlEvoList.Visible = true;
            this.Size = new System.Drawing.Size(pnlEvoList.Width, pnlEvoList.Height);
        }

        void btnEvoBranchLoad_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //if evo num is higher than max branches
            if (nudEvoNum.Value > nudMaxBranchEvos.Value) {
                lblSaveLoadMessage.Text = "Cannot load branch higher than maximum!";
                return;

            }

            //if stored evo count is lower than max branches
            for (int i = evolution.Branches.Count; i < nudMaxBranchEvos.Value; i++) {
                evolution.Branches.Add(new Evolutions.EvolutionBranch());
            }

            txtNewName.Text = evolution.Branches[nudEvoNum.Value - 1].Name;
            nudNewSpecies.Value = evolution.Branches[nudEvoNum.Value - 1].NewSpecies;
            nudReqScript.Value = evolution.Branches[nudEvoNum.Value - 1].ReqScript;
            nudData1.Value = evolution.Branches[nudEvoNum.Value - 1].Data1;
            nudData2.Value = evolution.Branches[nudEvoNum.Value - 1].Data2;
            nudData3.Value = evolution.Branches[nudEvoNum.Value - 1].Data3;

            lblSaveLoadMessage.Text = "Branch loaded.";
        }

        void btnEvoBranchSave_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (nudEvoNum.Value > nudMaxBranchEvos.Value) {
                lblSaveLoadMessage.Text = "Cannot save branch higher than maximum.";
                return;
            }

            for (int i = evolution.Branches.Count; i < nudMaxBranchEvos.Value; i++) {
                evolution.Branches.Add(new Evolutions.EvolutionBranch());
            }

            evolution.Branches[nudEvoNum.Value - 1].Name = txtNewName.Text;
            evolution.Branches[nudEvoNum.Value - 1].NewSpecies = nudNewSpecies.Value;
            evolution.Branches[nudEvoNum.Value - 1].ReqScript = nudReqScript.Value;
            evolution.Branches[nudEvoNum.Value - 1].Data1 = nudData1.Value;
            evolution.Branches[nudEvoNum.Value - 1].Data2 = nudData2.Value;
            evolution.Branches[nudEvoNum.Value - 1].Data3 = nudData3.Value;

            lblSaveLoadMessage.Text = "Branch saved.";
        }

        void nudSpecies_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            Surface sprite = GraphicsManager.GetSpriteSheet(nudSpecies.Value).GetSheet(FrameType.Idle, Enums.Direction.Down);
            picSprite.X = 175 - sprite.Width / 14;
            picSprite.Y = 120 - sprite.Height / 2;
            picSprite.Size = new Size(sprite.Width / 14, sprite.Height);

            picSprite.Image = sprite.CreateSurfaceFromClipRectangle(new Rectangle(new Point(3 * picSprite.Width, 0), picSprite.Size));
            //Graphics.Renderers.RenderLocation loc = new Graphics.Renderers.RenderLocation(new Graphics.Renderers.RendererDestinationData(picSprite.Image, new Point(0, 0), picSprite.Size), new Point(0, 0));

            //loc.DestinationSurface.Blit(sprite, loc.DestinationPoint, new Rectangle(new Point(3 * picSprite.Width, 0), picSprite.Size));
            //picSprite.SelectiveDrawBuffer();
            //picSprite.Image = Tools.CropImage(sprite, new Rectangle(new Point(3 * picSprite.Width / 14, 0), picSprite.Size));
        }

        void nudNewSpecies_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            Surface sprite = GraphicsManager.GetSpriteSheet(nudSpecies.Value).GetSheet(FrameType.Idle, Enums.Direction.Down);
            picNewSprite.X = 185;
            picNewSprite.Y = 120 - sprite.Height / 2;
            picNewSprite.Size = new Size(sprite.Width / 14, sprite.Height);

            picNewSprite.Image = sprite.CreateSurfaceFromClipRectangle(new Rectangle(new Point(3 * picNewSprite.Width, 0), picNewSprite.Size));
        }

        #endregion
    }
}
