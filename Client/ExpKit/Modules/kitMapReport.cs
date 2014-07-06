using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.ExpKit.Modules
{
    class kitMapReport : Panel, IKitModule
    {
        int currentTen = 0;
        int moduleIndex;
        bool mapNamesLoaded;
        bool enabled;
        string[] mapNames;
        ListBox lstMaps;
        Button btnLoadMapNames;
        VScrollBar vsbMaps;
        Label lblFindMap;
        TextBox txtFindMap;
        ListBox lstFindResults;

        public kitMapReport(string name)
            : base(name) {
            enabled = true;
            base.BackColor = Color.Transparent;

            lstMaps = new ListBox("lstMaps");
            lstMaps.Location = new Point(5, 5);
            lstMaps.MultiSelect = false;
            lstMaps.Height = 300;
            for (int i = 0; i < 10; i++) {
                ListBoxTextItem lbiMaps = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "");//(i + 1) + ": " + MapName);
                lstMaps.Items.Add(lbiMaps);
            }

            vsbMaps = new VScrollBar("vsbMaps");
            vsbMaps.ValueChanged += new EventHandler<ValueChangedEventArgs>(vsbMaps_ValueChanged);
            vsbMaps.Maximum = 0;

            btnLoadMapNames = new Button("btnLoadMapNames");
            btnLoadMapNames.Size = new Size(100, 30);
            btnLoadMapNames.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            btnLoadMapNames.Text = "Load Maps";
            Skins.SkinManager.LoadButtonGui(btnLoadMapNames);
            btnLoadMapNames.Click += new EventHandler<MouseButtonEventArgs>(btnLoadMapNames_Click);

            lblFindMap = new Label("lblFindMap");
            lblFindMap.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            lblFindMap.AutoSize = true;
            lblFindMap.Text = "Search for maps";
            lblFindMap.ForeColor = Color.WhiteSmoke;
            lblFindMap.Hide();

            txtFindMap = new TextBox("txtFindMap");
            txtFindMap.KeyDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(txtFindMap_KeyDown);
            txtFindMap.Hide();

            lstFindResults = new ListBox("lstFindResults");
            lstFindResults.MultiSelect = false;
            lstFindResults.Hide();

            this.AddWidget(lstMaps);
            this.AddWidget(vsbMaps);
            this.AddWidget(btnLoadMapNames);
            this.AddWidget(lblFindMap);
            this.AddWidget(txtFindMap);
            this.AddWidget(lstFindResults);
        }

        void txtFindMap_KeyDown(object sender, SdlDotNet.Input.KeyboardEventArgs e) {
            if (e.Key == SdlDotNet.Input.Key.Return) {
                lstFindResults.Items.Clear();
                if (!string.IsNullOrEmpty(txtFindMap.Text) && txtFindMap.Text.Length > 2) {
                    for (int i = 0; i < mapNames.Length; i++) {
                        if (mapNames[i].Contains(txtFindMap.Text)) {
                            lstFindResults.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 10), (i + 1) + ": " + mapNames[i]));
                        }
                    }
                }
            }
        }

        void btnLoadMapNames_Click(object sender, MouseButtonEventArgs e) {
            if (btnLoadMapNames.Text != "Loading...") {
                Network.Messenger.MapReportRequest();
                btnLoadMapNames.Text = "Loading...";
            }
        }

        void vsbMaps_ValueChanged(object sender, ValueChangedEventArgs e) {
            currentTen = e.NewValue;

            RefreshMapList();
        }

        public void LoadAllMapNames(string[] mapNames) {
            btnLoadMapNames.Hide();
            this.mapNames = mapNames;
            currentTen = 0;
            vsbMaps.Maximum = this.mapNames.Length / 10 - 1;
            mapNamesLoaded = true;
            RefreshMapList();

            lstFindResults.Show();
            lblFindMap.Show();
            txtFindMap.Show();
        }

        public void UpdateMapName(int slot, string newName) {
            if (mapNamesLoaded) {
                mapNames[slot - 1] = newName;
                RefreshMapList();
            }
        }

        public void RefreshMapList() {
            if (mapNamesLoaded) {
                for (int i = 0; i < 10; i++) {
                    if ((i + currentTen * 10) < mapNames.Length) {
                        ((ListBoxTextItem)lstMaps.Items[i]).Text = ((i + 1) + 10 * currentTen) + ": " + mapNames[(i) + 10 * currentTen];
                    } else {
                        ((ListBoxTextItem)lstMaps.Items[i]).Text = "---";
                    }
                }
            }
        }

        public void SwitchOut() {
        }

        public void Initialize(Size containerSize) {
            lstMaps.Size = new Size(containerSize.Width - (lstMaps.X * 2) - 14, 140);
            vsbMaps.Size = new Size(14, lstMaps.Height);
            vsbMaps.Location = new Point(lstMaps.X + lstMaps.Width, lstMaps.Y);

            btnLoadMapNames.Location = new Point(5, lstMaps.Y + lstMaps.Height + 5);

            lblFindMap.Location = new Point(5, lstMaps.Y + lstMaps.Height + 5);
            txtFindMap.Location = new Point(5, lblFindMap.Y + lblFindMap.Height + 5);
            txtFindMap.Size = new System.Drawing.Size(containerSize.Width - (txtFindMap.X * 2), 14);
            lstFindResults.Location = new Point(5, txtFindMap.Y + txtFindMap.Height + 5);
            lstFindResults.Size = new Size(containerSize.Width - (lstFindResults.X * 2), 100);
        }

        public int ModuleIndex {
            get { return moduleIndex; }
        }

        public string ModuleName {
            get { return "Map Report"; }
        }

        public void Created(int index) {
            moduleIndex = index;
        }

        public Panel ModulePanel {
            get { return this; }
        }


        public bool Enabled {
            get { return enabled; }
            set {
                enabled = value;
                if (EnabledChanged != null)
                    EnabledChanged(this, EventArgs.Empty);
            }
        }

        public event EventHandler EnabledChanged;


        public Enums.ExpKitModules ModuleID {
            get { return Enums.ExpKitModules.MapReport; }
        }
    }
}
