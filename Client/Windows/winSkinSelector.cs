using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using Client.Logic.Network;
using Client.Logic.Skins;
using SdlDotNet.Widgets;
using Gfx = Client.Logic.Graphics;
using System.Threading;
using PMU.Compression.Zip;
using System.IO;
using System.Xml;

namespace Client.Logic.Windows
{
    class winSkinSelector : Core.WindowCore
    {
        PictureBox picSkinPreview;
        ComboBox cmbSkinSelect;
        Button btnSave;
        Button btnFindSkin;
        Button btnCancel;

        Skin loadedSkin;

        public winSkinSelector()
            : base("winSkinSelector") {

            this.Windowed = true;
            this.Size = new Size(390, 300);
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);
            this.TitleBar.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.TitleBar.BackgroundImage = SkinManager.LoadGuiElement("Skin Selector", "titlebar.png");
            this.TitleBar.CloseButton.Visible = false;
            this.Text = "Skin Selector";
            this.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.BackgroundImage = SkinManager.LoadGui("Skin Selector");

            picSkinPreview = new PictureBox("picSkinPreview");
            picSkinPreview.SizeMode = ImageSizeMode.StretchImage;
            picSkinPreview.Size = new System.Drawing.Size(242, 182);
            picSkinPreview.Location = new Point(DrawingSupport.GetCenter(this.Width, picSkinPreview.Width), 10);
            picSkinPreview.BackColor = Color.Green;
            picSkinPreview.BorderStyle = SdlDotNet.Widgets.BorderStyle.FixedSingle;
            picSkinPreview.BorderColor = Color.Black;
            picSkinPreview.BorderWidth = 1;

            cmbSkinSelect = new ComboBox("cmbSkinSelect");
            cmbSkinSelect.Location = new Point(10, 200);
            cmbSkinSelect.Size = new Size(this.Width - 20, 30);
            cmbSkinSelect.ItemSelected += new EventHandler(cmbSkinSelect_ItemSelected);

            btnSave = new Button("btnSave");
            btnSave.Size = new Size(100, 30);
            btnSave.Location = new Point(10, 240);
            btnSave.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            btnSave.Text = "Switch Skins!";
            btnSave.Click += new EventHandler<MouseButtonEventArgs>(btnSave_Click);

            btnFindSkin = new Button("btnFindSkin");
            btnFindSkin.Size = new System.Drawing.Size(100, 30);
            btnFindSkin.Location = new Point(110, 240);
            btnFindSkin.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            btnFindSkin.Text = "Find Skin";
            btnFindSkin.Click += new EventHandler<MouseButtonEventArgs>(btnFindSkin_Click);

            btnCancel = new Button("btnCancel");
            btnCancel.Size = new Size(100, 30);
            btnCancel.Location = new Point(210, 240);
            btnCancel.Font = Logic.Graphics.FontManager.LoadFont("tahoma", 12);
            btnCancel.Text = "Cancel";
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            this.AddWidget(picSkinPreview);
            this.AddWidget(cmbSkinSelect);
            this.AddWidget(btnSave);
            this.AddWidget(btnFindSkin);
            this.AddWidget(btnCancel);

            PopulateSkinList();
        }

        void btnFindSkin_Click(object sender, MouseButtonEventArgs e) {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Skin Package|*.pmuskn";
            if (ofd.ShowDialog() == SdlDotNet.Widgets.DialogResult.OK) {
                bool installed = SkinManager.InstallSkin(ofd.FileName);
                if (installed) {
                    PopulateSkinList();
                } else {
                    MessageBox.Show("The selected file is not a valid skin package.", "Invalid Package");
                }
            }
        }

        

        void cmbSkinSelect_ItemSelected(object sender, EventArgs e) {
            if (loadedSkin != null) {
                loadedSkin.Unload();
            }
            loadedSkin = new Skin();
            if (cmbSkinSelect.SelectedItem != null) {
                loadedSkin.LoadSkin(cmbSkinSelect.SelectedItem.TextIdentifier);
                picSkinPreview.Image = Logic.Graphics.SurfaceManager.LoadSurface(IO.Paths.SkinPath + loadedSkin.Name + "/Configuration/preview.png");
            }
        }

        void btnSave_Click(object sender, MouseButtonEventArgs e) {
            if (cmbSkinSelect.SelectedItem != null) {
                SkinManager.ChangeActiveSkin(cmbSkinSelect.SelectedItem.TextIdentifier);
                SkinManager.PlaySkinMusic();
            }
            WindowSwitcher.ShowMainMenu();
            this.Close();
        }

        void btnCancel_Click(object sender, MouseButtonEventArgs e) {
            WindowSwitcher.ShowMainMenu();
            this.Close();
        }

        void PopulateSkinList() {
            Thread thread = new Thread(new ThreadStart(PopulateSkinListBackgroud));
            thread.IsBackground = true;
            thread.Start();
        }

        void PopulateSkinListBackgroud() {
            cmbSkinSelect.Items.Clear();
            string[] directories = System.IO.Directory.GetDirectories(IO.Paths.SkinPath);
            foreach (string file in directories) {//System.IO.Directory.EnumerateDirectories(IO.Paths.SkinPath)) {
                string name = System.IO.Path.GetFileNameWithoutExtension(file);
                if (!string.IsNullOrEmpty(name)) {
                    cmbSkinSelect.Items.Add(new ListBoxTextItem(Logic.Graphics.FontManager.LoadFont("tahoma", 12), name));
                }
            }
            cmbSkinSelect.SelectItem(SkinManager.ActiveSkin.Name);
        }
    }
}
