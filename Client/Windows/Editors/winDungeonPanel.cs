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
    class winDungeonPanel : Core.WindowCore
    {
        #region Fields

        int dungeonNum = 0;
        int currentTen = 0;
        Logic.Editors.Dungeons.EditableDungeon dungeon;

        Panel pnlDungeonList;
        Panel pnlDungeonEditor;

        #region DungeonList
        ListBox lbxDungeonList;
        Button btnAddNew;
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;
        #endregion

        #region DungeonEditor
        Button btnGeneral;
        Button btnScripts;
        Button btnStandardMaps;
        Button btnRandomMaps;
        Panel pnlDungeonGeneral;
        Panel pnlDungeonScripts;
        Panel pnlDungeonStandardMaps;
        Panel pnlDungeonRandomMaps;
        #endregion

        #region General
        Label lblName;
        TextBox txtName;
        CheckBox chkRescue;
        Button btnEditorCancel;
        Button btnEditorOK;

        #endregion

        #region Scripts
        ListBox lbxDungeonScripts;
        Label lblScriptNum;
        NumericUpDown nudScriptNum;
        Label lblScriptParam;
        TextBox txtScriptParam;
        Button btnAddScript;
        Button btnLoadScript;
        Button btnRemoveScript;

        #endregion

        #region StandardMaps
        ListBox lbxDungeonSMaps;
        Label lblMapNum;
        NumericUpDown nudMapNum;
        Label lblSMapDifficulty;
        NumericUpDown nudSMapDifficulty;
        CheckBox chkSMapBad;
        Button btnAddSMap;
        Button btnLoadSMap;
        Button btnRemoveSMap;

        #endregion

        #region RandomMaps
        ListBox lbxDungeonRMaps;
        Label lblRDungeonIndex;
        NumericUpDown nudRDungeonIndex;
        Label lblRDungeonFloor;
        NumericUpDown nudRDungeonFloor;
        Label lblRDungeonFloorEnd;
        NumericUpDown nudRDungeonFloorEnd;
        Label lblRMapDifficulty;
        NumericUpDown nudRMapDifficulty;
        CheckBox chkRMapBad;
        Button btnAddRMap;
        Button btnLoadRMap;
        Button btnRemoveRMap;

        #endregion


        #endregion

        #region Constructors
        public winDungeonPanel()
            : base("winDungeonPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Dungeon Panel";

            #region Panels
            pnlDungeonList = new Panel("pnlDungeonList");
            pnlDungeonList.Size = new System.Drawing.Size(200, 230);
            pnlDungeonList.Location = new Point(0, 0);
            pnlDungeonList.BackColor = Color.White;
            pnlDungeonList.Visible = true;

            pnlDungeonEditor = new Panel("pnlDungeonEditor");
            pnlDungeonEditor.Size = new System.Drawing.Size(320, 300);
            pnlDungeonEditor.Location = new Point(0, 0);
            pnlDungeonEditor.BackColor = Color.White;
            pnlDungeonEditor.Visible = false;

            pnlDungeonGeneral = new Panel("pnlDungeonGeneral");
            pnlDungeonGeneral.Size = new System.Drawing.Size(320, 270);
            pnlDungeonGeneral.Location = new Point(0, 30);
            pnlDungeonGeneral.BackColor = Color.White;
            pnlDungeonGeneral.Visible = true;

            pnlDungeonScripts = new Panel("pnlDungeonScripts");
            pnlDungeonScripts.Size = new System.Drawing.Size(320, 270);
            pnlDungeonScripts.Location = new Point(0, 30);
            pnlDungeonScripts.BackColor = Color.White;
            pnlDungeonScripts.Visible = false;

            pnlDungeonStandardMaps = new Panel("pnlDungeonStandardMaps");
            pnlDungeonStandardMaps.Size = new System.Drawing.Size(320, 270);
            pnlDungeonStandardMaps.Location = new Point(0, 30);
            pnlDungeonStandardMaps.BackColor = Color.White;
            pnlDungeonStandardMaps.Visible = false;

            pnlDungeonRandomMaps = new Panel("pnlDungeonRandomMaps");
            pnlDungeonRandomMaps.Size = new System.Drawing.Size(320, 270);
            pnlDungeonRandomMaps.Location = new Point(0, 30);
            pnlDungeonRandomMaps.BackColor = Color.White;
            pnlDungeonRandomMaps.Visible = false;
            #endregion

            #region Dungeon List
            lbxDungeonList = new ListBox("lbxDungeonList");
            lbxDungeonList.Location = new Point(10, 10);
            lbxDungeonList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                ListBoxTextItem lbiDungeon = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": ");
                lbxDungeonList.Items.Add(lbiDungeon);
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

            btnAddNew = new Button("btnAddNew");
            btnAddNew.Location = new Point(76, 190);
            btnAddNew.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddNew.Size = new System.Drawing.Size(48, 16);
            btnAddNew.Visible = true;
            btnAddNew.Text = "New";
            btnAddNew.Click += new EventHandler<MouseButtonEventArgs>(btnAddNew_Click);
            #endregion

            #region Dungeon Editor Panel

            btnGeneral = new Button("btnGeneral");
            btnGeneral.Location = new Point(5, 5);
            btnGeneral.Size = new Size(70, 20);
            btnGeneral.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnGeneral.Text = "General";
            btnGeneral.Click += new EventHandler<MouseButtonEventArgs>(btnGeneral_Click);

            btnStandardMaps = new Button("btnStandardMaps");
            btnStandardMaps.Location = new Point(80, 5);
            btnStandardMaps.Size = new Size(70, 20);
            btnStandardMaps.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnStandardMaps.Text = "Standard Maps";
            btnStandardMaps.Click += new EventHandler<MouseButtonEventArgs>(btnStandardMaps_Click);

            btnRandomMaps = new Button("btnRandomMaps");
            btnRandomMaps.Location = new Point(155, 5);
            btnRandomMaps.Size = new Size(70, 20);
            btnRandomMaps.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRandomMaps.Text = "Random Maps";
            btnRandomMaps.Click += new EventHandler<MouseButtonEventArgs>(btnRandomMaps_Click);

            btnScripts = new Button("btnScripts");
            btnScripts.Location = new Point(230, 5);
            btnScripts.Size = new Size(70, 20);
            btnScripts.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnScripts.Text = "Scripts";
            btnScripts.Click += new EventHandler<MouseButtonEventArgs>(btnScripts_Click);

            #endregion

            #region General

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblName.Text = "Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 24);
            txtName.Font = Graphics.FontManager.LoadFont("tahoma", 12);

            chkRescue = new CheckBox("chkRescue");
            chkRescue.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            chkRescue.Size = new Size(140, 14);
            chkRescue.Location = new Point(10, 48);
            chkRescue.Text = "Allow Rescue";

            btnEditorCancel = new Button("btnEditorCancel");
            btnEditorCancel.Location = new Point(120, 75);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(20, 75);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);

            #endregion

            #region Scripts

            lblScriptNum = new Label("lblScriptNum");
            lblScriptNum.AutoSize = true;
            lblScriptNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblScriptNum.Location = new Point(10, 0);
            lblScriptNum.Text = "Num:";

            nudScriptNum = new NumericUpDown("nudScriptNum");
            nudScriptNum.Size = new Size(70, 20);
            nudScriptNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudScriptNum.Maximum = Int32.MaxValue;
            nudScriptNum.Minimum = Int32.MinValue;
            nudScriptNum.Location = new Point(10, 14);

            lblScriptParam = new Label("lblScriptParam");
            lblScriptParam.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblScriptParam.Text = "Parameter String:";
            lblScriptParam.AutoSize = true;
            lblScriptParam.Location = new Point(10, 34);

            txtScriptParam = new TextBox("txtScriptParam");
            txtScriptParam.Size = new Size(200, 16);
            txtScriptParam.Location = new Point(10, 46);
            txtScriptParam.Font = Graphics.FontManager.LoadFont("tahoma", 12);

            btnAddScript = new Button("btnAddScript");
            btnAddScript.Size = new Size(70, 16);
            btnAddScript.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddScript.Location = new Point(5, 72);
            btnAddScript.Text = "Add";
            btnAddScript.Click += new EventHandler<MouseButtonEventArgs>(btnAddScript_Click);

            btnRemoveScript = new Button("btnRemoveScript");
            btnRemoveScript.Size = new Size(70, 16);
            btnRemoveScript.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveScript.Location = new Point(80, 72);
            btnRemoveScript.Text = "Remove";
            btnRemoveScript.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveScript_Click);

            btnLoadScript = new Button("btnLoadScript");
            btnLoadScript.Size = new Size(70, 16);
            btnLoadScript.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadScript.Location = new Point(155, 72);
            btnLoadScript.Text = "Load";
            btnLoadScript.Click += new EventHandler<MouseButtonEventArgs>(btnLoadScript_Click);
            
            lbxDungeonScripts = new ListBox("lbxDungeonScripts");
            lbxDungeonScripts.Location = new Point(10, 90);
            lbxDungeonScripts.Size = new Size(pnlDungeonStandardMaps.Size.Width - 20, pnlDungeonStandardMaps.Size.Height - 120);
            lbxDungeonScripts.MultiSelect = false;

            #endregion

            #region Standard Maps

            lblMapNum = new Label("lblMapNum");
            lblMapNum.AutoSize = true;
            lblMapNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblMapNum.Location = new Point(10, 0);
            lblMapNum.Text = "Num:";

            nudMapNum = new NumericUpDown("nudMapNum");
            nudMapNum.Size = new Size(70, 20);
            nudMapNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudMapNum.Maximum = 2000;
            nudMapNum.Minimum = 1;
            nudMapNum.Location = new Point(10, 14);

            lblSMapDifficulty = new Label("lblSMapDifficulty");
            lblSMapDifficulty.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblSMapDifficulty.Text = "Difficulty:";
            lblSMapDifficulty.AutoSize = true;
            lblSMapDifficulty.Location = new Point(10, 34);

            nudSMapDifficulty = new NumericUpDown("nudSMapDifficulty");
            nudSMapDifficulty.Size = new Size(50, 20);
            nudSMapDifficulty.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudSMapDifficulty.Location = new Point(10, 48);
            nudSMapDifficulty.Minimum = 1;
            nudSMapDifficulty.Maximum = 16;

            chkSMapBad = new CheckBox("chkSMap");
            chkSMapBad.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkSMapBad.Size = new Size(120, 14);
            chkSMapBad.Location = new Point(100, 40);
            chkSMapBad.Text = "Boss Goal Map";

            btnAddSMap = new Button("btnAddSMap");
            btnAddSMap.Size = new Size(70, 16);
            btnAddSMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddSMap.Location = new Point(5, 72);
            btnAddSMap.Text = "Add";
            btnAddSMap.Click += new EventHandler<MouseButtonEventArgs>(btnAddSMap_Click);

            btnRemoveSMap = new Button("btnRemoveSMap");
            btnRemoveSMap.Size = new Size(70, 16);
            btnRemoveSMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveSMap.Location = new Point(80, 72);
            btnRemoveSMap.Text = "Remove";
            btnRemoveSMap.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveSMap_Click);

            btnLoadSMap = new Button("btnLoadSMap");
            btnLoadSMap.Size = new Size(70, 16);
            btnLoadSMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadSMap.Location = new Point(155, 72);
            btnLoadSMap.Text = "Load";
            btnLoadSMap.Click += new EventHandler<MouseButtonEventArgs>(btnLoadSMap_Click);

            lbxDungeonSMaps = new ListBox("lbxDungeonSMaps");
            lbxDungeonSMaps.Location = new Point(10, 90);
            lbxDungeonSMaps.Size = new Size(pnlDungeonStandardMaps.Size.Width - 20, pnlDungeonStandardMaps.Size.Height - 120);
            lbxDungeonSMaps.MultiSelect = false;

            #endregion

            #region Random Maps

            lblRDungeonIndex = new Label("lblRDungeonIndex");
            lblRDungeonIndex.AutoSize = true;
            lblRDungeonIndex.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblRDungeonIndex.Location = new Point(2, 0);
            lblRDungeonIndex.Text = "Dungeon #:";

            nudRDungeonIndex = new NumericUpDown("nudRDungeonIndex");
            nudRDungeonIndex.Size = new Size(70, 20);
            nudRDungeonIndex.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudRDungeonIndex.Maximum = MaxInfo.MaxRDungeons;
            nudRDungeonIndex.Minimum = 1;
            nudRDungeonIndex.Location = new Point(10, 14);
            nudRDungeonIndex.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudRDungeonIndex_ValueChanged);

            lblRDungeonFloor = new Label("lblRDungeonFloor");
            lblRDungeonFloor.AutoSize = true;
            lblRDungeonFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblRDungeonFloor.Location = new Point(240, 0);
            lblRDungeonFloor.Text = "From Floor:";

            nudRDungeonFloor = new NumericUpDown("nudRDungeonFloor");
            nudRDungeonFloor.Size = new Size(70, 20);
            nudRDungeonFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudRDungeonFloor.Maximum = Int32.MaxValue;
            nudRDungeonFloor.Minimum = 1;
            nudRDungeonFloor.Location = new Point(240, 14);
            nudRDungeonFloor.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudRDungeonFloor_ValueChanged);

            lblRDungeonFloorEnd = new Label("lblRDungeonFloorEnd");
            lblRDungeonFloorEnd.AutoSize = true;
            lblRDungeonFloorEnd.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblRDungeonFloorEnd.Location = new Point(240, 34);
            lblRDungeonFloorEnd.Text = "To Floor:";

            nudRDungeonFloorEnd = new NumericUpDown("nudRDungeonFloorEnd");
            nudRDungeonFloorEnd.Size = new Size(70, 20);
            nudRDungeonFloorEnd.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudRDungeonFloorEnd.Maximum = Int32.MaxValue;
            nudRDungeonFloorEnd.Minimum = 1;
            nudRDungeonFloorEnd.Location = new Point(240, 48);
            nudRDungeonFloorEnd.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudRDungeonFloorEnd_ValueChanged);

            lblRMapDifficulty = new Label("lblRMapDifficulty");
            lblRMapDifficulty.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblRMapDifficulty.Text = "Difficulty:";
            lblRMapDifficulty.AutoSize = true;
            lblRMapDifficulty.Location = new Point(10, 34);

            nudRMapDifficulty = new NumericUpDown("nudRMapDifficulty");
            nudRMapDifficulty.Size = new Size(50, 20);
            nudRMapDifficulty.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudRMapDifficulty.Location = new Point(10, 48);
            nudRMapDifficulty.Minimum = 1;
            nudRMapDifficulty.Maximum = 16;

            chkRMapBad = new CheckBox("chkRMap");
            chkRMapBad.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkRMapBad.Size = new Size(120, 14);
            chkRMapBad.Location = new Point(100, 50);
            chkRMapBad.Text = "Boss Goal Map";

            btnAddRMap = new Button("btnAddRMap");
            btnAddRMap.Size = new Size(70, 16);
            btnAddRMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddRMap.Location = new Point(5, 72);
            btnAddRMap.Text = "Add";
            btnAddRMap.Click += new EventHandler<MouseButtonEventArgs>(btnAddRMap_Click);

            btnRemoveRMap = new Button("btnRemoveRMap");
            btnRemoveRMap.Size = new Size(70, 16);
            btnRemoveRMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveRMap.Location = new Point(80, 72);
            btnRemoveRMap.Text = "Remove";
            btnRemoveRMap.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveRMap_Click);

            btnLoadRMap = new Button("btnLoadRMap");
            btnLoadRMap.Size = new Size(70, 16);
            btnLoadRMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadRMap.Location = new Point(155, 72);
            btnLoadRMap.Text = "Load";
            btnLoadRMap.Click += new EventHandler<MouseButtonEventArgs>(btnLoadRMap_Click);

            lbxDungeonRMaps = new ListBox("lbxDungeonRMaps");
            lbxDungeonRMaps.Location = new Point(10, 90);
            lbxDungeonRMaps.Size = new Size(pnlDungeonRandomMaps.Size.Width - 20, pnlDungeonRandomMaps.Size.Height - 120);
            lbxDungeonRMaps.MultiSelect = false;

            #endregion

            #region Addwidget
            //Dungeon List
            pnlDungeonList.AddWidget(lbxDungeonList);
            pnlDungeonList.AddWidget(btnBack);
            pnlDungeonList.AddWidget(btnForward);
            pnlDungeonList.AddWidget(btnAddNew);
            pnlDungeonList.AddWidget(btnEdit);
            pnlDungeonList.AddWidget(btnCancel);
            //General
            pnlDungeonGeneral.AddWidget(lblName);
            pnlDungeonGeneral.AddWidget(txtName);
            pnlDungeonGeneral.AddWidget(chkRescue);
            pnlDungeonGeneral.AddWidget(btnEditorCancel);
            pnlDungeonGeneral.AddWidget(btnEditorOK);
            //Scripts
            pnlDungeonScripts.AddWidget(lblScriptNum);
            pnlDungeonScripts.AddWidget(nudScriptNum);
            pnlDungeonScripts.AddWidget(lblScriptParam);
            pnlDungeonScripts.AddWidget(txtScriptParam);
            pnlDungeonScripts.AddWidget(btnAddScript);
            pnlDungeonScripts.AddWidget(btnRemoveScript);
            pnlDungeonScripts.AddWidget(btnLoadScript);
            pnlDungeonScripts.AddWidget(lbxDungeonScripts);
            //Standard Maps
            pnlDungeonStandardMaps.AddWidget(lblMapNum);
            pnlDungeonStandardMaps.AddWidget(nudMapNum);
            pnlDungeonStandardMaps.AddWidget(lblSMapDifficulty);
            pnlDungeonStandardMaps.AddWidget(nudSMapDifficulty);
            pnlDungeonStandardMaps.AddWidget(chkSMapBad);
            pnlDungeonStandardMaps.AddWidget(btnAddSMap);
            pnlDungeonStandardMaps.AddWidget(btnRemoveSMap);
            pnlDungeonStandardMaps.AddWidget(btnLoadSMap);
            pnlDungeonStandardMaps.AddWidget(lbxDungeonSMaps);
            //RDungeon Maps
            pnlDungeonRandomMaps.AddWidget(lblRDungeonIndex);
            pnlDungeonRandomMaps.AddWidget(nudRDungeonIndex);
            pnlDungeonRandomMaps.AddWidget(lblRDungeonFloor);
            pnlDungeonRandomMaps.AddWidget(nudRDungeonFloor);
            pnlDungeonRandomMaps.AddWidget(lblRDungeonFloorEnd);
            pnlDungeonRandomMaps.AddWidget(nudRDungeonFloorEnd);
            pnlDungeonRandomMaps.AddWidget(lblRMapDifficulty);
            pnlDungeonRandomMaps.AddWidget(nudRMapDifficulty);
            pnlDungeonRandomMaps.AddWidget(chkRMapBad);
            pnlDungeonRandomMaps.AddWidget(btnAddRMap);
            pnlDungeonRandomMaps.AddWidget(btnRemoveRMap);
            pnlDungeonRandomMaps.AddWidget(btnLoadRMap);
            pnlDungeonRandomMaps.AddWidget(lbxDungeonRMaps);
            //Editor panel
            pnlDungeonEditor.AddWidget(btnGeneral);
            pnlDungeonEditor.AddWidget(btnStandardMaps);
            pnlDungeonEditor.AddWidget(btnRandomMaps);
            pnlDungeonEditor.AddWidget(btnScripts);
            pnlDungeonEditor.AddWidget(pnlDungeonGeneral);
            pnlDungeonEditor.AddWidget(pnlDungeonStandardMaps);
            pnlDungeonEditor.AddWidget(pnlDungeonRandomMaps);
            pnlDungeonEditor.AddWidget(pnlDungeonScripts);
            //This
            this.AddWidget(pnlDungeonList);
            this.AddWidget(pnlDungeonEditor);
            #endregion

            RefreshDungeonList();
        }

        #endregion



        
        #region Methods

        #region Dungeon List
        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshDungeonList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxDungeons / 10)) {
                currentTen++;
            }
            RefreshDungeonList();
        }

        void btnAddNew_Click(object sender, MouseButtonEventArgs e)
        {
            Messenger.SendAddDungeon();
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            if (lbxDungeonList.SelectedItems.Count == 1)
            {
                string[] index = ((ListBoxTextItem)lbxDungeonList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric())
                {
                    dungeonNum = index[0].ToInt() - 1;
                    btnEdit.Text = "Loading...";

                    Messenger.SendEditDungeon(dungeonNum);
                }
            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            this.Close();
            return;
        }

        public void RefreshDungeonList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxDungeons) {
                    ((ListBoxTextItem)lbxDungeonList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Dungeons.DungeonHelper.Dungeons[(i) + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxDungeonList.Items[i]).Text = "---";
                }
            }
        }

        #endregion

        #region Editor
        public void LoadDungeon(string[] parse) {
            this.Size = pnlDungeonEditor.Size;
            pnlDungeonList.Visible = false;
            pnlDungeonEditor.Visible = true;
            btnGeneral_Click(null, null);
            lbxDungeonSMaps.Items.Clear();
            lbxDungeonRMaps.Items.Clear();
            lbxDungeonScripts.Items.Clear();
            //this.Size = new System.Drawing.Size(pnlDungeonGeneral.Width, pnlDungeonGeneral.Height);

            dungeon = new Logic.Editors.Dungeons.EditableDungeon();

            dungeon.Name = parse[2];
            txtName.Text = dungeon.Name;
            dungeon.AllowsRescue = parse[3].ToBool();
            chkRescue.Checked = dungeon.AllowsRescue;
            int scriptCount = parse[4].ToInt();
            int n = 5;
            for (int i = 0; i < scriptCount; i++) {
                dungeon.ScriptList.Add(parse[n].ToInt(), parse[n + 1]);

                n += 2;

                ListBoxTextItem lbiScript = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), dungeon.ScriptList.KeyByIndex(i) + ": " + dungeon.ScriptList.ValueByIndex(i));
                lbxDungeonScripts.Items.Add(lbiScript);

            }

            int mapCount = parse[n].ToInt();
            n++;
            
            for (int i = 0; i < mapCount; i++) {
                Logic.Editors.Dungeons.EditableStandardDungeonMap map = new Logic.Editors.Dungeons.EditableStandardDungeonMap();
                map.Difficulty = (Enums.JobDifficulty)parse[n].ToInt();
                map.IsBadGoalMap = parse[n + 1].ToBool();
                map.MapNum = parse[n + 2].ToInt();

                dungeon.StandardMaps.Add(map);

                string mapText;
                if (map.IsBadGoalMap) {
                    mapText = (i + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Map #" + map.MapNum;
                } else {
                    mapText = (i + 1) + ": [" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Map #" + map.MapNum;
                }
                ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                lbxDungeonSMaps.Items.Add(lbiMap);

                n += 3;
            }
            mapCount = parse[n].ToInt();
            n++;
            for (int i = 0; i < mapCount; i++)
            {
                Logic.Editors.Dungeons.EditableRandomDungeonMap map = new Logic.Editors.Dungeons.EditableRandomDungeonMap();
                map.Difficulty = (Enums.JobDifficulty)parse[n].ToInt();
                map.IsBadGoalMap = parse[n + 1].ToBool();
                map.RDungeonIndex = parse[n + 2].ToInt();
                map.RDungeonFloor = parse[n + 3].ToInt();
                dungeon.RandomMaps.Add(map);

                n += 4;
                string mapText;
                if (map.IsBadGoalMap) {
                    mapText = (i + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Dun #" + (map.RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[map.RDungeonIndex].Name + ") " + (map.RDungeonFloor + 1) + "F";
                } else {
                    mapText = (i + 1) + ": [" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Dun #" + (map.RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[map.RDungeonIndex].Name + ") " + (map.RDungeonFloor + 1) + "F";
                }
                ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                lbxDungeonRMaps.Items.Add(lbiMap);
            }


            

            btnEdit.Text = "Edit";
        }



        void btnGeneral_Click(object sender, MouseButtonEventArgs e)
        {
            if (!btnGeneral.Selected)
            {
                btnGeneral.Selected = true;
                btnStandardMaps.Selected = false;
                btnRandomMaps.Selected = false;
                btnScripts.Selected = false;
                pnlDungeonGeneral.Visible = true;
                pnlDungeonStandardMaps.Visible = false;
                pnlDungeonRandomMaps.Visible = false;
                pnlDungeonScripts.Visible = false;
                this.TitleBar.Text = "Dungeon Editor - General";
            }
        }

        void btnStandardMaps_Click(object sender, MouseButtonEventArgs e)
        {
            if (!btnStandardMaps.Selected)
            {
                btnGeneral.Selected = false;
                btnStandardMaps.Selected = true;
                btnRandomMaps.Selected = false;
                btnScripts.Selected = false;
                pnlDungeonGeneral.Visible = false;
                pnlDungeonStandardMaps.Visible = true;
                pnlDungeonRandomMaps.Visible = false;
                pnlDungeonScripts.Visible = false;
                this.TitleBar.Text = "Dungeon Editor - Standard Maps";
            }
        }

        void btnRandomMaps_Click(object sender, MouseButtonEventArgs e)
        {
            if (!btnRandomMaps.Selected)
            {
                btnGeneral.Selected = false;
                btnStandardMaps.Selected = false;
                btnRandomMaps.Selected = true;
                btnScripts.Selected = false;
                pnlDungeonGeneral.Visible = false;
                pnlDungeonStandardMaps.Visible = false;
                pnlDungeonRandomMaps.Visible = true;
                pnlDungeonScripts.Visible = false;
                this.TitleBar.Text = "Dungeon Editor - Random Maps";
            }
        }

        void btnScripts_Click(object sender, MouseButtonEventArgs e) {
            if (!btnScripts.Selected) {
                btnGeneral.Selected = false;
                btnStandardMaps.Selected = false;
                btnRandomMaps.Selected = false;
                btnScripts.Selected = true;
                pnlDungeonGeneral.Visible = false;
                pnlDungeonStandardMaps.Visible = false;
                pnlDungeonRandomMaps.Visible = false;
                pnlDungeonScripts.Visible = true;
                this.TitleBar.Text = "Dungeon Editor - Scripts";
            }
        }

        #endregion

        #region General

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            dungeonNum = -1;
            pnlDungeonEditor.Visible = false;
            pnlDungeonList.Visible = true;
            this.Size = new System.Drawing.Size(pnlDungeonList.Width, pnlDungeonList.Height);
            this.TitleBar.Text = "Dungeon Panel";
        }


        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            dungeon.Name = txtName.Text;
            dungeon.AllowsRescue = chkRescue.Checked;
            Messenger.SendSaveDungeon(dungeonNum, dungeon);
            
            dungeonNum = -1;
            pnlDungeonEditor.Visible = false;
            pnlDungeonList.Visible = true;
            this.Size = new System.Drawing.Size(pnlDungeonList.Width, pnlDungeonList.Height);

        }

        #endregion

        #region Script

        void btnRemoveScript_Click(object sender, MouseButtonEventArgs e) {

            if (lbxDungeonScripts.SelectedIndex > -1) {
                dungeon.ScriptList.RemoveAt(lbxDungeonScripts.SelectedIndex);
                lbxDungeonScripts.Items.Clear();
                for (int scripts = 0; scripts < dungeon.ScriptList.Count; scripts++) {
                    ListBoxTextItem lbiScript = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), dungeon.ScriptList.KeyByIndex(scripts) + ": " + dungeon.ScriptList.ValueByIndex(scripts));
                    lbxDungeonScripts.Items.Add(lbiScript);
                }
            }
        }

        void btnAddScript_Click(object sender, MouseButtonEventArgs e) {
            if (dungeon.ScriptList.ContainsKey(nudScriptNum.Value)) {
                dungeon.ScriptList.RemoveAtKey(nudScriptNum.Value);
            }
            dungeon.ScriptList.Add(nudScriptNum.Value, txtScriptParam.Text);
            lbxDungeonScripts.Items.Clear();
            for (int scripts = 0; scripts < dungeon.ScriptList.Count; scripts++) {
                ListBoxTextItem lbiScript = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), dungeon.ScriptList.KeyByIndex(scripts) + ": " + dungeon.ScriptList.ValueByIndex(scripts));
                lbxDungeonScripts.Items.Add(lbiScript);
            }


        }

        void btnLoadScript_Click(object sender, MouseButtonEventArgs e) {
            if (lbxDungeonScripts.SelectedIndex > -1) {
                nudScriptNum.Value = dungeon.ScriptList.KeyByIndex(lbxDungeonScripts.SelectedIndex);
                txtScriptParam.Text = dungeon.ScriptList.ValueByIndex(lbxDungeonScripts.SelectedIndex);
            }
        }

        #endregion

        #region Standard Map
        void btnRemoveSMap_Click(object sender, MouseButtonEventArgs e)
        {
            
            if (lbxDungeonSMaps.SelectedIndex > -1) {
                dungeon.StandardMaps.RemoveAt(lbxDungeonSMaps.SelectedIndex);
                lbxDungeonSMaps.Items.Clear();
                for (int maps = 0; maps < dungeon.StandardMaps.Count; maps++) {
                    string mapText;
                    if (dungeon.StandardMaps[maps].IsBadGoalMap) {
                        mapText = (maps + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(dungeon.StandardMaps[maps].Difficulty) + "] Map #" + dungeon.StandardMaps[maps].MapNum;
                    } else {
                        mapText = (maps + 1) + ": [" + Missions.MissionManager.DifficultyToString(dungeon.StandardMaps[maps].Difficulty) + "] Map #" + dungeon.StandardMaps[maps].MapNum;
                    }
                    ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                    lbxDungeonSMaps.Items.Add(lbiMap);
                }
            }
        }

        void btnAddSMap_Click(object sender, MouseButtonEventArgs e)
        {
            if (MapIndex(nudMapNum.Value) > -1)
            {
                dungeon.StandardMaps.RemoveAt(MapIndex(nudMapNum.Value));
                lbxDungeonSMaps.Items.Clear();
                for (int maps = 0; maps < dungeon.StandardMaps.Count; maps++)
                {
                    string mapText2;
                    if (dungeon.StandardMaps[maps].IsBadGoalMap)
                    {
                        mapText2 = (maps + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(dungeon.StandardMaps[maps].Difficulty) + "] Map #" + dungeon.StandardMaps[maps].MapNum;
                    }
                    else
                    {
                        mapText2 = (maps + 1) + ": [" + Missions.MissionManager.DifficultyToString(dungeon.StandardMaps[maps].Difficulty) + "] Map #" + dungeon.StandardMaps[maps].MapNum;
                    }
                    ListBoxTextItem lbiMap2 = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText2);
                    lbxDungeonSMaps.Items.Add(lbiMap2);
                }
            }

            Logic.Editors.Dungeons.EditableStandardDungeonMap map = new Logic.Editors.Dungeons.EditableStandardDungeonMap();
            map.MapNum = nudMapNum.Value;
            map.Difficulty = (Enums.JobDifficulty)nudSMapDifficulty.Value;
            map.IsBadGoalMap = chkSMapBad.Checked;

            dungeon.StandardMaps.Add(map);

            string mapText;
            if (map.IsBadGoalMap) {
                mapText = (lbxDungeonSMaps.Items.Count + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Map #" + map.MapNum;
            } else {
                mapText = (lbxDungeonSMaps.Items.Count + 1) + ": [" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Map #" + map.MapNum;
            }
            ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
            lbxDungeonSMaps.Items.Add(lbiMap);
        }

        void btnLoadSMap_Click(object sender, MouseButtonEventArgs e)
        {
            if (lbxDungeonSMaps.SelectedIndex > -1)
            {
                nudMapNum.Value = dungeon.StandardMaps[lbxDungeonSMaps.SelectedIndex].MapNum;
                nudSMapDifficulty.Value = (int)dungeon.StandardMaps[lbxDungeonSMaps.SelectedIndex].Difficulty;
                chkSMapBad.Checked = dungeon.StandardMaps[lbxDungeonSMaps.SelectedIndex].IsBadGoalMap;
            }
        }

        int MapIndex(int mapNum)
        {
            for (int i = 0; i < dungeon.StandardMaps.Count; i++)
            {
                if (dungeon.StandardMaps[i].MapNum == mapNum)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #region Random Map
        void btnRemoveRMap_Click(object sender, MouseButtonEventArgs e)
        {
            
            if (lbxDungeonRMaps.SelectedIndex > -1) {
                dungeon.RandomMaps.RemoveAt(lbxDungeonRMaps.SelectedIndex);
                lbxDungeonRMaps.Items.Clear();
                for (int maps = 0; maps < dungeon.RandomMaps.Count; maps++) {
                    string mapText;
                    if (dungeon.RandomMaps[maps].IsBadGoalMap) {
                        mapText = (maps + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(dungeon.RandomMaps[maps].Difficulty) + "] Dun #" + (dungeon.RandomMaps[maps].RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[dungeon.RandomMaps[maps].RDungeonIndex].Name + ") " + (dungeon.RandomMaps[maps].RDungeonFloor + 1) + "F";
                    }
                    else {
                        mapText = (maps + 1) + ": [" + Missions.MissionManager.DifficultyToString(dungeon.RandomMaps[maps].Difficulty) + "] Dun #" + (dungeon.RandomMaps[maps].RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[dungeon.RandomMaps[maps].RDungeonIndex].Name + ") " + (dungeon.RandomMaps[maps].RDungeonFloor + 1) + "F";
                    }
                    ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                    lbxDungeonRMaps.Items.Add(lbiMap);
                }
            }
        }

        void btnAddRMap_Click(object sender, MouseButtonEventArgs e)
        {
            for (int i = nudRDungeonFloor.Value; i <= nudRDungeonFloorEnd.Value; i++)
            {
                int index = DungeonFloorIndex(nudRDungeonIndex.Value - 1, i - 1);
                if (index > -1)
                {
                    dungeon.RandomMaps.RemoveAt(index);
                }
            }

            lbxDungeonRMaps.Items.Clear();
            for (int maps = 0; maps < dungeon.RandomMaps.Count; maps++)
            {
                string mapText;
                if (dungeon.RandomMaps[maps].IsBadGoalMap)
                {
                    mapText = (maps + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(dungeon.RandomMaps[maps].Difficulty) + "] Dun #" + (dungeon.RandomMaps[maps].RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[dungeon.RandomMaps[maps].RDungeonIndex].Name + ") " + (dungeon.RandomMaps[maps].RDungeonFloor + 1) + "F";
                }
                else
                {
                    mapText = (maps + 1) + ": [" + Missions.MissionManager.DifficultyToString(dungeon.RandomMaps[maps].Difficulty) + "] Dun #" + (dungeon.RandomMaps[maps].RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[dungeon.RandomMaps[maps].RDungeonIndex].Name + ") " + (dungeon.RandomMaps[maps].RDungeonFloor + 1) + "F";
                }
                ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                lbxDungeonRMaps.Items.Add(lbiMap);
            }

            for (int i = nudRDungeonFloor.Value; i <= nudRDungeonFloorEnd.Value; i++)
            {
                
                Logic.Editors.Dungeons.EditableRandomDungeonMap map = new Logic.Editors.Dungeons.EditableRandomDungeonMap();
                map.RDungeonIndex = nudRDungeonIndex.Value - 1;
                map.RDungeonFloor = i - 1;
                map.Difficulty = (Enums.JobDifficulty)nudRMapDifficulty.Value;
                map.IsBadGoalMap = chkRMapBad.Checked;

                dungeon.RandomMaps.Add(map);

                string mapText;
                if (map.IsBadGoalMap)
                {
                    mapText = (lbxDungeonRMaps.Items.Count + 1) + ": (Boss)[" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Dun #" + (map.RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[map.RDungeonIndex].Name + ") " + (map.RDungeonFloor + 1) + "F";
                }
                else
                {
                    mapText = (lbxDungeonRMaps.Items.Count + 1) + ": [" + Missions.MissionManager.DifficultyToString(map.Difficulty) + "] Dun #" + (map.RDungeonIndex + 1) + " (" + RDungeons.RDungeonHelper.RDungeons[map.RDungeonIndex].Name + ") " + (map.RDungeonFloor + 1) + "F";
                }
                ListBoxTextItem lbiMap = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), mapText);
                lbxDungeonRMaps.Items.Add(lbiMap);
            }
        }

        void btnLoadRMap_Click(object sender, MouseButtonEventArgs e)
        {
            if (lbxDungeonRMaps.SelectedIndex > -1)
            {
                nudRDungeonIndex.Value = dungeon.RandomMaps[lbxDungeonRMaps.SelectedIndex].RDungeonIndex + 1;
                nudRDungeonFloor.Value = dungeon.RandomMaps[lbxDungeonRMaps.SelectedIndex].RDungeonFloor + 1;
                nudRMapDifficulty.Value = (int)dungeon.RandomMaps[lbxDungeonRMaps.SelectedIndex].Difficulty;
                chkRMapBad.Checked = dungeon.RandomMaps[lbxDungeonRMaps.SelectedIndex].IsBadGoalMap;
            }
        }

        void nudRDungeonIndex_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e)
        {
            lblRDungeonIndex.Text = RDungeons.RDungeonHelper.RDungeons[nudRDungeonIndex.Value - 1].Name;
        }

        void nudRDungeonFloor_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e)
        {
            if (nudRDungeonFloorEnd.Value < nudRDungeonFloor.Value)
            {
                nudRDungeonFloorEnd.Value = nudRDungeonFloor.Value;
            }
        }

        void nudRDungeonFloorEnd_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e)
        {
            if (nudRDungeonFloorEnd.Value < nudRDungeonFloor.Value)
            {
                nudRDungeonFloor.Value = nudRDungeonFloorEnd.Value;
            }
        }

        int DungeonFloorIndex(int dungeonIndex, int dungeonFloor)
        {
            for (int i = 0; i < dungeon.RandomMaps.Count; i++)
            {
                if (dungeon.RandomMaps[i].RDungeonIndex == dungeonIndex && dungeon.RandomMaps[i].RDungeonFloor == dungeonFloor)
                {
                    return i;
                }
            }

            return -1;
        }

        #endregion

        #endregion
    }
}
