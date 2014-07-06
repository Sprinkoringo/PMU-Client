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


namespace Client.Logic.Windows.Editors.MapEditor
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Widgets;
    using Client.Logic.Maps;
    using PMU.Core;
    using System.IO;
    using System.Xml;

    class winProperties : Window
    {
        #region Fields

        Button btnCancel;
        Button btnGeneral;
        Button btnNPC;
        Button btnOk;
        Button btnPlay;
        Button btnScroll;
        Button btnStop;
        CheckBox chkIndoors;
        CheckBox chkHunger;
        CheckBox chkRecruit;
        CheckBox chkExp;
        CheckBox chkInstanced;
        CheckBox chkScroll;
        ComboBox cmbMapMorality;
        NumericUpDown nudDarkness;
        NumericUpDown nudTimeLimit;
        ComboBox cmbMapWeather;
        ComboBox cmbMusic;
        Label lblMapMorality;
        Label lblMapDarkness;
        Label lblMapWeather;
        Label lblMapTimeLimit;
        Label lblClearNPC;
        Label lblEast;
        Label lblGlobal;
        Label lblMapName;
        Label lblMapSwitchover;
        Label lblMaxX;
        Label lblMaxY;
        Label lblMusic;
        Label lblNorth;
        Label lblSouth;
        Label lblWest;
        Panel pnlGeneral;
        Panel pnlNPC;
        Panel pnlScroll;
        TextBox txtEast;
        TextBox txtMapName;
        TextBox txtMaxX;
        TextBox txtMaxY;
        TextBox txtNorth;
        TextBox txtSouth;
        TextBox txtWest;

        //Label lblNpcSelector;
        //ComboBox cmbNpcSelector;
        //Label lblNpcNum;
        //NumericUpDown nudNpcNum;
        //Label lblNpcNumInfo;
        //Label lblNpcSpawnX;
        //NumericUpDown nudNpcSpawnX;
        //Label lblNpcSpawnY;
        //NumericUpDown nudNpcSpawnY;
        //Label lblNpcLevel;
        //NumericUpDown nudNpcLevel;
        //Button btnSaveNpc;
        //Button btnClearNpc;
        
        Label lblNpcSpawnTime;
        NumericUpDown nudNpcSpawnTime;

        Label lblNpcMin;
        NumericUpDown nudNpcMin;
        Label lblNpcMax;
        NumericUpDown nudNpcMax;

        ListBox lbxNpcs;

        Label lblNpcNum;
        NumericUpDown nudNpcNum;
        Label lblNpcSpawnX;
        NumericUpDown nudNpcSpawnX;
        Label lblNpcSpawnY;
        NumericUpDown nudNpcSpawnY;
        Label lblMinLevel;
        NumericUpDown nudMinLevel;
        Label lblMaxLevel;
        NumericUpDown nudMaxLevel;
        Label lblNpcSpawnRate;
        NumericUpDown nudNpcSpawnRate;
        Label lblNpcStartStatus;
        ComboBox cbNpcStartStatus;
        Label lblStatusCounter;
        NumericUpDown nudStatusCounter;
        Label lblStatusChance;
        NumericUpDown nudStatusChance;
        Button btnAddNpc;
        Button btnRemoveNpc;
        Button btnLoadNpc;


        MapProperties properties;

        #endregion Fields

        #region Constructors

        public winProperties()
            : base("winProperties") {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Map Properties";
            this.TitleBar.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            this.TitleBar.CloseButton.Visible = true;
            this.AlwaysOnTop = true;
            this.BackColor = Color.White;
            //this.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            //this.BorderWidth = 1;
            //this.BorderColor = Color.Black;
            this.Size = new Size(500, 500);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            properties = Maps.MapHelper.ActiveMap.ExportToPropertiesClass();

            pnlGeneral = new Panel("pnlGeneral");
            pnlGeneral.Size = this.Size;
            pnlGeneral.Location = new Point(0, 34);
            pnlGeneral.BackColor = Color.White;
            pnlGeneral.Visible = true;

            btnGeneral = new Button("btnGeneral");
            btnGeneral.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnGeneral.Size = new System.Drawing.Size(134, 32);
            btnGeneral.Location = new Point(49, 0);
            btnGeneral.Text = "General Options";
            btnGeneral.Selected = true;
            Skins.SkinManager.LoadButtonGui(btnGeneral);
            btnGeneral.Click += new EventHandler<MouseButtonEventArgs>(btnGeneral_Click);

            pnlNPC = new Panel("pnlNPC");
            pnlNPC.Size = this.Size;
            pnlNPC.Location = new Point(0, 34);
            pnlNPC.BackColor = Color.White;
            pnlNPC.Visible = false;

            btnNPC = new Button("btnNPC");
            btnNPC.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnNPC.Size = new System.Drawing.Size(134, 32);
            btnNPC.Location = new Point(183, 0);
            btnNPC.Text = "NPC";
            btnNPC.Selected = false;
            Skins.SkinManager.LoadButtonGui(btnNPC);
            btnNPC.Click += new EventHandler<MouseButtonEventArgs>(btnNPC_Click);

            pnlScroll = new Panel("pnlScroll");
            pnlScroll.Size = this.Size;
            pnlScroll.Location = new Point(0, 34);
            pnlScroll.BackColor = Color.White;
            pnlScroll.Visible = false;

            btnScroll = new Button("btnScroll");
            btnScroll.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnScroll.Size = new System.Drawing.Size(134, 32);
            btnScroll.Location = new Point(317, 0);
            btnScroll.Text = "Scroll";
            btnScroll.Selected = false;
            Skins.SkinManager.LoadButtonGui(btnScroll);
            btnScroll.Click += new EventHandler<MouseButtonEventArgs>(btnScroll_Click);

            #region General
            txtMapName = new TextBox("txtMapName");
            txtMapName.Size = new System.Drawing.Size(300, 18);
            txtMapName.Location = new Point(100, 10);
            txtMapName.Text = properties.Name;

            lblMapName = new Label("lblMapName");
            lblMapName.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMapName.AutoSize = true;
            lblMapName.Location = new Point(20, 10);
            lblMapName.Text = "Map Name: ";

            lblMapSwitchover = new Label("lblMapSwitchover");
            lblMapSwitchover.Font = Logic.Graphics.FontManager.LoadFont("PMU", 22);
            lblMapSwitchover.AutoSize = true;
            lblMapSwitchover.Location = new Point(20, 40);
            lblMapSwitchover.Text = "Map Switchover";

            lblNorth = new Label("lblNorth");
            lblNorth.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblNorth.AutoSize = true;
            lblNorth.Location = new Point(20, 65);
            lblNorth.Text = "North: ";

            txtNorth = new TextBox("txtNorth");
            txtNorth.Size = new System.Drawing.Size(50, 18);
            txtNorth.Location = new Point(100, 65);
            txtNorth.Text = properties.Up.ToString();

            lblSouth = new Label("lblSouth");
            lblSouth.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblSouth.AutoSize = true;
            lblSouth.Location = new Point(20, 85);
            lblSouth.Text = "South: ";

            txtSouth = new TextBox("txtSouth");
            txtSouth.Size = new System.Drawing.Size(50, 18);
            txtSouth.Location = new Point(100, 85);
            txtSouth.Text = properties.Down.ToString();

            lblEast = new Label("lblEast");
            lblEast.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblEast.AutoSize = true;
            lblEast.Location = new Point(20, 105);
            lblEast.Text = "East: ";

            txtEast = new TextBox("txtEast");
            txtEast.Size = new System.Drawing.Size(50, 18);
            txtEast.Location = new Point(100, 105);
            txtEast.Text = properties.Right.ToString();

            lblWest = new Label("lblWest");
            lblWest.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblWest.AutoSize = true;
            lblWest.Location = new Point(20, 125);
            lblWest.Text = "West: ";

            txtWest = new TextBox("txtWest");
            txtWest.Size = new System.Drawing.Size(50, 18);
            txtWest.Location = new Point(100, 125);
            txtWest.Text = properties.Left.ToString();

            chkIndoors = new CheckBox("chkIndoors");
            chkIndoors.Text = "Indoors";
            chkIndoors.Size = new System.Drawing.Size(100, 16);
            chkIndoors.Location = new Point(10, 155);
            chkIndoors.BackColor = Color.Transparent;
            chkIndoors.Checked = properties.Indoors;

            chkHunger = new CheckBox("chkHunger");
            chkHunger.Location = new Point(100, 155);
            chkHunger.Size = new System.Drawing.Size(100, 16);
            chkHunger.Text = "Hunger";
            chkHunger.BackColor = Color.Transparent;
            chkHunger.Checked = properties.Belly;

            chkRecruit = new CheckBox("chkRecruit");
            chkRecruit.Location = new Point(190, 155);
            chkRecruit.Size = new System.Drawing.Size(100, 16);
            chkRecruit.Text = "Recruit";
            chkRecruit.BackColor = Color.Transparent;
            chkRecruit.Checked = properties.Recruit;

            chkExp = new CheckBox("chkExp");
            chkExp.Location = new Point(280, 155);
            chkExp.Size = new System.Drawing.Size(100, 16);
            chkExp.Text = "Exp";
            chkExp.BackColor = Color.Transparent;
            chkExp.Checked = properties.Exp;

            chkInstanced = new CheckBox("chkInstanced");
            chkInstanced.Location = new Point(370, 155);
            chkInstanced.Size = new System.Drawing.Size(100, 16);
            chkInstanced.Text = "Instanced";
            chkInstanced.BackColor = Color.Transparent;
            chkInstanced.Checked = properties.Instanced;

            lblGlobal = new Label("lblGlobal");
            lblGlobal.Font = Logic.Graphics.FontManager.LoadFont("PMU", 22);
            lblGlobal.AutoSize = true;
            lblGlobal.Location = new Point(260, 40);
            lblGlobal.Text = "Global Options";

            lblMapMorality = new Label("lblMapMorality");
            lblMapMorality.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMapMorality.AutoSize = true;
            lblMapMorality.Location = new Point(240, 65);
            lblMapMorality.Text = "Moral: ";

            cmbMapMorality = new ComboBox("cmbMapMorality");
            //cmbMapMorality.Font = Graphics.FontManager.LoadFont("PMU", 18);
            cmbMapMorality.Size = new System.Drawing.Size(150, 18);
            cmbMapMorality.Location = new Point(300, 65);
            //cmbMapMorality.Text = "Map Morality";
            cmbMapMorality.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "None"));
            cmbMapMorality.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Safe Zone"));
            cmbMapMorality.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "No Death Penalty"));
            cmbMapMorality.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "House"));
            cmbMapMorality.SelectItem((int)properties.Moral);

            lblMapWeather = new Label("lblMapWeather");
            lblMapWeather.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMapWeather.AutoSize = true;
            lblMapWeather.Location = new Point(240, 85);
            lblMapWeather.Text = "Weather: ";

            cmbMapWeather = new ComboBox("cmbMapWeather");
            cmbMapWeather.Size = new System.Drawing.Size(150, 18);
            cmbMapWeather.Location = new Point(300, 85);
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Ambiguous"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "None"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Raining"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Snowing"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Thunder"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Hail"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Diamond Dust"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Cloudy"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Fog"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Sunny"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Sandstorm"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Snowstorm"));
            cmbMapWeather.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "Ashfall"));
            cmbMapWeather.SelectItem((int)properties.Weather);

            lblMapDarkness = new Label("lblMapDarkness");
            lblMapDarkness.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMapDarkness.AutoSize = true;
            lblMapDarkness.Location = new Point(240, 105);
            lblMapDarkness.Text = "Darkness: ";

            nudDarkness = new NumericUpDown("nudDarkness");
            nudDarkness.Size = new System.Drawing.Size(150, 18);
            nudDarkness.Location = new Point(300, 105);
            nudDarkness.Minimum = -1;
            nudDarkness.Maximum = 30;
            nudDarkness.Value = properties.Darkness;

            lblMapTimeLimit = new Label("lblMapTimeLimit");
            lblMapTimeLimit.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMapTimeLimit.AutoSize = true;
            lblMapTimeLimit.Location = new Point(240, 125);
            lblMapTimeLimit.Text = "Time: ";

            nudTimeLimit = new NumericUpDown("nudTimeLimit");
            nudTimeLimit.Size = new System.Drawing.Size(150, 18);
            nudTimeLimit.Location = new Point(300, 125);
            nudTimeLimit.Minimum = -1;
            nudTimeLimit.Maximum = Int32.MaxValue;
            nudTimeLimit.Value = properties.TimeLimit;

            lblMusic = new Label("lblMusic");
            lblMusic.Font = Logic.Graphics.FontManager.LoadFont("PMU", 22);
            lblMusic.AutoSize = true;
            lblMusic.Location = new Point(20, 170);
            lblMusic.Text = "Music";

            cmbMusic = new ComboBox("cmbMusic");
            cmbMusic.Size = new System.Drawing.Size(375, 30);
            cmbMusic.Location = new Point(25, 200);

            btnPlay = new Button("btnPlay");
            btnPlay.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnPlay.Size = new System.Drawing.Size(75, 30);
            btnPlay.Location = new Point(410, 200);
            Skins.SkinManager.LoadButtonGui(btnPlay);
            btnPlay.Text = "Play";
            btnPlay.Click += new EventHandler<MouseButtonEventArgs>(btnPlay_Click);

            btnStop = new Button("btnStop");
            btnStop.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnStop.Size = new System.Drawing.Size(75, 30);
            btnStop.Location = new Point(410, 230);
            Skins.SkinManager.LoadButtonGui(btnStop);
            btnStop.Text = "Stop";
            btnStop.Click += new EventHandler<MouseButtonEventArgs>(btnStop_Click);

            btnOk = new Button("btnOk");
            btnOk.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnOk.Size = new System.Drawing.Size(75, 30);
            btnOk.Location = new Point(0, 365);
            Skins.SkinManager.LoadButtonGui(btnOk);
            btnOk.Text = "Ok";
            btnOk.Click += new EventHandler<MouseButtonEventArgs>(btnOk_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            btnCancel.Size = new System.Drawing.Size(75, 30);
            btnCancel.Location = new Point(0, 395);
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            #endregion

            #region NPC

            //lblNpcSelector = new Label("lblNpcSelector");
            //lblNpcSelector.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcSelector.AutoSize = true;
            //lblNpcSelector.Location = new Point(5, 10);
            //lblNpcSelector.Text = "Select an NPC to edit:";

            //cmbNpcSelector = new ComboBox("cmbNpcSelector");
            //cmbNpcSelector.Size = new System.Drawing.Size(150, 22);
            //cmbNpcSelector.Location = new Point(lblNpcSelector.X + lblNpcSelector.Width + 5, lblNpcSelector.Y);
            //for (int i = 0; i < MaxInfo.MAX_MAP_NPCS; i++) {
            //    cmbNpcSelector.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("PMU", 18), "NPC " + (i + 1)));
            //}
            //cmbNpcSelector.ItemSelected += new EventHandler(cmbNpcSelector_ItemSelected);

            //lblNpcNum = new Label("lblNpcNum");
            //lblNpcNum.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcNum.AutoSize = true;
            //lblNpcNum.Location = new Point(lblNpcSelector.X, lblNpcSelector.Y + lblNpcSelector.Height + 10);
            //lblNpcNum.Text = "NPC Number:";

            //nudNpcNum = new NumericUpDown("nudNpcNum");
            //nudNpcNum.Size = new System.Drawing.Size(100, 18);
            //nudNpcNum.Location = new Point(lblNpcNum.X + lblNpcNum.Width + 5, lblNpcNum.Y);
            //nudNpcNum.Maximum = MaxInfo.MaxNpcs;
            //nudNpcNum.Minimum = 0;
            //nudNpcNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudNpcNum_ValueChanged);

            //lblNpcNumInfo = new Label("lblNpcNumInfo");
            //lblNpcNumInfo.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcNumInfo.AutoSize = true;
            //lblNpcNumInfo.Location = new Point(nudNpcNum.X + nudNpcNum.Width + 5, nudNpcNum.Y);

            //lblNpcSpawnX = new Label("lblNpcSpawnX");
            //lblNpcSpawnX.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcSpawnX.AutoSize = true;
            //lblNpcSpawnX.Location = new Point(lblNpcNum.X, lblNpcNum.Y + lblNpcNum.Height + 5);
            //lblNpcSpawnX.Text = "Spawn X:";

            //nudNpcSpawnX = new NumericUpDown("nudNpcSpawnX");
            //nudNpcSpawnX.Size = new System.Drawing.Size(100, 20);
            //nudNpcSpawnX.Location = new Point(lblNpcSpawnX.X + lblNpcSpawnX.Width + 5, lblNpcSpawnX.Y);
            //nudNpcSpawnX.Maximum = properties.MaxX;
            //nudNpcSpawnX.Minimum = 0;

            //lblNpcSpawnY = new Label("lblNpcSpawnY");
            //lblNpcSpawnY.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcSpawnY.AutoSize = true;
            //lblNpcSpawnY.Location = new Point(nudNpcSpawnX.X + nudNpcSpawnX.Width + 5, nudNpcSpawnX.Y);
            //lblNpcSpawnY.Text = "Spawn Y:";

            //nudNpcSpawnY = new NumericUpDown("nudNpcSpawnY");
            //nudNpcSpawnY.Size = new System.Drawing.Size(100, 20);
            //nudNpcSpawnY.Location = new Point(lblNpcSpawnY.X + lblNpcSpawnY.Width + 5, lblNpcSpawnY.Y);
            //nudNpcSpawnY.Maximum = properties.MaxY;
            //nudNpcSpawnY.Minimum = 0;

            //lblNpcLevel = new Label("lblNpcLevel");
            //lblNpcLevel.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //lblNpcLevel.AutoSize = true;
            //lblNpcLevel.Location = new Point(lblNpcSpawnX.X, lblNpcSpawnX.Y + lblNpcSpawnX.Height + 5);
            //lblNpcLevel.Text = "Level:";

            //nudNpcLevel = new NumericUpDown("nudNpcLevel");
            //nudNpcLevel.Size = new System.Drawing.Size(100, 20);
            //nudNpcLevel.Location = new Point(lblNpcLevel.X + lblNpcLevel.Width + 5, lblNpcLevel.Y);
            //nudNpcLevel.Maximum = 100;
            //nudNpcLevel.Minimum = 1;

            //btnSaveNpc = new Button("btnSaveNpc");
            //btnSaveNpc.Size = new System.Drawing.Size(100, 30);
            //btnSaveNpc.Location = new Point(lblNpcLevel.X, lblNpcLevel.Y + lblNpcLevel.Height + 5);
            //btnSaveNpc.Text = "Save NPC";
            //btnSaveNpc.Click += new EventHandler<MouseButtonEventArgs>(btnSaveNpc_Click);

            //btnClearNpc = new Button("btnClearNpc");
            //btnClearNpc.Size = new System.Drawing.Size(100, 30);
            //btnClearNpc.Location = new Point(btnSaveNpc.X + btnSaveNpc.Width + 5, btnSaveNpc.Y);
            //btnClearNpc.Text = "Clear NPC";
            //btnClearNpc.Click += new EventHandler<MouseButtonEventArgs>(btnClearNpc_Click);


            //ListBox lbxItems;
            lbxNpcs = new ListBox("lbxNpcs");
            lbxNpcs.Location = new Point(10, 200);
            lbxNpcs.Size = new Size(480, 280);
            lbxNpcs.Items.Clear();
            for (int npc = 0; npc < properties.Npcs.Count; npc++)
            {
                MapNpcSettings newNpc = new MapNpcSettings();
                newNpc.NpcNum = properties.Npcs[npc].NpcNum;
                newNpc.MinLevel = properties.Npcs[npc].MinLevel;
                newNpc.MaxLevel = properties.Npcs[npc].MaxLevel;
                newNpc.AppearanceRate = properties.Npcs[npc].AppearanceRate;
                newNpc.StartStatus = properties.Npcs[npc].StartStatus;
                //newNpc.StartStatusCounter = properties.Npcs[npc].StartStatusCounter;
                newNpc.StartStatusChance = properties.Npcs[npc].StartStatusChance;

                //properties.Npcs.Add(newNpc);
                lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                    + "(" + newNpc.AppearanceRate + "%) " + "Lv." + newNpc.MinLevel + "-" + newNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newNpc.NpcNum].Name
                    + " [" + newNpc.StartStatusChance + "% " + newNpc.StartStatus.ToString() + "]"));
            }

            //Label lblNpcSpawnTime;
            lblNpcSpawnTime = new Label("lblNpcSpawnTime");
            lblNpcSpawnTime.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcSpawnTime.Text = "Spawn Time:";
            lblNpcSpawnTime.AutoSize = true;
            lblNpcSpawnTime.Location = new Point(65, 10);
            //NumericUpDown nudNpcSpawnTime;
            nudNpcSpawnTime = new NumericUpDown("nudNpcSpawnTime");
            nudNpcSpawnTime.Minimum = 1;
            nudNpcSpawnTime.Maximum = 100;
            nudNpcSpawnTime.Size = new Size(80, 20);
            nudNpcSpawnTime.Location = new Point(65, 26);
            nudNpcSpawnTime.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            nudNpcSpawnTime.Value = properties.NpcSpawnTime;

            lblNpcMin = new Label("lblNpcMin");
            lblNpcMin.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcMin.Text = "Min Npcs";
            lblNpcMin.AutoSize = true;
            lblNpcMin.Location = new Point(210, 10);
            //NumericUpDown nudNpcMin;
            nudNpcMin = new NumericUpDown("nudNpcMin");
            nudNpcMin.Minimum = 0;
            nudNpcMin.Maximum = MaxInfo.MAX_MAP_NPCS;
            nudNpcMin.Size = new Size(80, 20);
            nudNpcMin.Location = new Point(210, 26);
            nudNpcMin.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            nudNpcMin.Value = properties.MinNpcs;

            //Label lblNpcMax;
            lblNpcMax = new Label("lblNpcMax");
            lblNpcMax.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcMax.Text = "Max Npcs";
            lblNpcMax.AutoSize = true;
            lblNpcMax.Location = new Point(355, 10);
            //NumericUpDown nudNpcMax;
            nudNpcMax = new NumericUpDown("nudNpcMax");
            nudNpcMax.Minimum = 0;
            nudNpcMax.Maximum = MaxInfo.MAX_MAP_NPCS;
            nudNpcMax.Size = new Size(80, 20);
            nudNpcMax.Location = new Point(355, 26);
            nudNpcMax.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            nudNpcMax.Value = properties.MaxNpcs;

            //Label lblNpcNum;
            lblNpcNum = new Label("lblNpcNum");
            lblNpcNum.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcNum.Text = "NPC #";
            lblNpcNum.AutoSize = true;
            lblNpcNum.Location = new Point(65, 70);
            //NumericUpDown nudNpcNum;
            nudNpcNum = new NumericUpDown("nudNpcNum");
            nudNpcNum.Minimum = 1;
            nudNpcNum.Maximum = MaxInfo.MaxItems;
            nudNpcNum.Size = new Size(80, 20);
            nudNpcNum.Location = new Point(65, 84);
            nudNpcNum.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            nudNpcNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudNpcNum_ValueChanged);

            //Label lblNpcSpawnX;
            lblNpcSpawnX = new Label("lblNpcSpawnX");
            lblNpcSpawnX.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcSpawnX.Text = "Spawn X";
            lblNpcSpawnX.AutoSize = true;
            lblNpcSpawnX.Location = new Point(210, 70);
            //NumericUpDown nudNpcSpawnX;
            nudNpcSpawnX = new NumericUpDown("nudNpcSpawnX");
            nudNpcSpawnX.Minimum = -1;
            nudNpcSpawnX.Maximum = properties.MaxX;
            nudNpcSpawnX.Size = new Size(80, 20);
            nudNpcSpawnX.Location = new Point(210, 84);
            nudNpcSpawnX.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            //Label lblNpcSpawnY;
            lblNpcSpawnY = new Label("lblNpcSpawnY");
            lblNpcSpawnY.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcSpawnY.Text = "Spawn Y";
            lblNpcSpawnY.AutoSize = true;
            lblNpcSpawnY.Location = new Point(355, 70);
            //NumericUpDown nudNpcSpawnY;
            nudNpcSpawnY = new NumericUpDown("nudNpcSpawnY");
            nudNpcSpawnY.Minimum = -1;
            nudNpcSpawnY.Maximum = properties.MaxY;
            nudNpcSpawnY.Size = new Size(80, 20);
            nudNpcSpawnY.Location = new Point(355, 84);
            nudNpcSpawnY.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            //Label lblNpcSpawnRate;
            lblNpcSpawnRate = new Label("lblNpcSpawnRate");
            lblNpcSpawnRate.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcSpawnRate.Text = "Spawn Rate:";
            lblNpcSpawnRate.AutoSize = true;
            lblNpcSpawnRate.Location = new Point(65, 104);
            //NumericUpDown nudNpcSpawnRate;
            nudNpcSpawnRate = new NumericUpDown("nudNpcSpawnRate");
            nudNpcSpawnRate.Minimum = 1;
            nudNpcSpawnRate.Maximum = 100;
            nudNpcSpawnRate.Size = new Size(80, 20);
            nudNpcSpawnRate.Location = new Point(65, 118);
            nudNpcSpawnRate.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            lblMinLevel = new Label("lblMinLevel");
            lblMinLevel.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblMinLevel.Text = "Min Level";
            lblMinLevel.AutoSize = true;
            lblMinLevel.Location = new Point(210, 104);
            //NumericUpDown nudMinLevel;
            nudMinLevel = new NumericUpDown("nudMinLevel");
            nudMinLevel.Minimum = 1;
            nudMinLevel.Maximum = 100;
            nudMinLevel.Size = new Size(80, 20);
            nudMinLevel.Location = new Point(210, 118);
            nudMinLevel.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            //Label lblMaxLevel;
            lblMaxLevel = new Label("lblMaxLevel");
            lblMaxLevel.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblMaxLevel.Text = "Max Level";
            lblMaxLevel.AutoSize = true;
            lblMaxLevel.Location = new Point(355, 104);
            //NumericUpDown nudMaxLevel;
            nudMaxLevel = new NumericUpDown("nudMaxLevel");
            nudMaxLevel.Minimum = 1;
            nudMaxLevel.Maximum = 100;
            nudMaxLevel.Size = new Size(80, 20);
            nudMaxLevel.Location = new Point(355, 118);
            nudMaxLevel.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            lblNpcStartStatus = new Label("lblNpcStartStatus");
            lblNpcStartStatus.Font = Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblNpcStartStatus.AutoSize = true;
            lblNpcStartStatus.Location = new Point(65, 138);
            lblNpcStartStatus.Text = "Start Status:";

            cbNpcStartStatus = new ComboBox("cbNpcStartStatus");
            cbNpcStartStatus.Size = new System.Drawing.Size(80, 20);
            cbNpcStartStatus.Location = new Point(65, 152);
            for (int i = 0; i < 6; i++) {
                cbNpcStartStatus.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("Tahoma", 10), ((Enums.StatusAilment)i).ToString()));
            }
            cbNpcStartStatus.SelectItem(0);

            lblStatusCounter = new Label("lblStatusCounter");
            lblStatusCounter.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblStatusCounter.Text = "Status Counter";
            lblStatusCounter.AutoSize = true;
            lblStatusCounter.Location = new Point(210, 138);
            //NumericUpDown nudStatusCounter;
            nudStatusCounter = new NumericUpDown("nudStatusCounter");
            nudStatusCounter.Minimum = Int32.MinValue;
            nudStatusCounter.Maximum = Int32.MaxValue;
            nudStatusCounter.Size = new Size(80, 20);
            nudStatusCounter.Location = new Point(210, 152);
            nudStatusCounter.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            //Label lblStatusChance;
            lblStatusChance = new Label("lblStatusChance");
            lblStatusChance.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
            lblStatusChance.Text = "Status Chance";
            lblStatusChance.AutoSize = true;
            lblStatusChance.Location = new Point(355, 138);
            //NumericUpDown nudStatusChance;
            nudStatusChance = new NumericUpDown("nudStatusChance");
            nudStatusChance.Minimum = 1;
            nudStatusChance.Maximum = 100;
            nudStatusChance.Size = new Size(80, 20);
            nudStatusChance.Location = new Point(355, 152);
            nudStatusChance.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

            //Button btnAddNpc;
            btnAddNpc = new Button("btnAddNpc");
            btnAddNpc.Location = new Point(10, 180);
            btnAddNpc.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddNpc.Size = new System.Drawing.Size(64, 16);
            btnAddNpc.Visible = true;
            btnAddNpc.Text = "Add Npc";
            btnAddNpc.Click += new EventHandler<MouseButtonEventArgs>(btnAddNpc_Click);
            //Button btnRemoveNpc;
            btnRemoveNpc = new Button("btnRemoveNpc");
            btnRemoveNpc.Location = new Point(110, 180);
            btnRemoveNpc.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveNpc.Size = new System.Drawing.Size(64, 16);
            btnRemoveNpc.Visible = true;
            btnRemoveNpc.Text = "Remove Npc";
            btnRemoveNpc.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveNpc_Click);
            //Button btnLoadNpc;
            btnLoadNpc = new Button("btnLoadNpc");
            btnLoadNpc.Location = new Point(210, 180);
            btnLoadNpc.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadNpc.Size = new System.Drawing.Size(64, 16);
            btnLoadNpc.Visible = true;
            btnLoadNpc.Text = "Load Npc";
            btnLoadNpc.Click += new EventHandler<MouseButtonEventArgs>(btnLoadNpc_Click);

            #endregion

            chkScroll = new CheckBox("chkScroll");
            chkScroll.Text = "Use scroll maps?";
            chkScroll.Size = new System.Drawing.Size(200, 16);
            chkScroll.Location = new Point(54, 60);
            chkScroll.BackColor = Color.Transparent;

            lblMaxX = new Label("lblMaxX");
            lblMaxX.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMaxX.AutoSize = true;
            lblMaxX.Location = new Point(54, 0);
            lblMaxX.Text = "Max X:";

            lblMaxY = new Label("lblMaxY");
            lblMaxY.Font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            lblMaxY.AutoSize = true;
            lblMaxY.Location = new Point(280, 0);
            lblMaxY.Text = "Max Y:";

            txtMaxX = new TextBox("txtMaxX");
            txtMaxX.Size = new System.Drawing.Size(134, 18);
            txtMaxX.Location = new Point(59, 25);
            txtMaxX.Text = properties.MaxX.ToString();

            txtMaxY = new TextBox("txtMaxY");
            txtMaxY.Size = new System.Drawing.Size(134, 18);
            txtMaxY.Location = new Point(285, 25);
            txtMaxY.Text = properties.MaxY.ToString();

            this.AddWidget(pnlGeneral);
            this.AddWidget(btnGeneral);
            this.AddWidget(pnlNPC);
            this.AddWidget(btnNPC);
            this.AddWidget(pnlScroll);
            this.AddWidget(btnScroll);

            #region General
            pnlGeneral.AddWidget(txtMapName);
            pnlGeneral.AddWidget(lblMapName);
            pnlGeneral.AddWidget(lblMapSwitchover);
            pnlGeneral.AddWidget(lblNorth);
            pnlGeneral.AddWidget(lblSouth);
            pnlGeneral.AddWidget(lblEast);
            pnlGeneral.AddWidget(lblWest);
            pnlGeneral.AddWidget(txtNorth);
            pnlGeneral.AddWidget(txtSouth);
            pnlGeneral.AddWidget(txtEast);
            pnlGeneral.AddWidget(txtWest);
            pnlGeneral.AddWidget(chkHunger);
            pnlGeneral.AddWidget(chkRecruit);
            pnlGeneral.AddWidget(chkExp);
            pnlGeneral.AddWidget(chkIndoors);
            pnlGeneral.AddWidget(chkInstanced);
            pnlGeneral.AddWidget(lblGlobal);
            pnlGeneral.AddWidget(lblMapMorality);
            pnlGeneral.AddWidget(lblMapWeather);
            pnlGeneral.AddWidget(lblMapDarkness);
            pnlGeneral.AddWidget(lblMapTimeLimit);
            pnlGeneral.AddWidget(cmbMapMorality);
            pnlGeneral.AddWidget(cmbMapWeather);
            pnlGeneral.AddWidget(nudDarkness);
            pnlGeneral.AddWidget(nudTimeLimit);
            pnlGeneral.AddWidget(lblMusic);
            pnlGeneral.AddWidget(cmbMusic);
            pnlGeneral.AddWidget(btnPlay);
            pnlGeneral.AddWidget(btnStop);
            pnlGeneral.AddWidget(btnOk);
            pnlGeneral.AddWidget(btnCancel);
            #endregion

            #region NPC
            //pnlNPC.AddWidget(lblNpcSelector);
            //pnlNPC.AddWidget(cmbNpcSelector);
            //pnlNPC.AddWidget(lblNpcNum);
            //pnlNPC.AddWidget(nudNpcNum);
            //pnlNPC.AddWidget(lblNpcNumInfo);
            //pnlNPC.AddWidget(lblNpcSpawnX);
            //pnlNPC.AddWidget(nudNpcSpawnX);
            //pnlNPC.AddWidget(lblNpcSpawnY);
            //pnlNPC.AddWidget(nudNpcSpawnY);
            //pnlNPC.AddWidget(lblNpcLevel);
            //pnlNPC.AddWidget(nudNpcLevel);
            //pnlNPC.AddWidget(btnSaveNpc);
            //pnlNPC.AddWidget(btnClearNpc);

            pnlNPC.AddWidget(lblNpcSpawnTime);
            pnlNPC.AddWidget(nudNpcSpawnTime);

            pnlNPC.AddWidget(lblNpcMin);
            pnlNPC.AddWidget(nudNpcMin);
            pnlNPC.AddWidget(lblNpcMax);
            pnlNPC.AddWidget(nudNpcMax);

            pnlNPC.AddWidget(lbxNpcs);

            pnlNPC.AddWidget(lblNpcNum);
            pnlNPC.AddWidget(nudNpcNum);
            pnlNPC.AddWidget(lblNpcSpawnX);
            pnlNPC.AddWidget(nudNpcSpawnX);
            pnlNPC.AddWidget(lblNpcSpawnY);
            pnlNPC.AddWidget(nudNpcSpawnY);
            pnlNPC.AddWidget(lblMinLevel);
            pnlNPC.AddWidget(nudMinLevel);
            pnlNPC.AddWidget(lblMaxLevel);
            pnlNPC.AddWidget(nudMaxLevel);
            pnlNPC.AddWidget(lblNpcSpawnRate);
            pnlNPC.AddWidget(nudNpcSpawnRate);
            pnlNPC.AddWidget(lblNpcStartStatus);
            pnlNPC.AddWidget(cbNpcStartStatus);
            pnlNPC.AddWidget(lblStatusCounter);
            pnlNPC.AddWidget(nudStatusCounter);
            pnlNPC.AddWidget(lblStatusChance);
            pnlNPC.AddWidget(nudStatusChance);
            pnlNPC.AddWidget(btnAddNpc);
            pnlNPC.AddWidget(btnRemoveNpc);
            pnlNPC.AddWidget(btnLoadNpc);
            #endregion

            pnlScroll.AddWidget(chkScroll);
            pnlScroll.AddWidget(txtMaxX);
            pnlScroll.AddWidget(txtMaxY);
            pnlScroll.AddWidget(lblMaxX);
            pnlScroll.AddWidget(lblMaxY);

            this.LoadComplete();

            LoadMusic();
        }

        //void btnClearNpc_Click(object sender, MouseButtonEventArgs e) {
        //    int npcIndex = cmbNpcSelector.SelectedIndex;
        //    if (npcIndex > -1) {
        //        properties.Npcs[npcIndex].NpcNum = 0;
        //        properties.Npcs[npcIndex].SpawnX = 0;
        //        properties.Npcs[npcIndex].SpawnY = 0;
                //properties.Npcs[npcIndex].Level = 1;
        
        //        nudNpcNum.Value = 0;
        //        nudNpcSpawnX.Value = 0;
        //        nudNpcSpawnY.Value = 0;
        //        nudNpcLevel.Value = 1;
        //    }
        //}

        //void btnSaveNpc_Click(object sender, MouseButtonEventArgs e) {
        //    int npcIndex = cmbNpcSelector.SelectedIndex;
        //    if (npcIndex > -1) {
        //        properties.Npcs[npcIndex].NpcNum = nudNpcNum.Value;
        //        properties.Npcs[npcIndex].SpawnX = nudNpcSpawnX.Value;
        //        properties.Npcs[npcIndex].SpawnY = nudNpcSpawnY.Value;
                //properties.Npcs[npcIndex].Level = nudNpcLevel.Value;
        //    }
        //}

        //void nudNpcNum_ValueChanged(object sender, ValueChangedEventArgs e) {
        //    if (e.NewValue != 0) {
        //        lblNpcNumInfo.Text = Npc.NpcHelper.Npcs[e.NewValue].Name;
        //    } else {
        //        lblNpcNumInfo.Text = "None";
        //    }
        //}

        //void cmbNpcSelector_ItemSelected(object sender, EventArgs e) {
        //    int npcIndex = cmbNpcSelector.SelectedIndex;
        //    if (npcIndex > -1) {
        //        nudNpcNum.Value = properties.Npcs[npcIndex].NpcNum;
        //        nudNpcSpawnX.Value = properties.Npcs[npcIndex].SpawnX;
        //        nudNpcSpawnY.Value = properties.Npcs[npcIndex].SpawnY;
                //nudNpcLevel.Value = properties.Npcs[npcIndex].Level;
        //    }
        //}

        void nudNpcNum_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e)
        {

            if (nudNpcNum.Value > 0 && nudNpcNum.Value < MaxInfo.MaxNpcs)
            {

                lblNpcNum.Text = Npc.NpcHelper.Npcs[nudNpcNum.Value].Name;

            }
            else
            {
                lblNpcNum.Text = "NPC #";
            }

        }

        void btnAddNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            if (nudNpcNum.Value > 0)
            {
                MapNpcSettings newNpc = new MapNpcSettings();
                newNpc.NpcNum = nudNpcNum.Value;
                newNpc.SpawnX = nudNpcSpawnX.Value;
                newNpc.SpawnY = nudNpcSpawnY.Value;
                newNpc.MinLevel = nudMinLevel.Value;
                newNpc.MaxLevel = nudMaxLevel.Value;
                newNpc.AppearanceRate = nudNpcSpawnRate.Value;
                newNpc.StartStatus = (Enums.StatusAilment)cbNpcStartStatus.SelectedIndex;
                newNpc.StartStatusCounter = nudStatusCounter.Value;
                newNpc.StartStatusChance = nudStatusChance.Value;

                properties.Npcs.Add(newNpc);
                lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), properties.Npcs.Count + ": "
                    + "(" + newNpc.AppearanceRate + "%) " + "Lv." + newNpc.MinLevel + "-" + newNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newNpc.NpcNum].Name
                    + " [" + newNpc.StartStatusChance + "% " + newNpc.StartStatus.ToString() + "]"));
            }
        }

        void btnLoadNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {

            if (lbxNpcs.SelectedIndex > -1)
            {
                nudNpcNum.Value = properties.Npcs[lbxNpcs.SelectedIndex].NpcNum;
                nudNpcSpawnX.Value = properties.Npcs[lbxNpcs.SelectedIndex].SpawnX;
                nudNpcSpawnY.Value = properties.Npcs[lbxNpcs.SelectedIndex].SpawnY;
                nudMinLevel.Value = properties.Npcs[lbxNpcs.SelectedIndex].MinLevel;
                nudMaxLevel.Value = properties.Npcs[lbxNpcs.SelectedIndex].MaxLevel;
                nudNpcSpawnRate.Value = properties.Npcs[lbxNpcs.SelectedIndex].AppearanceRate;
                cbNpcStartStatus.SelectItem((int)properties.Npcs[lbxNpcs.SelectedIndex].StartStatus);
                nudStatusCounter.Value = properties.Npcs[lbxNpcs.SelectedIndex].StartStatusCounter;
                nudStatusChance.Value = properties.Npcs[lbxNpcs.SelectedIndex].StartStatusChance;

            }

        }

        void btnRemoveNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            if (lbxNpcs.SelectedIndex > -1)
            {
                properties.Npcs.RemoveAt(lbxNpcs.SelectedIndex);


                lbxNpcs.Items.Clear();
                for (int npc = 0; npc < properties.Npcs.Count; npc++)
                {

                    lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                    + "(" + properties.Npcs[npc].AppearanceRate + "%) " + "Lv." + properties.Npcs[npc].MinLevel + "-" + properties.Npcs[npc].MaxLevel + " "
                    + Npc.NpcHelper.Npcs[properties.Npcs[npc].NpcNum].Name
                    + " [" + properties.Npcs[npc].StartStatusChance + "% " + properties.Npcs[npc].StartStatus.ToString() + "]"));
                }
            }

        }

        void btnStop_Click(object sender, MouseButtonEventArgs e) {
            // Stop the music
            Music.Music.AudioPlayer.StopMusic();
            // Play the map music
            if (!string.IsNullOrEmpty(MapHelper.ActiveMap.Music)) {
                //Music.Music.AudioPlayer.PlayMusic(MapHelper.ActiveMap.Music);
                ((Client.Logic.Music.Bass.BassAudioPlayer)Logic.Music.Music.AudioPlayer).FadeToNext(MapHelper.ActiveMap.Music, 1000);
            }
        }

        void btnPlay_Click(object sender, MouseButtonEventArgs e) {
            string song = null;
            if (cmbMusic.SelectedIndex > -1) {
                song = ((ListBoxTextItem)cmbMusic.SelectedItem).Text;
            }
            if (!string.IsNullOrEmpty(song)) {
                Music.Music.AudioPlayer.PlayMusic(song, -1, true, true);
            }
        }

        void btnOk_Click(object sender, MouseButtonEventArgs e) {
            bool maxDimensionsChanged = false;
            properties.Name = string.IsNullOrEmpty(txtMapName.Text) ? properties.Name : txtMapName.Text;
            properties.Right = string.IsNullOrEmpty(txtEast.Text) ? properties.Right : txtEast.Text.ToInt();
            properties.Left = string.IsNullOrEmpty(txtWest.Text) ? properties.Left : txtWest.Text.ToInt();
            properties.Up = string.IsNullOrEmpty(txtNorth.Text) ? properties.Up : txtNorth.Text.ToInt();
            properties.Down = string.IsNullOrEmpty(txtSouth.Text) ? properties.Down : txtSouth.Text.ToInt();
            properties.Music = (cmbMusic.SelectedItem == null || string.IsNullOrEmpty(cmbMusic.SelectedItem.TextIdentifier)) ? properties.Music : cmbMusic.SelectedItem.TextIdentifier;
            if (!string.IsNullOrEmpty(txtMaxX.Text)) {
                int oldMaxX = properties.MaxX;
                properties.MaxX = txtMaxX.Text.ToInt();
                if (properties.MaxX != oldMaxX) {
                    maxDimensionsChanged = true;
                }
            }
            if (!string.IsNullOrEmpty(txtMaxY.Text)) {
                int oldMaxY = properties.MaxY;
                properties.MaxY = txtMaxY.Text.ToInt();
                if (properties.MaxY != oldMaxY) {
                    maxDimensionsChanged = true;
                }
            }
            if (cmbMapMorality.SelectedIndex > -1) {
                properties.Moral = (Enums.MapMoral)cmbMapMorality.SelectedIndex;
            }
            
            properties.Darkness = nudDarkness.Value;

            properties.TimeLimit = nudTimeLimit.Value;
            
            if (cmbMapWeather.SelectedIndex > -1) {
                properties.Weather = (Enums.Weather)cmbMapWeather.SelectedIndex;
            }
            properties.Indoors = chkIndoors.Checked;
            properties.Belly = chkHunger.Checked;
            properties.Recruit = chkRecruit.Checked;
            properties.Exp = chkExp.Checked;
            properties.Instanced = chkInstanced.Checked;

            properties.NpcSpawnTime = nudNpcSpawnTime.Value;
            properties.MinNpcs = nudNpcMin.Value;
            properties.MaxNpcs = nudNpcMax.Value;

            if (maxDimensionsChanged) {
                Maps.MapHelper.ActiveMap.LoadFromPropertiesClass(properties);
                Network.Messenger.SendSaveMap(Maps.MapHelper.ActiveMap);
                this.Close();
            } else {
                Maps.MapHelper.ActiveMap.LoadFromPropertiesClass(properties);
                this.Close();
            }
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            this.Close();
        }

        #endregion Constructors

        #region Methods

        void btnGeneral_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnGeneral.Selected) {
                btnGeneral.Selected = true;
                btnNPC.Selected = false;
                btnScroll.Selected = false;
                pnlGeneral.Visible = true;
                pnlNPC.Visible = false;
                pnlScroll.Visible = false;
                this.Size = new System.Drawing.Size(500, 500);
            }
        }

        void btnNPC_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnNPC.Selected) {
                btnGeneral.Selected = false;
                btnNPC.Selected = true;
                btnScroll.Selected = false;
                pnlGeneral.Visible = false;
                pnlNPC.Visible = true;
                pnlScroll.Visible = false;
                this.Size = new System.Drawing.Size(500, 500);
            }
        }

        void btnScroll_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnScroll.Selected) {
                btnGeneral.Selected = false;
                btnNPC.Selected = false;
                btnScroll.Selected = true;
                pnlGeneral.Visible = false;
                pnlNPC.Visible = false;
                pnlScroll.Visible = true;
                this.Size = new System.Drawing.Size(500, 150);
            }
        }

        void LoadMusic()
        {
            System.Threading.Thread trackListDownloadThread = new System.Threading.Thread(new System.Threading.ThreadStart(LoadMusicBackground));
            trackListDownloadThread.IsBackground = true;
            trackListDownloadThread.Start();

            //SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            //string[] musicFiles = System.IO.Directory.GetFiles(IO.Paths.MusicPath);
            //for (int i = 0; i < musicFiles.Length; i++)
            //{
            //    cmbMusic.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(musicFiles[i])));
            //}
            //cmbMusic.SelectItem(properties.Music);
        }

        void LoadMusicBackground() {
            SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
            PMU.Net.FileDownloader downloader = new PMU.Net.FileDownloader();
            using (MemoryStream musicTrackListXml = new MemoryStream()) {
                downloader.DownloadFileQuick(IO.Options.MusicAddress+"/TrackList.xml", musicTrackListXml);
                Music.TrackList trackList = new Music.TrackList();
                musicTrackListXml.Seek(0, SeekOrigin.Begin);
                using (XmlReader reader = XmlReader.Create(musicTrackListXml)) {
                    trackList.Load(reader);
                }
                foreach (Music.TrackListEntry entry in trackList.Entries) {
                    cmbMusic.Items.Add(entry.TrackName);
                }
                //string[] musicFiles = System.IO.Directory.GetFiles(IO.Paths.MusicPath);
                //for (int i = 0; i < musicFiles.Length; i++) {
                //    cmbMusic.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(musicFiles[i])));
                //}
                cmbMusic.SelectItem(properties.Music);
            }
        }

        #endregion Methods
    }
}