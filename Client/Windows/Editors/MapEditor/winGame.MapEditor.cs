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


using SdlInput = SdlDotNet.Input;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;
using SdlDotNet.Graphics;
using Client.Logic.Network;
using PMU.Core;


namespace Client.Logic.Windows
{
    partial class winGame
    {
        #region Enumerations

        public enum MappingTool
        {
            Editor,
            EyeDropper
        }

        #endregion

        Enums.MapEditorLimitTypes limiter;

        Panel mapEditor_Menu;

        bool inLiveMode;
        Button btnTerrain;
        Button btnAttributes;
        Widgets.TilesetViewer tilesetViewer;

        MappingTool ActiveTool;

        #region Attributes Widgets

        Panel pnlAttributes;
        #region Attribute Options
        int Mode;
        Panel pnlAttOptions;
        //Button btnBack;
        Button btnOK;
        Button btnTitle;
        internal Label lbl1;
        Label lbl2;
        Label lbl3;
        Label lbl4;
        Label lblMode;
        TextBox txt1;
        TextBox txt2;
        TextBox txt3;
        HScrollBar hsb1;
        HScrollBar hsb2;
        HScrollBar hsb3;
        Rectangle rec = new Rectangle(96, 0, 32, 64);
        Rectangle rec2 = new Rectangle(192, 0, 64, 64);
        PictureBox picSprite;
        PictureBox picSprite2;
        ListBox lstSound;
        CheckBox chkTake;
        CheckBox chkHidden;
        RadioButton optAllow;
        RadioButton optBlock;
        NumericUpDown nudStoryLevel;
        bool[] tempArrayForMobility;
        #endregion Attribute Options
        RadioButton optBlocked;
        RadioButton optWarp;
        RadioButton optItem;
        RadioButton optNpcAvoid;
        RadioButton optKey;
        RadioButton optKeyOpen;
        RadioButton optHeal;
        RadioButton optKill;
        RadioButton optSound;
        RadioButton optScripted;
        RadioButton optNotice;
        RadioButton optLinkShop;
        RadioButton optDoor;
        RadioButton optSign;
        RadioButton optSpriteChange;
        RadioButton optShop;
        RadioButton optArena;
        RadioButton optBank;
        RadioButton optGuildBlock;
        RadioButton optSpriteBlock;
        RadioButton optMobileBlock;
        RadioButton optLevelBlock;
        RadioButton optAssembly;
        RadioButton optEvolution;
        RadioButton optStory;
        RadioButton optMission;
        RadioButton optScriptedSign;
        RadioButton optRoomWarp;
        RadioButton optAmbiguous;
        RadioButton optSlippery;
        RadioButton optSlow;
        RadioButton optDropShop;
        //RadioButton optHouseOwnerBlock;
        Label lblDungeonTileValue;
        NumericUpDown nudDungeonTileValue;

        #endregion

        #region Mapping Widgets

        Panel pnlMapping;
        Button btnMapping;

        Button btnMapProperties;
        Button btnHouseProperties;
        Button btnLoadMap;
        Button btnSaveMap;
        Button btnTakeScreenshot;
        public Button btnExit;

        #endregion

        #region Layers Widgets

        Button btnLayers;
        Panel pnlLayers;
        CheckBox chkAnim;
        RadioButton optGround;
        RadioButton optMask;
        RadioButton optMask2;
        RadioButton optFringe;
        RadioButton optFringe2;
        Button btnFill;
        Button btnClear;
        Button btnEyeDropper;

        #endregion

        #region Tileset Widgets

        Panel pnlTileset;
        Button btnTileset;
        HScrollBar hTilesetSelect;
        Label lblSelectedTileset;
        PictureBox[] picTilesetPreview;
        Button btnLoadTileset;

        #endregion

        #region Options Widgets

        Panel pnlSettings;
        Button btnSettings;

        Label lblDisplaySettings;
        CheckBox chkDisplayMapGrid;
        CheckBox chkDisplayAttributes;
        CheckBox chkDisplayDungeonValues;
        Label lblMappingSettings;
        CheckBox chkDragAndPlace;

        #endregion

        public void InitMapEditorWidgets() {
            mapEditor_Menu = new Panel("mapEditor_Menu");
            mapEditor_Menu.Location = new Point(0, this.shortcutBar.Y);
            mapEditor_Menu.Size = this.shortcutBar.Size;
            mapEditor_Menu.BackColor = Color.Transparent;

            tilesetViewer = new Widgets.TilesetViewer("tilesetViewer");
            tilesetViewer.Location = new Point(0, this.pnlTeam.Height + 32);
            tilesetViewer.Size = new Size(this.mapViewer.X, Screen.Height - pnlTeam.Height - shortcutBar.Height - 32);
            tilesetViewer.ActiveTilesetSurface = Graphics.GraphicsManager.Tiles[0];
            tilesetViewer.Visible = false;

            btnTerrain = new Button("btnTerrain");
            btnTerrain.Font = Graphics.FontManager.LoadFont("PMU", 18);
            btnTerrain.Location = new Point(0, this.pnlTeam.Height);
            btnTerrain.Size = new System.Drawing.Size(this.mapViewer.X / 2, 32);
            btnTerrain.Text = "Terrain";
            btnTerrain.Selected = true;
            btnTerrain.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnTerrain_Click);
            btnTerrain.Visible = false;
            Skins.SkinManager.LoadButtonGui(btnTerrain);

            btnAttributes = new Button("btnAttributes");
            btnAttributes.Font = Graphics.FontManager.LoadFont("PMU", 18);
            btnAttributes.Location = new Point(this.mapViewer.X / 2, this.pnlTeam.Height);
            btnAttributes.Size = new System.Drawing.Size(this.mapViewer.X / 2, 32);
            btnAttributes.Text = "Attributes";
            btnAttributes.Selected = false;
            btnAttributes.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnAttributes_Click);
            btnAttributes.Visible = false;
            Skins.SkinManager.LoadButtonGui(btnAttributes);

            #region Attributes Widgets

            pnlAttributes = new Panel("pnlAttributes");
            pnlAttributes.Size = tilesetViewer.Size;
            pnlAttributes.Location = tilesetViewer.Location;
            pnlAttributes.BackColor = Color.White;
            pnlAttributes.Visible = false;

            #region Attribute Options

            pnlAttOptions = new Panel("pnlAttOptions");
            pnlAttOptions.Size = tilesetViewer.Size;
            pnlAttOptions.Location = new Point(0, this.pnlTeam.Height);
            pnlAttOptions.BackColor = Color.White;
            pnlAttOptions.Visible = false;

            btnTitle = new Button("btnTitle");
            btnTitle.Location = new Point(0, 0);
            btnTitle.Size = new Size(134, 32);
            btnTitle.Font = Graphics.FontManager.LoadFont("PMU", 18);
            btnTitle.Visible = false;
            Skins.SkinManager.LoadButtonGui(btnTitle);

            lbl1 = new Label("lbl1");
            lbl1.AutoSize = true;
            lbl1.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lbl1.Location = new Point(0, 35);
            lbl1.Visible = false;

            lbl2 = new Label("lbl2");
            lbl2.AutoSize = true;
            lbl2.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lbl2.Visible = false;

            lbl3 = new Label("lbl3");
            lbl3.AutoSize = true;
            lbl3.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lbl3.Visible = false;

            lbl4 = new Label("lbl4");
            lbl4.AutoSize = true;
            lbl4.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lbl4.Visible = false;

            lblMode = new Label("lblMode");
            lblMode.AutoSize = true;
            lblMode.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblMode.Location = new Point();
            lblMode.Visible = false;

            txt1 = new TextBox("txt1");
            txt1.Size = new Size(134, 18);
            txt1.Visible = false;

            txt2 = new TextBox("txt2");
            txt2.Size = new Size(134, 18);
            txt2.Visible = false;

            txt3 = new TextBox("txt3");
            txt3.Size = new Size(134, 18);
            txt3.Visible = false;

            hsb1 = new HScrollBar("hsb1");
            hsb1.Size = new Size(134, 20);
            hsb1.Visible = false;
            hsb1.ValueChanged += new EventHandler<ValueChangedEventArgs>(hsb1_ValueChanged);

            hsb2 = new HScrollBar("hsb2");
            hsb2.Size = new Size(134, 20);
            hsb2.Visible = false;
            hsb2.ValueChanged += new EventHandler<ValueChangedEventArgs>(hsb2_ValueChanged);

            hsb3 = new HScrollBar("hsb3");
            hsb3.Size = new Size(134, 20);
            hsb3.Visible = false;
            hsb3.ValueChanged += new EventHandler<ValueChangedEventArgs>(hsb3_ValueChanged);

            picSprite = new PictureBox("picSprite");
            picSprite.Size = new Size(32, 64);
            picSprite.BlitToBuffer(Graphics.GraphicsManager.GetSpriteSheet(hsb1.Value).GetSheet(Graphics.FrameType.Idle, Enums.Direction.Down), new Rectangle(96, 0, 32, 64));
            
            picSprite.Location = new Point(140, 35);
            picSprite.BackColor = Color.White;
            picSprite.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            picSprite.Visible = false;

            picSprite2 = new PictureBox("picSprite2");
            picSprite2.Size = new Size(32, 64);
            picSprite2.BlitToBuffer(Graphics.GraphicsManager.GetSpriteSheet(hsb2.Value).GetSheet(Graphics.FrameType.Idle, Enums.Direction.Down), new Rectangle(96, 0, 32, 64));

            picSprite2.Location = new Point(75, 130);
            picSprite2.BackColor = Color.White;
            picSprite2.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            picSprite2.Visible = false;

            lstSound = new ListBox("lstSound");
            lstSound.Location = new Point(10, 60);
            lstSound.Size = new Size(180, 140);
            {
                SdlDotNet.Graphics.Font font = Logic.Graphics.FontManager.LoadFont("PMU", 18);
                string[] sfxFiles = System.IO.Directory.GetFiles(IO.Paths.SfxPath);
                for (int i = 0; i < sfxFiles.Length; i++) {
                    lstSound.Items.Add(new ListBoxTextItem(font, System.IO.Path.GetFileName(sfxFiles[i])));
                }
            }
            lstSound.Visible = false;

            chkTake = new CheckBox("chkTake");
            chkTake.BackColor = Color.Transparent;
            chkTake.Size = new System.Drawing.Size(125, 17);
            chkTake.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            chkTake.Text = "Take away key upon use";
            chkTake.Visible = false;

            chkHidden = new CheckBox("chkHidden");
            chkHidden.BackColor = Color.Transparent;
            chkHidden.Size = new System.Drawing.Size(125, 17);
            chkHidden.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            chkHidden.Text = "Hidden";
            chkHidden.Visible = false;
            chkHidden.CheckChanged += new EventHandler(chkHidden_CheckChanged);

            nudStoryLevel = new NumericUpDown("nudStoryLevel");
            nudStoryLevel.Size = new Size(134, 20);
            nudStoryLevel.Font = Graphics.FontManager.LoadFont("PMU", 18);
            nudStoryLevel.Visible = false;

            optAllow = new RadioButton("optAllow");
            optAllow.Size = new Size(75, 17);
            optAllow.Location = new Point(0, 200);
            optAllow.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optAllow.Text = "Allow";
            optAllow.Visible = false;
            optAllow.CheckChanged += new EventHandler(optAllow_CheckChanged);

            optBlock = new RadioButton("optBlock");
            optBlock.Size = new Size(75, 17);
            optBlock.Location = new Point(100, 200);
            optBlock.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optBlock.Text = "Block";
            optBlock.Visible = false;
            optBlock.CheckChanged += new EventHandler(optBlock_CheckChanged);

            btnOK = new Button("btnOK");
            btnOK.Location = new Point(this.mapViewer.X / 4, Screen.Height - shortcutBar.Height - 32);
            btnOK.Size = new System.Drawing.Size(this.mapViewer.X / 2, 32);
            btnOK.Font = Graphics.FontManager.LoadFont("PMU", 18);
            btnOK.Text = "Ok";
            btnOK.Visible = false;
            btnOK.Click += new EventHandler<MouseButtonEventArgs>(btnOK_Click);
            Skins.SkinManager.LoadButtonGui(btnOK);

            /*btnBack = new Button("btnBack");
            btnBack.Location = new Point(0, Screen.Height - shortcutBar.Height - 32);
            btnBack.Size = new System.Drawing.Size(this.mapViewer.X / 2, 32);
            btnBack.Font = Graphics.FontManager.LoadFont("PMU", 18);
            btnBack.Text = "Back";
            btnBack.Visible = false;
            btnBack.Click +=new EventHandler<MouseButtonEventArgs>(btnBack_Click);
            Skins.SkinManager.LoadButtonGui(btnBack);*/

            pnlAttOptions.AddWidget(lbl1);
            pnlAttOptions.AddWidget(lbl2);
            pnlAttOptions.AddWidget(lbl3);
            pnlAttOptions.AddWidget(lbl4);
            pnlAttOptions.AddWidget(lblMode);
            pnlAttOptions.AddWidget(txt1);
            pnlAttOptions.AddWidget(txt2);
            pnlAttOptions.AddWidget(txt3);
            pnlAttOptions.AddWidget(hsb1);
            pnlAttOptions.AddWidget(hsb2);
            pnlAttOptions.AddWidget(hsb3);
            pnlAttOptions.AddWidget(picSprite);
            pnlAttOptions.AddWidget(picSprite2);
            pnlAttOptions.AddWidget(lstSound);
            pnlAttOptions.AddWidget(chkTake);
            pnlAttOptions.AddWidget(chkHidden);
            pnlAttOptions.AddWidget(nudStoryLevel);
            pnlAttOptions.AddWidget(optAllow);
            pnlAttOptions.AddWidget(optBlock);
            pnlAttOptions.AddWidget(btnTitle);

            #endregion

            optBlocked = new RadioButton("optBlocked");
            optBlocked.BackColor = Color.Transparent;
            optBlocked.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optBlocked.Location = new Point(8, 6);
            optBlocked.Size = new System.Drawing.Size(95, 17);
            optBlocked.Text = "Blocked";
            optBlocked.Checked = true;

            optNpcAvoid = new RadioButton("optNpcAvoid");
            optNpcAvoid.BackColor = Color.Transparent;
            optNpcAvoid.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optNpcAvoid.Location = new Point(8, 75);
            optNpcAvoid.Size = new System.Drawing.Size(95, 17);
            optNpcAvoid.Text = "Npc Avoid";

            optNotice = new RadioButton("optNotice");
            optNotice.BackColor = Color.Transparent;
            optNotice.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optNotice.Location = new Point(8, 236);
            optNotice.Size = new System.Drawing.Size(95, 17);
            optNotice.Text = "Notice";
            optNotice.Click += new EventHandler<MouseButtonEventArgs>(optNotice_CheckChanged);

            optSign = new RadioButton("optSign");
            optSign.BackColor = Color.Transparent;
            optSign.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSign.Location = new Point(105, 6);
            optSign.Size = new System.Drawing.Size(95, 17);
            optSign.Text = "Sign";
            optSign.Click += new EventHandler<MouseButtonEventArgs>(optSign_CheckChanged);

            optHeal = new RadioButton("optHeal");
            optHeal.BackColor = Color.Transparent;
            optHeal.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optHeal.Location = new Point(8, 144);
            optHeal.Size = new System.Drawing.Size(95, 17);
            optHeal.Text = "Heal";

            optKill = new RadioButton("optKill");
            optKill.BackColor = Color.Transparent;
            optKill.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optKill.Location = new Point(8, 167);
            optKill.Size = new System.Drawing.Size(95, 17);
            optKill.Text = "Kill";

            optGuildBlock = new RadioButton("optGuildBlock");
            optGuildBlock.BackColor = Color.Transparent;
            optGuildBlock.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optGuildBlock.Location = new Point(105, 98);
            optGuildBlock.Size = new System.Drawing.Size(95, 17);
            optGuildBlock.Text = "Guild";
            //optGuildBlock.Click += new EventHandler<MouseButtonEventArgs>(optGuildBlock_CheckChanged);

            optLevelBlock = new RadioButton("optLevelBlock");
            optLevelBlock.BackColor = Color.Transparent;
            optLevelBlock.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optLevelBlock.Location = new Point(105, 144);
            optLevelBlock.Size = new System.Drawing.Size(95, 17);
            optLevelBlock.Text = "Level Block";
            optLevelBlock.Click += new EventHandler<MouseButtonEventArgs>(optLevelBlock_CheckChanged);

            optSpriteChange = new RadioButton("optSpriteChange");
            optSpriteChange.BackColor = Color.Transparent;
            optSpriteChange.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSpriteChange.Location = new Point(8, 282);
            optSpriteChange.Size = new System.Drawing.Size(112, 17);
            optSpriteChange.Text = "New Sprite";
            optSpriteChange.Click += new EventHandler<MouseButtonEventArgs>(optSpriteChange_CheckChanged);

            optWarp = new RadioButton("optWarp");
            optWarp.BackColor = Color.Transparent;
            optWarp.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optWarp.Location = new Point(8, 29);
            optWarp.Size = new System.Drawing.Size(95, 17);
            optWarp.Text = "Warp";
            optWarp.CheckChanged += new EventHandler(optWarp_CheckChanged);

            optItem = new RadioButton("optItem");
            optItem.BackColor = Color.Transparent;
            optItem.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optItem.Location = new Point(8, 52);
            optItem.Size = new System.Drawing.Size(95, 17);
            optItem.Text = "Item";
            optItem.CheckChanged += new EventHandler(optItem_CheckChanged);

            optKey = new RadioButton("optKey");
            optKey.BackColor = Color.Transparent;
            optKey.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optKey.Location = new Point(8, 98);
            optKey.Size = new System.Drawing.Size(95, 17);
            optKey.Text = "Key";
            optKey.CheckChanged += new EventHandler(optKey_CheckChanged);

            optKeyOpen = new RadioButton("optKeyOpen");
            optKeyOpen.BackColor = Color.Transparent;
            optKeyOpen.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optKeyOpen.Location = new Point(8, 121);
            optKeyOpen.Size = new System.Drawing.Size(95, 17);
            optKeyOpen.Text = "Key Open";
            optKeyOpen.CheckChanged += new EventHandler(optKeyOpen_CheckChanged);

            optSound = new RadioButton("optSound");
            optSound.BackColor = Color.Transparent;
            optSound.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSound.Location = new Point(8, 190);
            optSound.Size = new System.Drawing.Size(95, 17);
            optSound.Text = "Sound";
            optSound.CheckChanged += new EventHandler(optSound_CheckChanged);

            optScripted = new RadioButton("optScripted");
            optScripted.BackColor = Color.Transparent;
            optScripted.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optScripted.Location = new Point(8, 213);
            optScripted.Size = new System.Drawing.Size(95, 17);
            optScripted.Text = "Scripted";
            optScripted.CheckChanged += new EventHandler(optScripted_CheckChanged);

            optDoor = new RadioButton("optDoor");
            optDoor.BackColor = Color.Transparent;
            optDoor.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optDoor.Location = new Point(8, 259);
            optDoor.Size = new System.Drawing.Size(95, 17);
            optDoor.Text = "Door";

            optShop = new RadioButton("optShop");
            optShop.BackColor = Color.Transparent;
            optShop.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optShop.Location = new Point(105, 29);
            optShop.Size = new System.Drawing.Size(95, 17);
            optShop.Text = "Shop";
            optShop.CheckChanged += new EventHandler(optShop_CheckChanged);

            optArena = new RadioButton("optArena");
            optArena.BackColor = Color.Transparent;
            optArena.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optArena.Location = new Point(105, 52);
            optArena.Size = new System.Drawing.Size(95, 17);
            optArena.Text = "Arena";
            optArena.CheckChanged += new EventHandler(optArena_CheckChanged);

            optBank = new RadioButton("optBank");
            optBank.BackColor = Color.Transparent;
            optBank.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optBank.Location = new Point(105, 75);
            optBank.Size = new System.Drawing.Size(95, 17);
            optBank.Text = "Bank";
            optBank.CheckChanged += new EventHandler(optBank_CheckChanged);

            optSpriteBlock = new RadioButton("optSpriteBlock");
            optSpriteBlock.BackColor = Color.Transparent;
            optSpriteBlock.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSpriteBlock.Location = new Point(105, 121);
            optSpriteBlock.Size = new System.Drawing.Size(105, 17);
            optSpriteBlock.Text = "Sprite Block";
            optSpriteBlock.CheckChanged += new EventHandler(optSpriteBlock_CheckChanged);

            optAssembly = new RadioButton("optAssembly");
            optAssembly.BackColor = Color.Transparent;
            optAssembly.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optAssembly.Location = new Point(8, 328);
            optAssembly.Size = new System.Drawing.Size(145, 17);
            optAssembly.Text = "Assembly";

            optEvolution = new RadioButton("optEvolution");
            optEvolution.BackColor = Color.Transparent;
            optEvolution.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optEvolution.Location = new Point(105, 167);
            optEvolution.Size = new System.Drawing.Size(95, 17);
            optEvolution.Text = "Evolution";

            optStory = new RadioButton("optStory");
            optStory.BackColor = Color.Transparent;
            optStory.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optStory.Location = new Point(105, 190);
            optStory.Size = new System.Drawing.Size(95, 17);
            optStory.Text = "Story";
            optStory.CheckChanged += new EventHandler(optStory_CheckChanged);

            optLinkShop = new RadioButton("optLinkShop");
            optLinkShop.BackColor = Color.Transparent;
            optLinkShop.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optLinkShop.Location = new Point(105, 213);
            optLinkShop.Size = new System.Drawing.Size(95, 17);
            optLinkShop.Text = "Link Shop";
            optLinkShop.CheckChanged += new EventHandler(optLinkShop_CheckChanged);

            optMobileBlock = new RadioButton("optMobileBlock");
            optMobileBlock.BackColor = Color.Transparent;
            optMobileBlock.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optMobileBlock.Location = new Point(105, 236);
            optMobileBlock.Size = new System.Drawing.Size(105, 17);
            optMobileBlock.Text = "Mobile Block";
            optMobileBlock.CheckChanged += new EventHandler(optMobileBlock_CheckChanged);

            optMission = new RadioButton("optMission");
            optMission.BackColor = Color.Transparent;
            optMission.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optMission.Location = new Point(8, 351);
            optMission.Size = new System.Drawing.Size(115, 17);
            optMission.Text = "Mission Board";

            optScriptedSign = new RadioButton("optScriptedSign");
            optScriptedSign.BackColor = Color.Transparent;
            optScriptedSign.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optScriptedSign.Location = new Point(8, 305);
            optScriptedSign.Size = new System.Drawing.Size(105, 17);
            optScriptedSign.Text = "Scripted Sign";
            optScriptedSign.CheckChanged += new EventHandler(optScriptedSign_CheckChanged);

            optAmbiguous = new RadioButton("optAmbiguous");
            optAmbiguous.BackColor = Color.Transparent;
            optAmbiguous.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optAmbiguous.Location = new Point(105, 259);
            optAmbiguous.Size = new System.Drawing.Size(95, 17);
            optAmbiguous.Text = "Ambiguous";

            optSlippery = new RadioButton("optSlippery");
            optSlippery.BackColor = Color.Transparent;
            optSlippery.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSlippery.Location = new Point(105, 282);
            optSlippery.Size = new System.Drawing.Size(95, 17);
            optSlippery.Text = "Slippery";
            optSlippery.CheckChanged += new EventHandler(optSlippery_CheckChanged);

            optSlow = new RadioButton("optSlow");
            optSlow.BackColor = Color.Transparent;
            optSlow.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optSlow.Location = new Point(105, 305);
            optSlow.Size = new System.Drawing.Size(95, 17);
            optSlow.Text = "Slow";
            optSlow.CheckChanged += new EventHandler(optSlow_CheckChanged);

            optDropShop = new RadioButton("optDropShop");
            optDropShop.BackColor = Color.Transparent;
            optDropShop.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optDropShop.Location = new Point(105, 328);
            optDropShop.Size = new System.Drawing.Size(95, 17);
            optDropShop.Text = "DropShop";
            optDropShop.CheckChanged += new EventHandler(optDropShop_CheckChanged);

            lblDungeonTileValue = new Label("lblDungeonTileValue");
            lblDungeonTileValue.AutoSize = true;
            lblDungeonTileValue.Font = Graphics.FontManager.LoadFont("PMU", 18);
            lblDungeonTileValue.Text = "Dungeon Tile Value:";
            lblDungeonTileValue.Location = new Point(8, 376);

            nudDungeonTileValue = new NumericUpDown("nudDungeonTileValue");
            nudDungeonTileValue.Size = new Size(134, 20);
            nudDungeonTileValue.Maximum = Int32.MaxValue;
            nudDungeonTileValue.Font = Graphics.FontManager.LoadFont("PMU", 18);
            nudDungeonTileValue.Location = new Point(10, 400);

            #region House Editor Attributes

            optRoomWarp = new RadioButton("optRoomWarp");
            optRoomWarp.BackColor = Color.Transparent;
            optRoomWarp.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            optRoomWarp.Location = new Point(115, 6);
            optRoomWarp.Size = new System.Drawing.Size(95, 17);
            optRoomWarp.Text = "Warp to Room";

            #endregion

            tempArrayForMobility = new bool[16];

            pnlAttributes.AddWidget(optBlocked);
            pnlAttributes.AddWidget(optWarp);
            pnlAttributes.AddWidget(optItem);
            pnlAttributes.AddWidget(optNpcAvoid);
            pnlAttributes.AddWidget(optKey);
            pnlAttributes.AddWidget(optKeyOpen);
            pnlAttributes.AddWidget(optHeal);
            pnlAttributes.AddWidget(optKill);
            pnlAttributes.AddWidget(optSound);
            pnlAttributes.AddWidget(optScripted);
            pnlAttributes.AddWidget(optNotice);
            pnlAttributes.AddWidget(optDoor);
            pnlAttributes.AddWidget(optSign);
            pnlAttributes.AddWidget(optSpriteChange);
            pnlAttributes.AddWidget(optShop);
            pnlAttributes.AddWidget(optArena);
            pnlAttributes.AddWidget(optBank);
            pnlAttributes.AddWidget(optGuildBlock);
            pnlAttributes.AddWidget(optSpriteBlock);
            pnlAttributes.AddWidget(optMobileBlock);
            pnlAttributes.AddWidget(optLevelBlock);
            pnlAttributes.AddWidget(optAssembly);
            pnlAttributes.AddWidget(optEvolution);
            pnlAttributes.AddWidget(optStory);
            pnlAttributes.AddWidget(optLinkShop);
            pnlAttributes.AddWidget(optMission);
            pnlAttributes.AddWidget(optScriptedSign);
            pnlAttributes.AddWidget(optAmbiguous);
            pnlAttributes.AddWidget(optSlippery);
            pnlAttributes.AddWidget(optSlow);
            pnlAttributes.AddWidget(optDropShop);
            pnlAttributes.AddWidget(lblDungeonTileValue);
            pnlAttributes.AddWidget(nudDungeonTileValue);

            #endregion

            #region Mapping Widgets

            btnMapping = new Button("btnMapping");
            btnMapping.Size = new System.Drawing.Size(100, 30);
            btnMapping.Location = new Point(0, 0);
            btnMapping.Font = Graphics.FontManager.LoadFont("PMU.ttf", 24);
            btnMapping.Text = "Mapping";
            btnMapping.MouseHoverDelay = 100;
            btnMapping.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnMapping_Click);
            btnMapping.MouseHover += new EventHandler(btnMapping_MouseHover);
            Skins.SkinManager.LoadButtonGui(btnMapping);

            pnlMapping = new Panel("pnlMapping");
            pnlMapping.Size = new Size(150, 190);
            pnlMapping.Location = new Point(0, btnMapping.Y + mapEditor_Menu.Y - pnlMapping.Height);
            pnlMapping.BackColor = Color.White;
            pnlMapping.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pnlMapping.SetAutoHide();

            btnMapProperties = new Button("btnMapProperties");
            btnMapProperties.Size = new System.Drawing.Size(100, 30);
            btnMapProperties.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnMapProperties.Width), 10);
            btnMapProperties.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnMapProperties.Text = "Properties";
            btnMapProperties.Click += new EventHandler<MouseButtonEventArgs>(btnMapProperties_Click);
            Skins.SkinManager.LoadButtonGui(btnMapProperties);

            btnHouseProperties = new Button("btnHouseProperties");
            btnHouseProperties.Size = new System.Drawing.Size(100, 30);
            btnHouseProperties.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnMapProperties.Width), 10);
            btnHouseProperties.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnHouseProperties.Text = "Music";
            btnHouseProperties.Click += new EventHandler<MouseButtonEventArgs>(btnHouseProperties_Click);
            Skins.SkinManager.LoadButtonGui(btnHouseProperties);

            btnLoadMap = new Button("btnLoadMap");
            btnLoadMap.Size = new System.Drawing.Size(100, 30);
            btnLoadMap.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnLoadMap.Width), 40);
            btnLoadMap.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnLoadMap.Text = "Load Map";
            btnLoadMap.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLoadMap_Click);
            Skins.SkinManager.LoadButtonGui(btnLoadMap);

            btnSaveMap = new Button("btnSaveMap");
            btnSaveMap.Size = new System.Drawing.Size(100, 30);
            btnSaveMap.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnSaveMap.Width), 70);
            btnSaveMap.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnSaveMap.Text = "Save Map";
            btnSaveMap.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnSaveMap_Click);
            Skins.SkinManager.LoadButtonGui(btnSaveMap);

            btnTakeScreenshot = new Button("btnTakeScreenshot");
            btnTakeScreenshot.Size = new System.Drawing.Size(100, 30);
            btnTakeScreenshot.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnTakeScreenshot.Width), 100);
            btnTakeScreenshot.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnTakeScreenshot.Text = "Take Screenshot";
            btnTakeScreenshot.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnTakeScreenshot_Click);
            Skins.SkinManager.LoadButtonGui(btnTakeScreenshot);

            btnExit = new Button("btnExit");
            btnExit.Size = new System.Drawing.Size(100, 30);
            btnExit.Location = new Point(DrawingSupport.GetCenter(pnlMapping.Width, btnExit.Width), 130);
            btnExit.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            btnExit.Text = "Exit";
            btnExit.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnExit_Click);
            Skins.SkinManager.LoadButtonGui(btnExit);

            pnlMapping.AddWidget(btnMapProperties);
            pnlMapping.AddWidget(btnHouseProperties);
            pnlMapping.AddWidget(btnLoadMap);
            pnlMapping.AddWidget(btnSaveMap);
            pnlMapping.AddWidget(btnTakeScreenshot);
            pnlMapping.AddWidget(btnExit);

            #endregion

            #region Layer Widgets

            btnLayers = new Button("btnLayers");
            btnLayers.Size = new System.Drawing.Size(100, 30);
            btnLayers.Location = new Point(100, 0);
            btnLayers.Font = Graphics.FontManager.LoadFont("PMU.ttf", 24);
            btnLayers.Text = "Layers";
            btnLayers.MouseHoverDelay = 100;
            btnLayers.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLayers_Click);
            btnLayers.MouseHover += new EventHandler(btnLayers_MouseHover);
            Skins.SkinManager.LoadButtonGui(btnLayers);

            pnlLayers = new Panel("pnlLayers");
            pnlLayers.Size = new Size(350, 100);
            pnlLayers.Location = new Point(100, btnLayers.Y + mapEditor_Menu.Y - pnlLayers.Height);
            pnlLayers.BackColor = Color.White;
            pnlLayers.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pnlLayers.SetAutoHide();

            optGround = new RadioButton("optGround");
            optGround.BackColor = Color.Transparent;
            optGround.Location = new Point(8, 19);
            optGround.Size = new Size(60, 17);
            optGround.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            optGround.Text = "Ground";
            optGround.Checked = true;

            optMask = new RadioButton("optMask");
            optMask.BackColor = Color.Transparent;
            optMask.Location = new Point(74, 19);
            optMask.Size = new Size(51, 17);
            optMask.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            optMask.Text = "Mask";

            optMask2 = new RadioButton("optMask2");
            optMask2.BackColor = Color.Transparent;
            optMask2.Location = new Point(131, 19);
            optMask2.Size = new Size(60, 17);
            optMask2.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            optMask2.Text = "Mask 2";

            optFringe = new RadioButton("optFringe");
            optFringe.BackColor = Color.Transparent;
            optFringe.Location = new Point(197, 19);
            optFringe.Size = new Size(54, 17);
            optFringe.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            optFringe.Text = "Fringe";

            optFringe2 = new RadioButton("optFringe2");
            optFringe2.BackColor = Color.Transparent;
            optFringe2.Location = new Point(257, 19);
            optFringe2.Size = new Size(63, 17);
            optFringe2.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            optFringe2.Text = "Fringe 2";

            chkAnim = new CheckBox("chkAnim");
            chkAnim.BackColor = Color.Transparent;
            chkAnim.Location = new Point(8, 50);
            chkAnim.Size = new Size(70, 17);
            chkAnim.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            chkAnim.Text = "Animation";

            btnFill = new Button("btnFill");
            btnFill.Location = new Point(8, 70);
            btnFill.Size = new System.Drawing.Size(30, 20);
            btnFill.Text = "Fill";
            btnFill.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            btnFill.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnFill_Click);
            Skins.SkinManager.LoadButtonGui(btnFill);

            btnClear = new Button("btnClear");
            btnClear.Location = new Point(40, 70);
            btnClear.Size = new System.Drawing.Size(30, 20);
            btnClear.Text = "Clear";
            btnClear.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 8);
            btnClear.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnClear_Click);
            Skins.SkinManager.LoadButtonGui(btnClear);

            btnEyeDropper = new Button("btnEyeDropper");
            btnEyeDropper.Location = new Point(72, 70);
            btnEyeDropper.Size = new System.Drawing.Size(100, 20);
            btnEyeDropper.Text = "Eye Dropper";
            btnEyeDropper.Click += new EventHandler<MouseButtonEventArgs>(btnEyeDropper_Click);
            Skins.SkinManager.LoadButtonGui(btnEyeDropper);

            pnlLayers.AddWidget(optGround);
            pnlLayers.AddWidget(optMask);
            pnlLayers.AddWidget(optMask2);
            pnlLayers.AddWidget(optFringe);
            pnlLayers.AddWidget(optFringe2);
            pnlLayers.AddWidget(chkAnim);
            pnlLayers.AddWidget(btnFill);
            pnlLayers.AddWidget(btnClear);
            pnlLayers.AddWidget(btnEyeDropper);

            #endregion

            #region Tileset Widgets

            btnTileset = new Button("btnTileset");
            btnTileset.Size = new System.Drawing.Size(100, 30);
            btnTileset.Location = new Point(200, 0);
            btnTileset.Font = Graphics.FontManager.LoadFont("PMU.ttf", 24);
            btnTileset.Text = "Tilesets";
            btnTileset.MouseHoverDelay = 100;
            btnTileset.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnTileset_Click);
            btnTileset.MouseHover += new EventHandler(btnTileset_MouseHover);
            Skins.SkinManager.LoadButtonGui(btnTileset);

            pnlTileset = new Panel("pnlTileset");
            pnlTileset.Size = new Size(300, 100);
            pnlTileset.Location = new Point(200, btnTileset.Y + mapEditor_Menu.Y - pnlTileset.Height);
            pnlTileset.BackColor = Color.White;
            pnlTileset.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pnlTileset.SetAutoHide();

            hTilesetSelect = new HScrollBar("hTilesetSelect");
            hTilesetSelect.Location = new Point(5, 60);
            hTilesetSelect.Size = new Size(290, 12);
            hTilesetSelect.Maximum = 10;
            hTilesetSelect.ValueChanged += new EventHandler<ValueChangedEventArgs>(hTilesetSelect_ValueChanged);

            lblSelectedTileset = new Label("lblSelectedTileset");
            lblSelectedTileset.AutoSize = true;
            lblSelectedTileset.Location = new Point(5, 75);
            lblSelectedTileset.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            lblSelectedTileset.Text = "Tileset -1";

            btnLoadTileset = new Button("btnLoadTileset");
            btnLoadTileset.Size = new System.Drawing.Size(30, 20);
            btnLoadTileset.Location = new Point(265, 75);
            btnLoadTileset.Text = "Load";
            btnLoadTileset.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLoadTileset_Click);
            Skins.SkinManager.LoadButtonGui(btnLoadTileset);

            picTilesetPreview = new PictureBox[8];

            for (int i = 0; i < picTilesetPreview.Length; i++) {
                picTilesetPreview[i] = new PictureBox("picTilesetPreview" + i);
                picTilesetPreview[i].Location = new Point(5 + (i * 37), 10);
                picTilesetPreview[i].Size = new System.Drawing.Size(32, 32);
                picTilesetPreview[i].BackColor = Color.White;
                picTilesetPreview[i].BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            }

            pnlTileset.AddWidget(hTilesetSelect);
            pnlTileset.AddWidget(lblSelectedTileset);
            pnlTileset.AddWidget(btnLoadTileset);

            for (int i = 0; i < picTilesetPreview.Length; i++) {
                pnlTileset.AddWidget(picTilesetPreview[i]);
            }

            #endregion

            #region Settings Widgets

            btnSettings = new Button("btnSettings");
            btnSettings.Size = new System.Drawing.Size(100, 30);
            btnSettings.Location = new Point(300, 0);
            btnSettings.Font = Graphics.FontManager.LoadFont("PMU.ttf", 24);
            btnSettings.Text = "Settings";
            btnSettings.MouseHoverDelay = 100;
            btnSettings.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnSettings_Click);
            btnSettings.MouseHover += new EventHandler(btnSettings_MouseHover);
            Skins.SkinManager.LoadButtonGui(btnSettings);

            pnlSettings = new Panel("pnlOptions");
            pnlSettings.Size = new Size(200, 200);
            pnlSettings.Location = new Point(300, btnSettings.Y + mapEditor_Menu.Y - pnlSettings.Height);
            pnlSettings.BackColor = Color.White;
            pnlSettings.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            pnlSettings.SetAutoHide();

            lblDisplaySettings = new Label("lblDisplaySettings");
            lblDisplaySettings.AutoSize = true;
            lblDisplaySettings.Location = new Point(5, 5);
            lblDisplaySettings.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 12);
            lblDisplaySettings.Text = "Display Settings";

            chkDisplayMapGrid = new CheckBox("chkDisplayMapGrid");
            chkDisplayMapGrid.Location = new Point(5, 25);
            chkDisplayMapGrid.Size = new System.Drawing.Size(125, 17);
            chkDisplayMapGrid.BackColor = Color.Transparent;
            chkDisplayMapGrid.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            chkDisplayMapGrid.Text = "Map Grid";
            chkDisplayMapGrid.Checked = IO.Options.MapGrid;
            chkDisplayMapGrid.CheckChanged += new EventHandler(chkDisplayMapGrid_CheckChanged);

            chkDisplayAttributes = new CheckBox("chkDisplayAttributes");
            chkDisplayAttributes.Location = new Point(5, 45);
            chkDisplayAttributes.Size = new System.Drawing.Size(125, 17);
            chkDisplayAttributes.BackColor = Color.Transparent;
            chkDisplayAttributes.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            chkDisplayAttributes.Text = "Attributes";
            chkDisplayAttributes.Checked = IO.Options.DisplayAttributes;
            chkDisplayAttributes.CheckChanged += new EventHandler(chkDisplayAttributes_CheckChanged);

            chkDisplayDungeonValues = new CheckBox("chkDisplayDungeonValues");
            chkDisplayDungeonValues.Location = new Point(5, 65);
            chkDisplayDungeonValues.Size = new System.Drawing.Size(125, 17);
            chkDisplayDungeonValues.BackColor = Color.Transparent;
            chkDisplayDungeonValues.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            chkDisplayDungeonValues.Text = "DungeonValues";
            chkDisplayDungeonValues.Checked = IO.Options.DisplayDungeonValues;
            chkDisplayDungeonValues.CheckChanged += new EventHandler(chkDisplayDungeonValues_CheckChanged);

            lblMappingSettings = new Label("lblMappingSettings");
            lblMappingSettings.AutoSize = true;
            lblMappingSettings.Location = new Point(5, 85);
            lblMappingSettings.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 12);
            lblMappingSettings.Text = "Mapping Settings";

            chkDragAndPlace = new CheckBox("chkDragAndPlace");
            chkDragAndPlace.Location = new Point(5, 105);
            chkDragAndPlace.Size = new System.Drawing.Size(125, 17);
            chkDragAndPlace.BackColor = Color.Transparent;
            chkDragAndPlace.Font = Graphics.FontManager.LoadFont("tahoma.ttf", 10);
            chkDragAndPlace.Text = "Drag and Place";
            chkDragAndPlace.Checked = IO.Options.DragAndPlace;
            chkDragAndPlace.CheckChanged += new EventHandler(chkDragAndPlace_CheckChanged);

            pnlSettings.AddWidget(lblDisplaySettings);
            pnlSettings.AddWidget(chkDisplayMapGrid);
            pnlSettings.AddWidget(chkDisplayAttributes);
            pnlSettings.AddWidget(chkDisplayDungeonValues);
            pnlSettings.AddWidget(lblMappingSettings);
            pnlSettings.AddWidget(chkDragAndPlace);

            #endregion

            Screen.AddWidget(tilesetViewer);
            Screen.AddWidget(btnTerrain);
            Screen.AddWidget(btnAttributes);
            Screen.AddWidget(btnOK);
            //this.AddWidget(btnBack);

            Screen.AddWidget(pnlAttributes);
            Screen.AddWidget(pnlAttOptions);

            mapEditor_Menu.AddWidget(btnMapping);
            Screen.AddWidget(pnlMapping);

            mapEditor_Menu.AddWidget(btnLayers);
            Screen.AddWidget(pnlLayers);

            mapEditor_Menu.AddWidget(btnTileset);
            Screen.AddWidget(pnlTileset);

            mapEditor_Menu.AddWidget(btnSettings);
            Screen.AddWidget(pnlSettings);

            mapEditor_Menu.Visible = false;
            Screen.AddWidget(mapEditor_Menu);

            hTilesetSelect_ValueChanged(hTilesetSelect, new ValueChangedEventArgs(0, 0));
        }

        void EnforceMapEditorLimits() {
            switch (limiter) {
                case Enums.MapEditorLimitTypes.Full: {
                        optBlocked.Show();
                        optWarp.Show();
                        optItem.Show();
                        optNpcAvoid.Show();
                        optKey.Show();
                        optKeyOpen.Show();
                        optHeal.Show();
                        optKill.Show();
                        optSound.Show();
                        optScripted.Show();
                        optNotice.Show();
                        optDoor.Show();
                        optSign.Show();
                        optSpriteChange.Show();
                        optShop.Show();
                        optArena.Show();
                        optBank.Show();
                        optGuildBlock.Show();
                        optSpriteBlock.Show();
                        optMobileBlock.Show();
                        optLevelBlock.Show();
                        optAssembly.Show();
                        optEvolution.Show();
                        optStory.Show();
                        optLinkShop.Show();
                        optMission.Show();
                        optScriptedSign.Show();
                        optAmbiguous.Show();
                        optSlippery.Show();
                        optSlow.Show();
                        optDropShop.Show();
                        btnMapProperties.Show();
                        btnHouseProperties.Hide();
                        lblDungeonTileValue.Show();
                        nudDungeonTileValue.Show();
                        Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayLocation = true;
                    }
                    break;
                case Enums.MapEditorLimitTypes.House: {
                        HideAllAttributes();
                        optBlocked.Show();
                        btnMapProperties.Hide();
                        btnHouseProperties.Show();
                    }
                    break;
            }
        }

        void HideAllAttributes() {
            optBlocked.Hide();
            optWarp.Hide();
            optItem.Hide();
            optNpcAvoid.Hide();
            optKey.Hide();
            optKeyOpen.Hide();
            optHeal.Hide();
            optKill.Hide();
            optSound.Hide();
            optScripted.Hide();
            optNotice.Hide();
            optDoor.Hide();
            optSign.Hide();
            optSpriteChange.Hide();
            optShop.Hide();
            optArena.Hide();
            optBank.Hide();
            optGuildBlock.Hide();
            optSpriteBlock.Hide();
            optMobileBlock.Hide();
            optLevelBlock.Hide();
            optAssembly.Hide();
            optEvolution.Hide();
            optStory.Hide();
            optLinkShop.Hide();
            optMission.Hide();
            optScriptedSign.Hide();
            optSlippery.Hide();
            optSlow.Hide();
            optDropShop.Hide();
            optAmbiguous.Hide();
            lblDungeonTileValue.Hide();
            nudDungeonTileValue.Hide();
        }

        void btnEyeDropper_Click(object sender, MouseButtonEventArgs e) {
            if (limiter == Enums.MapEditorLimitTypes.Full) {
                if (ActiveTool == MappingTool.EyeDropper) {
                    SetActiveTool(MappingTool.Editor);
                    btnEyeDropper.Selected = false;
                } else {
                    SetActiveTool(MappingTool.EyeDropper);
                    btnEyeDropper.Selected = true;
                }
            }
        }

        public void SetActiveTool(MappingTool tool) {
            switch (tool) {
                case MappingTool.Editor: {
                        DisableActiveTool();
                    }
                    break;
                case MappingTool.EyeDropper: {
                        btnEyeDropper.Selected = true;
                    }
                    break;
            }
            ActiveTool = tool;
        }

        public void DisableActiveTool() {
            switch (ActiveTool) {
                case MappingTool.EyeDropper: {
                        if (btnEyeDropper.Selected) {
                            btnEyeDropper.Selected = false;
                        }
                    }
                    break;
            }
            ActiveTool = MappingTool.Editor;
        }

        void btnExit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Editors.EditorManager.CloseMapEditor();
            HideAutoHiddenPanels();
            btnAttributes.Selected = false;
            Messenger.SendRefresh();
        }

        void btnMapProperties_Click(object sender, MouseButtonEventArgs e) {
            if (SdlDotNet.Widgets.WindowManager.Windows.Contains("winProperties") == false) {
                SdlDotNet.Widgets.WindowManager.AddWindow(new Editors.MapEditor.winProperties());
            } else {
                SdlDotNet.Widgets.WindowManager.BringWindowToFront(SdlDotNet.Widgets.WindowManager.FindWindow("winProperties"));
            }
            // TODO: Map Properties [Map Editor] (and uncomment all of it)
        }

        void btnHouseProperties_Click(object sender, MouseButtonEventArgs e) {
            if (SdlDotNet.Widgets.WindowManager.Windows.Contains("winHouseProperties") == false) {
                SdlDotNet.Widgets.WindowManager.AddWindow(new Editors.MapEditor.winHouseProperties());
            } else {
                SdlDotNet.Widgets.WindowManager.BringWindowToFront(SdlDotNet.Widgets.WindowManager.FindWindow("winHouseProperties"));
            }
        }

        void btnTakeScreenshot_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (SdlDotNet.Widgets.WindowManager.Windows.Contains("winScreenshotOptions") == false) {
                SdlDotNet.Widgets.WindowManager.AddWindow(new Editors.MapEditor.winScreenshotOptions());
            } else {
                SdlDotNet.Widgets.WindowManager.BringWindowToFront(SdlDotNet.Widgets.WindowManager.FindWindow("winScreenshotOptions"));
            }
        }

        void btnSaveMap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Messenger.SendSaveMap(Maps.MapHelper.ActiveMap);
            btnAttributes.Selected = false;
            optBlocked.Checked = true;
        }

        void btnLoadMap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            // TODO: Load Map [Map Editor]
            //System.Windows.Forms.OpenFileDialog ofd = new System.Windows.Forms.OpenFileDialog();
            //ofd.Filter = "PMU Map|*.pmumap";
            //ofd.Multiselect = false;
            //if (ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK) {
            //    Maps.Map map = new Maps.Map();
            //    map.Init();
            //    map.LoadMap(ofd.FileName);
            //    mapViewer.ActiveMap = map;
            //}
        }

        void btnAttributes_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnAttributes.Selected) {
                btnAttributes.Selected = true;
                btnTerrain.Selected = false;
                tilesetViewer.Visible = false;
                pnlAttributes.Visible = true;
            }
        }

        void btnTerrain_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (!btnTerrain.Selected) {
                btnAttributes.Selected = false;
                btnTerrain.Selected = true;
                tilesetViewer.Visible = true;
                pnlAttributes.Visible = false;
            }
        }

        void btnClear_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (btnTerrain.Selected) {
                FillLayer(GetActiveLayer(), 0, 0);
                if (inLiveMode) {
                    Messenger.SendLayerFillData(GetActiveLayer(), 0, 0);
                }
            } else if (btnAttributes.Selected) {
                FillAttributes(Enums.TileType.Walkable, 0, 0, 0, "", "", "", 0);
                if (inLiveMode) {
                    Messenger.SendAttributeFillData(Enums.TileType.Walkable, 0, 0, 0, "", "", "", 0);
                }
            }
        }

        void btnFill_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (btnTerrain.Selected) {
                FillLayer(GetActiveLayer(), tilesetViewer.ActiveTilesetSurface.TilesetNumber, tilesetViewer.SelectedTile.Y * (tilesetViewer.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + tilesetViewer.SelectedTile.X);
                if (inLiveMode) {
                    Messenger.SendLayerFillData(GetActiveLayer(), tilesetViewer.ActiveTilesetSurface.TilesetNumber, tilesetViewer.SelectedTile.Y * (tilesetViewer.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + tilesetViewer.SelectedTile.X);
                }
            } else if (btnAttributes.Selected) {
                FillAttributesFromSettings(true);
            }
        }

        void chkDisplayMapGrid_CheckChanged(object sender, EventArgs e) {
            IO.Options.MapGrid = chkDisplayMapGrid.Checked;
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayMapGrid = chkDisplayMapGrid.Checked;
        }

        void chkDisplayAttributes_CheckChanged(object sender, EventArgs e) {
            IO.Options.DisplayAttributes = chkDisplayAttributes.Checked;
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayAttributes = chkDisplayAttributes.Checked;
        }

        void chkDisplayDungeonValues_CheckChanged(object sender, EventArgs e) {
            IO.Options.DisplayDungeonValues = chkDisplayDungeonValues.Checked;
            Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayDungeonValues = chkDisplayDungeonValues.Checked;
        }

        void chkDragAndPlace_CheckChanged(object sender, EventArgs e) {
            IO.Options.DragAndPlace = chkDragAndPlace.Checked;
        }

        void btnLoadTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            HideAutoHiddenPanels();
            tilesetViewer.ActiveTilesetSurface = Graphics.GraphicsManager.Tiles[hTilesetSelect.Value];
        }

        void hTilesetSelect_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (lblSelectedTileset.Text != "Tileset " + e.NewValue.ToString()) {
                lblSelectedTileset.Text = "Tileset " + e.NewValue.ToString();
                for (int i = 0; i < picTilesetPreview.Length; i++) {
                    if (picTilesetPreview[i].Image != null) {
                        picTilesetPreview[i].Image.Dispose();
                        picTilesetPreview[i].Image = null;
                    }
                    picTilesetPreview[i].Image = new Surface(Graphics.GraphicsManager.Tiles[e.NewValue][(i + 1)]);
                }
            }
        }

        void btnTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            ShowTilesetPanel();
        }

        void btnLayers_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            ShowLayersPanel();
        }

        void btnMapping_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            ShowMappingPanel();
        }

        void btnMapping_MouseHover(object sender, EventArgs e) {
            ShowMappingPanel();
        }

        void btnLayers_MouseHover(object sender, EventArgs e) {
            ShowLayersPanel();
        }

        void btnTileset_MouseHover(object sender, EventArgs e) {
            ShowTilesetPanel();
        }

        void btnSettings_MouseHover(object sender, EventArgs e) {
            ShowOptionsPanel();
        }

        void btnSettings_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            ShowOptionsPanel();
        }

        private void ShowMappingPanel() {
            if (pnlMapping.Visible == false) {
                HideAutoHiddenPanels();
                pnlMapping.ShowHidden();
            }
        }

        private void ShowLayersPanel() {
            if (pnlLayers.Visible == false) {
                HideAutoHiddenPanels();
                pnlLayers.ShowHidden();
            }
        }

        private void ShowTilesetPanel() {
            if (pnlTileset.Visible == false) {
                HideAutoHiddenPanels();
                pnlTileset.ShowHidden();
            }
        }

        private void ShowOptionsPanel() {
            if (pnlSettings.Visible == false) {
                HideAutoHiddenPanels();
                pnlSettings.ShowHidden();
            }
        }

        private void HideAutoHiddenPanels() {
            pnlMapping.HideHidden();
            pnlLayers.HideHidden();
            pnlTileset.HideHidden();
            pnlSettings.HideHidden();
        }

        void optWarp_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("warp");
            }
        }

        void optItem_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("item");
            }
        }

        void optKey_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("key");
            }
        }

        void optKeyOpen_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("keyopen");
            }
        }

        void optSound_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("sound");
            }
        }

        void optScripted_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("scripted");
            }
        }

        void optNotice_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("notice");
            }
        }

        void optSign_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("sign");
            }
        }

        void optSpriteChange_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("sprite");
            }
        }

        void optShop_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("shop");
            }
        }

        void optArena_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("arena");
            }
        }

        void optBank_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("bank");
            }
        }

        void optGuildBlock_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("guildblock");
            }
        }

        void optSpriteBlock_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("spriteblock");
            }
        }

        void optMobileBlock_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("mobileblock");
            }
        }

        void optLevelBlock_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("levelblock");
            }
        }

        void optStory_CheckChanged(object sender, EventArgs e) {//TODO
            if (((RadioButton)sender).Checked) {
                OptionPanel("story");
            }
        }

        void optLinkShop_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("linkshop");
            }
        }

        void optScriptedSign_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("scriptedsign");
            }
        }

        void optSlippery_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("slippery");
            }
        }

        void optSlow_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("slow");
            }
        }

        void optDropShop_CheckChanged(object sender, EventArgs e) {
            if (((RadioButton)sender).Checked) {
                OptionPanel("dropshop");
            }
        }

        #region Attribute Options

        #region OptionPanel

        void OptionPanel(string option) {
            switch (option.ToLower()) {
                case "warp": {
                        lbl1.Text = "Maps:";
                        nudStoryLevel.Location = new Point(9, 60);
                        if (hsb1.Value > 50) {
                            hsb1.Value = 50;
                        }
                        if (hsb2.Value > 50) {
                            hsb2.Value = 50;
                        }
                        lbl2.Text = "X: " + hsb1.Value;
                        lbl2.Location = new Point(0, 78);
                        lbl3.Text = "Y: " + hsb2.Value;
                        lbl3.Location = new Point(0, 119);
                        hsb1.Location = new Point(9, 99);
                        nudStoryLevel.Minimum = 1;
                        nudStoryLevel.Maximum = 2000;
                        hsb1.Maximum = 50;
                        hsb2.Maximum = 50;
                        hsb2.Location = new Point(9, 141);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        nudStoryLevel.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Warp Map Attribute";
                    }
                    break;
                case "item": {
                        if (hsb1.Value == 0) {
                            lbl1.Text = "Item 0: None";
                        } else if (hsb1.Value > MaxInfo.MaxItems) {
                            hsb1.Value = MaxInfo.MaxItems;
                        } else {
                            lbl1.Text = "Item " + hsb1.Value + ": " + Items.ItemHelper.Items[hsb1.Value].Name;
                        }
                        lbl2.Text = "Value: " + hsb2.Value;
                        lbl2.Location = new Point(0, 78);
                        lbl2.Visible = true;
                        hsb1.Maximum = MaxInfo.MaxItems;
                        hsb2.Maximum = 32759;
                        hsb1.Location = new Point(9, 55);
                        hsb2.Location = new Point(9, 105);
                        chkTake.Location = new Point(9, 130);
                        chkTake.Text = "Sticky";
                        chkTake.Visible = true;
                        chkHidden.Location = new Point(9, 155);
                        chkHidden.Text = "Hidden";
                        chkHidden.Visible = true;
                        txt1.Location = new Point(9, 175);
                        txt1.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Item Attribute";
                    }
                    break;
                case "key": {
                        if (hsb1.Value == 0) {
                            lbl1.Text = "Item 0: None";
                        } else if (hsb1.Value > MaxInfo.MaxItems) {
                            hsb1.Value = MaxInfo.MaxItems;
                        } else {
                            lbl1.Text = "Item " + hsb1.Value + ": " + Items.ItemHelper.Items[hsb1.Value].Name;
                        }
                        hsb1.Maximum = MaxInfo.MaxItems;
                        hsb1.Location = new Point(9, 55);
                        chkTake.Location = new Point(9, 80);
                        chkTake.Text = "Take away key upon use";
                        hsb1.Visible = true;
                        chkTake.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Key Attribute";
                    }
                    break;
                case "keyopen": {
                        if (hsb1.Value > 50) {
                            hsb1.Value = 50;
                        }
                        if (hsb2.Value > 50) {
                            hsb2.Value = 50;
                        }
                        lbl1.Text = "X: " + hsb1.Value;
                        lbl2.Text = "Y: " + hsb2.Value;
                        lbl3.Text = "Key Message (Leave blank for default)";
                        hsb1.Maximum = 50;
                        hsb2.Maximum = 50;
                        hsb1.Location = new Point(9, 55);
                        hsb2.Location = new Point(9, 105);
                        txt1.Location = new Point(9, 143);
                        lbl2.Location = new Point(0, 78);
                        lbl3.Location = new Point(0, 119);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        txt1.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Key Open Attribute";
                    }
                    break;
                case "sound": {
                        lstSound.Visible = true;
                        if (lstSound.SelectedItems.Count == 0) {
                            lstSound.SelectItem(0);
                        }
                        OpenOptionsPanel();
                        btnTitle.Text = "Sound Attribute";
                    }
                    break;
                case "scripted": {
                        if (hsb1.Value > 100) {
                            hsb1.Value = 100;
                        }
                        lbl1.Text = "Script " + hsb1.Value + ": (Needs work)"; 
                        Messenger.SendScriptedTileInfoRequest(hsb1.Value);
                        lbl2.Text = "Param 1:";
                        lbl3.Text = "Param 2:";
                        lbl4.Text = "Param 3:";
                        hsb1.Maximum = 100;
                        txt1.Location = new Point(9, 105);
                        txt2.Location = new Point(9, 143);
                        txt3.Location = new Point(9, 181);
                        lbl2.Location = new Point(0, 78);
                        lbl3.Location = new Point(0, 119);
                        lbl4.Location = new Point(0, 160);
                        hsb1.Location = new Point(9, 55);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        lbl4.Visible = true;
                        hsb1.Visible = true;
                        txt1.Visible = true;
                        txt2.Visible = true;
                        txt3.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Scripted Tile Attribute";
                    }
                    break;
                case "notice": {
                        lbl1.Text = "Sound:";
                        txt1.Location = new Point(9, 220);
                        lbl2.Text = "Title:";
                        lbl2.Location = new Point(0, 200);
                        txt2.Location = new Point(9, 265);
                        lbl3.Text = "Text:";
                        lbl3.Location = new Point(0, 245);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        txt1.Visible = true;
                        txt2.Visible = true;
                        lstSound.Visible = true;
                        if (lstSound.SelectedItems.Count == 0) {
                            lstSound.SelectItem(0);
                        }
                        OpenOptionsPanel();
                        btnTitle.Text = "Notice Attribute";
                    }
                    break;
                case "linkshop": {
                        if (hsb1.Value == 0) {
                            lbl1.Text = "Item Paid: 0 (None)";
                        } else if (hsb1.Value > MaxInfo.MaxItems) {
                            hsb1.Value = MaxInfo.MaxItems;
                        } else {
                            lbl1.Text = "Item Paid: " + hsb1.Value + " (" + Items.ItemHelper.Items[hsb1.Value].Name + ")";
                        }
                        lbl2.Text = "Value: " + hsb2.Value;
                        lbl2.Location = new Point(0, 78);
                        lbl2.Visible = true;
                        hsb1.Maximum = MaxInfo.MaxItems;
                        hsb2.Maximum = 32759;
                        hsb1.Location = new Point(9, 55);
                        hsb2.Location = new Point(9, 105);
                        chkTake.Location = new Point(9, 130);
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        chkTake.Text = "Pre-Evo Moves";
                        hsb1.Visible = true;
                        chkTake.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Link Shop Attribute";
                    }
                    break;
                case "sign": {
                        lbl1.Text = "Line 1:";
                        lbl2.Text = "Line 2:";
                        lbl3.Text = "Line 3:";
                        lbl2.Location = new Point(0, 85);
                        lbl3.Location = new Point(0, 135);
                        txt1.Location = new Point(9, 55);
                        txt2.Location = new Point(9, 105);
                        txt3.Location = new Point(9, 155);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        txt1.Visible = true;
                        txt2.Visible = true;
                        txt3.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Sign Attribute";
                    }
                    break;
                case "sprite": {
                        lbl1.Text = "Sprite: " + hsb1.Value;
                        if (hsb1.Value > 1000) {
                            hsb1.Value = 1000;
                        }
                        if (hsb2.Value > MaxInfo.MaxItems || hsb2.Value == 0) {
                            lbl2.Text = "Item 0: None";
                        } else {
                            lbl2.Text = "Item " + hsb2.Value + ": " + Items.ItemHelper.Items[hsb2.Value].Name;
                        }
                        hsb1.Maximum = 1000;
                        hsb1.Location = new Point(0, 55);
                        hsb2.Maximum = MaxInfo.MaxItems;
                        hsb2.Minimum = 0;
                        hsb2.Location = new Point(0, 105);
                        hsb3.Maximum = 30000;
                        hsb3.Location = new Point(0, 155);
                        lbl2.Location = new Point(0, 85);
                        lbl3.Text = "Value: " + hsb3.Value;
                        lbl3.Location = new Point(0, 135);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        hsb3.Visible = true;
                        picSprite.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Sprite Change Attribute";
                    }
                    break;
                case "shop": {
                        if (hsb1.Value > MaxInfo.MaxShops || hsb1.Value == 0) {
                            lbl1.Text = "Shop num 0- None";
                        } else {
                            lbl1.Text = "Shop num " + hsb1.Value + "- " + Shops.ShopHelper.Shops[hsb1.Value].Name;
                        }
                        hsb1.Maximum = MaxInfo.MaxShops;
                        hsb1.Location = new Point(9, 55);
                        hsb1.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Shop Attribute";
                    }
                    break;
                case "arena": {
                        lbl1.Text = "Maps: " + hsb3.Value;
                        hsb1.Location = new Point(9, 99);
                        if (hsb1.Value > 50) {
                            hsb1.Value = 50;
                        }
                        if (hsb2.Value > 50) {
                            hsb2.Value = 50;
                        }
                        if (hsb3.Value > 2000) {
                            hsb3.Value = 2000;
                        }
                        lbl2.Text = "X: " + hsb1.Value;
                        lbl2.Location = new Point(0, 78);
                        lbl3.Text = "Y: " + hsb2.Value;
                        lbl3.Location = new Point(0, 119);
                        hsb2.Location = new Point(9, 141);
                        nudStoryLevel.Minimum = 1;
                        nudStoryLevel.Maximum = 2000;
                        hsb1.Maximum = 50;
                        hsb2.Maximum = 50;
                        hsb3.Maximum = 2000;
                        hsb3.Location = new Point(9, 60);
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        hsb3.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Arena Attribute";
                    }
                    break;
                case "guildblock": {
                        lbl1.Text = "Guild Name to Pass Block:";
                        txt1.Location = new Point(9, 55);
                        txt1.Visible = true;
                        txt1.Size = new Size(230, 18);
                        OpenOptionsPanel();
                        btnTitle.Text = "Guild Block Attribute";
                    }
                    break;
                case "spriteblock": {
                        lbl1.Text = "Allow Sprite: " + hsb1.Value;
                        lbl2.Text = "Allow Sprite: " + hsb2.Value;
                        hsb1.Maximum = 1000;
                        hsb1.Location = new Point(0, 55);
                        hsb2.Maximum = 1000;
                        hsb2.Minimum = 0;
                        hsb2.Location = new Point(0, 105);
                        lbl2.Location = new Point(0, 85);
                        lbl3.Text = "Sprite 1:";
                        lbl3.Location = new Point(138, 7);
                        lbl4.Text = "Sprite 2:";
                        lbl4.Location = new Point(0, 130);
                        lblMode.Visible = true;
                        optAllow.Checked = true;
                        optAllow.Visible = true;
                        optBlock.Visible = true;
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        lbl4.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        picSprite.Visible = true;
                        picSprite2.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Sprite Block Attribute";
                    }
                    break;
                case "mobileblock": {
                        if (hsb1.Value > 31) {
                            hsb1.Value = 31;
                        }
                        lbl1.Text = "Mobility Flag #" + ": Loading..."; //Needs work
                        Messenger.SendMobilityInfoRequest(hsb1.Value);
                        hsb1.Maximum = 15;
                        hsb1.Location = new Point(9, 55);
                        hsb1.Visible = true;
                        chkHidden.Location = new Point(9, 85);
                        chkHidden.Text = "Blocked";
                        chkHidden.Visible = true;

                        OpenOptionsPanel();
                        btnTitle.Text = "Mobile Block Attribute";
                    }
                    break;
                case "levelblock": {
                        if (nudStoryLevel.Value > 100) {
                            nudStoryLevel.Value = 100;
                        }
                        lbl1.Text = "Select a level. All levels equal to or";
                        lbl2.Text = "under this level will be blocked.";
                        lbl2.Location = new Point(0, 53);
                        nudStoryLevel.Location = new Point(9, 71);
                        nudStoryLevel.Minimum = 1;
                        nudStoryLevel.Maximum = 100;
                        lbl2.Visible = true;
                        nudStoryLevel.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Level Block Attribute";
                    }
                    break;
                case "story": {
                        lbl1.Text = "Chapter";
                        nudStoryLevel.Location = new Point(9, 55);
                        nudStoryLevel.Minimum = 1;
                        nudStoryLevel.Maximum = MaxInfo.MaxStories;
                        nudStoryLevel.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Story Attribute";
                    }
                    break;
                case "scriptedsign": {
                        if (hsb1.Value > 100) {
                            hsb1.Value = 100;
                        }
                        lbl1.Text = "Script " + hsb1.Value + ": (Needs work)"; //Needs work
                        Messenger.SendScriptedSignInfoRequest(hsb1.Value);
                        hsb1.Maximum = 100;
                        txt1.Location = new Point(9, 105);
                        txt2.Location = new Point(9, 143);
                        txt3.Location = new Point(9, 181);
                        lbl2.Location = new Point(0, 78);
                        lbl3.Location = new Point(0, 119);
                        lbl4.Location = new Point(0, 160);
                        hsb1.Location = new Point(9, 55);
                        lbl2.Text = "Param 1:";
                        lbl3.Text = "Param 2:";
                        lbl4.Text = "Param 3:";
                        lbl2.Visible = true;
                        lbl3.Visible = true;
                        lbl4.Visible = true;
                        hsb1.Visible = true;
                        txt1.Visible = true;
                        txt2.Visible = true;
                        txt3.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Scripted Sign Attribute";
                    }
                    break;
                case "slippery": {
                        if (hsb1.Value > 31) {
                            hsb1.Value = 31;
                        }
                        lbl1.Text = "Mobility Flag #" + ": Loading..."; //Needs work
                        Messenger.SendMobilityInfoRequest(hsb1.Value);
                        hsb1.Maximum = 15;
                        hsb1.Location = new Point(9, 55);
                        hsb1.Visible = true;
                        chkHidden.Location = new Point(9, 85);
                        chkHidden.Text = "Slip";
                        chkHidden.Visible = true;

                        OpenOptionsPanel();
                        btnTitle.Text = "Slippery Attribute";
                    }
                    break;
                case "slow": {
                        if (hsb1.Value > 15) {
                            hsb1.Value = 15;
                        }
                        lbl1.Text = "Mobility Flag #" + ": Loading..."; //Needs work
                        Messenger.SendMobilityInfoRequest(hsb1.Value);
                        hsb1.Maximum = 15;
                        hsb1.Location = new Point(9, 55);
                        hsb1.Visible = true;
                        chkHidden.Location = new Point(9, 85);
                        chkHidden.Text = "Slow";
                        chkHidden.Visible = true;

                        lbl2.Text = "Speed:";
                        lbl2.Location = new Point(0, 115);
                        lbl2.Visible = true;

                        if (hsb2.Value > 7) {
                            hsb2.Value = 7;
                        }
                        hsb2.Maximum = 8;
                        hsb2.Location = new Point(9, 145);
                        hsb2.Visible = true;

                        OpenOptionsPanel();
                        btnTitle.Text = "Slow Attribute";
                    }
                    break;
                case "dropshop": {
                        if (hsb1.Value == 0) {
                            lbl1.Text = "Item 0: None";
                        } else if (hsb1.Value > MaxInfo.MaxItems) {
                            hsb1.Value = MaxInfo.MaxItems;
                        } else {
                            lbl1.Text = "Item " + hsb1.Value + ": " + Items.ItemHelper.Items[hsb1.Value].Name;
                        }
                        lbl2.Text = "Value: " + hsb2.Value;
                        lbl2.Location = new Point(0, 78);
                        lbl2.Visible = true;
                        lbl3.Text = "Paid Value: " + hsb2.Value;
                        lbl3.Location = new Point(0, 210);
                        lbl3.Visible = true;
                        hsb1.Maximum = MaxInfo.MaxItems;
                        hsb2.Maximum = 32759;
                        hsb3.Maximum = 32759;
                        hsb1.Location = new Point(9, 55);
                        hsb2.Location = new Point(9, 105);
                        txt1.Location = new Point(9, 175);
                        hsb3.Location = new Point(9, 245);
                        txt1.Visible = true;
                        hsb1.Visible = true;
                        hsb2.Visible = true;
                        hsb3.Visible = true;
                        OpenOptionsPanel();
                        btnTitle.Text = "Drop Shop Attribute";
                    }
                    break;
            }
        }

        #endregion OptionPanel

        void hsb1_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (btnTitle.Text == "Sprite Change Attribute" || btnTitle.Text == "Sprite Block Attribute") {
                lbl1.Text = "Sprite: " + hsb1.Value;
                picSprite.Size = new Size(32, 64);
                picSprite.BlitToBuffer(Graphics.GraphicsManager.GetSpriteSheet(hsb1.Value).GetSheet(Graphics.FrameType.Idle, Enums.Direction.Down), new Rectangle(96, 0, 32, 64));
            } else if (btnTitle.Text == "Warp Map Attribute" || btnTitle.Text == "Arena Attribute") {
                lbl2.Text = "X: " + hsb1.Value;
            } else if (btnTitle.Text == "Link Shop Attribute") {
                if (hsb1.Value == 0) {
                    lbl1.Text = "Item Paid: 0 (None)";
                } else {
                    lbl1.Text = "Item Paid: " + hsb1.Value + " (" + Items.ItemHelper.Items[hsb1.Value].Name + ")";
                }
            } else if (btnTitle.Text == "Item Attribute" || btnTitle.Text == "Key Attribute" || btnTitle.Text == "Drop Shop Attribute") {
                if (hsb1.Value == 0) {
                    lbl1.Text = "Item 0: None";
                } else {
                    lbl1.Text = "Item " + hsb1.Value + ": " + Items.ItemHelper.Items[hsb1.Value].Name;
                }
            } else if (btnTitle.Text == "Key Open Attribute") {
                lbl1.Text = "X: " + hsb1.Value;
            } else if (btnTitle.Text == "Scripted Tile Attribute") {
                lbl1.Text = "Script " + hsb1.Value + ": Loading..."; //something goes here.
                Messenger.SendScriptedTileInfoRequest(hsb1.Value);
            } else if (btnTitle.Text == "Scripted Sign Attribute") {
                lbl1.Text = "Script " + hsb1.Value + ": Loading..."; //something goes here.
                Messenger.SendScriptedSignInfoRequest(hsb1.Value);
            } else if (btnTitle.Text == "Mobile Block Attribute" || btnTitle.Text == "Slippery Attribute" || btnTitle.Text == "Slow Attribute") {
                lbl1.Text = "Mobility Flag #" + hsb1.Value + ": Loading..."; //something goes here.
                Messenger.SendMobilityInfoRequest(hsb1.Value);
                chkHidden.Checked = tempArrayForMobility[hsb1.Value];
            } else if (btnTitle.Text == "Shop Attribute") {
                if (hsb1.Value == 0) {
                    lbl1.Text = "Shop num 0: None";
                } else {
                    lbl1.Text = "Shop num " + hsb1.Value + "- " + Shops.ShopHelper.Shops[hsb1.Value].Name;
                }
            }
        }

        void hsb2_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (btnTitle.Text == "Sprite Change Attribute") {
                if (hsb2.Value == 0) {
                    lbl2.Text = "Item 0: None";
                } else {
                    lbl2.Text = "Item " + hsb2.Value + ": " + Items.ItemHelper.Items[hsb2.Value].Name;
                }
            } else if (btnTitle.Text == "Warp Map Attribute" || btnTitle.Text == "Arena Attribute") {
                lbl3.Text = "Y: " + hsb2.Value;
            } else if (btnTitle.Text == "Item Attribute" || btnTitle.Text == "Link Shop Attribute" || btnTitle.Text == "Drop Shop Attribute") {
                lbl2.Text = "Value: " + hsb2.Value;
            } else if (btnTitle.Text == "Key Open Attribute") {
                lbl2.Text = "Y: " + hsb2.Value;
            } else if (btnTitle.Text == "Sprite Block Attribute") {
                lbl2.Text = "Sprite: " + hsb2.Value;
                picSprite2.Size = new Size(32, 64);
                picSprite2.BlitToBuffer(Graphics.GraphicsManager.GetSpriteSheet(hsb2.Value).GetSheet(Graphics.FrameType.Idle, Enums.Direction.Down), new Rectangle(96, 0, 32, 64));
                
            } else if (btnTitle.Text == "Slow Attribute") {
                lbl2.Text = "Speed: " + (Enums.MovementSpeed)hsb2.Value;
            }
        }

        void hsb3_ValueChanged(object sender, ValueChangedEventArgs e) {
            if (btnTitle.Text == "Sprite Change Attribute") {
                lbl3.Text = "Value: " + hsb3.Value;
            } else if (btnTitle.Text == "Arena Attribute") {
                lbl1.Text = "Maps: " + hsb3.Value;
            } else if (btnTitle.Text == "Drop Shop Attribute") {
                lbl3.Text = "Given Value: " + hsb3.Value;
            }
        }

        void optAllow_CheckChanged(object sender, EventArgs e) {
            Mode = 1;
        }

        void optBlock_CheckChanged(object sender, EventArgs e) {
            Mode = 2;
        }

        void chkHidden_CheckChanged(object sender, EventArgs e) {
            if (btnTitle.Text == "Mobile Block Attribute" || btnTitle.Text == "Slippery Attribute" || btnTitle.Text == "Slow Attribute") {
                tempArrayForMobility[hsb1.Value] = chkHidden.Checked;
            }
        }

        void OpenOptionsPanel() {
            pnlAttributes.Visible = false;
            pnlAttOptions.Visible = true;
            btnTerrain.Visible = false;
            btnAttributes.Visible = false;
            btnOK.Visible = true;
            //btnBack.Visible = true;
            btnTitle.Visible = true;
            btnMapping.Visible = false;
            btnLayers.Visible = false;
            btnTileset.Visible = false;
            btnSettings.Visible = false;
            txt1.Text = "1";
            txt2.Text = "1";
            txt3.Text = "1";
            if (optSound.Checked == false) {
                lbl1.Visible = true;
            }
        }

        void CloseOptionsPanel() {
            pnlAttOptions.Visible = false;
            pnlAttributes.Visible = true;
            btnTerrain.Visible = true;
            btnAttributes.Visible = true;
            btnOK.Visible = false;
            //btnBack.Visible = false;
            btnTitle.Text = "";
            btnTitle.Visible = false;
            btnMapping.Visible = true;
            btnLayers.Visible = true;
            btnTileset.Visible = true;
            btnSettings.Visible = true;
            lbl1.Visible = false;
            txt1.Size = new Size(134, 18);
        }

        #region blah
        /*void btnBack_Click(object sender, MouseButtonEventArgs e){
            lbl1.Text = "";
            lbl1.Visible = false;
            lbl2.Text = "";
            lbl2.Visible = false;
            lbl3.Text = "";
            lbl3.Visible = false;
            lbl4.Text = "";
            lbl4.Visible = false;
            lblMode.Text = "";
            lblMode.Visible = false;
            txt1.Text = "";
            txt1.Visible = false;
            txt2.Text = "";
            txt2.Visible = false;
            txt3.Text = "";
            txt3.Visible = false;
            hsb1.Value = 0;
            hsb1.Visible = false;
            hsb2.Value = 0;
            hsb2.Visible = false;
            hsb3.Value = 0;
            hsb3.Visible = false;
            picSprite.Visible = false;
            picSprite2.Visible = false;
            lstSound.Visible = false;
            chkTake.Visible = false;
            optAllow.Visible = false;
            optBlock.Visible = false;
            nudStoryLevel.Value = 0;
            nudStoryLevel.Visible = false;
            CloseOptionsPanel();
            optBlocked.Checked = true;
        }*/
        #endregion

        void btnOK_Click(object sender, MouseButtonEventArgs e) {
            lbl1.Visible = false;
            lbl2.Visible = false;
            lbl3.Visible = false;
            lbl4.Visible = false;
            lblMode.Visible = false;
            txt1.Visible = false;
            txt2.Visible = false;
            txt3.Visible = false;
            hsb1.Visible = false;
            hsb2.Visible = false;
            hsb3.Visible = false;
            picSprite.Visible = false;
            picSprite2.Visible = false;
            lstSound.Visible = false;
            chkTake.Visible = false;
            chkHidden.Visible = false;
            optAllow.Visible = false;
            optBlock.Visible = false;
            nudStoryLevel.Visible = false;
            CloseOptionsPanel();
        }

        #endregion Attribute Options

        Maps.Tile selectedTile;

        void Editor_mapViewer_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if ((Ranks.IsAllowed(Players.PlayerManager.MyPlayer, Enums.Rank.Mapper) || Maps.MapHelper.ActiveMap.MapID.StartsWith("h-")) && (SdlDotNet.Input.Keyboard.IsKeyPressed(SdlDotNet.Input.Key.LeftShift) && e.MouseEventArgs.Button == SdlDotNet.Input.MouseButton.SecondaryButton)) {
                int newX = (e.RelativePosition.X / Constants.TILE_WIDTH) + Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
                int newY = (e.RelativePosition.Y / Constants.TILE_HEIGHT) + Logic.Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
                Messenger.WarpLoc(newX, newY);
                return;
            }
            if (inMapEditor) {
                switch (ActiveTool) {
                    case MappingTool.Editor: {
                            //Point location = mapViewer.ScreenLocation;
                            //Point relPoint = new Point(e.Position.X - location.X, e.Position.Y - location.Y);
                            if (btnTerrain.Selected) {
                                PlaceLayer(e.RelativePosition, e.MouseEventArgs.Button);
                            } else if (btnAttributes.Selected) {
                                PlaceAttribute(e.RelativePosition, e.MouseEventArgs.Button);
                            }
                        }
                        break;
                    case MappingTool.EyeDropper: {
                            Point location = mapViewer.ScreenLocation;
                            Point relPoint = new Point(e.Position.X - location.X, e.Position.Y - location.Y);
                            int X = (relPoint.X / Constants.TILE_WIDTH) + Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
                            int Y = (relPoint.Y / Constants.TILE_HEIGHT) + Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
                            if (X <= mapViewer.ActiveMap.MaxX && Y <= mapViewer.ActiveMap.MaxY && X >= 0 && Y >= 0) {
                                if (btnAttributes.Selected) {
                                    Maps.Tile tile = mapViewer.ActiveMap.Tile[X, Y];
                                    switch (tile.Type) {
                                        case Enums.TileType.Blocked: {
                                                optBlocked.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Warp: {
                                                optWarp.Checked = true;
                                                nudStoryLevel.Value = tile.Data1;
                                                hsb1.Value = tile.Data2;
                                                hsb2.Value = tile.Data3;
                                            }
                                            break;
                                        case Enums.TileType.Item: {
                                                optItem.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                hsb2.Value = tile.Data2;
                                                chkTake.Checked = tile.Data3.ToString().ToBool();
                                                chkHidden.Checked = tile.String1.ToBool();
                                                txt1.Text = tile.String2;
                                            }
                                            break;
                                        case Enums.TileType.NPCAvoid: {
                                                optNpcAvoid.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Key: {
                                                optKey.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                chkTake.Checked = tile.Data2.ToString().ToBool();
                                            }
                                            break;
                                        case Enums.TileType.KeyOpen: {
                                                optKeyOpen.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                hsb2.Value = tile.Data2;
                                                txt1.Text = tile.String1;
                                            }
                                            break;
                                        case Enums.TileType.Heal: {
                                                optHeal.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Kill: {
                                                optKill.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Sound: {
                                                optSound.Checked = true;
                                                lstSound.SelectItem(tile.String1);
                                            }
                                            break;
                                        case Enums.TileType.Scripted: {
                                                optScripted.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                txt1.Text = tile.String1;
                                                txt2.Text = tile.String2;
                                                txt3.Text = tile.String3;
                                            }
                                            break;
                                        case Enums.TileType.Notice: {
                                                optNotice.Checked = true;
                                                txt1.Text = tile.String1;
                                                txt2.Text = tile.String2;
                                                lstSound.SelectItem(tile.String3);
                                            }
                                            break;
                                        case Enums.TileType.LinkShop: {
                                                optLinkShop.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                hsb2.Value = tile.Data2;
                                                chkTake.Checked = tile.Data3.ToString().ToBool();
                                            }
                                            break;
                                        case Enums.TileType.Door: {
                                                optDoor.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Sign: {
                                                optSign.Checked = true;
                                                txt1.Text = tile.String1;
                                                txt2.Text = tile.String2;
                                                txt3.Text = tile.String3;
                                            }
                                            break;
                                        case Enums.TileType.SpriteChange: {
                                                optSpriteChange.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                hsb2.Value = tile.Data2;
                                                hsb3.Value = tile.Data3;
                                            }
                                            break;
                                        case Enums.TileType.Shop: {
                                                optShop.Checked = true;
                                                hsb1.Value = tile.Data1;
                                            }
                                            break;
                                        case Enums.TileType.Arena: {
                                                optArena.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                hsb2.Value = tile.Data2;
                                                hsb3.Value = tile.Data3;
                                            }
                                            break;
                                        case Enums.TileType.Bank: {
                                                optBank.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.LevelBlock: {
                                                optLevelBlock.Checked = true;
                                                nudStoryLevel.Value = tile.Data1;
                                            }
                                            break;
                                        case Enums.TileType.SpriteBlock: {
                                                optSpriteBlock.Checked = true;
                                                Mode = tile.Data1;
                                                hsb1.Value = tile.Data2;
                                                hsb3.Value = tile.Data3;
                                            }
                                            break;
                                        case Enums.TileType.MobileBlock: {
                                                optMobileBlock.Checked = true;
                                                int mobility = tile.Data1;
                                                for (int i = 0; i < 16; i++) {
                                                    tempArrayForMobility[i] = (mobility % 2).ToString().ToBool();
                                                    mobility /= 2;
                                                }
                                                hsb1.Value = 0;
                                                chkHidden.Checked = tempArrayForMobility[0];

                                                //hsb3.Value = tile.Data3;
                                            }
                                            break;
                                        case Enums.TileType.Guild: {
                                                optGuildBlock.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Assembly: {
                                                optAssembly.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Evolution: {
                                                optEvolution.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Story: {
                                                optStory.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.MissionBoard: {
                                                optMission.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.ScriptedSign: {
                                                optScriptedSign.Checked = true;
                                                hsb1.Value = tile.Data1;
                                                txt1.Text = tile.String1;
                                                txt2.Text = tile.String2;
                                                txt3.Text = tile.String3;
                                            }
                                            break;
                                        case Enums.TileType.Ambiguous: {
                                                optAmbiguous.Checked = true;
                                            }
                                            break;
                                        case Enums.TileType.Slippery: {
                                                optSlippery.Checked = true;
                                                int mobility = tile.Data1;
                                                for (int i = 0; i < 16; i++) {
                                                    tempArrayForMobility[i] = (mobility % 2).ToString().ToBool();
                                                    mobility /= 2;
                                                }
                                                hsb1.Value = 0;
                                                chkHidden.Checked = tempArrayForMobility[0];
                                            }
                                            break;
                                        case Enums.TileType.Slow: {
                                                optSlow.Checked = true;
                                                int mobility = tile.Data1;
                                                for (int i = 0; i < 16; i++) {
                                                    tempArrayForMobility[i] = (mobility % 2).ToString().ToBool();
                                                    mobility /= 2;
                                                }
                                                hsb1.Value = 0;
                                                chkHidden.Checked = tempArrayForMobility[0];
                                                hsb2.Value = tile.Data2;
                                            }
                                            break;
                                        case Enums.TileType.DropShop: {
                                                optDropShop.Checked = true;
                                                hsb1.Value = tile.Data2;
                                                hsb2.Value = tile.Data3;
                                                txt1.Text = tile.String2;
                                                hsb3.Value = tile.Data1;
                                            }
                                            break;
                                    }
                                    nudDungeonTileValue.Value = tile.RDungeonMapValue;
                                }
                            }
                            break;
                        }
                }
            }
        }

        void mapViewer_MouseMotion(object sender, SdlDotNet.Input.MouseMotionEventArgs e) {
            if (inMapEditor) {
                if (mapViewer.ActiveMap != null && mapViewer.ActiveMap.Loaded) {
                    Point location = mapViewer.ScreenLocation;
                    Point relPoint = new Point(e.Position.X - location.X, e.Position.Y - location.Y);
                    int X = (relPoint.X / Constants.TILE_WIDTH) + Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
                    int Y = (relPoint.Y / Constants.TILE_HEIGHT) + Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
                    if (X <= mapViewer.ActiveMap.MaxX && Y <= mapViewer.ActiveMap.MaxY && X >= 0 && Y >= 0) {
                        //Graphics.Renderers.Screen.ScreenRenderer.Camera.X = X;
                        //Graphics.Renderers.Screen.ScreenRenderer.Camera.Y = Y;
                        //if (tabControl1.SelectedIndex == 1) {
                        //    ShowAttribInfo(X, Y);
                        //}
                        if (IO.Options.DragAndPlace && (SdlDotNet.Input.Mouse.IsButtonPressed(SdlDotNet.Input.MouseButton.PrimaryButton) || SdlDotNet.Input.Mouse.IsButtonPressed(SdlDotNet.Input.MouseButton.SecondaryButton))) {
                            if (btnTerrain.Selected) {
                                PlaceLayer(relPoint, e.Button);
                            } else if (btnAttributes.Selected) {
                                PlaceAttribute(relPoint, e.Button);
                            }
                        }
                    }
                }
            }
        }

        private Enums.LayerType GetActiveLayer() {
            if (optGround.Checked) {
                if (!chkAnim.Checked){
                    return Enums.LayerType.Ground;
                }else{
                    return Enums.LayerType.GroundAnim;
                }
            } else if (optMask.Checked) {
                if (!chkAnim.Checked) {
                    return Enums.LayerType.Mask;
                } else {
                    return Enums.LayerType.MaskAnim;
                }
            } else if (optMask2.Checked) {
                if (!chkAnim.Checked) {
                    return Enums.LayerType.Mask2;
                } else {
                    return Enums.LayerType.Mask2Anim;
                }
            } else if (optFringe.Checked) {
                if (!chkAnim.Checked) {
                    return Enums.LayerType.Fringe;
                } else {
                    return Enums.LayerType.FringeAnim;
                }
            } else if (optFringe2.Checked) {
                if (!chkAnim.Checked) {
                    return Enums.LayerType.Fringe2;
                } else {
                    return Enums.LayerType.Fringe2Anim;
                }
            } else {
                return Enums.LayerType.None;
            }
        }

        private Enums.TileType GetActiveAttribute() {
            if (optBlocked.Checked)
                return Enums.TileType.Blocked;
            if (optWarp.Checked)
                return Enums.TileType.Warp;
            if (optItem.Checked)
                return Enums.TileType.Item;
            if (optNpcAvoid.Checked)
                return Enums.TileType.NPCAvoid;
            if (optKey.Checked)
                return Enums.TileType.Key;
            if (optKeyOpen.Checked)
                return Enums.TileType.KeyOpen;
            if (optHeal.Checked)
                return Enums.TileType.Heal;
            if (optKill.Checked)
                return Enums.TileType.Kill;
            if (optSound.Checked)
                return Enums.TileType.Sound;
            if (optScripted.Checked)
                return Enums.TileType.Scripted;
            if (optNotice.Checked)
                return Enums.TileType.Notice;
            if (optLinkShop.Checked)
                return Enums.TileType.LinkShop;
            if (optDoor.Checked)
                return Enums.TileType.Door;
            if (optSign.Checked)
                return Enums.TileType.Sign;
            if (optSpriteChange.Checked)
                return Enums.TileType.SpriteChange;
            if (optShop.Checked)
                return Enums.TileType.Shop;
            if (optArena.Checked)
                return Enums.TileType.Arena;
            if (optBank.Checked)
                return Enums.TileType.Bank;
            if (optGuildBlock.Checked)
                return Enums.TileType.Guild;
            if (optSpriteBlock.Checked)
                return Enums.TileType.SpriteBlock;
            if (optMobileBlock.Checked)
                return Enums.TileType.MobileBlock;
            if (optLevelBlock.Checked)
                return Enums.TileType.LevelBlock;
            if (optAssembly.Checked)
                return Enums.TileType.Assembly;
            if (optEvolution.Checked)
                return Enums.TileType.Evolution;
            if (optStory.Checked)
                return Enums.TileType.Story;
            if (optMission.Checked)
                return Enums.TileType.MissionBoard;
            if (optScriptedSign.Checked)
                return Enums.TileType.ScriptedSign;
            if (optAmbiguous.Checked)
                return Enums.TileType.Ambiguous;
            if (optSlippery.Checked)
                return Enums.TileType.Slippery;
            if (optSlow.Checked)
                return Enums.TileType.Slow;
            if (optDropShop.Checked)
                return Enums.TileType.DropShop;
            return Enums.TileType.Walkable;
        }

        public void SetMapLayer(int x, int y, Enums.LayerType layer, int set, int startX, int startY, int endX, int endY)
        {
            int length = tilesetViewer.ActiveTilesetSurface.Size.Width / 32;

            for (int i = startX; i <= endX; i++)
            {
                for (int j = startY; j <= endY; j++)
                {
                    SetMapLayer(x + i - startX, y + j - startY, layer, set, length*j + i);
                }
            }
        }

        public void SetMapLayer(int x, int y, Enums.LayerType layer, int set, int tile)
        {
            if (x < 0 || x > mapViewer.ActiveMap.MaxX || y < 0 || y > mapViewer.ActiveMap.MaxY) return;

            switch (layer) {
                case Enums.LayerType.Ground:
                    mapViewer.ActiveMap.Tile[x, y].GroundSet = set;
                    mapViewer.ActiveMap.Tile[x, y].Ground = tile;
                    break;
                case Enums.LayerType.GroundAnim:
                    mapViewer.ActiveMap.Tile[x, y].GroundAnimSet = set;
                    mapViewer.ActiveMap.Tile[x, y].GroundAnim = tile;
                    break;
                case Enums.LayerType.Mask:
                    mapViewer.ActiveMap.Tile[x, y].MaskSet = set;
                    mapViewer.ActiveMap.Tile[x, y].Mask = tile;
                    break;
                case Enums.LayerType.MaskAnim:
                    mapViewer.ActiveMap.Tile[x, y].AnimSet = set;
                    mapViewer.ActiveMap.Tile[x, y].Anim = tile;
                    break;
                case Enums.LayerType.Mask2:
                    mapViewer.ActiveMap.Tile[x, y].Mask2Set = set;
                    mapViewer.ActiveMap.Tile[x, y].Mask2 = tile;
                    break;
                case Enums.LayerType.Mask2Anim:
                    mapViewer.ActiveMap.Tile[x, y].M2AnimSet = set;
                    mapViewer.ActiveMap.Tile[x, y].M2Anim = tile;
                    break;
                case Enums.LayerType.Fringe:
                    mapViewer.ActiveMap.Tile[x, y].FringeSet = set;
                    mapViewer.ActiveMap.Tile[x, y].Fringe = tile;
                    break;
                case Enums.LayerType.FringeAnim:
                    mapViewer.ActiveMap.Tile[x, y].FAnimSet = set;
                    mapViewer.ActiveMap.Tile[x, y].FAnim = tile;
                    break;
                case Enums.LayerType.Fringe2:
                    mapViewer.ActiveMap.Tile[x, y].Fringe2Set = set;
                    mapViewer.ActiveMap.Tile[x, y].Fringe2 = tile;
                    break;
                case Enums.LayerType.Fringe2Anim:
                    mapViewer.ActiveMap.Tile[x, y].F2AnimSet = set;
                    mapViewer.ActiveMap.Tile[x, y].F2Anim = tile;
                    break;
            }
        }

        public int GetActiveLayerTileset(int x, int y) {
            switch (GetActiveLayer()) {
                case Enums.LayerType.Ground:
                    return mapViewer.ActiveMap.Tile[x, y].GroundSet;
                case Enums.LayerType.Mask:
                    return mapViewer.ActiveMap.Tile[x, y].MaskSet;
                case Enums.LayerType.MaskAnim:
                    return mapViewer.ActiveMap.Tile[x, y].AnimSet;
                case Enums.LayerType.Mask2:
                    return mapViewer.ActiveMap.Tile[x, y].Mask2Set;
                case Enums.LayerType.Mask2Anim:
                    return mapViewer.ActiveMap.Tile[x, y].M2AnimSet;
                case Enums.LayerType.Fringe:
                    return mapViewer.ActiveMap.Tile[x, y].FringeSet;
                case Enums.LayerType.FringeAnim:
                    return mapViewer.ActiveMap.Tile[x, y].FAnimSet;
                case Enums.LayerType.Fringe2:
                    return mapViewer.ActiveMap.Tile[x, y].Fringe2Set;
                case Enums.LayerType.Fringe2Anim:
                    return mapViewer.ActiveMap.Tile[x, y].F2AnimSet;
                default:
                    return 0;
            }
        }

        public int GetActiveLayerTile(int x, int y) {
            switch (GetActiveLayer()) {
                case Enums.LayerType.Ground:
                    return mapViewer.ActiveMap.Tile[x, y].Ground;
                case Enums.LayerType.GroundAnim:
                    return mapViewer.ActiveMap.Tile[x, y].GroundAnim;
                case Enums.LayerType.Mask:
                    return mapViewer.ActiveMap.Tile[x, y].Mask;
                case Enums.LayerType.MaskAnim:
                    return mapViewer.ActiveMap.Tile[x, y].Anim;
                case Enums.LayerType.Mask2:
                    return mapViewer.ActiveMap.Tile[x, y].Mask2;
                case Enums.LayerType.Mask2Anim:
                    return mapViewer.ActiveMap.Tile[x, y].M2Anim;
                case Enums.LayerType.Fringe:
                    return mapViewer.ActiveMap.Tile[x, y].Fringe;
                case Enums.LayerType.FringeAnim:
                    return mapViewer.ActiveMap.Tile[x, y].FAnim;
                case Enums.LayerType.Fringe2:
                    return mapViewer.ActiveMap.Tile[x, y].Fringe2;
                case Enums.LayerType.Fringe2Anim:
                    return mapViewer.ActiveMap.Tile[x, y].F2Anim;
                default:
                    return 0;
            }
        }

        public void SetMapAttribute(int x, int y, Enums.TileType tileType, int data1, int data2, int data3,
            string string1, string string2, string string3, int dungeonValue) {

            Maps.Tile tile = mapViewer.ActiveMap.Tile[x, y];

            tile.Type = tileType;
            tile.Data1 = data1;
            tile.Data2 = data2;
            tile.Data3 = data3;
            tile.String1 = string1;
            tile.String2 = string2;
            tile.String3 = string3;
            tile.RDungeonMapValue = dungeonValue;
        }

        private void PlaceLayer(Point relPoint, SdlDotNet.Input.MouseButton button) {
            int X = (relPoint.X / Constants.TILE_WIDTH) + Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
            int Y = (relPoint.Y / Constants.TILE_HEIGHT) + Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
            if (X <= mapViewer.ActiveMap.MaxX && Y <= mapViewer.ActiveMap.MaxY && X >= 0 && Y >= 0) {
                if (button == SdlDotNet.Input.MouseButton.PrimaryButton) {
                    if (tilesetViewer.SelectedTile == tilesetViewer.EndTile)
                    {
                        SetMapLayer(X, Y, GetActiveLayer(), tilesetViewer.ActiveTilesetSurface.TilesetNumber, tilesetViewer.SelectedTile.Y * (tilesetViewer.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + tilesetViewer.SelectedTile.X);
                    }
                    else
                    {
                        SetMapLayer(X, Y, GetActiveLayer(), tilesetViewer.ActiveTilesetSurface.TilesetNumber, tilesetViewer.SelectedTile.X, tilesetViewer.SelectedTile.Y, tilesetViewer.EndTile.X, tilesetViewer.EndTile.Y);
                    }
                } else if (button == SdlDotNet.Input.MouseButton.SecondaryButton) {
                    SetMapLayer(X, Y, GetActiveLayer(), 0, 0);
                }
                if (inLiveMode) {
                    if (tilesetViewer.SelectedTile == tilesetViewer.EndTile) {
                        Messenger.SendTilePlacedData(X, Y, GetActiveLayer(), GetActiveLayerTileset(X, Y), GetActiveLayerTile(X, Y));
                    } else {
                        for (int x = tilesetViewer.SelectedTile.X; x <= tilesetViewer.EndTile.X; x++) {
                            for (int y = tilesetViewer.SelectedTile.Y; y <= tilesetViewer.EndTile.Y; y++) {
                                if (X + x - tilesetViewer.SelectedTile.X < 0 || X + x - tilesetViewer.SelectedTile.X > mapViewer.ActiveMap.MaxX ||
                                    Y + y - tilesetViewer.SelectedTile.Y < 0 || Y + y - tilesetViewer.SelectedTile.Y > mapViewer.ActiveMap.MaxY) {

                                } else {
                                    Messenger.SendTilePlacedData(X + x - tilesetViewer.SelectedTile.X, Y + y - tilesetViewer.SelectedTile.Y, GetActiveLayer(), GetActiveLayerTileset(X + x - tilesetViewer.SelectedTile.X, Y + y - tilesetViewer.SelectedTile.Y), GetActiveLayerTile(X + x - tilesetViewer.SelectedTile.X, Y + y - tilesetViewer.SelectedTile.Y));
                                }
                            }
                        }

                        //Messenger.SendTilePlacedData(X, Y, X + tilesetViewer.EndTile.X - tilesetViewer.SelectedTile.X, Y + tilesetViewer.EndTile.Y - tilesetViewer.SelectedTile.Y, GetActiveLayer(), GetActiveLayerTileset(X, Y), GetActiveLayerTile(X, Y));
                    }
                }
            }
        }

        private void FillAttributesFromSettings(bool sendLiveModeData) {
            Enums.TileType tileType = Enums.TileType.Walkable;
            int data1 = 0;
            int data2 = 0;
            int data3 = 0;
            string string1 = "";
            string string2 = "";
            string string3 = "";
            int dungeonValue = 0;
            switch (GetActiveAttribute()) {
                case Enums.TileType.Blocked: {
                        tileType = Enums.TileType.Blocked;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Warp: {
                        tileType = Enums.TileType.Warp;
                        data1 = nudStoryLevel.Value;
                        data2 = hsb1.Value;
                        data3 = hsb2.Value;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Item: {
                        tileType = Enums.TileType.Item;
                        data1 = hsb1.Value;
                        data2 = hsb2.Value;
                        data3 = chkTake.Checked.ToIntString().ToInt();
                        string1 = chkHidden.Checked.ToIntString();
                        string2 = txt1.Text;
                        string3 = "";
                    }
                    break;
                case Enums.TileType.NPCAvoid: {
                        tileType = Enums.TileType.NPCAvoid;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Key: {
                        tileType = Enums.TileType.Key;
                        data1 = hsb1.Value;
                        data2 = chkTake.Checked.ToIntString().ToInt();
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.KeyOpen: {
                        tileType = Enums.TileType.KeyOpen;
                        data1 = hsb1.Value;
                        data2 = hsb2.Value;
                        data3 = 0;
                        string1 = txt1.Text;
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Heal: {
                        tileType = Enums.TileType.Heal;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Kill: {
                        tileType = Enums.TileType.Kill;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Sound: {
                        tileType = Enums.TileType.Sound;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = ((ListBoxTextItem)lstSound.SelectedItems[0]).Text;
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Scripted: {
                        tileType = Enums.TileType.Scripted;
                        data1 = hsb1.Value;
                        data2 = 0;
                        data3 = 0;
                        string1 = txt1.Text;
                        string2 = txt2.Text;
                        string3 = txt3.Text;
                    }
                    break;
                case Enums.TileType.Notice: {
                        tileType = Enums.TileType.Notice;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = txt1.Text;
                        string2 = txt2.Text;
                        string3 = ((ListBoxTextItem)lstSound.SelectedItems[0]).Text;
                    }
                    break;
                case Enums.TileType.LinkShop: {
                        tileType = Enums.TileType.LinkShop;
                        data1 = hsb1.Value;
                        data2 = hsb2.Value;
                        data3 = chkTake.Checked.ToIntString().ToInt();
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Door: {
                        tileType = Enums.TileType.Door;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Sign: {
                        tileType = Enums.TileType.Sign;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = txt1.Text;
                        string2 = txt2.Text;
                        string3 = txt3.Text;
                    }
                    break;
                case Enums.TileType.SpriteChange: {
                        tileType = Enums.TileType.SpriteChange;
                        data1 = hsb1.Value;
                        data2 = hsb2.Value;
                        data3 = hsb3.Value;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Shop: {
                        tileType = Enums.TileType.Shop;
                        data1 = hsb1.Value;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Arena: {
                        tileType = Enums.TileType.Arena;
                        data1 = hsb1.Value;
                        data2 = hsb2.Value;
                        data3 = hsb3.Value;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Bank: {
                        tileType = Enums.TileType.Bank;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.LevelBlock: {
                        tileType = Enums.TileType.LevelBlock;
                        data1 = nudStoryLevel.Value;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.SpriteBlock: {
                        tileType = Enums.TileType.SpriteBlock;
                        data1 = Mode;
                        data2 = hsb1.Value;
                        data3 = hsb2.Value;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.MobileBlock: {
                        tileType = Enums.TileType.MobileBlock;
                        int mobility = 0;
                        for (int i = 15; i >= 0; i--) {
                            if (tempArrayForMobility[i]) {
                                mobility += (int)System.Math.Pow(2, i);
                            }
                        }
                        data1 = mobility;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Guild: {
                        tileType = Enums.TileType.Guild;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Assembly: {
                        tileType = Enums.TileType.Assembly;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Evolution: {
                        tileType = Enums.TileType.Evolution;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Story: {
                        tileType = Enums.TileType.Story;
                        data1 = nudStoryLevel.Value - 1;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.MissionBoard: {
                        tileType = Enums.TileType.MissionBoard;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.ScriptedSign: {
                        tileType = Enums.TileType.ScriptedSign;
                        data1 = hsb1.Value;
                        data2 = 0;
                        data3 = 0;
                        string1 = txt1.Text;
                        string2 = txt2.Text;
                        string3 = txt3.Text;
                    }
                    break;
                case Enums.TileType.Ambiguous: {
                        tileType = Enums.TileType.Ambiguous;
                        data1 = 0;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Slippery: {
                        tileType = Enums.TileType.Slippery;
                        int mobility = 0;
                        for (int i = 15; i >= 0; i--) {
                            if (tempArrayForMobility[i]) {
                                mobility += (int)System.Math.Pow(2, i);
                            }
                        }
                        data1 = mobility;
                        data2 = 0;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.Slow: {
                        tileType = Enums.TileType.Slow;
                        int mobility = 0;
                        for (int i = 15; i >= 0; i--) {
                            if (tempArrayForMobility[i]) {
                                mobility += (int)System.Math.Pow(2, i);
                            }
                        }
                        data1 = mobility;
                        data2 = hsb2.Value;
                        data3 = 0;
                        string1 = "";
                        string2 = "";
                        string3 = "";
                    }
                    break;
                case Enums.TileType.DropShop: {
                    tileType = Enums.TileType.DropShop;
                        data2 = hsb1.Value;
                        data3 = hsb2.Value;
                        data1 = hsb3.Value;
                        string2 = txt1.Text;
                        string3 = "";
                    }
                    break;
            }
            dungeonValue = nudDungeonTileValue.Value;
            FillAttributes(tileType, data1, data2, data3, string1, string2, string3, dungeonValue);
            if (sendLiveModeData) {
                if (inLiveMode) {
                    Messenger.SendAttributeFillData(tileType, data1, data2, data3, string1, string2, string3, dungeonValue);
                }
            }
        }

        public void FillAttributes(Enums.TileType tileType, int data1, int data2, int data3, string string1, string string2, string string3, int dungeonValue) {
            if (mapViewer.ActiveMap != null && mapViewer.ActiveMap.Loaded) {
                for (int X = 0; X <= mapViewer.ActiveMap.MaxX; X++) {
                    for (int Y = 0; Y <= mapViewer.ActiveMap.MaxY; Y++) {
                        SetMapAttribute(X, Y, tileType, data1, data2, data3, string1, string2, string3, dungeonValue);
                    }
                }
            }
        }

        public void FillLayer(Enums.LayerType layer, int tileSet, int tileNum) {
            if (mapViewer.ActiveMap != null && mapViewer.ActiveMap.Loaded) {
                for (int X = 0; X <= mapViewer.ActiveMap.MaxX; X++) {
                    for (int Y = 0; Y <= mapViewer.ActiveMap.MaxY; Y++) {
                        SetMapLayer(X, Y, layer, tileSet, tileNum);
                    }
                }
            }
        }

        private void ClearLayer() {
            if (mapViewer.ActiveMap != null && mapViewer.ActiveMap.Loaded) {
                for (int X = 0; X <= mapViewer.ActiveMap.MaxX; X++) {
                    for (int Y = 0; Y <= mapViewer.ActiveMap.MaxY; Y++) {
                        if (btnTerrain.Selected) {
                            switch (GetActiveLayer()) {
                                case Enums.LayerType.Ground:
                                    mapViewer.ActiveMap.Tile[X, Y].GroundSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Ground = 0;
                                    break;
                                case Enums.LayerType.GroundAnim:
                                    mapViewer.ActiveMap.Tile[X, Y].GroundAnimSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].GroundAnim = 0;
                                    break;
                                case Enums.LayerType.Mask:
                                    mapViewer.ActiveMap.Tile[X, Y].MaskSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Mask = 0;
                                    break;
                                case Enums.LayerType.MaskAnim:
                                    mapViewer.ActiveMap.Tile[X, Y].AnimSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Anim = 0;
                                    break;
                                case Enums.LayerType.Mask2:
                                    mapViewer.ActiveMap.Tile[X, Y].Mask2Set = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Mask2 = 0;
                                    break;
                                case Enums.LayerType.Mask2Anim:
                                    mapViewer.ActiveMap.Tile[X, Y].M2AnimSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].M2Anim = 0;
                                    break;
                                case Enums.LayerType.Fringe:
                                    mapViewer.ActiveMap.Tile[X, Y].FringeSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Fringe = 0;
                                    break;
                                case Enums.LayerType.FringeAnim:
                                    mapViewer.ActiveMap.Tile[X, Y].FAnimSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].FAnim = 0;
                                    break;
                                case Enums.LayerType.Fringe2:
                                    mapViewer.ActiveMap.Tile[X, Y].Fringe2Set = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].Fringe2 = 0;
                                    break;
                                case Enums.LayerType.Fringe2Anim:
                                    mapViewer.ActiveMap.Tile[X, Y].F2AnimSet = 0;
                                    mapViewer.ActiveMap.Tile[X, Y].F2Anim = 0;
                                    break;
                            }
                        } else if (btnAttributes.Selected) {
                            mapViewer.ActiveMap.Tile[X, Y].Type = Enums.TileType.Walkable;
                            mapViewer.ActiveMap.Tile[X, Y].Data1 = 0;
                            mapViewer.ActiveMap.Tile[X, Y].Data2 = 0;
                            mapViewer.ActiveMap.Tile[X, Y].Data3 = 0;
                            mapViewer.ActiveMap.Tile[X, Y].String1 = "";
                            mapViewer.ActiveMap.Tile[X, Y].String2 = "";
                            mapViewer.ActiveMap.Tile[X, Y].String3 = "";
                            mapViewer.ActiveMap.Tile[X, Y].RDungeonMapValue = 0;
                        }
                    }
                }
            }
        }

        private void PlaceAttribute(Point relPoint, SdlDotNet.Input.MouseButton button) {
            int X = (relPoint.X / Constants.TILE_WIDTH) + Graphics.Renderers.Screen.ScreenRenderer.Camera.X;
            int Y = (relPoint.Y / Constants.TILE_HEIGHT) + Graphics.Renderers.Screen.ScreenRenderer.Camera.Y;
            if (X <= mapViewer.ActiveMap.MaxX && Y <= mapViewer.ActiveMap.MaxY && X >= 0 && Y >= 0) {
                Maps.Tile tile = mapViewer.ActiveMap.Tile[X, Y];
                if (button == SdlDotNet.Input.MouseButton.PrimaryButton) {
                    switch (GetActiveAttribute()) {
                        case Enums.TileType.Blocked: {
                                tile.Type = Enums.TileType.Blocked;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Warp: {
                                tile.Type = Enums.TileType.Warp;
                                tile.Data1 = nudStoryLevel.Value;
                                tile.Data2 = hsb1.Value;
                                tile.Data3 = hsb2.Value;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Item: {
                                tile.Type = Enums.TileType.Item;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = chkTake.Checked.ToIntString().ToInt();
                                tile.String1 = chkHidden.Checked.ToIntString();
                                tile.String2 = txt1.Text;
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.NPCAvoid: {
                                tile.Type = Enums.TileType.NPCAvoid;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Key: {
                                tile.Type = Enums.TileType.Key;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = chkTake.Checked.ToIntString().ToInt();
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.KeyOpen: {
                                tile.Type = Enums.TileType.KeyOpen;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = 0;
                                tile.String1 = txt1.Text;
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Heal: {
                                tile.Type = Enums.TileType.Heal;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Kill: {
                                tile.Type = Enums.TileType.Kill;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Sound: {
                                tile.Type = Enums.TileType.Sound;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = ((ListBoxTextItem)lstSound.SelectedItems[0]).Text;
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Scripted: {
                                tile.Type = Enums.TileType.Scripted;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = txt1.Text;
                                tile.String2 = txt2.Text;
                                tile.String3 = txt3.Text;
                            }
                            break;
                        case Enums.TileType.Notice: {
                                tile.Type = Enums.TileType.Notice;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = txt1.Text;
                                tile.String2 = txt2.Text;
                                tile.String3 = ((ListBoxTextItem)lstSound.SelectedItems[0]).Text;
                            }
                            break;
                        case Enums.TileType.LinkShop: {
                                tile.Type = Enums.TileType.LinkShop;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = chkTake.Checked.ToIntString().ToInt();
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Door: {
                                tile.Type = Enums.TileType.Door;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Sign: {
                                tile.Type = Enums.TileType.Sign;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = txt1.Text;
                                tile.String2 = txt2.Text;
                                tile.String3 = txt3.Text;
                            }
                            break;
                        case Enums.TileType.SpriteChange: {
                                tile.Type = Enums.TileType.SpriteChange;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = hsb3.Value;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Shop: {
                                tile.Type = Enums.TileType.Shop;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Arena: {
                                tile.Type = Enums.TileType.Arena;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = hsb3.Value;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Bank: {
                                tile.Type = Enums.TileType.Bank;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.LevelBlock: {
                                tile.Type = Enums.TileType.LevelBlock;
                                tile.Data1 = nudStoryLevel.Value;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.SpriteBlock: {
                                tile.Type = Enums.TileType.SpriteBlock;
                                tile.Data1 = Mode;
                                tile.Data2 = hsb1.Value;
                                tile.Data3 = hsb2.Value;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.MobileBlock: {
                                tile.Type = Enums.TileType.MobileBlock;
                                int mobility = 0;
                                for (int i = 15; i >= 0; i--) {
                                    if (tempArrayForMobility[i]) {
                                        mobility += (int)System.Math.Pow(2, i);
                                    }
                                }
                                tile.Data1 = mobility;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Guild: {
                                tile.Type = Enums.TileType.Guild;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Assembly: {
                                tile.Type = Enums.TileType.Assembly;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Evolution: {
                                tile.Type = Enums.TileType.Evolution;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Story: {
                                tile.Type = Enums.TileType.Story;
                                tile.Data1 = nudStoryLevel.Value - 1;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.MissionBoard: {
                                tile.Type = Enums.TileType.MissionBoard;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.ScriptedSign: {
                                tile.Type = Enums.TileType.ScriptedSign;
                                tile.Data1 = hsb1.Value;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = txt1.Text;
                                tile.String2 = txt2.Text;
                                tile.String3 = txt3.Text;
                            }
                            break;
                        case Enums.TileType.Ambiguous: {
                                tile.Type = Enums.TileType.Ambiguous;
                                tile.Data1 = 0;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Slippery: {
                                tile.Type = Enums.TileType.Slippery;
                                int mobility = 0;
                                for (int i = 15; i >= 0; i--) {
                                    if (tempArrayForMobility[i]) {
                                        mobility += (int)System.Math.Pow(2, i);
                                    }
                                }
                                tile.Data1 = mobility;
                                tile.Data2 = 0;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.Slow: {
                                tile.Type = Enums.TileType.Slow;
                                int mobility = 0;
                                for (int i = 15; i >= 0; i--) {
                                    if (tempArrayForMobility[i]) {
                                        mobility += (int)System.Math.Pow(2, i);
                                    }
                                }
                                tile.Data1 = mobility;
                                tile.Data2 = hsb2.Value;
                                tile.Data3 = 0;
                                tile.String1 = "";
                                tile.String2 = "";
                                tile.String3 = "";
                            }
                            break;
                        case Enums.TileType.DropShop: {
                                tile.Type = Enums.TileType.DropShop;
                                tile.Data2 = hsb1.Value;
                                tile.Data3 = hsb2.Value;
                                tile.Data1 = hsb3.Value;
                                tile.String2 = txt1.Text;
                                tile.String3 = "";
                            }
                            break;
                    }
                    tile.RDungeonMapValue = nudDungeonTileValue.Value;
                } else if (button == SdlDotNet.Input.MouseButton.SecondaryButton) {
                    tile.Type = Enums.TileType.Walkable;
                    tile.Data1 = 0;
                    tile.Data2 = 0;
                    tile.Data3 = 0;
                    tile.String1 = "";
                    tile.String2 = "";
                    tile.String3 = "";
                    tile.RDungeonMapValue = 0;
                }
                if (inLiveMode) {
                    Messenger.SendAttributePlacedData(X, Y, tile.Type, tile.Data1, tile.Data2, tile.Data3,
                        tile.String1, tile.String2, tile.String3, tile.RDungeonMapValue);
                }
            }
        }

        public int GetSelectedTileNumber() {
            return tilesetViewer.SelectedTile.Y * (tilesetViewer.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + tilesetViewer.SelectedTile.X;
        }
    }
}
