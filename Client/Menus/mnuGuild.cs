using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus
{
    class mnuGuild : Widgets.BorderedPanel, Core.IMenu
    {

        public bool Modal {
            get;
            set;
        }
        #region Fields

        const int MAX_ITEMS = 2;

        Widgets.MenuItemPicker itemPicker;
        Label lblGuild;
        Label lblName;
        TextBox txtName;
        Label lblMakeTrainee;
        Label lblMakeMember;
        Label lblDisown;

        #endregion Fields

        #region Constructors

        public mnuGuild(string name)
            : base(name) {
            this.Size = new Size(280, 206);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(18, 111);

            lblGuild = new Label("lblGuild");
            lblGuild.Location = new Point(20, 0);
            lblGuild.AutoSize = true;
            lblGuild.Font = FontManager.LoadFont("PMU", 48);
            lblGuild.Text = "Guild";
            lblGuild.ForeColor = Color.WhiteSmoke;

            lblName = new Label("lblName");
            lblName.AutoSize = true;
            lblName.Location = new Point(30, 48);
            lblName.Font = FontManager.LoadFont("PMU", 32);
            lblName.Text = "Name";
            lblName.ForeColor = Color.WhiteSmoke;
            //lblName.HoverColor = Color.Red;

            txtName = new TextBox("txtName");
            txtName.Size = new Size(120, 24);
            txtName.Location = new Point(32, 80);
            txtName.Font = FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadTextBoxGui(txtName);

            lblMakeTrainee = new Label("lblMakeTrainee");
            lblMakeTrainee.AutoSize = true;
            lblMakeTrainee.Location = new Point(30, 96);
            lblMakeTrainee.Font = FontManager.LoadFont("PMU", 32);
            lblMakeTrainee.Text = "Make Trainee";
            lblMakeTrainee.HoverColor = Color.Red;
            lblMakeTrainee.ForeColor = Color.WhiteSmoke;
            lblMakeTrainee.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblMakeTrainee_Click);

            lblMakeMember = new Label("lblMakeMember");
            lblMakeMember.AutoSize = true;
            lblMakeMember.Location = new Point(30, 126);
            lblMakeMember.Font = FontManager.LoadFont("PMU", 32);
            lblMakeMember.Text = "Make Member";
            lblMakeMember.HoverColor = Color.Red;
            lblMakeMember.ForeColor = Color.WhiteSmoke;
            lblMakeMember.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblMakeMember_Click);

            lblDisown = new Label("lblDisown");
            lblDisown.AutoSize = true;
            lblDisown.Location = new Point(30, 156);
            lblDisown.Font = FontManager.LoadFont("PMU", 32);
            lblDisown.Text = "Disown";
            lblDisown.HoverColor = Color.Red;
            lblDisown.ForeColor = Color.WhiteSmoke;
            lblDisown.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblDisown_Click);

            this.AddWidget(itemPicker);
            this.AddWidget(lblGuild);
            this.AddWidget(lblName);
            this.AddWidget(txtName);
            this.AddWidget(lblMakeTrainee);
            this.AddWidget(lblMakeMember);
            this.AddWidget(lblDisown);


        }

        #endregion Constructors

        void lblMakeTrainee_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(0);
        }

        void lblMakeMember_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(1);
        }

        void lblDisown_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(2);
        }

        #region Methods

        public void ChangeSelected(int itemNum) {
            itemPicker.Location = new Point(18, 111 + (30 * itemNum));
            itemPicker.SelectedItem = itemNum;
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.DownArrow: {
                        if (itemPicker.SelectedItem == MAX_ITEMS) {
                            ChangeSelected(0);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem + 1);
                        }
            				Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.UpArrow: {
                        if (itemPicker.SelectedItem == 0) {
                            ChangeSelected(MAX_ITEMS);
                        } else {
                            ChangeSelected(itemPicker.SelectedItem - 1);
                        }
                    	Music.Music.AudioPlayer.PlaySoundEffect("beep1.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem);
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace:
                    {
                    	//if (txtName.
                        MenuSwitcher.ShowMainMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            //switch (itemNum) {
            //    case 0: {
            //            Messenger.MakeTrainee(txtName.Text);
            //        }
            //        break;
            //    case 1: {
            //            Messenger.MakeMember(txtName.Text);
            //        }
            //        break;
            //    case 2: {
            //            Messenger.Disown(txtName.Text);
            //        }
            //        break;
                
            //}
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
