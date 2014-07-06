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
using Client.Logic.Graphics;

using PMU.Core;
using Client.Logic.Network;

namespace Client.Logic.Windows.Editors
{
    class winStoryPanel : Core.WindowCore
    {
        #region Fields

        int storyNum = -1;
        int currentTen = 0;
        Logic.Editors.Stories.EditableStory story;

        Panel pnlStoryList;
        Panel pnlEditorGeneral;
        Panel pnlEditorSegments;

        ListBox lbxStoryList;
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        #region General
        Button btnSegments;
        Label lblName;
        TextBox txtName;
        Label lblStoryStart;
        NumericUpDown nudStoryStart;
        Label lblExitAndContinue;
        Button btnAddExitAndContinue;
        NumericUpDown nudExitAndContinueCheckpoint;
        Button btnRemoveExitAndContinue;
        ListBox lbxExitAndContinue;
        #endregion

        #region Segments
        Button btnGeneral;
        //Label lblMaxSegments;
        //NumericUpDown nudMaxSegments;
        //Button btnSaveMaxSegments;
        //Label lblActiveSegment;
        //NumericUpDown nudActiveSegment;
        Button btnAddSegment;
        Button btnRemoveSegment;
        Button btnLoadSegment;
        Button btnSaveSegment;
        Label lblActions;
        ComboBox cmbSegmentTypes;
        ListBox lbxSegments;
        #endregion

        #region Actions
        #region Say
        Panel pnlSayAction;
        Label lblSayText;
        TextBox txtSayText;
        Label lblSayMugshot;
        NumericUpDown nudSayMugshot;
        PictureBox pbxSayMugshot;
        Label lblSaySpeed;
        NumericUpDown nudSaySpeed;
        Label lblSayPause;
        NumericUpDown nudSayPause;
        #endregion
        #region Pause
        Panel pnlPauseAction;
        Label lblPauseLength;
        NumericUpDown nudPauseLength;
        #endregion
        #region Map Visibility
        Panel pnlMapVisibilityAction;
        CheckBox chkMapVisibilityVisible;
        #endregion
        #region Padlock
        Panel pnlPadlockAction;
        CheckBox chkPadlockState;
        #endregion
        #region Play Music
        Panel pnlPlayMusicAction;
        ListBox lbxPlayMusicPicker;
        Button btnPlayMusicPlay;
        Button btnPlayMusicStop;
        CheckBox chkPlayMusicHonorSettings;
        CheckBox chkPlayMusicLoop;
        #endregion
        #region Stop Music
        Panel pnlStopMusicAction;
        #endregion
        #region Show Image
        Panel pnlShowImageAction;
        ListBox lbxShowImageFiles;
        Label lblShowImageID;
        TextBox txtShowImageID;
        Label lblShowImageX;
        NumericUpDown nudShowImageX;
        Label lblShowImageY;
        NumericUpDown nudShowImageY;
        #endregion
        #region Hide Image
        Panel pnlHideImageAction;
        Label lblHideImageID;
        TextBox txtHideImageID;
        #endregion
        #region Warp
        Panel pnlWarpAction;
        Label lblWarpMap;
        TextBox txtWarpMap;
        Label lblWarpX;
        NumericUpDown nudWarpX;
        Label lblWarpY;
        NumericUpDown nudWarpY;
        #endregion
        #region Player Padlock
        Panel pnlPlayerPadlockAction;
        CheckBox chkPlayerPadlockState;
        #endregion
        #region Show Background
        Panel pnlShowBackgroundAction;
        ListBox lbxShowBackgroundFiles;
        #endregion
        #region Hide Background
        Panel pnlHideBackgroundAction;
        #endregion
        #region Create FNPC
        Panel pnlCreateFNPCAction;
        Label lblCreateFNPCID;
        TextBox txtCreateFNPCID;
        Label lblCreateFNPCMap;
        TextBox txtCreateFNPCMap;
        Label lblCreateFNPCX;
        NumericUpDown nudCreateFNPCX;
        Label lblCreateFNPCY;
        NumericUpDown nudCreateFNPCY;
        Label lblCreateFNPCSprite;
        NumericUpDown nudCreateFNPCSprite;
        PictureBox pbxCreateFNPCSprite;
        #endregion
        #region Move FNPC
        Panel pnlMoveFNPCAction;
        Label lblMoveFNPCID;
        TextBox txtMoveFNPCID;
        Label lblMoveFNPCX;
        NumericUpDown nudMoveFNPCX;
        Label lblMoveFNPCY;
        NumericUpDown nudMoveFNPCY;
        Label lblMoveFNPCSpeed;
        ComboBox cbxMoveFNPCSpeed;
        CheckBox chkMoveFNPCPause;
        #endregion
        #region Warp FNPC
        Panel pnlWarpFNPCAction;
        Label lblWarpFNPCID;
        TextBox txtWarpFNPCID;
        Label lblWarpFNPCX;
        NumericUpDown nudWarpFNPCX;
        Label lblWarpFNPCY;
        NumericUpDown nudWarpFNPCY;
        #endregion
        #region Change FNPC Dir
        Panel pnlChangeFNPCDirAction;
        Label lblChangeFNPCDirID;
        TextBox txtChangeFNPCDirID;
        ComboBox cbxChangeFNPCDirDirection;
        #endregion
        #region Delete FNPC
        Panel pnlDeleteFNPCAction;
        Label lblDeleteFNPCID;
        TextBox txtDeleteFNPCID;
        #endregion
        #region Story Script
        Panel pnlStoryScriptAction;
        Label lblStoryScriptIndex;
        NumericUpDown nudStoryScriptIndex;
        Label lblStoryScriptParam1;
        TextBox txtStoryScriptParam1;
        Label lblStoryScriptParam2;
        TextBox txtStoryScriptParam2;
        Label lblStoryScriptParam3;
        TextBox txtStoryScriptParam3;
        CheckBox chkStoryScriptPause;
        #endregion
        #region Hide Players
        Panel pnlHidePlayersAction;
        #endregion
        #region Show Players
        Panel pnlShowPlayersAction;
        #endregion
        #region FNPC Emotion
        Panel pnlFNPCEmotionAction;
        Label lblFNPCEmotionID;
        TextBox txtFNPCEmotionID;
        Label lblFNPCEmotionNum;
        NumericUpDown nudFNPCEmotionNum;
        #endregion
        #region Weather
        Panel pnlWeatherAction;
        Label lblWeatherType;
        ComboBox cbxWeatherType;
        #endregion
        #region Hide NPCs
        Panel pnlHideNPCsAction;
        #endregion
        #region Show NPCs
        Panel pnlShowNPCsAction;
        #endregion
        #region Wait For Map
        Panel pnlWaitForMapAction;
        Label lblWaitForMapMap;
        TextBox txtWaitForMapMap;
        #endregion
        #region Wait For Loc
        Panel pnlWaitForLocAction;
        Label lblWaitForLocMap;
        TextBox txtWaitForLocMap;
        Label lblWaitForLocX;
        NumericUpDown nudWaitForLocX;
        Label lblWaitForLocY;
        NumericUpDown nudWaitForLocY;
        #endregion
        #region Ask Question
        Panel pnlAskQuestionAction;
        Label lblAskQuestionQuestion;
        TextBox txtAskQuestionQuestion;
        Label lblAskQuestionSegmentOnYes;
        NumericUpDown nudAskQuestionSegmentOnYes;
        Label lblAskQuestionSegmentOnNo;
        NumericUpDown nudAskQuestionSegmentOnNo;
        Label lblAskQuestionMugshot;
        NumericUpDown nudAskQuestionMugshot;
        PictureBox pbxAskQuestionMugshot;
        #endregion
        #region Go To Segment
        Panel pnlGoToSegmentAction;
        Label lblGoToSegmentSegment;
        NumericUpDown nudGoToSegmentSegment;
        #endregion
        #region Scroll Camera
        Panel pnlScrollCameraAction;
        Label lblScrollCameraX;
        NumericUpDown nudScrollCameraX;
        Label lblScrollCameraY;
        NumericUpDown nudScrollCameraY;
        Label lblScrollCameraSpeed;
        NumericUpDown nudScrollCameraSpeed;
        CheckBox chkScrollCameraPause;
        #endregion
        #region Reset Camera
        Panel pnlResetCameraAction;
        #endregion

        #region Move Player
        Panel pnlMovePlayerAction;
        Label lblMovePlayerX;
        NumericUpDown nudMovePlayerX;
        Label lblMovePlayerY;
        NumericUpDown nudMovePlayerY;
        Label lblMovePlayerSpeed;
        ComboBox cbxMovePlayerSpeed;
        CheckBox chkMovePlayerPause;
        #endregion
        #region Change Player Dir
        Panel pnlChangePlayerDirAction;
        ComboBox cbxChangePlayerDirDirection;
        #endregion

        #endregion

        #endregion Fields

        #region Constructors

        public winStoryPanel()
            : base("winStoryPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Story Editor";

            #region List
            pnlStoryList = new Panel("pnlStoryList");
            pnlStoryList.Size = new System.Drawing.Size(200, 230);
            pnlStoryList.Location = new Point(0, 0);
            pnlStoryList.BackColor = Color.White;
            pnlStoryList.Visible = true;

            lbxStoryList = new ListBox("lbxStoryList");
            lbxStoryList.Location = new Point(10, 10);
            lbxStoryList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                ListBoxTextItem lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": ");
                lbxStoryList.Items.Add(lbiItem);
            }
            lbxStoryList.SelectItem(0);

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
            #endregion

            #region General
            pnlEditorGeneral = new Panel("pnlEditorGeneral");
            pnlEditorGeneral.Size = new System.Drawing.Size(300, 400);
            pnlEditorGeneral.Location = new Point(0, 0);
            pnlEditorGeneral.BackColor = Color.White;
            pnlEditorGeneral.Visible = false;

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

            btnSegments = new Button("btnSegments");
            btnSegments.Location = new Point(215, 4);
            btnSegments.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnSegments.Size = new System.Drawing.Size(64, 16);
            btnSegments.Text = "Segments";
            btnSegments.Click += new EventHandler<MouseButtonEventArgs>(btnSegments_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Story Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 16);

            lblStoryStart = new Label("lblStoryStart");
            lblStoryStart.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblStoryStart.Text = "Only start if chapter is locked (0=disabled)";
            lblStoryStart.AutoSize = true;
            lblStoryStart.Location = new Point(10, 35);

            nudStoryStart = new NumericUpDown("nudStoryStart");
            nudStoryStart.Size = new System.Drawing.Size(100, 16);
            nudStoryStart.Location = new Point(10, 47);
            nudStoryStart.Maximum = MaxInfo.MaxStories;

            lblExitAndContinue = new Label("lblExitAndContinue");
            lblExitAndContinue.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblExitAndContinue.Text = "Checkpoint Segments";
            lblExitAndContinue.AutoSize = true;
            lblExitAndContinue.Location = new Point(10, 65);

            lbxExitAndContinue = new ListBox("lbxExitAndContinue");
            lbxExitAndContinue.Location = new Point(10, 77);
            lbxExitAndContinue.Size = new System.Drawing.Size(100, 100);
            lbxExitAndContinue.MultiSelect = false;

            btnAddExitAndContinue = new Button("btnAddExitAndContinue");
            btnAddExitAndContinue.Size = new System.Drawing.Size(64, 16);
            btnAddExitAndContinue.Location = new Point(110, 77);
            btnAddExitAndContinue.Text = "Add";
            btnAddExitAndContinue.Click += new EventHandler<MouseButtonEventArgs>(btnAddExitAndContinue_Click);

            nudExitAndContinueCheckpoint = new NumericUpDown("nudExitAndContinueCheckpoint");
            nudExitAndContinueCheckpoint.Location = new Point(110, 95);
            nudExitAndContinueCheckpoint.Maximum = 0;
            nudExitAndContinueCheckpoint.Size = new System.Drawing.Size(64, 14);

            btnRemoveExitAndContinue = new Button("btnRemoveExitAndContinue");
            btnRemoveExitAndContinue.Location = new Point(110, 110);
            btnRemoveExitAndContinue.Size = new System.Drawing.Size(64, 16);
            btnRemoveExitAndContinue.Text = "Remove";
            btnRemoveExitAndContinue.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveExitAndContinue_Click);

            #endregion

            #region Segments

            pnlEditorSegments = new Panel("pnlEditorSegments");
            pnlEditorSegments.Size = new System.Drawing.Size(300, 400);
            pnlEditorSegments.Location = new Point(0, 0);
            pnlEditorSegments.BackColor = Color.White;
            pnlEditorSegments.Visible = false;

            btnGeneral = new Button("btnGeneral");
            btnGeneral.Location = new Point(5, 5);
            btnGeneral.Size = new System.Drawing.Size(65, 15);
            btnGeneral.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnGeneral.Text = "General";
            btnGeneral.Click += new EventHandler<MouseButtonEventArgs>(btnGeneral_Click);

            //lblMaxSegments = new Label("lblMaxSegments");
            //lblMaxSegments.Location = new Point(75, 5);
            //lblMaxSegments.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            //lblMaxSegments.AutoSize = true;
            //lblMaxSegments.Text = "Max Segments:";

            //nudMaxSegments = new NumericUpDown("nudMaxSegments");
            //nudMaxSegments.Location = new Point(lblMaxSegments.X + lblMaxSegments.Width + 5, 5);
            //nudMaxSegments.Size = new System.Drawing.Size(64, 14);
            //nudMaxSegments.Minimum = 1;

            //btnSaveMaxSegments = new Button("btnSaveMaxSegments");
            //btnSaveMaxSegments.Location = new Point(nudMaxSegments.X + nudMaxSegments.Width + 5, 25);
            //btnSaveMaxSegments.Size = new System.Drawing.Size(64, 16);
            //btnSaveMaxSegments.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            //btnSaveMaxSegments.Text = "Save";
            //btnSaveMaxSegments.Click += new EventHandler<MouseButtonEventArgs>(btnSaveMaxSegments_Click);

            //lblActiveSegment = new Label("lblActiveSegment");
            //lblActiveSegment.Location = new Point(75, 5);
            //lblActiveSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            //lblActiveSegment.AutoSize = true;
            //lblActiveSegment.Text = "Segment:";

            //nudActiveSegment = new NumericUpDown("nudActiveSegment");
            //nudActiveSegment.Location = new Point(lblActiveSegment.X + lblActiveSegment.Width + 5, 25);
            //nudActiveSegment.Size = new System.Drawing.Size(64, 14);
            //nudActiveSegment.Minimum = 1;
            //nudActiveSegment.Maximum = 1;

            btnAddSegment = new Button("btnAddSegment");
            btnAddSegment.Size = new System.Drawing.Size(64, 16);
            btnAddSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddSegment.Location = new Point(5, 210);
            btnAddSegment.Text = "Add";
            btnAddSegment.Click += new EventHandler<MouseButtonEventArgs>(btnAddSegment_Click);

            btnRemoveSegment = new Button("btnRemoveSegment");
            btnRemoveSegment.Size = new System.Drawing.Size(64, 16);
            btnRemoveSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveSegment.Location = new Point(btnAddSegment.X + btnAddSegment.Width + 5, 210);
            btnRemoveSegment.Text = "Remove";
            btnRemoveSegment.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveSegment_Click);

            btnLoadSegment = new Button("btnLoadSegment");
            btnLoadSegment.Size = new System.Drawing.Size(64, 16);
            btnLoadSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadSegment.Location = new Point(btnRemoveSegment.X + btnRemoveSegment.Width + 5, 210);
            btnLoadSegment.Text = "Load";
            btnLoadSegment.Click += new EventHandler<MouseButtonEventArgs>(btnLoadSegment_Click);

            btnSaveSegment = new Button("btnSaveSegment");
            btnSaveSegment.Size = new System.Drawing.Size(64, 16);
            btnSaveSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnSaveSegment.Location = new Point(btnLoadSegment.X + btnLoadSegment.Width + 5, btnLoadSegment.Y);
            btnSaveSegment.Text = "Save";
            btnSaveSegment.Click += new EventHandler<MouseButtonEventArgs>(btnSaveSegment_Click);

            lblActions = new Label("lblActions");
            lblActions.Location = new Point(75, 5);
            lblActions.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            lblActions.AutoSize = true;
            lblActions.Text = "Action:";

            cmbSegmentTypes = new ComboBox("cmbSegmentTypes");
            cmbSegmentTypes.Location = new Point(lblActions.X + lblActions.Width + 5, 5);
            cmbSegmentTypes.Size = new System.Drawing.Size(150, 16);
            string[] storySegmentActions = Enum.GetNames(typeof(Enums.StoryAction));
            for (int i = 0; i < storySegmentActions.Length; i++) {
                cmbSegmentTypes.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), storySegmentActions[i]));
            }
            cmbSegmentTypes.ItemSelected += new EventHandler(cmbSegmentTypes_ItemSelected);
            cmbSegmentTypes.SelectItem(0);

            lbxSegments = new ListBox("lbxSegments");
            lbxSegments.Location = new Point(10, 230);
            lbxSegments.Size = new System.Drawing.Size(280, 140);
            lbxSegments.MultiSelect = false;

            #endregion

            #region List
            pnlStoryList.AddWidget(lbxStoryList);
            pnlStoryList.AddWidget(btnBack);
            pnlStoryList.AddWidget(btnForward);
            pnlStoryList.AddWidget(btnEdit);
            pnlStoryList.AddWidget(btnCancel);
            #endregion

            #region General
            pnlEditorGeneral.AddWidget(btnSegments);
            pnlEditorGeneral.AddWidget(lblName);
            pnlEditorGeneral.AddWidget(txtName);
            pnlEditorGeneral.AddWidget(lblStoryStart);
            pnlEditorGeneral.AddWidget(nudStoryStart);
            pnlEditorGeneral.AddWidget(lblExitAndContinue);
            pnlEditorGeneral.AddWidget(lbxExitAndContinue);
            pnlEditorGeneral.AddWidget(btnAddExitAndContinue);
            pnlEditorGeneral.AddWidget(nudExitAndContinueCheckpoint);
            pnlEditorGeneral.AddWidget(btnRemoveExitAndContinue);

            pnlEditorGeneral.AddWidget(btnEditorCancel);
            pnlEditorGeneral.AddWidget(btnEditorOK);
            #endregion

            #region Segments
            pnlEditorSegments.AddWidget(btnGeneral);
            //pnlEditorSegments.AddWidget(lblMaxSegments);
            //pnlEditorSegments.AddWidget(nudMaxSegments);
            //pnlEditorSegments.AddWidget(btnSaveMaxSegments);
            //pnlEditorSegments.AddWidget(lblActiveSegment);
            //pnlEditorSegments.AddWidget(nudActiveSegment);
            pnlEditorSegments.AddWidget(btnAddSegment);
            pnlEditorSegments.AddWidget(btnRemoveSegment);
            pnlEditorSegments.AddWidget(btnLoadSegment);
            pnlEditorSegments.AddWidget(btnSaveSegment);
            pnlEditorSegments.AddWidget(lblActions);
            pnlEditorSegments.AddWidget(cmbSegmentTypes);
            pnlEditorSegments.AddWidget(lbxSegments);
            #endregion

            this.AddWidget(pnlStoryList);
            this.AddWidget(pnlEditorGeneral);
            this.AddWidget(pnlEditorSegments);

            RefreshStoryList();
        }

        void btnRemoveExitAndContinue_Click(object sender, MouseButtonEventArgs e) {
            if (lbxExitAndContinue.SelectedItems.Count == 1) {
                story.ExitAndContinue.RemoveAt(lbxExitAndContinue.SelectedIndex);
                lbxExitAndContinue.Items.RemoveAt(lbxExitAndContinue.SelectedIndex);
            }
        }

        void btnAddExitAndContinue_Click(object sender, MouseButtonEventArgs e) {
            if (story.ExitAndContinue.Contains(nudExitAndContinueCheckpoint.Value) == false) {
                lbxExitAndContinue.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), nudExitAndContinueCheckpoint.Value.ToString()));
                story.ExitAndContinue.Add(nudExitAndContinueCheckpoint.Value);
            }
        }

        void btnSaveMaxSegments_Click(object sender, MouseButtonEventArgs e) {
            //nudActiveSegment.Maximum = nudMaxSegments.Value;

            //Logic.Editors.Stories.EditableStorySegment[] segmentsOld = new Logic.Editors.Stories.EditableStorySegment[story.Segments.Length];
            //Array.Copy(story.Segments, segmentsOld, story.Segments.Length);
            //story.Segments = new Logic.Editors.Stories.EditableStorySegment[nudMaxSegments.Value];
            //Array.Copy(segmentsOld, story.Segments, System.Math.Min(segmentsOld.Length, story.Segments.Length));
            //for (int i = segmentsOld.Length; i < story.Segments.Length; i++) {
            //    story.Segments[i] = new Logic.Editors.Stories.EditableStorySegment();
            //}

            //nudExitAndContinueCheckpoint.Maximum = story.Segments.Length;
            //if (nudAskQuestionSegmentOnNo != null) {
            //    nudAskQuestionSegmentOnNo.Maximum = story.Segments.Length;
            //}
            //if (nudAskQuestionSegmentOnYes != null) {
            //    nudAskQuestionSegmentOnYes.Maximum = story.Segments.Length;
            //}
            //if (nudGoToSegmentSegment != null) {
            //    nudGoToSegmentSegment.Maximum = story.Segments.Length;
            //}
        }

        #endregion Constructors

        #region Segment Loading/Saving
        void cmbSegmentTypes_ItemSelected(object sender, EventArgs e) {
            if (cmbSegmentTypes.SelectedIndex > -1) {
                Enums.StoryAction action = (Enums.StoryAction)Enum.Parse(typeof(Enums.StoryAction), cmbSegmentTypes.SelectedItem.TextIdentifier);
                switch (action) {
                    case Enums.StoryAction.Say:
                        SwitchToSayOptions();
                        break;
                    case Enums.StoryAction.Pause:
                        SwitchToPauseOptions();
                        break;
                    case Enums.StoryAction.MapVisibility:
                        SwitchToMapVisibilityOptions();
                        break;
                    case Enums.StoryAction.Padlock:
                        SwitchToPadlockOptions();
                        break;
                    case Enums.StoryAction.PlayMusic:
                        SwitchToPlayMusicOptions();
                        break;
                    case Enums.StoryAction.StopMusic:
                        SwitchToStopMusicOptions();
                        break;
                    case Enums.StoryAction.ShowImage:
                        SwitchToShowImageOptions();
                        break;
                    case Enums.StoryAction.HideImage:
                        SwitchToHideImageOptions();
                        break;
                    case Enums.StoryAction.Warp:
                        SwitchToWarpOptions();
                        break;
                    case Enums.StoryAction.PlayerPadlock:
                        SwitchToPlayerPadlockOptions();
                        break;
                    case Enums.StoryAction.ShowBackground:
                        SwitchToShowBackgroundOptions();
                        break;
                    case Enums.StoryAction.HideBackground:
                        SwitchToHideBackgroundOptions();
                        break;
                    case Enums.StoryAction.CreateFNPC:
                        SwitchToCreateFNPCOptions();
                        break;
                    case Enums.StoryAction.MoveFNPC:
                        SwitchToMoveFNPCOptions();
                        break;
                    case Enums.StoryAction.WarpFNPC:
                        SwitchToWarpFNPCOptions();
                        break;
                    case Enums.StoryAction.ChangeFNPCDir:
                        SwitchToChangeFNPCDirOptions();
                        break;
                    case Enums.StoryAction.DeleteFNPC:
                        SwitchToDeleteFNPCOptions();
                        break;
                    case Enums.StoryAction.RunScript:
                        SwitchToStoryScriptOptions();
                        break;
                    case Enums.StoryAction.HidePlayers:
                        SwitchToHidePlayersOptions();
                        break;
                    case Enums.StoryAction.ShowPlayers:
                        SwitchToShowPlayersOptions();
                        break;
                    case Enums.StoryAction.FNPCEmotion:
                        SwitchToFNPCEmotionOptions();
                        break;
                    case Enums.StoryAction.ChangeWeather:
                        SwitchToChangeWeatherOptions();
                        break;
                    case Enums.StoryAction.HideNPCs:
                        SwitchToHideNPCsOptions();
                        break;
                    case Enums.StoryAction.ShowNPCs:
                        SwitchToShowNPCsOptions();
                        break;
                    case Enums.StoryAction.WaitForMap:
                        SwitchToWaitForMapOptions();
                        break;
                    case Enums.StoryAction.WaitForLoc:
                        SwitchToWaitForLocOptions();
                        break;
                    case Enums.StoryAction.AskQuestion:
                        SwitchToAskQuestionOptions();
                        break;
                    case Enums.StoryAction.GoToSegment:
                        SwitchToGoToSegmentOptions();
                        break;
                    case Enums.StoryAction.ScrollCamera:
                        SwitchToScrollCameraOptions();
                        break;
                    case Enums.StoryAction.ResetCamera:
                        SwitchToResetCameraOptions();
                        break;
                    case Enums.StoryAction.MovePlayer:
                        SwitchToMovePlayerOptions();
                        break;
                    case Enums.StoryAction.ChangePlayerDir:
                        SwitchToChangePlayerDirOptions();
                        break;
                }
            }
        }

        void btnAddSegment_Click(object sender, MouseButtonEventArgs e)
        {
            int index = lbxSegments.SelectedIndex;

            if (index < 0 || index >= story.Segments.Count)
            {
                index = story.Segments.Count;
                story.Segments.Add(new Logic.Editors.Stories.EditableStorySegment());
                lbxSegments.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), "Adding..."));
                lbxSegments.SelectItem(index);
            }
            else
            {
                story.Segments.Insert(index, new Logic.Editors.Stories.EditableStorySegment());
            }



            Enums.StoryAction action = (Enums.StoryAction)Enum.Parse(typeof(Enums.StoryAction), cmbSegmentTypes.SelectedItem.TextIdentifier);
            switch (action)
            {
                case Enums.StoryAction.Say:
                    SaveSayOptions();
                    break;
                case Enums.StoryAction.Pause:
                    SavePauseOptions();
                    break;
                case Enums.StoryAction.MapVisibility:
                    SaveMapVisibilityOptions();
                    break;
                case Enums.StoryAction.Padlock:
                    SavePadlockOptions();
                    break;
                case Enums.StoryAction.PlayMusic:
                    SavePlayMusicOptions();
                    break;
                case Enums.StoryAction.StopMusic:
                    SaveStopMusicOptions();
                    break;
                case Enums.StoryAction.ShowImage:
                    SaveShowImageOptions();
                    break;
                case Enums.StoryAction.HideImage:
                    SaveHideImageOptions();
                    break;
                case Enums.StoryAction.Warp:
                    SaveWarpOptions();
                    break;
                case Enums.StoryAction.PlayerPadlock:
                    SavePlayerPadlockOptions();
                    break;
                case Enums.StoryAction.ShowBackground:
                    SaveShowBackgroundOptions();
                    break;
                case Enums.StoryAction.HideBackground:
                    SaveHideBackgroundOptions();
                    break;
                case Enums.StoryAction.CreateFNPC:
                    SaveCreateFNPCOptions();
                    break;
                case Enums.StoryAction.MoveFNPC:
                    SaveMoveFNPCOptions();
                    break;
                case Enums.StoryAction.WarpFNPC:
                    SaveWarpFNPCOptions();
                    break;
                case Enums.StoryAction.ChangeFNPCDir:
                    SaveChangeFNPCDirOptions();
                    break;
                case Enums.StoryAction.DeleteFNPC:
                    SaveDeleteFNPCOptions();
                    break;
                case Enums.StoryAction.RunScript:
                    SaveStoryScriptOptions();
                    break;
                case Enums.StoryAction.HidePlayers:
                    SaveHidePlayersOptions();
                    break;
                case Enums.StoryAction.ShowPlayers:
                    SaveShowPlayersOptions();
                    break;
                case Enums.StoryAction.FNPCEmotion:
                    SaveFNPCEmotionOptions();
                    break;
                case Enums.StoryAction.ChangeWeather:
                    SaveChangeWeatherOptions();
                    break;
                case Enums.StoryAction.HideNPCs:
                    SaveHideNPCsOptions();
                    break;
                case Enums.StoryAction.ShowNPCs:
                    SaveShowNPCsOptions();
                    break;
                case Enums.StoryAction.WaitForMap:
                    SaveWaitForMapOptions();
                    break;
                case Enums.StoryAction.WaitForLoc:
                    SaveWaitForLocOptions();
                    break;
                case Enums.StoryAction.AskQuestion:
                    SaveAskQuestionOptions();
                    break;
                case Enums.StoryAction.GoToSegment:
                    SaveGoToSegmentOptions();
                    break;
                case Enums.StoryAction.ScrollCamera:
                    SaveScrollCameraOptions();
                    break;
                case Enums.StoryAction.ResetCamera:
                    SaveResetCameraOptions();
                    break;
                case Enums.StoryAction.MovePlayer:
                    SaveMovePlayerOptions();
                    break;
                case Enums.StoryAction.ChangePlayerDir:
                    SaveChangePlayerDirOptions();
                    break;
            }

            RefreshSegmentList();

        }

        void btnRemoveSegment_Click(object sender, MouseButtonEventArgs e)
        {
            if (lbxSegments.SelectedIndex < 0 || lbxSegments.SelectedIndex >= story.Segments.Count)
            {
                return;
            }
            story.Segments.RemoveAt(lbxSegments.SelectedIndex);

            RefreshSegmentList();
        }

        void btnLoadSegment_Click(object sender, MouseButtonEventArgs e) {
            if (lbxSegments.SelectedIndex < 0 || lbxSegments.SelectedIndex >= story.Segments.Count)
            {
                return;
            }
            if (story.Segments[lbxSegments.SelectedIndex] == null) {
                story.Segments[lbxSegments.SelectedIndex] = new Logic.Editors.Stories.EditableStorySegment();
            }
            switch (story.Segments[lbxSegments.SelectedIndex].Action)
            {
                case Enums.StoryAction.Say:
                    LoadSayOptions();
                    break;
                case Enums.StoryAction.Pause:
                    LoadPauseOptions();
                    break;
                case Enums.StoryAction.MapVisibility:
                    LoadMapVisibilityOptions();
                    break;
                case Enums.StoryAction.Padlock:
                    LoadPadlockOptions();
                    break;
                case Enums.StoryAction.PlayMusic:
                    LoadPlayMusicOptions();
                    break;
                case Enums.StoryAction.StopMusic:
                    LoadStopMusicOptions();
                    break;
                case Enums.StoryAction.ShowImage:
                    LoadShowImageOptions();
                    break;
                case Enums.StoryAction.HideImage:
                    LoadHideImageOptions();
                    break;
                case Enums.StoryAction.Warp:
                    LoadWarpOptions();
                    break;
                case Enums.StoryAction.PlayerPadlock:
                    LoadPlayerPadlockOptions();
                    break;
                case Enums.StoryAction.ShowBackground:
                    LoadShowBackgroundOptions();
                    break;
                case Enums.StoryAction.HideBackground:
                    LoadHideBackgroundOptions();
                    break;
                case Enums.StoryAction.CreateFNPC:
                    LoadCreateFNPCOptions();
                    break;
                case Enums.StoryAction.MoveFNPC:
                    LoadMoveFNPCOptions();
                    break;
                case Enums.StoryAction.WarpFNPC:
                    LoadWarpFNPCOptions();
                    break;
                case Enums.StoryAction.ChangeFNPCDir:
                    LoadChangeFNPCDirOptions();
                    break;
                case Enums.StoryAction.DeleteFNPC:
                    LoadDeleteFNPCOptions();
                    break;
                case Enums.StoryAction.RunScript:
                    LoadStoryScriptOptions();
                    break;
                case Enums.StoryAction.HidePlayers:
                    LoadHidePlayersOptions();
                    break;
                case Enums.StoryAction.ShowPlayers:
                    LoadShowPlayersOptions();
                    break;
                case Enums.StoryAction.FNPCEmotion:
                    LoadFNPCEmotionOptions();
                    break;
                case Enums.StoryAction.ChangeWeather:
                    LoadChangeWeatherOptions();
                    break;
                case Enums.StoryAction.HideNPCs:
                    LoadHideNPCsOptions();
                    break;
                case Enums.StoryAction.ShowNPCs:
                    LoadShowNPCsOptions();
                    break;
                case Enums.StoryAction.WaitForMap:
                    LoadWaitForMapOptions();
                    break;
                case Enums.StoryAction.WaitForLoc:
                    LoadWaitForLocOptions();
                    break;
                case Enums.StoryAction.AskQuestion:
                    LoadAskQuestionOptions();
                    break;
                case Enums.StoryAction.GoToSegment:
                    LoadGoToSegmentOptions();
                    break;
                case Enums.StoryAction.ScrollCamera:
                    LoadScrollCameraOptions();
                    break;
                case Enums.StoryAction.ResetCamera:
                    LoadResetCameraOptions();
                    break;
                case Enums.StoryAction.MovePlayer:
                    LoadMovePlayerOptions();
                    break;
                case Enums.StoryAction.ChangePlayerDir:
                    LoadChangePlayerDirOptions();
                    break;
            }
        }

        void btnSaveSegment_Click(object sender, MouseButtonEventArgs e) {
            if (cmbSegmentTypes.SelectedItem == null) return;
            if (lbxSegments.SelectedIndex < 0 || lbxSegments.SelectedIndex >= story.Segments.Count) {
                return;
            }
            if (story.Segments[lbxSegments.SelectedIndex] == null) {
                story.Segments[lbxSegments.SelectedIndex] = new Logic.Editors.Stories.EditableStorySegment();
            }
            Enums.StoryAction action = (Enums.StoryAction)Enum.Parse(typeof(Enums.StoryAction), cmbSegmentTypes.SelectedItem.TextIdentifier);
            switch (action) {
                case Enums.StoryAction.Say:
                    SaveSayOptions();
                    break;
                case Enums.StoryAction.Pause:
                    SavePauseOptions();
                    break;
                case Enums.StoryAction.MapVisibility:
                    SaveMapVisibilityOptions();
                    break;
                case Enums.StoryAction.Padlock:
                    SavePadlockOptions();
                    break;
                case Enums.StoryAction.PlayMusic:
                    SavePlayMusicOptions();
                    break;
                case Enums.StoryAction.StopMusic:
                    SaveStopMusicOptions();
                    break;
                case Enums.StoryAction.ShowImage:
                    SaveShowImageOptions();
                    break;
                case Enums.StoryAction.HideImage:
                    SaveHideImageOptions();
                    break;
                case Enums.StoryAction.Warp:
                    SaveWarpOptions();
                    break;
                case Enums.StoryAction.PlayerPadlock:
                    SavePlayerPadlockOptions();
                    break;
                case Enums.StoryAction.ShowBackground:
                    SaveShowBackgroundOptions();
                    break;
                case Enums.StoryAction.HideBackground:
                    SaveHideBackgroundOptions();
                    break;
                case Enums.StoryAction.CreateFNPC:
                    SaveCreateFNPCOptions();
                    break;
                case Enums.StoryAction.MoveFNPC:
                    SaveMoveFNPCOptions();
                    break;
                case Enums.StoryAction.WarpFNPC:
                    SaveWarpFNPCOptions();
                    break;
                case Enums.StoryAction.ChangeFNPCDir:
                    SaveChangeFNPCDirOptions();
                    break;
                case Enums.StoryAction.DeleteFNPC:
                    SaveDeleteFNPCOptions();
                    break;
                case Enums.StoryAction.RunScript:
                    SaveStoryScriptOptions();
                    break;
                case Enums.StoryAction.HidePlayers:
                    SaveHidePlayersOptions();
                    break;
                case Enums.StoryAction.ShowPlayers:
                    SaveShowPlayersOptions();
                    break;
                case Enums.StoryAction.FNPCEmotion:
                    SaveFNPCEmotionOptions();
                    break;
                case Enums.StoryAction.ChangeWeather:
                    SaveChangeWeatherOptions();
                    break;
                case Enums.StoryAction.HideNPCs:
                    SaveHideNPCsOptions();
                    break;
                case Enums.StoryAction.ShowNPCs:
                    SaveShowNPCsOptions();
                    break;
                case Enums.StoryAction.WaitForMap:
                    SaveWaitForMapOptions();
                    break;
                case Enums.StoryAction.WaitForLoc:
                    SaveWaitForLocOptions();
                    break;
                case Enums.StoryAction.AskQuestion:
                    SaveAskQuestionOptions();
                    break;
                case Enums.StoryAction.GoToSegment:
                    SaveGoToSegmentOptions();
                    break;
                case Enums.StoryAction.ScrollCamera:
                    SaveScrollCameraOptions();
                    break;
                case Enums.StoryAction.ResetCamera:
                    SaveResetCameraOptions();
                    break;
                case Enums.StoryAction.MovePlayer:
                    SaveMovePlayerOptions();
                    break;
                case Enums.StoryAction.ChangePlayerDir:
                    SaveChangePlayerDirOptions();
                    break;
            }

            RefreshSegmentList();
        }

        public void RefreshSegmentList()
        {
            lbxSegments.Items.Clear();
            for (int i = 0; i < story.Segments.Count; i++)
            {
                lbxSegments.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + story.Segments[i].Action.ToString()));
            }
        }

        void HideAllOptionPanels() {
            HideIfLoaded(pnlSayAction);
            HideIfLoaded(pnlPauseAction);
            HideIfLoaded(pnlMapVisibilityAction);
            HideIfLoaded(pnlPadlockAction);
            HideIfLoaded(pnlPlayMusicAction);
            HideIfLoaded(pnlStopMusicAction);
            HideIfLoaded(pnlShowImageAction);
            HideIfLoaded(pnlHideImageAction);
            HideIfLoaded(pnlWarpAction);
            HideIfLoaded(pnlPlayerPadlockAction);
            HideIfLoaded(pnlShowBackgroundAction);
            HideIfLoaded(pnlHideBackgroundAction);
            HideIfLoaded(pnlCreateFNPCAction);
            HideIfLoaded(pnlMoveFNPCAction);
            HideIfLoaded(pnlWarpFNPCAction);
            HideIfLoaded(pnlChangeFNPCDirAction);
            HideIfLoaded(pnlDeleteFNPCAction);
            HideIfLoaded(pnlStoryScriptAction);
            HideIfLoaded(pnlHidePlayersAction);
            HideIfLoaded(pnlShowPlayersAction);
            HideIfLoaded(pnlFNPCEmotionAction);
            HideIfLoaded(pnlWeatherAction);
            HideIfLoaded(pnlHideNPCsAction);
            HideIfLoaded(pnlShowNPCsAction);
            HideIfLoaded(pnlWaitForMapAction);
            HideIfLoaded(pnlWaitForLocAction);
            HideIfLoaded(pnlAskQuestionAction);
            HideIfLoaded(pnlGoToSegmentAction);
            HideIfLoaded(pnlScrollCameraAction);
            HideIfLoaded(pnlResetCameraAction);
            HideIfLoaded(pnlMovePlayerAction);
            HideIfLoaded(pnlChangePlayerDirAction);
        }
        #endregion

        #region Action Widget Loading
        #region Say
        public void SwitchToSayOptions() {
            if (pnlSayAction == null) {
                pnlSayAction = new Panel("pnlSayAction");
                pnlSayAction.Size = new Size(300, 180);
                pnlSayAction.Location = new Point(0, 30);
                pnlSayAction.BackColor = Color.Transparent;
                pnlSayAction.Hide();

                lblSayText = new Label("lblSayText");
                lblSayText.Location = new Point(5, 5);
                lblSayText.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblSayText.AutoSize = true;
                lblSayText.Text = "Text:";

                txtSayText = new TextBox("txtSayText");
                txtSayText.Location = new Point(75, 5);
                txtSayText.Size = new System.Drawing.Size(200, 16);

                lblSayMugshot = new Label("lblSayMugshot");
                lblSayMugshot.Location = new Point(5, 25);
                lblSayMugshot.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblSayMugshot.AutoSize = true;
                lblSayMugshot.Text = "Mugshot:";

                nudSayMugshot = new NumericUpDown("nudSayMugshot");
                nudSayMugshot.Location = new Point(75, 25);
                nudSayMugshot.Size = new System.Drawing.Size(100, 14);
                nudSayMugshot.Maximum = Int32.MaxValue;
                nudSayMugshot.Minimum = -1;
                nudSayMugshot.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudSayMugshot_ValueChanged);

                pbxSayMugshot = new PictureBox("pbxSayMugshot");
                pbxSayMugshot.Location = new Point(nudSayMugshot.X + nudSayMugshot.Width + 5, 25);
                pbxSayMugshot.Size = new System.Drawing.Size(40, 40);

                lblSaySpeed = new Label("lblSaySpeed");
                lblSaySpeed.Location = new Point(5, 45);
                lblSaySpeed.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblSaySpeed.AutoSize = true;
                lblSaySpeed.Text = "Speed:";

                nudSaySpeed = new NumericUpDown("nudSaySpeed");
                nudSaySpeed.Location = new Point(75, 45);
                nudSaySpeed.Size = new System.Drawing.Size(100, 14);
                nudSaySpeed.Maximum = Int32.MaxValue;

                lblSayPause = new Label("lblSayPause");
                lblSayPause.Location = new Point(5, 65);
                lblSayPause.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblSayPause.AutoSize = true;
                lblSayPause.Text = "Pause:";

                nudSayPause = new NumericUpDown("nudSayPause");
                nudSayPause.Location = new Point(75, 65);
                nudSayPause.Size = new System.Drawing.Size(100, 14);
                nudSayPause.Maximum = Int32.MaxValue;

                pnlSayAction.AddWidget(lblSayText);
                pnlSayAction.AddWidget(txtSayText);
                pnlSayAction.AddWidget(lblSayMugshot);
                pnlSayAction.AddWidget(nudSayMugshot);
                pnlSayAction.AddWidget(pbxSayMugshot);
                pnlSayAction.AddWidget(lblSaySpeed);
                pnlSayAction.AddWidget(nudSaySpeed);
                pnlSayAction.AddWidget(lblSayPause);
                pnlSayAction.AddWidget(nudSayPause);

                pnlEditorSegments.AddWidget(pnlSayAction);
            }

            txtSayText.Text = "";
            HideAllOptionPanels();
            pnlSayAction.Show();
        }

        void nudSayMugshot_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (e.NewValue > 0) {
                Mugshot mugshot = Logic.Graphics.GraphicsManager.GetMugshot(e.NewValue, 0, 0, 0);
                if (mugshot != null) {
                    pbxSayMugshot.Image = mugshot.GetEmote(0);
                } else {
                    pbxSayMugshot.Image = null;
                }
            } else {
                pbxSayMugshot.Image = null;
            }
        }

        public void SaveSayOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.Say;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Text", txtSayText.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Mugshot", nudSayMugshot.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Speed", nudSaySpeed.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("PauseLocation", nudSayPause.Value.ToString());
        }

        public void LoadSayOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtSayText.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Text") ?? "";
            nudSayMugshot.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Mugshot").ToInt();
            nudSaySpeed.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Speed").ToInt();
            nudSayPause.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("PauseLocation").ToInt();
        }
        #endregion
        #region Pause
        public void SwitchToPauseOptions() {
            if (pnlPauseAction == null) {
                pnlPauseAction = new Panel("pnlPauseAction");
                pnlPauseAction.Size = new Size(300, 180);
                pnlPauseAction.Location = new Point(0, 30);
                pnlPauseAction.BackColor = Color.Transparent;
                pnlPauseAction.Hide();

                lblPauseLength = new Label("lblPauseLength");
                lblPauseLength.Location = new Point(5, 5);
                lblPauseLength.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblPauseLength.AutoSize = true;
                lblPauseLength.Text = "Length:";

                nudPauseLength = new NumericUpDown("nudPauseLength");
                nudPauseLength.Location = new Point(75, 5);
                nudPauseLength.Size = new System.Drawing.Size(100, 14);
                nudPauseLength.Maximum = Int32.MaxValue;

                pnlPauseAction.AddWidget(lblPauseLength);
                pnlPauseAction.AddWidget(nudPauseLength);

                pnlEditorSegments.AddWidget(pnlPauseAction);
            }

            HideAllOptionPanels();
            pnlPauseAction.Show();
        }

        public void SavePauseOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.Pause;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Length", nudPauseLength.Value.ToString());
        }

        public void LoadPauseOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            nudPauseLength.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Length").ToInt();
        }
        #endregion
        #region Map Visibility
        public void SwitchToMapVisibilityOptions() {
            if (pnlMapVisibilityAction == null) {
                pnlMapVisibilityAction = new Panel("pnlMapVisibilityAction");
                pnlMapVisibilityAction.Size = new Size(300, 180);
                pnlMapVisibilityAction.Location = new Point(0, 30);
                pnlMapVisibilityAction.BackColor = Color.Transparent;
                pnlMapVisibilityAction.Hide();

                chkMapVisibilityVisible = new CheckBox("chkMapVisibilityVisible");
                chkMapVisibilityVisible.Location = new Point(5, 5);
                chkMapVisibilityVisible.Size = new System.Drawing.Size(100, 14);
                chkMapVisibilityVisible.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkMapVisibilityVisible.BackColor = Color.Transparent;
                chkMapVisibilityVisible.Text = "Visible";

                pnlMapVisibilityAction.AddWidget(chkMapVisibilityVisible);

                pnlEditorSegments.AddWidget(pnlMapVisibilityAction);
            }

            HideAllOptionPanels();
            pnlMapVisibilityAction.Show();
        }

        public void SaveMapVisibilityOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.MapVisibility;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Visible", chkMapVisibilityVisible.Checked.ToString());
        }

        public void LoadMapVisibilityOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            chkMapVisibilityVisible.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Visible").ToBool();
        }
        #endregion
        #region Padlock
        public void SwitchToPadlockOptions() {
            if (pnlPadlockAction == null) {
                pnlPadlockAction = new Panel("pnlPadlockAction");
                pnlPadlockAction.Size = new Size(300, 180);
                pnlPadlockAction.Location = new Point(0, 30);
                pnlPadlockAction.BackColor = Color.Transparent;
                pnlPadlockAction.Hide();

                chkPadlockState = new CheckBox("chkPadlockState");
                chkPadlockState.Location = new Point(5, 5);
                chkPadlockState.Size = new System.Drawing.Size(100, 14);
                chkPadlockState.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkPadlockState.BackColor = Color.Transparent;
                chkPadlockState.Text = "Lock";

                pnlPadlockAction.AddWidget(chkPadlockState);

                pnlEditorSegments.AddWidget(pnlPadlockAction);
            }

            HideAllOptionPanels();
            pnlPadlockAction.Show();
        }

        public void SavePadlockOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.Padlock;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("State", chkPadlockState.Checked ? "Lock" : "Unlock");
        }

        public void LoadPadlockOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            chkPadlockState.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("State") == "Lock";
        }
        #endregion
        #region Play Music
        public void SwitchToPlayMusicOptions() {
            if (pnlPlayMusicAction == null) {
                pnlPlayMusicAction = new Panel("pnlPlayMusicAction");
                pnlPlayMusicAction.Size = new Size(300, 180);
                pnlPlayMusicAction.Location = new Point(0, 30);
                pnlPlayMusicAction.BackColor = Color.Transparent;
                pnlPlayMusicAction.Hide();

                lbxPlayMusicPicker = new ListBox("lbxPlayMusicPicker");
                lbxPlayMusicPicker.Location = new Point(5, 5);
                lbxPlayMusicPicker.Size = new System.Drawing.Size(200, 100);
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                string[] musicFiles = System.IO.Directory.GetFiles(IO.Paths.MusicPath);
                for (int i = 0; i < musicFiles.Length; i++) {
                    lbxPlayMusicPicker.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(musicFiles[i])));
                }

                btnPlayMusicPlay = new Button("btnPlayMusicPlay");
                btnPlayMusicPlay.Location = new Point(210, 5);
                btnPlayMusicPlay.Size = new System.Drawing.Size(64, 15);
                btnPlayMusicPlay.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                btnPlayMusicPlay.Text = "Play";
                btnPlayMusicPlay.Click += new EventHandler<MouseButtonEventArgs>(btnPlayMusicPlay_Click);

                btnPlayMusicStop = new Button("btnPlayMusicStop");
                btnPlayMusicStop.Location = new Point(210, 25);
                btnPlayMusicStop.Size = new System.Drawing.Size(64, 15);
                btnPlayMusicStop.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                btnPlayMusicStop.Text = "Stop";
                btnPlayMusicStop.Click += new EventHandler<MouseButtonEventArgs>(btnPlayMusicStop_Click);

                chkPlayMusicHonorSettings = new CheckBox("chkPlayMusicHonorSettings");
                chkPlayMusicHonorSettings.Location = new Point(5, 105);
                chkPlayMusicHonorSettings.Size = new System.Drawing.Size(100, 14);
                chkPlayMusicHonorSettings.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkPlayMusicHonorSettings.BackColor = Color.Transparent;
                chkPlayMusicHonorSettings.Text = "Honor Settings";

                chkPlayMusicLoop = new CheckBox("chkPlayMusicLoop");
                chkPlayMusicLoop.Location = new Point(5, 125);
                chkPlayMusicLoop.Size = new System.Drawing.Size(100, 14);
                chkPlayMusicLoop.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkPlayMusicLoop.BackColor = Color.Transparent;
                chkPlayMusicLoop.Text = "Loop";

                pnlPlayMusicAction.AddWidget(lbxPlayMusicPicker);
                pnlPlayMusicAction.AddWidget(btnPlayMusicPlay);
                pnlPlayMusicAction.AddWidget(btnPlayMusicStop);
                pnlPlayMusicAction.AddWidget(chkPlayMusicHonorSettings);
                pnlPlayMusicAction.AddWidget(chkPlayMusicLoop);

                pnlEditorSegments.AddWidget(pnlPlayMusicAction);
            }

            HideAllOptionPanels();
            pnlPlayMusicAction.Show();
        }

        public void SavePlayMusicOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.PlayMusic;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            if (lbxPlayMusicPicker.SelectedItems.Count == 1) {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", ((ListBoxTextItem)lbxPlayMusicPicker.SelectedItems[0]).Text);
            } else {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", "");
            }
            story.Segments[lbxSegments.SelectedIndex].AddParameter("HonorSettings", chkPlayMusicHonorSettings.Checked.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Loop", chkPlayMusicLoop.Checked.ToString());
        }

        public void LoadPlayMusicOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            lbxPlayMusicPicker.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("File"));
            chkPlayMusicHonorSettings.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("HonorSettings").ToBool();
            chkPlayMusicLoop.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Loop").ToBool();
        }

        void btnPlayMusicPlay_Click(object sender, MouseButtonEventArgs e) {
            if (lbxPlayMusicPicker.SelectedItems.Count == 1) {
                Logic.Music.Music.AudioPlayer.PlayMusic(((ListBoxTextItem)lbxPlayMusicPicker.SelectedItems[0]).Text, 1, true, false);
            }
        }

        void btnPlayMusicStop_Click(object sender, MouseButtonEventArgs e) {
            Logic.Music.Music.AudioPlayer.StopMusic();
        }
        #endregion
        #region Stop Music
        public void SwitchToStopMusicOptions() {
            if (pnlStopMusicAction == null) {
                pnlStopMusicAction = new Panel("pnlStopMusicAction");
                pnlStopMusicAction.Size = new Size(300, 180);
                pnlStopMusicAction.Location = new Point(0, 30);
                pnlStopMusicAction.BackColor = Color.Transparent;
                pnlStopMusicAction.Hide();

                pnlEditorSegments.AddWidget(pnlStopMusicAction);
            }

            HideAllOptionPanels();
            pnlStopMusicAction.Show();
        }

        public void SaveStopMusicOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.StopMusic;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadStopMusicOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region Show Image
        public void SwitchToShowImageOptions() {
            if (pnlShowImageAction == null) {
                pnlShowImageAction = new Panel("pnlShowImageAction");
                pnlShowImageAction.Size = new Size(300, 180);
                pnlShowImageAction.Location = new Point(0, 30);
                pnlShowImageAction.BackColor = Color.Transparent;
                pnlShowImageAction.Hide();

                lbxShowImageFiles = new ListBox("lbxShowImageFiles");
                lbxShowImageFiles.Location = new Point(5, 5);
                lbxShowImageFiles.Size = new System.Drawing.Size(200, 100);
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                string[] imageFiles = System.IO.Directory.GetFiles(IO.Paths.StartupPath + "Story/Images/");
                for (int i = 0; i < imageFiles.Length; i++) {
                    lbxShowImageFiles.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(imageFiles[i])));
                }

                lblShowImageID = new Label("lblShowImageID");
                lblShowImageID.Location = new Point(5, 105);
                lblShowImageID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblShowImageID.AutoSize = true;
                lblShowImageID.Text = "Image ID:";

                txtShowImageID = new TextBox("txtShowImageID");
                txtShowImageID.Location = new Point(75, 105);
                txtShowImageID.Size = new System.Drawing.Size(125, 15);

                lblShowImageX = new Label("lblShowImageX");
                lblShowImageX.Location = new Point(5, 125);
                lblShowImageX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblShowImageX.AutoSize = true;
                lblShowImageX.Text = "Image X:";

                nudShowImageX = new NumericUpDown("nudShowImageX");
                nudShowImageX.Location = new Point(75, 125);
                nudShowImageX.Size = new System.Drawing.Size(125, 15);
                nudShowImageX.Maximum = 640;

                lblShowImageY = new Label("lblShowImageY");
                lblShowImageY.Location = new Point(5, 145);
                lblShowImageY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblShowImageY.AutoSize = true;
                lblShowImageY.Text = "Image Y:";

                nudShowImageY = new NumericUpDown("nudShowImageY");
                nudShowImageY.Location = new Point(75, 145);
                nudShowImageY.Size = new System.Drawing.Size(125, 15);
                nudShowImageY.Maximum = 480;

                pnlShowImageAction.AddWidget(lbxShowImageFiles);
                pnlShowImageAction.AddWidget(lblShowImageID);
                pnlShowImageAction.AddWidget(txtShowImageID);
                pnlShowImageAction.AddWidget(lblShowImageX);
                pnlShowImageAction.AddWidget(nudShowImageX);
                pnlShowImageAction.AddWidget(lblShowImageY);
                pnlShowImageAction.AddWidget(nudShowImageY);

                pnlEditorSegments.AddWidget(pnlShowImageAction);
            }

            HideAllOptionPanels();
            pnlShowImageAction.Show();
        }

        public void SaveShowImageOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ShowImage;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            if (lbxShowImageFiles.SelectedItems.Count == 1) {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", ((ListBoxTextItem)lbxShowImageFiles.SelectedItems[0]).Text);
            } else {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", "");
            }
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ImageID", txtShowImageID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudShowImageX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudShowImageY.Value.ToString());
        }

        public void LoadShowImageOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            lbxShowImageFiles.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("File"));
            txtShowImageID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ImageID") ?? "";
            nudShowImageX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudShowImageY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
        }
        #endregion
        #region Hide Image
        public void SwitchToHideImageOptions() {
            if (pnlHideImageAction == null) {
                pnlHideImageAction = new Panel("pnlHideImageAction");
                pnlHideImageAction.Size = new Size(300, 180);
                pnlHideImageAction.Location = new Point(0, 30);
                pnlHideImageAction.BackColor = Color.Transparent;
                pnlHideImageAction.Hide();

                lblHideImageID = new Label("lblHideImageID");
                lblHideImageID.Location = new Point(5, 5);
                lblHideImageID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblHideImageID.AutoSize = true;
                lblHideImageID.Text = "Image ID:";

                txtHideImageID = new TextBox("txtHideImageID");
                txtHideImageID.Location = new Point(75, 5);
                txtHideImageID.Size = new System.Drawing.Size(125, 15);

                pnlHideImageAction.AddWidget(lblHideImageID);
                pnlHideImageAction.AddWidget(txtHideImageID);

                pnlEditorSegments.AddWidget(pnlHideImageAction);
            }

            HideAllOptionPanels();
            pnlHideImageAction.Show();
        }

        public void SaveHideImageOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.HideImage;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ImageID", txtHideImageID.Text);
        }

        public void LoadHideImageOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtHideImageID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ImageID") ?? "";
        }
        #endregion
        #region Warp
        public void SwitchToWarpOptions() {
            if (pnlWarpAction == null) {
                pnlWarpAction = new Panel("pnlWarpAction");
                pnlWarpAction.Size = new Size(300, 180);
                pnlWarpAction.Location = new Point(0, 30);
                pnlWarpAction.BackColor = Color.Transparent;
                pnlWarpAction.Hide();

                lblWarpMap = new Label("lblWarpMap");
                lblWarpMap.Location = new Point(5, 5);
                lblWarpMap.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpMap.AutoSize = true;
                lblWarpMap.Text = "Map:";

                txtWarpMap = new TextBox("txtWarpMap");
                txtWarpMap.Location = new Point(75, 5);
                txtWarpMap.Size = new System.Drawing.Size(125, 15);

                lblWarpX = new Label("lblWarpX");
                lblWarpX.Location = new Point(5, 25);
                lblWarpX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpX.AutoSize = true;
                lblWarpX.Text = "X:";

                nudWarpX = new NumericUpDown("nudWarpX");
                nudWarpX.Location = new Point(75, 25);
                nudWarpX.Size = new System.Drawing.Size(125, 15);
                nudWarpX.Maximum = 50;

                lblWarpY = new Label("lblWarpY");
                lblWarpY.Location = new Point(5, 45);
                lblWarpY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpY.AutoSize = true;
                lblWarpY.Text = "Y:";

                nudWarpY = new NumericUpDown("nudWarpY");
                nudWarpY.Location = new Point(75, 45);
                nudWarpY.Size = new System.Drawing.Size(125, 15);
                nudWarpY.Maximum = 50;

                pnlWarpAction.AddWidget(lblWarpMap);
                pnlWarpAction.AddWidget(txtWarpMap);
                pnlWarpAction.AddWidget(lblWarpX);
                pnlWarpAction.AddWidget(nudWarpX);
                pnlWarpAction.AddWidget(lblWarpY);
                pnlWarpAction.AddWidget(nudWarpY);

                pnlEditorSegments.AddWidget(pnlWarpAction);
            }

            HideAllOptionPanels();
            pnlWarpAction.Show();
        }

        public void SaveWarpOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.Warp;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("MapID", txtWarpMap.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudWarpX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudWarpY.Value.ToString());
        }

        public void LoadWarpOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtWarpMap.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("MapID") ?? "";
            nudWarpX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudWarpY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
        }
        #endregion
        #region Player Padlock
        public void SwitchToPlayerPadlockOptions() {
            if (pnlPlayerPadlockAction == null) {
                pnlPlayerPadlockAction = new Panel("pnlPlayerPadlockAction");
                pnlPlayerPadlockAction.Size = new Size(300, 180);
                pnlPlayerPadlockAction.Location = new Point(0, 30);
                pnlPlayerPadlockAction.BackColor = Color.Transparent;
                pnlPlayerPadlockAction.Hide();

                chkPlayerPadlockState = new CheckBox("chkPlayerPadlockState");
                chkPlayerPadlockState.Location = new Point(5, 5);
                chkPlayerPadlockState.Size = new System.Drawing.Size(100, 14);
                chkPlayerPadlockState.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkPlayerPadlockState.BackColor = Color.Transparent;
                chkPlayerPadlockState.Text = "Lock";

                pnlPlayerPadlockAction.AddWidget(chkPlayerPadlockState);

                pnlEditorSegments.AddWidget(pnlPlayerPadlockAction);
            }

            HideAllOptionPanels();
            pnlPlayerPadlockAction.Show();
        }

        public void SavePlayerPadlockOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.PlayerPadlock;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("MovementState", chkPlayerPadlockState.Checked ? "Lock" : "Unlock");
        }

        public void LoadPlayerPadlockOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            chkPlayerPadlockState.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("MovementState") == "Lock";
        }
        #endregion
        #region Show Background
        public void SwitchToShowBackgroundOptions() {
            if (pnlShowBackgroundAction == null) {
                pnlShowBackgroundAction = new Panel("pnlShowBackgroundAction");
                pnlShowBackgroundAction.Size = new Size(300, 180);
                pnlShowBackgroundAction.Location = new Point(0, 30);
                pnlShowBackgroundAction.BackColor = Color.Transparent;
                pnlShowBackgroundAction.Hide();

                lbxShowBackgroundFiles = new ListBox("lbxShowBackgroundFiles");
                lbxShowBackgroundFiles.Location = new Point(5, 5);
                lbxShowBackgroundFiles.Size = new System.Drawing.Size(200, 100);
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                string[] imageFiles = System.IO.Directory.GetFiles(IO.Paths.StartupPath + "Story/Backgrounds/");
                for (int i = 0; i < imageFiles.Length; i++) {
                    lbxShowBackgroundFiles.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(imageFiles[i])));
                }

                pnlShowBackgroundAction.AddWidget(lbxShowImageFiles);

                pnlEditorSegments.AddWidget(pnlShowBackgroundAction);
            }

            HideAllOptionPanels();
            pnlShowBackgroundAction.Show();
        }

        public void SaveShowBackgroundOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ShowBackground;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            if (lbxShowBackgroundFiles.SelectedItems.Count == 1) {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", ((ListBoxTextItem)lbxShowBackgroundFiles.SelectedItems[0]).Text);
            } else {
                story.Segments[lbxSegments.SelectedIndex].AddParameter("File", "");
            }
        }

        public void LoadShowBackgroundOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            lbxShowBackgroundFiles.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("File"));
        }
        #endregion
        #region Hide Background
        public void SwitchToHideBackgroundOptions() {
            if (pnlHideBackgroundAction == null) {
                pnlHideBackgroundAction = new Panel("pnlHideBackgroundAction");
                pnlHideBackgroundAction.Size = new Size(300, 180);
                pnlHideBackgroundAction.Location = new Point(0, 30);
                pnlHideBackgroundAction.BackColor = Color.Transparent;
                pnlHideBackgroundAction.Hide();

                pnlEditorSegments.AddWidget(pnlHideBackgroundAction);
            }

            HideAllOptionPanels();
            pnlHideBackgroundAction.Show();
        }

        public void SaveHideBackgroundOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.HideBackground;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadHideBackgroundOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region Create FNPC
        public void SwitchToCreateFNPCOptions() {
            if (pnlCreateFNPCAction == null) {
                pnlCreateFNPCAction = new Panel("pnlCreateFNPCAction");
                pnlCreateFNPCAction.Size = new Size(300, 180);
                pnlCreateFNPCAction.Location = new Point(0, 30);
                pnlCreateFNPCAction.BackColor = Color.Transparent;
                pnlCreateFNPCAction.Hide();

                lblCreateFNPCID = new Label("lblCreateFNPCID");
                lblCreateFNPCID.Location = new Point(5, 5);
                lblCreateFNPCID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblCreateFNPCID.AutoSize = true;
                lblCreateFNPCID.Text = "ID:";

                txtCreateFNPCID = new TextBox("txtCreateFNPCID");
                txtCreateFNPCID.Location = new Point(75, 5);
                txtCreateFNPCID.Size = new System.Drawing.Size(125, 15);

                lblCreateFNPCMap = new Label("lblCreateFNPCMap");
                lblCreateFNPCMap.Location = new Point(5, 25);
                lblCreateFNPCMap.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblCreateFNPCMap.AutoSize = true;
                lblCreateFNPCMap.Text = "Map:";

                txtCreateFNPCMap = new TextBox("txtCreateFNPCMap");
                txtCreateFNPCMap.Location = new Point(75, 25);
                txtCreateFNPCMap.Size = new System.Drawing.Size(125, 15);

                lblCreateFNPCX = new Label("lblCreateFNPCX");
                lblCreateFNPCX.Location = new Point(5, 45);
                lblCreateFNPCX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblCreateFNPCX.AutoSize = true;
                lblCreateFNPCX.Text = "X:";

                nudCreateFNPCX = new NumericUpDown("nudCreateFNPCX");
                nudCreateFNPCX.Location = new Point(75, 45);
                nudCreateFNPCX.Size = new System.Drawing.Size(125, 15);
                nudCreateFNPCX.Maximum = 50;

                lblCreateFNPCY = new Label("lblCreateFNPCY");
                lblCreateFNPCY.Location = new Point(5, 65);
                lblCreateFNPCY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblCreateFNPCY.AutoSize = true;
                lblCreateFNPCY.Text = "Y:";

                nudCreateFNPCY = new NumericUpDown("nudCreateFNPCY");
                nudCreateFNPCY.Location = new Point(75, 65);
                nudCreateFNPCY.Size = new System.Drawing.Size(125, 15);
                nudCreateFNPCY.Maximum = 50;

                lblCreateFNPCSprite = new Label("lblCreateFNPCSprite");
                lblCreateFNPCSprite.Location = new Point(5, 85);
                lblCreateFNPCSprite.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblCreateFNPCSprite.AutoSize = true;
                lblCreateFNPCSprite.Text = "Sprite:";

                nudCreateFNPCSprite = new NumericUpDown("nudCreateFNPCSprite");
                nudCreateFNPCSprite.Location = new Point(75, 85);
                nudCreateFNPCSprite.Size = new System.Drawing.Size(125, 15);
                nudCreateFNPCSprite.Maximum = Int32.MaxValue;
                nudCreateFNPCSprite.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudCreateFNPCSprite_ValueChanged);
                nudCreateFNPCSprite.Minimum = 1;

                pbxCreateFNPCSprite = new PictureBox("pbxCreateFNPCSprite");
                pbxCreateFNPCSprite.Location = new Point(205, 85);

                pnlCreateFNPCAction.AddWidget(lblCreateFNPCID);
                pnlCreateFNPCAction.AddWidget(txtCreateFNPCID);
                pnlCreateFNPCAction.AddWidget(lblCreateFNPCMap);
                pnlCreateFNPCAction.AddWidget(txtCreateFNPCMap);
                pnlCreateFNPCAction.AddWidget(lblCreateFNPCX);
                pnlCreateFNPCAction.AddWidget(nudCreateFNPCX);
                pnlCreateFNPCAction.AddWidget(lblCreateFNPCY);
                pnlCreateFNPCAction.AddWidget(nudCreateFNPCY);
                pnlCreateFNPCAction.AddWidget(lblCreateFNPCSprite);
                pnlCreateFNPCAction.AddWidget(nudCreateFNPCSprite);
                pnlCreateFNPCAction.AddWidget(pbxCreateFNPCSprite);

                pnlEditorSegments.AddWidget(pnlCreateFNPCAction);
            }

            HideAllOptionPanels();
            pnlCreateFNPCAction.Show();
        }

        public void SaveCreateFNPCOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.CreateFNPC;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtCreateFNPCID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ParentMapID", txtCreateFNPCMap.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudCreateFNPCX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudCreateFNPCY.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Sprite", nudCreateFNPCSprite.Value.ToString());
        }

        public void LoadCreateFNPCOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtCreateFNPCID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
            txtCreateFNPCMap.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ParentMapID") ?? "";
            nudCreateFNPCX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudCreateFNPCY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
            nudCreateFNPCSprite.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Sprite").ToInt();
        }

        void nudCreateFNPCSprite_ValueChanged(object sender, ValueChangedEventArgs e) {
            SpriteSheet sheet = Logic.Graphics.GraphicsManager.GetSpriteSheet(e.NewValue);
            if (sheet != null) {
                pbxCreateFNPCSprite.Size = new Size(32, 64);
                pbxCreateFNPCSprite.BlitToBuffer(sheet.GetSheet(FrameType.Idle, Enums.Direction.Down), new Rectangle(96, 0, 32, 64));
            }
        }
        #endregion
        #region Move FNPC
        public void SwitchToMoveFNPCOptions() {
            if (pnlMoveFNPCAction == null) {
                pnlMoveFNPCAction = new Panel("pnlMoveFNPCAction");
                pnlMoveFNPCAction.Size = new Size(300, 180);
                pnlMoveFNPCAction.Location = new Point(0, 30);
                pnlMoveFNPCAction.BackColor = Color.Transparent;
                pnlMoveFNPCAction.Hide();

                lblMoveFNPCID = new Label("lblMoveFNPCID");
                lblMoveFNPCID.Location = new Point(5, 5);
                lblMoveFNPCID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMoveFNPCID.AutoSize = true;
                lblMoveFNPCID.Text = "ID:";

                txtMoveFNPCID = new TextBox("txtMoveFNPCID");
                txtMoveFNPCID.Location = new Point(75, 5);
                txtMoveFNPCID.Size = new System.Drawing.Size(125, 15);

                lblMoveFNPCX = new Label("lblMoveFNPCX");
                lblMoveFNPCX.Location = new Point(5, 25);
                lblMoveFNPCX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMoveFNPCX.AutoSize = true;
                lblMoveFNPCX.Text = "Target X:";

                nudMoveFNPCX = new NumericUpDown("nudMoveFNPCX");
                nudMoveFNPCX.Location = new Point(75, 25);
                nudMoveFNPCX.Size = new System.Drawing.Size(125, 15);
                nudMoveFNPCX.Maximum = 50;

                lblMoveFNPCY = new Label("lblMoveFNPCY");
                lblMoveFNPCY.Location = new Point(5, 45);
                lblMoveFNPCY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMoveFNPCY.AutoSize = true;
                lblMoveFNPCY.Text = "Target Y:";

                nudMoveFNPCY = new NumericUpDown("nudMoveFNPCY");
                nudMoveFNPCY.Location = new Point(75, 45);
                nudMoveFNPCY.Size = new System.Drawing.Size(125, 15);
                nudMoveFNPCY.Maximum = 50;

                lblMoveFNPCSpeed = new Label("lblMoveFNPCSpeed");
                lblMoveFNPCSpeed.Location = new Point(5, 65);
                lblMoveFNPCSpeed.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMoveFNPCSpeed.AutoSize = true;
                lblMoveFNPCSpeed.Text = "Speed:";

                cbxMoveFNPCSpeed = new ComboBox("cbxMoveFNPCSpeed");
                cbxMoveFNPCSpeed.Location = new Point(75, 65);
                cbxMoveFNPCSpeed.Size = new System.Drawing.Size(125, 15);
                string[] values = Enum.GetNames(typeof(Enums.MovementSpeed));
                for (int i = 0; i < values.Length; i++) {
                    cbxMoveFNPCSpeed.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), values[i]));
                }
                cbxMoveFNPCSpeed.SelectItem(0);

                chkMoveFNPCPause = new CheckBox("chkMoveFNPCPause");
                chkMoveFNPCPause.Location = new Point(5, 85);
                chkMoveFNPCPause.Size = new System.Drawing.Size(200, 15);
                chkMoveFNPCPause.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkMoveFNPCPause.Text = "Pause until complete";

                pnlMoveFNPCAction.AddWidget(lblMoveFNPCID);
                pnlMoveFNPCAction.AddWidget(txtMoveFNPCID);
                pnlMoveFNPCAction.AddWidget(lblMoveFNPCX);
                pnlMoveFNPCAction.AddWidget(nudMoveFNPCX);
                pnlMoveFNPCAction.AddWidget(lblMoveFNPCY);
                pnlMoveFNPCAction.AddWidget(nudMoveFNPCY);
                pnlMoveFNPCAction.AddWidget(lblMoveFNPCSpeed);
                pnlMoveFNPCAction.AddWidget(cbxMoveFNPCSpeed);
                pnlMoveFNPCAction.AddWidget(chkMoveFNPCPause);

                pnlEditorSegments.AddWidget(pnlMoveFNPCAction);
            }

            HideAllOptionPanels();
            pnlMoveFNPCAction.Show();
        }

        public void SaveMoveFNPCOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.MoveFNPC;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtMoveFNPCID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudMoveFNPCX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudMoveFNPCY.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Speed", (cbxMoveFNPCSpeed.SelectedIndex).ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Pause", chkMoveFNPCPause.Checked.ToString());
        }

        public void LoadMoveFNPCOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtMoveFNPCID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
            nudMoveFNPCX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudMoveFNPCY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
            cbxMoveFNPCSpeed.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Speed").ToInt());
            chkMoveFNPCPause.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Pause").ToBool();
        }
        #endregion
        #region Warp FNPC
        public void SwitchToWarpFNPCOptions() {
            if (pnlWarpFNPCAction == null) {
                pnlWarpFNPCAction = new Panel("pnlWarpFNPCAction");
                pnlWarpFNPCAction.Size = new Size(300, 180);
                pnlWarpFNPCAction.Location = new Point(0, 30);
                pnlWarpFNPCAction.BackColor = Color.Transparent;
                pnlWarpFNPCAction.Hide();

                lblWarpFNPCID = new Label("lblWarpFNPCID");
                lblWarpFNPCID.Location = new Point(5, 5);
                lblWarpFNPCID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpFNPCID.AutoSize = true;
                lblWarpFNPCID.Text = "ID:";

                txtWarpFNPCID = new TextBox("txtWarpFNPCID");
                txtWarpFNPCID.Location = new Point(75, 5);
                txtWarpFNPCID.Size = new System.Drawing.Size(125, 15);

                lblWarpFNPCX = new Label("lblWarpFNPCX");
                lblWarpFNPCX.Location = new Point(5, 25);
                lblWarpFNPCX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpFNPCX.AutoSize = true;
                lblWarpFNPCX.Text = "Image X:";

                nudWarpFNPCX = new NumericUpDown("nudWarpFNPCX");
                nudWarpFNPCX.Location = new Point(75, 25);
                nudWarpFNPCX.Size = new System.Drawing.Size(125, 15);
                nudWarpFNPCX.Maximum = 640;

                lblWarpFNPCY = new Label("lblWarpFNPCY");
                lblWarpFNPCY.Location = new Point(5, 45);
                lblWarpFNPCY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWarpFNPCY.AutoSize = true;
                lblWarpFNPCY.Text = "Image Y:";

                nudWarpFNPCY = new NumericUpDown("nudWarpFNPCY");
                nudWarpFNPCY.Location = new Point(75, 45);
                nudWarpFNPCY.Size = new System.Drawing.Size(125, 15);
                nudWarpFNPCY.Maximum = 480;

                pnlWarpFNPCAction.AddWidget(lblWarpFNPCID);
                pnlWarpFNPCAction.AddWidget(txtWarpFNPCID);
                pnlWarpFNPCAction.AddWidget(lblWarpFNPCX);
                pnlWarpFNPCAction.AddWidget(nudWarpFNPCX);
                pnlWarpFNPCAction.AddWidget(lblWarpFNPCY);
                pnlWarpFNPCAction.AddWidget(nudWarpFNPCY);

                pnlEditorSegments.AddWidget(pnlWarpFNPCAction);
            }

            HideAllOptionPanels();
            pnlWarpFNPCAction.Show();
        }

        public void SaveWarpFNPCOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.WarpFNPC;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtWarpFNPCID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudWarpFNPCX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudWarpFNPCY.Value.ToString());
        }

        public void LoadWarpFNPCOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtShowImageID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
            nudWarpFNPCX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudWarpFNPCY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
        }
        #endregion
        #region Change FNPC Dir
        public void SwitchToChangeFNPCDirOptions() {
            if (pnlChangeFNPCDirAction == null) {
                pnlChangeFNPCDirAction = new Panel("pnlChangeFNPCDirAction");
                pnlChangeFNPCDirAction.Size = new Size(300, 180);
                pnlChangeFNPCDirAction.Location = new Point(0, 30);
                pnlChangeFNPCDirAction.BackColor = Color.Transparent;
                pnlChangeFNPCDirAction.Hide();

                lblChangeFNPCDirID = new Label("lblChangeFNPCDirID");
                lblChangeFNPCDirID.Location = new Point(5, 5);
                lblChangeFNPCDirID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblChangeFNPCDirID.AutoSize = true;
                lblChangeFNPCDirID.Text = "ID:";

                txtChangeFNPCDirID = new TextBox("txtChangeFNPCDirID");
                txtChangeFNPCDirID.Location = new Point(75, 5);
                txtChangeFNPCDirID.Size = new System.Drawing.Size(125, 15);

                cbxChangeFNPCDirDirection = new ComboBox("cbxChangeFNPCDirDirection");
                cbxChangeFNPCDirDirection.Location = new Point(5, 25);
                cbxChangeFNPCDirDirection.Size = new System.Drawing.Size(200, 15);
                string[] values = Enum.GetNames(typeof(Enums.Direction));
                for (int i = 0; i < values.Length; i++) {
                    cbxChangeFNPCDirDirection.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), values[i]));
                }
                cbxChangeFNPCDirDirection.SelectItem(0);

                pnlChangeFNPCDirAction.AddWidget(lblChangeFNPCDirID);
                pnlChangeFNPCDirAction.AddWidget(txtChangeFNPCDirID);
                pnlChangeFNPCDirAction.AddWidget(cbxChangeFNPCDirDirection);

                pnlEditorSegments.AddWidget(pnlChangeFNPCDirAction);
            }

            HideAllOptionPanels();
            pnlChangeFNPCDirAction.Show();
        }

        public void SaveChangeFNPCDirOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ChangeFNPCDir;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtChangeFNPCDirID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Direction", cbxChangeFNPCDirDirection.SelectedIndex.ToString());
        }

        public void LoadChangeFNPCDirOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtChangeFNPCDirID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
            cbxChangeFNPCDirDirection.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Direction").ToInt());
        }
        #endregion
        #region Delete FNPC
        public void SwitchToDeleteFNPCOptions() {
            if (pnlDeleteFNPCAction == null) {
                pnlDeleteFNPCAction = new Panel("pnlDeleteFNPCAction");
                pnlDeleteFNPCAction.Size = new Size(300, 180);
                pnlDeleteFNPCAction.Location = new Point(0, 30);
                pnlDeleteFNPCAction.BackColor = Color.Transparent;
                pnlDeleteFNPCAction.Hide();

                lblDeleteFNPCID = new Label("lblDeleteFNPCID");
                lblDeleteFNPCID.Location = new Point(5, 5);
                lblDeleteFNPCID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblDeleteFNPCID.AutoSize = true;
                lblDeleteFNPCID.Text = "ID:";

                txtDeleteFNPCID = new TextBox("txtDeleteFNPCID");
                txtDeleteFNPCID.Location = new Point(75, 5);
                txtDeleteFNPCID.Size = new System.Drawing.Size(125, 15);

                pnlDeleteFNPCAction.AddWidget(lblDeleteFNPCID);
                pnlDeleteFNPCAction.AddWidget(txtDeleteFNPCID);

                pnlEditorSegments.AddWidget(pnlDeleteFNPCAction);
            }

            HideAllOptionPanels();
            pnlDeleteFNPCAction.Show();
        }

        public void SaveDeleteFNPCOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.DeleteFNPC;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtDeleteFNPCID.Text);
        }

        public void LoadDeleteFNPCOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtDeleteFNPCID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
        }
        #endregion
        #region Story Script
        public void SwitchToStoryScriptOptions() {
            if (pnlStoryScriptAction == null) {
                pnlStoryScriptAction = new Panel("pnlStoryScriptAction");
                pnlStoryScriptAction.Size = new Size(300, 180);
                pnlStoryScriptAction.Location = new Point(0, 30);
                pnlStoryScriptAction.BackColor = Color.Transparent;
                pnlStoryScriptAction.Hide();

                lblStoryScriptIndex = new Label("lblStoryScriptIndex");
                lblStoryScriptIndex.Location = new Point(5, 5);
                lblStoryScriptIndex.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblStoryScriptIndex.AutoSize = true;
                lblStoryScriptIndex.Text = "Script:";

                nudStoryScriptIndex = new NumericUpDown("nudStoryScriptIndex");
                nudStoryScriptIndex.Location = new Point(75, 5);
                nudStoryScriptIndex.Size = new System.Drawing.Size(100, 14);
                nudStoryScriptIndex.Maximum = Int32.MaxValue;



                lblStoryScriptParam1 = new Label("lblStoryScriptParam1");
                lblStoryScriptParam1.Location = new Point(5, 25);
                lblStoryScriptParam1.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblStoryScriptParam1.AutoSize = true;
                lblStoryScriptParam1.Text = "Script Parameter 1:";

                txtStoryScriptParam1 = new TextBox("txtStoryScriptParam1");
                txtStoryScriptParam1.Location = new Point(5, 40);
                txtStoryScriptParam1.Size = new System.Drawing.Size(270, 16);

                lblStoryScriptParam2 = new Label("lblStoryScriptParam2");
                lblStoryScriptParam2.Location = new Point(5, 60);
                lblStoryScriptParam2.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblStoryScriptParam2.AutoSize = true;
                lblStoryScriptParam2.Text = "Script Parameter 2:";

                txtStoryScriptParam2 = new TextBox("txtStoryScriptParam2");
                txtStoryScriptParam2.Location = new Point(5, 75);
                txtStoryScriptParam2.Size = new System.Drawing.Size(270, 16);

                lblStoryScriptParam3 = new Label("lblStoryScriptParam3");
                lblStoryScriptParam3.Location = new Point(5, 95);
                lblStoryScriptParam3.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblStoryScriptParam3.AutoSize = true;
                lblStoryScriptParam3.Text = "Script Parameter 3:";

                txtStoryScriptParam3 = new TextBox("txtStoryScriptParam3");
                txtStoryScriptParam3.Location = new Point(5, 110);
                txtStoryScriptParam3.Size = new System.Drawing.Size(270, 16);

                chkStoryScriptPause = new CheckBox("chkStoryScriptPause");
                chkStoryScriptPause.Location = new Point(5, 135);
                chkStoryScriptPause.Size = new System.Drawing.Size(200, 105);
                chkStoryScriptPause.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkStoryScriptPause.Text = "Pause until complete";

                pnlStoryScriptAction.AddWidget(lblStoryScriptIndex);
                pnlStoryScriptAction.AddWidget(nudStoryScriptIndex);
                pnlStoryScriptAction.AddWidget(lblStoryScriptParam1);
                pnlStoryScriptAction.AddWidget(txtStoryScriptParam1);
                pnlStoryScriptAction.AddWidget(lblStoryScriptParam2);
                pnlStoryScriptAction.AddWidget(txtStoryScriptParam2);
                pnlStoryScriptAction.AddWidget(lblStoryScriptParam3);
                pnlStoryScriptAction.AddWidget(txtStoryScriptParam3);
                pnlStoryScriptAction.AddWidget(chkStoryScriptPause);

                pnlEditorSegments.AddWidget(pnlStoryScriptAction);
            }

            HideAllOptionPanels();
            pnlStoryScriptAction.Show();
        }

        public void SaveStoryScriptOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.RunScript;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ScriptIndex", nudStoryScriptIndex.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ScriptParam1", txtStoryScriptParam1.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ScriptParam2", txtStoryScriptParam2.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ScriptParam3", txtStoryScriptParam3.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Pause", chkStoryScriptPause.Checked.ToString());
        }

        public void LoadStoryScriptOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            nudStoryScriptIndex.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ScriptIndex").ToInt();
            txtStoryScriptParam1.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ScriptParam1");
            txtStoryScriptParam2.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ScriptParam2");
            txtStoryScriptParam3.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ScriptParam3");
            chkStoryScriptPause.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Pause").ToBool();
        }
        #endregion
        #region Hide Players
        public void SwitchToHidePlayersOptions() {
            if (pnlHidePlayersAction == null) {
                pnlHidePlayersAction = new Panel("pnlHidePlayersAction");
                pnlHidePlayersAction.Size = new Size(300, 180);
                pnlHidePlayersAction.Location = new Point(0, 30);
                pnlHidePlayersAction.BackColor = Color.Transparent;
                pnlHidePlayersAction.Hide();

                pnlEditorSegments.AddWidget(pnlHidePlayersAction);
            }

            HideAllOptionPanels();
            pnlHidePlayersAction.Show();
        }

        public void SaveHidePlayersOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.HidePlayers;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadHidePlayersOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region Show Players
        public void SwitchToShowPlayersOptions() {
            if (pnlShowPlayersAction == null) {
                pnlShowPlayersAction = new Panel("pnlShowPlayersAction");
                pnlShowPlayersAction.Size = new Size(300, 180);
                pnlShowPlayersAction.Location = new Point(0, 30);
                pnlShowPlayersAction.BackColor = Color.Transparent;
                pnlShowPlayersAction.Hide();

                pnlEditorSegments.AddWidget(pnlShowPlayersAction);
            }

            HideAllOptionPanels();
            pnlShowPlayersAction.Show();
        }

        public void SaveShowPlayersOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ShowPlayers;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadShowPlayersOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region FNPC Emotion
        public void SwitchToFNPCEmotionOptions() {
            if (pnlFNPCEmotionAction == null) {
                pnlFNPCEmotionAction = new Panel("pnlFNPCEmotionAction");
                pnlFNPCEmotionAction.Size = new Size(300, 180);
                pnlFNPCEmotionAction.Location = new Point(0, 30);
                pnlFNPCEmotionAction.BackColor = Color.Transparent;
                pnlFNPCEmotionAction.Hide();

                lblFNPCEmotionID = new Label("lblFNPCEmotionID");
                lblFNPCEmotionID.Location = new Point(5, 5);
                lblFNPCEmotionID.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblFNPCEmotionID.AutoSize = true;
                lblFNPCEmotionID.Text = "ID:";

                txtFNPCEmotionID = new TextBox("txtFNPCEmotionID");
                txtFNPCEmotionID.Location = new Point(75, 5);
                txtFNPCEmotionID.Size = new System.Drawing.Size(125, 15);

                lblFNPCEmotionNum = new Label("lblFNPCEmotionNum");
                lblFNPCEmotionNum.Location = new Point(5, 25);
                lblFNPCEmotionNum.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblFNPCEmotionNum.AutoSize = true;
                lblFNPCEmotionNum.Text = "Emotion:";

                nudFNPCEmotionNum = new NumericUpDown("nudFNPCEmotionNum");
                nudFNPCEmotionNum.Location = new Point(75, 25);
                nudFNPCEmotionNum.Size = new System.Drawing.Size(125, 15);
                nudFNPCEmotionNum.Maximum = 10;

                pnlFNPCEmotionAction.AddWidget(lblFNPCEmotionID);
                pnlFNPCEmotionAction.AddWidget(txtFNPCEmotionID);
                pnlFNPCEmotionAction.AddWidget(lblFNPCEmotionNum);
                pnlFNPCEmotionAction.AddWidget(nudFNPCEmotionNum);

                pnlEditorSegments.AddWidget(pnlFNPCEmotionAction);
            }

            HideAllOptionPanels();
            pnlFNPCEmotionAction.Show();
        }

        public void SaveFNPCEmotionOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.FNPCEmotion;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("ID", txtFNPCEmotionID.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Emotion", nudFNPCEmotionNum.Value.ToString());
        }

        public void LoadFNPCEmotionOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtFNPCEmotionID.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("ID") ?? "";
            nudFNPCEmotionNum.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Emotion").ToInt();
        }
        #endregion
        #region Weather
        public void SwitchToChangeWeatherOptions() {
            if (pnlWeatherAction == null) {
                pnlWeatherAction = new Panel("pnlWeatherAction");
                pnlWeatherAction.Size = new Size(300, 180);
                pnlWeatherAction.Location = new Point(0, 30);
                pnlWeatherAction.BackColor = Color.Transparent;
                pnlWeatherAction.Hide();

                lblWeatherType = new Label("lblWeatherType");
                lblWeatherType.Location = new Point(5, 5);
                lblWeatherType.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWeatherType.AutoSize = true;
                lblWeatherType.Text = "Weather:";

                cbxWeatherType = new ComboBox("cbxWeatherType");
                cbxWeatherType.Location = new Point(75, 5);
                cbxWeatherType.Size = new System.Drawing.Size(125, 15);
                string[] value = Enum.GetNames(typeof(Enums.Weather));
                for (int i = 0; i < value.Length; i++) {
                    cbxWeatherType.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), value[i]));
                }
                cbxWeatherType.SelectItem(0);

                pnlWeatherAction.AddWidget(lblWeatherType);
                pnlWeatherAction.AddWidget(cbxWeatherType);

                pnlEditorSegments.AddWidget(pnlWeatherAction);
            }

            HideAllOptionPanels();
            pnlWeatherAction.Show();
        }

        public void SaveChangeWeatherOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ChangeWeather;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Weather", cbxWeatherType.SelectedIndex.ToString());
        }

        public void LoadChangeWeatherOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            cbxWeatherType.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Weather").ToInt());
        }
        #endregion
        #region Hide NPCs
        public void SwitchToHideNPCsOptions() {
            if (pnlHideNPCsAction == null) {
                pnlHideNPCsAction = new Panel("pnlHideNPCsAction");
                pnlHideNPCsAction.Size = new Size(300, 180);
                pnlHideNPCsAction.Location = new Point(0, 30);
                pnlHideNPCsAction.BackColor = Color.Transparent;
                pnlHideNPCsAction.Hide();

                pnlEditorSegments.AddWidget(pnlHideNPCsAction);
            }

            HideAllOptionPanels();
            pnlHideNPCsAction.Show();
        }

        public void SaveHideNPCsOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.HideNPCs;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadHideNPCsOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region Show NPCs
        public void SwitchToShowNPCsOptions() {
            if (pnlShowNPCsAction == null) {
                pnlShowNPCsAction = new Panel("pnlShowNPCsAction");
                pnlShowNPCsAction.Size = new Size(300, 180);
                pnlShowNPCsAction.Location = new Point(0, 30);
                pnlShowNPCsAction.BackColor = Color.Transparent;
                pnlShowNPCsAction.Hide();

                pnlEditorSegments.AddWidget(pnlShowNPCsAction);
            }

            HideAllOptionPanels();
            pnlShowNPCsAction.Show();
        }

        public void SaveShowNPCsOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ShowNPCs;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadShowNPCsOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion
        #region Wait For Map
        public void SwitchToWaitForMapOptions() {
            if (pnlWaitForMapAction == null) {
                pnlWaitForMapAction = new Panel("pnlWaitForMapAction");
                pnlWaitForMapAction.Size = new Size(300, 180);
                pnlWaitForMapAction.Location = new Point(0, 30);
                pnlWaitForMapAction.BackColor = Color.Transparent;
                pnlWaitForMapAction.Hide();

                lblWaitForMapMap = new Label("lblWaitForMapMap");
                lblWaitForMapMap.Location = new Point(5, 5);
                lblWaitForMapMap.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWaitForMapMap.AutoSize = true;
                lblWaitForMapMap.Text = "Map:";

                txtWaitForMapMap = new TextBox("txtWaitForMapMap");
                txtWaitForMapMap.Location = new Point(75, 5);
                txtWaitForMapMap.Size = new System.Drawing.Size(125, 15);

                pnlWaitForMapAction.AddWidget(lblWaitForMapMap);
                pnlWaitForMapAction.AddWidget(txtWaitForMapMap);

                pnlEditorSegments.AddWidget(pnlWaitForMapAction);
            }

            HideAllOptionPanels();
            pnlWaitForMapAction.Show();
        }

        public void SaveWaitForMapOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.WaitForMap;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("MapID", txtWaitForMapMap.Text);
        }

        public void LoadWaitForMapOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtWaitForMapMap.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("MapID") ?? "";
        }
        #endregion
        #region Wait For Loc
        public void SwitchToWaitForLocOptions() {
            if (pnlWaitForLocAction == null) {
                pnlWaitForLocAction = new Panel("pnlWaitForLocAction");
                pnlWaitForLocAction.Size = new Size(300, 180);
                pnlWaitForLocAction.Location = new Point(0, 30);
                pnlWaitForLocAction.BackColor = Color.Transparent;
                pnlWaitForLocAction.Hide();

                lblWaitForLocMap = new Label("lblWaitForLocMap");
                lblWaitForLocMap.Location = new Point(5, 5);
                lblWaitForLocMap.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWaitForLocMap.AutoSize = true;
                lblWaitForLocMap.Text = "Map:";

                txtWaitForLocMap = new TextBox("txtWaitForLocMap");
                txtWaitForLocMap.Location = new Point(75, 5);
                txtWaitForLocMap.Size = new System.Drawing.Size(125, 15);

                lblWaitForLocX = new Label("lblWaitForLocX");
                lblWaitForLocX.Location = new Point(5, 25);
                lblWaitForLocX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWaitForLocX.AutoSize = true;
                lblWaitForLocX.Text = "Target X:";

                nudWaitForLocX = new NumericUpDown("nudWaitForLocX");
                nudWaitForLocX.Location = new Point(75, 25);
                nudWaitForLocX.Size = new System.Drawing.Size(125, 15);
                nudWaitForLocX.Maximum = 50;

                lblWaitForLocY = new Label("lblWaitForLocY");
                lblWaitForLocY.Location = new Point(5, 45);
                lblWaitForLocY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblWaitForLocY.AutoSize = true;
                lblWaitForLocY.Text = "Target Y:";

                nudWaitForLocY = new NumericUpDown("nudWaitForLocY");
                nudWaitForLocY.Location = new Point(75, 45);
                nudWaitForLocY.Size = new System.Drawing.Size(125, 15);
                nudWaitForLocY.Maximum = 50;

                pnlWaitForLocAction.AddWidget(lblWaitForLocMap);
                pnlWaitForLocAction.AddWidget(txtWaitForLocMap);
                pnlWaitForLocAction.AddWidget(lblWaitForLocX);
                pnlWaitForLocAction.AddWidget(nudWaitForLocX);
                pnlWaitForLocAction.AddWidget(lblWaitForLocY);
                pnlWaitForLocAction.AddWidget(nudWaitForLocY);

                pnlEditorSegments.AddWidget(pnlWaitForLocAction);
            }

            HideAllOptionPanels();
            pnlWaitForLocAction.Show();
        }

        public void SaveWaitForLocOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.WaitForLoc;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("MapID", txtWaitForLocMap.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudWaitForLocX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudWaitForLocY.Value.ToString());
        }

        public void LoadWaitForLocOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtWaitForLocMap.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("MapID") ?? "";
            nudWaitForLocX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudWaitForLocY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
        }
        #endregion
        #region Ask Question
        public void SwitchToAskQuestionOptions() {
            if (pnlAskQuestionAction == null) {
                pnlAskQuestionAction = new Panel("pnlAskQuestionAction");
                pnlAskQuestionAction.Size = new Size(300, 180);
                pnlAskQuestionAction.Location = new Point(0, 30);
                pnlAskQuestionAction.BackColor = Color.Transparent;
                pnlAskQuestionAction.Hide();

                lblAskQuestionQuestion = new Label("lblAskQuestionQuestion");
                lblAskQuestionQuestion.Location = new Point(5, 5);
                lblAskQuestionQuestion.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblAskQuestionQuestion.AutoSize = true;
                lblAskQuestionQuestion.Text = "Question:";

                txtAskQuestionQuestion = new TextBox("txtAskQuestionQuestion");
                txtAskQuestionQuestion.Location = new Point(75, 5);
                txtAskQuestionQuestion.Size = new System.Drawing.Size(125, 15);

                lblAskQuestionSegmentOnYes = new Label("lblAskQuestionSegmentOnYes");
                lblAskQuestionSegmentOnYes.Location = new Point(5, 25);
                lblAskQuestionSegmentOnYes.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblAskQuestionSegmentOnYes.AutoSize = true;
                lblAskQuestionSegmentOnYes.Text = "Segment On Yes:";

                nudAskQuestionSegmentOnYes = new NumericUpDown("nudAskQuestionSegmentOnYes");
                nudAskQuestionSegmentOnYes.Location = new Point(75, 25);
                nudAskQuestionSegmentOnYes.Size = new System.Drawing.Size(125, 15);
                nudAskQuestionSegmentOnYes.Minimum = 1;
                nudAskQuestionSegmentOnYes.Maximum = story.Segments.Count;

                lblAskQuestionSegmentOnNo = new Label("lblAskQuestionSegmentOnNo");
                lblAskQuestionSegmentOnNo.Location = new Point(5, 45);
                lblAskQuestionSegmentOnNo.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblAskQuestionSegmentOnNo.AutoSize = true;
                lblAskQuestionSegmentOnNo.Text = "Segment On No:";

                nudAskQuestionSegmentOnNo = new NumericUpDown("nudAskQuestionSegmentOnNo");
                nudAskQuestionSegmentOnNo.Location = new Point(75, 45);
                nudAskQuestionSegmentOnNo.Size = new System.Drawing.Size(125, 15);
                nudAskQuestionSegmentOnNo.Minimum = 1;
                nudAskQuestionSegmentOnNo.Maximum = story.Segments.Count;

                lblAskQuestionMugshot = new Label("lblAskQuestionMugshot");
                lblAskQuestionMugshot.Location = new Point(5, 65);
                lblAskQuestionMugshot.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblAskQuestionMugshot.AutoSize = true;
                lblAskQuestionMugshot.Text = "Mugshot:";

                nudAskQuestionMugshot = new NumericUpDown("nudAskQuestionMugshot");
                nudAskQuestionMugshot.Location = new Point(75, 65);
                nudAskQuestionMugshot.Size = new System.Drawing.Size(100, 14);
                nudAskQuestionMugshot.Maximum = Int32.MaxValue;
                nudAskQuestionMugshot.Minimum = 1;
                nudAskQuestionMugshot.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudAskQuestionMugshot_ValueChanged);

                pbxAskQuestionMugshot = new PictureBox("pbxAskQuestionMugshot");
                pbxAskQuestionMugshot.Location = new Point(nudAskQuestionMugshot.X + nudAskQuestionMugshot.Width + 5, 65);
                pbxAskQuestionMugshot.Size = new System.Drawing.Size(40, 40);

                pnlAskQuestionAction.AddWidget(lblAskQuestionQuestion);
                pnlAskQuestionAction.AddWidget(txtAskQuestionQuestion);
                pnlAskQuestionAction.AddWidget(lblAskQuestionSegmentOnYes);
                pnlAskQuestionAction.AddWidget(nudAskQuestionSegmentOnYes);
                pnlAskQuestionAction.AddWidget(lblAskQuestionSegmentOnNo);
                pnlAskQuestionAction.AddWidget(nudAskQuestionSegmentOnNo);
                pnlAskQuestionAction.AddWidget(lblAskQuestionMugshot);
                pnlAskQuestionAction.AddWidget(nudAskQuestionMugshot);
                pnlAskQuestionAction.AddWidget(pbxAskQuestionMugshot);

                pnlEditorSegments.AddWidget(pnlAskQuestionAction);
            }

            HideAllOptionPanels();
            pnlAskQuestionAction.Show();
        }

        public void SaveAskQuestionOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.AskQuestion;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Question", txtAskQuestionQuestion.Text);
            story.Segments[lbxSegments.SelectedIndex].AddParameter("SegmentOnYes", nudAskQuestionSegmentOnYes.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("SegmentOnNo", nudAskQuestionSegmentOnNo.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Mugshot", nudAskQuestionMugshot.Value.ToString());
        }

        public void LoadAskQuestionOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            txtAskQuestionQuestion.Text = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Question") ?? "";
            nudAskQuestionSegmentOnYes.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("SegmentOnYes").ToInt();
            nudAskQuestionSegmentOnNo.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("SegmentOnNo").ToInt();
            nudAskQuestionMugshot.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Mugshot").ToInt();
        }

        void nudAskQuestionMugshot_ValueChanged(object sender, ValueChangedEventArgs e) {
            Mugshot mugshot = Logic.Graphics.GraphicsManager.GetMugshot(e.NewValue, 0, 0, 0);
            if (mugshot != null) {
                pbxAskQuestionMugshot.Image = mugshot.GetEmote(0);
            }
        }
        #endregion
        #region Go To Segment
        public void SwitchToGoToSegmentOptions() {
            if (pnlGoToSegmentAction == null) {
                pnlGoToSegmentAction = new Panel("pnlGoToSegmentAction");
                pnlGoToSegmentAction.Size = new Size(300, 180);
                pnlGoToSegmentAction.Location = new Point(0, 30);
                pnlGoToSegmentAction.BackColor = Color.Transparent;
                pnlGoToSegmentAction.Hide();

                lblGoToSegmentSegment = new Label("lblGoToSegmentSegment");
                lblGoToSegmentSegment.Location = new Point(5, 5);
                lblGoToSegmentSegment.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblGoToSegmentSegment.AutoSize = true;
                lblGoToSegmentSegment.Text = "Script:";

                nudGoToSegmentSegment = new NumericUpDown("nudGoToSegmentSegment");
                nudGoToSegmentSegment.Location = new Point(75, 5);
                nudGoToSegmentSegment.Size = new System.Drawing.Size(100, 14);
                nudGoToSegmentSegment.Minimum = 1;
                nudGoToSegmentSegment.Maximum = story.Segments.Count;

                pnlGoToSegmentAction.AddWidget(lblGoToSegmentSegment);
                pnlGoToSegmentAction.AddWidget(nudGoToSegmentSegment);

                pnlEditorSegments.AddWidget(pnlGoToSegmentAction);
            }

            HideAllOptionPanels();
            pnlGoToSegmentAction.Show();
        }

        public void SaveGoToSegmentOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.GoToSegment;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Segment", nudGoToSegmentSegment.Value.ToString());
        }

        public void LoadGoToSegmentOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            nudGoToSegmentSegment.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Segment").ToInt();
        }
        #endregion
        #region Scroll Camera
        public void SwitchToScrollCameraOptions() {
            if (pnlScrollCameraAction == null) {
                pnlScrollCameraAction = new Panel("pnlScrollCameraAction");
                pnlScrollCameraAction.Size = new Size(300, 180);
                pnlScrollCameraAction.Location = new Point(0, 30);
                pnlScrollCameraAction.BackColor = Color.Transparent;
                pnlScrollCameraAction.Hide();

                lblScrollCameraX = new Label("lblScrollCameraX");
                lblScrollCameraX.Location = new Point(5, 5);
                lblScrollCameraX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblScrollCameraX.AutoSize = true;
                lblScrollCameraX.Text = "X:";

                nudScrollCameraX = new NumericUpDown("nudScrollCameraX");
                nudScrollCameraX.Location = new Point(75, 5);
                nudScrollCameraX.Size = new System.Drawing.Size(100, 14);
                nudScrollCameraX.Maximum = 50;

                lblScrollCameraY = new Label("lblScrollCameraY");
                lblScrollCameraY.Location = new Point(5, 25);
                lblScrollCameraY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblScrollCameraY.AutoSize = true;
                lblScrollCameraY.Text = "Y:";

                nudScrollCameraY = new NumericUpDown("nudScrollCameraY");
                nudScrollCameraY.Location = new Point(75, 25);
                nudScrollCameraY.Size = new System.Drawing.Size(100, 14);
                nudScrollCameraY.Maximum = 50;

                lblScrollCameraSpeed = new Label("lblScrollCameraSpeed");
                lblScrollCameraSpeed.Location = new Point(5, 45);
                lblScrollCameraSpeed.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblScrollCameraSpeed.AutoSize = true;
                lblScrollCameraSpeed.Text = "Speed:";

                nudScrollCameraSpeed = new NumericUpDown("nudScrollCameraSpeed");
                nudScrollCameraSpeed.Location = new Point(75, 45);
                nudScrollCameraSpeed.Size = new System.Drawing.Size(100, 14);
                nudScrollCameraSpeed.Maximum = Int32.MaxValue;

                chkScrollCameraPause = new CheckBox("chkScrollCameraPause");
                chkScrollCameraPause.Location = new Point(5, 65);
                chkScrollCameraPause.Size = new System.Drawing.Size(100, 14);
                chkScrollCameraPause.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkScrollCameraPause.BackColor = Color.Transparent;
                chkScrollCameraPause.Text = "Pause until complete";

                pnlScrollCameraAction.AddWidget(lblScrollCameraX);
                pnlScrollCameraAction.AddWidget(nudScrollCameraX);
                pnlScrollCameraAction.AddWidget(lblScrollCameraY);
                pnlScrollCameraAction.AddWidget(nudScrollCameraY);
                pnlScrollCameraAction.AddWidget(lblScrollCameraSpeed);
                pnlScrollCameraAction.AddWidget(nudScrollCameraSpeed);
                pnlScrollCameraAction.AddWidget(chkScrollCameraPause);

                pnlEditorSegments.AddWidget(pnlScrollCameraAction);
            }

            HideAllOptionPanels();
            pnlScrollCameraAction.Show();
        }

        public void SaveScrollCameraOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ScrollCamera;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudScrollCameraX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudScrollCameraY.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Speed", nudScrollCameraSpeed.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Pause", chkScrollCameraPause.Checked.ToString());
        }

        public void LoadScrollCameraOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            nudScrollCameraX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudScrollCameraY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
            nudScrollCameraSpeed.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Speed").ToInt();
            chkScrollCameraPause.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Pause").ToBool();
        }
        #endregion
        #region Hide NPCs
        public void SwitchToResetCameraOptions() {
            if (pnlResetCameraAction == null) {
                pnlResetCameraAction = new Panel("pnlResetCameraAction");
                pnlResetCameraAction.Size = new Size(300, 180);
                pnlResetCameraAction.Location = new Point(0, 30);
                pnlResetCameraAction.BackColor = Color.Transparent;
                pnlResetCameraAction.Hide();

                pnlEditorSegments.AddWidget(pnlResetCameraAction);
            }

            HideAllOptionPanels();
            pnlResetCameraAction.Show();
        }

        public void SaveResetCameraOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ResetCamera;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
        }

        public void LoadResetCameraOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
        }
        #endregion

        #region Move Player
        public void SwitchToMovePlayerOptions() {
            if (pnlMovePlayerAction == null) {
                pnlMovePlayerAction = new Panel("pnlMovePlayerAction");
                pnlMovePlayerAction.Size = new Size(300, 180);
                pnlMovePlayerAction.Location = new Point(0, 30);
                pnlMovePlayerAction.BackColor = Color.Transparent;
                pnlMovePlayerAction.Hide();

                lblMovePlayerX = new Label("lblMovePlayerX");
                lblMovePlayerX.Location = new Point(5, 5);
                lblMovePlayerX.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMovePlayerX.AutoSize = true;
                lblMovePlayerX.Text = "Target X:";

                nudMovePlayerX = new NumericUpDown("nudMovePlayerX");
                nudMovePlayerX.Location = new Point(75, 5);
                nudMovePlayerX.Size = new System.Drawing.Size(125, 15);
                nudMovePlayerX.Maximum = 50;

                lblMovePlayerY = new Label("lblMovePlayerY");
                lblMovePlayerY.Location = new Point(5, 25);
                lblMovePlayerY.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMovePlayerY.AutoSize = true;
                lblMovePlayerY.Text = "Target Y:";

                nudMovePlayerY = new NumericUpDown("nudMovePlayerY");
                nudMovePlayerY.Location = new Point(75, 25);
                nudMovePlayerY.Size = new System.Drawing.Size(125, 15);
                nudMovePlayerY.Maximum = 50;

                lblMovePlayerSpeed = new Label("lblMovePlayerSpeed");
                lblMovePlayerSpeed.Location = new Point(5, 45);
                lblMovePlayerSpeed.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                lblMovePlayerSpeed.AutoSize = true;
                lblMovePlayerSpeed.Text = "Speed:";

                cbxMovePlayerSpeed = new ComboBox("cbxMovePlayerSpeed");
                cbxMovePlayerSpeed.Location = new Point(75, 45);
                cbxMovePlayerSpeed.Size = new System.Drawing.Size(125, 15);
                string[] values = Enum.GetNames(typeof(Enums.MovementSpeed));
                for (int i = 0; i < values.Length; i++) {
                    cbxMovePlayerSpeed.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), values[i]));
                }
                cbxMovePlayerSpeed.SelectItem(0);

                chkMovePlayerPause = new CheckBox("chkMovePlayerPause");
                chkMovePlayerPause.Location = new Point(5, 65);
                chkMovePlayerPause.Size = new System.Drawing.Size(200, 15);
                chkMovePlayerPause.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 10);
                chkMovePlayerPause.Text = "Pause until complete";

                pnlMovePlayerAction.AddWidget(lblMovePlayerX);
                pnlMovePlayerAction.AddWidget(nudMovePlayerX);
                pnlMovePlayerAction.AddWidget(lblMovePlayerY);
                pnlMovePlayerAction.AddWidget(nudMovePlayerY);
                pnlMovePlayerAction.AddWidget(lblMovePlayerSpeed);
                pnlMovePlayerAction.AddWidget(cbxMovePlayerSpeed);
                pnlMovePlayerAction.AddWidget(chkMovePlayerPause);

                pnlEditorSegments.AddWidget(pnlMovePlayerAction);
            }

            HideAllOptionPanels();
            pnlMovePlayerAction.Show();
        }

        public void SaveMovePlayerOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.MovePlayer;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("X", nudMovePlayerX.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Y", nudMovePlayerY.Value.ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Speed", (cbxMovePlayerSpeed.SelectedIndex).ToString());
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Pause", chkMovePlayerPause.Checked.ToString());
        }

        public void LoadMovePlayerOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            nudMovePlayerX.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("X").ToInt();
            nudMovePlayerY.Value = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Y").ToInt();
            cbxMovePlayerSpeed.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Speed").ToInt());
            chkMovePlayerPause.Checked = story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Pause").ToBool();
        }
        #endregion
        #region Change Player Dir
        public void SwitchToChangePlayerDirOptions() {
            if (pnlChangePlayerDirAction == null) {
                pnlChangePlayerDirAction = new Panel("pnlChangePlayerDirAction");
                pnlChangePlayerDirAction.Size = new Size(300, 180);
                pnlChangePlayerDirAction.Location = new Point(0, 30);
                pnlChangePlayerDirAction.BackColor = Color.Transparent;
                pnlChangePlayerDirAction.Hide();

                cbxChangePlayerDirDirection = new ComboBox("cbxChangePlayerDirDirection");
                cbxChangePlayerDirDirection.Location = new Point(5, 5);
                cbxChangePlayerDirDirection.Size = new System.Drawing.Size(200, 15);
                string[] values = Enum.GetNames(typeof(Enums.Direction));
                for (int i = 0; i < values.Length; i++) {
                    cbxChangePlayerDirDirection.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), values[i]));
                }

                pnlChangePlayerDirAction.AddWidget(cbxChangePlayerDirDirection);

                pnlEditorSegments.AddWidget(pnlChangePlayerDirAction);
            }

            HideAllOptionPanels();
            pnlChangePlayerDirAction.Show();
        }

        public void SaveChangePlayerDirOptions() {
            story.Segments[lbxSegments.SelectedIndex].Action = Enums.StoryAction.ChangePlayerDir;
            story.Segments[lbxSegments.SelectedIndex].Parameters.Clear();
            story.Segments[lbxSegments.SelectedIndex].AddParameter("Direction", cbxChangePlayerDirDirection.SelectedIndex.ToString());
        }

        public void LoadChangePlayerDirOptions() {
            cmbSegmentTypes.SelectItem(story.Segments[lbxSegments.SelectedIndex].Action.ToString());
            cbxChangePlayerDirDirection.SelectItem(story.Segments[lbxSegments.SelectedIndex].Parameters.GetValue("Direction").ToInt());
        }
        #endregion

        #endregion

        //int GetActiveSegment() {
        //    return nudActiveSegment.Value - 1;
        //}

        void HideIfLoaded(Panel pnl) {
            if (pnl != null)
                pnl.Hide();
        }

        #region General
        void btnSegments_Click(object sender, MouseButtonEventArgs e) {
            pnlEditorGeneral.Hide();
            pnlEditorSegments.Show();
            this.Size = pnlEditorSegments.Size;
        }
        #endregion

        #region Segments
        void btnGeneral_Click(object sender, MouseButtonEventArgs e) {
            pnlEditorGeneral.Show();
            pnlEditorSegments.Hide();
            this.Size = pnlEditorGeneral.Size;
        }
        #endregion

        public void LoadStory(string[] parse) {

            pnlStoryList.Visible = false;
            pnlEditorGeneral.Visible = true;
            this.Size = new System.Drawing.Size(pnlEditorGeneral.Width, pnlEditorGeneral.Height);

            story = new Logic.Editors.Stories.EditableStory();
            story.Name = parse[2];
            // parse[3] = revision
            story.StoryStart = parse[4].ToInt();
            //story.Segments = new Logic.Editors.Stories.EditableStorySegment[parse[5].ToInt()];
            int n = 6;
            for (int i = 0; i < parse[5].ToInt(); i++) {
                int paramCount = parse[n].ToInt();
                Logic.Editors.Stories.EditableStorySegment segment = new Logic.Editors.Stories.EditableStorySegment();
                segment.Action = (Enums.StoryAction)parse[n + 1].ToInt();
                n += 2;
                for (int z = 0; z < paramCount; z++) {
                    segment.AddParameter(parse[n], parse[n + 1]);

                    n += 2;
                }
                story.Segments.Add(segment);
            }
            int exitAndContinueCount = parse[n].ToInt();
            n++;
            for (int i = 0; i < exitAndContinueCount; i++) {
                story.ExitAndContinue.Add(parse[n].ToInt());

                n += 1;
            }

            RefreshSegmentList();

            //if (story.Segments.Length == 0) {
            //    story.Segments = new Logic.Editors.Stories.EditableStorySegment[1];
            //    story.Segments[0] = new Logic.Editors.Stories.EditableStorySegment();
            //}

            txtName.Text = story.Name;
            //nudMaxSegments.Value = story.Segments.Length;
            //nudActiveSegment.Maximum = nudMaxSegments.Value;
            nudExitAndContinueCheckpoint.Maximum = story.Segments.Count;
            lbxExitAndContinue.Items.Clear();
            for (int i = 0; i < story.ExitAndContinue.Count; i++) {
                lbxExitAndContinue.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), story.ExitAndContinue[i].ToString()));
            }

            btnEdit.Text = "Edit";
        }

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshStoryList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxMoves / 10)) {
                currentTen++;
            }
            RefreshStoryList();
        }

        public void RefreshStoryList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxStories) {
                    ((ListBoxTextItem)lbxStoryList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Stories.StoryHelper.Stories[(i) + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxStoryList.Items[i]).Text = "---";
                }
            }
        }


        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxStoryList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxStoryList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    storyNum = index[0].ToInt() - 1;
                    btnEdit.Text = "Loading...";
                }

                Messenger.SendEditStory(storyNum);
            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            storyNum = -1;
            pnlEditorGeneral.Visible = false;
            pnlStoryList.Visible = true;
            this.Size = new System.Drawing.Size(pnlStoryList.Width, pnlStoryList.Height);

        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            story.Name = txtName.Text;
            story.StoryStart = nudStoryStart.Value;

            Messenger.SendSaveStory(storyNum, story);
            storyNum = -1;
            pnlEditorGeneral.Visible = false;
            pnlStoryList.Visible = true;
            this.Size = new System.Drawing.Size(pnlStoryList.Width, pnlStoryList.Height);


        }
    }
}
