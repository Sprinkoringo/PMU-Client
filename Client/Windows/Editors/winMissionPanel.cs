using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using SdlDotNet.Widgets;
using Client.Logic.Network;
using PMU.Sockets;
using PMU.Core;

namespace Client.Logic.Windows.Editors {
    class winMissionPanel : Core.WindowCore {
        #region Fields

        int difficultyNum = 0;
        int currentTen = 0;
        Logic.Editors.Missions.EditableMissionPool missionPool;

        Panel pnlMissionList;
        Panel pnlMissionEditor;

        #region MissionList
        ListBox lbxMissionList;
        
        Button btnBack;
        Button btnForward;
        Button btnCancel;
        Button btnEdit;
        #endregion

        #region MissionEditor
        Button btnGeneral;
        Button btnRewards;
        Button btnClients;
        Button btnEnemies;
        Panel pnlMissionGeneral;
        Panel pnlMissionRewards;
        Panel pnlMissionClients;
        Panel pnlMissionEnemies;
        #endregion

        #region General
        Button btnEditorCancel;
        Button btnEditorOK;

        #endregion

        #region Rewards
        ListBox lbxMissionRewards;
        Label lblItemNum;
        NumericUpDown nudItemNum;
        Label lblItemAmount;
        NumericUpDown nudItemAmount;
        Label lblItemTag;
        TextBox txtItemTag;
        Button btnAddItem;
        Button btnLoadItem;
        Button btnRemoveItem;

        #endregion

        #region Clients
        ListBox lbxMissionClients;
        Label lblDexNum;
        NumericUpDown nudDexNum;
        Label lblFormNum;
        NumericUpDown nudFormNum;
        Button btnAddMissionClient;
        Button btnLoadMissionClient;
        Button btnRemoveMissionClient;

        #endregion

        #region Enemies
        ListBox lbxMissionEnemies;
        Label lblNpcNum;
        NumericUpDown nudNpcNum;
        Button btnAddEnemy;
        Button btnLoadEnemy;
        Button btnRemoveEnemy;

        #endregion


        #endregion

        #region Constructors
        public winMissionPanel()
            : base("winMissionPanel") {

            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.Size = new System.Drawing.Size(200, 230);
            this.Location = new System.Drawing.Point(210, WindowSwitcher.GameWindow.ActiveTeam.Y + WindowSwitcher.GameWindow.ActiveTeam.Height + 0);
            this.AlwaysOnTop = true;
            this.TitleBar.CloseButton.Visible = true;
            this.TitleBar.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            this.TitleBar.Text = "Mission Panel";

            #region Panels
            pnlMissionList = new Panel("pnlMissionList");
            pnlMissionList.Size = new System.Drawing.Size(200, 230);
            pnlMissionList.Location = new Point(0, 0);
            pnlMissionList.BackColor = Color.White;
            pnlMissionList.Visible = true;

            pnlMissionEditor = new Panel("pnlMissionEditor");
            pnlMissionEditor.Size = new System.Drawing.Size(320, 300);
            pnlMissionEditor.Location = new Point(0, 0);
            pnlMissionEditor.BackColor = Color.White;
            pnlMissionEditor.Visible = false;

            pnlMissionGeneral = new Panel("pnlMissionGeneral");
            pnlMissionGeneral.Size = new System.Drawing.Size(320, 270);
            pnlMissionGeneral.Location = new Point(0, 30);
            pnlMissionGeneral.BackColor = Color.White;
            pnlMissionGeneral.Visible = true;

            pnlMissionRewards = new Panel("pnlMissionRewards");
            pnlMissionRewards.Size = new System.Drawing.Size(320, 270);
            pnlMissionRewards.Location = new Point(0, 30);
            pnlMissionRewards.BackColor = Color.White;
            pnlMissionRewards.Visible = false;

            pnlMissionClients = new Panel("pnlMissionClients");
            pnlMissionClients.Size = new System.Drawing.Size(320, 270);
            pnlMissionClients.Location = new Point(0, 30);
            pnlMissionClients.BackColor = Color.White;
            pnlMissionClients.Visible = false;

            pnlMissionEnemies = new Panel("pnlMissionEnemies");
            pnlMissionEnemies.Size = new System.Drawing.Size(320, 270);
            pnlMissionEnemies.Location = new Point(0, 30);
            pnlMissionEnemies.BackColor = Color.White;
            pnlMissionEnemies.Visible = false;
            #endregion

            #region Dungeon List
            lbxMissionList = new ListBox("lbxMissionList");
            lbxMissionList.Location = new Point(10, 10);
            lbxMissionList.Size = new Size(180, 140);
            for (int i = 0; i < 10; i++) {
                ListBoxTextItem lbiMission = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), i + ": ");
                lbxMissionList.Items.Add(lbiMission);
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

            #endregion

            #region Dungeon Editor Panel

            btnGeneral = new Button("btnGeneral");
            btnGeneral.Location = new Point(5, 5);
            btnGeneral.Size = new Size(70, 20);
            btnGeneral.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnGeneral.Text = "General";
            btnGeneral.Click += new EventHandler<MouseButtonEventArgs>(btnGeneral_Click);

            btnClients = new Button("btnClients");
            btnClients.Location = new Point(80, 5);
            btnClients.Size = new Size(70, 20);
            btnClients.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnClients.Text = "Clients";
            btnClients.Click += new EventHandler<MouseButtonEventArgs>(btnClients_Click);

            btnEnemies = new Button("btnEnemies");
            btnEnemies.Location = new Point(155, 5);
            btnEnemies.Size = new Size(70, 20);
            btnEnemies.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnEnemies.Text = "Enemies";
            btnEnemies.Click += new EventHandler<MouseButtonEventArgs>(btnEnemies_Click);

            btnRewards = new Button("btnRewards");
            btnRewards.Location = new Point(230, 5);
            btnRewards.Size = new Size(70, 20);
            btnRewards.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRewards.Text = "Rewards";
            btnRewards.Click += new EventHandler<MouseButtonEventArgs>(btnRewards_Click);

            #endregion

            #region General


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

            #region Enemies

            lblNpcNum = new Label("lblNpcNum");
            lblNpcNum.AutoSize = true;
            lblNpcNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblNpcNum.Location = new Point(10, 0);
            lblNpcNum.Text = "Num:";

            nudNpcNum = new NumericUpDown("nudNpcNum");
            nudNpcNum.Size = new Size(70, 20);
            nudNpcNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudNpcNum.Maximum = Int32.MaxValue;
            nudNpcNum.Minimum = 1;
            nudNpcNum.Location = new Point(10, 14);

            btnAddEnemy = new Button("btnAddEnemy");
            btnAddEnemy.Size = new Size(70, 16);
            btnAddEnemy.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddEnemy.Location = new Point(5, 72);
            btnAddEnemy.Text = "Add";
            btnAddEnemy.Click += new EventHandler<MouseButtonEventArgs>(btnAddEnemy_Click);

            btnRemoveEnemy = new Button("btnRemoveEnemy");
            btnRemoveEnemy.Size = new Size(70, 16);
            btnRemoveEnemy.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveEnemy.Location = new Point(80, 72);
            btnRemoveEnemy.Text = "Remove";
            btnRemoveEnemy.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveEnemy_Click);

            btnLoadEnemy = new Button("btnLoadEnemy");
            btnLoadEnemy.Size = new Size(70, 16);
            btnLoadEnemy.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadEnemy.Location = new Point(155, 72);
            btnLoadEnemy.Text = "Load";
            //btnLoadEnemy.Click += new EventHandler<MouseButtonEventArgs>(btnLoadEnemy_Click);

            lbxMissionEnemies = new ListBox("lbxMissionEnemies");
            lbxMissionEnemies.Location = new Point(10, 90);
            lbxMissionEnemies.Size = new Size(pnlMissionClients.Size.Width - 20, pnlMissionClients.Size.Height - 120);
            lbxMissionEnemies.MultiSelect = false;

            #endregion

            #region Rewards

            lblItemNum = new Label("lblItemNum");
            lblItemNum.AutoSize = true;
            lblItemNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblItemNum.Location = new Point(10, 0);
            lblItemNum.Text = "Num:";

            nudItemNum = new NumericUpDown("nudItemNum");
            nudItemNum.Size = new Size(170, 20);
            nudItemNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudItemNum.Maximum = 2000;
            nudItemNum.Minimum = 1;
            nudItemNum.Location = new Point(10, 14);
            nudItemNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudItemNum_ValueChanged);

            lblItemAmount = new Label("lblItemAmount");
            lblItemAmount.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblItemAmount.Text = "Amount:";
            lblItemAmount.AutoSize = true;
            lblItemAmount.Location = new Point(200, 0);

            nudItemAmount = new NumericUpDown("nudItemAmount");
            nudItemAmount.Size = new Size(70, 20);
            nudItemAmount.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudItemAmount.Location = new Point(200, 14);
            nudItemAmount.Maximum = Int32.MaxValue;

            lblItemTag = new Label("lblItemTag");
            lblItemTag.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblItemTag.Text = "Tag:";
            lblItemTag.AutoSize = true;
            lblItemTag.Location = new Point(10, 34);

            txtItemTag = new TextBox("txtItemTag");
            txtItemTag.Size = new Size(200, 20);
            txtItemTag.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            txtItemTag.Location = new Point(10, 48);

            btnAddItem = new Button("btnAddItem");
            btnAddItem.Size = new Size(70, 16);
            btnAddItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddItem.Location = new Point(5, 72);
            btnAddItem.Text = "Add";
            btnAddItem.Click += new EventHandler<MouseButtonEventArgs>(btnAddItem_Click);

            btnRemoveItem = new Button("btnRemoveItem");
            btnRemoveItem.Size = new Size(70, 16);
            btnRemoveItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveItem.Location = new Point(80, 72);
            btnRemoveItem.Text = "Remove";
            btnRemoveItem.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveItem_Click);

            btnLoadItem = new Button("btnLoadItem");
            btnLoadItem.Size = new Size(70, 16);
            btnLoadItem.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadItem.Location = new Point(155, 72);
            btnLoadItem.Text = "Load";
            btnLoadItem.Click += new EventHandler<MouseButtonEventArgs>(btnLoadItem_Click);

            lbxMissionRewards = new ListBox("lbxMissionRewards");
            lbxMissionRewards.Location = new Point(10, 90);
            lbxMissionRewards.Size = new Size(pnlMissionClients.Size.Width - 20, pnlMissionClients.Size.Height - 120);
            lbxMissionRewards.MultiSelect = false;

            #endregion

            #region Clients

            lblDexNum = new Label("lblDexNum");
            lblDexNum.AutoSize = true;
            lblDexNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblDexNum.Location = new Point(2, 0);
            lblDexNum.Text = "Dex #";

            nudDexNum = new NumericUpDown("nudDexNum");
            nudDexNum.Size = new Size(70, 20);
            nudDexNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudDexNum.Maximum = 649;
            nudDexNum.Minimum = 1;
            nudDexNum.Location = new Point(10, 14);
            nudDexNum.ValueChanged += new EventHandler<ValueChangedEventArgs>(nudDexNum_ValueChanged);

            lblFormNum = new Label("lblFormNum");
            lblFormNum.AutoSize = true;
            lblFormNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            lblFormNum.Location = new Point(240, 0);
            lblFormNum.Text = "Form #";

            nudFormNum = new NumericUpDown("nudFormNum");
            nudFormNum.Size = new Size(70, 20);
            nudFormNum.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            nudFormNum.Maximum = Int32.MaxValue;
            nudFormNum.Minimum = 0;
            nudFormNum.Location = new Point(240, 14);

            btnAddMissionClient = new Button("btnAddMissionClient");
            btnAddMissionClient.Size = new Size(70, 16);
            btnAddMissionClient.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnAddMissionClient.Location = new Point(5, 72);
            btnAddMissionClient.Text = "Add";
            btnAddMissionClient.Click += new EventHandler<MouseButtonEventArgs>(btnAddMissionClient_Click);

            btnRemoveMissionClient = new Button("btnRemoveMissionClient");
            btnRemoveMissionClient.Size = new Size(70, 16);
            btnRemoveMissionClient.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnRemoveMissionClient.Location = new Point(80, 72);
            btnRemoveMissionClient.Text = "Remove";
            btnRemoveMissionClient.Click += new EventHandler<MouseButtonEventArgs>(btnRemoveMissionClient_Click);

            btnLoadMissionClient = new Button("btnLoadMissionClient");
            btnLoadMissionClient.Size = new Size(70, 16);
            btnLoadMissionClient.Font = Graphics.FontManager.LoadFont("tahoma", 10);
            btnLoadMissionClient.Location = new Point(155, 72);
            btnLoadMissionClient.Text = "Load";
            btnLoadMissionClient.Click += new EventHandler<MouseButtonEventArgs>(btnLoadMissionClient_Click);

            lbxMissionClients = new ListBox("lbxMissionClients");
            lbxMissionClients.Location = new Point(10, 90);
            lbxMissionClients.Size = new Size(pnlMissionEnemies.Size.Width - 20, pnlMissionEnemies.Size.Height - 120);
            lbxMissionClients.MultiSelect = false;

            #endregion

            #region Addwidget
            //Dungeon List
            pnlMissionList.AddWidget(lbxMissionList);
            pnlMissionList.AddWidget(btnBack);
            pnlMissionList.AddWidget(btnForward);
            pnlMissionList.AddWidget(btnEdit);
            pnlMissionList.AddWidget(btnCancel);
            //General
            pnlMissionGeneral.AddWidget(btnEditorCancel);
            pnlMissionGeneral.AddWidget(btnEditorOK);
            //Clients
            pnlMissionClients.AddWidget(lblDexNum);
            pnlMissionClients.AddWidget(nudDexNum);
            pnlMissionClients.AddWidget(lblFormNum);
            pnlMissionClients.AddWidget(nudFormNum);
            pnlMissionClients.AddWidget(btnAddMissionClient);
            pnlMissionClients.AddWidget(btnRemoveMissionClient);
            pnlMissionClients.AddWidget(btnLoadMissionClient);
            pnlMissionClients.AddWidget(lbxMissionClients);
            //Enemies
            pnlMissionEnemies.AddWidget(lblNpcNum);
            pnlMissionEnemies.AddWidget(nudNpcNum);
            pnlMissionEnemies.AddWidget(btnAddEnemy);
            pnlMissionEnemies.AddWidget(btnRemoveEnemy);
            //pnlMissionEnemies.AddWidget(btnLoadEnemy);
            pnlMissionEnemies.AddWidget(lbxMissionEnemies);
            //Rewards
            pnlMissionRewards.AddWidget(lblItemNum);
            pnlMissionRewards.AddWidget(nudItemNum);
            pnlMissionRewards.AddWidget(lblItemAmount);
            pnlMissionRewards.AddWidget(nudItemAmount);
            pnlMissionRewards.AddWidget(lblItemTag);
            pnlMissionRewards.AddWidget(txtItemTag);
            pnlMissionRewards.AddWidget(btnAddItem);
            pnlMissionRewards.AddWidget(btnRemoveItem);
            pnlMissionRewards.AddWidget(btnLoadItem);
            pnlMissionRewards.AddWidget(lbxMissionRewards);
            //Editor panel
            pnlMissionEditor.AddWidget(btnGeneral);
            pnlMissionEditor.AddWidget(btnClients);
            pnlMissionEditor.AddWidget(btnEnemies);
            pnlMissionEditor.AddWidget(btnRewards);
            pnlMissionEditor.AddWidget(pnlMissionGeneral);
            pnlMissionEditor.AddWidget(pnlMissionClients);
            pnlMissionEditor.AddWidget(pnlMissionEnemies);
            pnlMissionEditor.AddWidget(pnlMissionRewards);
            //This
            this.AddWidget(pnlMissionList);
            this.AddWidget(pnlMissionEditor);
            #endregion

            RefreshMissionList();
        }

        #endregion


        

        #region Methods

        #region Dungeon List
        void btnBack_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen > 0) {
                currentTen--;
            }
            RefreshMissionList();
        }

        void btnForward_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (currentTen < (15 / 10)) {
                currentTen++;
            }
            RefreshMissionList();
        }

        void btnEdit_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (lbxMissionList.SelectedItems.Count == 1) {
                string[] index = ((ListBoxTextItem)lbxMissionList.SelectedItems[0]).Text.Split(':');
                if (index[0].IsNumeric()) {
                    difficultyNum = index[0].ToInt();
                    btnEdit.Text = "Loading...";

                    Messenger.SendEditMission(difficultyNum);
                    //LoadMission(new string[10] { "0", "0", "1", "492", "1", "1", "500", "0", "0", "0" });
                }
            }
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            this.Close();
            return;
        }

        public void RefreshMissionList() {
            for (int i = 0; i < 10; i++) {
                if ((i + currentTen * 10) < 15) {
                    ((ListBoxTextItem)lbxMissionList.Items[i]).Text = (((i + 1) + 10 * currentTen) + ": " + ((Enums.JobDifficulty)((i+1) + 10 * currentTen)).ToString());
                } else {
                    ((ListBoxTextItem)lbxMissionList.Items[i]).Text = "---";
                }
            }
        }

        #endregion

        

        #region Editor
        
        public void LoadMission(string[] parse) {
            this.Size = pnlMissionEditor.Size;
            pnlMissionList.Visible = false;
            pnlMissionEditor.Visible = true;


            
            btnGeneral_Click(null, null);
            lbxMissionRewards.Items.Clear();
            lbxMissionEnemies.Items.Clear();
            lbxMissionClients.Items.Clear();
            //this.Size = new System.Drawing.Size(pnlDungeonGeneral.Width, pnlDungeonGeneral.Height);


            missionPool = new Logic.Editors.Missions.EditableMissionPool();

            
            int clientCount = parse[2].ToInt();
            int n = 3;
            for (int i = 0; i < clientCount; i++) {
                Logic.Editors.Missions.EditableMissionClient missionClient = new Logic.Editors.Missions.EditableMissionClient();
                missionClient.DexNum = parse[n].ToInt();
                missionClient.FormNum = parse[n+1].ToInt();
                missionPool.Clients.Add(missionClient);

                n += 2;

                ListBoxTextItem lbiClient = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": #" + missionClient.DexNum + " " + Pokedex.PokemonHelper.Pokemon[missionClient.DexNum-1].Name + " (Form: " + missionClient.FormNum + ")");
                lbxMissionClients.Items.Add(lbiClient);

            }

            int enemyCount = parse[n].ToInt();
            n++;

            for (int i = 0; i < enemyCount; i++) {
                
                missionPool.Enemies.Add(parse[n].ToInt());

                ListBoxTextItem lbiEnemy = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": NPC #" + missionPool.Enemies[i] + ", " + Npc.NpcHelper.Npcs[missionPool.Enemies[i]].Name);
                lbxMissionEnemies.Items.Add(lbiEnemy);

                n++;
            }

            int rewardCount = parse[n].ToInt();
            n++;

            for (int i = 0; i < rewardCount; i++) {

                Logic.Editors.Missions.EditableMissionReward missionReward = new Logic.Editors.Missions.EditableMissionReward();
                missionReward.ItemNum = parse[n].ToInt();
                missionReward.ItemAmount = parse[n + 1].ToInt();
                missionReward.ItemTag = parse[n + 2];
                missionPool.Rewards.Add(missionReward);

                n += 3;

                ListBoxTextItem lbiReward = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + Items.ItemHelper.Items[missionReward.ItemNum].Name + " x" + missionReward.ItemAmount + " (Tag: " + missionReward.ItemTag + ")");
                lbxMissionRewards.Items.Add(lbiReward);
            }

            btnEdit.Text = "Edit";
        }


        void btnGeneral_Click(object sender, MouseButtonEventArgs e) {
            if (!btnGeneral.Selected) {
                btnGeneral.Selected = true;
                btnClients.Selected = false;
                btnEnemies.Selected = false;
                btnRewards.Selected = false;
                pnlMissionGeneral.Visible = true;
                pnlMissionClients.Visible = false;
                pnlMissionEnemies.Visible = false;
                pnlMissionRewards.Visible = false;
                this.TitleBar.Text = "Mission Editor - General";
            }
        }

        void btnClients_Click(object sender, MouseButtonEventArgs e) {
            if (!btnClients.Selected) {
                btnGeneral.Selected = false;
                btnClients.Selected = true;
                btnEnemies.Selected = false;
                btnRewards.Selected = false;
                pnlMissionGeneral.Visible = false;
                pnlMissionClients.Visible = true;
                pnlMissionEnemies.Visible = false;
                pnlMissionRewards.Visible = false;
                this.TitleBar.Text = "Mission Editor - Clients";
            }
        }

        void btnEnemies_Click(object sender, MouseButtonEventArgs e) {
            if (!btnEnemies.Selected) {
                btnGeneral.Selected = false;
                btnClients.Selected = false;
                btnEnemies.Selected = true;
                btnRewards.Selected = false;
                pnlMissionGeneral.Visible = false;
                pnlMissionClients.Visible = false;
                pnlMissionEnemies.Visible = true;
                pnlMissionRewards.Visible = false;
                this.TitleBar.Text = "Mission Editor - Enemies";
            }
        }

        void btnRewards_Click(object sender, MouseButtonEventArgs e) {
            if (!btnRewards.Selected) {
                btnGeneral.Selected = false;
                btnClients.Selected = false;
                btnEnemies.Selected = false;
                btnRewards.Selected = true;
                pnlMissionGeneral.Visible = false;
                pnlMissionClients.Visible = false;
                pnlMissionEnemies.Visible = false;
                pnlMissionRewards.Visible = true;
                this.TitleBar.Text = "Mission Editor - Rewards";
            }
        }

        #endregion

        
        
        #region General

        void btnEditorCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            difficultyNum = -1;
            pnlMissionEditor.Visible = false;
            pnlMissionList.Visible = true;
            this.Size = new System.Drawing.Size(pnlMissionList.Width, pnlMissionList.Height);
            this.TitleBar.Text = "Mission Panel";
        }


        void btnEditorOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {


            Messenger.SendSaveMission(difficultyNum, missionPool);

            difficultyNum = -1;
            pnlMissionEditor.Visible = false;
            pnlMissionList.Visible = true;
            this.Size = new System.Drawing.Size(pnlMissionList.Width, pnlMissionList.Height);

        }

        #endregion
        
        #region Enemy

        void btnRemoveEnemy_Click(object sender, MouseButtonEventArgs e) {

            if (lbxMissionEnemies.SelectedIndex > -1) {
                missionPool.Enemies.RemoveAt(lbxMissionEnemies.SelectedIndex);
                lbxMissionEnemies.Items.Clear();
                for (int enemies = 0; enemies < missionPool.Enemies.Count; enemies++) {
                    ListBoxTextItem lbiEnemy = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (enemies + 1) + ": NPC #" + missionPool.Enemies[enemies] + ", " + Npc.NpcHelper.Npcs[missionPool.Enemies[enemies]].Name);
                    lbxMissionEnemies.Items.Add(lbiEnemy);
                }
            }
        }

        void btnAddEnemy_Click(object sender, MouseButtonEventArgs e) {
            
            missionPool.Enemies.Add(nudNpcNum.Value);

            ListBoxTextItem lbiEnemy = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), missionPool.Enemies.Count + ": NPC #" + missionPool.Enemies[missionPool.Enemies.Count - 1] + ", " + Npc.NpcHelper.Npcs[missionPool.Enemies[missionPool.Enemies.Count - 1]].Name);
            lbxMissionEnemies.Items.Add(lbiEnemy);
        }

        //void btnLoadEnemy_Click(object sender, MouseButtonEventArgs e) {
        //    if (lbxMissionEnemies.SelectedIndex > -1) {
        //        nudNpcNum.Value = dungeon.ScriptList.KeyByIndex(lbxMissionEnemies.SelectedIndex);
        //        txtScriptParam.Text = dungeon.ScriptList.ValueByIndex(lbxMissionEnemies.SelectedIndex);
        //    }
        //}

        #endregion
        
        #region Rewards

        void nudItemNum_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            lblItemNum.Text = Items.ItemHelper.Items[nudItemNum.Value].Name;
        }
        
        void btnRemoveItem_Click(object sender, MouseButtonEventArgs e) {

            if (lbxMissionRewards.SelectedIndex > -1) {
                missionPool.Rewards.RemoveAt(lbxMissionRewards.SelectedIndex);
                lbxMissionRewards.Items.Clear();
                for (int rewards = 0; rewards < missionPool.Rewards.Count; rewards++) {
                    ListBoxTextItem lbiReward = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (rewards + 1) + ": " + Items.ItemHelper.Items[missionPool.Rewards[rewards].ItemNum].Name + " x" + missionPool.Rewards[rewards].ItemAmount + " (Tag: " + missionPool.Rewards[rewards].ItemTag + ")");
                    lbxMissionRewards.Items.Add(lbiReward);
                }
            }
        }

        void btnAddItem_Click(object sender, MouseButtonEventArgs e) {
            
            Logic.Editors.Missions.EditableMissionReward reward = new Logic.Editors.Missions.EditableMissionReward();
            reward.ItemNum = nudItemNum.Value;
            reward.ItemAmount = nudItemAmount.Value;
            reward.ItemTag = txtItemTag.Text;

            missionPool.Rewards.Add(reward);

            ListBoxTextItem lbiReward = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), missionPool.Rewards.Count + ": " + Items.ItemHelper.Items[reward.ItemNum].Name + " x" + reward.ItemAmount + " (Tag: " + reward.ItemTag + ")");
            lbxMissionRewards.Items.Add(lbiReward);
        }

        void btnLoadItem_Click(object sender, MouseButtonEventArgs e) {
            if (lbxMissionRewards.SelectedIndex > -1) {
                nudItemNum.Value = missionPool.Rewards[lbxMissionRewards.SelectedIndex].ItemNum;
                nudItemAmount.Value = missionPool.Rewards[lbxMissionRewards.SelectedIndex].ItemAmount;
                txtItemTag.Text = missionPool.Rewards[lbxMissionRewards.SelectedIndex].ItemTag;
            }
        }

        #endregion
        
        #region Clients
        
        
        void nudDexNum_ValueChanged(object sender, SdlDotNet.Widgets.ValueChangedEventArgs e) {
            lblDexNum.Text = Pokedex.PokemonHelper.Pokemon[nudDexNum.Value-1].Name;
        }
        
        void btnRemoveMissionClient_Click(object sender, MouseButtonEventArgs e) {

            if (lbxMissionClients.SelectedIndex > -1) {
                missionPool.Clients.RemoveAt(lbxMissionClients.SelectedIndex);
                lbxMissionClients.Items.Clear();
                for (int clients = 0; clients < missionPool.Clients.Count; clients++) {
                    
                    ListBoxTextItem lbiClient = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (clients + 1) + ": #" + missionPool.Clients[clients].DexNum + " " + Pokedex.PokemonHelper.Pokemon[missionPool.Clients[clients].DexNum-1].Name + " (Form: " + missionPool.Clients[clients].FormNum + ")");
                    lbxMissionClients.Items.Add(lbiClient);
                }
            }
        }

        void btnAddMissionClient_Click(object sender, MouseButtonEventArgs e) {


            Logic.Editors.Missions.EditableMissionClient client = new Logic.Editors.Missions.EditableMissionClient();
            client.DexNum = nudDexNum.Value;
            client.FormNum = nudFormNum.Value;

            missionPool.Clients.Add(client);

            ListBoxTextItem lbiClient = new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), missionPool.Clients.Count + ": #" + client.DexNum + " " + Pokedex.PokemonHelper.Pokemon[client.DexNum - 1].Name + " (Form: " + client.FormNum + ")");
            lbxMissionClients.Items.Add(lbiClient);

        }

        void btnLoadMissionClient_Click(object sender, MouseButtonEventArgs e) {
            if (lbxMissionClients.SelectedIndex > -1) {
                nudDexNum.Value = missionPool.Clients[lbxMissionClients.SelectedIndex].DexNum;
                nudFormNum.Value = missionPool.Clients[lbxMissionClients.SelectedIndex].FormNum;
            }
        }

        #endregion
        
        #endregion

        
    }
}
