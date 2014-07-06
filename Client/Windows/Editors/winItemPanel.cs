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
    class winItemPanel : Core.WindowCore
    {
        #region Fields

        int itemNum = -1;
        int currentTen = 0;

        Panel pnlItemList;
        Panel pnlItemEditor;

        ListBox lbxItemList;
        ListBoxTextItem lbiItem;
        //Button btnAddNew; (Can implement later...)
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;

        Button btnEditorCancel;
        Button btnEditorOK;

        Label lblName;
        TextBox txtName;
        Label lblDescription;
        TextBox txtDescription;
        Label lblSprite;
        Widgets.ItemsetViewer pic;

        Label lblType;
        RadioButton optTypeNone;
        RadioButton optTypeHeld;
        RadioButton optTypeHeldByParty;
        RadioButton optTypeHeldInBag;
        RadioButton optTypePotionAddHP;
        RadioButton optTypePotionAddPP;
        RadioButton optTypePotionAddBelly;
        RadioButton optTypePotionSubHP;
        RadioButton optTypePotionSubPP;
        RadioButton optTypePotionSubBelly;
        RadioButton optTypeKey;
        RadioButton optTypeCurrency;
        RadioButton optTypeTM;
        RadioButton optTypeScripted;

        Label lblData1;
        NumericUpDown nudData1;
        Label lblData2;
        NumericUpDown nudData2;
        Label lblData3;
        NumericUpDown nudData3;

        Label lblRarity;
        NumericUpDown nudRarity;

        Label lblAtkReq;
        NumericUpDown nudAtkReq;
        Label lblDefReq;
        NumericUpDown nudDefReq;
        Label lblSpAtkReq;
        NumericUpDown nudSpAtkReq;
        Label lblSpDefReq;
        NumericUpDown nudSpDefReq;
        Label lblSpeedReq;
        NumericUpDown nudSpeedReq;
        Label lblScriptedReq;
        NumericUpDown nudScriptedReq;

        //No need for durability

        Label lblAddHP;
        NumericUpDown nudAddHP;
        Label lblAddPP;
        NumericUpDown nudAddPP;
        Label lblAddAtk;
        NumericUpDown nudAddAtk;
        Label lblAddDef;
        NumericUpDown nudAddDef;
        Label lblAddSpAtk;
        NumericUpDown nudAddSpAtk;
        Label lblAddSpDef;
        NumericUpDown nudAddSpDef;
        Label lblAddSpeed;
        NumericUpDown nudAddSpeed;
        Label lblAddEXP;
        NumericUpDown nudAddEXP;
        Label lblAttackSpeed;
        NumericUpDown nudAttackSpeed;
        Label lblSellPrice;
        NumericUpDown nudSellPrice;
        Label lblStackCap;
        NumericUpDown nudStackCap;
        //Label lblBound;
        CheckBox chkBound;
        //Label lblLoseable;
        CheckBox chkLoseable;
        Label lblRecruitBonus;
        NumericUpDown nudRecruitBonus;

        #endregion Fields

        #region Constructors

        public winItemPanel()
            : base("winItemPanel") {



            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Item Panel";

            pnlItemList = new Panel("pnlItemList");
            pnlItemList.Size = new System.Drawing.Size(200, 230);
            pnlItemList.Location = new Point(0, 0);
            pnlItemList.BackColor = Color.White;
            pnlItemList.Visible = true;

            pnlItemEditor = new Panel("pnlItemEditor");
            pnlItemEditor.Size = new System.Drawing.Size(580, 380);
            pnlItemEditor.Location = new Point(0, 0);
            pnlItemEditor.BackColor = Color.White;
            pnlItemEditor.Visible = false;


            lbxItemList = new ListBox("lbxItemList");
            lbxItemList.Location = new Point(10, 10);
            lbxItemList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                lbiItem = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Items.ItemHelper.Items[(i + 1) + 10 * currentTen].Name);
                lbxItemList.Items.Add(lbiItem);
            }
            lbxItemList.SelectItem(0);

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
            btnEditorCancel.Location = new Point(100, 334);
            btnEditorCancel.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorCancel.Size = new System.Drawing.Size(64, 16);
            btnEditorCancel.Visible = true;
            btnEditorCancel.Text = "Cancel";
            btnEditorCancel.Click += new EventHandler<MouseButtonEventArgs>(btnEditorCancel_Click);

            btnEditorOK = new Button("btnEditorOK");
            btnEditorOK.Location = new Point(10, 334);
            btnEditorOK.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEditorOK.Size = new System.Drawing.Size(64, 16);
            btnEditorOK.Visible = true;
            btnEditorOK.Text = "OK";
            btnEditorOK.Click += new EventHandler<MouseButtonEventArgs>(btnEditorOK_Click);

            lblName = new Label("lblName");
            lblName.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblName.Text = "Item Name:";
            lblName.AutoSize = true;
            lblName.Location = new Point(10, 4);

            txtName = new TextBox("txtName");
            txtName.Size = new Size(200, 16);
            txtName.Location = new Point(10, 16);

            lblSprite = new Label("lblSprite");
            lblSprite.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSprite.Text = "Item Sprite:";
            lblSprite.AutoSize = true;
            lblSprite.Location = new Point(10, 36);

            pic = new Widgets.ItemsetViewer("pic");
            pic.Location = new Point(10, 48);
            pic.Size = new Size(204, 144);
            pic.ActiveItemSurface = Graphics.GraphicsManager.Items;

            lblSellPrice = new Label("lblSellPrice");
            lblSellPrice.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSellPrice.Text = "Sell Price:";
            lblSellPrice.AutoSize = true;
            lblSellPrice.Location = new Point(10, 200);

            nudSellPrice = new NumericUpDown("nudSellPrice");
            nudSellPrice.Size = new Size(200, 16);
            nudSellPrice.Location = new Point(10, 212);
            nudSellPrice.Minimum = 0;
            nudSellPrice.Maximum = Int32.MaxValue;

            nudStackCap = new NumericUpDown("nudStackCap");
            nudStackCap.Location = new Point(10, 232);
            nudStackCap.Size = new System.Drawing.Size(95, 17);
            nudStackCap.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudStackCap.Minimum = 0;
            nudStackCap.Maximum = Int32.MaxValue;

            lblStackCap = new Label("lblStackCap");
            lblStackCap.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblStackCap.Text = "Stack Cap";
            lblStackCap.AutoSize = true;
            lblStackCap.Location = new Point(110, 232);

            chkBound = new CheckBox("chkBound");
            chkBound.Location = new Point(10, 252);
            chkBound.Size = new System.Drawing.Size(95, 17);
            chkBound.BackColor = Color.Transparent;
            chkBound.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkBound.Text = "Bound";

            chkLoseable = new CheckBox("chkLoseable");
            chkLoseable.Location = new Point(10, 272);
            chkLoseable.Size = new System.Drawing.Size(95, 17);
            chkLoseable.BackColor = Color.Transparent;
            chkLoseable.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            chkLoseable.Text = "Loseable";

            lblDescription = new Label("lblDescription");
            lblDescription.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblDescription.Text = "Description:";
            lblDescription.AutoSize = true;
            lblDescription.Location = new Point(10, 292);

            txtDescription = new TextBox("txtDescription");
            txtDescription.Size = new Size(300, 16);
            txtDescription.Location = new Point(10, 304);


            lblType = new Label("lblType");
            lblType.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblType.Text = "Item Type:";
            lblType.AutoSize = true;
            lblType.Location = new Point(220, 4);

            optTypeNone = new RadioButton("optTypeNone");
            optTypeNone.BackColor = Color.Transparent;
            optTypeNone.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeNone.Location = new Point(220, 24);
            optTypeNone.Size = new System.Drawing.Size(95, 17);
            optTypeNone.Text = "None";
            optTypeNone.Checked = true;

            optTypeHeld = new RadioButton("optTypeHeld");
            optTypeHeld.BackColor = Color.Transparent;
            optTypeHeld.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeHeld.Location = new Point(220, 44);
            optTypeHeld.Size = new System.Drawing.Size(95, 17);
            optTypeHeld.Text = "Held Item";

            optTypeHeldByParty = new RadioButton("optTypeHeldByParty");
            optTypeHeldByParty.BackColor = Color.Transparent;
            optTypeHeldByParty.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeHeldByParty.Location = new Point(220, 64);
            optTypeHeldByParty.Size = new System.Drawing.Size(95, 17);
            optTypeHeldByParty.Text = "Party Item";

            optTypeHeldInBag = new RadioButton("optTypeHeldInBag");
            optTypeHeldInBag.BackColor = Color.Transparent;
            optTypeHeldInBag.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeHeldInBag.Location = new Point(220, 84);
            optTypeHeldInBag.Size = new System.Drawing.Size(95, 17);
            optTypeHeldInBag.Text = "Bag Item";

            optTypePotionAddHP = new RadioButton("optTypePotionAddHP");
            optTypePotionAddHP.BackColor = Color.Transparent;
            optTypePotionAddHP.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionAddHP.Location = new Point(220, 104);
            optTypePotionAddHP.Size = new System.Drawing.Size(95, 17);
            optTypePotionAddHP.Text = "HP Heal";

            optTypePotionAddPP = new RadioButton("optTypePotionAddPP");
            optTypePotionAddPP.BackColor = Color.Transparent;
            optTypePotionAddPP.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionAddPP.Location = new Point(220, 124);
            optTypePotionAddPP.Size = new System.Drawing.Size(95, 17);
            optTypePotionAddPP.Text = "PP Heal";

            optTypePotionAddBelly = new RadioButton("optTypePotionAddBelly");
            optTypePotionAddBelly.BackColor = Color.Transparent;
            optTypePotionAddBelly.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionAddBelly.Location = new Point(220, 144);
            optTypePotionAddBelly.Size = new System.Drawing.Size(95, 17);
            optTypePotionAddBelly.Text = "Belly Heal";

            optTypePotionSubHP = new RadioButton("optTypePotionSubHP");
            optTypePotionSubHP.BackColor = Color.Transparent;
            optTypePotionSubHP.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionSubHP.Location = new Point(220, 164);
            optTypePotionSubHP.Size = new System.Drawing.Size(95, 17);
            optTypePotionSubHP.Text = "HP Loss";

            optTypePotionSubPP = new RadioButton("optTypePotionSubPP");
            optTypePotionSubPP.BackColor = Color.Transparent;
            optTypePotionSubPP.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionSubPP.Location = new Point(220, 184);
            optTypePotionSubPP.Size = new System.Drawing.Size(95, 17);
            optTypePotionSubPP.Text = "PP Loss";

            optTypePotionSubBelly = new RadioButton("optTypePotionSubBelly");
            optTypePotionSubBelly.BackColor = Color.Transparent;
            optTypePotionSubBelly.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypePotionSubBelly.Location = new Point(220, 204);
            optTypePotionSubBelly.Size = new System.Drawing.Size(95, 17);
            optTypePotionSubBelly.Text = "Belly Loss";

            optTypeKey = new RadioButton("optTypeKey");
            optTypeKey.BackColor = Color.Transparent;
            optTypeKey.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeKey.Location = new Point(220, 224);
            optTypeKey.Size = new System.Drawing.Size(95, 17);
            optTypeKey.Text = "Key";

            optTypeCurrency = new RadioButton("optTypeCurrency");
            optTypeCurrency.BackColor = Color.Transparent;
            optTypeCurrency.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeCurrency.Location = new Point(220, 244);
            optTypeCurrency.Size = new System.Drawing.Size(95, 17);
            optTypeCurrency.Text = "Currency";

            optTypeTM = new RadioButton("optTypeTM");
            optTypeTM.BackColor = Color.Transparent;
            optTypeTM.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeTM.Location = new Point(220, 264);
            optTypeTM.Size = new System.Drawing.Size(95, 17);
            optTypeTM.Text = "TM";

            optTypeScripted = new RadioButton("optTypeScripted");
            optTypeScripted.BackColor = Color.Transparent;
            optTypeScripted.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            optTypeScripted.Location = new Point(220, 284);
            optTypeScripted.Size = new System.Drawing.Size(95, 17);
            optTypeScripted.Text = "Scripted";


            lblData1 = new Label("lblData1");
            lblData1.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData1.Text = "Data1:";
            lblData1.AutoSize = true;
            lblData1.Location = new Point(340, 4);


            nudData1 = new NumericUpDown("nudData1");
            nudData1.Size = new Size(100, 16);
            nudData1.Location = new Point(340, 16);
            nudData1.Minimum = Int32.MinValue;
            nudData1.Maximum = Int32.MaxValue;

            lblData2 = new Label("lblData2");
            lblData2.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData2.Text = "Data2:";
            lblData2.AutoSize = true;
            lblData2.Location = new Point(340, 36);

            nudData2 = new NumericUpDown("nudData2");
            nudData2.Size = new Size(100, 16);
            nudData2.Location = new Point(340, 48);
            nudData2.Minimum = Int32.MinValue;
            nudData2.Maximum = Int32.MaxValue;

            lblData3 = new Label("lblData3");
            lblData3.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblData3.Text = "Data3:";
            lblData3.AutoSize = true;
            lblData3.Location = new Point(340, 68);

            nudData3 = new NumericUpDown("nudData3");
            nudData3.Size = new Size(100, 16);
            nudData3.Location = new Point(340, 80);
            nudData3.Minimum = Int32.MinValue;
            nudData3.Maximum = Int32.MaxValue;

            lblRarity = new Label("lblRarity");
            lblRarity.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblRarity.Text = "Rarity: (1-10)";
            lblRarity.AutoSize = true;
            lblRarity.Location = new Point(340, 100);

            nudRarity = new NumericUpDown("nudRarity");
            nudRarity.Size = new Size(100, 16);
            nudRarity.Location = new Point(340, 112);
            nudRarity.Minimum = 1;
            nudRarity.Maximum = 10;

            lblAtkReq = new Label("lblAtkReq");
            lblAtkReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAtkReq.Text = "Req Data 1:";
            lblAtkReq.AutoSize = true;
            lblAtkReq.Location = new Point(340, 132);

            nudAtkReq = new NumericUpDown("nudAtkReq");
            nudAtkReq.Size = new Size(100, 16);
            nudAtkReq.Location = new Point(340, 144);
            nudAtkReq.Minimum = Int32.MinValue;
            nudAtkReq.Maximum = Int32.MaxValue;

            lblDefReq = new Label("lblDefReq");
            lblDefReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblDefReq.Text = "Req Data 2:";
            lblDefReq.AutoSize = true;
            lblDefReq.Location = new Point(340, 164);

            nudDefReq = new NumericUpDown("nudDefReq");
            nudDefReq.Size = new Size(100, 16);
            nudDefReq.Location = new Point(340, 176);
            nudDefReq.Minimum = Int32.MinValue;
            nudDefReq.Maximum = Int32.MaxValue;

            lblSpAtkReq = new Label("lblSpAtkReq");
            lblSpAtkReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSpAtkReq.Text = "Req Data 3:";
            lblSpAtkReq.AutoSize = true;
            lblSpAtkReq.Location = new Point(340, 196);

            nudSpAtkReq = new NumericUpDown("nudSpAtkReq");
            nudSpAtkReq.Size = new Size(100, 16);
            nudSpAtkReq.Location = new Point(340, 208);
            nudSpAtkReq.Minimum = Int32.MinValue;
            nudSpAtkReq.Maximum = Int32.MaxValue;

            lblSpDefReq = new Label("lblSpDefReq");
            lblSpDefReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSpDefReq.Text = "Req Data 4:";
            lblSpDefReq.AutoSize = true;
            lblSpDefReq.Location = new Point(340, 228);

            nudSpDefReq = new NumericUpDown("nudSpDefReq");
            nudSpDefReq.Size = new Size(100, 16);
            nudSpDefReq.Location = new Point(340, 240);
            nudSpDefReq.Minimum = Int32.MinValue;
            nudSpDefReq.Maximum = Int32.MaxValue;

            lblSpeedReq = new Label("lblSpeedReq");
            lblSpeedReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblSpeedReq.Text = "Req Data 5:";
            lblSpeedReq.AutoSize = true;
            lblSpeedReq.Location = new Point(340, 260);

            nudSpeedReq = new NumericUpDown("nudSpeedReq");
            nudSpeedReq.Size = new Size(100, 16);
            nudSpeedReq.Location = new Point(340, 272);
            nudSpeedReq.Minimum = Int32.MinValue;
            nudSpeedReq.Maximum = Int32.MaxValue;

            lblScriptedReq = new Label("lblScriptedReq");
            lblScriptedReq.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblScriptedReq.Text = "Req Script: >= -1";
            lblScriptedReq.AutoSize = true;
            lblScriptedReq.Location = new Point(340, 292);

            nudScriptedReq = new NumericUpDown("nudScriptedReq");
            nudScriptedReq.Size = new Size(100, 16);
            nudScriptedReq.Location = new Point(340, 304);
            nudScriptedReq.Minimum = -1;
            nudScriptedReq.Maximum = Int32.MaxValue;

            lblAddHP = new Label("lblAddHP");
            lblAddHP.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddHP.Text = "Add HP:";
            lblAddHP.AutoSize = true;
            lblAddHP.Location = new Point(460, 4);

            nudAddHP = new NumericUpDown("nudAddHP");
            nudAddHP.Size = new Size(100, 16);
            nudAddHP.Location = new Point(460, 16);
            nudAddHP.Minimum = Int32.MinValue;
            nudAddHP.Maximum = Int32.MaxValue;

            lblAddPP = new Label("lblAddPP");
            lblAddPP.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddPP.Text = "Add PP:";
            lblAddPP.AutoSize = true;
            lblAddPP.Location = new Point(460, 36);

            nudAddPP = new NumericUpDown("nudAddPP");
            nudAddPP.Size = new Size(100, 16);
            nudAddPP.Location = new Point(460, 48);
            nudAddPP.Minimum = Int32.MinValue;
            nudAddPP.Maximum = Int32.MaxValue;

            lblAddEXP = new Label("lblAddEXP");
            lblAddEXP.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddEXP.Text = "Add EXP: (+-100)";
            lblAddEXP.AutoSize = true;
            lblAddEXP.Location = new Point(460, 68);

            nudAddEXP = new NumericUpDown("nudAddEXP");
            nudAddEXP.Size = new Size(100, 16);
            nudAddEXP.Location = new Point(460, 80);
            nudAddEXP.Minimum = -100;
            nudAddEXP.Maximum = 100;

            lblAddAtk = new Label("lblAddAtk");
            lblAddAtk.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddAtk.Text = "Add Atk:";
            lblAddAtk.AutoSize = true;
            lblAddAtk.Location = new Point(460, 100);

            nudAddAtk = new NumericUpDown("nudAddAtk");
            nudAddAtk.Size = new Size(100, 16);
            nudAddAtk.Location = new Point(460, 112);
            nudAddAtk.Minimum = Int32.MinValue;
            nudAddAtk.Maximum = Int32.MaxValue;

            lblAddDef = new Label("lblAddDef");
            lblAddDef.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddDef.Text = "Add Def:";
            lblAddDef.AutoSize = true;
            lblAddDef.Location = new Point(460, 132);

            nudAddDef = new NumericUpDown("nudAddDef");
            nudAddDef.Size = new Size(100, 16);
            nudAddDef.Location = new Point(460, 144);
            nudAddDef.Minimum = Int32.MinValue;
            nudAddDef.Maximum = Int32.MaxValue;

            lblAddSpAtk = new Label("lblAddSpAtk");
            lblAddSpAtk.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddSpAtk.Text = "Add SpAtk:";
            lblAddSpAtk.AutoSize = true;
            lblAddSpAtk.Location = new Point(460, 164);

            nudAddSpAtk = new NumericUpDown("nudAddSpAtk");
            nudAddSpAtk.Size = new Size(100, 16);
            nudAddSpAtk.Location = new Point(460, 176);
            nudAddSpAtk.Minimum = Int32.MinValue;
            nudAddSpAtk.Maximum = Int32.MaxValue;

            lblAddSpDef = new Label("lblAddSpDef");
            lblAddSpDef.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddSpDef.Text = "Add SpDef:";
            lblAddSpDef.AutoSize = true;
            lblAddSpDef.Location = new Point(460, 196);

            nudAddSpDef = new NumericUpDown("nudAddSpDef");
            nudAddSpDef.Size = new Size(100, 16);
            nudAddSpDef.Location = new Point(460, 208);
            nudAddSpDef.Minimum = Int32.MinValue;
            nudAddSpDef.Maximum = Int32.MaxValue;

            lblAddSpeed = new Label("lblAddSpeed");
            lblAddSpeed.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAddSpeed.Text = "Add Speed:";
            lblAddSpeed.AutoSize = true;
            lblAddSpeed.Location = new Point(460, 228);

            nudAddSpeed = new NumericUpDown("nudAddSpeed");
            nudAddSpeed.Size = new Size(100, 16);
            nudAddSpeed.Location = new Point(460, 240);
            nudAddSpeed.Minimum = Int32.MinValue;
            nudAddSpeed.Maximum = Int32.MaxValue;

            lblAttackSpeed = new Label("lblAttackSpeed");
            lblAttackSpeed.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblAttackSpeed.Text = "Hit Rate: (1-10000)";//confirm this
            lblAttackSpeed.AutoSize = true;
            lblAttackSpeed.Location = new Point(460, 260);

            nudAttackSpeed = new NumericUpDown("nudAttackSpeed");
            nudAttackSpeed.Size = new Size(100, 16);
            nudAttackSpeed.Location = new Point(460, 272);
            nudAttackSpeed.Minimum = 1;
            nudAttackSpeed.Maximum = 10000;

            lblRecruitBonus = new Label("lblRecruitBonus");
            lblRecruitBonus.Font = Graphics.FontManager.LoadFont("Tahoma", 10);
            lblRecruitBonus.Text = "Add Recruit:";
            lblRecruitBonus.AutoSize = true;
            lblRecruitBonus.Location = new Point(460, 292);

            nudRecruitBonus = new NumericUpDown("nudRecruitBonus");
            nudRecruitBonus.Size = new Size(100, 16);
            nudRecruitBonus.Location = new Point(460, 304);
            nudRecruitBonus.Minimum = Int32.MinValue;
            nudRecruitBonus.Maximum = Int32.MaxValue;


            //lbxItems = new SdlDotNet.Widgets.ListBox("lbxItems");
            //lbxItems.Location = new Point(20, 20);

            //set font properties, items, etc.


            //this.AddWidget(lbxItems);
            pnlItemList.AddWidget(lbxItemList);
            pnlItemList.AddWidget(btnBack);
            pnlItemList.AddWidget(btnForward);
            //pnlItemList.AddWidget(btnAddNew);
            pnlItemList.AddWidget(btnEdit);
            pnlItemList.AddWidget(btnCancel);

            pnlItemEditor.AddWidget(lblName);
            pnlItemEditor.AddWidget(txtName);
            pnlItemEditor.AddWidget(lblSprite);
            pnlItemEditor.AddWidget(pic);
            pnlItemEditor.AddWidget(lblSellPrice);
            pnlItemEditor.AddWidget(nudSellPrice);
            pnlItemEditor.AddWidget(nudStackCap);
            pnlItemEditor.AddWidget(lblStackCap);
            pnlItemEditor.AddWidget(chkBound);
            pnlItemEditor.AddWidget(chkLoseable);
            pnlItemEditor.AddWidget(lblDescription);
            pnlItemEditor.AddWidget(txtDescription);

            pnlItemEditor.AddWidget(lblType);
            pnlItemEditor.AddWidget(optTypeNone);
            pnlItemEditor.AddWidget(optTypeHeld);
            pnlItemEditor.AddWidget(optTypeHeldByParty);
            pnlItemEditor.AddWidget(optTypeHeldInBag);
            pnlItemEditor.AddWidget(optTypePotionAddHP);
            pnlItemEditor.AddWidget(optTypePotionAddPP);
            pnlItemEditor.AddWidget(optTypePotionAddBelly);
            pnlItemEditor.AddWidget(optTypePotionSubHP);
            pnlItemEditor.AddWidget(optTypePotionSubPP);
            pnlItemEditor.AddWidget(optTypePotionSubBelly);
            pnlItemEditor.AddWidget(optTypeKey);
            pnlItemEditor.AddWidget(optTypeCurrency);
            pnlItemEditor.AddWidget(optTypeTM);
            pnlItemEditor.AddWidget(optTypeScripted);

            pnlItemEditor.AddWidget(lblData1);
            pnlItemEditor.AddWidget(nudData1);
            pnlItemEditor.AddWidget(lblData2);
            pnlItemEditor.AddWidget(nudData2);
            pnlItemEditor.AddWidget(lblData3);
            pnlItemEditor.AddWidget(nudData3);
            pnlItemEditor.AddWidget(lblRarity);
            pnlItemEditor.AddWidget(nudRarity);
            pnlItemEditor.AddWidget(lblAtkReq);
            pnlItemEditor.AddWidget(nudAtkReq);
            pnlItemEditor.AddWidget(lblDefReq);
            pnlItemEditor.AddWidget(nudDefReq);
            pnlItemEditor.AddWidget(lblSpAtkReq);
            pnlItemEditor.AddWidget(nudSpAtkReq);
            pnlItemEditor.AddWidget(lblSpDefReq);
            pnlItemEditor.AddWidget(nudSpDefReq);
            pnlItemEditor.AddWidget(lblSpeedReq);
            pnlItemEditor.AddWidget(nudSpeedReq);
            pnlItemEditor.AddWidget(lblScriptedReq);
            pnlItemEditor.AddWidget(nudScriptedReq);

            pnlItemEditor.AddWidget(lblAddHP);
            pnlItemEditor.AddWidget(nudAddHP);
            pnlItemEditor.AddWidget(lblAddPP);
            pnlItemEditor.AddWidget(nudAddPP);
            pnlItemEditor.AddWidget(lblAddAtk);
            pnlItemEditor.AddWidget(nudAddAtk);
            pnlItemEditor.AddWidget(lblAddDef);
            pnlItemEditor.AddWidget(nudAddDef);
            pnlItemEditor.AddWidget(lblAddSpAtk);
            pnlItemEditor.AddWidget(nudAddSpAtk);
            pnlItemEditor.AddWidget(lblAddSpDef);
            pnlItemEditor.AddWidget(nudAddSpDef);
            pnlItemEditor.AddWidget(lblAddSpeed);
            pnlItemEditor.AddWidget(nudAddSpeed);
            pnlItemEditor.AddWidget(lblAddEXP);
            pnlItemEditor.AddWidget(nudAddEXP);
            pnlItemEditor.AddWidget(lblAttackSpeed);
            pnlItemEditor.AddWidget(nudAttackSpeed);
            pnlItemEditor.AddWidget(lblRecruitBonus);
            pnlItemEditor.AddWidget(nudRecruitBonus);

            pnlItemEditor.AddWidget(btnEditorCancel);
            pnlItemEditor.AddWidget(btnEditorOK);


            this.AddWidget(pnlItemList);
            this.AddWidget(pnlItemEditor);

            this.LoadComplete();

        }

        #endregion Constructors

        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshItemList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (MaxInfo.MaxItems / 10)) {
                currentTen++;
            }
            RefreshItemList();
        }

        public void RefreshItemList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < MaxInfo.MaxItems) {
                    ((ListBoxTextItem)lbxItemList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + Items.ItemHelper.Items[(i + 1) + 10 * currentTen].Name);
                } else {
                    ((ListBoxTextItem)lbxItemList.Items[i]).Text = "---";
                }
            }
        }


        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxItemList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxItemList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    LoadItem(index[0].ToInt());
                }
            }
        }

        public void LoadItem(int index) {
            itemNum = index;
            Items.Item item = Items.ItemHelper.Items[itemNum];
            pnlItemList.Visible = false;
            pnlItemEditor.Visible = true;
            this.Size = new System.Drawing.Size(pnlItemEditor.Width, pnlItemEditor.Height);

            txtName.Text = item.Name;
            pic.SelectedTile = new Point(item.Pic % (pic.Width / Constants.TILE_WIDTH), item.Pic / (pic.Width / Constants.TILE_WIDTH));
            nudSellPrice.Value = item.Price;
            nudStackCap.Value = item.StackCap;
            chkBound.Checked = item.Bound;
            chkLoseable.Checked = item.Loseable;
            txtDescription.Text = item.Desc;
            nudRarity.Value = item.Rarity;
            switch (item.Type) {
                case Enums.ItemType.None: {
                        optTypeNone.Checked = true;
                    }
                    break;
                case Enums.ItemType.Held: {
                        optTypeHeld.Checked = true;
                    }
                    break;
                case Enums.ItemType.HeldByParty: {
                        optTypeHeldByParty.Checked = true;
                    }
                    break;
                case Enums.ItemType.HeldInBag: {
                        optTypeHeldInBag.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionAddHP: {
                        optTypePotionAddHP.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionAddPP: {
                        optTypePotionAddPP.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionAddBelly: {
                        optTypePotionAddBelly.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionSubHP: {
                        optTypePotionSubHP.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionSubPP: {
                        optTypePotionSubPP.Checked = true;
                    }
                    break;
                case Enums.ItemType.PotionSubBelly: {
                        optTypePotionSubBelly.Checked = true;
                    }
                    break;
                case Enums.ItemType.Key: {
                        optTypeKey.Checked = true;
                    }
                    break;
                case Enums.ItemType.Currency: {
                        optTypeCurrency.Checked = true;
                    }
                    break;
                case Enums.ItemType.TM: {
                        optTypeTM.Checked = true;
                    }
                    break;
                case Enums.ItemType.Scripted: {
                        optTypeScripted.Checked = true;
                    }
                    break;
                default: {
                        optTypeNone.Checked = true;
                    }
                    break;
            }

            nudData1.Value = item.Data1;
            nudData2.Value = item.Data2;
            nudData3.Value = item.Data3;

            nudAtkReq.Value = item.AttackReq;
            nudDefReq.Value = item.DefenseReq;
            nudSpAtkReq.Value = item.SpAtkReq;
            nudSpDefReq.Value = item.SpDefReq;
            nudSpeedReq.Value = item.SpeedReq;
            nudScriptedReq.Value = item.ScriptedReq;


            nudAddHP.Value = item.AddHP;
            nudAddPP.Value = item.AddPP;
            nudAddEXP.Value = item.AddEXP;
            nudAddAtk.Value = item.AddAttack;
            nudAddDef.Value = item.AddDefense;
            nudAddSpAtk.Value = item.AddSpAtk;
            nudAddSpDef.Value = item.AddSpDef;
            nudAddSpeed.Value = item.AddSpeed;
            nudAttackSpeed.Value = item.AttackSpeed;
            nudRecruitBonus.Value = item.RecruitBonus;
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            itemNum = -1;
            pnlItemEditor.Visible = false;
            pnlItemList.Visible = true;
            this.Size = new System.Drawing.Size(pnlItemList.Width, pnlItemList.Height);

        }

        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Items.Item itemToSend = new Items.Item();

            itemToSend.Name = txtName.Text;
            itemToSend.Pic = pic.DetermineTileNumber(pic.SelectedTile.X, pic.SelectedTile.Y);
            itemToSend.Price = nudSellPrice.Value;
            itemToSend.StackCap = nudStackCap.Value;
            itemToSend.Bound = chkBound.Checked;
            itemToSend.Loseable = chkLoseable.Checked;
            itemToSend.Desc = txtDescription.Text;

            itemToSend.Type = Enums.ItemType.None;
            if (optTypeNone.Checked == true) {
                itemToSend.Type = Enums.ItemType.None;
            }
            if (optTypeHeld.Checked == true) {
                itemToSend.Type = Enums.ItemType.Held;
            }
            if (optTypeHeldByParty.Checked == true) {
                itemToSend.Type = Enums.ItemType.HeldByParty;
            }
            if (optTypeHeldInBag.Checked == true) {
                itemToSend.Type = Enums.ItemType.HeldInBag;
            }
            if (optTypePotionAddHP.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionAddHP;
            }
            if (optTypePotionAddPP.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionAddPP;
            }
            if (optTypePotionAddBelly.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionAddBelly;
            }
            if (optTypePotionSubHP.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionSubHP;
            }
            if (optTypePotionSubPP.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionSubPP;
            }
            if (optTypePotionSubBelly.Checked == true) {
                itemToSend.Type = Enums.ItemType.PotionSubBelly;
            }
            if (optTypeKey.Checked == true) {
                itemToSend.Type = Enums.ItemType.Key;
            }
            if (optTypeCurrency.Checked == true) {
                itemToSend.Type = Enums.ItemType.Currency;
            }
            if (optTypeTM.Checked == true) {
                itemToSend.Type = Enums.ItemType.TM;
            }
            if (optTypeScripted.Checked == true) {
                itemToSend.Type = Enums.ItemType.Scripted;
            }


            itemToSend.Data1 = nudData1.Value;


            itemToSend.Data2 = nudData2.Value;
            itemToSend.Data3 = nudData3.Value;
            itemToSend.Rarity = nudRarity.Value;
            itemToSend.AttackReq = nudAtkReq.Value;
            itemToSend.DefenseReq = nudDefReq.Value;
            itemToSend.SpAtkReq = nudSpAtkReq.Value;
            itemToSend.SpDefReq = nudSpDefReq.Value;
            itemToSend.SpeedReq = nudSpeedReq.Value;
            itemToSend.ScriptedReq = nudScriptedReq.Value;
            itemToSend.AddHP = nudAddHP.Value;
            itemToSend.AddPP = nudAddPP.Value;
            itemToSend.AddEXP = nudAddEXP.Value;
            itemToSend.AddAttack = nudAddAtk.Value;
            itemToSend.AddDefense = nudAddDef.Value;
            itemToSend.AddSpAtk = nudAddSpAtk.Value;
            itemToSend.AddSpDef = nudAddSpDef.Value;
            itemToSend.AddSpeed = nudAddSpeed.Value;
            itemToSend.AttackSpeed = nudAttackSpeed.Value;
            itemToSend.RecruitBonus = nudRecruitBonus.Value;





            Messenger.SendSaveItem(itemNum, itemToSend);
            pnlItemEditor.Visible = false;
            pnlItemList.Visible = true;
            this.Size = new System.Drawing.Size(pnlItemList.Width, pnlItemList.Height);

        }






    }
}
