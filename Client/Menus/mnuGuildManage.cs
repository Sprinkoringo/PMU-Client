using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

using Client.Logic.Graphics;
using PMU.Core;
using SdlDotNet.Widgets;
using Client.Logic.Network;

namespace Client.Logic.Menus {
    class mnuGuildManage : Widgets.BorderedPanel, Core.IMenu {

        public const int RECRUIT_PRICE = 1000;
        public const int PROMOTE_PRICE = 10000;
        public const int CREATE_PRICE = 100000;

        public bool Modal {
            get;
            set;
        }
        #region Fields

        List<Enums.GuildRank> ranks;

        Label lblGuild;
        Label lblYourName;
        Button btnStepDown;
        Label lblMembers;
        ListBox lbxMembers;
        Label lblPromotePrice;
        Button btnPromote;
        Button btnDemote;
        Button btnOKAdd;
        Label lblName;
        TextBox txtName;
        Button btnCancel;

        #endregion Fields

        #region Constructors

        public mnuGuildManage(string name, string[] parse)
            : base(name) {
            this.Size = new Size(380, 400);
            this.MenuDirection = Enums.MenuDirection.Vertical;
            this.Location = new Point(130, 50);

            lblGuild = new Label("lblGuild");
            lblGuild.Location = new Point(20, 0);
            lblGuild.AutoSize = true;
            lblGuild.Font = FontManager.LoadFont("PMU", 48);
            lblGuild.Text = "Manage Guild";
            lblGuild.ForeColor = Color.WhiteSmoke;

            lblYourName = new Label("lblYourName");
            lblYourName.Location = new Point(20, 56);
            lblYourName.AutoSize = true;
            lblYourName.Font = FontManager.LoadFont("PMU", 16);
            lblYourName.ForeColor = Color.WhiteSmoke;

            btnStepDown = new Button("btnStepDown");
            btnStepDown.Size = new System.Drawing.Size(64, 24);
            btnStepDown.Location = new Point(260, 56);
            btnStepDown.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadButtonGui(btnStepDown);
            btnStepDown.Click +=new EventHandler<MouseButtonEventArgs>(btnStepDown_Click);
            
            lblMembers = new Label("lblMembers");
            lblMembers.Location = new Point(20, 76);
            lblMembers.AutoSize = true;
            lblMembers.Font = FontManager.LoadFont("PMU", 16);
            lblMembers.Text = "Guild Members:";
            lblMembers.ForeColor = Color.WhiteSmoke;

            lbxMembers = new ListBox("lbxMembers");
            lbxMembers.Location = new Point(20, 96);
            lbxMembers.Size = new Size(330, 140);
            lbxMembers.BackColor = Color.Transparent;
            lbxMembers.ItemSelected +=new EventHandler(lbxMembers_ItemSelected);

            lblPromotePrice = new Label("lblPromotePrice");
            lblPromotePrice.Location = new Point(20, 240);
            lblPromotePrice.AutoSize = true;
            lblPromotePrice.Font = FontManager.LoadFont("PMU", 16);
            lblPromotePrice.ForeColor = Color.WhiteSmoke;

            btnPromote = new Button("btnPromote");
            btnPromote.Size = new System.Drawing.Size(60, 24);
            btnPromote.Location = new Point(60, 260);
            btnPromote.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnPromote.Text = "Promote";
            Skins.SkinManager.LoadButtonGui(btnPromote);
            btnPromote.Click += new EventHandler<MouseButtonEventArgs>(btnPromote_Click);

            btnDemote = new Button("btnDemote");
            btnDemote.Size = new System.Drawing.Size(60, 24);
            btnDemote.Location = new Point(240, 260);
            btnDemote.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadButtonGui(btnDemote);
            btnDemote.Click += new EventHandler<MouseButtonEventArgs>(btnDemote_Click);

            btnOKAdd = new Button("btnOK");
            btnOKAdd.Size = new System.Drawing.Size(60, 24);
            btnOKAdd.Location = new Point(280, 300);
            btnOKAdd.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnOKAdd.Text = "Add";
            Skins.SkinManager.LoadButtonGui(btnOKAdd);
            btnOKAdd.Click += new EventHandler<MouseButtonEventArgs>(btnOK_Click);

            lblName = new Label("lblName");
            lblName.Location = new Point(20, 280);
            lblName.AutoSize = true;
            lblName.Font = FontManager.LoadFont("PMU", 16);
            lblName.ForeColor = Color.WhiteSmoke;

            txtName = new TextBox("txtName");
            txtName.Size = new Size(220, 24);
            txtName.Location = new Point(20, 300);
            txtName.Font = FontManager.LoadFont("PMU", 16);
            Skins.SkinManager.LoadTextBoxGui(txtName);

            btnCancel = new Button("btnCancel");
            btnCancel.Size = new System.Drawing.Size(60, 32);
            btnCancel.Location = new Point(150, 340);
            btnCancel.Font = Client.Logic.Graphics.FontManager.LoadFont("PMU", 16);
            btnCancel.Text = "Exit";
            Skins.SkinManager.LoadButtonGui(btnCancel);
            btnCancel.Click += new EventHandler<MouseButtonEventArgs>(btnCancel_Click);

            this.AddWidget(lblGuild);
            this.AddWidget(lblYourName);
            this.AddWidget(btnStepDown);
            this.AddWidget(lblMembers);
            this.AddWidget(lbxMembers);
            this.AddWidget(lblPromotePrice);
            this.AddWidget(btnPromote);
            this.AddWidget(btnDemote);
            this.AddWidget(btnOKAdd);
            this.AddWidget(lblName);
            this.AddWidget(txtName);
            this.AddWidget(btnCancel);

            LoadGuildFromPacket(parse);
            
        }

        #endregion Constructors


        void btnStepDown_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Messenger.GuildStepDown();
            MenuSwitcher.CloseAllMenus();
        }

        void btnPromote_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Messenger.GuildPromote(lbxMembers.SelectedIndex);
            //MenuSwitcher.CloseAllMenus();
        }

        void btnDemote_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            if (ranks[lbxMembers.SelectedIndex] > Enums.GuildRank.Member) {
                Messenger.GuildDemote(lbxMembers.SelectedIndex);
            } else {
                Messenger.GuildDisown(lbxMembers.SelectedIndex);
            }
            //MenuSwitcher.CloseAllMenus();
        }

        void btnOK_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            Messenger.MakeGuildMember(txtName.Text);
            //MenuSwitcher.CloseAllMenus();
        }

        void btnCancel_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            MenuSwitcher.CloseAllMenus();
        }

        void lbxMembers_ItemSelected(object sender, EventArgs e) {
            UpdateOptions();
        }

        #region Methods

        public void LoadGuildFromPacket(string[] parse) {
            
            ranks = new List<Enums.GuildRank>();
            lbxMembers.Items.Clear();

            int count = parse[1].ToInt();

            for (int i = 0; i < count; i++) {
                Enums.GuildRank rank = (Enums.GuildRank)parse[2 + i * 3 + 1].ToInt();
                Color color;
                switch (rank) {
                    case Enums.GuildRank.Member:
                        color = Color.LightSkyBlue;
                        break;
                    case Enums.GuildRank.Admin:
                        color = Color.Yellow;
                        break;
                    case Enums.GuildRank.Founder:
                        color = Color.LawnGreen;
                        break;
                    default:
                        color = Color.Red;
                        break;
                }
                ListBoxTextItem lbiName = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "[" + rank + "] " + parse[2 + i * 3] + " (Last Login: " + parse[2 + i * 3 + 2] + ")");
                ranks.Add(rank);
                lbiName.ForeColor = color;
                lbxMembers.Items.Add(lbiName);
            }
            Refresh();
        }

        public void AddMember(string name) {
            Color color = Color.LightSkyBlue;
            ListBoxTextItem lbiName = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "[" + Enums.GuildRank.Member + "] " + name + " (NEW)");
            ranks.Add(Enums.GuildRank.Member);
            lbiName.ForeColor = color;
            lbxMembers.Items.Add(lbiName);

            Refresh();
        }

        public void RemoveMember(int index) {
            ranks.RemoveAt(index);
            lbxMembers.Items.RemoveAt(index);

            Refresh();
        }

        public void UpdateMember(int index, Enums.GuildRank rank) {
            Color color;
            switch (rank) {
                case Enums.GuildRank.Member:
                    color = Color.LightSkyBlue;
                    break;
                case Enums.GuildRank.Admin:
                    color = Color.Yellow;
                    break;
                case Enums.GuildRank.Founder:
                    color = Color.LawnGreen;
                    break;
                default:
                    color = Color.Red;
                    break;
            }
            string name = ((ListBoxTextItem)lbxMembers.Items[index]).Text.Split(']')[1];
            ListBoxTextItem lbiName = new ListBoxTextItem(Graphics.FontManager.LoadFont("tahoma", 10), "[" + rank + "] " + name);
            ranks[index] = rank;
            ((ListBoxTextItem)lbxMembers.Items[index]).ForeColor = color;
            ((ListBoxTextItem)lbxMembers.Items[index]).Text = "[" + rank + "] " + name;

            Refresh();
        }

        public void Refresh() {
            
            lblYourName.Text = Players.PlayerManager.MyPlayer.Name + " (" + Players.PlayerManager.MyPlayer.GuildAccess + ")";

            UpdateOptions();

            int memberCount = 0;
            int adminCount = 0;

            foreach (Enums.GuildRank rank in ranks) {
                if (rank > Enums.GuildRank.Member) {
                    adminCount++;
                }
                memberCount++;
            }

            lblPromotePrice.Text = "Promote member: (" + PROMOTE_PRICE * adminCount + " " + Items.ItemHelper.Items[1].Name + ")";
            lblName.Text = "Add member: (" + RECRUIT_PRICE * memberCount + " " + Items.ItemHelper.Items[1].Name + ")";
            
        }

        public void UpdateOptions() {
            if (Players.PlayerManager.MyPlayer.GuildAccess > Enums.GuildRank.Member) {
                btnStepDown.Text = "Step Down";
            } else {
                btnStepDown.Text = "Leave Guild";
            }
            if (lbxMembers.SelectedIndex > -1) {
                Enums.GuildRank rank = ranks[lbxMembers.SelectedIndex];
                if (Players.PlayerManager.MyPlayer.GuildAccess == Enums.GuildRank.Founder) {
                    if (rank == Enums.GuildRank.Founder) {
                        lblPromotePrice.Visible = false;
                        btnPromote.Visible = false;
                        btnDemote.Visible = false;
                    } else if (rank == Enums.GuildRank.Admin) {
                        lblPromotePrice.Visible = false;
                        btnPromote.Visible = false;
                        btnDemote.Visible = true;
                        btnDemote.Text = "Demote";
                    } else {
                        lblPromotePrice.Visible = true;
                        btnPromote.Visible = true;
                        btnDemote.Visible = true;
                        btnDemote.Text = "Remove";
                    }
                } else if (Players.PlayerManager.MyPlayer.GuildAccess == Enums.GuildRank.Admin) {
                    if (rank <= Enums.GuildRank.Member) {
                        btnDemote.Visible = true;
                        btnDemote.Text = "Remove";
                    } else {
                        lblPromotePrice.Visible = false;
                        btnPromote.Visible = false;
                        btnDemote.Visible = false;
                    }
                } else {
                    lblPromotePrice.Visible = false;
                    btnPromote.Visible = false;
                    btnDemote.Visible = false;
                }
            } else {
                lblPromotePrice.Visible = false;
                btnPromote.Visible = false;
                btnDemote.Visible = false;
            }

            if (Players.PlayerManager.MyPlayer.GuildAccess >= Enums.GuildRank.Admin) {
                lblName.Visible = true;
                txtName.Visible = true;
                btnOKAdd.Visible = true;
            } else {
                lblName.Visible = false;
                txtName.Visible = false;
                btnOKAdd.Visible = false;
            }
        }

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        #endregion Methods
    }
}
