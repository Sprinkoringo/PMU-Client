using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;
using Client.Logic.Network;
using PMU.Sockets;
using PMU.Core;
using Client.Logic.Maps;
using Client.Logic.Editors.RDungeons;

namespace Client.Logic.Windows.Editors
{
    class winRDungeonPanel : Core.WindowCore
    {
        #region Fields

        //int itemNum = 0;
        int currentTen = 0;
        int editedTile = -1;
        EditableRDungeon rdungeon;

        #region Panels
        Panel pnlRDungeonList;
        Panel pnlRDungeonGeneral;
        Panel pnlRDungeonFloors;
        Panel pnlRDungeonFloorSettingSelection;
        Panel pnlRDungeonStructure;
        Panel pnlRDungeonLandTiles;
        Panel pnlRDungeonLandAltTiles;
        Panel pnlRDungeonWaterTiles;
        Panel pnlRDungeonWaterAnimTiles;
        Panel pnlRDungeonAttributes;
        Panel pnlRDungeonItems;
        Panel pnlRDungeonNpcs;
        Panel pnlRDungeonTraps;
        Panel pnlRDungeonWeather;
        Panel pnlRDungeonGoal;
        Panel pnlRDungeonChambers;
        Panel pnlRDungeonMisc;
        Panel pnlTileSelector;


        bool listLoaded;
        bool generalLoaded;
        bool floorsLoaded;
        bool floorSettingSelectionLoaded;
        bool structureLoaded;
        bool landTilesLoaded;
        bool landAltTilesLoaded;
        bool waterTilesLoaded;
        bool waterAnimTilesLoaded;
        bool attributesLoaded;
        bool itemsLoaded;
        bool npcsLoaded;
        bool trapsLoaded;
        bool weatherLoaded;
        bool goalLoaded;
        bool chambersLoaded;
        bool miscLoaded;
        bool tileSelectorLoaded;

        #endregion

        #region RDungeon List
        ListBox lbxRDungeonList;
        //ListBoxTextItem lbiRDungeon;
        Button btnBack;
        Button btnForward;
        Button btnAddNew;
        //Implement btnAddNew to certain editors when Ready
        Button btnCancel;
        Button btnEdit;
        #endregion

        #region General Widgets
        Label lblGeneral;
        Button btnFloors;
        Label lblDungeonName;
        TextBox txtDungeonName;
        Label lblDirection;
        RadioButton optUp;
        RadioButton optDown;
        Label lblMaxFloors;
        NumericUpDown nudMaxFloors;
        CheckBox chkRecruiting;
        CheckBox chkEXPGain;
        Label lblWindTimer;
        NumericUpDown nudWindTimer;
        Button btnEditorCancel;
        Button btnSave;
        #endregion

        #region Floor Widgets
        Label lblFloors;
        Button btnGeneral;
        Label lblFromFloorNumber;
        Label lblToFloorNumber;
        NumericUpDown nudFirstFloor;
        NumericUpDown nudLastFloor;
        Button btnSettingsMenu;
        Button btnSaveFloor;
        Button btnLoadFloor;
        Label lblSaveLoadMessage;
        #endregion

        #region Floor Settings Selection Widgets
        Button btnStructure;
        Button btnLandTiles;
        Button btnWaterTiles;
        Button btnAttributes;
        Button btnItems;
        Button btnNpcs;
        Button btnTraps;
        Button btnWeather;
        Button btnGoal;
        Button btnChambers;
        Button btnMisc;
        #endregion

        #region Floor Panels
        #region Structure
        Label lblStructure;
        Label lblTrapMin;
        NumericUpDown nudTrapMin;
        Label lblTrapMax;
        NumericUpDown nudTrapMax;
        Label lblItemMin;
        NumericUpDown nudItemMin;
        Label lblItemMax;
        NumericUpDown nudItemMax;
        Label lblRoomWidthMin;
        NumericUpDown nudRoomWidthMin;
        Label lblRoomWidthMax;
        NumericUpDown nudRoomWidthMax;
        Label lblRoomLengthMin;
        NumericUpDown nudRoomLengthMin;
        Label lblRoomLengthMax;
        NumericUpDown nudRoomLengthMax;
        Label lblHallTurnMin;
        NumericUpDown nudHallTurnMin;
        Label lblHallTurnMax;
        NumericUpDown nudHallTurnMax;
        Label lblHallVarMin;
        NumericUpDown nudHallVarMin;
        Label lblHallVarMax;
        NumericUpDown nudHallVarMax;
        Label lblWaterFrequency;
        NumericUpDown nudWaterFrequency;
        Label lblCraters;
        NumericUpDown nudCraters;
        Label lblCraterMinLength;
        NumericUpDown nudCraterMinLength;
        Label lblCraterMaxLength;
        NumericUpDown nudCraterMaxLength;
        CheckBox chkCraterFuzzy;
        #endregion
        #region Land Tiles
        Label lblLandTiles;

        Label[] lblLandTileset;
        PictureBox[] picLandTileset;
        int[,] landTileNumbers;
        Button btnLandAltSwitch;
        /*
        Label lblStairs;
        Label lblGround;
        Label lblTopLeft;
        Label lblTopCenter;
        Label lblTopRight;
        Label lblCenterLeft;
        Label lblCenterCenter;
        Label lblCenterRight;
        Label lblBottomLeft;
        Label lblBottomCenter;
        Label lblBottomRight;
        Label lblInnerTopLeft;
        Label lblInnerTopRight;
        Label lblInnerBottomLeft;
        Label lblInnerBottomRight;
        Label lblIsolatedWall;
        Label lblColumnTop;
        Label lblColumnCenter;
        Label lblColumnBottom;
        Label lblRowLeft;
        Label lblRowCenter;
        Label lblRowRight;
        
        */
        #endregion
        #region Land Alt Tiles
        Label lblLandAltTiles;

        Label[] lblLandAltTileset;
        PictureBox[] picLandAltTileset;
        int[,] landAltTileNumbers;
        Button btnLandSwitch;
        /*
        Label lblStairsAlt;
        Label lblGroundAlt;
        Label lblTopLeftAlt;
        Label lblTopCenterAlt;
        Label lblTopRightAlt;
        Label lblCenterLeftAlt;
        Label lblCenterCenterAlt;
        Label lblCenterRightAlt;
        Label lblBottomLeftAlt;
        Label lblBottomCenterAlt;
        Label lblBottomRightAlt;
        Label lblInnerTopLeftAlt;
        Label lblInnerTopRightAlt;
        Label lblInnerBottomLeftAlt;
        Label lblInnerBottomRightAlt;
        Label lblIsolatedWallAlt;
        Label lblColumnTopAlt;
        Label lblColumnCenterAlt;
        Label lblColumnBottomAlt;
        Label lblRowLeftAlt;
        Label lblRowCenterAlt;
        Label lblRowRightAlt;
        
        */
        #endregion
        #region Water Tiles
        Label lblWaterTiles;

        Label[] lblWaterTileset;
        PictureBox[] picWaterTileset;
        int[,] waterTileNumbers;
        Button btnWaterAnimSwitch;
        /*
        Label lblShoreSurrounded;
        Label lblShoreInnerTopLeft;
        Label lblShoreTop;
        Label lblShoreInnerTopRight;
        Label lblShoreLeft;
        Label lblWater;
        Label lblShoreRight;
        Label lblShoreInnerBottomLeft;
        Label lblShoreBottom;
        Label lblShoreInnerBottomRight;
        Label lblShoreTopLeft;
        Label lblShoreTopRight;
        Label lblShoreBottomLeft;
        Label lblShoreBottomRight;
        Label lblShoreDiagonalForward;
        Label lblShoreDiagonalBack;
        Label lblShoreInnerTop;
        Label lblShoreVertical;
        Label lblShoreInnerBottom;
        Label lblShoreInnerLeft;
        Label lblShoreHorizontal;
        Label lblShoreInnerRight;
        
        */

        #endregion
        #region Water Anim Tiles
        Label lblWaterAnimTiles;

        Label[] lblWaterAnimTileset;
        PictureBox[] picWaterAnimTileset;
        int[,] waterAnimTileNumbers;
        Button btnWaterSwitch;
        /*
        Label lblShoreSurroundedAnim;
        Label lblShoreInnerTopLeftAnim;
        Label lblShoreTopAnim;
        Label lblShoreInnerTopRightAnim;
        Label lblShoreLeftAnim;
        Label lblWaterAnim;
        Label lblShoreRightAnim;
        Label lblShoreInnerBottomLeftAnim;
        Label lblShoreBottomAnim;
        Label lblShoreInnerBottomRightAnim;
        Label lblShoreTopLeftAnim;
        Label lblShoreTopRightAnim;
        Label lblShoreBottomLeftAnim;
        Label lblShoreBottomRightAnim;
        Label lblShoreDiagonalForwardAnim;
        Label lblShoreDiagonalBackAnim;
        Label lblShoreInnerTopAnim;
        Label lblShoreVerticalAnim;
        Label lblShoreInnerBottomAnim;
        Label lblShoreInnerLeftAnim;
        Label lblShoreHorizontalAnim;
        Label lblShoreInnerRightAnim;
        
        */

        #endregion
        #region Attributes
        Label lblAttributes;

        ComboBox cbGroundType;
        Label lblGroundData1;
        NumericUpDown nudGroundData1;
        Label lblGroundData2;
        NumericUpDown nudGroundData2;
        Label lblGroundData3;
        NumericUpDown nudGroundData3;

        Label lblGroundString1;
        TextBox txtGroundString1;
        Label lblGroundString2;
        TextBox txtGroundString2;
        Label lblGroundString3;
        TextBox txtGroundString3;

        ComboBox cbHallType;
        Label lblHallData1;
        NumericUpDown nudHallData1;
        Label lblHallData2;
        NumericUpDown nudHallData2;
        Label lblHallData3;
        NumericUpDown nudHallData3;

        Label lblHallString1;
        TextBox txtHallString1;
        Label lblHallString2;
        TextBox txtHallString2;
        Label lblHallString3;
        TextBox txtHallString3;

        ComboBox cbWaterType;
        Label lblWaterData1;
        NumericUpDown nudWaterData1;
        Label lblWaterData2;
        NumericUpDown nudWaterData2;
        Label lblWaterData3;
        NumericUpDown nudWaterData3;

        Label lblWaterString1;
        TextBox txtWaterString1;
        Label lblWaterString2;
        TextBox txtWaterString2;
        Label lblWaterString3;
        TextBox txtWaterString3;

        ComboBox cbWallType;
        Label lblWallData1;
        NumericUpDown nudWallData1;
        Label lblWallData2;
        NumericUpDown nudWallData2;
        Label lblWallData3;
        NumericUpDown nudWallData3;

        Label lblWallString1;
        TextBox txtWallString1;
        Label lblWallString2;
        TextBox txtWallString2;
        Label lblWallString3;
        TextBox txtWallString3;

        #endregion
        #region Items
        Label lblItems;

        ListBox lbxItems;
        List<EditableRDungeonItem> itemList;

        Label lblItemNum;
        NumericUpDown nudItemNum;
        Label lblMinValue;
        NumericUpDown nudMinValue;
        Label lblMaxValue;
        NumericUpDown nudMaxValue;
        Label lblItemSpawnRate;
        NumericUpDown nudItemSpawnRate;
        Label lblStickyRate;
        NumericUpDown nudStickyRate;
        Label lblTag;
        TextBox txtTag;
        CheckBox chkHidden;
        CheckBox chkOnGround;
        CheckBox chkOnWater;
        CheckBox chkOnWall;
        Button btnAddItem;
        Button btnRemoveItem;
        Button btnLoadItem;
        Button btnChangeItem;
        CheckBox chkBulkItem;

        #endregion
        #region NPCs
        Label lblNpcs;

        Label lblNpcSpawnTime;
        NumericUpDown nudNpcSpawnTime;

        Label lblNpcMin;
        NumericUpDown nudNpcMin;
        Label lblNpcMax;
        NumericUpDown nudNpcMax;

        ListBox lbxNpcs;
        List<MapNpcSettings> npcList;

        Label lblNpcNum;
        NumericUpDown nudNpcNum;
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
        Button btnChangeNpc;
        CheckBox chkBulkNpc;

        #endregion
        #region Traps
        Label lblTraps;

        ListBox lbxTraps;
        List<EditableRDungeonTrap> trapList;
        Label[] lblTrapTileset;
        PictureBox[] picTrapTileset;
        int[,] trapTileNumbers;

        ComboBox cbTrapType;
        Label lblTrapData1;
        NumericUpDown nudTrapData1;
        Label lblTrapData2;
        NumericUpDown nudTrapData2;
        Label lblTrapData3;
        NumericUpDown nudTrapData3;
        Label lblTrapString1;
        TextBox txtTrapString1;
        Label lblTrapString2;
        TextBox txtTrapString2;
        Label lblTrapString3;
        TextBox txtTrapString3;


        Label lblTrapChance;
        NumericUpDown nudTrapChance;

        Button btnAddTrap;
        Button btnRemoveTrap;
        Button btnLoadTrap;
        Button btnChangeTrap;

        #endregion
        #region Weather
        Label lblWeather;

        ListBox lbxWeather;
        ComboBox cbWeather;
        Button btnAddWeather;
        Button btnRemoveWeather;
        #endregion
        #region Goal
        Label lblGoal;
        RadioButton optNextFloor;
        RadioButton optMap;
        RadioButton optScripted;
        Label lblData1;
        Label lblData2;
        Label lblData3;
        NumericUpDown nudData1;
        NumericUpDown nudData2;
        NumericUpDown nudData3;

        #endregion
        #region Chambers
        Label lblChambers;

        ListBox lbxChambers;
        List<EditableRDungeonChamber> chamberList;
        Label lblChamberNum;
        NumericUpDown nudChamber;
        Label lblChamberString1;
        TextBox txtChamberString1;
        Label lblChamberString2;
        TextBox txtChamberString2;
        Label lblChamberString3;
        TextBox txtChamberString3;
        Button btnAddChamber;
        Button btnRemoveChamber;
        #endregion
        #region Misc
        Label lblMisc;
        Label lblMusic;
        ListBox lbxMusic;
        Button btnPlayMusic;
        Label lblDarkness;
        NumericUpDown nudDarkness;
        #endregion
        #endregion

        #region Tile Selector

        Widgets.TilesetViewer TileSelector;

        Label lblTileSet;
        NumericUpDown nudTileSet;
        Button btnTileSetOK;
        Button btnTileSetCancel;

        #endregion

        #endregion



        #region Constructors
        public winRDungeonPanel()
            : base("winRDungeonPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Random Dungeon Panel";



            LoadpnlRDungeonList();

            this.LoadComplete();

        }
        #endregion

        #region Methods

        #region Loading

        void LoadpnlRDungeonList() {
            if (!listLoaded) {
                pnlRDungeonList = new Panel("pnlRDungeonList");
                pnlRDungeonList.Size = this.Size;
                pnlRDungeonList.Location = new Point(0, 0);
                pnlRDungeonList.BackColor = Color.White;
                pnlRDungeonList.Visible = true;
                #region RDungeon List

                lbxRDungeonList = new ListBox("lbxRDungeonList");
                lbxRDungeonList.Location = new Point(10, 10);
                lbxRDungeonList.Size = new Size(180, 140);
                for (int i = 0; i < 10; i++) {

                    lbxRDungeonList.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + RDungeons.RDungeonHelper.RDungeons[i].Name));
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
                btnEdit.Size = new System.Drawing.Size(48, 16);
                btnEdit.Visible = true;
                btnEdit.Text = "Edit";
                btnEdit.Click += new EventHandler<MouseButtonEventArgs>(btnEdit_Click);

                btnCancel = new Button("btnCancel");
                btnCancel.Location = new Point(142, 190);
                btnCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnCancel.Size = new System.Drawing.Size(48, 16);
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
                #region Added to pnlRDungeonList
                pnlRDungeonList.AddWidget(lbxRDungeonList);
                pnlRDungeonList.AddWidget(btnBack);
                pnlRDungeonList.AddWidget(btnForward);
                pnlRDungeonList.AddWidget(btnAddNew);
                pnlRDungeonList.AddWidget(btnEdit);
                pnlRDungeonList.AddWidget(btnCancel);
                #endregion
                this.AddWidget(pnlRDungeonList);
                listLoaded = true;
            }
        }

        void LoadpnlRDungeonGeneral() {
            if (!generalLoaded) {
                pnlRDungeonGeneral = new Panel("pnlRDungeonGeneral");
                pnlRDungeonGeneral.Size = new Size(250, 220);
                pnlRDungeonGeneral.Location = new Point(0, 0);
                pnlRDungeonGeneral.BackColor = Color.White;
                pnlRDungeonGeneral.Visible = false;
                #region General
                lblGeneral = new Label("lblGeneral");
                lblGeneral.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGeneral.Text = "General Settings";
                lblGeneral.AutoSize = true;
                lblGeneral.Location = new Point(10, 4);

                btnFloors = new Button("btnFloors");
                btnFloors.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                btnFloors.Size = new System.Drawing.Size(64, 16);
                btnFloors.Text = "Floors ->";
                btnFloors.Location = new Point(158, 4);
                btnFloors.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnFloors_Click);


                lblDungeonName = new Label("lblDungeonName");
                lblDungeonName.AutoSize = true;
                lblDungeonName.Location = new Point(10, 34);
                lblDungeonName.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                lblDungeonName.Text = "Dungeon Name";

                txtDungeonName = new TextBox("txtDungeonName");
                txtDungeonName.Size = new Size(210, 18);
                txtDungeonName.Location = new Point(10, 48);

                lblDirection = new Label("lblDirection");
                lblDirection.AutoSize = true;
                lblDirection.Location = new Point(10, 72);
                lblDirection.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                lblDirection.Text = "Direction";

                optUp = new RadioButton("optUp");
                optUp.BackColor = Color.Transparent;
                optUp.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                optUp.Location = new Point(10, 84);
                optUp.Size = new System.Drawing.Size(95, 17);
                optUp.Text = "Up";

                optDown = new RadioButton("optDown");
                optDown.BackColor = Color.Transparent;
                optDown.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                optDown.Location = new Point(63, 84);
                optDown.Size = new System.Drawing.Size(95, 17);
                optDown.Text = "Down";

                lblMaxFloors = new Label("lblMaxFloors");
                lblMaxFloors.AutoSize = true;
                lblMaxFloors.Location = new Point(128, 72);
                lblMaxFloors.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                lblMaxFloors.Text = "Max Floors";

                nudMaxFloors = new NumericUpDown("nudMaxFloors");
                nudMaxFloors.Minimum = 1;
                nudMaxFloors.Maximum = Int32.MaxValue;
                nudMaxFloors.Size = new Size(80, 20);
                nudMaxFloors.Location = new Point(134, 84);
                nudMaxFloors.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                chkRecruiting = new CheckBox("chkRecruiting");
                chkRecruiting.Size = new Size(100, 17);
                chkRecruiting.Location = new Point(10, 114);
                chkRecruiting.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkRecruiting.Text = "Recruiting";

                chkEXPGain = new CheckBox("chkEXPGain");
                chkEXPGain.Size = new Size(100, 17);
                chkEXPGain.Location = new Point(10, 134);
                chkEXPGain.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkEXPGain.Text = "EXP Gained";

                lblWindTimer = new Label("lblWindTimer");
                lblWindTimer.AutoSize = true;
                lblWindTimer.Location = new Point(128, 118);
                lblWindTimer.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                lblWindTimer.Text = "Time Limit";

                nudWindTimer = new NumericUpDown("nudWindTimer");
                nudWindTimer.Minimum = -1;
                nudWindTimer.Maximum = Int32.MaxValue;
                nudWindTimer.Size = new Size(80, 20);
                nudWindTimer.Location = new Point(134, 130);
                nudWindTimer.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                btnEditorCancel = new Button("btnEditorCancel");
                btnEditorCancel.Location = new Point(120, 170);
                btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnEditorCancel.Size = new System.Drawing.Size(64, 16);
                btnEditorCancel.Visible = true;
                btnEditorCancel.Text = "Cancel";
                btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

                btnSave = new Button("btnSave");
                btnSave.Location = new Point(20, 170);
                btnSave.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnSave.Size = new System.Drawing.Size(64, 16);
                btnSave.Visible = true;
                btnSave.Text = "OK";
                btnSave.Click += new EventHandler<MouseButtonEventArgs>(btnSave_Click);

                #endregion
                #region Added to pnlRDungeonGeneral
                pnlRDungeonGeneral.AddWidget(lblGeneral);
                pnlRDungeonGeneral.AddWidget(btnFloors);
                pnlRDungeonGeneral.AddWidget(btnEditorCancel);
                pnlRDungeonGeneral.AddWidget(btnSave);
                pnlRDungeonGeneral.AddWidget(lblDungeonName);
                pnlRDungeonGeneral.AddWidget(txtDungeonName);
                pnlRDungeonGeneral.AddWidget(lblDirection);
                pnlRDungeonGeneral.AddWidget(optUp);
                pnlRDungeonGeneral.AddWidget(optDown);
                pnlRDungeonGeneral.AddWidget(lblMaxFloors);
                pnlRDungeonGeneral.AddWidget(nudMaxFloors);
                pnlRDungeonGeneral.AddWidget(chkRecruiting);
                pnlRDungeonGeneral.AddWidget(chkEXPGain);
                pnlRDungeonGeneral.AddWidget(lblWindTimer);
                pnlRDungeonGeneral.AddWidget(nudWindTimer);
                #endregion General
                this.AddWidget(pnlRDungeonGeneral);
                generalLoaded = true;
            }
        }

        void LoadpnlRDungeonFloors() {
            if (!floorsLoaded) {
                pnlRDungeonFloors = new Panel("pnlRDungeonFloors");
                pnlRDungeonFloors.Size = new Size(600, 400);
                pnlRDungeonFloors.Location = new Point(0, 0);
                pnlRDungeonFloors.BackColor = Color.White;
                pnlRDungeonFloors.Visible = false;
                #region Floors

                lblFloors = new Label("lblFloors");
                lblFloors.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblFloors.Text = "Floors Settings";
                lblFloors.AutoSize = true;
                lblFloors.Location = new Point(100, 4);

                btnGeneral = new Button("btnGeneral");
                btnGeneral.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnGeneral.Size = new Size(64, 16);
                btnGeneral.Location = new Point(10, 0);
                btnGeneral.Text = "<- General";
                btnGeneral.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnGeneral_Click);

                lblFromFloorNumber = new Label("lblFromFloorNumber");
                lblFromFloorNumber.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblFromFloorNumber.Text = "From Floor #";
                lblFromFloorNumber.AutoSize = true;
                lblFromFloorNumber.Location = new Point(10, 24);

                lblToFloorNumber = new Label("lblToFloorNumber");
                lblToFloorNumber.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblToFloorNumber.Text = "To Floor #";
                lblToFloorNumber.AutoSize = true;
                lblToFloorNumber.Location = new Point(160, 24);

                nudFirstFloor = new NumericUpDown("nudFirstFloor");
                nudFirstFloor.Minimum = 1;
                nudFirstFloor.Maximum = Int32.MaxValue;
                nudFirstFloor.Size = new Size(80, 20);
                nudFirstFloor.Location = new Point(80, 24);
                nudFirstFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                nudFirstFloor.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudFirstFloor_ValueChanged);

                nudLastFloor = new NumericUpDown("nudLastFloor");
                nudLastFloor.Minimum = 1;
                nudLastFloor.Maximum = Int32.MaxValue;
                nudLastFloor.Size = new Size(80, 20);
                nudLastFloor.Location = new Point(220, 24);
                nudLastFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                nudLastFloor.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudLastFloor_ValueChanged);

                btnSettingsMenu = new Button("btnSettingsMenu");
                btnSettingsMenu.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnSettingsMenu.Size = new Size(80, 24);
                btnSettingsMenu.Location = new Point(10, 52);
                btnSettingsMenu.Text = "Settings Menu";
                btnSettingsMenu.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnSettingsMenu_Click);

                btnSaveFloor = new Button("btnSaveFloor");
                btnSaveFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnSaveFloor.Size = new Size(150, 24);
                btnSaveFloor.Location = new Point(100, 52);
                btnSaveFloor.Text = "Save All Settings to Floor(s)";
                btnSaveFloor.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnSaveFloor_Click);

                btnLoadFloor = new Button("btnLoadFloor");
                btnLoadFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLoadFloor.Size = new Size(150, 24);
                btnLoadFloor.Location = new Point(260, 52);
                btnLoadFloor.Text = "Load All Settings from Floor";
                btnLoadFloor.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnLoadFloor_Click);

                lblSaveLoadMessage = new Label("lblSaveLoadMessage");
                lblSaveLoadMessage.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblSaveLoadMessage.Text = "SaveLoadMessage Here";
                lblSaveLoadMessage.AutoSize = true;
                lblSaveLoadMessage.Location = new Point(420, 60);

                #endregion
                #region Added to pnlRDungeonFloors
                pnlRDungeonFloors.AddWidget(lblFloors);
                pnlRDungeonFloors.AddWidget(btnGeneral);
                pnlRDungeonFloors.AddWidget(lblFromFloorNumber);
                pnlRDungeonFloors.AddWidget(lblToFloorNumber);
                pnlRDungeonFloors.AddWidget(nudFirstFloor);
                pnlRDungeonFloors.AddWidget(nudLastFloor);
                pnlRDungeonFloors.AddWidget(btnSettingsMenu);
                pnlRDungeonFloors.AddWidget(btnSaveFloor);
                pnlRDungeonFloors.AddWidget(btnLoadFloor);
                pnlRDungeonFloors.AddWidget(lblSaveLoadMessage);

                #endregion
                this.AddWidget(pnlRDungeonFloors);
                floorsLoaded = true;
            }
        }

        void LoadpnlRDungeonFloorSettingSelection() {
            if (!floorSettingSelectionLoaded) {
                pnlRDungeonFloorSettingSelection = new Panel("pnlRDungeonFloorSettingSelection");
                pnlRDungeonFloorSettingSelection.Size = new Size(600, 320);
                pnlRDungeonFloorSettingSelection.Location = new Point(0, 80);
                pnlRDungeonFloorSettingSelection.BackColor = Color.LightGray;
                pnlRDungeonFloorSettingSelection.Visible = false;
                #region Floor Settings Selection

                btnStructure = new Button("btnStructure");
                btnStructure.Location = new Point(10, 10);
                btnStructure.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnStructure.Size = new System.Drawing.Size(100, 32);
                btnStructure.Text = "Structure";
                btnStructure.Click += new EventHandler<MouseButtonEventArgs>(btnStructure_Click);

                btnLandTiles = new Button("btnLandTiles");
                btnLandTiles.Location = new Point(200, 10);
                btnLandTiles.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnLandTiles.Size = new System.Drawing.Size(100, 32);
                btnLandTiles.Text = "LandTiles";
                btnLandTiles.Click += new EventHandler<MouseButtonEventArgs>(btnLandTiles_Click);

                btnWaterTiles = new Button("btnWaterTiles");
                btnWaterTiles.Location = new Point(390, 10);
                btnWaterTiles.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnWaterTiles.Size = new System.Drawing.Size(100, 32);
                btnWaterTiles.Text = "WaterTiles";
                btnWaterTiles.Click += new EventHandler<MouseButtonEventArgs>(btnWaterTiles_Click);

                btnAttributes = new Button("btnAttributes");
                btnAttributes.Location = new Point(10, 50);
                btnAttributes.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnAttributes.Size = new System.Drawing.Size(100, 32);
                btnAttributes.Text = "Attributes";
                btnAttributes.Click += new EventHandler<MouseButtonEventArgs>(btnAttributes_Click);

                btnItems = new Button("btnItems");
                btnItems.Location = new Point(200, 50);
                btnItems.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnItems.Size = new System.Drawing.Size(100, 32);
                btnItems.Text = "Items";
                btnItems.Click += new EventHandler<MouseButtonEventArgs>(btnItems_Click);

                btnNpcs = new Button("btnNpcs");
                btnNpcs.Location = new Point(390, 50);
                btnNpcs.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnNpcs.Size = new System.Drawing.Size(100, 32);
                btnNpcs.Text = "Npcs";
                btnNpcs.Click += new EventHandler<MouseButtonEventArgs>(btnNpcs_Click);

                btnTraps = new Button("btnTraps");
                btnTraps.Location = new Point(10, 90);
                btnTraps.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnTraps.Size = new System.Drawing.Size(100, 32);
                btnTraps.Text = "Traps";
                btnTraps.Click += new EventHandler<MouseButtonEventArgs>(btnTraps_Click);

                btnWeather = new Button("btnWeather");
                btnWeather.Location = new Point(200, 90);
                btnWeather.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnWeather.Size = new System.Drawing.Size(100, 32);
                btnWeather.Text = "Weather";
                btnWeather.Click += new EventHandler<MouseButtonEventArgs>(btnWeather_Click);

                btnGoal = new Button("btnGoal");
                btnGoal.Location = new Point(390, 90);
                btnGoal.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnGoal.Size = new System.Drawing.Size(100, 32);
                btnGoal.Text = "Goal";
                btnGoal.Click += new EventHandler<MouseButtonEventArgs>(btnGoal_Click);

                btnChambers = new Button("btnChambers");
                btnChambers.Location = new Point(10, 130);
                btnChambers.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnChambers.Size = new System.Drawing.Size(100, 32);
                btnChambers.Text = "Chambers";
                btnChambers.Click += new EventHandler<MouseButtonEventArgs>(btnChambers_Click);

                btnMisc = new Button("btnMisc");
                btnMisc.Location = new Point(200, 130);
                btnMisc.Font = Graphics.FontManager.LoadFont("tahoma", 18);
                btnMisc.Size = new System.Drawing.Size(100, 32);
                btnMisc.Text = "Misc";
                btnMisc.Click += new EventHandler<MouseButtonEventArgs>(btnMisc_Click);


                #endregion
                #region Added to pnlFloorSettingSelection

                pnlRDungeonFloorSettingSelection.AddWidget(btnStructure);
                pnlRDungeonFloorSettingSelection.AddWidget(btnLandTiles);
                pnlRDungeonFloorSettingSelection.AddWidget(btnWaterTiles);
                pnlRDungeonFloorSettingSelection.AddWidget(btnAttributes);
                pnlRDungeonFloorSettingSelection.AddWidget(btnItems);
                pnlRDungeonFloorSettingSelection.AddWidget(btnNpcs);
                pnlRDungeonFloorSettingSelection.AddWidget(btnTraps);
                pnlRDungeonFloorSettingSelection.AddWidget(btnWeather);
                pnlRDungeonFloorSettingSelection.AddWidget(btnGoal);
                pnlRDungeonFloorSettingSelection.AddWidget(btnChambers);
                pnlRDungeonFloorSettingSelection.AddWidget(btnMisc);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonFloorSettingSelection);
                floorSettingSelectionLoaded = true;
            }
        }

        void LoadpnlRDungeonStructure() {
            if (!structureLoaded) {
                pnlRDungeonStructure = new Panel("pnlRDungeonStructure");
                pnlRDungeonStructure.Size = new Size(600, 320);
                pnlRDungeonStructure.Location = new Point(0, 80);
                pnlRDungeonStructure.BackColor = Color.LightGray;
                pnlRDungeonStructure.Visible = false;
                #region Structure
                lblStructure = new Label("lblStructure");
                lblStructure.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblStructure.Text = "Structure Settings";
                lblStructure.AutoSize = true;
                lblStructure.Location = new Point(10, 4);

                lblTrapMin = new Label("lblTrapMin");
                lblTrapMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapMin.Text = "Minimum Traps:";
                lblTrapMin.AutoSize = true;
                lblTrapMin.Location = new Point(10, 28);

                nudTrapMin = new NumericUpDown("nudTrapMin");
                nudTrapMin.Minimum = 0;
                nudTrapMin.Maximum = Int32.MaxValue;
                nudTrapMin.Size = new Size(80, 20);
                nudTrapMin.Location = new Point(10, 42);
                nudTrapMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapMax = new Label("lblTrapMax");
                lblTrapMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapMax.Text = "Maximum Traps:";
                lblTrapMax.AutoSize = true;
                lblTrapMax.Location = new Point(10, 72);

                nudTrapMax = new NumericUpDown("nudTrapMax");
                nudTrapMax.Minimum = 0;
                nudTrapMax.Maximum = Int32.MaxValue;
                nudTrapMax.Size = new Size(80, 20);
                nudTrapMax.Location = new Point(10, 86);
                nudTrapMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblItemMin = new Label("lblItemMin");
                lblItemMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblItemMin.Text = "Minimum Items:";
                lblItemMin.AutoSize = true;
                lblItemMin.Location = new Point(10, 116);

                nudItemMin = new NumericUpDown("nudItemMin");
                nudItemMin.Minimum = 0;
                nudItemMin.Maximum = 255;
                nudItemMin.Size = new Size(80, 20);
                nudItemMin.Location = new Point(10, 130);
                nudItemMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblItemMax = new Label("lblItemMax");
                lblItemMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblItemMax.Text = "Maximum Items:";
                lblItemMax.AutoSize = true;
                lblItemMax.Location = new Point(10, 160);

                nudItemMax = new NumericUpDown("nudItemMax");
                nudItemMax.Minimum = 0;
                nudItemMax.Maximum = 255;
                nudItemMax.Size = new Size(80, 20);
                nudItemMax.Location = new Point(10, 174);
                nudItemMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblRoomWidthMin = new Label("lblRoomWidthMin");
                lblRoomWidthMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblRoomWidthMin.Text = "Min Room Width:";
                lblRoomWidthMin.AutoSize = true;
                lblRoomWidthMin.Location = new Point(140, 28);

                nudRoomWidthMin = new NumericUpDown("nudRoomWidthMin");
                nudRoomWidthMin.Minimum = 0;
                nudRoomWidthMin.Maximum = 48;
                nudRoomWidthMin.Size = new Size(80, 20);
                nudRoomWidthMin.Location = new Point(140, 42);
                nudRoomWidthMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblRoomWidthMax = new Label("lblRoomWidthMax");
                lblRoomWidthMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblRoomWidthMax.Text = "Max Room Width:";
                lblRoomWidthMax.AutoSize = true;
                lblRoomWidthMax.Location = new Point(140, 72);

                nudRoomWidthMax = new NumericUpDown("nudRoomWidthMax");
                nudRoomWidthMax.Minimum = 0;
                nudRoomWidthMax.Maximum = 48;
                nudRoomWidthMax.Size = new Size(80, 20);
                nudRoomWidthMax.Location = new Point(140, 86);
                nudRoomWidthMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblRoomLengthMin = new Label("lblRoomLengthMin");
                lblRoomLengthMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblRoomLengthMin.Text = "Min Room Length:";
                lblRoomLengthMin.AutoSize = true;
                lblRoomLengthMin.Location = new Point(140, 116);

                nudRoomLengthMin = new NumericUpDown("nudRoomLengthMin");
                nudRoomLengthMin.Minimum = 0;
                nudRoomLengthMin.Maximum = 48;
                nudRoomLengthMin.Size = new Size(80, 20);
                nudRoomLengthMin.Location = new Point(140, 130);
                nudRoomLengthMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblRoomLengthMax = new Label("lblRoomLengthMax");
                lblRoomLengthMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblRoomLengthMax.Text = "Max Room Length:";
                lblRoomLengthMax.AutoSize = true;
                lblRoomLengthMax.Location = new Point(140, 160);

                nudRoomLengthMax = new NumericUpDown("nudRoomLengthMax");
                nudRoomLengthMax.Minimum = 0;
                nudRoomLengthMax.Maximum = 48;
                nudRoomLengthMax.Size = new Size(80, 20);
                nudRoomLengthMax.Location = new Point(140, 174);
                nudRoomLengthMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallTurnMin = new Label("lblHallTurnMin");
                lblHallTurnMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallTurnMin.Text = "Min Hall Turns:";
                lblHallTurnMin.AutoSize = true;
                lblHallTurnMin.Location = new Point(270, 28);

                nudHallTurnMin = new NumericUpDown("nudHallTurnMin");
                nudHallTurnMin.Minimum = 0;
                nudHallTurnMin.Maximum = 48;
                nudHallTurnMin.Size = new Size(80, 20);
                nudHallTurnMin.Location = new Point(270, 42);
                nudHallTurnMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallTurnMax = new Label("lblHallTurnMax");
                lblHallTurnMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallTurnMax.Text = "Max Hall Turns:";
                lblHallTurnMax.AutoSize = true;
                lblHallTurnMax.Location = new Point(270, 72);

                nudHallTurnMax = new NumericUpDown("nudHallTurnMax");
                nudHallTurnMax.Minimum = 0;
                nudHallTurnMax.Maximum = 48;
                nudHallTurnMax.Size = new Size(80, 20);
                nudHallTurnMax.Location = new Point(270, 86);
                nudHallTurnMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallVarMin = new Label("lblHallVarMin");
                lblHallVarMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallVarMin.Text = "Min Hall Variation:";
                lblHallVarMin.AutoSize = true;
                lblHallVarMin.Location = new Point(270, 116);

                nudHallVarMin = new NumericUpDown("nudHallVarMin");
                nudHallVarMin.Minimum = 0;
                nudHallVarMin.Maximum = 48;
                nudHallVarMin.Size = new Size(80, 20);
                nudHallVarMin.Location = new Point(270, 130);
                nudHallVarMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallVarMax = new Label("lblHallVarMax");
                lblHallVarMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallVarMax.Text = "Max Hall Variation:";
                lblHallVarMax.AutoSize = true;
                lblHallVarMax.Location = new Point(270, 160);

                nudHallVarMax = new NumericUpDown("nudHallVarMax");
                nudHallVarMax.Minimum = 0;
                nudHallVarMax.Maximum = 48;
                nudHallVarMax.Size = new Size(80, 20);
                nudHallVarMax.Location = new Point(270, 174);
                nudHallVarMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterFrequency = new Label("lblWaterFrequency");
                lblWaterFrequency.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterFrequency.Text = "Water Frequency:";
                lblWaterFrequency.AutoSize = true;
                lblWaterFrequency.Location = new Point(400, 28);

                nudWaterFrequency = new NumericUpDown("nudWaterFrequency");
                nudWaterFrequency.Minimum = 0;
                nudWaterFrequency.Maximum = 100;
                nudWaterFrequency.Size = new Size(80, 20);
                nudWaterFrequency.Location = new Point(400, 42);
                nudWaterFrequency.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblCraters = new Label("lblCraters");
                lblCraters.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblCraters.Text = "Craters:";
                lblCraters.AutoSize = true;
                lblCraters.Location = new Point(400, 72);

                nudCraters = new NumericUpDown("nudCraters");
                nudCraters.Minimum = 0;
                nudCraters.Maximum = 20;
                nudCraters.Size = new Size(80, 20);
                nudCraters.Location = new Point(400, 86);
                nudCraters.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblCraterMinLength = new Label("lblCraterMinLength");
                lblCraterMinLength.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblCraterMinLength.Text = "Min Crater Length:";
                lblCraterMinLength.AutoSize = true;
                lblCraterMinLength.Location = new Point(400, 116);

                nudCraterMinLength = new NumericUpDown("nudCraterMinLength");
                nudCraterMinLength.Minimum = 0;
                nudCraterMinLength.Maximum = 100;
                nudCraterMinLength.Size = new Size(80, 20);
                nudCraterMinLength.Location = new Point(400, 130);
                nudCraterMinLength.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblCraterMaxLength = new Label("lblCraterMaxLength");
                lblCraterMaxLength.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblCraterMaxLength.Text = "Max Crater Length:";
                lblCraterMaxLength.AutoSize = true;
                lblCraterMaxLength.Location = new Point(400, 160);

                nudCraterMaxLength = new NumericUpDown("nudCraterMaxLength");
                nudCraterMaxLength.Minimum = 0;
                nudCraterMaxLength.Maximum = 100;
                nudCraterMaxLength.Size = new Size(80, 20);
                nudCraterMaxLength.Location = new Point(400, 174);
                nudCraterMaxLength.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                chkCraterFuzzy = new CheckBox("chkCraterFuzzy");
                chkCraterFuzzy.Size = new Size(100, 17);
                chkCraterFuzzy.Location = new Point(400, 204);
                chkCraterFuzzy.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkCraterFuzzy.Text = "Fuzzy Craters";
                #endregion
                #region Added to pnlRDungeonStructure
                pnlRDungeonStructure.AddWidget(lblStructure);
                pnlRDungeonStructure.AddWidget(lblTrapMin);
                pnlRDungeonStructure.AddWidget(nudTrapMin);
                pnlRDungeonStructure.AddWidget(lblTrapMax);
                pnlRDungeonStructure.AddWidget(nudTrapMax);
                pnlRDungeonStructure.AddWidget(lblItemMin);
                pnlRDungeonStructure.AddWidget(nudItemMin);
                pnlRDungeonStructure.AddWidget(lblItemMax);
                pnlRDungeonStructure.AddWidget(nudItemMax);
                pnlRDungeonStructure.AddWidget(lblRoomWidthMin);
                pnlRDungeonStructure.AddWidget(nudRoomWidthMin);
                pnlRDungeonStructure.AddWidget(lblRoomWidthMax);
                pnlRDungeonStructure.AddWidget(nudRoomWidthMax);
                pnlRDungeonStructure.AddWidget(lblRoomLengthMin);
                pnlRDungeonStructure.AddWidget(nudRoomLengthMin);
                pnlRDungeonStructure.AddWidget(lblRoomLengthMax);
                pnlRDungeonStructure.AddWidget(nudRoomLengthMax);
                pnlRDungeonStructure.AddWidget(lblHallTurnMin);
                pnlRDungeonStructure.AddWidget(nudHallTurnMin);
                pnlRDungeonStructure.AddWidget(lblHallTurnMax);
                pnlRDungeonStructure.AddWidget(nudHallTurnMax);
                pnlRDungeonStructure.AddWidget(lblHallVarMin);
                pnlRDungeonStructure.AddWidget(nudHallVarMin);
                pnlRDungeonStructure.AddWidget(lblHallVarMax);
                pnlRDungeonStructure.AddWidget(nudHallVarMax);
                pnlRDungeonStructure.AddWidget(lblWaterFrequency);
                pnlRDungeonStructure.AddWidget(nudWaterFrequency);
                pnlRDungeonStructure.AddWidget(lblCraters);
                pnlRDungeonStructure.AddWidget(nudCraters);
                pnlRDungeonStructure.AddWidget(lblCraterMinLength);
                pnlRDungeonStructure.AddWidget(nudCraterMinLength);
                pnlRDungeonStructure.AddWidget(lblCraterMaxLength);
                pnlRDungeonStructure.AddWidget(nudCraterMaxLength);
                pnlRDungeonStructure.AddWidget(chkCraterFuzzy);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonStructure);
                structureLoaded = true;
            }
        }

        void LoadpnlRDungeonLandTiles() {
            if (!landTilesLoaded) {
                pnlRDungeonLandTiles = new Panel("pnlRDungeonLandTiles");
                pnlRDungeonLandTiles.Size = new Size(600, 320);
                pnlRDungeonLandTiles.Location = new Point(0, 80);
                pnlRDungeonLandTiles.BackColor = Color.LightGray;
                pnlRDungeonLandTiles.Visible = false;
                #region Land Tiles
                //Label lblLandTiles;
                lblLandTiles = new Label("lblLandTiles");
                lblLandTiles.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblLandTiles.Text = "Land Tiles Settings";
                lblLandTiles.AutoSize = true;
                lblLandTiles.Location = new Point(10, 4);


                lblLandTileset = new Label[22];
                picLandTileset = new PictureBox[22];
                for (int i = 0; i < 22; i++) {
                    lblLandTileset[i] = new Label("lblLandTileset" + i);
                    lblLandTileset[i].Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                    lblLandTileset[i].AutoSize = true;
                    if (i < 16) {
                        lblLandTileset[i].Location = new Point(2 + 70 * (i / 4), 30 + 70 * (i % 4));
                    } else {
                        lblLandTileset[i].Location = new Point(352 + 70 * ((i - 16) / 3), 30 + 70 * ((i - 16) % 3));
                    }

                    picLandTileset[i] = new PictureBox("picLandTileset" + i);
                    picLandTileset[i].Size = new Size(32, 32);
                    picLandTileset[i].BackColor = Color.Transparent;
                    picLandTileset[i].Image = new SdlDotNet.Graphics.Surface(32, 32);
                    picLandTileset[i].Click += new EventHandler<MouseButtonEventArgs>(picLandTileset_Click);

                    if (i < 16) {
                        picLandTileset[i].Location = new Point(10 + 70 * (i / 4), 46 + 70 * (i % 4));
                    } else {
                        picLandTileset[i].Location = new Point(360 + 70 * ((i - 16) / 3), 46 + 70 * ((i - 16) % 3));
                    }

                    pnlRDungeonLandTiles.AddWidget(lblLandTileset[i]);
                    pnlRDungeonLandTiles.AddWidget(picLandTileset[i]);
                }

                lblLandTileset[19].Text = "Stairs";
                lblLandTileset[16].Text = "Ground";
                lblLandTileset[10].Text = "Top Left";
                lblLandTileset[6].Text = "Top Center";
                lblLandTileset[2].Text = "Top Right";
                lblLandTileset[9].Text = "Center Left";
                lblLandTileset[5].Text = "Center Center";
                lblLandTileset[1].Text = "Center Right";
                lblLandTileset[8].Text = "Bottom Left";
                lblLandTileset[4].Text = "Bottom Center";
                lblLandTileset[0].Text = "Bottom Right";
                lblLandTileset[17].Text = "Inner Top Left";
                lblLandTileset[20].Text = "Inner Top Right";
                lblLandTileset[18].Text = "Inner Bottom Left";
                lblLandTileset[21].Text = "Inner Bottom Right";
                lblLandTileset[15].Text = "Isolated Wall";
                lblLandTileset[12].Text = "Column Top";
                lblLandTileset[13].Text = "Column Center";
                lblLandTileset[14].Text = "Column Bottom";
                lblLandTileset[3].Text = "Row Left";
                lblLandTileset[7].Text = "Row Center";
                lblLandTileset[11].Text = "Row Right";

                btnLandAltSwitch = new Button("btnLandAltSwitch");
                btnLandAltSwitch.Location = new Point(520, 280);
                btnLandAltSwitch.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLandAltSwitch.Size = new System.Drawing.Size(64, 16);
                btnLandAltSwitch.Text = "Alt ->";
                btnLandAltSwitch.Click += new EventHandler<MouseButtonEventArgs>(btnLandAltSwitch_Click);

                #endregion
                #region Added to pnlRDungeonLandTiles
                pnlRDungeonLandTiles.AddWidget(lblLandTiles);
                landTileNumbers = new int[2, 22];
                pnlRDungeonLandTiles.AddWidget(btnLandAltSwitch);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonLandTiles);
                landTilesLoaded = true;
            }
        }

        void LoadpnlRDungeonLandAltTiles() {
            if (!landAltTilesLoaded) {
                pnlRDungeonLandAltTiles = new Panel("pnlRDungeonLandAltTiles");
                pnlRDungeonLandAltTiles.Size = new Size(600, 320);
                pnlRDungeonLandAltTiles.Location = new Point(0, 80);
                pnlRDungeonLandAltTiles.BackColor = Color.LightGray;
                pnlRDungeonLandAltTiles.Visible = false;
                #region Land Alt Tiles
                //Label lblLandTiles;
                lblLandAltTiles = new Label("lblLandAltTiles");
                lblLandAltTiles.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblLandAltTiles.Text = "Land Alt Tiles Settings";
                lblLandAltTiles.AutoSize = true;
                lblLandAltTiles.Location = new Point(10, 4);


                lblLandAltTileset = new Label[23];
                picLandAltTileset = new PictureBox[23];
                for (int i = 0; i < 23; i++) {
                    lblLandAltTileset[i] = new Label("lblLandAltTileset" + i);
                    lblLandAltTileset[i].Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                    lblLandAltTileset[i].AutoSize = true;
                    if (i == 5) {
                        lblLandAltTileset[i].Location = new Point(352, 240);
                    } else if (i == 22) {
                        lblLandAltTileset[i].Location = new Point(422, 240);
                    } else if (i < 16) {
                        lblLandAltTileset[i].Location = new Point(2 + 70 * (i / 4), 30 + 70 * (i % 4));
                    } else {
                        lblLandAltTileset[i].Location = new Point(352 + 70 * ((i - 16) / 3), 30 + 70 * ((i - 16) % 3));
                    }

                    picLandAltTileset[i] = new PictureBox("picLandAltTileset" + i);
                    picLandAltTileset[i].Size = new Size(32, 32);
                    picLandAltTileset[i].BackColor = Color.Transparent;
                    picLandAltTileset[i].Image = new SdlDotNet.Graphics.Surface(32, 32);
                    picLandAltTileset[i].Click += new EventHandler<MouseButtonEventArgs>(picLandAltTileset_Click);

                    if (i == 5) {
                        picLandAltTileset[i].Location = new Point(360, 256);
                    } else if (i == 22) {
                        picLandAltTileset[i].Location = new Point(430, 256);
                    } else if (i < 16) {
                        picLandAltTileset[i].Location = new Point(10 + 70 * (i / 4), 46 + 70 * (i % 4));
                    } else {
                        picLandAltTileset[i].Location = new Point(360 + 70 * ((i - 16) / 3), 46 + 70 * ((i - 16) % 3));
                    }

                    pnlRDungeonLandAltTiles.AddWidget(lblLandAltTileset[i]);
                    pnlRDungeonLandAltTiles.AddWidget(picLandAltTileset[i]);
                }

                lblLandAltTileset[19].Text = "Ground2";
                lblLandAltTileset[16].Text = "Ground";
                lblLandAltTileset[10].Text = "Top Left";
                lblLandAltTileset[6].Text = "Top Center";
                lblLandAltTileset[2].Text = "Top Right";
                lblLandAltTileset[9].Text = "Center Left";
                lblLandAltTileset[5].Text = "Center Center";
                lblLandAltTileset[22].Text = "Center Center2";
                lblLandAltTileset[1].Text = "Center Right";
                lblLandAltTileset[8].Text = "Bottom Left";
                lblLandAltTileset[4].Text = "Bottom Center";
                lblLandAltTileset[0].Text = "Bottom Right";
                lblLandAltTileset[17].Text = "Inner Top Left";
                lblLandAltTileset[20].Text = "Inner Top Right";
                lblLandAltTileset[18].Text = "Inner Bottom Left";
                lblLandAltTileset[21].Text = "Inner Bottom Right";
                lblLandAltTileset[15].Text = "Isolated Wall";
                lblLandAltTileset[12].Text = "Column Top";
                lblLandAltTileset[13].Text = "Column Center";
                lblLandAltTileset[14].Text = "Column Bottom";
                lblLandAltTileset[3].Text = "Row Left";
                lblLandAltTileset[7].Text = "Row Center";
                lblLandAltTileset[11].Text = "Row Right";

                btnLandSwitch = new Button("btnLandSwitch");
                btnLandSwitch.Location = new Point(520, 280);
                btnLandSwitch.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLandSwitch.Size = new System.Drawing.Size(64, 16);
                btnLandSwitch.Text = "Normal ->";
                btnLandSwitch.Click += new EventHandler<MouseButtonEventArgs>(btnLandSwitch_Click);

                #endregion
                #region Added to pnlRDungeonLandAltTiles
                pnlRDungeonLandAltTiles.AddWidget(lblLandAltTiles);
                landAltTileNumbers = new int[2, 23];
                pnlRDungeonLandAltTiles.AddWidget(btnLandSwitch);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonLandAltTiles);
                landAltTilesLoaded = true;
            }
        }

        void LoadpnlRDungeonWaterTiles() {
            if (!waterTilesLoaded) {
                pnlRDungeonWaterTiles = new Panel("pnlRDungeonWaterTiles");
                pnlRDungeonWaterTiles.Size = new Size(600, 320);
                pnlRDungeonWaterTiles.Location = new Point(0, 80);
                pnlRDungeonWaterTiles.BackColor = Color.LightGray;
                pnlRDungeonWaterTiles.Visible = false;
                #region Water Tiles
                //Label lblWaterTiles;
                lblWaterTiles = new Label("lblWaterTiles");
                lblWaterTiles.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterTiles.Text = "WaterTiles Settings";
                lblWaterTiles.AutoSize = true;
                lblWaterTiles.Location = new Point(10, 4);

                lblWaterTileset = new Label[22];
                picWaterTileset = new PictureBox[22];
                for (int i = 0; i < 22; i++) {
                    lblWaterTileset[i] = new Label("lblWaterTileset" + i);
                    lblWaterTileset[i].Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                    lblWaterTileset[i].AutoSize = true;
                    if (i < 16) {
                        lblWaterTileset[i].Location = new Point(2 + 70 * (i / 4), 30 + 70 * (i % 4));
                    } else {
                        lblWaterTileset[i].Location = new Point(352 + 70 * ((i - 16) / 3), 30 + 70 * ((i - 16) % 3));
                    }

                    picWaterTileset[i] = new PictureBox("picWaterTileset" + i);
                    picWaterTileset[i].Size = new Size(32, 32);
                    picWaterTileset[i].BackColor = Color.Transparent;
                    picWaterTileset[i].Image = new SdlDotNet.Graphics.Surface(32, 32);
                    picWaterTileset[i].Click += new EventHandler<MouseButtonEventArgs>(picWaterTileset_Click);

                    if (i < 16) {
                        picWaterTileset[i].Location = new Point(10 + 70 * (i / 4), 46 + 70 * (i % 4));
                    } else {
                        picWaterTileset[i].Location = new Point(360 + 70 * ((i - 16) / 3), 46 + 70 * ((i - 16) % 3));
                    }

                    pnlRDungeonWaterTiles.AddWidget(lblWaterTileset[i]);
                    pnlRDungeonWaterTiles.AddWidget(picWaterTileset[i]);
                }

                lblWaterTileset[15].Text = "Surrounded";
                lblWaterTileset[0].Text = "InnerTopLeft";
                lblWaterTileset[4].Text = "Top";
                lblWaterTileset[8].Text = "InnerTopRight";
                lblWaterTileset[1].Text = "Left";
                lblWaterTileset[5].Text = "Water";
                lblWaterTileset[9].Text = "Right";
                lblWaterTileset[2].Text = "InnerBottomLeft";
                lblWaterTileset[6].Text = "Bottom";
                lblWaterTileset[10].Text = "InnerBottomRight";
                lblWaterTileset[16].Text = "TopLeft";
                lblWaterTileset[19].Text = "TopRight";
                lblWaterTileset[17].Text = "BottomLeft";
                lblWaterTileset[20].Text = "BottomRight";
                lblWaterTileset[18].Text = "DiagonalForward";
                lblWaterTileset[21].Text = "DiagonalBack";
                lblWaterTileset[12].Text = "InnerTop";
                lblWaterTileset[13].Text = "Vertical";
                lblWaterTileset[14].Text = "InnerBottom";
                lblWaterTileset[3].Text = "InnerLeft";
                lblWaterTileset[7].Text = "Horizontal";
                lblWaterTileset[11].Text = "InnerRight";

                btnWaterAnimSwitch = new Button("btnWaterAnimSwitch");
                btnWaterAnimSwitch.Location = new Point(520, 280);
                btnWaterAnimSwitch.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnWaterAnimSwitch.Size = new System.Drawing.Size(64, 16);
                btnWaterAnimSwitch.Text = "Anim ->";
                btnWaterAnimSwitch.Click += new EventHandler<MouseButtonEventArgs>(btnWaterAnimSwitch_Click);

                #endregion
                #region Added to pnlRDungeonWaterTiles
                pnlRDungeonWaterTiles.AddWidget(lblWaterTiles);
                waterTileNumbers = new int[2, 22];
                pnlRDungeonWaterTiles.AddWidget(btnWaterAnimSwitch);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonWaterTiles);
                waterTilesLoaded = true;
            }
        }

        void LoadpnlRDungeonWaterAnimTiles() {
            if (!waterAnimTilesLoaded) {
                pnlRDungeonWaterAnimTiles = new Panel("pnlRDungeonWaterAnimTiles");
                pnlRDungeonWaterAnimTiles.Size = new Size(600, 320);
                pnlRDungeonWaterAnimTiles.Location = new Point(0, 80);
                pnlRDungeonWaterAnimTiles.BackColor = Color.LightGray;
                pnlRDungeonWaterAnimTiles.Visible = false;
                #region Water Anim Tiles
                //Label lblWaterAnimTiles;
                lblWaterAnimTiles = new Label("lblWaterAnimTiles");
                lblWaterAnimTiles.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterAnimTiles.Text = "WaterAnimTiles Settings";
                lblWaterAnimTiles.AutoSize = true;
                lblWaterAnimTiles.Location = new Point(10, 4);

                lblWaterAnimTileset = new Label[22];
                picWaterAnimTileset = new PictureBox[22];
                for (int i = 0; i < 22; i++) {
                    lblWaterAnimTileset[i] = new Label("lblWaterAnimTileset" + i);
                    lblWaterAnimTileset[i].Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                    lblWaterAnimTileset[i].AutoSize = true;
                    if (i < 16) {
                        lblWaterAnimTileset[i].Location = new Point(2 + 70 * (i / 4), 30 + 70 * (i % 4));
                    } else {
                        lblWaterAnimTileset[i].Location = new Point(352 + 70 * ((i - 16) / 3), 30 + 70 * ((i - 16) % 3));
                    }

                    picWaterAnimTileset[i] = new PictureBox("picWaterAnimTileset" + i);
                    picWaterAnimTileset[i].Size = new Size(32, 32);
                    picWaterAnimTileset[i].BackColor = Color.Transparent;
                    picWaterAnimTileset[i].Image = new SdlDotNet.Graphics.Surface(32, 32);
                    picWaterAnimTileset[i].Click += new EventHandler<MouseButtonEventArgs>(picWaterAnimTileset_Click);

                    if (i < 16) {
                        picWaterAnimTileset[i].Location = new Point(10 + 70 * (i / 4), 46 + 70 * (i % 4));
                    } else {
                        picWaterAnimTileset[i].Location = new Point(360 + 70 * ((i - 16) / 3), 46 + 70 * ((i - 16) % 3));
                    }

                    pnlRDungeonWaterAnimTiles.AddWidget(lblWaterAnimTileset[i]);
                    pnlRDungeonWaterAnimTiles.AddWidget(picWaterAnimTileset[i]);
                }

                lblWaterAnimTileset[15].Text = "Surrounded";
                lblWaterAnimTileset[0].Text = "InnerTopLeft";
                lblWaterAnimTileset[4].Text = "Top";
                lblWaterAnimTileset[8].Text = "InnerTopRight";
                lblWaterAnimTileset[1].Text = "Left";
                lblWaterAnimTileset[5].Text = "Water";
                lblWaterAnimTileset[9].Text = "Right";
                lblWaterAnimTileset[2].Text = "InnerBottomLeft";
                lblWaterAnimTileset[6].Text = "Bottom";
                lblWaterAnimTileset[10].Text = "InnerBottomRight";
                lblWaterAnimTileset[16].Text = "TopLeft";
                lblWaterAnimTileset[19].Text = "TopRight";
                lblWaterAnimTileset[17].Text = "BottomLeft";
                lblWaterAnimTileset[20].Text = "BottomRight";
                lblWaterAnimTileset[18].Text = "DiagonalForward";
                lblWaterAnimTileset[21].Text = "DiagonalBack";
                lblWaterAnimTileset[12].Text = "InnerTop";
                lblWaterAnimTileset[13].Text = "Vertical";
                lblWaterAnimTileset[14].Text = "InnerBottom";
                lblWaterAnimTileset[3].Text = "InnerLeft";
                lblWaterAnimTileset[7].Text = "Horizontal";
                lblWaterAnimTileset[11].Text = "InnerRight";

                btnWaterSwitch = new Button("btnWaterSwitch");
                btnWaterSwitch.Location = new Point(520, 280);
                btnWaterSwitch.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnWaterSwitch.Size = new System.Drawing.Size(64, 16);
                btnWaterSwitch.Text = "Normal ->";
                btnWaterSwitch.Click += new EventHandler<MouseButtonEventArgs>(btnWaterSwitch_Click);

                #endregion
                #region Added to pnlRDungeonWaterAnimTiles
                pnlRDungeonWaterAnimTiles.AddWidget(lblWaterAnimTiles);
                waterAnimTileNumbers = new int[2, 22];
                pnlRDungeonWaterAnimTiles.AddWidget(btnWaterSwitch);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonWaterAnimTiles);
                waterAnimTilesLoaded = true;
            }
        }

        void LoadpnlRDungeonAttributes() {
            if (!attributesLoaded) {
                pnlRDungeonAttributes = new Panel("pnlRDungeonAttributes");
                pnlRDungeonAttributes.Size = new Size(600, 320);
                pnlRDungeonAttributes.Location = new Point(0, 80);
                pnlRDungeonAttributes.BackColor = Color.LightGray;
                pnlRDungeonAttributes.Visible = false;
                #region Attributes
                lblAttributes = new Label("lblAttributes");
                lblAttributes.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblAttributes.Text = "Attributes Settings";
                lblAttributes.AutoSize = true;
                lblAttributes.Location = new Point(10, 4);

                cbGroundType = new ComboBox("cbGroundType");
                cbGroundType.Location = new Point(10, 26);
                cbGroundType.Size = new Size(100, 16);
                for (int i = 0; i < 40; i++) {
                    cbGroundType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.TileType), i)));
                }
                cbGroundType.SelectItem(0);

                lblGroundData1 = new Label("lblGroundData1");
                lblGroundData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundData1.Text = "Ground Data1:";
                lblGroundData1.AutoSize = true;
                lblGroundData1.Location = new Point(10, 52);

                nudGroundData1 = new NumericUpDown("nudGroundData1");
                nudGroundData1.Minimum = Int32.MinValue;
                nudGroundData1.Maximum = Int32.MaxValue;
                nudGroundData1.Size = new Size(80, 20);
                nudGroundData1.Location = new Point(10, 66);
                nudGroundData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblGroundData2 = new Label("lblGroundData2");
                lblGroundData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundData2.Text = "Ground Data2:";
                lblGroundData2.AutoSize = true;
                lblGroundData2.Location = new Point(10, 92);

                nudGroundData2 = new NumericUpDown("nudGroundData2");
                nudGroundData2.Minimum = Int32.MinValue;
                nudGroundData2.Maximum = Int32.MaxValue;
                nudGroundData2.Size = new Size(80, 20);
                nudGroundData2.Location = new Point(10, 106);
                nudGroundData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblGroundData3 = new Label("lblGroundData3");
                lblGroundData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundData3.Text = "Ground Data3:";
                lblGroundData3.AutoSize = true;
                lblGroundData3.Location = new Point(10, 132);

                nudGroundData3 = new NumericUpDown("nudGroundData3");
                nudGroundData3.Minimum = Int32.MinValue;
                nudGroundData3.Maximum = Int32.MaxValue;
                nudGroundData3.Size = new Size(80, 20);
                nudGroundData3.Location = new Point(10, 146);
                nudGroundData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblGroundString1 = new Label("lblGroundString1");
                lblGroundString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundString1.Text = "Ground String1:";
                lblGroundString1.AutoSize = true;
                lblGroundString1.Location = new Point(10, 172);

                txtGroundString1 = new TextBox("txtGroundString1");
                txtGroundString1.BackColor = Color.White;
                txtGroundString1.Size = new Size(80, 20);
                txtGroundString1.Location = new Point(10, 186);
                txtGroundString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblGroundString2 = new Label("lblGroundString2");
                lblGroundString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundString2.Text = "Ground String2:";
                lblGroundString2.AutoSize = true;
                lblGroundString2.Location = new Point(10, 212);

                txtGroundString2 = new TextBox("txtGroundString2");
                txtGroundString2.BackColor = Color.White;
                txtGroundString2.Size = new Size(80, 20);
                txtGroundString2.Location = new Point(10, 226);
                txtGroundString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblGroundString3 = new Label("lblGroundString3");
                lblGroundString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGroundString3.Text = "Ground String3:";
                lblGroundString3.AutoSize = true;
                lblGroundString3.Location = new Point(10, 252);

                txtGroundString3 = new TextBox("txtGroundString3");
                txtGroundString3.BackColor = Color.White;
                txtGroundString3.Size = new Size(80, 20);
                txtGroundString3.Location = new Point(10, 266);
                txtGroundString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);



                cbHallType = new ComboBox("cbHallType");
                cbHallType.Location = new Point(120, 26);
                cbHallType.Size = new Size(100, 16);
                for (int i = 0; i < 40; i++) {
                    cbHallType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.TileType), i)));
                }
                cbHallType.SelectItem(0);

                lblHallData1 = new Label("lblHallData1");
                lblHallData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallData1.Text = "Hall Data1:";
                lblHallData1.AutoSize = true;
                lblHallData1.Location = new Point(120, 52);

                nudHallData1 = new NumericUpDown("nudHallData1");
                nudHallData1.Minimum = Int32.MinValue;
                nudHallData1.Maximum = Int32.MaxValue;
                nudHallData1.Size = new Size(80, 20);
                nudHallData1.Location = new Point(120, 66);
                nudHallData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallData2 = new Label("lblHallData2");
                lblHallData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallData2.Text = "Hall Data2:";
                lblHallData2.AutoSize = true;
                lblHallData2.Location = new Point(120, 92);

                nudHallData2 = new NumericUpDown("nudHallData2");
                nudHallData2.Minimum = Int32.MinValue;
                nudHallData2.Maximum = Int32.MaxValue;
                nudHallData2.Size = new Size(80, 20);
                nudHallData2.Location = new Point(120, 106);
                nudHallData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallData3 = new Label("lblHallData3");
                lblHallData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallData3.Text = "Hall Data3:";
                lblHallData3.AutoSize = true;
                lblHallData3.Location = new Point(120, 132);

                nudHallData3 = new NumericUpDown("nudHallData3");
                nudHallData3.Minimum = Int32.MinValue;
                nudHallData3.Maximum = Int32.MaxValue;
                nudHallData3.Size = new Size(80, 20);
                nudHallData3.Location = new Point(120, 146);
                nudHallData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallString1 = new Label("lblHallString1");
                lblHallString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallString1.Text = "Hall String1:";
                lblHallString1.AutoSize = true;
                lblHallString1.Location = new Point(120, 172);

                txtHallString1 = new TextBox("txtHallString1");
                txtHallString1.BackColor = Color.White;
                txtHallString1.Size = new Size(80, 20);
                txtHallString1.Location = new Point(120, 186);
                txtHallString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallString2 = new Label("lblHallString2");
                lblHallString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallString2.Text = "Hall String2:";
                lblHallString2.AutoSize = true;
                lblHallString2.Location = new Point(120, 212);

                txtHallString2 = new TextBox("txtHallString2");
                txtHallString2.BackColor = Color.White;
                txtHallString2.Size = new Size(80, 20);
                txtHallString2.Location = new Point(120, 226);
                txtHallString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblHallString3 = new Label("lblHallString3");
                lblHallString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblHallString3.Text = "Hall String3:";
                lblHallString3.AutoSize = true;
                lblHallString3.Location = new Point(120, 252);

                txtHallString3 = new TextBox("txtHallString3");
                txtHallString3.BackColor = Color.White;
                txtHallString3.Size = new Size(80, 20);
                txtHallString3.Location = new Point(120, 266);
                txtHallString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);



                cbWaterType = new ComboBox("cbWaterType");
                cbWaterType.Location = new Point(230, 26);
                cbWaterType.Size = new Size(100, 16);
                for (int i = 0; i < 40; i++) {
                    cbWaterType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.TileType), i)));
                }
                cbWaterType.SelectItem(0);

                lblWaterData1 = new Label("lblWaterData1");
                lblWaterData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterData1.Text = "Water Data1:";
                lblWaterData1.AutoSize = true;
                lblWaterData1.Location = new Point(230, 52);

                nudWaterData1 = new NumericUpDown("nudWaterData1");
                nudWaterData1.Minimum = Int32.MinValue;
                nudWaterData1.Maximum = Int32.MaxValue;
                nudWaterData1.Size = new Size(80, 20);
                nudWaterData1.Location = new Point(230, 66);
                nudWaterData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterData2 = new Label("lblWaterData2");
                lblWaterData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterData2.Text = "Water Data2:";
                lblWaterData2.AutoSize = true;
                lblWaterData2.Location = new Point(230, 92);

                nudWaterData2 = new NumericUpDown("nudWaterData2");
                nudWaterData2.Minimum = Int32.MinValue;
                nudWaterData2.Maximum = Int32.MaxValue;
                nudWaterData2.Size = new Size(80, 20);
                nudWaterData2.Location = new Point(230, 106);
                nudWaterData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterData3 = new Label("lblWaterData3");
                lblWaterData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterData3.Text = "Water Data3:";
                lblWaterData3.AutoSize = true;
                lblWaterData3.Location = new Point(230, 132);

                nudWaterData3 = new NumericUpDown("nudWaterData3");
                nudWaterData3.Minimum = Int32.MinValue;
                nudWaterData3.Maximum = Int32.MaxValue;
                nudWaterData3.Size = new Size(80, 20);
                nudWaterData3.Location = new Point(230, 146);
                nudWaterData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterString1 = new Label("lblWaterString1");
                lblWaterString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterString1.Text = "Water String1:";
                lblWaterString1.AutoSize = true;
                lblWaterString1.Location = new Point(230, 172);

                txtWaterString1 = new TextBox("txtWaterString1");
                txtWaterString1.BackColor = Color.White;
                txtWaterString1.Size = new Size(80, 20);
                txtWaterString1.Location = new Point(230, 186);
                txtWaterString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterString2 = new Label("lblWaterString2");
                lblWaterString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterString2.Text = "Water String2:";
                lblWaterString2.AutoSize = true;
                lblWaterString2.Location = new Point(230, 212);

                txtWaterString2 = new TextBox("txtWaterString2");
                txtWaterString2.BackColor = Color.White;
                txtWaterString2.Size = new Size(80, 20);
                txtWaterString2.Location = new Point(230, 226);
                txtWaterString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWaterString3 = new Label("lblWaterString3");
                lblWaterString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWaterString3.Text = "Water String3:";
                lblWaterString3.AutoSize = true;
                lblWaterString3.Location = new Point(230, 252);

                txtWaterString3 = new TextBox("txtWaterString3");
                txtWaterString3.BackColor = Color.White;
                txtWaterString3.Size = new Size(80, 20);
                txtWaterString3.Location = new Point(230, 266);
                txtWaterString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);



                cbWallType = new ComboBox("cbWallType");
                cbWallType.Location = new Point(340, 26);
                cbWallType.Size = new Size(100, 16);
                for (int i = 0; i < 40; i++) {
                    cbWallType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.TileType), i)));
                }
                cbWallType.SelectItem(0);

                lblWallData1 = new Label("lblWallData1");
                lblWallData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallData1.Text = "Wall Data1:";
                lblWallData1.AutoSize = true;
                lblWallData1.Location = new Point(340, 52);

                nudWallData1 = new NumericUpDown("nudWallData1");
                nudWallData1.Minimum = Int32.MinValue;
                nudWallData1.Maximum = Int32.MaxValue;
                nudWallData1.Size = new Size(80, 20);
                nudWallData1.Location = new Point(340, 66);
                nudWallData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWallData2 = new Label("lblWallData2");
                lblWallData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallData2.Text = "Wall Data2:";
                lblWallData2.AutoSize = true;
                lblWallData2.Location = new Point(340, 92);

                nudWallData2 = new NumericUpDown("nudWallData2");
                nudWallData2.Minimum = Int32.MinValue;
                nudWallData2.Maximum = Int32.MaxValue;
                nudWallData2.Size = new Size(80, 20);
                nudWallData2.Location = new Point(340, 106);
                nudWallData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWallData3 = new Label("lblWallData3");
                lblWallData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallData3.Text = "Wall Data3:";
                lblWallData3.AutoSize = true;
                lblWallData3.Location = new Point(340, 132);

                nudWallData3 = new NumericUpDown("nudWallData3");
                nudWallData3.Minimum = Int32.MinValue;
                nudWallData3.Maximum = Int32.MaxValue;
                nudWallData3.Size = new Size(80, 20);
                nudWallData3.Location = new Point(340, 146);
                nudWallData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWallString1 = new Label("lblWallString1");
                lblWallString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallString1.Text = "Wall String1:";
                lblWallString1.AutoSize = true;
                lblWallString1.Location = new Point(340, 172);

                txtWallString1 = new TextBox("txtWallString1");
                txtWallString1.BackColor = Color.White;
                txtWallString1.Size = new Size(80, 20);
                txtWallString1.Location = new Point(340, 186);
                txtWallString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWallString2 = new Label("lblWallString2");
                lblWallString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallString2.Text = "Wall String2:";
                lblWallString2.AutoSize = true;
                lblWallString2.Location = new Point(340, 212);

                txtWallString2 = new TextBox("txtWallString2");
                txtWallString2.BackColor = Color.White;
                txtWallString2.Size = new Size(80, 20);
                txtWallString2.Location = new Point(340, 226);
                txtWallString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblWallString3 = new Label("lblWallString3");
                lblWallString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWallString3.Text = "Wall String3:";
                lblWallString3.AutoSize = true;
                lblWallString3.Location = new Point(340, 252);

                txtWallString3 = new TextBox("txtWallString3");
                txtWallString3.BackColor = Color.White;
                txtWallString3.Size = new Size(80, 20);
                txtWallString3.Location = new Point(340, 266);
                txtWallString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);


                #endregion
                #region Added to pnlRDungeonAttributes
                pnlRDungeonAttributes.AddWidget(lblAttributes);

                pnlRDungeonAttributes.AddWidget(cbGroundType);
                pnlRDungeonAttributes.AddWidget(lblGroundData1);
                pnlRDungeonAttributes.AddWidget(nudGroundData1);
                pnlRDungeonAttributes.AddWidget(lblGroundData2);
                pnlRDungeonAttributes.AddWidget(nudGroundData2);
                pnlRDungeonAttributes.AddWidget(lblGroundData3);
                pnlRDungeonAttributes.AddWidget(nudGroundData3);

                pnlRDungeonAttributes.AddWidget(lblGroundString1);
                pnlRDungeonAttributes.AddWidget(txtGroundString1);
                pnlRDungeonAttributes.AddWidget(lblGroundString2);
                pnlRDungeonAttributes.AddWidget(txtGroundString2);
                pnlRDungeonAttributes.AddWidget(lblGroundString3);
                pnlRDungeonAttributes.AddWidget(txtGroundString3);

                pnlRDungeonAttributes.AddWidget(cbHallType);
                pnlRDungeonAttributes.AddWidget(lblHallData1);
                pnlRDungeonAttributes.AddWidget(nudHallData1);
                pnlRDungeonAttributes.AddWidget(lblHallData2);
                pnlRDungeonAttributes.AddWidget(nudHallData2);
                pnlRDungeonAttributes.AddWidget(lblHallData3);
                pnlRDungeonAttributes.AddWidget(nudHallData3);

                pnlRDungeonAttributes.AddWidget(lblHallString1);
                pnlRDungeonAttributes.AddWidget(txtHallString1);
                pnlRDungeonAttributes.AddWidget(lblHallString2);
                pnlRDungeonAttributes.AddWidget(txtHallString2);
                pnlRDungeonAttributes.AddWidget(lblHallString3);
                pnlRDungeonAttributes.AddWidget(txtHallString3);

                pnlRDungeonAttributes.AddWidget(cbWaterType);
                pnlRDungeonAttributes.AddWidget(lblWaterData1);
                pnlRDungeonAttributes.AddWidget(nudWaterData1);
                pnlRDungeonAttributes.AddWidget(lblWaterData2);
                pnlRDungeonAttributes.AddWidget(nudWaterData2);
                pnlRDungeonAttributes.AddWidget(lblWaterData3);
                pnlRDungeonAttributes.AddWidget(nudWaterData3);

                pnlRDungeonAttributes.AddWidget(lblWaterString1);
                pnlRDungeonAttributes.AddWidget(txtWaterString1);
                pnlRDungeonAttributes.AddWidget(lblWaterString2);
                pnlRDungeonAttributes.AddWidget(txtWaterString2);
                pnlRDungeonAttributes.AddWidget(lblWaterString3);
                pnlRDungeonAttributes.AddWidget(txtWaterString3);

                pnlRDungeonAttributes.AddWidget(cbWallType);
                pnlRDungeonAttributes.AddWidget(lblWallData1);
                pnlRDungeonAttributes.AddWidget(nudWallData1);
                pnlRDungeonAttributes.AddWidget(lblWallData2);
                pnlRDungeonAttributes.AddWidget(nudWallData2);
                pnlRDungeonAttributes.AddWidget(lblWallData3);
                pnlRDungeonAttributes.AddWidget(nudWallData3);

                pnlRDungeonAttributes.AddWidget(lblWallString1);
                pnlRDungeonAttributes.AddWidget(txtWallString1);
                pnlRDungeonAttributes.AddWidget(lblWallString2);
                pnlRDungeonAttributes.AddWidget(txtWallString2);
                pnlRDungeonAttributes.AddWidget(lblWallString3);
                pnlRDungeonAttributes.AddWidget(txtWallString3);

                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonAttributes);
                attributesLoaded = true;
            }
        }

        void LoadpnlRDungeonItems() {
            if (!itemsLoaded) {
                pnlRDungeonItems = new Panel("pnlRDungeonItems");
                pnlRDungeonItems.Size = new Size(600, 320);
                pnlRDungeonItems.Location = new Point(0, 80);
                pnlRDungeonItems.BackColor = Color.LightGray;
                pnlRDungeonItems.Visible = false;
                #region Items
                //Label lblItems;
                lblItems = new Label("lblItems");
                lblItems.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblItems.Text = "Items Settings";
                lblItems.AutoSize = true;
                lblItems.Location = new Point(10, 4);
                //ListBox lbxItems;
                lbxItems = new ListBox("lbxItems");
                lbxItems.Location = new Point(10, 130);
                lbxItems.Size = new Size(580, 160);



                //Label lblItemNum;
                lblItemNum = new Label("lblItemNum");
                lblItemNum.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblItemNum.Text = "Item #";
                lblItemNum.AutoSize = true;
                lblItemNum.Location = new Point(10, 26);
                //NumericUpDown nudItemNum;
                nudItemNum = new NumericUpDown("nudItemNum");
                nudItemNum.Minimum = 1;
                nudItemNum.Maximum = MaxInfo.MaxItems;
                nudItemNum.Size = new Size(80, 20);
                nudItemNum.Location = new Point(10, 40);
                nudItemNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                nudItemNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudItemNum_ValueChanged);

                //Label lblItemSpawnRate;
                lblItemSpawnRate = new Label("lblItemSpawnRate");
                lblItemSpawnRate.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblItemSpawnRate.Text = "Spawn Rate:";
                lblItemSpawnRate.AutoSize = true;
                lblItemSpawnRate.Location = new Point(10, 66);
                //NumericUpDown nudItemSpawnRate;
                nudItemSpawnRate = new NumericUpDown("nudItemSpawnRate");
                nudItemSpawnRate.Minimum = 1;
                nudItemSpawnRate.Maximum = 100;
                nudItemSpawnRate.Size = new Size(80, 20);
                nudItemSpawnRate.Location = new Point(10, 80);
                nudItemSpawnRate.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblMinValue = new Label("lblMinValue");
                lblMinValue.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMinValue.Text = "Min Value";
                lblMinValue.AutoSize = true;
                lblMinValue.Location = new Point(120, 26);
                //NumericUpDown nudMinValue;
                nudMinValue = new NumericUpDown("nudMinValue");
                nudMinValue.Minimum = 1;
                nudMinValue.Maximum = Int32.MaxValue;
                nudMinValue.Size = new Size(80, 20);
                nudMinValue.Location = new Point(120, 40);
                nudMinValue.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblMaxValue;
                lblMaxValue = new Label("lblMaxValue");
                lblMaxValue.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMaxValue.Text = "Max Value";
                lblMaxValue.AutoSize = true;
                lblMaxValue.Location = new Point(120, 66);
                //NumericUpDown nudMaxValue;
                nudMaxValue = new NumericUpDown("nudMaxValue");
                nudMaxValue.Minimum = 1;
                nudMaxValue.Maximum = Int32.MaxValue;
                nudMaxValue.Size = new Size(80, 20);
                nudMaxValue.Location = new Point(120, 80);
                nudMaxValue.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblStickyRate;
                lblStickyRate = new Label("lblStickyRate");
                lblStickyRate.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblStickyRate.Text = "Sticky Rate";
                lblStickyRate.AutoSize = true;
                lblStickyRate.Location = new Point(230, 26);
                //NumericUpDown nudStickyRate;
                nudStickyRate = new NumericUpDown("nudStickyRate");
                nudStickyRate.Minimum = 0;
                nudStickyRate.Maximum = 100;
                nudStickyRate.Size = new Size(80, 20);
                nudStickyRate.Location = new Point(230, 40);
                nudStickyRate.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblTag;
                lblTag = new Label("lblTag");
                lblTag.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTag.Text = "Tag";
                lblTag.AutoSize = true;
                lblTag.Location = new Point(230, 66);
                //NumericUpDown txtTag;
                txtTag = new TextBox("nudTag");
                txtTag.BackColor = Color.White;
                txtTag.Size = new Size(80, 20);
                txtTag.Location = new Point(230, 80);
                txtTag.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                chkHidden = new CheckBox("chkHidden");
                chkHidden.Size = new Size(100, 17);
                chkHidden.Location = new Point(340, 26);
                chkHidden.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkHidden.Text = "Hidden";

                chkOnGround = new CheckBox("chkOnGround");
                chkOnGround.Size = new Size(100, 17);
                chkOnGround.Location = new Point(340, 66);
                chkOnGround.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkOnGround.Text = "On Ground";

                chkOnWater = new CheckBox("chkOnWater");
                chkOnWater.Size = new Size(100, 17);
                chkOnWater.Location = new Point(450, 26);
                chkOnWater.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkOnWater.Text = "On Water";

                chkOnWall = new CheckBox("chkOnWall");
                chkOnWall.Size = new Size(100, 17);
                chkOnWall.Location = new Point(450, 66);
                chkOnWall.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkOnWall.Text = "On Wall";

                //Button btnAddItem;
                btnAddItem = new Button("btnAddItem");
                btnAddItem.Location = new Point(10, 110);
                btnAddItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnAddItem.Size = new System.Drawing.Size(64, 16);
                btnAddItem.Visible = true;
                btnAddItem.Text = "Add Item";
                btnAddItem.Click += new EventHandler<MouseButtonEventArgs>(btnAddItem_Click);
                //Button btnRemoveItem;
                btnRemoveItem = new Button("btnRemoveItem");
                btnRemoveItem.Location = new Point(110, 110);
                btnRemoveItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnRemoveItem.Size = new System.Drawing.Size(64, 16);
                btnRemoveItem.Visible = true;
                btnRemoveItem.Text = "Remove Item";
                btnRemoveItem.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveItem_Click);
                //Button btnLoadItem;
                btnLoadItem = new Button("btnLoadItem");
                btnLoadItem.Location = new Point(210, 110);
                btnLoadItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLoadItem.Size = new System.Drawing.Size(64, 16);
                btnLoadItem.Visible = true;
                btnLoadItem.Text = "Load Item";
                btnLoadItem.Click += new EventHandler<MouseButtonEventArgs>(btnLoadItem_Click);
                //Button btnChangeItem;
                btnChangeItem = new Button("btnChangeItem");
                btnChangeItem.Location = new Point(310, 110);
                btnChangeItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnChangeItem.Size = new System.Drawing.Size(64, 16);
                btnChangeItem.Visible = true;
                btnChangeItem.Text = "Change Item";
                btnChangeItem.Click += new EventHandler<MouseButtonEventArgs>(btnChangeItem_Click);

                

                chkBulkItem = new CheckBox("chkBulkItem");
                chkBulkItem.Size = new Size(100, 17);
                chkBulkItem.Location = new Point(410, 110);
                chkBulkItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkBulkItem.Text = "Bulk Edit";


                #endregion
                #region Added to pnlRDungeonItems
                pnlRDungeonItems.AddWidget(lblItems);

                itemList = new List<EditableRDungeonItem>();
                pnlRDungeonItems.AddWidget(lbxItems);

                pnlRDungeonItems.AddWidget(lblItemNum);
                pnlRDungeonItems.AddWidget(nudItemNum);
                pnlRDungeonItems.AddWidget(lblMinValue);
                pnlRDungeonItems.AddWidget(nudMinValue);
                pnlRDungeonItems.AddWidget(lblMaxValue);
                pnlRDungeonItems.AddWidget(nudMaxValue);
                pnlRDungeonItems.AddWidget(lblItemSpawnRate);
                pnlRDungeonItems.AddWidget(nudItemSpawnRate);
                pnlRDungeonItems.AddWidget(lblStickyRate);
                pnlRDungeonItems.AddWidget(nudStickyRate);
                pnlRDungeonItems.AddWidget(lblTag);
                pnlRDungeonItems.AddWidget(txtTag);
                pnlRDungeonItems.AddWidget(chkHidden);
                pnlRDungeonItems.AddWidget(chkOnGround);
                pnlRDungeonItems.AddWidget(chkOnWater);
                pnlRDungeonItems.AddWidget(chkOnWall);
                pnlRDungeonItems.AddWidget(btnAddItem);
                pnlRDungeonItems.AddWidget(btnRemoveItem);
                pnlRDungeonItems.AddWidget(btnLoadItem);
                pnlRDungeonItems.AddWidget(btnChangeItem);
                pnlRDungeonItems.AddWidget(chkBulkItem);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonItems);
                itemsLoaded = true;
            }
        }

        void LoadpnlRDungeonNpcs() {
            if (!npcsLoaded) {
                pnlRDungeonNpcs = new Panel("pnlRDungeonNpcs");
                pnlRDungeonNpcs.Size = new Size(600, 320);
                pnlRDungeonNpcs.Location = new Point(0, 80);
                pnlRDungeonNpcs.BackColor = Color.LightGray;
                pnlRDungeonNpcs.Visible = false;
                #region NPCs
                //Label lblNpcs;
                lblNpcs = new Label("lblNpcs");
                lblNpcs.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcs.Text = "Npcs Settings";
                lblNpcs.AutoSize = true;
                lblNpcs.Location = new Point(10, 4);

                //ListBox lbxItems;
                lbxNpcs = new ListBox("lbxNpcs");
                lbxNpcs.Location = new Point(10, 150);
                lbxNpcs.Size = new Size(580, 140);

                //Label lblNpcSpawnTime;
                lblNpcSpawnTime = new Label("lblNpcSpawnTime");
                lblNpcSpawnTime.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcSpawnTime.Text = "Spawn Time:";
                lblNpcSpawnTime.AutoSize = true;
                lblNpcSpawnTime.Location = new Point(100, 22);
                //NumericUpDown nudNpcSpawnTime;
                nudNpcSpawnTime = new NumericUpDown("nudNpcSpawnTime");
                nudNpcSpawnTime.Minimum = 1;
                nudNpcSpawnTime.Maximum = Int32.MaxValue;
                nudNpcSpawnTime.Size = new Size(80, 20);
                nudNpcSpawnTime.Location = new Point(100, 36);
                nudNpcSpawnTime.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblNpcMin = new Label("lblNpcMin");
                lblNpcMin.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcMin.Text = "Min Npcs";
                lblNpcMin.AutoSize = true;
                lblNpcMin.Location = new Point(250, 20);
                //NumericUpDown nudNpcMin;
                nudNpcMin = new NumericUpDown("nudNpcMin");
                nudNpcMin.Minimum = 0;
                nudNpcMin.Maximum = MaxInfo.MAX_MAP_NPCS;
                nudNpcMin.Size = new Size(80, 20);
                nudNpcMin.Location = new Point(250, 36);
                nudNpcMin.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblNpcMax;
                lblNpcMax = new Label("lblNpcMax");
                lblNpcMax.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcMax.Text = "Max Npcs";
                lblNpcMax.AutoSize = true;
                lblNpcMax.Location = new Point(400, 20);
                //NumericUpDown nudNpcMax;
                nudNpcMax = new NumericUpDown("nudNpcMax");
                nudNpcMax.Minimum = 0;
                nudNpcMax.Maximum = MaxInfo.MAX_MAP_NPCS;
                nudNpcMax.Size = new Size(80, 20);
                nudNpcMax.Location = new Point(400, 36);
                nudNpcMax.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblNpcNum;
                lblNpcNum = new Label("lblNpcNum");
                lblNpcNum.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcNum.Text = "NPC #";
                lblNpcNum.AutoSize = true;
                lblNpcNum.Location = new Point(50, 60);
                //NumericUpDown nudNpcNum;
                nudNpcNum = new NumericUpDown("nudNpcNum");
                nudNpcNum.Minimum = 1;
                nudNpcNum.Maximum = MaxInfo.MaxItems;
                nudNpcNum.Size = new Size(80, 20);
                nudNpcNum.Location = new Point(50, 74);
                nudNpcNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                nudNpcNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudNpcNum_ValueChanged);

                //Label lblNpcSpawnRate;
                lblNpcSpawnRate = new Label("lblNpcSpawnRate");
                lblNpcSpawnRate.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcSpawnRate.Text = "Spawn Rate:";
                lblNpcSpawnRate.AutoSize = true;
                lblNpcSpawnRate.Location = new Point(190, 60);
                //NumericUpDown nudNpcSpawnRate;
                nudNpcSpawnRate = new NumericUpDown("nudNpcSpawnRate");
                nudNpcSpawnRate.Minimum = 1;
                nudNpcSpawnRate.Maximum = 100;
                nudNpcSpawnRate.Size = new Size(80, 20);
                nudNpcSpawnRate.Location = new Point(190, 74);
                nudNpcSpawnRate.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblMinLevel = new Label("lblMinLevel");
                lblMinLevel.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMinLevel.Text = "Min Level";
                lblMinLevel.AutoSize = true;
                lblMinLevel.Location = new Point(320, 60);
                //NumericUpDown nudMinLevel;
                nudMinLevel = new NumericUpDown("nudMinLevel");
                nudMinLevel.Minimum = 1;
                nudMinLevel.Maximum = 100;
                nudMinLevel.Size = new Size(80, 20);
                nudMinLevel.Location = new Point(320, 74);
                nudMinLevel.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblMaxLevel;
                lblMaxLevel = new Label("lblMaxLevel");
                lblMaxLevel.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMaxLevel.Text = "Max Level";
                lblMaxLevel.AutoSize = true;
                lblMaxLevel.Location = new Point(450, 60);
                //NumericUpDown nudMaxLevel;
                nudMaxLevel = new NumericUpDown("nudMaxLevel");
                nudMaxLevel.Minimum = 1;
                nudMaxLevel.Maximum = 100;
                nudMaxLevel.Size = new Size(80, 20);
                nudMaxLevel.Location = new Point(450, 74);
                nudMaxLevel.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblNpcStartStatus = new Label("lblNpcStartStatus");
                lblNpcStartStatus.Font = Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
                lblNpcStartStatus.AutoSize = true;
                lblNpcStartStatus.Location = new Point(190, 94);
                lblNpcStartStatus.Text = "Start Status:";

                cbNpcStartStatus = new ComboBox("cbNpcStartStatus");
                cbNpcStartStatus.Size = new System.Drawing.Size(80, 20);
                cbNpcStartStatus.Location = new Point(190, 108);
                for (int i = 0; i < 6; i++) {
                    cbNpcStartStatus.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("Tahoma", 10), ((Enums.StatusAilment)i).ToString()));
                }
                cbNpcStartStatus.SelectItem(0);

                lblStatusCounter = new Label("lblStatusCounter");
                lblStatusCounter.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
                lblStatusCounter.Text = "Status Counter";
                lblStatusCounter.AutoSize = true;
                lblStatusCounter.Location = new Point(320, 94);
                //NumericUpDown nudStatusCounter;
                nudStatusCounter = new NumericUpDown("nudStatusCounter");
                nudStatusCounter.Minimum = Int32.MinValue;
                nudStatusCounter.Maximum = Int32.MaxValue;
                nudStatusCounter.Size = new Size(80, 20);
                nudStatusCounter.Location = new Point(320, 108);
                nudStatusCounter.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

                //Label lblStatusChance;
                lblStatusChance = new Label("lblStatusChance");
                lblStatusChance.Font = Client.Logic.Graphics.FontManager.LoadFont("Tahoma", 10);
                lblStatusChance.Text = "Status Chance";
                lblStatusChance.AutoSize = true;
                lblStatusChance.Location = new Point(450, 94);
                //NumericUpDown nudStatusChance;
                nudStatusChance = new NumericUpDown("nudStatusChance");
                nudStatusChance.Minimum = 1;
                nudStatusChance.Maximum = 100;
                nudStatusChance.Size = new Size(80, 20);
                nudStatusChance.Location = new Point(450, 108);
                nudStatusChance.Font = Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10);

                //Button btnAddNpc;
                btnAddNpc = new Button("btnAddNpc");
                btnAddNpc.Location = new Point(10, 130);
                btnAddNpc.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnAddNpc.Size = new System.Drawing.Size(64, 16);
                btnAddNpc.Visible = true;
                btnAddNpc.Text = "Add Npc";
                btnAddNpc.Click += new EventHandler<MouseButtonEventArgs>(btnAddNpc_Click);
                //Button btnRemoveNpc;
                btnRemoveNpc = new Button("btnRemoveNpc");
                btnRemoveNpc.Location = new Point(110, 130);
                btnRemoveNpc.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnRemoveNpc.Size = new System.Drawing.Size(64, 16);
                btnRemoveNpc.Visible = true;
                btnRemoveNpc.Text = "Remove Npc";
                btnRemoveNpc.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveNpc_Click);
                //Button btnLoadNpc;
                btnLoadNpc = new Button("btnLoadNpc");
                btnLoadNpc.Location = new Point(210, 130);
                btnLoadNpc.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLoadNpc.Size = new System.Drawing.Size(64, 16);
                btnLoadNpc.Visible = true;
                btnLoadNpc.Text = "Load Npc";
                btnLoadNpc.Click += new EventHandler<MouseButtonEventArgs>(btnLoadNpc_Click);
                //Button btnChangeNpc;
                btnChangeNpc = new Button("btnChangeNpc");
                btnChangeNpc.Location = new Point(310, 130);
                btnChangeNpc.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnChangeNpc.Size = new System.Drawing.Size(64, 16);
                btnChangeNpc.Visible = true;
                btnChangeNpc.Text = "Change Npc";
                btnChangeNpc.Click += new EventHandler<MouseButtonEventArgs>(btnChangeNpc_Click);


                chkBulkNpc = new CheckBox("chkBulkNpc");
                chkBulkNpc.Size = new Size(100, 17);
                chkBulkNpc.Location = new Point(450, 130);
                chkBulkNpc.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                chkBulkNpc.Text = "Bulk Edit";


                #endregion
                #region Added to pnlRDungeonNpcs
                pnlRDungeonNpcs.AddWidget(lblNpcs);

                pnlRDungeonNpcs.AddWidget(lblNpcSpawnTime);
                pnlRDungeonNpcs.AddWidget(nudNpcSpawnTime);

                pnlRDungeonNpcs.AddWidget(lblNpcMin);
                pnlRDungeonNpcs.AddWidget(nudNpcMin);
                pnlRDungeonNpcs.AddWidget(lblNpcMax);
                pnlRDungeonNpcs.AddWidget(nudNpcMax);

                npcList = new List<MapNpcSettings>();
                pnlRDungeonNpcs.AddWidget(lbxNpcs);

                pnlRDungeonNpcs.AddWidget(lblNpcNum);
                pnlRDungeonNpcs.AddWidget(nudNpcNum);
                pnlRDungeonNpcs.AddWidget(lblMinLevel);
                pnlRDungeonNpcs.AddWidget(nudMinLevel);
                pnlRDungeonNpcs.AddWidget(lblMaxLevel);
                pnlRDungeonNpcs.AddWidget(nudMaxLevel);
                pnlRDungeonNpcs.AddWidget(lblNpcSpawnRate);
                pnlRDungeonNpcs.AddWidget(nudNpcSpawnRate);
                pnlRDungeonNpcs.AddWidget(lblNpcStartStatus);
                pnlRDungeonNpcs.AddWidget(cbNpcStartStatus);
                pnlRDungeonNpcs.AddWidget(lblStatusCounter);
                pnlRDungeonNpcs.AddWidget(nudStatusCounter);
                pnlRDungeonNpcs.AddWidget(lblStatusChance);
                pnlRDungeonNpcs.AddWidget(nudStatusChance);
                pnlRDungeonNpcs.AddWidget(btnAddNpc);
                pnlRDungeonNpcs.AddWidget(btnRemoveNpc);
                pnlRDungeonNpcs.AddWidget(btnLoadNpc);
                pnlRDungeonNpcs.AddWidget(btnChangeNpc);
                pnlRDungeonNpcs.AddWidget(chkBulkNpc);

                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonNpcs);
                npcsLoaded = true;
            }
        }

        void LoadpnlRDungeonTraps() {
            if (!trapsLoaded) {
                pnlRDungeonTraps = new Panel("pnlRDungeonTraps");
                pnlRDungeonTraps.Size = new Size(600, 320);
                pnlRDungeonTraps.Location = new Point(0, 80);
                pnlRDungeonTraps.BackColor = Color.LightGray;
                pnlRDungeonTraps.Visible = false;
                #region Traps
                //Label lblTraps;
                lblTraps = new Label("lblTraps");
                lblTraps.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTraps.Text = "Traps Settings";
                lblTraps.AutoSize = true;
                lblTraps.Location = new Point(10, 4);
                //ListBox lbxTraps;
                lbxTraps = new ListBox("lbxTraps");
                lbxTraps.Location = new Point(10, 170);
                lbxTraps.Size = new Size(580, 120);



                lblTrapTileset = new Label[10];
                picTrapTileset = new PictureBox[10];
                for (int i = 0; i < 10; i++) {
                    lblTrapTileset[i] = new Label("lblTrapTileset" + i);
                    lblTrapTileset[i].Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                    lblTrapTileset[i].AutoSize = true;

                    lblTrapTileset[i].Location = new Point(2 + i * 60, 22);


                    picTrapTileset[i] = new PictureBox("picTrapTileset" + i);
                    picTrapTileset[i].Size = new Size(32, 32);
                    picTrapTileset[i].BackColor = Color.Transparent;
                    picTrapTileset[i].Image = new SdlDotNet.Graphics.Surface(32, 32);
                    picTrapTileset[i].Click += new EventHandler<MouseButtonEventArgs>(picTrapTileset_Click);

                    picTrapTileset[i].Location = new Point(10 + i * 60, 38);

                    pnlRDungeonTraps.AddWidget(lblTrapTileset[i]);
                    pnlRDungeonTraps.AddWidget(picTrapTileset[i]);
                }

                lblTrapTileset[0].Text = "Ground";
                lblTrapTileset[1].Text = "GAnim";
                lblTrapTileset[2].Text = "Mask";
                lblTrapTileset[3].Text = "MAnim";
                lblTrapTileset[4].Text = "Mask2";
                lblTrapTileset[5].Text = "M2Anim";
                lblTrapTileset[6].Text = "Fringe";
                lblTrapTileset[7].Text = "FAnim";
                lblTrapTileset[8].Text = "Fringe2";
                lblTrapTileset[9].Text = "F2Anim";


                cbTrapType = new ComboBox("cbTrapType");
                cbTrapType.Location = new Point(10, 80);
                cbTrapType.Size = new Size(120, 16);
                for (int i = 0; i < 40; i++) {
                    cbTrapType.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.TileType), i)));
                }
                cbTrapType.SelectItem(0);

                lblTrapChance = new Label("lblTrapChance");
                lblTrapChance.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapChance.Text = "Trap Chance:";
                lblTrapChance.AutoSize = true;
                lblTrapChance.Location = new Point(10, 106);

                nudTrapChance = new NumericUpDown("nudTrapChance");
                nudTrapChance.Minimum = 1;
                nudTrapChance.Maximum = 100;
                nudTrapChance.Size = new Size(80, 20);
                nudTrapChance.Location = new Point(10, 120);
                nudTrapChance.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapData1 = new Label("lblTrapData1");
                lblTrapData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapData1.Text = "Trap Data1:";
                lblTrapData1.AutoSize = true;
                lblTrapData1.Location = new Point(310, 76);

                nudTrapData1 = new NumericUpDown("nudTrapData1");
                nudTrapData1.Minimum = Int32.MinValue;
                nudTrapData1.Maximum = Int32.MaxValue;
                nudTrapData1.Size = new Size(80, 20);
                nudTrapData1.Location = new Point(310, 90);
                nudTrapData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapData2 = new Label("lblTrapData2");
                lblTrapData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapData2.Text = "Trap Data2:";
                lblTrapData2.AutoSize = true;
                lblTrapData2.Location = new Point(410, 76);

                nudTrapData2 = new NumericUpDown("nudTrapData2");
                nudTrapData2.Minimum = Int32.MinValue;
                nudTrapData2.Maximum = Int32.MaxValue;
                nudTrapData2.Size = new Size(80, 20);
                nudTrapData2.Location = new Point(410, 90);
                nudTrapData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapData3 = new Label("lblTrapData3");
                lblTrapData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapData3.Text = "Trap Data3:";
                lblTrapData3.AutoSize = true;
                lblTrapData3.Location = new Point(510, 76);

                nudTrapData3 = new NumericUpDown("nudTrapData3");
                nudTrapData3.Minimum = Int32.MinValue;
                nudTrapData3.Maximum = Int32.MaxValue;
                nudTrapData3.Size = new Size(80, 20);
                nudTrapData3.Location = new Point(510, 90);
                nudTrapData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapString1 = new Label("lblTrapString1");
                lblTrapString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapString1.Text = "Trap String1:";
                lblTrapString1.AutoSize = true;
                lblTrapString1.Location = new Point(310, 120);

                txtTrapString1 = new TextBox("txtTrapString1");
                txtTrapString1.BackColor = Color.White;
                txtTrapString1.Size = new Size(80, 20);
                txtTrapString1.Location = new Point(310, 134);
                txtTrapString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapString2 = new Label("lblTrapString2");
                lblTrapString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapString2.Text = "Trap String2:";
                lblTrapString2.AutoSize = true;
                lblTrapString2.Location = new Point(410, 120);

                txtTrapString2 = new TextBox("txtTrapString2");
                txtTrapString2.BackColor = Color.White;
                txtTrapString2.Size = new Size(80, 20);
                txtTrapString2.Location = new Point(410, 134);
                txtTrapString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                lblTrapString3 = new Label("lblTrapString3");
                lblTrapString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTrapString3.Text = "Trap String3:";
                lblTrapString3.AutoSize = true;
                lblTrapString3.Location = new Point(510, 120);

                txtTrapString3 = new TextBox("txtTrapString3");
                txtTrapString3.BackColor = Color.White;
                txtTrapString3.Size = new Size(80, 20);
                txtTrapString3.Location = new Point(510, 134);
                txtTrapString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);


                //Button btnAddTrap;
                btnAddTrap = new Button("btnAddTrap");
                btnAddTrap.Location = new Point(220, 150);
                btnAddTrap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnAddTrap.Size = new System.Drawing.Size(64, 16);
                btnAddTrap.Visible = true;
                btnAddTrap.Text = "Add Trap";
                btnAddTrap.Click += new EventHandler<MouseButtonEventArgs>(btnAddTrap_Click);
                //Button btnRemoveTrap;
                btnRemoveTrap = new Button("btnRemoveTrap");
                btnRemoveTrap.Location = new Point(150, 150);
                btnRemoveTrap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnRemoveTrap.Size = new System.Drawing.Size(64, 16);
                btnRemoveTrap.Visible = true;
                btnRemoveTrap.Text = "Remove Trap";
                btnRemoveTrap.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveTrap_Click);
                //Button btnLoadTrap;
                btnLoadTrap = new Button("btnLoadTrap");
                btnLoadTrap.Location = new Point(80, 150);
                btnLoadTrap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnLoadTrap.Size = new System.Drawing.Size(64, 16);
                btnLoadTrap.Visible = true;
                btnLoadTrap.Text = "Load Trap";
                btnLoadTrap.Click += new EventHandler<MouseButtonEventArgs>(btnLoadTrap_Click);
                //Button btnChangeTrap;
                btnChangeTrap = new Button("btnChangeTrap");
                btnChangeTrap.Location = new Point(10, 150);
                btnChangeTrap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnChangeTrap.Size = new System.Drawing.Size(64, 16);
                btnChangeTrap.Visible = true;
                btnChangeTrap.Text = "Change Trap";
                btnChangeTrap.Click += new EventHandler<MouseButtonEventArgs>(btnChangeTrap_Click);

                #endregion
                #region Added to pnlRDungeonTraps
                pnlRDungeonTraps.AddWidget(lblTraps);

                trapList = new List<EditableRDungeonTrap>();
                pnlRDungeonTraps.AddWidget(lbxTraps);
                trapTileNumbers = new int[2, 10];

                pnlRDungeonTraps.AddWidget(cbTrapType);
                pnlRDungeonTraps.AddWidget(lblTrapData1);
                pnlRDungeonTraps.AddWidget(nudTrapData1);
                pnlRDungeonTraps.AddWidget(lblTrapData2);
                pnlRDungeonTraps.AddWidget(nudTrapData2);
                pnlRDungeonTraps.AddWidget(lblTrapData3);
                pnlRDungeonTraps.AddWidget(nudTrapData3);
                pnlRDungeonTraps.AddWidget(lblTrapString1);
                pnlRDungeonTraps.AddWidget(txtTrapString1);
                pnlRDungeonTraps.AddWidget(lblTrapString2);
                pnlRDungeonTraps.AddWidget(txtTrapString2);
                pnlRDungeonTraps.AddWidget(lblTrapString3);
                pnlRDungeonTraps.AddWidget(txtTrapString3);

                pnlRDungeonTraps.AddWidget(lblTrapChance);
                pnlRDungeonTraps.AddWidget(nudTrapChance);

                pnlRDungeonTraps.AddWidget(btnAddTrap);
                pnlRDungeonTraps.AddWidget(btnRemoveTrap);
                pnlRDungeonTraps.AddWidget(btnLoadTrap);
                pnlRDungeonTraps.AddWidget(btnChangeTrap);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonTraps);
                trapsLoaded = true;
            }
        }

        void LoadpnlRDungeonWeather() {
            if (!weatherLoaded) {
                pnlRDungeonWeather = new Panel("pnlRDungeonWeather");
                pnlRDungeonWeather.Size = new Size(600, 320);
                pnlRDungeonWeather.Location = new Point(0, 80);
                pnlRDungeonWeather.BackColor = Color.LightGray;
                pnlRDungeonWeather.Visible = false;
                #region Weather
                //Label lblWeather;
                lblWeather = new Label("lblWeather");
                lblWeather.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblWeather.Text = "Weather Settings";
                lblWeather.AutoSize = true;
                lblWeather.Location = new Point(10, 4);
                //ListBox lbxWeather;
                lbxWeather = new ListBox("lbxWeather");
                lbxWeather.Location = new Point(300, 26);
                lbxWeather.Size = new Size(260, 260);


                cbWeather = new ComboBox("cbWeather");
                cbWeather.Location = new Point(10, 26);
                cbWeather.Size = new Size(200, 16);

                //ComboBox cbWeather;
                for (int i = 0; i < 13; i++) {
                    //lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), );
                    cbWeather.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": " + Enum.GetName(typeof(Enums.Weather), i)));
                }
                cbWeather.SelectItem(0);


                btnAddWeather = new Button("btnAddWeather");
                btnAddWeather.Location = new Point(10, 70);
                btnAddWeather.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnAddWeather.Size = new System.Drawing.Size(80, 16);
                btnAddWeather.Visible = true;
                btnAddWeather.Text = "Add Weather";
                btnAddWeather.Click += new EventHandler<MouseButtonEventArgs>(btnAddWeather_Click);

                btnRemoveWeather = new Button("btnRemoveWeather");
                btnRemoveWeather.Location = new Point(120, 70);
                btnRemoveWeather.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnRemoveWeather.Size = new System.Drawing.Size(80, 16);
                btnRemoveWeather.Visible = true;
                btnRemoveWeather.Text = "Remove Weather";
                btnRemoveWeather.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveWeather_Click);
                //ComboBox cbWeather;
                //Button btnAddWeather;
                //Button btnRemoveWeather;
                #endregion
                #region Added to pnlRDungeonWeather
                pnlRDungeonWeather.AddWidget(lblWeather);
                pnlRDungeonWeather.AddWidget(lbxWeather);
                pnlRDungeonWeather.AddWidget(cbWeather);
                pnlRDungeonWeather.AddWidget(btnAddWeather);
                pnlRDungeonWeather.AddWidget(btnRemoveWeather);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonWeather);
                weatherLoaded = true;
            }
        }

        void LoadpnlRDungeonGoal() {
            if (!goalLoaded) {
                pnlRDungeonGoal = new Panel("pnlRDungeonGoal");
                pnlRDungeonGoal.Size = new Size(600, 320);
                pnlRDungeonGoal.Location = new Point(0, 80);
                pnlRDungeonGoal.BackColor = Color.LightGray;
                pnlRDungeonGoal.Visible = false;
                #region Goal
                //Label lblGoal;
                lblGoal = new Label("lblGoal");
                lblGoal.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblGoal.Text = "Goal Settings";
                lblGoal.AutoSize = true;
                lblGoal.Location = new Point(10, 4);
                //RadioButton optNextFloor;
                optNextFloor = new RadioButton("optNextFloor");
                optNextFloor.BackColor = Color.Transparent;
                optNextFloor.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                optNextFloor.Location = new Point(10, 24);
                optNextFloor.Size = new System.Drawing.Size(95, 17);
                optNextFloor.Text = "Next Floor";
                optNextFloor.CheckChanged += new EventHandler(optNextFloor_Checked);
                //RadioButton optMap;
                optMap = new RadioButton("optMap");
                optMap.BackColor = Color.Transparent;
                optMap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                optMap.Location = new Point(10, 44);
                optMap.Size = new System.Drawing.Size(95, 17);
                optMap.Text = "Map";
                optMap.CheckChanged += new EventHandler(optMap_Checked);
                //RadioButton optScripted;
                optScripted = new RadioButton("optScripted");
                optScripted.BackColor = Color.Transparent;
                optScripted.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                optScripted.Location = new Point(10, 64);
                optScripted.Size = new System.Drawing.Size(95, 17);
                optScripted.Text = "Script";
                optScripted.CheckChanged += new EventHandler(optScripted_Checked);
                //Label lblData1;
                lblData1 = new Label("lblData1");
                lblData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblData1.Text = "Data1:";
                lblData1.AutoSize = true;
                lblData1.Location = new Point(10, 94);
                //Label lblData2;
                lblData2 = new Label("lblData2");
                lblData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblData2.Text = "Data2:";
                lblData2.AutoSize = true;
                lblData2.Location = new Point(10, 114);
                //Label lblData3;
                lblData3 = new Label("lblData3");
                lblData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblData3.Text = "Data3:";
                lblData3.AutoSize = true;
                lblData3.Location = new Point(10, 134);
                //NumericUpDown nudData1;
                nudData1 = new NumericUpDown("nudData1");
                nudData1.Minimum = Int32.MinValue;
                nudData1.Maximum = Int32.MaxValue;
                nudData1.Size = new Size(80, 20);
                nudData1.Location = new Point(100, 94);
                nudData1.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                //NumericUpDown nudData2;
                nudData2 = new NumericUpDown("nudData2");
                nudData2.Minimum = Int32.MinValue;
                nudData2.Maximum = Int32.MaxValue;
                nudData2.Size = new Size(80, 20);
                nudData2.Location = new Point(100, 114);
                nudData2.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                //NumericUpDown nudData3;
                nudData3 = new NumericUpDown("nudData3");
                nudData3.Minimum = Int32.MinValue;
                nudData3.Maximum = Int32.MaxValue;
                nudData3.Size = new Size(80, 20);
                nudData3.Location = new Point(100, 134);
                nudData3.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                #endregion
                #region Added to pnlRDungeonGoal
                pnlRDungeonGoal.AddWidget(lblGoal);
                pnlRDungeonGoal.AddWidget(optNextFloor);
                pnlRDungeonGoal.AddWidget(optMap);
                pnlRDungeonGoal.AddWidget(optScripted);
                pnlRDungeonGoal.AddWidget(lblData1);
                pnlRDungeonGoal.AddWidget(lblData2);
                pnlRDungeonGoal.AddWidget(lblData3);
                pnlRDungeonGoal.AddWidget(nudData1);
                pnlRDungeonGoal.AddWidget(nudData2);
                pnlRDungeonGoal.AddWidget(nudData3);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonGoal);
                goalLoaded = true;
            }
        }

        void LoadpnlRDungeonChambers() {
            if (!chambersLoaded) {
                pnlRDungeonChambers = new Panel("pnlRDungeonChambers");
                pnlRDungeonChambers.Size = new Size(600, 320);
                pnlRDungeonChambers.Location = new Point(0, 80);
                pnlRDungeonChambers.BackColor = Color.LightGray;
                pnlRDungeonChambers.Visible = false;
                #region Chambers
                //Label lblChambers;
                lblChambers = new Label("lblChambers");
                lblChambers.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblChambers.Text = "Chambers Settings";
                lblChambers.AutoSize = true;
                lblChambers.Location = new Point(10, 4);
                //ListBox lbxChambers;
                lbxChambers = new ListBox("lbxChambers");
                lbxChambers.Location = new Point(300, 26);
                lbxChambers.Size = new Size(260, 260);

                //lblChamberNum
                lblChamberNum = new Label("lblChamberNum");
                lblChamberNum.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblChamberNum.Text = "Chamber Num:";
                lblChamberNum.AutoSize = true;
                lblChamberNum.Location = new Point(10, 22);

                //NumericUpDown Chamber;
                nudChamber = new NumericUpDown("nudChamber");
                nudChamber.Minimum = Int32.MinValue;
                nudChamber.Maximum = Int32.MaxValue;
                nudChamber.Size = new Size(200, 16);
                nudChamber.Location = new Point(10, 36);
                nudChamber.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //lblChamberString1
                lblChamberString1 = new Label("lblChamberString1");
                lblChamberString1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblChamberString1.Text = "Chamber String 1:";
                lblChamberString1.AutoSize = true;
                lblChamberString1.Location = new Point(10, 58);

                //Textbox ChamberString1;
                txtChamberString1 = new TextBox("txtChamberString1");
                txtChamberString1.BackColor = Color.White;
                txtChamberString1.Size = new Size(80, 20);
                txtChamberString1.Location = new Point(10, 72);
                txtChamberString1.Font = Graphics.FontManager.LoadFont("tahoma", 10);


                //lblChamberString2
                lblChamberString2 = new Label("lblChamberString2");
                lblChamberString2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblChamberString2.Text = "Chamber String 2";
                lblChamberString2.AutoSize = true;
                lblChamberString2.Location = new Point(100, 58);

                //Textbox ChamberString2;
                txtChamberString2 = new TextBox("txtChamberString2");
                txtChamberString2.BackColor = Color.White;
                txtChamberString2.Size = new Size(80, 20);
                txtChamberString2.Location = new Point(100, 72);
                txtChamberString2.Font = Graphics.FontManager.LoadFont("tahoma", 10);

                //lblChamberString3
                lblChamberString3 = new Label("lblChamberString3");
                lblChamberString3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblChamberString3.Text = "Chamber String 3:";
                lblChamberString3.AutoSize = true;
                lblChamberString3.Location = new Point(190, 58);

                //Textbox ChamberString3;
                txtChamberString3 = new TextBox("txtChamberString3");
                txtChamberString3.BackColor = Color.White;
                txtChamberString3.Size = new Size(80, 20);
                txtChamberString3.Location = new Point(190, 72);
                txtChamberString3.Font = Graphics.FontManager.LoadFont("tahoma", 10);


                btnAddChamber = new Button("btnAddChamber");
                btnAddChamber.Location = new Point(10, 120);
                btnAddChamber.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnAddChamber.Size = new System.Drawing.Size(80, 16);
                btnAddChamber.Visible = true;
                btnAddChamber.Text = "Add Chamber";
                btnAddChamber.Click += new EventHandler<MouseButtonEventArgs>(btnAddChamber_Click);

                btnRemoveChamber = new Button("btnRemoveChamber");
                btnRemoveChamber.Location = new Point(120, 120);
                btnRemoveChamber.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnRemoveChamber.Size = new System.Drawing.Size(80, 16);
                btnRemoveChamber.Visible = true;
                btnRemoveChamber.Text = "Remove Chamber";
                btnRemoveChamber.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveChamber_Click);
                //ComboBox cbChamber;
                //Button btnAddChamber;
                //Button btnRemoveChamber;
                #endregion
                #region Added to pnlRDungeonChambers
                pnlRDungeonChambers.AddWidget(lblChambers);

                chamberList = new List<EditableRDungeonChamber>();
                pnlRDungeonChambers.AddWidget(lbxChambers);
                pnlRDungeonChambers.AddWidget(lblChamberNum);
                pnlRDungeonChambers.AddWidget(nudChamber);
                pnlRDungeonChambers.AddWidget(lblChamberString1);
                pnlRDungeonChambers.AddWidget(txtChamberString1);
                pnlRDungeonChambers.AddWidget(lblChamberString2);
                pnlRDungeonChambers.AddWidget(txtChamberString2);
                pnlRDungeonChambers.AddWidget(lblChamberString3);
                pnlRDungeonChambers.AddWidget(txtChamberString3);
                pnlRDungeonChambers.AddWidget(btnAddChamber);
                pnlRDungeonChambers.AddWidget(btnRemoveChamber);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonChambers);
                chambersLoaded = true;
            }
        }

        void LoadpnlRDungeonMisc() {
            if (!miscLoaded) {
                pnlRDungeonMisc = new Panel("pnlRDungeonMisc");
                pnlRDungeonMisc.Size = new Size(600, 320);
                pnlRDungeonMisc.Location = new Point(0, 80);
                pnlRDungeonMisc.BackColor = Color.LightGray;
                pnlRDungeonMisc.Visible = false;
                #region Misc
                //Label lblMisc;
                lblMisc = new Label("lblMisc");
                lblMisc.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMisc.Text = "Misc Settings";
                lblMisc.AutoSize = true;
                lblMisc.Location = new Point(10, 4);
                //Label lblDarkness;
                lblDarkness = new Label("lblDarkness");
                lblDarkness.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblDarkness.Text = "Darkness Settings";
                lblDarkness.AutoSize = true;
                lblDarkness.Location = new Point(10, 30);
                //NumericUpDown nudDarkness;
                nudDarkness = new NumericUpDown("nudDarkness");
                nudDarkness.Minimum = Int32.MinValue;
                nudDarkness.Maximum = Int32.MaxValue;
                nudDarkness.Size = new Size(80, 20);
                nudDarkness.Location = new Point(100, 30);
                nudDarkness.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                //Label lblMusic;
                lblMusic = new Label("lblMusic");
                lblMusic.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblMusic.Text = "Music";
                lblMusic.AutoSize = true;
                lblMusic.Location = new Point(10, 60);
                //ListBox lbxMusic;
                lbxMusic = new ListBox("lbxMusic");
                lbxMusic.Location = new Point(10, 76);
                lbxMusic.Size = new Size(260, 220);
                lbxMusic.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("Tahoma", 10), "None"));
                string[] songs = System.IO.Directory.GetFiles(IO.Paths.MusicPath);
                for (int i = 0; i < songs.Length; i++) {
                    lbxMusic.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("Tahoma", 10), System.IO.Path.GetFileName(songs[i])));
                }
                //Button btnPlayMusic;
                btnPlayMusic = new Button("btnPlayMusic");
                btnPlayMusic.Location = new Point(90, 60);
                btnPlayMusic.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnPlayMusic.Size = new System.Drawing.Size(80, 16);
                btnPlayMusic.Visible = true;
                btnPlayMusic.Text = "Play Song";
                btnPlayMusic.Click += new EventHandler<MouseButtonEventArgs>(btnPlayMusic_Click);
                #endregion
                #region Added to pnlRDungeonMisc
                pnlRDungeonMisc.AddWidget(lblMisc);
                pnlRDungeonMisc.AddWidget(lblMusic);
                pnlRDungeonMisc.AddWidget(lbxMusic);
                pnlRDungeonMisc.AddWidget(btnPlayMusic);
                pnlRDungeonMisc.AddWidget(lblDarkness);
                pnlRDungeonMisc.AddWidget(nudDarkness);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlRDungeonMisc);
                miscLoaded = true;
            }
        }

        void LoadpnlTileSelector() {
            if (!tileSelectorLoaded) {
                pnlTileSelector = new Panel("pnlTileSelector");
                pnlTileSelector.Size = new Size(600, 400);
                pnlTileSelector.Location = new Point(0, 0);
                pnlTileSelector.BackColor = Color.LightGray;
                pnlTileSelector.Visible = false;
                #region Tile Selector
                TileSelector = new Widgets.TilesetViewer("TileSelector");
                TileSelector.Location = new Point(0, 32);
                TileSelector.Size = new Size(458, 350);
                TileSelector.ActiveTilesetSurface = Graphics.GraphicsManager.Tiles[0];

                lblTileSet = new Label("lblTileSet");
                lblTileSet.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
                lblTileSet.Text = "TileSet Settings";
                lblTileSet.AutoSize = true;
                lblTileSet.Location = new Point(10, 4);

                nudTileSet = new NumericUpDown("nudTileSet");
                nudTileSet.Minimum = 0;
                nudTileSet.Maximum = 10;
                nudTileSet.Size = new Size(80, 20);
                nudTileSet.Location = new Point(100, 4);
                nudTileSet.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                nudTileSet.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudTileSet_ValueChanged);

                btnTileSetOK = new Button("btnTileSetOK");
                btnTileSetOK.Location = new Point(200, 4);
                btnTileSetOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnTileSetOK.Size = new System.Drawing.Size(80, 16);
                btnTileSetOK.Visible = true;
                btnTileSetOK.Text = "OK";
                btnTileSetOK.Click += new EventHandler<MouseButtonEventArgs>(btnTileSetOK_Click);

                btnTileSetCancel = new Button("btnTileSetCancel");
                btnTileSetCancel.Location = new Point(300, 4);
                btnTileSetCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
                btnTileSetCancel.Size = new System.Drawing.Size(80, 16);
                btnTileSetCancel.Visible = true;
                btnTileSetCancel.Text = "Cancel";
                btnTileSetCancel.Click += new EventHandler<MouseButtonEventArgs>(btnTileSetCancel_Click);
                #endregion
                #region Added to pnlTileSelector
                pnlTileSelector.AddWidget(TileSelector);
                pnlTileSelector.AddWidget(lblTileSet);
                pnlTileSelector.AddWidget(nudTileSet);
                pnlTileSelector.AddWidget(btnTileSetOK);
                pnlTileSelector.AddWidget(btnTileSetCancel);
                #endregion
                pnlRDungeonFloors.AddWidget(pnlTileSelector);
                tileSelectorLoaded = true;
            }
        }

        #endregion

        #region RDungeon List

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshRDungeonList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxRDungeons / 10)) {
                currentTen++;
            }
            RefreshRDungeonList();
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxRDungeonList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxRDungeonList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {

                    //Messenger.SendEditRDungeon(index[0].ToInt());
                    btnEdit.Text = "Loading...";

                    LoadpnlRDungeonGeneral();

                    Messenger.SendEditRDungeon(index[0].ToInt() - 1);
                }
            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnAddNew_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            Messenger.SendAddRDungeon();


        }

        public void RefreshRDungeonList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxRDungeons) {
                    ((ListBoxTextItem)lbxRDungeonList.Items[i]).Text = ((i + 1) + 10 * currentTen) + ": " + RDungeons.RDungeonHelper.RDungeons[(i + 10 * currentTen)].Name;
                } else {
                    ((ListBoxTextItem)lbxRDungeonList.Items[i]).Text = "---";
                }
            }
        }

        #endregion

        #region General

        public void LoadRDungeon(string[] parse) {

            //load Rdungeon from packet
            rdungeon = new EditableRDungeon(parse[1].ToInt());
            rdungeon.DungeonName = parse[2];
            rdungeon.Direction = (Enums.Direction)parse[3].ToInt();
            rdungeon.MaxFloors = parse[4].ToInt();
            rdungeon.Recruitment = parse[5].ToBool();
            rdungeon.Exp = parse[6].ToBool();
            rdungeon.WindTimer = parse[7].ToInt();
            rdungeon.DungeonIndex = parse[8].ToInt();
            int n = 9;
            for (int i = 0; i < rdungeon.MaxFloors; i++) {
                rdungeon.Floors.Add(new EditableRDungeonFloor());
                rdungeon.Floors[i].TrapMin = parse[n].ToInt();
                rdungeon.Floors[i].TrapMax = parse[n + 1].ToInt();
                rdungeon.Floors[i].ItemMin = parse[n + 2].ToInt();
                rdungeon.Floors[i].ItemMax = parse[n + 3].ToInt();
                rdungeon.Floors[i].Intricacy = parse[n + 4].ToInt();
                rdungeon.Floors[i].RoomWidthMin = parse[n + 5].ToInt();
                rdungeon.Floors[i].RoomWidthMax = parse[n + 6].ToInt();
                rdungeon.Floors[i].RoomLengthMin = parse[n + 7].ToInt();
                rdungeon.Floors[i].RoomLengthMax = parse[n + 8].ToInt();
                rdungeon.Floors[i].HallTurnMin = parse[n + 9].ToInt();
                rdungeon.Floors[i].HallTurnMax = parse[n + 10].ToInt();
                rdungeon.Floors[i].HallVarMin = parse[n + 11].ToInt();
                rdungeon.Floors[i].HallVarMax = parse[n + 12].ToInt();
                rdungeon.Floors[i].WaterFrequency = parse[n + 13].ToInt();
                rdungeon.Floors[i].Craters = parse[n + 14].ToInt();
                rdungeon.Floors[i].CraterMinLength = parse[n + 15].ToInt();
                rdungeon.Floors[i].CraterMaxLength = parse[n + 16].ToInt();
                rdungeon.Floors[i].CraterFuzzy = parse[n + 17].ToBool();
                rdungeon.Floors[i].MinChambers = parse[n + 18].ToInt();
                rdungeon.Floors[i].MaxChambers = parse[n + 19].ToInt();
                rdungeon.Floors[i].Darkness = parse[n + 20].ToInt();
                rdungeon.Floors[i].GoalType = (Enums.RFloorGoalType)parse[n + 21].ToInt();
                rdungeon.Floors[i].GoalMap = parse[n + 22].ToInt();
                rdungeon.Floors[i].GoalX = parse[n + 23].ToInt();
                rdungeon.Floors[i].GoalY = parse[n + 24].ToInt();
                rdungeon.Floors[i].Music = parse[n + 25];

                n += 26;

                rdungeon.Floors[i].StairsX = parse[n].ToInt();
                rdungeon.Floors[i].StairsSheet = parse[n + 1].ToInt();

                rdungeon.Floors[i].mGroundX = parse[n + 2].ToInt();
                rdungeon.Floors[i].mGroundSheet = parse[n + 3].ToInt();

                rdungeon.Floors[i].mTopLeftX = parse[n + 4].ToInt();
                rdungeon.Floors[i].mTopLeftSheet = parse[n + 5].ToInt();
                rdungeon.Floors[i].mTopCenterX = parse[n + 6].ToInt();
                rdungeon.Floors[i].mTopCenterSheet = parse[n + 7].ToInt();
                rdungeon.Floors[i].mTopRightX = parse[n + 8].ToInt();
                rdungeon.Floors[i].mTopRightSheet = parse[n + 9].ToInt();

                rdungeon.Floors[i].mCenterLeftX = parse[n + 10].ToInt();
                rdungeon.Floors[i].mCenterLeftSheet = parse[n + 11].ToInt();
                rdungeon.Floors[i].mCenterCenterX = parse[n + 12].ToInt();
                rdungeon.Floors[i].mCenterCenterSheet = parse[n + 13].ToInt();
                rdungeon.Floors[i].mCenterRightX = parse[n + 14].ToInt();
                rdungeon.Floors[i].mCenterRightSheet = parse[n + 15].ToInt();

                rdungeon.Floors[i].mBottomLeftX = parse[n + 16].ToInt();
                rdungeon.Floors[i].mBottomLeftSheet = parse[n + 17].ToInt();
                rdungeon.Floors[i].mBottomCenterX = parse[n + 18].ToInt();
                rdungeon.Floors[i].mBottomCenterSheet = parse[n + 19].ToInt();
                rdungeon.Floors[i].mBottomRightX = parse[n + 20].ToInt();
                rdungeon.Floors[i].mBottomRightSheet = parse[n + 21].ToInt();

                rdungeon.Floors[i].mInnerTopLeftX = parse[n + 22].ToInt();
                rdungeon.Floors[i].mInnerTopLeftSheet = parse[n + 23].ToInt();
                rdungeon.Floors[i].mInnerBottomLeftX = parse[n + 24].ToInt();
                rdungeon.Floors[i].mInnerBottomLeftSheet = parse[n + 25].ToInt();
                rdungeon.Floors[i].mInnerTopRightX = parse[n + 26].ToInt();
                rdungeon.Floors[i].mInnerTopRightSheet = parse[n + 27].ToInt();
                rdungeon.Floors[i].mInnerBottomRightX = parse[n + 28].ToInt();
                rdungeon.Floors[i].mInnerBottomRightSheet = parse[n + 29].ToInt();

                rdungeon.Floors[i].mIsolatedWallX = parse[n + 30].ToInt();
                rdungeon.Floors[i].mIsolatedWallSheet = parse[n + 31].ToInt();
                rdungeon.Floors[i].mColumnTopX = parse[n + 32].ToInt();
                rdungeon.Floors[i].mColumnTopSheet = parse[n + 33].ToInt();
                rdungeon.Floors[i].mColumnCenterX = parse[n + 34].ToInt();
                rdungeon.Floors[i].mColumnCenterSheet = parse[n + 35].ToInt();
                rdungeon.Floors[i].mColumnBottomX = parse[n + 36].ToInt();
                rdungeon.Floors[i].mColumnBottomSheet = parse[n + 37].ToInt();

                rdungeon.Floors[i].mRowLeftX = parse[n + 38].ToInt();
                rdungeon.Floors[i].mRowLeftSheet = parse[n + 39].ToInt();
                rdungeon.Floors[i].mRowCenterX = parse[n + 40].ToInt();
                rdungeon.Floors[i].mRowCenterSheet = parse[n + 41].ToInt();
                rdungeon.Floors[i].mRowRightX = parse[n + 42].ToInt();
                rdungeon.Floors[i].mRowRightSheet = parse[n + 43].ToInt();


                rdungeon.Floors[i].mGroundAltX = parse[n + 44].ToInt();
                rdungeon.Floors[i].mGroundAltSheet = parse[n + 45].ToInt();
                rdungeon.Floors[i].mGroundAlt2X = parse[n + 46].ToInt();
                rdungeon.Floors[i].mGroundAlt2Sheet = parse[n + 47].ToInt();

                rdungeon.Floors[i].mTopLeftAltX = parse[n + 48].ToInt();
                rdungeon.Floors[i].mTopLeftAltSheet = parse[n + 49].ToInt();
                rdungeon.Floors[i].mTopCenterAltX = parse[n + 50].ToInt();
                rdungeon.Floors[i].mTopCenterAltSheet = parse[n + 51].ToInt();
                rdungeon.Floors[i].mTopRightAltX = parse[n + 52].ToInt();
                rdungeon.Floors[i].mTopRightAltSheet = parse[n + 53].ToInt();

                rdungeon.Floors[i].mCenterLeftAltX = parse[n + 54].ToInt();
                rdungeon.Floors[i].mCenterLeftAltSheet = parse[n + 55].ToInt();
                rdungeon.Floors[i].mCenterCenterAltX = parse[n + 56].ToInt();
                rdungeon.Floors[i].mCenterCenterAltSheet = parse[n + 57].ToInt();
                rdungeon.Floors[i].mCenterCenterAlt2X = parse[n + 58].ToInt();
                rdungeon.Floors[i].mCenterCenterAlt2Sheet = parse[n + 59].ToInt();
                rdungeon.Floors[i].mCenterRightAltX = parse[n + 60].ToInt();
                rdungeon.Floors[i].mCenterRightAltSheet = parse[n + 61].ToInt();

                rdungeon.Floors[i].mBottomLeftAltX = parse[n + 62].ToInt();
                rdungeon.Floors[i].mBottomLeftAltSheet = parse[n + 63].ToInt();
                rdungeon.Floors[i].mBottomCenterAltX = parse[n + 64].ToInt();
                rdungeon.Floors[i].mBottomCenterAltSheet = parse[n + 65].ToInt();
                rdungeon.Floors[i].mBottomRightAltX = parse[n + 66].ToInt();
                rdungeon.Floors[i].mBottomRightAltSheet = parse[n + 67].ToInt();

                rdungeon.Floors[i].mInnerTopLeftAltX = parse[n + 68].ToInt();
                rdungeon.Floors[i].mInnerTopLeftAltSheet = parse[n + 69].ToInt();
                rdungeon.Floors[i].mInnerBottomLeftAltX = parse[n + 70].ToInt();
                rdungeon.Floors[i].mInnerBottomLeftAltSheet = parse[n + 71].ToInt();
                rdungeon.Floors[i].mInnerTopRightAltX = parse[n + 72].ToInt();
                rdungeon.Floors[i].mInnerTopRightAltSheet = parse[n + 73].ToInt();
                rdungeon.Floors[i].mInnerBottomRightAltX = parse[n + 74].ToInt();
                rdungeon.Floors[i].mInnerBottomRightAltSheet = parse[n + 75].ToInt();

                rdungeon.Floors[i].mIsolatedWallAltX = parse[n + 76].ToInt();
                rdungeon.Floors[i].mIsolatedWallAltSheet = parse[n + 77].ToInt();
                rdungeon.Floors[i].mColumnTopAltX = parse[n + 78].ToInt();
                rdungeon.Floors[i].mColumnTopAltSheet = parse[n + 79].ToInt();
                rdungeon.Floors[i].mColumnCenterAltX = parse[n + 80].ToInt();
                rdungeon.Floors[i].mColumnCenterAltSheet = parse[n + 81].ToInt();
                rdungeon.Floors[i].mColumnBottomAltX = parse[n + 82].ToInt();
                rdungeon.Floors[i].mColumnBottomAltSheet = parse[n + 83].ToInt();

                rdungeon.Floors[i].mRowLeftAltX = parse[n + 84].ToInt();
                rdungeon.Floors[i].mRowLeftAltSheet = parse[n + 85].ToInt();
                rdungeon.Floors[i].mRowCenterAltX = parse[n + 86].ToInt();
                rdungeon.Floors[i].mRowCenterAltSheet = parse[n + 87].ToInt();
                rdungeon.Floors[i].mRowRightAltX = parse[n + 88].ToInt();
                rdungeon.Floors[i].mRowRightAltSheet = parse[n + 89].ToInt();

                n += 90;

                rdungeon.Floors[i].mWaterX = parse[n].ToInt();
                rdungeon.Floors[i].mWaterSheet = parse[n + 1].ToInt();
                rdungeon.Floors[i].mWaterAnimX = parse[n + 2].ToInt();
                rdungeon.Floors[i].mWaterAnimSheet = parse[n + 3].ToInt();

                rdungeon.Floors[i].mShoreTopLeftX = parse[n + 4].ToInt();
                rdungeon.Floors[i].mShoreTopLeftSheet = parse[n + 5].ToInt();
                rdungeon.Floors[i].mShoreTopRightX = parse[n + 6].ToInt();
                rdungeon.Floors[i].mShoreTopRightSheet = parse[n + 7].ToInt();
                rdungeon.Floors[i].mShoreBottomRightX = parse[n + 8].ToInt();
                rdungeon.Floors[i].mShoreBottomRightSheet = parse[n + 9].ToInt();
                rdungeon.Floors[i].mShoreBottomLeftX = parse[n + 10].ToInt();
                rdungeon.Floors[i].mShoreBottomLeftSheet = parse[n + 11].ToInt();

                rdungeon.Floors[i].mShoreDiagonalForwardX = parse[n + 12].ToInt();
                rdungeon.Floors[i].mShoreDiagonalForwardSheet = parse[n + 13].ToInt();
                rdungeon.Floors[i].mShoreDiagonalBackX = parse[n + 14].ToInt();
                rdungeon.Floors[i].mShoreDiagonalBackSheet = parse[n + 15].ToInt();

                rdungeon.Floors[i].mShoreTopX = parse[n + 16].ToInt();
                rdungeon.Floors[i].mShoreTopSheet = parse[n + 17].ToInt();
                rdungeon.Floors[i].mShoreRightX = parse[n + 18].ToInt();
                rdungeon.Floors[i].mShoreRightSheet = parse[n + 19].ToInt();
                rdungeon.Floors[i].mShoreBottomX = parse[n + 20].ToInt();
                rdungeon.Floors[i].mShoreBottomSheet = parse[n + 21].ToInt();
                rdungeon.Floors[i].mShoreLeftX = parse[n + 22].ToInt();
                rdungeon.Floors[i].mShoreLeftSheet = parse[n + 23].ToInt();

                rdungeon.Floors[i].mShoreVerticalX = parse[n + 24].ToInt();
                rdungeon.Floors[i].mShoreVerticalSheet = parse[n + 25].ToInt();
                rdungeon.Floors[i].mShoreHorizontalX = parse[n + 26].ToInt();
                rdungeon.Floors[i].mShoreHorizontalSheet = parse[n + 27].ToInt();

                rdungeon.Floors[i].mShoreInnerTopLeftX = parse[n + 28].ToInt();
                rdungeon.Floors[i].mShoreInnerTopLeftSheet = parse[n + 29].ToInt();
                rdungeon.Floors[i].mShoreInnerTopRightX = parse[n + 30].ToInt();
                rdungeon.Floors[i].mShoreInnerTopRightSheet = parse[n + 31].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomRightX = parse[n + 32].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomRightSheet = parse[n + 33].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomLeftX = parse[n + 34].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomLeftSheet = parse[n + 35].ToInt();

                rdungeon.Floors[i].mShoreInnerTopX = parse[n + 36].ToInt();
                rdungeon.Floors[i].mShoreInnerTopSheet = parse[n + 37].ToInt();
                rdungeon.Floors[i].mShoreInnerRightX = parse[n + 38].ToInt();
                rdungeon.Floors[i].mShoreInnerRightSheet = parse[n + 39].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomX = parse[n + 40].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomSheet = parse[n + 41].ToInt();
                rdungeon.Floors[i].mShoreInnerLeftX = parse[n + 42].ToInt();
                rdungeon.Floors[i].mShoreInnerLeftSheet = parse[n + 43].ToInt();
                rdungeon.Floors[i].mShoreSurroundedX = parse[n + 44].ToInt();
                rdungeon.Floors[i].mShoreSurroundedSheet = parse[n + 45].ToInt();

                rdungeon.Floors[i].mShoreTopLeftAnimX = parse[n + 46].ToInt();
                rdungeon.Floors[i].mShoreTopLeftAnimSheet = parse[n + 47].ToInt();
                rdungeon.Floors[i].mShoreTopRightAnimX = parse[n + 48].ToInt();
                rdungeon.Floors[i].mShoreTopRightAnimSheet = parse[n + 49].ToInt();
                rdungeon.Floors[i].mShoreBottomRightAnimX = parse[n + 50].ToInt();
                rdungeon.Floors[i].mShoreBottomRightAnimSheet = parse[n + 51].ToInt();
                rdungeon.Floors[i].mShoreBottomLeftAnimX = parse[n + 52].ToInt();
                rdungeon.Floors[i].mShoreBottomLeftAnimSheet = parse[n + 53].ToInt();

                rdungeon.Floors[i].mShoreDiagonalForwardAnimX = parse[n + 54].ToInt();
                rdungeon.Floors[i].mShoreDiagonalForwardAnimSheet = parse[n + 55].ToInt();
                rdungeon.Floors[i].mShoreDiagonalBackAnimX = parse[n + 56].ToInt();
                rdungeon.Floors[i].mShoreDiagonalBackAnimSheet = parse[n + 57].ToInt();

                rdungeon.Floors[i].mShoreTopAnimX = parse[n + 58].ToInt();
                rdungeon.Floors[i].mShoreTopAnimSheet = parse[n + 59].ToInt();
                rdungeon.Floors[i].mShoreRightAnimX = parse[n + 60].ToInt();
                rdungeon.Floors[i].mShoreRightAnimSheet = parse[n + 61].ToInt();
                rdungeon.Floors[i].mShoreBottomAnimX = parse[n + 62].ToInt();
                rdungeon.Floors[i].mShoreBottomAnimSheet = parse[n + 63].ToInt();
                rdungeon.Floors[i].mShoreLeftAnimX = parse[n + 64].ToInt();
                rdungeon.Floors[i].mShoreLeftAnimSheet = parse[n + 65].ToInt();

                rdungeon.Floors[i].mShoreVerticalAnimX = parse[n + 66].ToInt();
                rdungeon.Floors[i].mShoreVerticalAnimSheet = parse[n + 67].ToInt();
                rdungeon.Floors[i].mShoreHorizontalAnimX = parse[n + 68].ToInt();
                rdungeon.Floors[i].mShoreHorizontalAnimSheet = parse[n + 69].ToInt();

                rdungeon.Floors[i].mShoreInnerTopLeftAnimX = parse[n + 70].ToInt();
                rdungeon.Floors[i].mShoreInnerTopLeftAnimSheet = parse[n + 71].ToInt();
                rdungeon.Floors[i].mShoreInnerTopRightAnimX = parse[n + 72].ToInt();
                rdungeon.Floors[i].mShoreInnerTopRightAnimSheet = parse[n + 73].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomRightAnimX = parse[n + 74].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomRightAnimSheet = parse[n + 75].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomLeftAnimX = parse[n + 76].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomLeftAnimSheet = parse[n + 77].ToInt();

                rdungeon.Floors[i].mShoreInnerTopAnimX = parse[n + 78].ToInt();
                rdungeon.Floors[i].mShoreInnerTopAnimSheet = parse[n + 79].ToInt();
                rdungeon.Floors[i].mShoreInnerRightAnimX = parse[n + 80].ToInt();
                rdungeon.Floors[i].mShoreInnerRightAnimSheet = parse[n + 81].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomAnimX = parse[n + 82].ToInt();
                rdungeon.Floors[i].mShoreInnerBottomAnimSheet = parse[n + 83].ToInt();
                rdungeon.Floors[i].mShoreInnerLeftAnimX = parse[n + 84].ToInt();
                rdungeon.Floors[i].mShoreInnerLeftAnimSheet = parse[n + 85].ToInt();

                rdungeon.Floors[i].mShoreSurroundedAnimX = parse[n + 86].ToInt();
                rdungeon.Floors[i].mShoreSurroundedAnimSheet = parse[n + 87].ToInt();

                n += 88;

                rdungeon.Floors[i].GroundTile.Type = (Enums.TileType)parse[n].ToInt();
                rdungeon.Floors[i].GroundTile.Data1 = parse[n + 1].ToInt();
                rdungeon.Floors[i].GroundTile.Data2 = parse[n + 2].ToInt();
                rdungeon.Floors[i].GroundTile.Data3 = parse[n + 3].ToInt();
                rdungeon.Floors[i].GroundTile.String1 = parse[n + 4];
                rdungeon.Floors[i].GroundTile.String2 = parse[n + 5];
                rdungeon.Floors[i].GroundTile.String3 = parse[n + 6];

                rdungeon.Floors[i].HallTile.Type = (Enums.TileType)parse[n + 7].ToInt();
                rdungeon.Floors[i].HallTile.Data1 = parse[n + 8].ToInt();
                rdungeon.Floors[i].HallTile.Data2 = parse[n + 9].ToInt();
                rdungeon.Floors[i].HallTile.Data3 = parse[n + 10].ToInt();
                rdungeon.Floors[i].HallTile.String1 = parse[n + 11];
                rdungeon.Floors[i].HallTile.String2 = parse[n + 12];
                rdungeon.Floors[i].HallTile.String3 = parse[n + 13];

                rdungeon.Floors[i].WaterTile.Type = (Enums.TileType)parse[n + 14].ToInt();
                rdungeon.Floors[i].WaterTile.Data1 = parse[n + 15].ToInt();
                rdungeon.Floors[i].WaterTile.Data2 = parse[n + 16].ToInt();
                rdungeon.Floors[i].WaterTile.Data3 = parse[n + 17].ToInt();
                rdungeon.Floors[i].WaterTile.String1 = parse[n + 18];
                rdungeon.Floors[i].WaterTile.String2 = parse[n + 19];
                rdungeon.Floors[i].WaterTile.String3 = parse[n + 20];

                rdungeon.Floors[i].WallTile.Type = (Enums.TileType)parse[n + 21].ToInt();
                rdungeon.Floors[i].WallTile.Data1 = parse[n + 22].ToInt();
                rdungeon.Floors[i].WallTile.Data2 = parse[n + 23].ToInt();
                rdungeon.Floors[i].WallTile.Data3 = parse[n + 24].ToInt();
                rdungeon.Floors[i].WallTile.String1 = parse[n + 25];
                rdungeon.Floors[i].WallTile.String2 = parse[n + 26];
                rdungeon.Floors[i].WallTile.String3 = parse[n + 27];

                rdungeon.Floors[i].NpcSpawnTime = parse[n + 28].ToInt();
                rdungeon.Floors[i].NpcMin = parse[n + 29].ToInt();
                rdungeon.Floors[i].NpcMax = parse[n + 30].ToInt();

                n += 31;


                for (int item = 0; item < parse[n].ToInt(); item++) {
                    EditableRDungeonItem newItem = new EditableRDungeonItem();
                    newItem.ItemNum = parse[n + item * 10 + 1].ToInt();
                    newItem.MinAmount = parse[n + item * 10 + 2].ToInt();
                    newItem.MaxAmount = parse[n + item * 10 + 3].ToInt();
                    newItem.AppearanceRate = parse[n + item * 10 + 4].ToInt();
                    newItem.StickyRate = parse[n + item * 10 + 5].ToInt();
                    newItem.Tag = parse[n + item * 10 + 6];
                    newItem.Hidden = parse[n + item * 10 + 7].ToBool();
                    newItem.OnGround = parse[n + item * 10 + 8].ToBool();
                    newItem.OnWater = parse[n + item * 10 + 9].ToBool();
                    newItem.OnWall = parse[n + item * 10 + 10].ToBool();

                    rdungeon.Floors[i].Items.Add(newItem);
                }
                n += rdungeon.Floors[i].Items.Count * 10 + 1;

                for (int npc = 0; npc < parse[n].ToInt(); npc++) {
                    MapNpcSettings newNpc = new MapNpcSettings();
                    newNpc.NpcNum = parse[n + npc * 7 + 1].ToInt();
                    newNpc.MinLevel = parse[n + npc * 7 + 2].ToInt();
                    newNpc.MaxLevel = parse[n + npc * 7 + 3].ToInt();
                    newNpc.AppearanceRate = parse[n + npc * 7 + 4].ToInt();
                    newNpc.StartStatus = (Enums.StatusAilment)parse[n + npc * 7 + 5].ToInt();
                    newNpc.StartStatusCounter = parse[n + npc * 7 + 6].ToInt();
                    newNpc.StartStatusChance = parse[n + npc * 7 + 7].ToInt();

                    rdungeon.Floors[i].Npcs.Add(newNpc);
                }
                n += rdungeon.Floors[i].Npcs.Count * 7 + 1;

                for (int traps = 0; traps < parse[n].ToInt(); traps++) {
                    EditableRDungeonTrap newTile = new EditableRDungeonTrap();
                    newTile.SpecialTile.Type = (Enums.TileType)parse[n + traps * 29 + 1].ToInt();
                    newTile.SpecialTile.Data1 = parse[n + traps * 29 + 2].ToInt();
                    newTile.SpecialTile.Data2 = parse[n + traps * 29 + 3].ToInt();
                    newTile.SpecialTile.Data3 = parse[n + traps * 29 + 4].ToInt();
                    newTile.SpecialTile.String1 = parse[n + traps * 29 + 5];
                    newTile.SpecialTile.String2 = parse[n + traps * 29 + 6];
                    newTile.SpecialTile.String3 = parse[n + traps * 29 + 7];
                    newTile.SpecialTile.Ground = parse[n + traps * 29 + 8].ToInt();
                    newTile.SpecialTile.GroundSet = parse[n + traps * 29 + 9].ToInt();
                    newTile.SpecialTile.GroundAnim = parse[n + traps * 29 + 10].ToInt();
                    newTile.SpecialTile.GroundAnimSet = parse[n + traps * 29 + 11].ToInt();
                    newTile.SpecialTile.Mask = parse[n + traps * 29 + 12].ToInt();
                    newTile.SpecialTile.MaskSet = parse[n + traps * 29 + 13].ToInt();
                    newTile.SpecialTile.Anim = parse[n + traps * 29 + 14].ToInt();
                    newTile.SpecialTile.AnimSet = parse[n + traps * 29 + 15].ToInt();
                    newTile.SpecialTile.Mask2 = parse[n + traps * 29 + 16].ToInt();
                    newTile.SpecialTile.Mask2Set = parse[n + traps * 29 + 17].ToInt();
                    newTile.SpecialTile.M2Anim = parse[n + traps * 29 + 18].ToInt();
                    newTile.SpecialTile.M2AnimSet = parse[n + traps * 29 + 19].ToInt();
                    newTile.SpecialTile.Fringe = parse[n + traps * 29 + 20].ToInt();
                    newTile.SpecialTile.FringeSet = parse[n + traps * 29 + 21].ToInt();
                    newTile.SpecialTile.FAnim = parse[n + traps * 29 + 22].ToInt();
                    newTile.SpecialTile.FAnimSet = parse[n + traps * 29 + 23].ToInt();
                    newTile.SpecialTile.Fringe2 = parse[n + traps * 29 + 24].ToInt();
                    newTile.SpecialTile.Fringe2Set = parse[n + traps * 29 + 25].ToInt();
                    newTile.SpecialTile.F2Anim = parse[n + traps * 29 + 26].ToInt();
                    newTile.SpecialTile.F2AnimSet = parse[n + traps * 29 + 27].ToInt();
                    newTile.SpecialTile.RDungeonMapValue = parse[n + traps * 29 + 28].ToInt();
                    newTile.AppearanceRate = parse[n + traps * 29 + 29].ToInt();

                    rdungeon.Floors[i].SpecialTiles.Add(newTile);

                }
                n += rdungeon.Floors[i].SpecialTiles.Count * 29 + 1;

                for (int weather = 0; weather < parse[n].ToInt(); weather++) {
                    rdungeon.Floors[i].Weather.Add((Enums.Weather)parse[n + 1 + weather].ToInt());

                }
                n += rdungeon.Floors[i].Weather.Count + 1;

                for (int chamber = 0; chamber < parse[n].ToInt(); chamber++) {
                    EditableRDungeonChamber newChamber = new EditableRDungeonChamber();
                    newChamber.ChamberNum = parse[n + chamber * 4 + 1].ToInt();
                    newChamber.String1 = parse[n + chamber * 4 + 2];
                    newChamber.String2 = parse[n + chamber * 4 + 3];
                    newChamber.String3 = parse[n + chamber * 4 + 4];
                    rdungeon.Floors[i].Chambers.Add(newChamber);
                }
                n += rdungeon.Floors[i].Chambers.Count * 4 + 1;

            }

            pnlRDungeonList.Visible = false;
            btnEdit.Text = "Edit";

            txtDungeonName.Text = rdungeon.DungeonName;
            if (rdungeon.Direction == Enums.Direction.Up) {
                optUp.Checked = true;
            } else {
                optDown.Checked = true;
            }
            nudMaxFloors.Value = rdungeon.MaxFloors;
            chkRecruiting.Checked = rdungeon.Recruitment;
            chkEXPGain.Checked = rdungeon.Exp;
            nudWindTimer.Value = rdungeon.WindTimer;

            pnlRDungeonGeneral.Visible = true;
            this.Size = new System.Drawing.Size(pnlRDungeonGeneral.Width, pnlRDungeonGeneral.Height);

            //btnGeneral.Visible = true;
            //btnFloors.Visible = true;
            //btnTerrain.Visible = true;
            //btnGeneral.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            //btnFloors.Location = new Point((pnlRDungeonGeneral.Width - 21) / 3 + 10, 0);
            //btnFloors.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            //btnFloors.Visible = true;
            //btnTerrain.Location = new Point((pnlRDungeonGeneral.Width - 21) / 3 * 2 + 10, 0);
            //btnTerrain.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            //btnGeneral.Selected = true;

            //this.TitleBar.Text = "General Random Dungeon Options";

        }

        void btnFloors_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonFloors();
            LoadpnlRDungeonFloorSettingSelection();

            pnlRDungeonGeneral.Visible = false;
            pnlRDungeonFloors.Visible = true;
            if (pnlRDungeonStructure != null) pnlRDungeonStructure.Visible = false;
            if (pnlRDungeonLandTiles != null) pnlRDungeonLandTiles.Visible = false;
            if (pnlRDungeonLandAltTiles != null) pnlRDungeonLandAltTiles.Visible = false;
            if (pnlRDungeonWaterTiles != null) pnlRDungeonWaterTiles.Visible = false;
            if (pnlRDungeonWaterAnimTiles != null) pnlRDungeonWaterAnimTiles.Visible = false;
            if (pnlRDungeonAttributes != null) pnlRDungeonAttributes.Visible = false;
            if (pnlRDungeonItems != null) pnlRDungeonItems.Visible = false;
            if (pnlRDungeonNpcs != null) pnlRDungeonNpcs.Visible = false;
            if (pnlRDungeonTraps != null) pnlRDungeonTraps.Visible = false;
            if (pnlRDungeonWeather != null) pnlRDungeonWeather.Visible = false;
            if (pnlRDungeonGoal != null) pnlRDungeonGoal.Visible = false;
            if (pnlRDungeonChambers != null) pnlRDungeonChambers.Visible = false;
            if (pnlRDungeonMisc != null) pnlRDungeonMisc.Visible = false;
            if (pnlTileSelector != null) pnlTileSelector.Visible = false;
            pnlRDungeonFloorSettingSelection.Visible = true;
            btnSaveFloor.Text = "Save All Settings to Floor(s)";
            btnLoadFloor.Text = "Load All Settings from Floor";

            this.Size = new System.Drawing.Size(pnlRDungeonFloors.Width, pnlRDungeonFloors.Height);
            this.TitleBar.Text = "Random Dungeon Floor Options";

        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            rdungeon = null;
            pnlRDungeonGeneral.Visible = false;
            pnlRDungeonList.Visible = true;
            this.Size = new System.Drawing.Size(pnlRDungeonList.Width, pnlRDungeonList.Height);
            this.TitleBar.Text = "Random Dungeon Panel";
        }

        void btnSave_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            rdungeon.DungeonName = txtDungeonName.Text;
            if (optUp.Checked) {
                rdungeon.Direction = Enums.Direction.Up;
            } else {
                rdungeon.Direction = Enums.Direction.Down;
            }
            rdungeon.MaxFloors = nudMaxFloors.Value;
            rdungeon.Recruitment = chkRecruiting.Checked;
            rdungeon.Exp = chkEXPGain.Checked;
            rdungeon.WindTimer = nudWindTimer.Value;

            Messenger.SendSaveRDungeon(rdungeon);


            rdungeon = null;
            pnlRDungeonGeneral.Visible = false;
            pnlRDungeonList.Visible = true;
            this.Size = new System.Drawing.Size(pnlRDungeonList.Width, pnlRDungeonList.Height);
            this.TitleBar.Text = "Random Dungeon Panel";

        }

        #endregion

        #region Floors
        void btnGeneral_Click(object sender, MouseButtonEventArgs e) {
            //if (!btnGeneral.Selected) {
            //btnGeneral.Selected = true;
            //btnFloors.Selected = false;
            //btnTerrain.Selected = false;
            //btnGeneral.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            //btnFloors.Location = new Point((pnlRDungeonGeneral.Width - 21) / 3 + 10, 0);
            //btnFloors.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            //btnTerrain.Location = new Point((pnlRDungeonGeneral.Width - 21) / 3 * 2 + 10, 0);
            //btnTerrain.Size = new Size((pnlRDungeonGeneral.Width - 21) / 3, 32);
            pnlRDungeonGeneral.Visible = true;
            pnlRDungeonFloors.Visible = false;
            //pnlRDungeonTerrain.Visible = false;
            this.Size = new System.Drawing.Size(pnlRDungeonGeneral.Width, pnlRDungeonGeneral.Height + 32);
            this.TitleBar.Text = "General Random Dungeon Options";
            //}
        }

        void nudFirstFloor_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            if (nudFirstFloor.Value > nudLastFloor.Value) {
                nudLastFloor.Value = nudFirstFloor.Value;
            }
        }

        void nudLastFloor_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            if (nudFirstFloor.Value > nudLastFloor.Value) {
                nudFirstFloor.Value = nudLastFloor.Value;
            }
        }

        void btnSettingsMenu_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            pnlRDungeonGeneral.Visible = false;
            pnlRDungeonFloors.Visible = true;
            if (pnlRDungeonStructure != null) pnlRDungeonStructure.Visible = false;
            if (pnlRDungeonLandTiles != null) pnlRDungeonLandTiles.Visible = false;
            if (pnlRDungeonLandAltTiles != null) pnlRDungeonLandAltTiles.Visible = false;
            if (pnlRDungeonWaterTiles != null) pnlRDungeonWaterTiles.Visible = false;
            if (pnlRDungeonWaterAnimTiles != null) pnlRDungeonWaterAnimTiles.Visible = false;
            if (pnlRDungeonAttributes != null) pnlRDungeonAttributes.Visible = false;
            if (pnlRDungeonItems != null) pnlRDungeonItems.Visible = false;
            if (pnlRDungeonNpcs != null) pnlRDungeonNpcs.Visible = false;
            if (pnlRDungeonTraps != null) pnlRDungeonTraps.Visible = false;
            if (pnlRDungeonWeather != null) pnlRDungeonWeather.Visible = false;
            if (pnlRDungeonGoal != null) pnlRDungeonGoal.Visible = false;
            if (pnlRDungeonChambers != null) pnlRDungeonChambers.Visible = false;
            if (pnlRDungeonMisc != null) pnlRDungeonMisc.Visible = false;
            if (pnlTileSelector != null) pnlTileSelector.Visible = false;
            pnlRDungeonFloorSettingSelection.Visible = true;
            btnSaveFloor.Text = "Save All Settings to Floor(s)";
            btnLoadFloor.Text = "Load All Settings from Floor";
        }

        void btnSaveFloor_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            for (int i = rdungeon.Floors.Count; i < nudMaxFloors.Value; i++) {
                rdungeon.Floors.Add(new EditableRDungeonFloor());
            }

            if (nudFirstFloor.Value > nudMaxFloors.Value || nudLastFloor.Value > nudMaxFloors.Value) {
                lblSaveLoadMessage.Text = "Cannot save floor(s) above the maximum.";
                return;
            }

            if (pnlRDungeonFloorSettingSelection.Visible == true) {
                LoadpnlRDungeonStructure();
                LoadpnlRDungeonLandTiles();
                LoadpnlRDungeonLandAltTiles();
                LoadpnlRDungeonWaterTiles();
                LoadpnlRDungeonWaterAnimTiles();
                LoadpnlRDungeonAttributes();
                LoadpnlRDungeonItems();
                LoadpnlRDungeonNpcs();
                LoadpnlRDungeonTraps();
                LoadpnlRDungeonWeather();
                LoadpnlRDungeonGoal();
                LoadpnlRDungeonChambers();
                LoadpnlRDungeonMisc();

                //Structure
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].TrapMin = nudTrapMin.Value;
                    rdungeon.Floors[floor].TrapMax = nudTrapMax.Value;
                    rdungeon.Floors[floor].ItemMin = nudItemMin.Value;
                    rdungeon.Floors[floor].ItemMax = nudItemMax.Value;
                    rdungeon.Floors[floor].RoomWidthMin = nudRoomWidthMin.Value;
                    rdungeon.Floors[floor].RoomWidthMax = nudRoomWidthMax.Value;
                    rdungeon.Floors[floor].RoomLengthMin = nudRoomLengthMin.Value;
                    rdungeon.Floors[floor].RoomLengthMax = nudRoomLengthMax.Value;
                    rdungeon.Floors[floor].HallTurnMin = nudHallTurnMin.Value;
                    rdungeon.Floors[floor].HallTurnMax = nudHallTurnMax.Value;
                    rdungeon.Floors[floor].HallVarMin = nudHallVarMin.Value;
                    rdungeon.Floors[floor].HallVarMax = nudHallVarMax.Value;
                    rdungeon.Floors[floor].WaterFrequency = nudWaterFrequency.Value;
                    rdungeon.Floors[floor].Craters = nudCraters.Value;
                    rdungeon.Floors[floor].CraterMinLength = nudCraterMinLength.Value;
                    rdungeon.Floors[floor].CraterMaxLength = nudCraterMaxLength.Value;
                    rdungeon.Floors[floor].CraterFuzzy = chkCraterFuzzy.Checked;


                    //Land Tiles
                    rdungeon.Floors[floor].StairsSheet = landTileNumbers[0, 19];
                    rdungeon.Floors[floor].mGroundSheet = landTileNumbers[0, 16];
                    rdungeon.Floors[floor].mTopLeftSheet = landTileNumbers[0, 10];
                    rdungeon.Floors[floor].mTopCenterSheet = landTileNumbers[0, 6];
                    rdungeon.Floors[floor].mTopRightSheet = landTileNumbers[0, 2];
                    rdungeon.Floors[floor].mCenterLeftSheet = landTileNumbers[0, 9];
                    rdungeon.Floors[floor].mCenterCenterSheet = landTileNumbers[0, 5];
                    rdungeon.Floors[floor].mCenterRightSheet = landTileNumbers[0, 1];
                    rdungeon.Floors[floor].mBottomLeftSheet = landTileNumbers[0, 8];
                    rdungeon.Floors[floor].mBottomCenterSheet = landTileNumbers[0, 4];
                    rdungeon.Floors[floor].mBottomRightSheet = landTileNumbers[0, 0];
                    rdungeon.Floors[floor].mInnerTopLeftSheet = landTileNumbers[0, 17];
                    rdungeon.Floors[floor].mInnerTopRightSheet = landTileNumbers[0, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftSheet = landTileNumbers[0, 18];
                    rdungeon.Floors[floor].mInnerBottomRightSheet = landTileNumbers[0, 21];
                    rdungeon.Floors[floor].mIsolatedWallSheet = landTileNumbers[0, 15];
                    rdungeon.Floors[floor].mColumnTopSheet = landTileNumbers[0, 12];
                    rdungeon.Floors[floor].mColumnCenterSheet = landTileNumbers[0, 13];
                    rdungeon.Floors[floor].mColumnBottomSheet = landTileNumbers[0, 14];
                    rdungeon.Floors[floor].mRowLeftSheet = landTileNumbers[0, 3];
                    rdungeon.Floors[floor].mRowCenterSheet = landTileNumbers[0, 7];
                    rdungeon.Floors[floor].mRowRightSheet = landTileNumbers[0, 11];

                    rdungeon.Floors[floor].StairsX = landTileNumbers[1, 19];
                    rdungeon.Floors[floor].mGroundX = landTileNumbers[1, 16];
                    rdungeon.Floors[floor].mTopLeftX = landTileNumbers[1, 10];
                    rdungeon.Floors[floor].mTopCenterX = landTileNumbers[1, 6];
                    rdungeon.Floors[floor].mTopRightX = landTileNumbers[1, 2];
                    rdungeon.Floors[floor].mCenterLeftX = landTileNumbers[1, 9];
                    rdungeon.Floors[floor].mCenterCenterX = landTileNumbers[1, 5];
                    rdungeon.Floors[floor].mCenterRightX = landTileNumbers[1, 1];
                    rdungeon.Floors[floor].mBottomLeftX = landTileNumbers[1, 8];
                    rdungeon.Floors[floor].mBottomCenterX = landTileNumbers[1, 4];
                    rdungeon.Floors[floor].mBottomRightX = landTileNumbers[1, 0];
                    rdungeon.Floors[floor].mInnerTopLeftX = landTileNumbers[1, 17];
                    rdungeon.Floors[floor].mInnerTopRightX = landTileNumbers[1, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftX = landTileNumbers[1, 18];
                    rdungeon.Floors[floor].mInnerBottomRightX = landTileNumbers[1, 21];
                    rdungeon.Floors[floor].mIsolatedWallX = landTileNumbers[1, 15];
                    rdungeon.Floors[floor].mColumnTopX = landTileNumbers[1, 12];
                    rdungeon.Floors[floor].mColumnCenterX = landTileNumbers[1, 13];
                    rdungeon.Floors[floor].mColumnBottomX = landTileNumbers[1, 14];
                    rdungeon.Floors[floor].mRowLeftX = landTileNumbers[1, 3];
                    rdungeon.Floors[floor].mRowCenterX = landTileNumbers[1, 7];
                    rdungeon.Floors[floor].mRowRightX = landTileNumbers[1, 11];

                    //Land Alt Tiles
                    rdungeon.Floors[floor].mGroundAlt2Sheet = landAltTileNumbers[0, 19];
                    rdungeon.Floors[floor].mGroundAltSheet = landAltTileNumbers[0, 16];
                    rdungeon.Floors[floor].mTopLeftAltSheet = landAltTileNumbers[0, 10];
                    rdungeon.Floors[floor].mTopCenterAltSheet = landAltTileNumbers[0, 6];
                    rdungeon.Floors[floor].mTopRightAltSheet = landAltTileNumbers[0, 2];
                    rdungeon.Floors[floor].mCenterLeftAltSheet = landAltTileNumbers[0, 9];
                    rdungeon.Floors[floor].mCenterCenterAltSheet = landAltTileNumbers[0, 5];
                    rdungeon.Floors[floor].mCenterCenterAlt2Sheet = landAltTileNumbers[0, 22];
                    rdungeon.Floors[floor].mCenterRightAltSheet = landAltTileNumbers[0, 1];
                    rdungeon.Floors[floor].mBottomLeftAltSheet = landAltTileNumbers[0, 8];
                    rdungeon.Floors[floor].mBottomCenterAltSheet = landAltTileNumbers[0, 4];
                    rdungeon.Floors[floor].mBottomRightAltSheet = landAltTileNumbers[0, 0];
                    rdungeon.Floors[floor].mInnerTopLeftAltSheet = landAltTileNumbers[0, 17];
                    rdungeon.Floors[floor].mInnerTopRightAltSheet = landAltTileNumbers[0, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftAltSheet = landAltTileNumbers[0, 18];
                    rdungeon.Floors[floor].mInnerBottomRightAltSheet = landAltTileNumbers[0, 21];
                    rdungeon.Floors[floor].mIsolatedWallAltSheet = landAltTileNumbers[0, 15];
                    rdungeon.Floors[floor].mColumnTopAltSheet = landAltTileNumbers[0, 12];
                    rdungeon.Floors[floor].mColumnCenterAltSheet = landAltTileNumbers[0, 13];
                    rdungeon.Floors[floor].mColumnBottomAltSheet = landAltTileNumbers[0, 14];
                    rdungeon.Floors[floor].mRowLeftAltSheet = landAltTileNumbers[0, 3];
                    rdungeon.Floors[floor].mRowCenterAltSheet = landAltTileNumbers[0, 7];
                    rdungeon.Floors[floor].mRowRightAltSheet = landAltTileNumbers[0, 11];

                    rdungeon.Floors[floor].mGroundAlt2X = landAltTileNumbers[1, 19];
                    rdungeon.Floors[floor].mGroundAltX = landAltTileNumbers[1, 16];
                    rdungeon.Floors[floor].mTopLeftAltX = landAltTileNumbers[1, 10];
                    rdungeon.Floors[floor].mTopCenterAltX = landAltTileNumbers[1, 6];
                    rdungeon.Floors[floor].mTopRightAltX = landAltTileNumbers[1, 2];
                    rdungeon.Floors[floor].mCenterLeftAltX = landAltTileNumbers[1, 9];
                    rdungeon.Floors[floor].mCenterCenterAltX = landAltTileNumbers[1, 5];
                    rdungeon.Floors[floor].mCenterCenterAlt2X = landAltTileNumbers[1, 22];
                    rdungeon.Floors[floor].mCenterRightAltX = landAltTileNumbers[1, 1];
                    rdungeon.Floors[floor].mBottomLeftAltX = landAltTileNumbers[1, 8];
                    rdungeon.Floors[floor].mBottomCenterAltX = landAltTileNumbers[1, 4];
                    rdungeon.Floors[floor].mBottomRightAltX = landAltTileNumbers[1, 0];
                    rdungeon.Floors[floor].mInnerTopLeftAltX = landAltTileNumbers[1, 17];
                    rdungeon.Floors[floor].mInnerTopRightAltX = landAltTileNumbers[1, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftAltX = landAltTileNumbers[1, 18];
                    rdungeon.Floors[floor].mInnerBottomRightAltX = landAltTileNumbers[1, 21];
                    rdungeon.Floors[floor].mIsolatedWallAltX = landAltTileNumbers[1, 15];
                    rdungeon.Floors[floor].mColumnTopAltX = landAltTileNumbers[1, 12];
                    rdungeon.Floors[floor].mColumnCenterAltX = landAltTileNumbers[1, 13];
                    rdungeon.Floors[floor].mColumnBottomAltX = landAltTileNumbers[1, 14];
                    rdungeon.Floors[floor].mRowLeftAltX = landAltTileNumbers[1, 3];
                    rdungeon.Floors[floor].mRowCenterAltX = landAltTileNumbers[1, 7];
                    rdungeon.Floors[floor].mRowRightAltX = landAltTileNumbers[1, 11];

                    //Water Tiles
                    rdungeon.Floors[floor].mShoreSurroundedSheet = waterTileNumbers[0, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftSheet = waterTileNumbers[0, 0];
                    rdungeon.Floors[floor].mShoreTopSheet = waterTileNumbers[0, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightSheet = waterTileNumbers[0, 8];
                    rdungeon.Floors[floor].mShoreLeftSheet = waterTileNumbers[0, 1];
                    rdungeon.Floors[floor].mWaterSheet = waterTileNumbers[0, 5];
                    rdungeon.Floors[floor].mShoreRightSheet = waterTileNumbers[0, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftSheet = waterTileNumbers[0, 2];
                    rdungeon.Floors[floor].mShoreBottomSheet = waterTileNumbers[0, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightSheet = waterTileNumbers[0, 10];
                    rdungeon.Floors[floor].mShoreTopLeftSheet = waterTileNumbers[0, 16];
                    rdungeon.Floors[floor].mShoreTopRightSheet = waterTileNumbers[0, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftSheet = waterTileNumbers[0, 17];
                    rdungeon.Floors[floor].mShoreBottomRightSheet = waterTileNumbers[0, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardSheet = waterTileNumbers[0, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackSheet = waterTileNumbers[0, 21];
                    rdungeon.Floors[floor].mShoreInnerTopSheet = waterTileNumbers[0, 12];
                    rdungeon.Floors[floor].mShoreVerticalSheet = waterTileNumbers[0, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomSheet = waterTileNumbers[0, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftSheet = waterTileNumbers[0, 3];
                    rdungeon.Floors[floor].mShoreHorizontalSheet = waterTileNumbers[0, 7];
                    rdungeon.Floors[floor].mShoreInnerRightSheet = waterTileNumbers[0, 11];

                    rdungeon.Floors[floor].mShoreSurroundedX = waterTileNumbers[1, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftX = waterTileNumbers[1, 0];
                    rdungeon.Floors[floor].mShoreTopX = waterTileNumbers[1, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightX = waterTileNumbers[1, 8];
                    rdungeon.Floors[floor].mShoreLeftX = waterTileNumbers[1, 1];
                    rdungeon.Floors[floor].mWaterX = waterTileNumbers[1, 5];
                    rdungeon.Floors[floor].mShoreRightX = waterTileNumbers[1, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftX = waterTileNumbers[1, 2];
                    rdungeon.Floors[floor].mShoreBottomX = waterTileNumbers[1, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightX = waterTileNumbers[1, 10];
                    rdungeon.Floors[floor].mShoreTopLeftX = waterTileNumbers[1, 16];
                    rdungeon.Floors[floor].mShoreTopRightX = waterTileNumbers[1, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftX = waterTileNumbers[1, 17];
                    rdungeon.Floors[floor].mShoreBottomRightX = waterTileNumbers[1, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardX = waterTileNumbers[1, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackX = waterTileNumbers[1, 21];
                    rdungeon.Floors[floor].mShoreInnerTopX = waterTileNumbers[1, 12];
                    rdungeon.Floors[floor].mShoreVerticalX = waterTileNumbers[1, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomX = waterTileNumbers[1, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftX = waterTileNumbers[1, 3];
                    rdungeon.Floors[floor].mShoreHorizontalX = waterTileNumbers[1, 7];
                    rdungeon.Floors[floor].mShoreInnerRightX = waterTileNumbers[1, 11];


                    //Water Anim Tiles
                    rdungeon.Floors[floor].mShoreSurroundedAnimSheet = waterAnimTileNumbers[0, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftAnimSheet = waterAnimTileNumbers[0, 0];
                    rdungeon.Floors[floor].mShoreTopAnimSheet = waterAnimTileNumbers[0, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightAnimSheet = waterAnimTileNumbers[0, 8];
                    rdungeon.Floors[floor].mShoreLeftAnimSheet = waterAnimTileNumbers[0, 1];
                    rdungeon.Floors[floor].mWaterAnimSheet = waterAnimTileNumbers[0, 5];
                    rdungeon.Floors[floor].mShoreRightAnimSheet = waterAnimTileNumbers[0, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftAnimSheet = waterAnimTileNumbers[0, 2];
                    rdungeon.Floors[floor].mShoreBottomAnimSheet = waterAnimTileNumbers[0, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightAnimSheet = waterAnimTileNumbers[0, 10];
                    rdungeon.Floors[floor].mShoreTopLeftAnimSheet = waterAnimTileNumbers[0, 16];
                    rdungeon.Floors[floor].mShoreTopRightAnimSheet = waterAnimTileNumbers[0, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftAnimSheet = waterAnimTileNumbers[0, 17];
                    rdungeon.Floors[floor].mShoreBottomRightAnimSheet = waterAnimTileNumbers[0, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardAnimSheet = waterAnimTileNumbers[0, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackAnimSheet = waterAnimTileNumbers[0, 21];
                    rdungeon.Floors[floor].mShoreInnerTopAnimSheet = waterAnimTileNumbers[0, 12];
                    rdungeon.Floors[floor].mShoreVerticalAnimSheet = waterAnimTileNumbers[0, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomAnimSheet = waterAnimTileNumbers[0, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftAnimSheet = waterAnimTileNumbers[0, 3];
                    rdungeon.Floors[floor].mShoreHorizontalAnimSheet = waterAnimTileNumbers[0, 7];
                    rdungeon.Floors[floor].mShoreInnerRightAnimSheet = waterAnimTileNumbers[0, 11];

                    rdungeon.Floors[floor].mShoreSurroundedAnimX = waterAnimTileNumbers[1, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftAnimX = waterAnimTileNumbers[1, 0];
                    rdungeon.Floors[floor].mShoreTopAnimX = waterAnimTileNumbers[1, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightAnimX = waterAnimTileNumbers[1, 8];
                    rdungeon.Floors[floor].mShoreLeftAnimX = waterAnimTileNumbers[1, 1];
                    rdungeon.Floors[floor].mWaterAnimX = waterAnimTileNumbers[1, 5];
                    rdungeon.Floors[floor].mShoreRightAnimX = waterAnimTileNumbers[1, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftAnimX = waterAnimTileNumbers[1, 2];
                    rdungeon.Floors[floor].mShoreBottomAnimX = waterAnimTileNumbers[1, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightAnimX = waterAnimTileNumbers[1, 10];
                    rdungeon.Floors[floor].mShoreTopLeftAnimX = waterAnimTileNumbers[1, 16];
                    rdungeon.Floors[floor].mShoreTopRightAnimX = waterAnimTileNumbers[1, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftAnimX = waterAnimTileNumbers[1, 17];
                    rdungeon.Floors[floor].mShoreBottomRightAnimX = waterAnimTileNumbers[1, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardAnimX = waterAnimTileNumbers[1, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackAnimX = waterAnimTileNumbers[1, 21];
                    rdungeon.Floors[floor].mShoreInnerTopAnimX = waterAnimTileNumbers[1, 12];
                    rdungeon.Floors[floor].mShoreVerticalAnimX = waterAnimTileNumbers[1, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomAnimX = waterAnimTileNumbers[1, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftAnimX = waterAnimTileNumbers[1, 3];
                    rdungeon.Floors[floor].mShoreHorizontalAnimX = waterAnimTileNumbers[1, 7];
                    rdungeon.Floors[floor].mShoreInnerRightAnimX = waterAnimTileNumbers[1, 11];

                    //Attributes
                    rdungeon.Floors[floor].GroundTile.Type = (Enums.TileType)cbGroundType.SelectedIndex;
                    rdungeon.Floors[floor].GroundTile.Data1 = nudGroundData1.Value;
                    rdungeon.Floors[floor].GroundTile.Data2 = nudGroundData2.Value;
                    rdungeon.Floors[floor].GroundTile.Data3 = nudGroundData3.Value;
                    rdungeon.Floors[floor].GroundTile.String1 = txtGroundString1.Text;
                    rdungeon.Floors[floor].GroundTile.String2 = txtGroundString2.Text;
                    rdungeon.Floors[floor].GroundTile.String3 = txtGroundString3.Text;

                    rdungeon.Floors[floor].HallTile.Type = (Enums.TileType)cbHallType.SelectedIndex;
                    rdungeon.Floors[floor].HallTile.Data1 = nudHallData1.Value;
                    rdungeon.Floors[floor].HallTile.Data2 = nudHallData2.Value;
                    rdungeon.Floors[floor].HallTile.Data3 = nudHallData3.Value;
                    rdungeon.Floors[floor].HallTile.String1 = txtHallString1.Text;
                    rdungeon.Floors[floor].HallTile.String2 = txtHallString2.Text;
                    rdungeon.Floors[floor].HallTile.String3 = txtHallString3.Text;

                    rdungeon.Floors[floor].WaterTile.Type = (Enums.TileType)cbWaterType.SelectedIndex;
                    rdungeon.Floors[floor].WaterTile.Data1 = nudWaterData1.Value;
                    rdungeon.Floors[floor].WaterTile.Data2 = nudWaterData2.Value;
                    rdungeon.Floors[floor].WaterTile.Data3 = nudWaterData3.Value;
                    rdungeon.Floors[floor].WaterTile.String1 = txtWaterString1.Text;
                    rdungeon.Floors[floor].WaterTile.String2 = txtWaterString2.Text;
                    rdungeon.Floors[floor].WaterTile.String3 = txtWaterString3.Text;

                    rdungeon.Floors[floor].WallTile.Type = (Enums.TileType)cbWallType.SelectedIndex;
                    rdungeon.Floors[floor].WallTile.Data1 = nudWallData1.Value;
                    rdungeon.Floors[floor].WallTile.Data2 = nudWallData2.Value;
                    rdungeon.Floors[floor].WallTile.Data3 = nudWallData3.Value;
                    rdungeon.Floors[floor].WallTile.String1 = txtWallString1.Text;
                    rdungeon.Floors[floor].WallTile.String2 = txtWallString2.Text;
                    rdungeon.Floors[floor].WallTile.String3 = txtWallString3.Text;

                    //Items
                    rdungeon.Floors[floor].Items.Clear();
                    for (int item = 0; item < itemList.Count; item++) {
                        EditableRDungeonItem newItem = new EditableRDungeonItem();
                        newItem.ItemNum = itemList[item].ItemNum;
                        newItem.MinAmount = itemList[item].MinAmount;
                        newItem.MaxAmount = itemList[item].MaxAmount;
                        newItem.AppearanceRate = itemList[item].AppearanceRate;
                        newItem.StickyRate = itemList[item].StickyRate;
                        newItem.Tag = itemList[item].Tag;
                        newItem.Hidden = itemList[item].Hidden;
                        newItem.OnGround = itemList[item].OnGround;
                        newItem.OnWater = itemList[item].OnWater;
                        newItem.OnWall = itemList[item].OnWall;

                        rdungeon.Floors[floor].Items.Add(newItem);
                    }

                    //Npcs
                    rdungeon.Floors[floor].NpcSpawnTime = nudNpcSpawnTime.Value;
                    rdungeon.Floors[floor].NpcMin = nudNpcMin.Value;
                    rdungeon.Floors[floor].NpcMax = nudNpcMax.Value;

                    rdungeon.Floors[floor].Npcs.Clear();
                    for (int npc = 0; npc < npcList.Count; npc++) {
                        MapNpcSettings newNpc = new MapNpcSettings();
                        newNpc.NpcNum = npcList[npc].NpcNum;
                        newNpc.MinLevel = npcList[npc].MinLevel;
                        newNpc.MaxLevel = npcList[npc].MaxLevel;
                        newNpc.AppearanceRate = npcList[npc].AppearanceRate;
                        newNpc.StartStatus = npcList[npc].StartStatus;
                        newNpc.StartStatusCounter = npcList[npc].StartStatusCounter;
                        newNpc.StartStatusChance = npcList[npc].StartStatusChance;

                        rdungeon.Floors[floor].Npcs.Add(newNpc);
                    }

                    //Traps
                    rdungeon.Floors[floor].SpecialTiles.Clear();
                    for (int traps = 0; traps < trapList.Count; traps++) {
                        EditableRDungeonTrap newTile = new EditableRDungeonTrap();

                        newTile.SpecialTile.Ground = trapList[traps].SpecialTile.Ground;
                        newTile.SpecialTile.GroundAnim = trapList[traps].SpecialTile.GroundAnim;
                        newTile.SpecialTile.Mask = trapList[traps].SpecialTile.Mask;
                        newTile.SpecialTile.Anim = trapList[traps].SpecialTile.Anim;
                        newTile.SpecialTile.Mask2 = trapList[traps].SpecialTile.Mask2;
                        newTile.SpecialTile.M2Anim = trapList[traps].SpecialTile.M2Anim;
                        newTile.SpecialTile.Fringe = trapList[traps].SpecialTile.Fringe;
                        newTile.SpecialTile.FAnim = trapList[traps].SpecialTile.FAnim;
                        newTile.SpecialTile.Fringe2 = trapList[traps].SpecialTile.Fringe2;
                        newTile.SpecialTile.F2Anim = trapList[traps].SpecialTile.F2Anim;

                        newTile.SpecialTile.GroundSet = trapList[traps].SpecialTile.GroundSet;
                        newTile.SpecialTile.GroundAnimSet = trapList[traps].SpecialTile.GroundAnimSet;
                        newTile.SpecialTile.MaskSet = trapList[traps].SpecialTile.MaskSet;
                        newTile.SpecialTile.AnimSet = trapList[traps].SpecialTile.AnimSet;
                        newTile.SpecialTile.Mask2Set = trapList[traps].SpecialTile.Mask2Set;
                        newTile.SpecialTile.M2AnimSet = trapList[traps].SpecialTile.M2AnimSet;
                        newTile.SpecialTile.FringeSet = trapList[traps].SpecialTile.FringeSet;
                        newTile.SpecialTile.FAnimSet = trapList[traps].SpecialTile.FAnimSet;
                        newTile.SpecialTile.Fringe2Set = trapList[traps].SpecialTile.Fringe2Set;
                        newTile.SpecialTile.F2AnimSet = trapList[traps].SpecialTile.F2AnimSet;

                        newTile.SpecialTile.Type = trapList[traps].SpecialTile.Type;
                        newTile.SpecialTile.Data1 = trapList[traps].SpecialTile.Data1;
                        newTile.SpecialTile.Data2 = trapList[traps].SpecialTile.Data2;
                        newTile.SpecialTile.Data3 = trapList[traps].SpecialTile.Data3;
                        newTile.SpecialTile.String1 = trapList[traps].SpecialTile.String1;
                        newTile.SpecialTile.String2 = trapList[traps].SpecialTile.String2;
                        newTile.SpecialTile.String3 = trapList[traps].SpecialTile.String3;

                        newTile.AppearanceRate = trapList[traps].AppearanceRate;

                        rdungeon.Floors[floor].SpecialTiles.Add(newTile);
                    }

                    //Weather
                    rdungeon.Floors[floor].Weather.Clear();
                    for (int weather = 0; weather < lbxWeather.Items.Count; weather++) {
                        string[] weatherindex = lbxWeather.Items[weather].TextIdentifier.Split(':');
                        if (weatherindex[1].IsNumeric()) {
                            rdungeon.Floors[floor].Weather.Add((Enums.Weather)weatherindex[1].ToInt());
                        }
                    }

                    //Goal
                    if (optNextFloor.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.NextFloor;
                    } else if (optMap.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.Map;
                    } else if (optScripted.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.Scripted;
                    } else {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.NextFloor;
                    }

                    rdungeon.Floors[floor].GoalMap = nudData1.Value;
                    rdungeon.Floors[floor].GoalX = nudData2.Value;
                    rdungeon.Floors[floor].GoalY = nudData3.Value;

                    //chambers
                    rdungeon.Floors[floor].Chambers.Clear();
                    for (int chamber = 0; chamber < chamberList.Count; chamber++) {
                        EditableRDungeonChamber addedChamber = new EditableRDungeonChamber();
                        addedChamber.ChamberNum = chamberList[chamber].ChamberNum;
                        addedChamber.String1 = chamberList[chamber].String1;
                        addedChamber.String2 = chamberList[chamber].String2;
                        addedChamber.String3 = chamberList[chamber].String3;
                        rdungeon.Floors[floor].Chambers.Add(addedChamber);

                    }

                    //Misc
                    rdungeon.Floors[floor].Darkness = nudDarkness.Value;
                    if (lbxMusic.SelectedItems.Count != 1 || lbxMusic.Items[0].Selected) {
                        rdungeon.Floors[floor].Music = "";
                    } else {
                        rdungeon.Floors[floor].Music = lbxMusic.SelectedItems[0].TextIdentifier;
                    }


                }





                lblSaveLoadMessage.Text = "All settings saved to floor(s)";
            } else if (pnlRDungeonStructure != null && pnlRDungeonStructure.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].TrapMin = nudTrapMin.Value;
                    rdungeon.Floors[floor].TrapMax = nudTrapMax.Value;
                    rdungeon.Floors[floor].ItemMin = nudItemMin.Value;
                    rdungeon.Floors[floor].ItemMax = nudItemMax.Value;
                    rdungeon.Floors[floor].RoomWidthMin = nudRoomWidthMin.Value;
                    rdungeon.Floors[floor].RoomWidthMax = nudRoomWidthMax.Value;
                    rdungeon.Floors[floor].RoomLengthMin = nudRoomLengthMin.Value;
                    rdungeon.Floors[floor].RoomLengthMax = nudRoomLengthMax.Value;
                    rdungeon.Floors[floor].HallTurnMin = nudHallTurnMin.Value;
                    rdungeon.Floors[floor].HallTurnMax = nudHallTurnMax.Value;
                    rdungeon.Floors[floor].HallVarMin = nudHallVarMin.Value;
                    rdungeon.Floors[floor].HallVarMax = nudHallVarMax.Value;
                    rdungeon.Floors[floor].WaterFrequency = nudWaterFrequency.Value;
                    rdungeon.Floors[floor].Craters = nudCraters.Value;
                    rdungeon.Floors[floor].CraterMinLength = nudCraterMinLength.Value;
                    rdungeon.Floors[floor].CraterMaxLength = nudCraterMaxLength.Value;
                    rdungeon.Floors[floor].CraterFuzzy = chkCraterFuzzy.Checked;

                }
                lblSaveLoadMessage.Text = "Structure settings saved to floor(s)";
            } else if (pnlRDungeonLandTiles != null && pnlRDungeonLandTiles.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].StairsSheet = landTileNumbers[0, 19];
                    rdungeon.Floors[floor].mGroundSheet = landTileNumbers[0, 16];
                    rdungeon.Floors[floor].mTopLeftSheet = landTileNumbers[0, 10];
                    rdungeon.Floors[floor].mTopCenterSheet = landTileNumbers[0, 6];
                    rdungeon.Floors[floor].mTopRightSheet = landTileNumbers[0, 2];
                    rdungeon.Floors[floor].mCenterLeftSheet = landTileNumbers[0, 9];
                    rdungeon.Floors[floor].mCenterCenterSheet = landTileNumbers[0, 5];
                    rdungeon.Floors[floor].mCenterRightSheet = landTileNumbers[0, 1];
                    rdungeon.Floors[floor].mBottomLeftSheet = landTileNumbers[0, 8];
                    rdungeon.Floors[floor].mBottomCenterSheet = landTileNumbers[0, 4];
                    rdungeon.Floors[floor].mBottomRightSheet = landTileNumbers[0, 0];
                    rdungeon.Floors[floor].mInnerTopLeftSheet = landTileNumbers[0, 17];
                    rdungeon.Floors[floor].mInnerTopRightSheet = landTileNumbers[0, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftSheet = landTileNumbers[0, 18];
                    rdungeon.Floors[floor].mInnerBottomRightSheet = landTileNumbers[0, 21];
                    rdungeon.Floors[floor].mIsolatedWallSheet = landTileNumbers[0, 15];
                    rdungeon.Floors[floor].mColumnTopSheet = landTileNumbers[0, 12];
                    rdungeon.Floors[floor].mColumnCenterSheet = landTileNumbers[0, 13];
                    rdungeon.Floors[floor].mColumnBottomSheet = landTileNumbers[0, 14];
                    rdungeon.Floors[floor].mRowLeftSheet = landTileNumbers[0, 3];
                    rdungeon.Floors[floor].mRowCenterSheet = landTileNumbers[0, 7];
                    rdungeon.Floors[floor].mRowRightSheet = landTileNumbers[0, 11];

                    rdungeon.Floors[floor].StairsX = landTileNumbers[1, 19];
                    rdungeon.Floors[floor].mGroundX = landTileNumbers[1, 16];
                    rdungeon.Floors[floor].mTopLeftX = landTileNumbers[1, 10];
                    rdungeon.Floors[floor].mTopCenterX = landTileNumbers[1, 6];
                    rdungeon.Floors[floor].mTopRightX = landTileNumbers[1, 2];
                    rdungeon.Floors[floor].mCenterLeftX = landTileNumbers[1, 9];
                    rdungeon.Floors[floor].mCenterCenterX = landTileNumbers[1, 5];
                    rdungeon.Floors[floor].mCenterRightX = landTileNumbers[1, 1];
                    rdungeon.Floors[floor].mBottomLeftX = landTileNumbers[1, 8];
                    rdungeon.Floors[floor].mBottomCenterX = landTileNumbers[1, 4];
                    rdungeon.Floors[floor].mBottomRightX = landTileNumbers[1, 0];
                    rdungeon.Floors[floor].mInnerTopLeftX = landTileNumbers[1, 17];
                    rdungeon.Floors[floor].mInnerTopRightX = landTileNumbers[1, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftX = landTileNumbers[1, 18];
                    rdungeon.Floors[floor].mInnerBottomRightX = landTileNumbers[1, 21];
                    rdungeon.Floors[floor].mIsolatedWallX = landTileNumbers[1, 15];
                    rdungeon.Floors[floor].mColumnTopX = landTileNumbers[1, 12];
                    rdungeon.Floors[floor].mColumnCenterX = landTileNumbers[1, 13];
                    rdungeon.Floors[floor].mColumnBottomX = landTileNumbers[1, 14];
                    rdungeon.Floors[floor].mRowLeftX = landTileNumbers[1, 3];
                    rdungeon.Floors[floor].mRowCenterX = landTileNumbers[1, 7];
                    rdungeon.Floors[floor].mRowRightX = landTileNumbers[1, 11];

                }
                lblSaveLoadMessage.Text = "Land Tile settings saved to floor(s)";
            } else if (pnlRDungeonLandAltTiles != null && pnlRDungeonLandAltTiles.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                    rdungeon.Floors[floor].mGroundAlt2Sheet = landAltTileNumbers[0, 19];
                    rdungeon.Floors[floor].mGroundAltSheet = landAltTileNumbers[0, 16];
                    rdungeon.Floors[floor].mTopLeftAltSheet = landAltTileNumbers[0, 10];
                    rdungeon.Floors[floor].mTopCenterAltSheet = landAltTileNumbers[0, 6];
                    rdungeon.Floors[floor].mTopRightAltSheet = landAltTileNumbers[0, 2];
                    rdungeon.Floors[floor].mCenterLeftAltSheet = landAltTileNumbers[0, 9];
                    rdungeon.Floors[floor].mCenterCenterAltSheet = landAltTileNumbers[0, 5];
                    rdungeon.Floors[floor].mCenterCenterAlt2Sheet = landAltTileNumbers[0, 22];
                    rdungeon.Floors[floor].mCenterRightAltSheet = landAltTileNumbers[0, 1];
                    rdungeon.Floors[floor].mBottomLeftAltSheet = landAltTileNumbers[0, 8];
                    rdungeon.Floors[floor].mBottomCenterAltSheet = landAltTileNumbers[0, 4];
                    rdungeon.Floors[floor].mBottomRightAltSheet = landAltTileNumbers[0, 0];
                    rdungeon.Floors[floor].mInnerTopLeftAltSheet = landAltTileNumbers[0, 17];
                    rdungeon.Floors[floor].mInnerTopRightAltSheet = landAltTileNumbers[0, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftAltSheet = landAltTileNumbers[0, 18];
                    rdungeon.Floors[floor].mInnerBottomRightAltSheet = landAltTileNumbers[0, 21];
                    rdungeon.Floors[floor].mIsolatedWallAltSheet = landAltTileNumbers[0, 15];
                    rdungeon.Floors[floor].mColumnTopAltSheet = landAltTileNumbers[0, 12];
                    rdungeon.Floors[floor].mColumnCenterAltSheet = landAltTileNumbers[0, 13];
                    rdungeon.Floors[floor].mColumnBottomAltSheet = landAltTileNumbers[0, 14];
                    rdungeon.Floors[floor].mRowLeftAltSheet = landAltTileNumbers[0, 3];
                    rdungeon.Floors[floor].mRowCenterAltSheet = landAltTileNumbers[0, 7];
                    rdungeon.Floors[floor].mRowRightAltSheet = landAltTileNumbers[0, 11];

                    rdungeon.Floors[floor].mGroundAlt2X = landAltTileNumbers[1, 19];
                    rdungeon.Floors[floor].mGroundAltX = landAltTileNumbers[1, 16];
                    rdungeon.Floors[floor].mTopLeftAltX = landAltTileNumbers[1, 10];
                    rdungeon.Floors[floor].mTopCenterAltX = landAltTileNumbers[1, 6];
                    rdungeon.Floors[floor].mTopRightAltX = landAltTileNumbers[1, 2];
                    rdungeon.Floors[floor].mCenterLeftAltX = landAltTileNumbers[1, 9];
                    rdungeon.Floors[floor].mCenterCenterAltX = landAltTileNumbers[1, 5];
                    rdungeon.Floors[floor].mCenterCenterAlt2X = landAltTileNumbers[1, 22];
                    rdungeon.Floors[floor].mCenterRightAltX = landAltTileNumbers[1, 1];
                    rdungeon.Floors[floor].mBottomLeftAltX = landAltTileNumbers[1, 8];
                    rdungeon.Floors[floor].mBottomCenterAltX = landAltTileNumbers[1, 4];
                    rdungeon.Floors[floor].mBottomRightAltX = landAltTileNumbers[1, 0];
                    rdungeon.Floors[floor].mInnerTopLeftAltX = landAltTileNumbers[1, 17];
                    rdungeon.Floors[floor].mInnerTopRightAltX = landAltTileNumbers[1, 20];
                    rdungeon.Floors[floor].mInnerBottomLeftAltX = landAltTileNumbers[1, 18];
                    rdungeon.Floors[floor].mInnerBottomRightAltX = landAltTileNumbers[1, 21];
                    rdungeon.Floors[floor].mIsolatedWallAltX = landAltTileNumbers[1, 15];
                    rdungeon.Floors[floor].mColumnTopAltX = landAltTileNumbers[1, 12];
                    rdungeon.Floors[floor].mColumnCenterAltX = landAltTileNumbers[1, 13];
                    rdungeon.Floors[floor].mColumnBottomAltX = landAltTileNumbers[1, 14];
                    rdungeon.Floors[floor].mRowLeftAltX = landAltTileNumbers[1, 3];
                    rdungeon.Floors[floor].mRowCenterAltX = landAltTileNumbers[1, 7];
                    rdungeon.Floors[floor].mRowRightAltX = landAltTileNumbers[1, 11];

                }
                lblSaveLoadMessage.Text = "Land Alt Tile settings saved to floor(s)";
            } else if (pnlRDungeonWaterTiles != null && pnlRDungeonWaterTiles.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].mShoreSurroundedSheet = waterTileNumbers[0, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftSheet = waterTileNumbers[0, 0];
                    rdungeon.Floors[floor].mShoreTopSheet = waterTileNumbers[0, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightSheet = waterTileNumbers[0, 8];
                    rdungeon.Floors[floor].mShoreLeftSheet = waterTileNumbers[0, 1];
                    rdungeon.Floors[floor].mWaterSheet = waterTileNumbers[0, 5];
                    rdungeon.Floors[floor].mShoreRightSheet = waterTileNumbers[0, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftSheet = waterTileNumbers[0, 2];
                    rdungeon.Floors[floor].mShoreBottomSheet = waterTileNumbers[0, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightSheet = waterTileNumbers[0, 10];
                    rdungeon.Floors[floor].mShoreTopLeftSheet = waterTileNumbers[0, 16];
                    rdungeon.Floors[floor].mShoreTopRightSheet = waterTileNumbers[0, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftSheet = waterTileNumbers[0, 17];
                    rdungeon.Floors[floor].mShoreBottomRightSheet = waterTileNumbers[0, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardSheet = waterTileNumbers[0, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackSheet = waterTileNumbers[0, 21];
                    rdungeon.Floors[floor].mShoreInnerTopSheet = waterTileNumbers[0, 12];
                    rdungeon.Floors[floor].mShoreVerticalSheet = waterTileNumbers[0, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomSheet = waterTileNumbers[0, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftSheet = waterTileNumbers[0, 3];
                    rdungeon.Floors[floor].mShoreHorizontalSheet = waterTileNumbers[0, 7];
                    rdungeon.Floors[floor].mShoreInnerRightSheet = waterTileNumbers[0, 11];

                    rdungeon.Floors[floor].mShoreSurroundedX = waterTileNumbers[1, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftX = waterTileNumbers[1, 0];
                    rdungeon.Floors[floor].mShoreTopX = waterTileNumbers[1, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightX = waterTileNumbers[1, 8];
                    rdungeon.Floors[floor].mShoreLeftX = waterTileNumbers[1, 1];
                    rdungeon.Floors[floor].mWaterX = waterTileNumbers[1, 5];
                    rdungeon.Floors[floor].mShoreRightX = waterTileNumbers[1, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftX = waterTileNumbers[1, 2];
                    rdungeon.Floors[floor].mShoreBottomX = waterTileNumbers[1, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightX = waterTileNumbers[1, 10];
                    rdungeon.Floors[floor].mShoreTopLeftX = waterTileNumbers[1, 16];
                    rdungeon.Floors[floor].mShoreTopRightX = waterTileNumbers[1, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftX = waterTileNumbers[1, 17];
                    rdungeon.Floors[floor].mShoreBottomRightX = waterTileNumbers[1, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardX = waterTileNumbers[1, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackX = waterTileNumbers[1, 21];
                    rdungeon.Floors[floor].mShoreInnerTopX = waterTileNumbers[1, 12];
                    rdungeon.Floors[floor].mShoreVerticalX = waterTileNumbers[1, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomX = waterTileNumbers[1, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftX = waterTileNumbers[1, 3];
                    rdungeon.Floors[floor].mShoreHorizontalX = waterTileNumbers[1, 7];
                    rdungeon.Floors[floor].mShoreInnerRightX = waterTileNumbers[1, 11];

                }
                lblSaveLoadMessage.Text = "Water Tile settings saved to floor(s)";
            } else if (pnlRDungeonWaterAnimTiles != null && pnlRDungeonWaterAnimTiles.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].mShoreSurroundedAnimSheet = waterAnimTileNumbers[0, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftAnimSheet = waterAnimTileNumbers[0, 0];
                    rdungeon.Floors[floor].mShoreTopAnimSheet = waterAnimTileNumbers[0, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightAnimSheet = waterAnimTileNumbers[0, 8];
                    rdungeon.Floors[floor].mShoreLeftAnimSheet = waterAnimTileNumbers[0, 1];
                    rdungeon.Floors[floor].mWaterAnimSheet = waterAnimTileNumbers[0, 5];
                    rdungeon.Floors[floor].mShoreRightAnimSheet = waterAnimTileNumbers[0, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftAnimSheet = waterAnimTileNumbers[0, 2];
                    rdungeon.Floors[floor].mShoreBottomAnimSheet = waterAnimTileNumbers[0, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightAnimSheet = waterAnimTileNumbers[0, 10];
                    rdungeon.Floors[floor].mShoreTopLeftAnimSheet = waterAnimTileNumbers[0, 16];
                    rdungeon.Floors[floor].mShoreTopRightAnimSheet = waterAnimTileNumbers[0, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftAnimSheet = waterAnimTileNumbers[0, 17];
                    rdungeon.Floors[floor].mShoreBottomRightAnimSheet = waterAnimTileNumbers[0, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardAnimSheet = waterAnimTileNumbers[0, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackAnimSheet = waterAnimTileNumbers[0, 21];
                    rdungeon.Floors[floor].mShoreInnerTopAnimSheet = waterAnimTileNumbers[0, 12];
                    rdungeon.Floors[floor].mShoreVerticalAnimSheet = waterAnimTileNumbers[0, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomAnimSheet = waterAnimTileNumbers[0, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftAnimSheet = waterAnimTileNumbers[0, 3];
                    rdungeon.Floors[floor].mShoreHorizontalAnimSheet = waterAnimTileNumbers[0, 7];
                    rdungeon.Floors[floor].mShoreInnerRightAnimSheet = waterAnimTileNumbers[0, 11];

                    rdungeon.Floors[floor].mShoreSurroundedAnimX = waterAnimTileNumbers[1, 15];
                    rdungeon.Floors[floor].mShoreInnerTopLeftAnimX = waterAnimTileNumbers[1, 0];
                    rdungeon.Floors[floor].mShoreTopAnimX = waterAnimTileNumbers[1, 4];
                    rdungeon.Floors[floor].mShoreInnerTopRightAnimX = waterAnimTileNumbers[1, 8];
                    rdungeon.Floors[floor].mShoreLeftAnimX = waterAnimTileNumbers[1, 1];
                    rdungeon.Floors[floor].mWaterAnimX = waterAnimTileNumbers[1, 5];
                    rdungeon.Floors[floor].mShoreRightAnimX = waterAnimTileNumbers[1, 9];
                    rdungeon.Floors[floor].mShoreInnerBottomLeftAnimX = waterAnimTileNumbers[1, 2];
                    rdungeon.Floors[floor].mShoreBottomAnimX = waterAnimTileNumbers[1, 6];
                    rdungeon.Floors[floor].mShoreInnerBottomRightAnimX = waterAnimTileNumbers[1, 10];
                    rdungeon.Floors[floor].mShoreTopLeftAnimX = waterAnimTileNumbers[1, 16];
                    rdungeon.Floors[floor].mShoreTopRightAnimX = waterAnimTileNumbers[1, 19];
                    rdungeon.Floors[floor].mShoreBottomLeftAnimX = waterAnimTileNumbers[1, 17];
                    rdungeon.Floors[floor].mShoreBottomRightAnimX = waterAnimTileNumbers[1, 20];
                    rdungeon.Floors[floor].mShoreDiagonalForwardAnimX = waterAnimTileNumbers[1, 18];
                    rdungeon.Floors[floor].mShoreDiagonalBackAnimX = waterAnimTileNumbers[1, 21];
                    rdungeon.Floors[floor].mShoreInnerTopAnimX = waterAnimTileNumbers[1, 12];
                    rdungeon.Floors[floor].mShoreVerticalAnimX = waterAnimTileNumbers[1, 13];
                    rdungeon.Floors[floor].mShoreInnerBottomAnimX = waterAnimTileNumbers[1, 14];
                    rdungeon.Floors[floor].mShoreInnerLeftAnimX = waterAnimTileNumbers[1, 3];
                    rdungeon.Floors[floor].mShoreHorizontalAnimX = waterAnimTileNumbers[1, 7];
                    rdungeon.Floors[floor].mShoreInnerRightAnimX = waterAnimTileNumbers[1, 11];

                }
                lblSaveLoadMessage.Text = "Water Anim Tile settings saved to floor(s)";
            } else if (pnlRDungeonAttributes != null && pnlRDungeonAttributes.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {


                    rdungeon.Floors[floor].GroundTile.Type = (Enums.TileType)cbGroundType.SelectedIndex;
                    rdungeon.Floors[floor].GroundTile.Data1 = nudGroundData1.Value;
                    rdungeon.Floors[floor].GroundTile.Data2 = nudGroundData2.Value;
                    rdungeon.Floors[floor].GroundTile.Data3 = nudGroundData3.Value;
                    rdungeon.Floors[floor].GroundTile.String1 = txtGroundString1.Text;
                    rdungeon.Floors[floor].GroundTile.String2 = txtGroundString2.Text;
                    rdungeon.Floors[floor].GroundTile.String3 = txtGroundString3.Text;

                    rdungeon.Floors[floor].HallTile.Type = (Enums.TileType)cbHallType.SelectedIndex;
                    rdungeon.Floors[floor].HallTile.Data1 = nudHallData1.Value;
                    rdungeon.Floors[floor].HallTile.Data2 = nudHallData2.Value;
                    rdungeon.Floors[floor].HallTile.Data3 = nudHallData3.Value;
                    rdungeon.Floors[floor].HallTile.String1 = txtHallString1.Text;
                    rdungeon.Floors[floor].HallTile.String2 = txtHallString2.Text;
                    rdungeon.Floors[floor].HallTile.String3 = txtHallString3.Text;

                    rdungeon.Floors[floor].WaterTile.Type = (Enums.TileType)cbWaterType.SelectedIndex;
                    rdungeon.Floors[floor].WaterTile.Data1 = nudWaterData1.Value;
                    rdungeon.Floors[floor].WaterTile.Data2 = nudWaterData2.Value;
                    rdungeon.Floors[floor].WaterTile.Data3 = nudWaterData3.Value;
                    rdungeon.Floors[floor].WaterTile.String1 = txtWaterString1.Text;
                    rdungeon.Floors[floor].WaterTile.String2 = txtWaterString2.Text;
                    rdungeon.Floors[floor].WaterTile.String3 = txtWaterString3.Text;

                    rdungeon.Floors[floor].WallTile.Type = (Enums.TileType)cbWallType.SelectedIndex;
                    rdungeon.Floors[floor].WallTile.Data1 = nudWallData1.Value;
                    rdungeon.Floors[floor].WallTile.Data2 = nudWallData2.Value;
                    rdungeon.Floors[floor].WallTile.Data3 = nudWallData3.Value;
                    rdungeon.Floors[floor].WallTile.String1 = txtWallString1.Text;
                    rdungeon.Floors[floor].WallTile.String2 = txtWallString2.Text;
                    rdungeon.Floors[floor].WallTile.String3 = txtWallString3.Text;


                }
                lblSaveLoadMessage.Text = "Attribute settings saved to floor(s)";
            } else if (pnlRDungeonItems != null && pnlRDungeonItems.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].Items.Clear();
                    for (int item = 0; item < itemList.Count; item++) {
                        EditableRDungeonItem newItem = new EditableRDungeonItem();
                        newItem.ItemNum = itemList[item].ItemNum;
                        newItem.MinAmount = itemList[item].MinAmount;
                        newItem.MaxAmount = itemList[item].MaxAmount;
                        newItem.AppearanceRate = itemList[item].AppearanceRate;
                        newItem.StickyRate = itemList[item].StickyRate;
                        newItem.Tag = itemList[item].Tag;
                        newItem.Hidden = itemList[item].Hidden;
                        newItem.OnGround = itemList[item].OnGround;
                        newItem.OnWater = itemList[item].OnWater;
                        newItem.OnWall = itemList[item].OnWall;

                        rdungeon.Floors[floor].Items.Add(newItem);
                    }

                }
                lblSaveLoadMessage.Text = "Item settings saved to floor(s)";
            } else if (pnlRDungeonNpcs != null && pnlRDungeonNpcs.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].NpcSpawnTime = nudNpcSpawnTime.Value;
                    rdungeon.Floors[floor].NpcMin = nudNpcMin.Value;
                    rdungeon.Floors[floor].NpcMax = nudNpcMax.Value;

                    rdungeon.Floors[floor].Npcs.Clear();
                    for (int npc = 0; npc < npcList.Count; npc++) {
                        MapNpcSettings newNpc = new MapNpcSettings();
                        newNpc.NpcNum = npcList[npc].NpcNum;
                        newNpc.MinLevel = npcList[npc].MinLevel;
                        newNpc.MaxLevel = npcList[npc].MaxLevel;
                        newNpc.AppearanceRate = npcList[npc].AppearanceRate;
                        newNpc.StartStatus = npcList[npc].StartStatus;
                        newNpc.StartStatusCounter = npcList[npc].StartStatusCounter;
                        newNpc.StartStatusChance = npcList[npc].StartStatusChance;

                        rdungeon.Floors[floor].Npcs.Add(newNpc);
                    }

                }
                lblSaveLoadMessage.Text = "Npc settings saved to floor(s)";
            } else if (pnlRDungeonTraps != null && pnlRDungeonTraps.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].SpecialTiles.Clear();
                    for (int traps = 0; traps < trapList.Count; traps++) {
                        EditableRDungeonTrap newTile = new EditableRDungeonTrap();

                        newTile.SpecialTile.Ground = trapList[traps].SpecialTile.Ground;
                        newTile.SpecialTile.GroundAnim = trapList[traps].SpecialTile.GroundAnim;
                        newTile.SpecialTile.Mask = trapList[traps].SpecialTile.Mask;
                        newTile.SpecialTile.Anim = trapList[traps].SpecialTile.Anim;
                        newTile.SpecialTile.Mask2 = trapList[traps].SpecialTile.Mask2;
                        newTile.SpecialTile.M2Anim = trapList[traps].SpecialTile.M2Anim;
                        newTile.SpecialTile.Fringe = trapList[traps].SpecialTile.Fringe;
                        newTile.SpecialTile.FAnim = trapList[traps].SpecialTile.FAnim;
                        newTile.SpecialTile.Fringe2 = trapList[traps].SpecialTile.Fringe2;
                        newTile.SpecialTile.F2Anim = trapList[traps].SpecialTile.F2Anim;

                        newTile.SpecialTile.GroundSet = trapList[traps].SpecialTile.GroundSet;
                        newTile.SpecialTile.GroundAnimSet = trapList[traps].SpecialTile.GroundAnimSet;
                        newTile.SpecialTile.MaskSet = trapList[traps].SpecialTile.MaskSet;
                        newTile.SpecialTile.AnimSet = trapList[traps].SpecialTile.AnimSet;
                        newTile.SpecialTile.Mask2Set = trapList[traps].SpecialTile.Mask2Set;
                        newTile.SpecialTile.M2AnimSet = trapList[traps].SpecialTile.M2AnimSet;
                        newTile.SpecialTile.FringeSet = trapList[traps].SpecialTile.FringeSet;
                        newTile.SpecialTile.FAnimSet = trapList[traps].SpecialTile.FAnimSet;
                        newTile.SpecialTile.Fringe2Set = trapList[traps].SpecialTile.Fringe2Set;
                        newTile.SpecialTile.F2AnimSet = trapList[traps].SpecialTile.F2AnimSet;

                        newTile.SpecialTile.Type = trapList[traps].SpecialTile.Type;
                        newTile.SpecialTile.Data1 = trapList[traps].SpecialTile.Data1;
                        newTile.SpecialTile.Data2 = trapList[traps].SpecialTile.Data2;
                        newTile.SpecialTile.Data3 = trapList[traps].SpecialTile.Data3;
                        newTile.SpecialTile.String1 = trapList[traps].SpecialTile.String1;
                        newTile.SpecialTile.String2 = trapList[traps].SpecialTile.String2;
                        newTile.SpecialTile.String3 = trapList[traps].SpecialTile.String3;

                        newTile.AppearanceRate = trapList[traps].AppearanceRate;

                        rdungeon.Floors[floor].SpecialTiles.Add(newTile);
                    }

                }
                lblSaveLoadMessage.Text = "Trap settings saved to floor(s)";
            } else if (pnlRDungeonWeather != null && pnlRDungeonWeather.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].Weather.Clear();
                    for (int weather = 0; weather < lbxWeather.Items.Count; weather++) {
                        string[] weatherindex = lbxWeather.Items[weather].TextIdentifier.Split(':');
                        if (weatherindex[1].IsNumeric()) {
                            rdungeon.Floors[floor].Weather.Add((Enums.Weather)weatherindex[1].ToInt());
                        }
                    }


                }
                lblSaveLoadMessage.Text = "Weather settings saved to floor(s)";
            } else if (pnlRDungeonGoal != null && pnlRDungeonGoal.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {


                    if (optNextFloor.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.NextFloor;
                    } else if (optMap.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.Map;
                    } else if (optScripted.Checked) {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.Scripted;
                    } else {
                        rdungeon.Floors[floor].GoalType = Enums.RFloorGoalType.NextFloor;
                    }

                    rdungeon.Floors[floor].GoalMap = nudData1.Value;
                    rdungeon.Floors[floor].GoalX = nudData2.Value;
                    rdungeon.Floors[floor].GoalY = nudData3.Value;



                }
                lblSaveLoadMessage.Text = "Goal settings saved to floor(s)";
            } else if (pnlRDungeonChambers != null && pnlRDungeonChambers.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {

                    rdungeon.Floors[floor].Chambers.Clear();
                    for (int chamber = 0; chamber < chamberList.Count; chamber++) {
                        EditableRDungeonChamber addedChamber = new EditableRDungeonChamber();
                        addedChamber.ChamberNum = chamberList[chamber].ChamberNum;
                        addedChamber.String1 = chamberList[chamber].String1;
                        addedChamber.String2 = chamberList[chamber].String2;
                        addedChamber.String3 = chamberList[chamber].String3;
                        rdungeon.Floors[floor].Chambers.Add(addedChamber);

                    }


                }
                lblSaveLoadMessage.Text = "Chamber settings saved to floor(s)";
            } else if (pnlRDungeonMisc != null && pnlRDungeonMisc.Visible == true) {
                for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {


                    rdungeon.Floors[floor].Darkness = nudDarkness.Value;
                    if (lbxMusic.SelectedItems.Count != 1 || lbxMusic.Items[0].Selected) {
                        rdungeon.Floors[floor].Music = "";
                    } else {
                        rdungeon.Floors[floor].Music = lbxMusic.SelectedItems[0].TextIdentifier;
                    }

                }


                lblSaveLoadMessage.Text = "Misc settings saved to floor(s)";
            }

        }

        void btnLoadFloor_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            for (int i = rdungeon.Floors.Count; i < nudMaxFloors.Value; i++) {
                rdungeon.Floors.Add(new EditableRDungeonFloor());
            }
            if (nudFirstFloor.Value > nudMaxFloors.Value) {
                lblSaveLoadMessage.Text = "Cannot load floor above the maximum.";
                return;
            }

            EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];

            if (pnlRDungeonFloorSettingSelection.Visible == true) {
                LoadpnlRDungeonStructure();
                LoadpnlRDungeonLandTiles();
                LoadpnlRDungeonLandAltTiles();
                LoadpnlRDungeonWaterTiles();
                LoadpnlRDungeonWaterAnimTiles();
                LoadpnlRDungeonAttributes();
                LoadpnlRDungeonItems();
                LoadpnlRDungeonNpcs();
                LoadpnlRDungeonTraps();
                LoadpnlRDungeonWeather();
                LoadpnlRDungeonGoal();
                LoadpnlRDungeonChambers();
                LoadpnlRDungeonMisc();

                //Structure
                nudTrapMin.Value = loadingfloor.TrapMin;
                nudTrapMax.Value = loadingfloor.TrapMax;
                nudItemMin.Value = loadingfloor.ItemMin;
                nudItemMax.Value = loadingfloor.ItemMax;
                nudRoomWidthMin.Value = loadingfloor.RoomWidthMin;
                nudRoomWidthMax.Value = loadingfloor.RoomWidthMax;
                nudRoomLengthMin.Value = loadingfloor.RoomLengthMin;
                nudRoomLengthMax.Value = loadingfloor.RoomLengthMax;
                nudHallTurnMin.Value = loadingfloor.HallTurnMin;
                nudHallTurnMax.Value = loadingfloor.HallTurnMax;
                nudHallVarMin.Value = loadingfloor.HallVarMin;
                nudHallVarMax.Value = loadingfloor.HallVarMax;
                nudWaterFrequency.Value = loadingfloor.WaterFrequency;
                nudCraters.Value = loadingfloor.Craters;
                nudCraterMinLength.Value = loadingfloor.CraterMinLength;
                nudCraterMaxLength.Value = loadingfloor.CraterMaxLength;
                chkCraterFuzzy.Checked = loadingfloor.CraterFuzzy;

                //Land Tiles
                landTileNumbers[0, 19] = loadingfloor.StairsSheet;
                landTileNumbers[0, 16] = loadingfloor.mGroundSheet;
                landTileNumbers[0, 10] = loadingfloor.mTopLeftSheet;
                landTileNumbers[0, 6] = loadingfloor.mTopCenterSheet;
                landTileNumbers[0, 2] = loadingfloor.mTopRightSheet;
                landTileNumbers[0, 9] = loadingfloor.mCenterLeftSheet;
                landTileNumbers[0, 5] = loadingfloor.mCenterCenterSheet;
                landTileNumbers[0, 1] = loadingfloor.mCenterRightSheet;
                landTileNumbers[0, 8] = loadingfloor.mBottomLeftSheet;
                landTileNumbers[0, 4] = loadingfloor.mBottomCenterSheet;
                landTileNumbers[0, 0] = loadingfloor.mBottomRightSheet;
                landTileNumbers[0, 17] = loadingfloor.mInnerTopLeftSheet;
                landTileNumbers[0, 20] = loadingfloor.mInnerTopRightSheet;
                landTileNumbers[0, 18] = loadingfloor.mInnerBottomLeftSheet;
                landTileNumbers[0, 21] = loadingfloor.mInnerBottomRightSheet;
                landTileNumbers[0, 15] = loadingfloor.mIsolatedWallSheet;
                landTileNumbers[0, 12] = loadingfloor.mColumnTopSheet;
                landTileNumbers[0, 13] = loadingfloor.mColumnCenterSheet;
                landTileNumbers[0, 14] = loadingfloor.mColumnBottomSheet;
                landTileNumbers[0, 3] = loadingfloor.mRowLeftSheet;
                landTileNumbers[0, 7] = loadingfloor.mRowCenterSheet;
                landTileNumbers[0, 11] = loadingfloor.mRowRightSheet;

                landTileNumbers[1, 19] = loadingfloor.StairsX;
                landTileNumbers[1, 16] = loadingfloor.mGroundX;
                landTileNumbers[1, 10] = loadingfloor.mTopLeftX;
                landTileNumbers[1, 6] = loadingfloor.mTopCenterX;
                landTileNumbers[1, 2] = loadingfloor.mTopRightX;
                landTileNumbers[1, 9] = loadingfloor.mCenterLeftX;
                landTileNumbers[1, 5] = loadingfloor.mCenterCenterX;
                landTileNumbers[1, 1] = loadingfloor.mCenterRightX;
                landTileNumbers[1, 8] = loadingfloor.mBottomLeftX;
                landTileNumbers[1, 4] = loadingfloor.mBottomCenterX;
                landTileNumbers[1, 0] = loadingfloor.mBottomRightX;
                landTileNumbers[1, 17] = loadingfloor.mInnerTopLeftX;
                landTileNumbers[1, 20] = loadingfloor.mInnerTopRightX;
                landTileNumbers[1, 18] = loadingfloor.mInnerBottomLeftX;
                landTileNumbers[1, 21] = loadingfloor.mInnerBottomRightX;
                landTileNumbers[1, 15] = loadingfloor.mIsolatedWallX;
                landTileNumbers[1, 12] = loadingfloor.mColumnTopX;
                landTileNumbers[1, 13] = loadingfloor.mColumnCenterX;
                landTileNumbers[1, 14] = loadingfloor.mColumnBottomX;
                landTileNumbers[1, 3] = loadingfloor.mRowLeftX;
                landTileNumbers[1, 7] = loadingfloor.mRowCenterX;
                landTileNumbers[1, 11] = loadingfloor.mRowRightX;

                for (int i = 0; i < 22; i++) {
                    picLandTileset[i].Image = Graphics.GraphicsManager.Tiles[landTileNumbers[0, i]][landTileNumbers[1, i]];
                }

                //Land Alt Tiles
                landAltTileNumbers[0, 19] = loadingfloor.mGroundAlt2Sheet;
                landAltTileNumbers[0, 16] = loadingfloor.mGroundAltSheet;
                landAltTileNumbers[0, 10] = loadingfloor.mTopLeftAltSheet;
                landAltTileNumbers[0, 6] = loadingfloor.mTopCenterAltSheet;
                landAltTileNumbers[0, 2] = loadingfloor.mTopRightAltSheet;
                landAltTileNumbers[0, 9] = loadingfloor.mCenterLeftAltSheet;
                landAltTileNumbers[0, 5] = loadingfloor.mCenterCenterAltSheet;
                landAltTileNumbers[0, 22] = loadingfloor.mCenterCenterAlt2Sheet;
                landAltTileNumbers[0, 1] = loadingfloor.mCenterRightAltSheet;
                landAltTileNumbers[0, 8] = loadingfloor.mBottomLeftAltSheet;
                landAltTileNumbers[0, 4] = loadingfloor.mBottomCenterAltSheet;
                landAltTileNumbers[0, 0] = loadingfloor.mBottomRightAltSheet;
                landAltTileNumbers[0, 17] = loadingfloor.mInnerTopLeftAltSheet;
                landAltTileNumbers[0, 20] = loadingfloor.mInnerTopRightAltSheet;
                landAltTileNumbers[0, 18] = loadingfloor.mInnerBottomLeftAltSheet;
                landAltTileNumbers[0, 21] = loadingfloor.mInnerBottomRightAltSheet;
                landAltTileNumbers[0, 15] = loadingfloor.mIsolatedWallAltSheet;
                landAltTileNumbers[0, 12] = loadingfloor.mColumnTopAltSheet;
                landAltTileNumbers[0, 13] = loadingfloor.mColumnCenterAltSheet;
                landAltTileNumbers[0, 14] = loadingfloor.mColumnBottomAltSheet;
                landAltTileNumbers[0, 3] = loadingfloor.mRowLeftAltSheet;
                landAltTileNumbers[0, 7] = loadingfloor.mRowCenterAltSheet;
                landAltTileNumbers[0, 11] = loadingfloor.mRowRightAltSheet;

                landAltTileNumbers[1, 19] = loadingfloor.mGroundAlt2X;
                landAltTileNumbers[1, 16] = loadingfloor.mGroundAltX;
                landAltTileNumbers[1, 10] = loadingfloor.mTopLeftAltX;
                landAltTileNumbers[1, 6] = loadingfloor.mTopCenterAltX;
                landAltTileNumbers[1, 2] = loadingfloor.mTopRightAltX;
                landAltTileNumbers[1, 9] = loadingfloor.mCenterLeftAltX;
                landAltTileNumbers[1, 5] = loadingfloor.mCenterCenterAltX;
                landAltTileNumbers[1, 22] = loadingfloor.mCenterCenterAlt2X;
                landAltTileNumbers[1, 1] = loadingfloor.mCenterRightAltX;
                landAltTileNumbers[1, 8] = loadingfloor.mBottomLeftAltX;
                landAltTileNumbers[1, 4] = loadingfloor.mBottomCenterAltX;
                landAltTileNumbers[1, 0] = loadingfloor.mBottomRightAltX;
                landAltTileNumbers[1, 17] = loadingfloor.mInnerTopLeftAltX;
                landAltTileNumbers[1, 20] = loadingfloor.mInnerTopRightAltX;
                landAltTileNumbers[1, 18] = loadingfloor.mInnerBottomLeftAltX;
                landAltTileNumbers[1, 21] = loadingfloor.mInnerBottomRightAltX;
                landAltTileNumbers[1, 15] = loadingfloor.mIsolatedWallAltX;
                landAltTileNumbers[1, 12] = loadingfloor.mColumnTopAltX;
                landAltTileNumbers[1, 13] = loadingfloor.mColumnCenterAltX;
                landAltTileNumbers[1, 14] = loadingfloor.mColumnBottomAltX;
                landAltTileNumbers[1, 3] = loadingfloor.mRowLeftAltX;
                landAltTileNumbers[1, 7] = loadingfloor.mRowCenterAltX;
                landAltTileNumbers[1, 11] = loadingfloor.mRowRightAltX;

                for (int i = 0; i < 23; i++) {
                    picLandAltTileset[i].Image = Graphics.GraphicsManager.Tiles[landAltTileNumbers[0, i]][landAltTileNumbers[1, i]];
                }


                //Water Tiles
                waterTileNumbers[0, 15] = loadingfloor.mShoreSurroundedSheet;
                waterTileNumbers[0, 0] = loadingfloor.mShoreInnerTopLeftSheet;
                waterTileNumbers[0, 4] = loadingfloor.mShoreTopSheet;
                waterTileNumbers[0, 8] = loadingfloor.mShoreInnerTopRightSheet;
                waterTileNumbers[0, 1] = loadingfloor.mShoreLeftSheet;
                waterTileNumbers[0, 5] = loadingfloor.mWaterSheet;
                waterTileNumbers[0, 9] = loadingfloor.mShoreRightSheet;
                waterTileNumbers[0, 2] = loadingfloor.mShoreInnerBottomLeftSheet;
                waterTileNumbers[0, 6] = loadingfloor.mShoreBottomSheet;
                waterTileNumbers[0, 10] = loadingfloor.mShoreInnerBottomRightSheet;
                waterTileNumbers[0, 16] = loadingfloor.mShoreTopLeftSheet;
                waterTileNumbers[0, 19] = loadingfloor.mShoreTopRightSheet;
                waterTileNumbers[0, 17] = loadingfloor.mShoreBottomLeftSheet;
                waterTileNumbers[0, 20] = loadingfloor.mShoreBottomRightSheet;
                waterTileNumbers[0, 18] = loadingfloor.mShoreDiagonalForwardSheet;
                waterTileNumbers[0, 21] = loadingfloor.mShoreDiagonalBackSheet;
                waterTileNumbers[0, 12] = loadingfloor.mShoreInnerTopSheet;
                waterTileNumbers[0, 13] = loadingfloor.mShoreVerticalSheet;
                waterTileNumbers[0, 14] = loadingfloor.mShoreInnerBottomSheet;
                waterTileNumbers[0, 3] = loadingfloor.mShoreInnerLeftSheet;
                waterTileNumbers[0, 7] = loadingfloor.mShoreHorizontalSheet;
                waterTileNumbers[0, 11] = loadingfloor.mShoreInnerRightSheet;

                waterTileNumbers[1, 15] = loadingfloor.mShoreSurroundedX;
                waterTileNumbers[1, 0] = loadingfloor.mShoreInnerTopLeftX;
                waterTileNumbers[1, 4] = loadingfloor.mShoreTopX;
                waterTileNumbers[1, 8] = loadingfloor.mShoreInnerTopRightX;
                waterTileNumbers[1, 1] = loadingfloor.mShoreLeftX;
                waterTileNumbers[1, 5] = loadingfloor.mWaterX;
                waterTileNumbers[1, 9] = loadingfloor.mShoreRightX;
                waterTileNumbers[1, 2] = loadingfloor.mShoreInnerBottomLeftX;
                waterTileNumbers[1, 6] = loadingfloor.mShoreBottomX;
                waterTileNumbers[1, 10] = loadingfloor.mShoreInnerBottomRightX;
                waterTileNumbers[1, 16] = loadingfloor.mShoreTopLeftX;
                waterTileNumbers[1, 19] = loadingfloor.mShoreTopRightX;
                waterTileNumbers[1, 17] = loadingfloor.mShoreBottomLeftX;
                waterTileNumbers[1, 20] = loadingfloor.mShoreBottomRightX;
                waterTileNumbers[1, 18] = loadingfloor.mShoreDiagonalForwardX;
                waterTileNumbers[1, 21] = loadingfloor.mShoreDiagonalBackX;
                waterTileNumbers[1, 12] = loadingfloor.mShoreInnerTopX;
                waterTileNumbers[1, 13] = loadingfloor.mShoreVerticalX;
                waterTileNumbers[1, 14] = loadingfloor.mShoreInnerBottomX;
                waterTileNumbers[1, 3] = loadingfloor.mShoreInnerLeftX;
                waterTileNumbers[1, 7] = loadingfloor.mShoreHorizontalX;
                waterTileNumbers[1, 11] = loadingfloor.mShoreInnerRightX;

                for (int i = 0; i < 22; i++) {
                    picWaterTileset[i].Image = Graphics.GraphicsManager.Tiles[waterTileNumbers[0, i]][waterTileNumbers[1, i]];
                }

                //Water Anim Tiles
                waterAnimTileNumbers[0, 15] = loadingfloor.mShoreSurroundedAnimSheet;
                waterAnimTileNumbers[0, 0] = loadingfloor.mShoreInnerTopLeftAnimSheet;
                waterAnimTileNumbers[0, 4] = loadingfloor.mShoreTopAnimSheet;
                waterAnimTileNumbers[0, 8] = loadingfloor.mShoreInnerTopRightAnimSheet;
                waterAnimTileNumbers[0, 1] = loadingfloor.mShoreLeftAnimSheet;
                waterAnimTileNumbers[0, 5] = loadingfloor.mWaterAnimSheet;
                waterAnimTileNumbers[0, 9] = loadingfloor.mShoreRightAnimSheet;
                waterAnimTileNumbers[0, 2] = loadingfloor.mShoreInnerBottomLeftAnimSheet;
                waterAnimTileNumbers[0, 6] = loadingfloor.mShoreBottomAnimSheet;
                waterAnimTileNumbers[0, 10] = loadingfloor.mShoreInnerBottomRightAnimSheet;
                waterAnimTileNumbers[0, 16] = loadingfloor.mShoreTopLeftAnimSheet;
                waterAnimTileNumbers[0, 19] = loadingfloor.mShoreTopRightAnimSheet;
                waterAnimTileNumbers[0, 17] = loadingfloor.mShoreBottomLeftAnimSheet;
                waterAnimTileNumbers[0, 20] = loadingfloor.mShoreBottomRightAnimSheet;
                waterAnimTileNumbers[0, 18] = loadingfloor.mShoreDiagonalForwardAnimSheet;
                waterAnimTileNumbers[0, 21] = loadingfloor.mShoreDiagonalBackAnimSheet;
                waterAnimTileNumbers[0, 12] = loadingfloor.mShoreInnerTopAnimSheet;
                waterAnimTileNumbers[0, 13] = loadingfloor.mShoreVerticalAnimSheet;
                waterAnimTileNumbers[0, 14] = loadingfloor.mShoreInnerBottomAnimSheet;
                waterAnimTileNumbers[0, 3] = loadingfloor.mShoreInnerLeftAnimSheet;
                waterAnimTileNumbers[0, 7] = loadingfloor.mShoreHorizontalAnimSheet;
                waterAnimTileNumbers[0, 11] = loadingfloor.mShoreInnerRightAnimSheet;

                waterAnimTileNumbers[1, 15] = loadingfloor.mShoreSurroundedAnimX;
                waterAnimTileNumbers[1, 0] = loadingfloor.mShoreInnerTopLeftAnimX;
                waterAnimTileNumbers[1, 4] = loadingfloor.mShoreTopAnimX;
                waterAnimTileNumbers[1, 8] = loadingfloor.mShoreInnerTopRightAnimX;
                waterAnimTileNumbers[1, 1] = loadingfloor.mShoreLeftAnimX;
                waterAnimTileNumbers[1, 5] = loadingfloor.mWaterAnimX;
                waterAnimTileNumbers[1, 9] = loadingfloor.mShoreRightAnimX;
                waterAnimTileNumbers[1, 2] = loadingfloor.mShoreInnerBottomLeftAnimX;
                waterAnimTileNumbers[1, 6] = loadingfloor.mShoreBottomAnimX;
                waterAnimTileNumbers[1, 10] = loadingfloor.mShoreInnerBottomRightAnimX;
                waterAnimTileNumbers[1, 16] = loadingfloor.mShoreTopLeftAnimX;
                waterAnimTileNumbers[1, 19] = loadingfloor.mShoreTopRightAnimX;
                waterAnimTileNumbers[1, 17] = loadingfloor.mShoreBottomLeftAnimX;
                waterAnimTileNumbers[1, 20] = loadingfloor.mShoreBottomRightAnimX;
                waterAnimTileNumbers[1, 18] = loadingfloor.mShoreDiagonalForwardAnimX;
                waterAnimTileNumbers[1, 21] = loadingfloor.mShoreDiagonalBackAnimX;
                waterAnimTileNumbers[1, 12] = loadingfloor.mShoreInnerTopAnimX;
                waterAnimTileNumbers[1, 13] = loadingfloor.mShoreVerticalAnimX;
                waterAnimTileNumbers[1, 14] = loadingfloor.mShoreInnerBottomAnimX;
                waterAnimTileNumbers[1, 3] = loadingfloor.mShoreInnerLeftAnimX;
                waterAnimTileNumbers[1, 7] = loadingfloor.mShoreHorizontalAnimX;
                waterAnimTileNumbers[1, 11] = loadingfloor.mShoreInnerRightAnimX;

                for (int i = 0; i < 22; i++) {
                    picWaterAnimTileset[i].Image = Graphics.GraphicsManager.Tiles[waterAnimTileNumbers[0, i]][waterAnimTileNumbers[1, i]];
                }

                //Attributes
                cbGroundType.SelectItem((int)loadingfloor.GroundTile.Type);
                nudGroundData1.Value = loadingfloor.GroundTile.Data1;
                nudGroundData2.Value = loadingfloor.GroundTile.Data2;
                nudGroundData3.Value = loadingfloor.GroundTile.Data3;
                txtGroundString1.Text = loadingfloor.GroundTile.String1;
                txtGroundString2.Text = loadingfloor.GroundTile.String2;
                txtGroundString3.Text = loadingfloor.GroundTile.String3;

                cbHallType.SelectItem((int)loadingfloor.HallTile.Type);
                nudHallData1.Value = loadingfloor.HallTile.Data1;
                nudHallData2.Value = loadingfloor.HallTile.Data2;
                nudHallData3.Value = loadingfloor.HallTile.Data3;
                txtHallString1.Text = loadingfloor.HallTile.String1;
                txtHallString2.Text = loadingfloor.HallTile.String2;
                txtHallString3.Text = loadingfloor.HallTile.String3;

                cbWaterType.SelectItem((int)loadingfloor.WaterTile.Type);
                nudWaterData1.Value = loadingfloor.WaterTile.Data1;
                nudWaterData2.Value = loadingfloor.WaterTile.Data2;
                nudWaterData3.Value = loadingfloor.WaterTile.Data3;
                txtWaterString1.Text = loadingfloor.WaterTile.String1;
                txtWaterString2.Text = loadingfloor.WaterTile.String2;
                txtWaterString3.Text = loadingfloor.WaterTile.String3;

                cbWallType.SelectItem((int)loadingfloor.WallTile.Type);
                nudWallData1.Value = loadingfloor.WallTile.Data1;
                nudWallData2.Value = loadingfloor.WallTile.Data2;
                nudWallData3.Value = loadingfloor.WallTile.Data3;
                txtWallString1.Text = loadingfloor.WallTile.String1;
                txtWallString2.Text = loadingfloor.WallTile.String2;
                txtWallString3.Text = loadingfloor.WallTile.String3;

                //Items
                itemList.Clear();
                lbxItems.Items.Clear();
                for (int item = 0; item < loadingfloor.Items.Count; item++) {
                    EditableRDungeonItem newItem = new EditableRDungeonItem();
                    newItem.ItemNum = loadingfloor.Items[item].ItemNum;
                    newItem.MinAmount = loadingfloor.Items[item].MinAmount;
                    newItem.MaxAmount = loadingfloor.Items[item].MaxAmount;
                    newItem.AppearanceRate = loadingfloor.Items[item].AppearanceRate;
                    newItem.StickyRate = loadingfloor.Items[item].StickyRate;
                    newItem.Tag = loadingfloor.Items[item].Tag;
                    newItem.Hidden = loadingfloor.Items[item].Hidden;
                    newItem.OnGround = loadingfloor.Items[item].OnGround;
                    newItem.OnWater = loadingfloor.Items[item].OnWater;
                    newItem.OnWall = loadingfloor.Items[item].OnWall;

                    itemList.Add(newItem);
                    lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + newItem.AppearanceRate + "%) " + newItem.MinAmount + "-" + newItem.MaxAmount + " " + Items.ItemHelper.Items[newItem.ItemNum].Name + " (" + newItem.StickyRate + "% Sticky)"));
                }

                //Npcs
                nudNpcSpawnTime.Value = loadingfloor.NpcSpawnTime;
                nudNpcMin.Value = loadingfloor.NpcMin;
                nudNpcMax.Value = loadingfloor.NpcMax;

                npcList.Clear();
                lbxNpcs.Items.Clear();
                for (int npc = 0; npc < loadingfloor.Npcs.Count; npc++) {
                    MapNpcSettings newNpc = new MapNpcSettings();
                    newNpc.NpcNum = loadingfloor.Npcs[npc].NpcNum;
                    newNpc.MinLevel = loadingfloor.Npcs[npc].MinLevel;
                    newNpc.MaxLevel = loadingfloor.Npcs[npc].MaxLevel;
                    newNpc.AppearanceRate = loadingfloor.Npcs[npc].AppearanceRate;
                    newNpc.StartStatus = loadingfloor.Npcs[npc].StartStatus;
                    newNpc.StartStatusCounter = loadingfloor.Npcs[npc].StartStatusCounter;
                    newNpc.StartStatusChance = loadingfloor.Npcs[npc].StartStatusChance;

                    npcList.Add(newNpc);
                    lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                + "(" + newNpc.AppearanceRate + "%) " + "Lv." + newNpc.MinLevel + "-" + newNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newNpc.NpcNum].Name
                + " [" + newNpc.StartStatusChance + "% " + newNpc.StartStatus.ToString() + "]"));
                }

                //Traps
                trapList.Clear();
                lbxTraps.Items.Clear();
                for (int traps = 0; traps < loadingfloor.SpecialTiles.Count; traps++) {
                    EditableRDungeonTrap newTile = new EditableRDungeonTrap();

                    newTile.SpecialTile.Ground = loadingfloor.SpecialTiles[traps].SpecialTile.Ground;
                    newTile.SpecialTile.GroundAnim = loadingfloor.SpecialTiles[traps].SpecialTile.GroundAnim;
                    newTile.SpecialTile.Mask = loadingfloor.SpecialTiles[traps].SpecialTile.Mask;
                    newTile.SpecialTile.Anim = loadingfloor.SpecialTiles[traps].SpecialTile.Anim;
                    newTile.SpecialTile.Mask2 = loadingfloor.SpecialTiles[traps].SpecialTile.Mask2;
                    newTile.SpecialTile.M2Anim = loadingfloor.SpecialTiles[traps].SpecialTile.M2Anim;
                    newTile.SpecialTile.Fringe = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe;
                    newTile.SpecialTile.FAnim = loadingfloor.SpecialTiles[traps].SpecialTile.FAnim;
                    newTile.SpecialTile.Fringe2 = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe2;
                    newTile.SpecialTile.F2Anim = loadingfloor.SpecialTiles[traps].SpecialTile.F2Anim;

                    newTile.SpecialTile.GroundSet = loadingfloor.SpecialTiles[traps].SpecialTile.GroundSet;
                    newTile.SpecialTile.GroundAnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.GroundAnimSet;
                    newTile.SpecialTile.MaskSet = loadingfloor.SpecialTiles[traps].SpecialTile.MaskSet;
                    newTile.SpecialTile.AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.AnimSet;
                    newTile.SpecialTile.Mask2Set = loadingfloor.SpecialTiles[traps].SpecialTile.Mask2Set;
                    newTile.SpecialTile.M2AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.M2AnimSet;
                    newTile.SpecialTile.FringeSet = loadingfloor.SpecialTiles[traps].SpecialTile.FringeSet;
                    newTile.SpecialTile.FAnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.FAnimSet;
                    newTile.SpecialTile.Fringe2Set = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe2Set;
                    newTile.SpecialTile.F2AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.F2AnimSet;

                    newTile.SpecialTile.Type = loadingfloor.SpecialTiles[traps].SpecialTile.Type;
                    newTile.SpecialTile.Data1 = loadingfloor.SpecialTiles[traps].SpecialTile.Data1;
                    newTile.SpecialTile.Data2 = loadingfloor.SpecialTiles[traps].SpecialTile.Data2;
                    newTile.SpecialTile.Data3 = loadingfloor.SpecialTiles[traps].SpecialTile.Data3;
                    newTile.SpecialTile.String1 = loadingfloor.SpecialTiles[traps].SpecialTile.String1;
                    newTile.SpecialTile.String2 = loadingfloor.SpecialTiles[traps].SpecialTile.String2;
                    newTile.SpecialTile.String3 = loadingfloor.SpecialTiles[traps].SpecialTile.String3;

                    newTile.AppearanceRate = loadingfloor.SpecialTiles[traps].AppearanceRate;

                    trapList.Add(newTile);
                    lbxTraps.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (traps + 1) + ": " + newTile.SpecialTile.Type + "/" + newTile.SpecialTile.Data1 + "/" + newTile.SpecialTile.Data2 + "/" + newTile.SpecialTile.Data3));
                }

                //Weather
                lbxWeather.Items.Clear();
                for (int weather = 0; weather < loadingfloor.Weather.Count; weather++) {
                    lbxWeather.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (weather + 1) + ":" + (int)loadingfloor.Weather[weather] + ": " + Enum.GetName(typeof(Enums.Weather), loadingfloor.Weather[weather])));
                }

                //Goal
                switch (loadingfloor.GoalType) {
                    case Enums.RFloorGoalType.NextFloor: {
                            optNextFloor.Checked = true;
                        }
                        break;
                    case Enums.RFloorGoalType.Map: {
                            optMap.Checked = true;
                        }
                        break;
                    case Enums.RFloorGoalType.Scripted: {
                            optScripted.Checked = true;
                        }
                        break;
                    default: {
                            optNextFloor.Checked = true;
                        }
                        break;
                }

                nudData1.Value = loadingfloor.GoalMap;
                nudData2.Value = loadingfloor.GoalX;
                nudData3.Value = loadingfloor.GoalY;

                //chambers
                chamberList.Clear();
                lbxChambers.Items.Clear();
                for (int chamber = 0; chamber < loadingfloor.Chambers.Count; chamber++) {
                    EditableRDungeonChamber newChamber = new EditableRDungeonChamber();
                    newChamber.ChamberNum = loadingfloor.Chambers[chamber].ChamberNum;
                    newChamber.String1 = loadingfloor.Chambers[chamber].String1;
                    newChamber.String2 = loadingfloor.Chambers[chamber].String2;
                    newChamber.String3 = loadingfloor.Chambers[chamber].String3;
                    chamberList.Add(newChamber);
                    lbxChambers.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "#" + loadingfloor.Chambers[chamber].ChamberNum + "/" + loadingfloor.Chambers[chamber].String1 + "/" + loadingfloor.Chambers[chamber].String2 + "/" + loadingfloor.Chambers[chamber].String3));
                }

                //Misc
                nudDarkness.Value = loadingfloor.Darkness;
                if (loadingfloor.Music == "") {
                    lbxMusic.SelectItem(0);
                } else {
                    for (int i = 0; i < lbxMusic.Items.Count; i++) {
                        if (lbxMusic.Items[i].TextIdentifier == loadingfloor.Music) {
                            lbxMusic.SelectItem(i);
                        }
                    }
                }
                lblSaveLoadMessage.Text = "All settings loaded";
            } else if (pnlRDungeonStructure != null && pnlRDungeonStructure.Visible == true) {

                nudTrapMin.Value = loadingfloor.TrapMin;
                nudTrapMax.Value = loadingfloor.TrapMax;
                nudItemMin.Value = loadingfloor.ItemMin;
                nudItemMax.Value = loadingfloor.ItemMax;
                nudRoomWidthMin.Value = loadingfloor.RoomWidthMin;
                nudRoomWidthMax.Value = loadingfloor.RoomWidthMax;
                nudRoomLengthMin.Value = loadingfloor.RoomLengthMin;
                nudRoomLengthMax.Value = loadingfloor.RoomLengthMax;
                nudHallTurnMin.Value = loadingfloor.HallTurnMin;
                nudHallTurnMax.Value = loadingfloor.HallTurnMax;
                nudHallVarMin.Value = loadingfloor.HallVarMin;
                nudHallVarMax.Value = loadingfloor.HallVarMax;
                nudWaterFrequency.Value = loadingfloor.WaterFrequency;
                nudCraters.Value = loadingfloor.Craters;
                nudCraterMinLength.Value = loadingfloor.CraterMinLength;
                nudCraterMaxLength.Value = loadingfloor.CraterMaxLength;
                chkCraterFuzzy.Checked = loadingfloor.CraterFuzzy;

                lblSaveLoadMessage.Text = "Structure settings loaded";
            } else if (pnlRDungeonLandTiles != null && pnlRDungeonLandTiles.Visible == true) {

                landTileNumbers[0, 19] = loadingfloor.StairsSheet;
                landTileNumbers[0, 16] = loadingfloor.mGroundSheet;
                landTileNumbers[0, 10] = loadingfloor.mTopLeftSheet;
                landTileNumbers[0, 6] = loadingfloor.mTopCenterSheet;
                landTileNumbers[0, 2] = loadingfloor.mTopRightSheet;
                landTileNumbers[0, 9] = loadingfloor.mCenterLeftSheet;
                landTileNumbers[0, 5] = loadingfloor.mCenterCenterSheet;
                landTileNumbers[0, 1] = loadingfloor.mCenterRightSheet;
                landTileNumbers[0, 8] = loadingfloor.mBottomLeftSheet;
                landTileNumbers[0, 4] = loadingfloor.mBottomCenterSheet;
                landTileNumbers[0, 0] = loadingfloor.mBottomRightSheet;
                landTileNumbers[0, 17] = loadingfloor.mInnerTopLeftSheet;
                landTileNumbers[0, 20] = loadingfloor.mInnerTopRightSheet;
                landTileNumbers[0, 18] = loadingfloor.mInnerBottomLeftSheet;
                landTileNumbers[0, 21] = loadingfloor.mInnerBottomRightSheet;
                landTileNumbers[0, 15] = loadingfloor.mIsolatedWallSheet;
                landTileNumbers[0, 12] = loadingfloor.mColumnTopSheet;
                landTileNumbers[0, 13] = loadingfloor.mColumnCenterSheet;
                landTileNumbers[0, 14] = loadingfloor.mColumnBottomSheet;
                landTileNumbers[0, 3] = loadingfloor.mRowLeftSheet;
                landTileNumbers[0, 7] = loadingfloor.mRowCenterSheet;
                landTileNumbers[0, 11] = loadingfloor.mRowRightSheet;

                landTileNumbers[1, 19] = loadingfloor.StairsX;
                landTileNumbers[1, 16] = loadingfloor.mGroundX;
                landTileNumbers[1, 10] = loadingfloor.mTopLeftX;
                landTileNumbers[1, 6] = loadingfloor.mTopCenterX;
                landTileNumbers[1, 2] = loadingfloor.mTopRightX;
                landTileNumbers[1, 9] = loadingfloor.mCenterLeftX;
                landTileNumbers[1, 5] = loadingfloor.mCenterCenterX;
                landTileNumbers[1, 1] = loadingfloor.mCenterRightX;
                landTileNumbers[1, 8] = loadingfloor.mBottomLeftX;
                landTileNumbers[1, 4] = loadingfloor.mBottomCenterX;
                landTileNumbers[1, 0] = loadingfloor.mBottomRightX;
                landTileNumbers[1, 17] = loadingfloor.mInnerTopLeftX;
                landTileNumbers[1, 20] = loadingfloor.mInnerTopRightX;
                landTileNumbers[1, 18] = loadingfloor.mInnerBottomLeftX;
                landTileNumbers[1, 21] = loadingfloor.mInnerBottomRightX;
                landTileNumbers[1, 15] = loadingfloor.mIsolatedWallX;
                landTileNumbers[1, 12] = loadingfloor.mColumnTopX;
                landTileNumbers[1, 13] = loadingfloor.mColumnCenterX;
                landTileNumbers[1, 14] = loadingfloor.mColumnBottomX;
                landTileNumbers[1, 3] = loadingfloor.mRowLeftX;
                landTileNumbers[1, 7] = loadingfloor.mRowCenterX;
                landTileNumbers[1, 11] = loadingfloor.mRowRightX;

                for (int i = 0; i < 22; i++) {
                    picLandTileset[i].Image = Graphics.GraphicsManager.Tiles[landTileNumbers[0, i]][landTileNumbers[1, i]];
                }

                lblSaveLoadMessage.Text = "Land Tile settings loaded";
            } else if (pnlRDungeonLandAltTiles != null && pnlRDungeonLandAltTiles.Visible == true) {

                landAltTileNumbers[0, 19] = loadingfloor.mGroundAlt2Sheet;
                landAltTileNumbers[0, 16] = loadingfloor.mGroundAltSheet;
                landAltTileNumbers[0, 10] = loadingfloor.mTopLeftAltSheet;
                landAltTileNumbers[0, 6] = loadingfloor.mTopCenterAltSheet;
                landAltTileNumbers[0, 2] = loadingfloor.mTopRightAltSheet;
                landAltTileNumbers[0, 9] = loadingfloor.mCenterLeftAltSheet;
                landAltTileNumbers[0, 5] = loadingfloor.mCenterCenterAltSheet;
                landAltTileNumbers[0, 22] = loadingfloor.mCenterCenterAlt2Sheet;
                landAltTileNumbers[0, 1] = loadingfloor.mCenterRightAltSheet;
                landAltTileNumbers[0, 8] = loadingfloor.mBottomLeftAltSheet;
                landAltTileNumbers[0, 4] = loadingfloor.mBottomCenterAltSheet;
                landAltTileNumbers[0, 0] = loadingfloor.mBottomRightAltSheet;
                landAltTileNumbers[0, 17] = loadingfloor.mInnerTopLeftAltSheet;
                landAltTileNumbers[0, 20] = loadingfloor.mInnerTopRightAltSheet;
                landAltTileNumbers[0, 18] = loadingfloor.mInnerBottomLeftAltSheet;
                landAltTileNumbers[0, 21] = loadingfloor.mInnerBottomRightAltSheet;
                landAltTileNumbers[0, 15] = loadingfloor.mIsolatedWallAltSheet;
                landAltTileNumbers[0, 12] = loadingfloor.mColumnTopAltSheet;
                landAltTileNumbers[0, 13] = loadingfloor.mColumnCenterAltSheet;
                landAltTileNumbers[0, 14] = loadingfloor.mColumnBottomAltSheet;
                landAltTileNumbers[0, 3] = loadingfloor.mRowLeftAltSheet;
                landAltTileNumbers[0, 7] = loadingfloor.mRowCenterAltSheet;
                landAltTileNumbers[0, 11] = loadingfloor.mRowRightAltSheet;

                landAltTileNumbers[1, 19] = loadingfloor.mGroundAlt2X;
                landAltTileNumbers[1, 16] = loadingfloor.mGroundAltX;
                landAltTileNumbers[1, 10] = loadingfloor.mTopLeftAltX;
                landAltTileNumbers[1, 6] = loadingfloor.mTopCenterAltX;
                landAltTileNumbers[1, 2] = loadingfloor.mTopRightAltX;
                landAltTileNumbers[1, 9] = loadingfloor.mCenterLeftAltX;
                landAltTileNumbers[1, 5] = loadingfloor.mCenterCenterAltX;
                landAltTileNumbers[1, 22] = loadingfloor.mCenterCenterAlt2X;
                landAltTileNumbers[1, 1] = loadingfloor.mCenterRightAltX;
                landAltTileNumbers[1, 8] = loadingfloor.mBottomLeftAltX;
                landAltTileNumbers[1, 4] = loadingfloor.mBottomCenterAltX;
                landAltTileNumbers[1, 0] = loadingfloor.mBottomRightAltX;
                landAltTileNumbers[1, 17] = loadingfloor.mInnerTopLeftAltX;
                landAltTileNumbers[1, 20] = loadingfloor.mInnerTopRightAltX;
                landAltTileNumbers[1, 18] = loadingfloor.mInnerBottomLeftAltX;
                landAltTileNumbers[1, 21] = loadingfloor.mInnerBottomRightAltX;
                landAltTileNumbers[1, 15] = loadingfloor.mIsolatedWallAltX;
                landAltTileNumbers[1, 12] = loadingfloor.mColumnTopAltX;
                landAltTileNumbers[1, 13] = loadingfloor.mColumnCenterAltX;
                landAltTileNumbers[1, 14] = loadingfloor.mColumnBottomAltX;
                landAltTileNumbers[1, 3] = loadingfloor.mRowLeftAltX;
                landAltTileNumbers[1, 7] = loadingfloor.mRowCenterAltX;
                landAltTileNumbers[1, 11] = loadingfloor.mRowRightAltX;

                for (int i = 0; i < 23; i++) {
                    picLandAltTileset[i].Image = Graphics.GraphicsManager.Tiles[landAltTileNumbers[0, i]][landAltTileNumbers[1, i]];
                }

                lblSaveLoadMessage.Text = "Land Alt Tile settings loaded";
            } else if (pnlRDungeonWaterTiles != null && pnlRDungeonWaterTiles.Visible == true) {

                waterTileNumbers[0, 15] = loadingfloor.mShoreSurroundedSheet;
                waterTileNumbers[0, 0] = loadingfloor.mShoreInnerTopLeftSheet;
                waterTileNumbers[0, 4] = loadingfloor.mShoreTopSheet;
                waterTileNumbers[0, 8] = loadingfloor.mShoreInnerTopRightSheet;
                waterTileNumbers[0, 1] = loadingfloor.mShoreLeftSheet;
                waterTileNumbers[0, 5] = loadingfloor.mWaterSheet;
                waterTileNumbers[0, 9] = loadingfloor.mShoreRightSheet;
                waterTileNumbers[0, 2] = loadingfloor.mShoreInnerBottomLeftSheet;
                waterTileNumbers[0, 6] = loadingfloor.mShoreBottomSheet;
                waterTileNumbers[0, 10] = loadingfloor.mShoreInnerBottomRightSheet;
                waterTileNumbers[0, 16] = loadingfloor.mShoreTopLeftSheet;
                waterTileNumbers[0, 19] = loadingfloor.mShoreTopRightSheet;
                waterTileNumbers[0, 17] = loadingfloor.mShoreBottomLeftSheet;
                waterTileNumbers[0, 20] = loadingfloor.mShoreBottomRightSheet;
                waterTileNumbers[0, 18] = loadingfloor.mShoreDiagonalForwardSheet;
                waterTileNumbers[0, 21] = loadingfloor.mShoreDiagonalBackSheet;
                waterTileNumbers[0, 12] = loadingfloor.mShoreInnerTopSheet;
                waterTileNumbers[0, 13] = loadingfloor.mShoreVerticalSheet;
                waterTileNumbers[0, 14] = loadingfloor.mShoreInnerBottomSheet;
                waterTileNumbers[0, 3] = loadingfloor.mShoreInnerLeftSheet;
                waterTileNumbers[0, 7] = loadingfloor.mShoreHorizontalSheet;
                waterTileNumbers[0, 11] = loadingfloor.mShoreInnerRightSheet;

                waterTileNumbers[1, 15] = loadingfloor.mShoreSurroundedX;
                waterTileNumbers[1, 0] = loadingfloor.mShoreInnerTopLeftX;
                waterTileNumbers[1, 4] = loadingfloor.mShoreTopX;
                waterTileNumbers[1, 8] = loadingfloor.mShoreInnerTopRightX;
                waterTileNumbers[1, 1] = loadingfloor.mShoreLeftX;
                waterTileNumbers[1, 5] = loadingfloor.mWaterX;
                waterTileNumbers[1, 9] = loadingfloor.mShoreRightX;
                waterTileNumbers[1, 2] = loadingfloor.mShoreInnerBottomLeftX;
                waterTileNumbers[1, 6] = loadingfloor.mShoreBottomX;
                waterTileNumbers[1, 10] = loadingfloor.mShoreInnerBottomRightX;
                waterTileNumbers[1, 16] = loadingfloor.mShoreTopLeftX;
                waterTileNumbers[1, 19] = loadingfloor.mShoreTopRightX;
                waterTileNumbers[1, 17] = loadingfloor.mShoreBottomLeftX;
                waterTileNumbers[1, 20] = loadingfloor.mShoreBottomRightX;
                waterTileNumbers[1, 18] = loadingfloor.mShoreDiagonalForwardX;
                waterTileNumbers[1, 21] = loadingfloor.mShoreDiagonalBackX;
                waterTileNumbers[1, 12] = loadingfloor.mShoreInnerTopX;
                waterTileNumbers[1, 13] = loadingfloor.mShoreVerticalX;
                waterTileNumbers[1, 14] = loadingfloor.mShoreInnerBottomX;
                waterTileNumbers[1, 3] = loadingfloor.mShoreInnerLeftX;
                waterTileNumbers[1, 7] = loadingfloor.mShoreHorizontalX;
                waterTileNumbers[1, 11] = loadingfloor.mShoreInnerRightX;

                for (int i = 0; i < 22; i++) {
                    picWaterTileset[i].Image = Graphics.GraphicsManager.Tiles[waterTileNumbers[0, i]][waterTileNumbers[1, i]];
                }

                lblSaveLoadMessage.Text = "Water Tile settings loaded";
            } else if (pnlRDungeonWaterAnimTiles != null && pnlRDungeonWaterAnimTiles.Visible == true) {

                waterAnimTileNumbers[0, 15] = loadingfloor.mShoreSurroundedAnimSheet;
                waterAnimTileNumbers[0, 0] = loadingfloor.mShoreInnerTopLeftAnimSheet;
                waterAnimTileNumbers[0, 4] = loadingfloor.mShoreTopAnimSheet;
                waterAnimTileNumbers[0, 8] = loadingfloor.mShoreInnerTopRightAnimSheet;
                waterAnimTileNumbers[0, 1] = loadingfloor.mShoreLeftAnimSheet;
                waterAnimTileNumbers[0, 5] = loadingfloor.mWaterAnimSheet;
                waterAnimTileNumbers[0, 9] = loadingfloor.mShoreRightAnimSheet;
                waterAnimTileNumbers[0, 2] = loadingfloor.mShoreInnerBottomLeftAnimSheet;
                waterAnimTileNumbers[0, 6] = loadingfloor.mShoreBottomAnimSheet;
                waterAnimTileNumbers[0, 10] = loadingfloor.mShoreInnerBottomRightAnimSheet;
                waterAnimTileNumbers[0, 16] = loadingfloor.mShoreTopLeftAnimSheet;
                waterAnimTileNumbers[0, 19] = loadingfloor.mShoreTopRightAnimSheet;
                waterAnimTileNumbers[0, 17] = loadingfloor.mShoreBottomLeftAnimSheet;
                waterAnimTileNumbers[0, 20] = loadingfloor.mShoreBottomRightAnimSheet;
                waterAnimTileNumbers[0, 18] = loadingfloor.mShoreDiagonalForwardAnimSheet;
                waterAnimTileNumbers[0, 21] = loadingfloor.mShoreDiagonalBackAnimSheet;
                waterAnimTileNumbers[0, 12] = loadingfloor.mShoreInnerTopAnimSheet;
                waterAnimTileNumbers[0, 13] = loadingfloor.mShoreVerticalAnimSheet;
                waterAnimTileNumbers[0, 14] = loadingfloor.mShoreInnerBottomAnimSheet;
                waterAnimTileNumbers[0, 3] = loadingfloor.mShoreInnerLeftAnimSheet;
                waterAnimTileNumbers[0, 7] = loadingfloor.mShoreHorizontalAnimSheet;
                waterAnimTileNumbers[0, 11] = loadingfloor.mShoreInnerRightAnimSheet;

                waterAnimTileNumbers[1, 15] = loadingfloor.mShoreSurroundedAnimX;
                waterAnimTileNumbers[1, 0] = loadingfloor.mShoreInnerTopLeftAnimX;
                waterAnimTileNumbers[1, 4] = loadingfloor.mShoreTopAnimX;
                waterAnimTileNumbers[1, 8] = loadingfloor.mShoreInnerTopRightAnimX;
                waterAnimTileNumbers[1, 1] = loadingfloor.mShoreLeftAnimX;
                waterAnimTileNumbers[1, 5] = loadingfloor.mWaterAnimX;
                waterAnimTileNumbers[1, 9] = loadingfloor.mShoreRightAnimX;
                waterAnimTileNumbers[1, 2] = loadingfloor.mShoreInnerBottomLeftAnimX;
                waterAnimTileNumbers[1, 6] = loadingfloor.mShoreBottomAnimX;
                waterAnimTileNumbers[1, 10] = loadingfloor.mShoreInnerBottomRightAnimX;
                waterAnimTileNumbers[1, 16] = loadingfloor.mShoreTopLeftAnimX;
                waterAnimTileNumbers[1, 19] = loadingfloor.mShoreTopRightAnimX;
                waterAnimTileNumbers[1, 17] = loadingfloor.mShoreBottomLeftAnimX;
                waterAnimTileNumbers[1, 20] = loadingfloor.mShoreBottomRightAnimX;
                waterAnimTileNumbers[1, 18] = loadingfloor.mShoreDiagonalForwardAnimX;
                waterAnimTileNumbers[1, 21] = loadingfloor.mShoreDiagonalBackAnimX;
                waterAnimTileNumbers[1, 12] = loadingfloor.mShoreInnerTopAnimX;
                waterAnimTileNumbers[1, 13] = loadingfloor.mShoreVerticalAnimX;
                waterAnimTileNumbers[1, 14] = loadingfloor.mShoreInnerBottomAnimX;
                waterAnimTileNumbers[1, 3] = loadingfloor.mShoreInnerLeftAnimX;
                waterAnimTileNumbers[1, 7] = loadingfloor.mShoreHorizontalAnimX;
                waterAnimTileNumbers[1, 11] = loadingfloor.mShoreInnerRightAnimX;

                for (int i = 0; i < 22; i++) {
                    picWaterAnimTileset[i].Image = Graphics.GraphicsManager.Tiles[waterAnimTileNumbers[0, i]][waterAnimTileNumbers[1, i]];
                }

                lblSaveLoadMessage.Text = "Water Anim Tile settings loaded";
            } else if (pnlRDungeonAttributes != null && pnlRDungeonAttributes.Visible == true) {

                cbGroundType.SelectItem((int)loadingfloor.GroundTile.Type);
                nudGroundData1.Value = loadingfloor.GroundTile.Data1;
                nudGroundData2.Value = loadingfloor.GroundTile.Data2;
                nudGroundData3.Value = loadingfloor.GroundTile.Data3;
                txtGroundString1.Text = loadingfloor.GroundTile.String1;
                txtGroundString2.Text = loadingfloor.GroundTile.String2;
                txtGroundString3.Text = loadingfloor.GroundTile.String3;

                cbHallType.SelectItem((int)loadingfloor.HallTile.Type);
                nudHallData1.Value = loadingfloor.HallTile.Data1;
                nudHallData2.Value = loadingfloor.HallTile.Data2;
                nudHallData3.Value = loadingfloor.HallTile.Data3;
                txtHallString1.Text = loadingfloor.HallTile.String1;
                txtHallString2.Text = loadingfloor.HallTile.String2;
                txtHallString3.Text = loadingfloor.HallTile.String3;

                cbWaterType.SelectItem((int)loadingfloor.WaterTile.Type);
                nudWaterData1.Value = loadingfloor.WaterTile.Data1;
                nudWaterData2.Value = loadingfloor.WaterTile.Data2;
                nudWaterData3.Value = loadingfloor.WaterTile.Data3;
                txtWaterString1.Text = loadingfloor.WaterTile.String1;
                txtWaterString2.Text = loadingfloor.WaterTile.String2;
                txtWaterString3.Text = loadingfloor.WaterTile.String3;

                cbWallType.SelectItem((int)loadingfloor.WallTile.Type);
                nudWallData1.Value = loadingfloor.WallTile.Data1;
                nudWallData2.Value = loadingfloor.WallTile.Data2;
                nudWallData3.Value = loadingfloor.WallTile.Data3;
                txtWallString1.Text = loadingfloor.WallTile.String1;
                txtWallString2.Text = loadingfloor.WallTile.String2;
                txtWallString3.Text = loadingfloor.WallTile.String3;

                lblSaveLoadMessage.Text = "Attribute settings loaded";
            } else if (pnlRDungeonItems != null && pnlRDungeonItems.Visible == true) {

                itemList.Clear();
                lbxItems.Items.Clear();
                for (int item = 0; item < loadingfloor.Items.Count; item++) {
                    EditableRDungeonItem newItem = new EditableRDungeonItem();
                    newItem.ItemNum = loadingfloor.Items[item].ItemNum;
                    newItem.MinAmount = loadingfloor.Items[item].MinAmount;
                    newItem.MaxAmount = loadingfloor.Items[item].MaxAmount;
                    newItem.AppearanceRate = loadingfloor.Items[item].AppearanceRate;
                    newItem.StickyRate = loadingfloor.Items[item].StickyRate;
                    newItem.Tag = loadingfloor.Items[item].Tag;
                    newItem.Hidden = loadingfloor.Items[item].Hidden;
                    newItem.OnGround = loadingfloor.Items[item].OnGround;
                    newItem.OnWater = loadingfloor.Items[item].OnWater;
                    newItem.OnWall = loadingfloor.Items[item].OnWall;

                    itemList.Add(newItem);
                    lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + newItem.AppearanceRate + "%) " + newItem.MinAmount + "-" + newItem.MaxAmount + " " + Items.ItemHelper.Items[newItem.ItemNum].Name + " (" + newItem.StickyRate + "% Sticky)"));
                }


                lblSaveLoadMessage.Text = "Item settings loaded";
            } else if (pnlRDungeonNpcs != null && pnlRDungeonNpcs.Visible == true) {

                nudNpcSpawnTime.Value = loadingfloor.NpcSpawnTime;
                nudNpcMin.Value = loadingfloor.NpcMin;
                nudNpcMax.Value = loadingfloor.NpcMax;

                npcList.Clear();
                lbxNpcs.Items.Clear();
                for (int npc = 0; npc < loadingfloor.Npcs.Count; npc++) {
                    MapNpcSettings newNpc = new MapNpcSettings();
                    newNpc.NpcNum = loadingfloor.Npcs[npc].NpcNum;
                    newNpc.MinLevel = loadingfloor.Npcs[npc].MinLevel;
                    newNpc.MaxLevel = loadingfloor.Npcs[npc].MaxLevel;
                    newNpc.AppearanceRate = loadingfloor.Npcs[npc].AppearanceRate;
                    newNpc.StartStatus = loadingfloor.Npcs[npc].StartStatus;
                    newNpc.StartStatusCounter = loadingfloor.Npcs[npc].StartStatusCounter;
                    newNpc.StartStatusChance = loadingfloor.Npcs[npc].StartStatusChance;

                    npcList.Add(newNpc);
                    lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                + "(" + newNpc.AppearanceRate + "%) " + "Lv." + newNpc.MinLevel + "-" + newNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newNpc.NpcNum].Name
                + " [" + newNpc.StartStatusChance + "% " + newNpc.StartStatus.ToString() + "]"));

                }

                lblSaveLoadMessage.Text = "Npc settings loaded";
            } else if (pnlRDungeonTraps != null && pnlRDungeonTraps.Visible == true) {

                trapList.Clear();
                lbxTraps.Items.Clear();
                for (int traps = 0; traps < loadingfloor.SpecialTiles.Count; traps++) {
                    EditableRDungeonTrap newTile = new EditableRDungeonTrap();

                    newTile.SpecialTile.Ground = loadingfloor.SpecialTiles[traps].SpecialTile.Ground;
                    newTile.SpecialTile.GroundAnim = loadingfloor.SpecialTiles[traps].SpecialTile.GroundAnim;
                    newTile.SpecialTile.Mask = loadingfloor.SpecialTiles[traps].SpecialTile.Mask;
                    newTile.SpecialTile.Anim = loadingfloor.SpecialTiles[traps].SpecialTile.Anim;
                    newTile.SpecialTile.Mask2 = loadingfloor.SpecialTiles[traps].SpecialTile.Mask2;
                    newTile.SpecialTile.M2Anim = loadingfloor.SpecialTiles[traps].SpecialTile.M2Anim;
                    newTile.SpecialTile.Fringe = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe;
                    newTile.SpecialTile.FAnim = loadingfloor.SpecialTiles[traps].SpecialTile.FAnim;
                    newTile.SpecialTile.Fringe2 = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe2;
                    newTile.SpecialTile.F2Anim = loadingfloor.SpecialTiles[traps].SpecialTile.F2Anim;

                    newTile.SpecialTile.GroundSet = loadingfloor.SpecialTiles[traps].SpecialTile.GroundSet;
                    newTile.SpecialTile.GroundAnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.GroundAnimSet;
                    newTile.SpecialTile.MaskSet = loadingfloor.SpecialTiles[traps].SpecialTile.MaskSet;
                    newTile.SpecialTile.AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.AnimSet;
                    newTile.SpecialTile.Mask2Set = loadingfloor.SpecialTiles[traps].SpecialTile.Mask2Set;
                    newTile.SpecialTile.M2AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.M2AnimSet;
                    newTile.SpecialTile.FringeSet = loadingfloor.SpecialTiles[traps].SpecialTile.FringeSet;
                    newTile.SpecialTile.FAnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.FAnimSet;
                    newTile.SpecialTile.Fringe2Set = loadingfloor.SpecialTiles[traps].SpecialTile.Fringe2Set;
                    newTile.SpecialTile.F2AnimSet = loadingfloor.SpecialTiles[traps].SpecialTile.F2AnimSet;

                    newTile.SpecialTile.Type = loadingfloor.SpecialTiles[traps].SpecialTile.Type;
                    newTile.SpecialTile.Data1 = loadingfloor.SpecialTiles[traps].SpecialTile.Data1;
                    newTile.SpecialTile.Data2 = loadingfloor.SpecialTiles[traps].SpecialTile.Data2;
                    newTile.SpecialTile.Data3 = loadingfloor.SpecialTiles[traps].SpecialTile.Data3;
                    newTile.SpecialTile.String1 = loadingfloor.SpecialTiles[traps].SpecialTile.String1;
                    newTile.SpecialTile.String2 = loadingfloor.SpecialTiles[traps].SpecialTile.String2;
                    newTile.SpecialTile.String3 = loadingfloor.SpecialTiles[traps].SpecialTile.String3;

                    newTile.AppearanceRate = loadingfloor.SpecialTiles[traps].AppearanceRate;

                    trapList.Add(newTile);
                    lbxTraps.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (traps + 1) + ": " + newTile.SpecialTile.Type + "/" + newTile.SpecialTile.Data1 + "/" + newTile.SpecialTile.Data2 + "/" + newTile.SpecialTile.Data3));
                }


                lblSaveLoadMessage.Text = "Trap settings loaded";
            } else if (pnlRDungeonWeather != null && pnlRDungeonWeather.Visible == true) {

                lbxWeather.Items.Clear();
                for (int weather = 0; weather < loadingfloor.Weather.Count; weather++) {
                    lbxWeather.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (weather + 1) + ":" + (int)loadingfloor.Weather[weather] + ": " + Enum.GetName(typeof(Enums.Weather), loadingfloor.Weather[weather])));
                }

                lblSaveLoadMessage.Text = "Weather settings loaded";
            } else if (pnlRDungeonGoal != null && pnlRDungeonGoal.Visible == true) {

                switch (loadingfloor.GoalType) {
                    case Enums.RFloorGoalType.NextFloor: {
                            optNextFloor.Checked = true;
                        }
                        break;
                    case Enums.RFloorGoalType.Map: {
                            optMap.Checked = true;
                        }
                        break;
                    case Enums.RFloorGoalType.Scripted: {
                            optScripted.Checked = true;
                        }
                        break;
                    default: {
                            optNextFloor.Checked = true;
                        }
                        break;
                }

                nudData1.Value = loadingfloor.GoalMap;
                nudData2.Value = loadingfloor.GoalX;
                nudData3.Value = loadingfloor.GoalY;

                lblSaveLoadMessage.Text = "Goal settings loaded";
            } else if (pnlRDungeonChambers != null && pnlRDungeonChambers.Visible == true) {

                chamberList.Clear();
                lbxChambers.Items.Clear();
                for (int chamber = 0; chamber < loadingfloor.Chambers.Count; chamber++) {
                    EditableRDungeonChamber newChamber = new EditableRDungeonChamber();
                    newChamber.ChamberNum = loadingfloor.Chambers[chamber].ChamberNum;
                    newChamber.String1 = loadingfloor.Chambers[chamber].String1;
                    newChamber.String2 = loadingfloor.Chambers[chamber].String2;
                    newChamber.String3 = loadingfloor.Chambers[chamber].String3;
                    chamberList.Add(newChamber);
                    lbxChambers.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "#" + loadingfloor.Chambers[chamber].ChamberNum + "/" + loadingfloor.Chambers[chamber].String1 + "/" + loadingfloor.Chambers[chamber].String2 + "/" + loadingfloor.Chambers[chamber].String3));
                }

                lblSaveLoadMessage.Text = "Chamber settings loaded";
            } else if (pnlRDungeonMisc != null && pnlRDungeonMisc.Visible == true) {

                nudDarkness.Value = loadingfloor.Darkness;
                if (loadingfloor.Music == "") {
                    lbxMusic.SelectItem(0);
                } else {
                    for (int i = 0; i < lbxMusic.Items.Count; i++) {
                        if (lbxMusic.Items[i].TextIdentifier == loadingfloor.Music) {
                            lbxMusic.SelectItem(i);
                        }
                    }
                }

                lblSaveLoadMessage.Text = "Misc settings loaded";
            }
        }

        #endregion

        #region Settings Menu
        void btnStructure_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonStructure();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonStructure.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnLandTiles_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonLandTiles();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonLandTiles.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnWaterTiles_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonWaterTiles();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonWaterTiles.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnAttributes_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonAttributes();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonAttributes.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnItems_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonItems();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonItems.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnNpcs_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonNpcs();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonNpcs.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnTraps_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonTraps();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonTraps.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";

        }

        void btnWeather_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonWeather();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonWeather.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnGoal_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonGoal();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonGoal.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnChambers_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonChambers();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonChambers.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        void btnMisc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonMisc();

            pnlRDungeonFloorSettingSelection.Visible = false;
            pnlRDungeonMisc.Visible = true;
            btnSaveFloor.Text = "Save These Settings to Floor(s)";
            btnLoadFloor.Text = "Load These Settings from Floor";
        }

        #endregion

        #region Land Tiles

        void picLandTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlTileSelector();
            editedTile = Array.IndexOf(picLandTileset, sender);
            pnlRDungeonLandTiles.Visible = false;
            pnlTileSelector.Visible = true;

        }

        void btnLandAltSwitch_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonLandAltTiles();

            pnlRDungeonLandTiles.Visible = false;
            pnlRDungeonLandAltTiles.Visible = true;
        }

        #endregion
        #region Land Alt Tiles

        void picLandAltTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlTileSelector();
            editedTile = Array.IndexOf(picLandAltTileset, sender) + 44;
            pnlRDungeonLandAltTiles.Visible = false;
            pnlTileSelector.Visible = true;

        }

        void btnLandSwitch_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            pnlRDungeonLandAltTiles.Visible = false;
            pnlRDungeonLandTiles.Visible = true;
        }

        #endregion
        #region Water Tiles

        void picWaterTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlTileSelector();
            editedTile = Array.IndexOf(picWaterTileset, sender) + 22;
            pnlRDungeonWaterTiles.Visible = false;
            pnlTileSelector.Visible = true;

        }

        void btnWaterAnimSwitch_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlRDungeonWaterAnimTiles();

            pnlRDungeonWaterTiles.Visible = false;
            pnlRDungeonWaterAnimTiles.Visible = true;
        }

        #endregion
        #region Water Anim Tiles

        void picWaterAnimTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlTileSelector();
            editedTile = Array.IndexOf(picWaterAnimTileset, sender) + 67;
            pnlRDungeonWaterAnimTiles.Visible = false;
            pnlTileSelector.Visible = true;

        }

        void btnWaterSwitch_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            pnlRDungeonWaterAnimTiles.Visible = false;
            pnlRDungeonWaterTiles.Visible = true;
        }

        #endregion
        #region Items

        void nudItemNum_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {

            if (nudItemNum.Value > 0 && nudItemNum.Value < MaxInfo.MaxItems) {

                lblItemNum.Text = Items.ItemHelper.Items[nudItemNum.Value].Name;

            } else {
                lblItemNum.Text = "Item # ";
            }

        }

        void btnAddItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (nudItemNum.Value > 0) {
                EditableRDungeonItem newItem = new EditableRDungeonItem();
                newItem.ItemNum = nudItemNum.Value;
                newItem.MinAmount = nudMinValue.Value;
                newItem.MaxAmount = nudMaxValue.Value;
                newItem.AppearanceRate = nudItemSpawnRate.Value;
                newItem.StickyRate = nudStickyRate.Value;
                newItem.Tag = txtTag.Text;
                newItem.Hidden = chkHidden.Checked;
                newItem.OnGround = chkOnGround.Checked;
                newItem.OnWater = chkOnWater.Checked;
                newItem.OnWall = chkOnWall.Checked;

                if (chkBulkItem.Checked) {
                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        if (lbxItems.SelectedIndex >= 0 && lbxItems.SelectedIndex < rdungeon.Floors[floor].Items.Count) {
                            rdungeon.Floors[floor].Items.Insert(lbxItems.SelectedIndex, newItem);
                        } else {
                            rdungeon.Floors[floor].Items.Add(newItem);
                        }

                    }

                    itemList.Clear();
                    lbxItems.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int item = 0; item < loadingfloor.Items.Count; item++) {
                        EditableRDungeonItem newLoadItem = new EditableRDungeonItem();
                        newLoadItem.ItemNum = loadingfloor.Items[item].ItemNum;
                        newLoadItem.MinAmount = loadingfloor.Items[item].MinAmount;
                        newLoadItem.MaxAmount = loadingfloor.Items[item].MaxAmount;
                        newLoadItem.AppearanceRate = loadingfloor.Items[item].AppearanceRate;
                        newLoadItem.StickyRate = loadingfloor.Items[item].StickyRate;
                        newLoadItem.Tag = loadingfloor.Items[item].Tag;
                        newLoadItem.Hidden = loadingfloor.Items[item].Hidden;
                        newLoadItem.OnGround = loadingfloor.Items[item].OnGround;
                        newLoadItem.OnWater = loadingfloor.Items[item].OnWater;
                        newLoadItem.OnWall = loadingfloor.Items[item].OnWall;

                        itemList.Add(newLoadItem);
                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + newLoadItem.AppearanceRate + "%) " + newLoadItem.MinAmount + "-" + newLoadItem.MaxAmount + " " + Items.ItemHelper.Items[newLoadItem.ItemNum].Name + " (" + newLoadItem.StickyRate + "% Sticky)"));
                    }

                } else {
                    if (lbxItems.SelectedIndex >= 0 && lbxItems.SelectedIndex < lbxItems.Items.Count) {
                        itemList.Insert(lbxItems.SelectedIndex, newItem);
                    } else {
                        itemList.Add(newItem);
                    }

                    lbxItems.Items.Clear();
                    for (int item = 0; item < itemList.Count; item++) {
                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + itemList[item].AppearanceRate + "%) " + itemList[item].MinAmount + "-" + itemList[item].MaxAmount + " " + Items.ItemHelper.Items[itemList[item].ItemNum].Name + " (" + itemList[item].StickyRate + "% Sticky)"));
                    }

                }
            }
        }

        void btnLoadItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (lbxItems.SelectedIndex > -1) {
                nudItemNum.Value = itemList[lbxItems.SelectedIndex].ItemNum;
                nudMinValue.Value = itemList[lbxItems.SelectedIndex].MinAmount;
                nudMaxValue.Value = itemList[lbxItems.SelectedIndex].MaxAmount;
                nudItemSpawnRate.Value = itemList[lbxItems.SelectedIndex].AppearanceRate;
                nudStickyRate.Value = itemList[lbxItems.SelectedIndex].StickyRate;
                txtTag.Text = itemList[lbxItems.SelectedIndex].Tag;
                chkHidden.Checked = itemList[lbxItems.SelectedIndex].Hidden;
                chkOnGround.Checked = itemList[lbxItems.SelectedIndex].OnGround;
                chkOnWater.Checked = itemList[lbxItems.SelectedIndex].OnWater;
                chkOnWall.Checked = itemList[lbxItems.SelectedIndex].OnWall;
            }

        }

        void btnChangeItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (lbxItems.SelectedIndex > -1) {
                EditableRDungeonItem newItem = new EditableRDungeonItem();
                newItem.ItemNum = nudItemNum.Value;
                newItem.MinAmount = nudMinValue.Value;
                newItem.MaxAmount = nudMaxValue.Value;
                newItem.AppearanceRate = nudItemSpawnRate.Value;
                newItem.StickyRate = nudStickyRate.Value;
                newItem.Tag = txtTag.Text;
                newItem.Hidden = chkHidden.Checked;
                newItem.OnGround = chkOnGround.Checked;
                newItem.OnWater = chkOnWater.Checked;
                newItem.OnWall = chkOnWall.Checked;

                if (chkBulkItem.Checked) {
                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        for (int j = 0; j < rdungeon.Floors[floor].Items.Count; j++) {
                            if (itemList[lbxItems.SelectedIndex].Equals(rdungeon.Floors[floor].Items[j])) {
                                rdungeon.Floors[floor].Items[j] = newItem;
                            }
                        }
                    }

                    itemList.Clear();
                    lbxItems.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int item = 0; item < loadingfloor.Items.Count; item++) {
                        EditableRDungeonItem newLoadItem = new EditableRDungeonItem();
                        newLoadItem.ItemNum = loadingfloor.Items[item].ItemNum;
                        newLoadItem.MinAmount = loadingfloor.Items[item].MinAmount;
                        newLoadItem.MaxAmount = loadingfloor.Items[item].MaxAmount;
                        newLoadItem.AppearanceRate = loadingfloor.Items[item].AppearanceRate;
                        newLoadItem.StickyRate = loadingfloor.Items[item].StickyRate;
                        newLoadItem.Tag = loadingfloor.Items[item].Tag;
                        newLoadItem.Hidden = loadingfloor.Items[item].Hidden;
                        newLoadItem.OnGround = loadingfloor.Items[item].OnGround;
                        newLoadItem.OnWater = loadingfloor.Items[item].OnWater;
                        newLoadItem.OnWall = loadingfloor.Items[item].OnWall;

                        itemList.Add(newLoadItem);
                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + newLoadItem.AppearanceRate + "%) " + newLoadItem.MinAmount + "-" + newLoadItem.MaxAmount + " " + Items.ItemHelper.Items[newLoadItem.ItemNum].Name + " (" + newLoadItem.StickyRate + "% Sticky)"));
                    }

                } else {
                    itemList[lbxItems.SelectedIndex] = newItem;

                    lbxItems.Items.Clear();
                    for (int item = 0; item < itemList.Count; item++) {

                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + itemList[item].AppearanceRate + "%) " + itemList[item].MinAmount + "-" + itemList[item].MaxAmount + " " + Items.ItemHelper.Items[itemList[item].ItemNum].Name + " (" + itemList[item].StickyRate + "% Sticky)"));
                    }
                }
            }

        }

        void btnRemoveItem_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxItems.SelectedIndex > -1) {

                if (chkBulkItem.Checked) {
                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        for (int j = rdungeon.Floors[floor].Items.Count-1; j >= 0; j--) {
                            if (itemList[lbxItems.SelectedIndex].Equals(rdungeon.Floors[floor].Items[j])) {
                                rdungeon.Floors[floor].Items.RemoveAt(j);
                            }
                        }
                    }

                    itemList.Clear();
                    lbxItems.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int item = 0; item < loadingfloor.Items.Count; item++) {
                        EditableRDungeonItem newLoadItem = new EditableRDungeonItem();
                        newLoadItem.ItemNum = loadingfloor.Items[item].ItemNum;
                        newLoadItem.MinAmount = loadingfloor.Items[item].MinAmount;
                        newLoadItem.MaxAmount = loadingfloor.Items[item].MaxAmount;
                        newLoadItem.AppearanceRate = loadingfloor.Items[item].AppearanceRate;
                        newLoadItem.StickyRate = loadingfloor.Items[item].StickyRate;
                        newLoadItem.Tag = loadingfloor.Items[item].Tag;
                        newLoadItem.Hidden = loadingfloor.Items[item].Hidden;
                        newLoadItem.OnGround = loadingfloor.Items[item].OnGround;
                        newLoadItem.OnWater = loadingfloor.Items[item].OnWater;
                        newLoadItem.OnWall = loadingfloor.Items[item].OnWall;

                        itemList.Add(newLoadItem);
                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + newLoadItem.AppearanceRate + "%) " + newLoadItem.MinAmount + "-" + newLoadItem.MaxAmount + " " + Items.ItemHelper.Items[newLoadItem.ItemNum].Name + " (" + newLoadItem.StickyRate + "% Sticky)"));
                    }

                } else {
                    itemList.RemoveAt(lbxItems.SelectedIndex);
                    lbxItems.Items.Clear();
                    for (int item = 0; item < itemList.Count; item++) {

                        lbxItems.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (item + 1) + ": (" + itemList[item].AppearanceRate + "%) " + itemList[item].MinAmount + "-" + itemList[item].MaxAmount + " " + Items.ItemHelper.Items[itemList[item].ItemNum].Name + " (" + itemList[item].StickyRate + "% Sticky)"));
                    }
                }
            }

        }

        #endregion
        #region NPCs

        void nudNpcNum_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {

            if (nudNpcNum.Value > 0 && nudNpcNum.Value < MaxInfo.MaxNpcs) {

                lblNpcNum.Text = Npc.NpcHelper.Npcs[nudNpcNum.Value].Name;

            } else {
                lblNpcNum.Text = "NPC #";
            }

        }

        void btnAddNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (nudNpcNum.Value > 0) {
                MapNpcSettings newNpc = new MapNpcSettings();
                newNpc.NpcNum = nudNpcNum.Value;
                newNpc.MinLevel = nudMinLevel.Value;
                newNpc.MaxLevel = nudMaxLevel.Value;
                newNpc.AppearanceRate = nudNpcSpawnRate.Value;
                newNpc.StartStatus = (Enums.StatusAilment)cbNpcStartStatus.SelectedIndex;
                newNpc.StartStatusCounter = nudStatusCounter.Value;
                newNpc.StartStatusChance = nudStatusChance.Value;

                if (chkBulkNpc.Checked) {
                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        if (lbxNpcs.SelectedIndex >= 0 && lbxNpcs.SelectedIndex < rdungeon.Floors[floor].Npcs.Count) {
                            rdungeon.Floors[floor].Npcs.Insert(lbxNpcs.SelectedIndex, newNpc);
                        } else {
                            rdungeon.Floors[floor].Npcs.Add(newNpc);
                        }

                    }

                    npcList.Clear();
                    lbxNpcs.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int npc = 0; npc < loadingfloor.Npcs.Count; npc++) {
                        MapNpcSettings newLoadNpc = new MapNpcSettings();
                        newLoadNpc.NpcNum = loadingfloor.Npcs[npc].NpcNum;
                        newLoadNpc.MinLevel = loadingfloor.Npcs[npc].MinLevel;
                        newLoadNpc.MaxLevel = loadingfloor.Npcs[npc].MaxLevel;
                        newLoadNpc.AppearanceRate = loadingfloor.Npcs[npc].AppearanceRate;
                        newLoadNpc.StartStatus = loadingfloor.Npcs[npc].StartStatus;
                        newLoadNpc.StartStatusCounter = loadingfloor.Npcs[npc].StartStatusCounter;
                        newLoadNpc.StartStatusChance = loadingfloor.Npcs[npc].StartStatusChance;

                        npcList.Add(newLoadNpc);
                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                    + "(" + newLoadNpc.AppearanceRate + "%) " + "Lv." + newLoadNpc.MinLevel + "-" + newLoadNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newLoadNpc.NpcNum].Name
                    + " [" + newLoadNpc.StartStatusChance + "% " + newLoadNpc.StartStatus.ToString() + "]"));
                    }

                } else {
                    if (lbxNpcs.SelectedIndex >= 0 && lbxNpcs.SelectedIndex < lbxNpcs.Items.Count) {
                        npcList.Insert(lbxNpcs.SelectedIndex, newNpc);
                    } else {
                        npcList.Add(newNpc);
                    }

                    lbxNpcs.Items.Clear();
                    for (int npc = 0; npc < npcList.Count; npc++) {

                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                        + "(" + npcList[npc].AppearanceRate + "%) " + "Lv." + npcList[npc].MinLevel + "-" + npcList[npc].MaxLevel + " "
                        + Npc.NpcHelper.Npcs[npcList[npc].NpcNum].Name
                        + " [" + npcList[npc].StartStatusChance + "% " + npcList[npc].StartStatus.ToString() + "]"));
                    }

                }
            }
        }


        void btnLoadNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (lbxNpcs.SelectedIndex > -1) {
                nudNpcNum.Value = npcList[lbxNpcs.SelectedIndex].NpcNum;
                nudMinLevel.Value = npcList[lbxNpcs.SelectedIndex].MinLevel;
                nudMaxLevel.Value = npcList[lbxNpcs.SelectedIndex].MaxLevel;
                nudNpcSpawnRate.Value = npcList[lbxNpcs.SelectedIndex].AppearanceRate;
                cbNpcStartStatus.SelectItem((int)npcList[lbxNpcs.SelectedIndex].StartStatus);
                nudStatusCounter.Value = npcList[lbxNpcs.SelectedIndex].StartStatusCounter;
                nudStatusChance.Value = npcList[lbxNpcs.SelectedIndex].StartStatusChance;
            }

        }

        void btnChangeNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (nudNpcNum.Value > 0) {
                MapNpcSettings newNpc = new MapNpcSettings();
                newNpc.NpcNum = nudNpcNum.Value;
                newNpc.MinLevel = nudMinLevel.Value;
                newNpc.MaxLevel = nudMaxLevel.Value;
                newNpc.AppearanceRate = nudNpcSpawnRate.Value;
                newNpc.StartStatus = (Enums.StatusAilment)cbNpcStartStatus.SelectedIndex;
                newNpc.StartStatusCounter = nudStatusCounter.Value;
                newNpc.StartStatusChance = nudStatusChance.Value;

                if (chkBulkNpc.Checked) {
                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        for (int j = 0; j < rdungeon.Floors[floor].Npcs.Count; j++) {
                            if (npcList[lbxNpcs.SelectedIndex].Equals(rdungeon.Floors[floor].Npcs[j])) {
                                rdungeon.Floors[floor].Npcs[j] = newNpc;
                            }
                        }
                    }

                    npcList.Clear();
                    lbxNpcs.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int npc = 0; npc < loadingfloor.Npcs.Count; npc++) {
                        MapNpcSettings newLoadNpc = new MapNpcSettings();
                        newLoadNpc.NpcNum = loadingfloor.Npcs[npc].NpcNum;
                        newLoadNpc.MinLevel = loadingfloor.Npcs[npc].MinLevel;
                        newLoadNpc.MaxLevel = loadingfloor.Npcs[npc].MaxLevel;
                        newLoadNpc.AppearanceRate = loadingfloor.Npcs[npc].AppearanceRate;
                        newLoadNpc.StartStatus = loadingfloor.Npcs[npc].StartStatus;
                        newLoadNpc.StartStatusCounter = loadingfloor.Npcs[npc].StartStatusCounter;
                        newLoadNpc.StartStatusChance = loadingfloor.Npcs[npc].StartStatusChance;

                        npcList.Add(newLoadNpc);
                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                    + "(" + newLoadNpc.AppearanceRate + "%) " + "Lv." + newLoadNpc.MinLevel + "-" + newLoadNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newLoadNpc.NpcNum].Name
                    + " [" + newLoadNpc.StartStatusChance + "% " + newLoadNpc.StartStatus.ToString() + "]"));
                    }

                } else {
                    npcList[lbxNpcs.SelectedIndex] = newNpc;

                    lbxNpcs.Items.Clear();
                    for (int npc = 0; npc < npcList.Count; npc++) {

                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                        + "(" + npcList[npc].AppearanceRate + "%) " + "Lv." + npcList[npc].MinLevel + "-" + npcList[npc].MaxLevel + " "
                        + Npc.NpcHelper.Npcs[npcList[npc].NpcNum].Name
                        + " [" + npcList[npc].StartStatusChance + "% " + npcList[npc].StartStatus.ToString() + "]"));
                    }

                }
            }
            
        }

        void btnRemoveNpc_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (lbxNpcs.SelectedIndex > -1) {

                if (chkBulkNpc.Checked) {

                    for (int floor = nudFirstFloor.Value - 1; floor < nudLastFloor.Value; floor++) {
                        for (int j = rdungeon.Floors[floor].Npcs.Count - 1; j >= 0; j--) {
                            if (npcList[lbxNpcs.SelectedIndex].Equals(rdungeon.Floors[floor].Npcs[j])) {
                                rdungeon.Floors[floor].Npcs.RemoveAt(j);
                            }
                        }
                    }

                    npcList.Clear();
                    lbxNpcs.Items.Clear();
                    EditableRDungeonFloor loadingfloor = rdungeon.Floors[nudFirstFloor.Value - 1];
                    for (int npc = 0; npc < loadingfloor.Npcs.Count; npc++) {
                        MapNpcSettings newLoadNpc = new MapNpcSettings();
                        newLoadNpc.NpcNum = loadingfloor.Npcs[npc].NpcNum;
                        newLoadNpc.MinLevel = loadingfloor.Npcs[npc].MinLevel;
                        newLoadNpc.MaxLevel = loadingfloor.Npcs[npc].MaxLevel;
                        newLoadNpc.AppearanceRate = loadingfloor.Npcs[npc].AppearanceRate;
                        newLoadNpc.StartStatus = loadingfloor.Npcs[npc].StartStatus;
                        newLoadNpc.StartStatusCounter = loadingfloor.Npcs[npc].StartStatusCounter;
                        newLoadNpc.StartStatusChance = loadingfloor.Npcs[npc].StartStatusChance;

                        npcList.Add(newLoadNpc);
                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                    + "(" + newLoadNpc.AppearanceRate + "%) " + "Lv." + newLoadNpc.MinLevel + "-" + newLoadNpc.MaxLevel + " " + Npc.NpcHelper.Npcs[newLoadNpc.NpcNum].Name
                    + " [" + newLoadNpc.StartStatusChance + "% " + newLoadNpc.StartStatus.ToString() + "]"));
                    }

                } else {
                    npcList.RemoveAt(lbxNpcs.SelectedIndex);


                    lbxNpcs.Items.Clear();
                    for (int npc = 0; npc < npcList.Count; npc++) {

                        lbxNpcs.Items.Add(new ListBoxTextItem(Client.Logic.Graphics.FontManager.LoadFont("tahoma", 10), (npc + 1) + ": "
                        + "(" + npcList[npc].AppearanceRate + "%) " + "Lv." + npcList[npc].MinLevel + "-" + npcList[npc].MaxLevel + " "
                        + Npc.NpcHelper.Npcs[npcList[npc].NpcNum].Name
                        + " [" + npcList[npc].StartStatusChance + "% " + npcList[npc].StartStatus.ToString() + "]"));
                    }

                }
                
            }

        }

        #endregion
        #region Traps

        void btnAddTrap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (cbTrapType.SelectedIndex > -1) {
                EditableRDungeonTrap newTile = new EditableRDungeonTrap();
                newTile.SpecialTile.Ground = trapTileNumbers[1, 0];
                newTile.SpecialTile.GroundAnim = trapTileNumbers[1, 1];
                newTile.SpecialTile.Mask = trapTileNumbers[1, 2];
                newTile.SpecialTile.Anim = trapTileNumbers[1, 3];
                newTile.SpecialTile.Mask2 = trapTileNumbers[1, 4];
                newTile.SpecialTile.M2Anim = trapTileNumbers[1, 5];
                newTile.SpecialTile.Fringe = trapTileNumbers[1, 6];
                newTile.SpecialTile.FAnim = trapTileNumbers[1, 7];
                newTile.SpecialTile.Fringe2 = trapTileNumbers[1, 8];
                newTile.SpecialTile.F2Anim = trapTileNumbers[1, 9];

                newTile.SpecialTile.GroundSet = trapTileNumbers[0, 0];
                newTile.SpecialTile.GroundAnimSet = trapTileNumbers[0, 1];
                newTile.SpecialTile.MaskSet = trapTileNumbers[0, 2];
                newTile.SpecialTile.AnimSet = trapTileNumbers[0, 3];
                newTile.SpecialTile.Mask2Set = trapTileNumbers[0, 4];
                newTile.SpecialTile.M2AnimSet = trapTileNumbers[0, 5];
                newTile.SpecialTile.FringeSet = trapTileNumbers[0, 6];
                newTile.SpecialTile.FAnimSet = trapTileNumbers[0, 7];
                newTile.SpecialTile.Fringe2Set = trapTileNumbers[0, 8];
                newTile.SpecialTile.F2AnimSet = trapTileNumbers[0, 9];

                newTile.SpecialTile.Type = (Enums.TileType)cbTrapType.SelectedIndex;

                newTile.SpecialTile.Data1 = nudTrapData1.Value;
                newTile.SpecialTile.Data2 = nudTrapData2.Value;
                newTile.SpecialTile.Data3 = nudTrapData3.Value;
                newTile.SpecialTile.String1 = txtTrapString1.Text;
                newTile.SpecialTile.String2 = txtTrapString2.Text;
                newTile.SpecialTile.String3 = txtTrapString3.Text;

                newTile.AppearanceRate = nudTrapChance.Value;

                if (lbxTraps.SelectedIndex >= 0 && lbxTraps.SelectedIndex < lbxTraps.Items.Count) {
                    trapList.Insert(lbxTraps.SelectedIndex, newTile);
                } else {
                    trapList.Add(newTile);
                }

                lbxTraps.Items.Clear();
                for (int traps = 0; traps < trapList.Count; traps++)
                {
                    lbxTraps.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (traps + 1) + ": " + trapList[traps].SpecialTile.Type + "/" + trapList[traps].SpecialTile.Data1 + "/" + trapList[traps].SpecialTile.Data2 + "/" + trapList[traps].SpecialTile.Data3));
                }
            }
        }

        void btnLoadTrap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxTraps.SelectedIndex > -1) {

                trapTileNumbers[1, 0] = trapList[lbxTraps.SelectedIndex].SpecialTile.Ground;
                trapTileNumbers[1, 1] = trapList[lbxTraps.SelectedIndex].SpecialTile.GroundAnim;
                trapTileNumbers[1, 2] = trapList[lbxTraps.SelectedIndex].SpecialTile.Mask;
                trapTileNumbers[1, 3] = trapList[lbxTraps.SelectedIndex].SpecialTile.Anim;
                trapTileNumbers[1, 4] = trapList[lbxTraps.SelectedIndex].SpecialTile.Mask2;
                trapTileNumbers[1, 5] = trapList[lbxTraps.SelectedIndex].SpecialTile.M2Anim;
                trapTileNumbers[1, 6] = trapList[lbxTraps.SelectedIndex].SpecialTile.Fringe;
                trapTileNumbers[1, 7] = trapList[lbxTraps.SelectedIndex].SpecialTile.FAnim;
                trapTileNumbers[1, 8] = trapList[lbxTraps.SelectedIndex].SpecialTile.Fringe2;
                trapTileNumbers[1, 9] = trapList[lbxTraps.SelectedIndex].SpecialTile.F2Anim;

                trapTileNumbers[0, 0] = trapList[lbxTraps.SelectedIndex].SpecialTile.GroundSet;
                trapTileNumbers[0, 1] = trapList[lbxTraps.SelectedIndex].SpecialTile.GroundAnimSet;
                trapTileNumbers[0, 2] = trapList[lbxTraps.SelectedIndex].SpecialTile.MaskSet;
                trapTileNumbers[0, 3] = trapList[lbxTraps.SelectedIndex].SpecialTile.AnimSet;
                trapTileNumbers[0, 4] = trapList[lbxTraps.SelectedIndex].SpecialTile.Mask2Set;
                trapTileNumbers[0, 5] = trapList[lbxTraps.SelectedIndex].SpecialTile.M2AnimSet;
                trapTileNumbers[0, 6] = trapList[lbxTraps.SelectedIndex].SpecialTile.FringeSet;
                trapTileNumbers[0, 7] = trapList[lbxTraps.SelectedIndex].SpecialTile.FAnimSet;
                trapTileNumbers[0, 8] = trapList[lbxTraps.SelectedIndex].SpecialTile.Fringe2Set;
                trapTileNumbers[0, 9] = trapList[lbxTraps.SelectedIndex].SpecialTile.F2AnimSet;

                for (int i = 0; i < 10; i++) {
                    picTrapTileset[i].Image = Graphics.GraphicsManager.Tiles[trapTileNumbers[0, i]][trapTileNumbers[1, i]];
                }

                cbTrapType.SelectItem((int)trapList[lbxTraps.SelectedIndex].SpecialTile.Type);

                nudTrapData1.Value = trapList[lbxTraps.SelectedIndex].SpecialTile.Data1;
                nudTrapData2.Value = trapList[lbxTraps.SelectedIndex].SpecialTile.Data2;
                nudTrapData3.Value = trapList[lbxTraps.SelectedIndex].SpecialTile.Data3;
                txtTrapString1.Text = trapList[lbxTraps.SelectedIndex].SpecialTile.String1;
                txtTrapString2.Text = trapList[lbxTraps.SelectedIndex].SpecialTile.String2;
                txtTrapString3.Text = trapList[lbxTraps.SelectedIndex].SpecialTile.String3;
                nudTrapChance.Value = trapList[lbxTraps.SelectedIndex].AppearanceRate;
            }
        }

        void btnChangeTrap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            if (lbxTraps.SelectedIndex > -1)
            {
                EditableRDungeonTrap newTile = new EditableRDungeonTrap();
                newTile.SpecialTile.Ground = trapTileNumbers[1, 0];
                newTile.SpecialTile.GroundAnim = trapTileNumbers[1, 1];
                newTile.SpecialTile.Mask = trapTileNumbers[1, 2];
                newTile.SpecialTile.Anim = trapTileNumbers[1, 3];
                newTile.SpecialTile.Mask2 = trapTileNumbers[1, 4];
                newTile.SpecialTile.M2Anim = trapTileNumbers[1, 5];
                newTile.SpecialTile.Fringe = trapTileNumbers[1, 6];
                newTile.SpecialTile.FAnim = trapTileNumbers[1, 7];
                newTile.SpecialTile.Fringe2 = trapTileNumbers[1, 8];
                newTile.SpecialTile.F2Anim = trapTileNumbers[1, 9];

                newTile.SpecialTile.GroundSet = trapTileNumbers[0, 0];
                newTile.SpecialTile.GroundAnimSet = trapTileNumbers[0, 1];
                newTile.SpecialTile.MaskSet = trapTileNumbers[0, 2];
                newTile.SpecialTile.AnimSet = trapTileNumbers[0, 3];
                newTile.SpecialTile.Mask2Set = trapTileNumbers[0, 4];
                newTile.SpecialTile.M2AnimSet = trapTileNumbers[0, 5];
                newTile.SpecialTile.FringeSet = trapTileNumbers[0, 6];
                newTile.SpecialTile.FAnimSet = trapTileNumbers[0, 7];
                newTile.SpecialTile.Fringe2Set = trapTileNumbers[0, 8];
                newTile.SpecialTile.F2AnimSet = trapTileNumbers[0, 9];

                newTile.SpecialTile.Type = (Enums.TileType)cbTrapType.SelectedIndex;
                newTile.AppearanceRate = nudTrapChance.Value;

                newTile.SpecialTile.Data1 = nudTrapData1.Value;
                newTile.SpecialTile.Data2 = nudTrapData2.Value;
                newTile.SpecialTile.Data3 = nudTrapData3.Value;
                newTile.SpecialTile.String1 = txtTrapString1.Text;
                newTile.SpecialTile.String2 = txtTrapString2.Text;
                newTile.SpecialTile.String3 = txtTrapString3.Text;

                trapList[lbxTraps.SelectedIndex] = newTile;

                lbxTraps.Items.Clear();
                for (int traps = 0; traps < trapList.Count; traps++)
                {
                    lbxTraps.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (traps + 1) + ": " + trapList[traps].SpecialTile.Type + "/" + trapList[traps].SpecialTile.Data1 + "/" + trapList[traps].SpecialTile.Data2 + "/" + trapList[traps].SpecialTile.Data3));
                }
            }
        }

        void picTrapTileset_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            LoadpnlTileSelector();

            editedTile = Array.IndexOf(picTrapTileset, sender) + 89;
            pnlRDungeonTraps.Visible = false;
            pnlTileSelector.Visible = true;

        }

        void btnRemoveTrap_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            if (lbxTraps.SelectedIndex > -1) {
                trapList.RemoveAt(lbxTraps.SelectedIndex);


                lbxTraps.Items.Clear();
                for (int traps = 0; traps < trapList.Count; traps++) {

                    lbxTraps.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (traps + 1) + ": " + trapList[traps].SpecialTile.Type + "/" + trapList[traps].SpecialTile.Data1 + "/" + trapList[traps].SpecialTile.Data2 + "/" + trapList[traps].SpecialTile.Data3));
                }
            }

        }


        #endregion
        #region Weather

        void btnAddWeather_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            lbxWeather.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (lbxWeather.Items.Count + 1) + ":" + cbWeather.SelectedIndex + ": " + Enum.GetName(typeof(Enums.Weather), cbWeather.SelectedIndex)));

        }

        void btnRemoveWeather_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {


            for (int weather = lbxWeather.SelectedIndex + 1; weather < lbxWeather.Items.Count; weather++) {
                string[] weatherindex = lbxWeather.Items[weather].TextIdentifier.Split(':');
                lbxWeather.Items[weather].TextIdentifier = weather + ":" + weatherindex[1] + ":" + weatherindex[2];
            }

            lbxWeather.Items.RemoveAt(lbxWeather.SelectedIndex);
        }

        #endregion
        #region Goal

        void optNextFloor_Checked(object sender, EventArgs e) {

            lblData1.Text = "[No Parameter]";
            lblData2.Text = "[No Parameter]";
            lblData3.Text = "[No Parameter]";

        }

        void optMap_Checked(object sender, EventArgs e) {

            lblData1.Text = "Map Number:";
            lblData2.Text = "X:";
            lblData3.Text = "Y:";

        }

        void optScripted_Checked(object sender, EventArgs e) {

            lblData1.Text = "Script Number:";
            lblData2.Text = "[No Parameter]";
            lblData3.Text = "[No Parameter]";

        }

        #endregion
        #region Chambers

        void btnAddChamber_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            EditableRDungeonChamber chamber = new EditableRDungeonChamber();
            chamber.ChamberNum = nudChamber.Value;
            chamber.String1 = txtChamberString1.Text;
            chamber.String2 = txtChamberString2.Text;
            chamber.String3 = txtChamberString3.Text;
            chamberList.Add(chamber);
            lbxChambers.Items.Add(new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "#" + chamber.ChamberNum + "/" + chamber.String1 + "/" + chamber.String2 + "/" + chamber.String3));

        }

        void btnRemoveChamber_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxChambers.SelectedIndex > -1) {
                chamberList.RemoveAt(lbxChambers.SelectedIndex);
                lbxChambers.Items.RemoveAt(lbxChambers.SelectedIndex);
            }
        }

        #endregion
        #region Misc

        void btnPlayMusic_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            string song = null;
            if (lbxMusic.SelectedIndex > 0) {
                song = ((ListBoxTextItem)lbxMusic.SelectedItems[0]).Text;
            } else if (lbxMusic.SelectedIndex == 0) {
                Music.Music.AudioPlayer.StopMusic();
            }
            if (!string.IsNullOrEmpty(song)) {
                Music.Music.AudioPlayer.PlayMusic(song, -1, true, true);
            }

        }

        #endregion

        #region Tileset

        void nudTileSet_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {

            TileSelector.ActiveTilesetSurface = Graphics.GraphicsManager.Tiles[nudTileSet.Value];
        }

        void btnTileSetOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            pnlTileSelector.Visible = false;
            if (editedTile < 22) {
                pnlRDungeonLandTiles.Visible = true;
                landTileNumbers[0, editedTile] = nudTileSet.Value;
                landTileNumbers[1, editedTile] = TileSelector.SelectedTile.Y * (TileSelector.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + TileSelector.SelectedTile.X;
                picLandTileset[editedTile].Image = TileSelector.ActiveTilesetSurface[landTileNumbers[1, editedTile]];
            } else if (editedTile < 44) {
                pnlRDungeonWaterTiles.Visible = true;
                waterTileNumbers[0, editedTile - 22] = nudTileSet.Value;
                waterTileNumbers[1, editedTile - 22] = TileSelector.SelectedTile.Y * (TileSelector.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + TileSelector.SelectedTile.X;
                picWaterTileset[editedTile - 22].Image = TileSelector.ActiveTilesetSurface[waterTileNumbers[1, editedTile - 22]];
            } else if (editedTile < 67) {
                pnlRDungeonLandAltTiles.Visible = true;
                landAltTileNumbers[0, editedTile - 44] = nudTileSet.Value;
                landAltTileNumbers[1, editedTile - 44] = TileSelector.SelectedTile.Y * (TileSelector.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + TileSelector.SelectedTile.X;
                picLandAltTileset[editedTile - 44].Image = TileSelector.ActiveTilesetSurface[landAltTileNumbers[1, editedTile - 44]];
            } else if (editedTile < 89) {
                pnlRDungeonWaterAnimTiles.Visible = true;
                waterAnimTileNumbers[0, editedTile - 67] = nudTileSet.Value;
                waterAnimTileNumbers[1, editedTile - 67] = TileSelector.SelectedTile.Y * (TileSelector.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + TileSelector.SelectedTile.X;
                picWaterAnimTileset[editedTile - 67].Image = TileSelector.ActiveTilesetSurface[waterAnimTileNumbers[1, editedTile - 67]];
            } else if (editedTile < 99) {
                pnlRDungeonTraps.Visible = true;
                trapTileNumbers[0, editedTile - 89] = nudTileSet.Value;
                trapTileNumbers[1, editedTile - 89] = TileSelector.SelectedTile.Y * (TileSelector.ActiveTilesetSurface.Size.Width / Constants.TILE_WIDTH) + TileSelector.SelectedTile.X;
                picTrapTileset[editedTile - 89].Image = TileSelector.ActiveTilesetSurface[trapTileNumbers[1, editedTile - 89]];
            }
            editedTile = -1;

        }

        void btnTileSetCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {

            pnlTileSelector.Visible = false;
            if (editedTile < 22) {
                pnlRDungeonLandTiles.Visible = true;
            } else if (editedTile < 44) {
                pnlRDungeonWaterTiles.Visible = true;
            } else if (editedTile < 67) {
                pnlRDungeonLandAltTiles.Visible = true;
            } else if (editedTile < 89) {
                pnlRDungeonWaterAnimTiles.Visible = true;
            } else if (editedTile < 99) {
                pnlRDungeonTraps.Visible = true;
            }
            editedTile = -1;



        }

        #endregion

        #endregion
    }
}
