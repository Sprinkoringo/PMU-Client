using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using PMU.Core;
using SdlDotNet.Widgets;

namespace Client.Logic.ExpKit.Modules
{
    class kitCounter : Panel, IKitModule
    {
        int moduleIndex;
        Size containerSize;
        Label lblCounter;
        Button btnIncrement;
        Button btnDecrement;
        bool enabled;

        public kitCounter(string name)
            : base(name) {
            enabled = true;

            base.BackColor = Color.Transparent;

            lblCounter = new Label("lblCounter");
            lblCounter.Location = new Point(5, 5);
            lblCounter.Font = Graphics.FontManager.LoadFont("tahoma", 16);
            lblCounter.ForeColor = Color.WhiteSmoke;
            lblCounter.Text = "0";
            lblCounter.Centered = true;

            btnIncrement = new Button("btnIncrement");
            btnIncrement.Location = new Point(5, lblCounter.Y + lblCounter.Height + 5);
            btnIncrement.Size = new System.Drawing.Size(30, 20);
            btnIncrement.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnIncrement.Text = "+";
            Skins.SkinManager.LoadButtonGui(btnIncrement);
            btnIncrement.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnIncrement_Click);

            btnDecrement = new Button("btnDecrement");
            btnDecrement.Location = new Point(btnIncrement.X + btnIncrement.Width, lblCounter.Y + lblCounter.Height + 5);
            btnDecrement.Size = new System.Drawing.Size(30, 20);
            btnDecrement.Font = Graphics.FontManager.LoadFont("tahoma", 12);
            btnDecrement.Text = "-";
            Skins.SkinManager.LoadButtonGui(btnDecrement);
            btnDecrement.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(btnDecrement_Click);

            this.AddWidget(lblCounter);
            this.AddWidget(btnIncrement);
            this.AddWidget(btnDecrement);

        }

        

        void btnDecrement_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            lblCounter.Text = (lblCounter.Text.ToInt() - 1).ToString();
        }

        void btnIncrement_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            lblCounter.Text = (lblCounter.Text.ToInt() + 1).ToString();
        }

        public void Created(int index) {
            moduleIndex = index;
        }

        public void SwitchOut() {

        }

        public void Initialize(Size containerSize) {
            this.containerSize = containerSize;
            RecalculatePositions();
            RequestRedraw();
        }

        private void RecalculatePositions() {
            lblCounter.Size = new Size(containerSize.Width - 10, 30);
            btnIncrement.Location = new Point(DrawingSupport.GetCenter(containerSize, btnIncrement.Size).X - (btnIncrement.Width / 2), lblCounter.Y + lblCounter.Height + 5);
            btnDecrement.Location = new Point(DrawingSupport.GetCenter(containerSize, btnDecrement.Size).X + (btnDecrement.Width / 2), lblCounter.Y + lblCounter.Height + 5);
        }

        public int ModuleIndex {
            get { return moduleIndex; }
        }

        public string ModuleName {
            get { return "Counter"; }
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
            get { return Enums.ExpKitModules.Counter; }
        }
    }
}
