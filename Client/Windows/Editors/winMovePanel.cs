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
    class winMovePanel : Core.WindowCore
    {
        #region Fields


        int moveNum = -1;
        int currentTen = 0;

        Panel pnlMoveList;
        Panel pnlMoveEditor;

        ListBox lbxMoveList;
        ListBoxTextItem lbiItem;
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        Label lblName;
        TextBox txtName;
        Label lblMaxPP;
        NumericUpDown nudMaxPP;
        Label lblEffectType;
        ComboBox cbEffectType;
        Label lblElement;
        ComboBox cbElement;
        Label lblMoveCategory;
        ComboBox cbMoveCategory;
        Label lblTargetType;
        ComboBox cbTargetType;
        Label lblRangeType;
        ComboBox cbRangeType;
        Label lblRange;
        NumericUpDown nudRange;

        Label lblData1;
        NumericUpDown nudData1;
        Label lblData2;
        NumericUpDown nudData2;
        Label lblData3;
        NumericUpDown nudData3;
        Label lblAccuracy;
        NumericUpDown nudAccuracy;
        Label lblHitTime;
        NumericUpDown nudHitTime;
        Label lblExtraEffectData1;
        NumericUpDown nudExtraEffectData1;
        Label lblExtraEffectData2;
        NumericUpDown nudExtraEffectData2;
        Label lblExtraEffectData3;
        NumericUpDown nudExtraEffectData3;
        CheckBox chkPerPlayer;
        CheckBox chkHitFreeze;
        Label lblKeyItem;
        NumericUpDown nudKeyItem;

        Label lblSound;
        NumericUpDown nudSound;

        Panel pnlAttackerAnim;
        Panel pnlTravelingAnim;
        Panel pnlDefenderAnim;

        Button btnAttackerAnim;
        Button btnTravelingAnim;
        Button btnDefenderAnim;

        Label lblAttackerAnimIndex;
        NumericUpDown nudAttackerAnimIndex;
        Label lblAttackerAnimTime;
        NumericUpDown nudAttackerAnimTime;
        Label lblAttackerAnimCycle;
        NumericUpDown nudAttackerAnimCycle;

        Label lblTravelingAnimType;
        ComboBox cbTravelingAnimType;
        Label lblTravelingAnimIndex;
        NumericUpDown nudTravelingAnimIndex;
        Label lblTravelingAnimTime;
        NumericUpDown nudTravelingAnimTime;
        Label lblTravelingAnimCycle;
        NumericUpDown nudTravelingAnimCycle;

        Label lblDefenderAnimIndex;
        NumericUpDown nudDefenderAnimIndex;
        Label lblDefenderAnimTime;
        NumericUpDown nudDefenderAnimTime;
        Label lblDefenderAnimCycle;
        NumericUpDown nudDefenderAnimCycle;

        Button btnPreview;
        PictureBox picPreview;
        Timer tmrPreview;
        Graphics.Renderers.Moves.IMoveAnimation moveAnim;

        //Image Preview


        #endregion Fields

        #region Constructors

        public winMovePanel()
            : base("winMovePanel") {



            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Move Panel";

            pnlMoveList = new Panel("pnlMoveList");
            pnlMoveList.Size = new System.Drawing.Size(200, 230);
            pnlMoveList.Location = new Point(0, 0);
            pnlMoveList.BackColor = Color.White;
            pnlMoveList.Visible = true;

            pnlMoveEditor = new Panel("pnlMoveEditor");
            pnlMoveEditor.Size = new System.Drawing.Size(580, 380);
            pnlMoveEditor.Location = new Point(0, 0);
            pnlMoveEditor.BackColor = Color.White;
            pnlMoveEditor.Visible = false;

            pnlAttackerAnim = new Panel("pnlAttackerAnim");
            pnlAttackerAnim.Size = new System.Drawing.Size(234, 140);
            pnlAttackerAnim.Location = new Point(346, 60);
            pnlAttackerAnim.BackColor = Color.White;
            pnlAttackerAnim.Visible = true;

            pnlTravelingAnim = new Panel("pnlTravelingAnim");
            pnlTravelingAnim.Size = new System.Drawing.Size(234, 140);
            pnlTravelingAnim.Location = new Point(346, 60);
            pnlTravelingAnim.BackColor = Color.White;
            pnlTravelingAnim.Visible = false;

            pnlDefenderAnim = new Panel("pnlDefenderAnim");
            pnlDefenderAnim.Size = new System.Drawing.Size(234, 140);
            pnlDefenderAnim.Location = new Point(346, 60);
            pnlDefenderAnim.BackColor = Color.White;
            pnlDefenderAnim.Visible = false;

            lbxMoveList = new ListBox("lbxMoveList");
            lbxMoveList.Location = new Point(10, 10);
            lbxMoveList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Moves.MoveHelper.Moves[(i + 1) + 10 * currentTen].Name);
                lbxMoveList.Items.Add(lbiItem);
            }
            lbxMoveList.SelectItem(0);

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


            btnEditorCancel = new Button("btnEditorCancel");
            btnEditorCancel.Location = new Point(10, 334);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(100, 334);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Move Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 16);
            //txtName.Text = "Loading...";

            lblMaxPP = new Label("lblMaxPP");
            lblMaxPP.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblMaxPP.Text = "MaxPP: >=1";
            lblMaxPP.AutoSize = true;
            lblMaxPP.Location = new Point(10, 36);

            nudMaxPP = new NumericUpDown("nudMaxPP");
            nudMaxPP.Size = new Size(200, 16);
            nudMaxPP.Location = new Point(10, 48);
            nudMaxPP.Minimum = 1;
            nudMaxPP.Maximum = Int32.MaxValue;

            lblEffectType = new Label("lblEffectType");
            lblEffectType.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblEffectType.Text = "Effect Type:";
            lblEffectType.AutoSize = true;
            lblEffectType.Location = new Point(10, 68);

            cbEffectType = new ComboBox("cbEffectType");
            cbEffectType.Location = new Point(10, 88);
            cbEffectType.Size = new Size(200, 16);
            for (int i = 0; i < 7; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), );
                cbEffectType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.MoveType), i)));
            }

            lblElement = new Label("lblElement");
            lblElement.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblElement.Text = "Element:";
            lblElement.AutoSize = true;
            lblElement.Location = new Point(10, 100);

            cbElement = new ComboBox("cbElement");
            cbElement.Location = new Point(10, 118);
            cbElement.Size = new Size(200, 16);
            for (int i = 0; i < 19; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), );
                cbElement.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.PokemonType), i)));
            }

            lblMoveCategory = new Label("lblMoveCategory");
            lblMoveCategory.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblMoveCategory.Text = "Category:";
            lblMoveCategory.AutoSize = true;
            lblMoveCategory.Location = new Point(10, 132);

            cbMoveCategory = new ComboBox("cbMoveCategory");
            cbMoveCategory.Location = new Point(10, 144);
            cbMoveCategory.Size = new Size(200, 16);
            for (int i = 0; i < 3; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), );
                cbMoveCategory.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.MoveCategory), i)));
            }

            lblTargetType = new Label("lblTargetType");
            lblTargetType.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblTargetType.Text = "Targets:";
            lblTargetType.AutoSize = true;
            lblTargetType.Location = new Point(10, 164);

            cbTargetType = new ComboBox("cbTargetType");
            cbTargetType.Location = new Point(10, 176);
            cbTargetType.Size = new Size(200, 16);
            for (int i = 0; i < 8; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), );
                cbTargetType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.MoveTarget), i)));
            }

            lblRangeType = new Label("lblRangeType");
            lblRangeType.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblRangeType.Text = "RangeType:";
            lblRangeType.AutoSize = true;
            lblRangeType.Location = new Point(10, 196);

            cbRangeType = new ComboBox("cbRangeType");
            cbRangeType.Location = new Point(10, 208);
            cbRangeType.Size = new Size(200, 16);
            for (int i = 0; i < 10; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "j");                
                cbRangeType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.MoveRange), i)));
            }

            lblRange = new Label("lblRange");
            lblRange.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblRange.Text = "Range: >=0";
            lblRange.AutoSize = true;
            lblRange.Location = new Point(10, 228);

            nudRange = new NumericUpDown("nudRange");
            nudRange.Size = new Size(200, 16);
            nudRange.Location = new Point(10, 240);
            nudRange.Minimum = 0;
            nudRange.Maximum = Int32.MaxValue;

            lblKeyItem = new Label("lblKeyItem");
            lblKeyItem.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblKeyItem.Text = "KeyItem:";
            lblKeyItem.AutoSize = true;
            lblKeyItem.Location = new Point(10, 260);

            nudKeyItem = new NumericUpDown("nudKeyItem");
            nudKeyItem.Size = new Size(100, 16);
            nudKeyItem.Location = new Point(10, 272);
            nudKeyItem.Minimum = 0;
            nudKeyItem.Maximum = MaxInfo.MaxItems;


            lblData1 = new Label("lblData1");
            lblData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData1.Text = "Data1:";
            lblData1.AutoSize = true;
            lblData1.Location = new Point(220, 4);

            nudData1 = new NumericUpDown("nudData1");
            nudData1.Size = new Size(100, 16);
            nudData1.Location = new Point(220, 16);
            nudData1.Minimum = Int32.MinValue;
            nudData1.Maximum = Int32.MaxValue;

            lblData2 = new Label("lblData2");
            lblData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData2.Text = "Data2:";
            lblData2.AutoSize = true;
            lblData2.Location = new Point(220, 36);

            nudData2 = new NumericUpDown("nudData2");
            nudData2.Size = new Size(100, 16);
            nudData2.Location = new Point(220, 48);
            nudData2.Minimum = Int32.MinValue;
            nudData2.Maximum = Int32.MaxValue;

            lblData3 = new Label("lblData3");
            lblData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData3.Text = "Data3:";
            lblData3.AutoSize = true;
            lblData3.Location = new Point(220, 68);

            nudData3 = new NumericUpDown("nudData3");
            nudData3.Size = new Size(100, 16);
            nudData3.Location = new Point(220, 80);
            nudData3.Minimum = Int32.MinValue;
            nudData3.Maximum = Int32.MaxValue;

            lblAccuracy = new Label("lblAccuracy");
            lblAccuracy.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAccuracy.Text = "Accuracy: >=-1";
            lblAccuracy.AutoSize = true;
            lblAccuracy.Location = new Point(220, 100);

            nudAccuracy = new NumericUpDown("nudAccuracy");
            nudAccuracy.Size = new Size(100, 16);
            nudAccuracy.Location = new Point(220, 112);
            nudAccuracy.Minimum = -1;
            nudAccuracy.Maximum = Int32.MaxValue;

            lblHitTime = new Label("lblHitTime");
            lblHitTime.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblHitTime.Text = "HitTime: >= 1";
            lblHitTime.AutoSize = true;
            lblHitTime.Location = new Point(220, 132);

            nudHitTime = new NumericUpDown("nudHitTime");
            nudHitTime.Size = new Size(100, 16);
            nudHitTime.Location = new Point(220, 144);
            nudHitTime.Minimum = 1;
            nudHitTime.Maximum = Int32.MaxValue;

            lblExtraEffectData1 = new Label("lblExtraEffectData1");
            lblExtraEffectData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblExtraEffectData1.Text = "Effect1:";
            lblExtraEffectData1.AutoSize = true;
            lblExtraEffectData1.Location = new Point(220, 164);

            nudExtraEffectData1 = new NumericUpDown("nudExtraEffectData1");
            nudExtraEffectData1.Size = new Size(100, 16);
            nudExtraEffectData1.Location = new Point(220, 176);
            nudExtraEffectData1.Minimum = Int32.MinValue;
            nudExtraEffectData1.Maximum = Int32.MaxValue;

            lblExtraEffectData2 = new Label("lblExtraEffectData2");
            lblExtraEffectData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblExtraEffectData2.Text = "Effect2:";
            lblExtraEffectData2.AutoSize = true;
            lblExtraEffectData2.Location = new Point(220, 196);

            nudExtraEffectData2 = new NumericUpDown("nudExtraEffectData2");
            nudExtraEffectData2.Size = new Size(100, 16);
            nudExtraEffectData2.Location = new Point(220, 208);
            nudExtraEffectData2.Minimum = Int32.MinValue;
            nudExtraEffectData2.Maximum = Int32.MaxValue;

            lblExtraEffectData3 = new Label("lblExtraEffectData3");
            lblExtraEffectData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblExtraEffectData3.Text = "Effect3:";
            lblExtraEffectData3.AutoSize = true;
            lblExtraEffectData3.Location = new Point(220, 228);

            nudExtraEffectData3 = new NumericUpDown("nudExtraEffectData3");
            nudExtraEffectData3.Size = new Size(100, 16);
            nudExtraEffectData3.Location = new Point(220, 240);
            nudExtraEffectData3.Minimum = Int32.MinValue;
            nudExtraEffectData3.Maximum = Int32.MaxValue;

            chkPerPlayer = new CheckBox("chkPerPlayer");
            chkPerPlayer.Location = new Point(220, 260);
            chkPerPlayer.Size = new System.Drawing.Size(95, 17);
            chkPerPlayer.BackColor = Color.Transparent;
            chkPerPlayer.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkPerPlayer.Text = "PerPlayer";

            chkHitFreeze = new CheckBox("chkHitFreeze");
            chkHitFreeze.Location = new Point(220, 280);
            chkHitFreeze.Size = new System.Drawing.Size(95, 17);
            chkHitFreeze.BackColor = Color.Transparent;
            chkHitFreeze.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkHitFreeze.Text = "HitFreeze";


            lblSound = new Label("lblSound");
            lblSound.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSound.Text = "Sound:";
            lblSound.AutoSize = true;
            lblSound.Location = new Point(340, 4);

            nudSound = new NumericUpDown("nudSound");
            nudSound.Size = new Size(100, 16);
            nudSound.Location = new Point(340, 16);
            nudSound.Minimum = 0;
            nudSound.Maximum = Int32.MaxValue;
            nudSound.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudSound_ValueChanged);

            btnAttackerAnim = new Button("btnAttackerAnim");
            btnAttackerAnim.Location = new Point(340, 36);
            btnAttackerAnim.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAttackerAnim.Size = new System.Drawing.Size(64, 16);
            btnAttackerAnim.Text = "Attacker";
            btnAttackerAnim.Click += new EventHandler<MouseButtonEventArgs>(btnAttackerAnim_Click);
            

            btnTravelingAnim = new Button("btnTravelingAnim");
            btnTravelingAnim.Location = new Point(420, 36);
            btnTravelingAnim.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnTravelingAnim.Size = new System.Drawing.Size(64, 16);
            btnTravelingAnim.Text = "Traveling";
            btnTravelingAnim.Click += new EventHandler<MouseButtonEventArgs>(btnTravelingAnim_Click);

            btnDefenderAnim = new Button("btnDefenderAnim");
            btnDefenderAnim.Location = new Point(500, 36);
            btnDefenderAnim.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnDefenderAnim.Size = new System.Drawing.Size(64, 16);
            btnDefenderAnim.Text = "Defender";
            btnDefenderAnim.Click += new EventHandler<MouseButtonEventArgs>(btnDefenderAnim_Click);

            lblAttackerAnimIndex = new Label("lblAttackerAnimIndex");
            lblAttackerAnimIndex.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAttackerAnimIndex.Text = "Animation:";
            lblAttackerAnimIndex.AutoSize = true;
            lblAttackerAnimIndex.Location = new Point(0, 10);

            nudAttackerAnimIndex = new NumericUpDown("nudAttackerAnimIndex");
            nudAttackerAnimIndex.Size = new Size(100, 16);
            nudAttackerAnimIndex.Location = new Point(0, 22);
            nudAttackerAnimIndex.Minimum = Int32.MinValue;
            nudAttackerAnimIndex.Maximum = Int32.MaxValue;
            nudAttackerAnimIndex.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudAttackerAnimIndex_ValueChanged);

            lblAttackerAnimTime = new Label("lblAttackerAnimTime");
            lblAttackerAnimTime.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAttackerAnimTime.Text = "FrameTime: (1-1000)";
            lblAttackerAnimTime.AutoSize = true;
            lblAttackerAnimTime.Location = new Point(0, 42);

            nudAttackerAnimTime = new NumericUpDown("nudAttackerAnimTime");
            nudAttackerAnimTime.Size = new Size(100, 16);
            nudAttackerAnimTime.Location = new Point(0, 54);
            nudAttackerAnimTime.Minimum = 1;
            nudAttackerAnimTime.Maximum = 1000;

            lblAttackerAnimCycle = new Label("lblAttackerAnimCycle");
            lblAttackerAnimCycle.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAttackerAnimCycle.Text = "Cycles: (1-10)";
            lblAttackerAnimCycle.AutoSize = true;
            lblAttackerAnimCycle.Location = new Point(0, 74);

            nudAttackerAnimCycle = new NumericUpDown("nudAttackerAnimCycle");
            nudAttackerAnimCycle.Size = new Size(100, 16);
            nudAttackerAnimCycle.Location = new Point(0, 86);
            nudAttackerAnimCycle.Minimum = 1;
            nudAttackerAnimCycle.Maximum = 10;

            lblTravelingAnimType = new Label("lblTravelingAnimType");
            lblTravelingAnimType.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblTravelingAnimType.Text = "Anim Type:";
            lblTravelingAnimType.AutoSize = true;
            lblTravelingAnimType.Location = new Point(0, 10);

            cbTravelingAnimType = new ComboBox("cbTravelingAnimType");
            cbTravelingAnimType.Location = new Point(0, 22);
            cbTravelingAnimType.Size = new Size(200, 16);
            for (int i = 0; i < 8; i++) {
                //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "j");                
                cbTravelingAnimType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), Enum.GetName(typeof(Enums.MoveAnimationType), i)));
            }

            lblTravelingAnimIndex = new Label("lblTravelingAnimIndex");
            lblTravelingAnimIndex.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblTravelingAnimIndex.Text = "Animation:";
            lblTravelingAnimIndex.AutoSize = true;
            lblTravelingAnimIndex.Location = new Point(0, 42);

            nudTravelingAnimIndex = new NumericUpDown("nudTravelingAnimIndex");
            nudTravelingAnimIndex.Size = new Size(100, 16);
            nudTravelingAnimIndex.Location = new Point(0, 54);
            nudTravelingAnimIndex.Minimum = Int32.MinValue;
            nudTravelingAnimIndex.Maximum = Int32.MaxValue;
            nudTravelingAnimIndex.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudTravelingAnimIndex_ValueChanged);

            lblTravelingAnimTime = new Label("lblTravelingAnimTime");
            lblTravelingAnimTime.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblTravelingAnimTime.Text = "FrameTime: (1-1000)";
            lblTravelingAnimTime.AutoSize = true;
            lblTravelingAnimTime.Location = new Point(0, 74);

            nudTravelingAnimTime = new NumericUpDown("nudTravelingAnimTime");
            nudTravelingAnimTime.Size = new Size(100, 16);
            nudTravelingAnimTime.Location = new Point(0, 86);
            nudTravelingAnimTime.Minimum = 1;
            nudTravelingAnimTime.Maximum = 1000;

            lblTravelingAnimCycle = new Label("lblTravelingAnimCycle");
            lblTravelingAnimCycle.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblTravelingAnimCycle.Text = "Cycles: (1-10)";
            lblTravelingAnimCycle.AutoSize = true;
            lblTravelingAnimCycle.Location = new Point(0, 106);

            nudTravelingAnimCycle = new NumericUpDown("nudTravelingAnimCycle");
            nudTravelingAnimCycle.Size = new Size(100, 16);
            nudTravelingAnimCycle.Location = new Point(0, 118);
            nudTravelingAnimCycle.Minimum = 1;
            nudTravelingAnimCycle.Maximum = 10;


            lblDefenderAnimIndex = new Label("lblDefenderAnimIndex");
            lblDefenderAnimIndex.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblDefenderAnimIndex.Text = "Animation:";
            lblDefenderAnimIndex.AutoSize = true;
            lblDefenderAnimIndex.Location = new Point(0, 10);

            nudDefenderAnimIndex = new NumericUpDown("nudDefenderAnimIndex");
            nudDefenderAnimIndex.Size = new Size(100, 16);
            nudDefenderAnimIndex.Location = new Point(0, 22);
            nudDefenderAnimIndex.Minimum = Int32.MinValue;
            nudDefenderAnimIndex.Maximum = Int32.MaxValue;
            nudDefenderAnimIndex.ValueChanged +=new EventHandler<ValueChangedEventArgs>(nudDefenderAnimIndex_ValueChanged);

            lblDefenderAnimTime = new Label("lblDefenderAnimTime");
            lblDefenderAnimTime.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblDefenderAnimTime.Text = "FrameTime: (1-1000)";
            lblDefenderAnimTime.AutoSize = true;
            lblDefenderAnimTime.Location = new Point(0, 42);

            nudDefenderAnimTime = new NumericUpDown("nudDefenderAnimTime");
            nudDefenderAnimTime.Size = new Size(100, 16);
            nudDefenderAnimTime.Location = new Point(0, 54);
            nudDefenderAnimTime.Minimum = 1;
            nudDefenderAnimTime.Maximum = 1000;

            lblDefenderAnimCycle = new Label("lblDefenderAnimCycle");
            lblDefenderAnimCycle.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblDefenderAnimCycle.Text = "Cycles: (1-10)";
            lblDefenderAnimCycle.AutoSize = true;
            lblDefenderAnimCycle.Location = new Point(0, 74);

            nudDefenderAnimCycle = new NumericUpDown("nudDefenderAnimCycle");
            nudDefenderAnimCycle.Size = new Size(100, 16);
            nudDefenderAnimCycle.Location = new Point(0, 86);
            nudDefenderAnimCycle.Minimum = 1;
            nudDefenderAnimCycle.Maximum = 10;


            btnPreview = new Button("btnPreview");
            btnPreview.Location = new Point(220, 300);
            btnPreview.Font = Graphics.FontManager.LoadFont("tahoma", 20);
            btnPreview.Size = new System.Drawing.Size(96, 32);
            btnPreview.Visible = true;
            btnPreview.Text = "Preview";
            btnPreview.Click += new EventHandler<MouseButtonEventArgs>(btnPreview_Click);

            picPreview = new PictureBox("picPreview");
            picPreview.Size = new Size(196, 128);
            picPreview.BackColor = Color.Transparent;
            picPreview.Location = new Point(340, 200);
            picPreview.Image = new SdlDotNet.Graphics.Surface(196, 128);


            tmrPreview = new Timer("tmrPreview");
            tmrPreview.Elapsed += new EventHandler(tmrPreview_Elapsed);

            //moveAnim = new Graphics.Renderers.Moves.FixedMoveAnimation(0, 0);




            //this.AddWidget(lbxItems);
            pnlMoveList.AddWidget(lbxMoveList);
            pnlMoveList.AddWidget(btnBack);
            pnlMoveList.AddWidget(btnForward);
            pnlMoveList.AddWidget(btnEdit);
            pnlMoveList.AddWidget(btnCancel);

            pnlMoveEditor.AddWidget(lblName);
            pnlMoveEditor.AddWidget(txtName);
            pnlMoveEditor.AddWidget(lblMaxPP);
            pnlMoveEditor.AddWidget(nudMaxPP);
            pnlMoveEditor.AddWidget(lblEffectType);
            pnlMoveEditor.AddWidget(cbEffectType);
            pnlMoveEditor.AddWidget(lblElement);
            pnlMoveEditor.AddWidget(cbElement);
            pnlMoveEditor.AddWidget(lblMoveCategory);
            pnlMoveEditor.AddWidget(cbMoveCategory);
            pnlMoveEditor.AddWidget(lblTargetType);
            pnlMoveEditor.AddWidget(cbTargetType);
            pnlMoveEditor.AddWidget(lblRangeType);
            pnlMoveEditor.AddWidget(cbRangeType);
            pnlMoveEditor.AddWidget(lblRange);
            pnlMoveEditor.AddWidget(nudRange);
            pnlMoveEditor.AddWidget(lblKeyItem);
            pnlMoveEditor.AddWidget(nudKeyItem);



            pnlMoveEditor.AddWidget(lblData1);
            pnlMoveEditor.AddWidget(nudData1);
            pnlMoveEditor.AddWidget(lblData2);
            pnlMoveEditor.AddWidget(nudData2);
            pnlMoveEditor.AddWidget(lblData3);
            pnlMoveEditor.AddWidget(nudData3);
            pnlMoveEditor.AddWidget(lblAccuracy);
            pnlMoveEditor.AddWidget(nudAccuracy);
            pnlMoveEditor.AddWidget(lblHitTime);
            pnlMoveEditor.AddWidget(nudHitTime);
            pnlMoveEditor.AddWidget(lblExtraEffectData1);
            pnlMoveEditor.AddWidget(nudExtraEffectData1);
            pnlMoveEditor.AddWidget(lblExtraEffectData2);
            pnlMoveEditor.AddWidget(nudExtraEffectData2);
            pnlMoveEditor.AddWidget(lblExtraEffectData3);
            pnlMoveEditor.AddWidget(nudExtraEffectData3);
            pnlMoveEditor.AddWidget(chkPerPlayer);
            pnlMoveEditor.AddWidget(chkHitFreeze);

            pnlMoveEditor.AddWidget(lblSound);
            pnlMoveEditor.AddWidget(nudSound);

            pnlMoveEditor.AddWidget(btnAttackerAnim);
            pnlMoveEditor.AddWidget(btnTravelingAnim);
            pnlMoveEditor.AddWidget(btnDefenderAnim);

            pnlAttackerAnim.AddWidget(lblAttackerAnimIndex);
            pnlAttackerAnim.AddWidget(nudAttackerAnimIndex);
            pnlAttackerAnim.AddWidget(lblAttackerAnimTime);
            pnlAttackerAnim.AddWidget(nudAttackerAnimTime);
            pnlAttackerAnim.AddWidget(lblAttackerAnimCycle);
            pnlAttackerAnim.AddWidget(nudAttackerAnimCycle);

            pnlTravelingAnim.AddWidget(lblTravelingAnimType);
            pnlTravelingAnim.AddWidget(cbTravelingAnimType);
            pnlTravelingAnim.AddWidget(lblTravelingAnimIndex);
            pnlTravelingAnim.AddWidget(nudTravelingAnimIndex);
            pnlTravelingAnim.AddWidget(lblTravelingAnimTime);
            pnlTravelingAnim.AddWidget(nudTravelingAnimTime);
            pnlTravelingAnim.AddWidget(lblTravelingAnimCycle);
            pnlTravelingAnim.AddWidget(nudTravelingAnimCycle);

            pnlDefenderAnim.AddWidget(lblDefenderAnimIndex);
            pnlDefenderAnim.AddWidget(nudDefenderAnimIndex);
            pnlDefenderAnim.AddWidget(lblDefenderAnimTime);
            pnlDefenderAnim.AddWidget(nudDefenderAnimTime);
            pnlDefenderAnim.AddWidget(lblDefenderAnimCycle);
            pnlDefenderAnim.AddWidget(nudDefenderAnimCycle);

            pnlMoveEditor.AddWidget(pnlAttackerAnim);
            pnlMoveEditor.AddWidget(pnlTravelingAnim);
            pnlMoveEditor.AddWidget(pnlDefenderAnim);

            pnlMoveEditor.AddWidget(btnPreview);
            pnlMoveEditor.AddWidget(picPreview);
            pnlMoveEditor.AddWidget(tmrPreview);


            pnlMoveEditor.AddWidget(btnEditorCancel);
            pnlMoveEditor.AddWidget(btnEditorOK);



            this.AddWidget(pnlMoveList);
            this.AddWidget(pnlMoveEditor);

            //this.LoadComplete();

        }

        #endregion Constructors

        public void LoadMove(string[] parse) {

            pnlMoveList.Visible = false;
            pnlMoveEditor.Visible = true;
            this.Size = new System.Drawing.Size(pnlMoveEditor.Width, pnlMoveEditor.Height);

            txtName.Text = parse[2];
            nudMaxPP.Value = parse[3].ToInt();
            cbEffectType.SelectItem(Enum.GetName(typeof(Enums.MoveType), parse[4].ToInt()));
            cbElement.SelectItem(Enum.GetName(typeof(Enums.PokemonType), parse[5].ToInt()));
            cbMoveCategory.SelectItem(Enum.GetName(typeof(Enums.MoveCategory), parse[6].ToInt()));
            cbRangeType.SelectItem(Enum.GetName(typeof(Enums.MoveRange), parse[7].ToInt()));
            nudRange.Value = parse[8].ToInt();
            cbTargetType.SelectItem(Enum.GetName(typeof(Enums.MoveTarget), parse[9].ToInt()));
            nudData1.Value = parse[10].ToInt();
            nudData2.Value = parse[11].ToInt();
            nudData3.Value = parse[12].ToInt();
            nudAccuracy.Value = parse[13].ToInt();
            nudHitTime.Value = parse[14].ToInt();
            chkHitFreeze.Checked = parse[15].ToBool();
            nudExtraEffectData1.Value = parse[16].ToInt();
            nudExtraEffectData2.Value = parse[17].ToInt();
            nudExtraEffectData3.Value = parse[18].ToInt();
            chkPerPlayer.Checked = parse[19].ToBool();
            nudKeyItem.Value = parse[20].ToInt();
            nudSound.Value = parse[21].ToInt();

            //attackeranimationtype - 22
            nudAttackerAnimIndex.Value = parse[23].ToInt();
            nudAttackerAnimTime.Value = parse[24].ToInt();
            nudAttackerAnimCycle.Value = parse[25].ToInt();

            cbTravelingAnimType.SelectItem(Enum.GetName(typeof(Enums.MoveAnimationType), parse[26].ToInt()));
            nudTravelingAnimIndex.Value = parse[27].ToInt();
            nudTravelingAnimTime.Value = parse[28].ToInt();
            nudTravelingAnimCycle.Value = parse[29].ToInt();

            //defenderanimationtype - 30
            nudDefenderAnimIndex.Value = parse[31].ToInt();
            nudDefenderAnimTime.Value = parse[32].ToInt();
            nudDefenderAnimCycle.Value = parse[33].ToInt();


            btnEdit.Text = "Edit";
        }

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshMoveList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxMoves / 10)) {
                currentTen++;
            }
            RefreshMoveList();
        }

        void btnAttackerAnim_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            pnlAttackerAnim.Visible = true;
            pnlTravelingAnim.Visible = false;
            pnlDefenderAnim.Visible = false;
        }

        void btnTravelingAnim_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            pnlAttackerAnim.Visible = false;
            pnlTravelingAnim.Visible = true;
            pnlDefenderAnim.Visible = false;
        }

        void btnDefenderAnim_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            pnlAttackerAnim.Visible = false;
            pnlTravelingAnim.Visible = false;
            pnlDefenderAnim.Visible = true;
        }

        void nudSound_ValueChanged(object sender, ValueChangedEventArgs e) {
            ShowPreview();
        }

        void nudAttackerAnimIndex_ValueChanged(object sender, ValueChangedEventArgs e) {
            ShowPreview();
        }

        void nudTravelingAnimIndex_ValueChanged(object sender, ValueChangedEventArgs e) {
            ShowPreview();
        }

        void nudDefenderAnimIndex_ValueChanged(object sender, ValueChangedEventArgs e) {
            ShowPreview();
        }

        void btnPreview_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            ShowPreview();
        }

        void ShowPreview() {

            if (pnlAttackerAnim.Visible) {
                moveAnim = new Graphics.Renderers.Moves.NormalMoveAnimation(2, 2);
                moveAnim.AnimationIndex = nudAttackerAnimIndex.Value;
                moveAnim.MoveTime = nudAttackerAnimTime.Value;
                moveAnim.RenderLoops = nudAttackerAnimCycle.Value;
                tmrPreview.Interval = nudAttackerAnimTime.Value;
            } else if (pnlDefenderAnim.Visible) {
                moveAnim = new Graphics.Renderers.Moves.NormalMoveAnimation(2, 2);
                moveAnim.AnimationIndex = nudDefenderAnimIndex.Value;
                moveAnim.MoveTime = nudDefenderAnimTime.Value;
                moveAnim.RenderLoops = nudDefenderAnimCycle.Value;
                tmrPreview.Interval = nudDefenderAnimTime.Value;
            } else {
                switch ((Enums.MoveAnimationType)cbTravelingAnimType.SelectedIndex) {
                    case Enums.MoveAnimationType.Normal: {
                            moveAnim = new Graphics.Renderers.Moves.NormalMoveAnimation(2, 2);
                        }
                        break;
                    case Enums.MoveAnimationType.Overlay: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.OverlayMoveAnimation();
                        }
                        break;
                    case Enums.MoveAnimationType.Tile: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.TileMoveAnimation(2, 2, (Enums.MoveRange)cbRangeType.SelectedIndex, Enums.Direction.Right, 3);
                        }
                        break;
                    case Enums.MoveAnimationType.Arrow: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.ArrowMoveAnimation(1, 2, Enums.Direction.Right, 4);
                        }
                        break;
                    case Enums.MoveAnimationType.Throw: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.ThrowMoveAnimation(1, 4, 3, -1);
                        }
                        break;
                    case Enums.MoveAnimationType.Beam: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.BeamMoveAnimation(1, 2, Enums.Direction.Right, 4);
                        }
                        break;
                    case Enums.MoveAnimationType.ItemArrow: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.ItemArrowMoveAnimation(1, 2, Enums.Direction.Right, 4);
                        }
                        break;
                    case Enums.MoveAnimationType.ItemThrow: {
                            moveAnim = new Logic.Graphics.Renderers.Moves.ItemThrowMoveAnimation(1, 4, 3, -1);
                        }
                        break;
                }

                moveAnim.AnimationIndex = nudTravelingAnimIndex.Value;
                moveAnim.MoveTime = nudTravelingAnimTime.Value;
                moveAnim.RenderLoops = nudTravelingAnimCycle.Value;

                tmrPreview.Interval = nudTravelingAnimTime.Value;
            }
            


            Music.Music.AudioPlayer.PlaySoundEffect("magic" + nudSound.Value + ".wav");
            moveAnim.Active = true;
            moveAnim.Frame = 0;
            moveAnim.CompletedLoops = 0;
            RenderAnimationToPictureBox(picPreview, moveAnim);
            tmrPreview.Start();
        }

        void tmrPreview_Elapsed(object sender, EventArgs e) {
            picPreview.Image = new SdlDotNet.Graphics.Surface(196, 128);
            RenderAnimationToPictureBox(picPreview, moveAnim);
            if (!moveAnim.Active) {
                tmrPreview.Stop();
            }

        }

        public static void RenderAnimationToPictureBox(SdlDotNet.Widgets.PictureBox pic, Graphics.Renderers.Moves.IMoveAnimation animation) {
            if (animation.Active) {
                Graphics.Renderers.RendererDestinationData destData = new Graphics.Renderers.RendererDestinationData(pic.Image, new Point(0, 0), pic.Size);

                Graphics.Renderers.Moves.MoveRenderer.RenderMoveAnimation(destData, animation, new Point(32*animation.StartX,32*animation.StartY));
                pic.RequestRedraw();
            }
        }

        public void RefreshMoveList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxMoves) {
                    ((ListBoxTextItem)lbxMoveList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Moves.MoveHelper.Moves[(i + 1) + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxMoveList.Items[i]).Text = "---";
                }
            }
        }


        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxMoveList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxMoveList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    moveNum = index[0].ToInt();
                    btnEdit.Text = "Loading...";
                }

                Messenger.SendEditMove(moveNum);
            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            moveNum = -1;
            pnlMoveEditor.Visible = false;
            pnlMoveList.Visible = true;
            this.Size = new System.Drawing.Size(pnlMoveList.Width, pnlMoveList.Height);

        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            string[] moveToSend = new String[32];


            moveToSend[0] = txtName.Text;
            moveToSend[1] = nudMaxPP.Value.ToString();


            moveToSend[2] = cbEffectType.SelectedIndex.ToString();
            moveToSend[3] = cbElement.SelectedIndex.ToString();
            moveToSend[4] = cbMoveCategory.SelectedIndex.ToString();
            moveToSend[5] = cbTargetType.SelectedIndex.ToString();
            moveToSend[6] = cbRangeType.SelectedIndex.ToString();

            moveToSend[7] = nudRange.Value.ToString();
            moveToSend[8] = nudData1.Value.ToString();
            moveToSend[9] = nudData2.Value.ToString();
            moveToSend[10] = nudData3.Value.ToString();
            moveToSend[11] = nudAccuracy.Value.ToString();
            moveToSend[12] = nudHitTime.Value.ToString();
            moveToSend[13] = chkHitFreeze.Checked.ToIntString();
            moveToSend[14] = nudExtraEffectData1.Value.ToString();
            moveToSend[15] = nudExtraEffectData2.Value.ToString();
            moveToSend[16] = nudExtraEffectData3.Value.ToString();
            moveToSend[17] = chkPerPlayer.Checked.ToIntString();

            moveToSend[18] = nudKeyItem.Value.ToString();
            moveToSend[19] = nudSound.Value.ToString();

            moveToSend[20] = "";//attacker animation type
            moveToSend[21] = nudAttackerAnimIndex.Value.ToString();
            moveToSend[22] = nudAttackerAnimTime.Value.ToString();
            moveToSend[23] = nudAttackerAnimCycle.Value.ToString();

            moveToSend[24] = cbTravelingAnimType.SelectedIndex.ToString();
            moveToSend[25] = nudTravelingAnimIndex.Value.ToString();
            moveToSend[26] = nudTravelingAnimTime.Value.ToString();
            moveToSend[27] = nudTravelingAnimCycle.Value.ToString();

            moveToSend[28] = "";//defender animation type
            moveToSend[29] = nudDefenderAnimIndex.Value.ToString();
            moveToSend[30] = nudDefenderAnimTime.Value.ToString();
            moveToSend[31] = nudDefenderAnimCycle.Value.ToString();

            Messenger.SendSaveMove(moveNum, moveToSend);
            moveNum = -1;
            pnlMoveEditor.Visible = false;
            pnlMoveList.Visible = true;
            this.Size = new System.Drawing.Size(pnlMoveList.Width, pnlMoveList.Height);


        }


    }
}
