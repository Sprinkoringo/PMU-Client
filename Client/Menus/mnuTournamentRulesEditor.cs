using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Client.Logic.Graphics;
using SdlDotNet.Widgets;
using Client.Logic.Tournaments;

namespace Client.Logic.Menus
{
    class mnuTournamentRulesEditor : Widgets.BorderedPanel, Core.IMenu
    {
        public bool Modal {
            get;
            set;
        }

        Label lblEditRules;
        TournamentRules rules;

        CheckBox chkSleepClause;
        CheckBox chkAccuracyClause;
        CheckBox chkSpeciesClause;
        CheckBox chkFreezeClause;
        CheckBox chkOHKOClause;
        CheckBox chkSelfKOClause;

        Button btnSave;

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public mnuTournamentRulesEditor(string name, TournamentRules rules)
            : base(name) {

            this.rules = rules;

            base.Size = new Size(315, 360);
            base.MenuDirection = Enums.MenuDirection.Vertical;
            base.Location = new Point(10, 40);

            lblEditRules = new Label("lblEditRules");
            lblEditRules.AutoSize = true;
            lblEditRules.Font = FontManager.LoadFont("PMU", 48);
            lblEditRules.Text = "Edit Rules";
            lblEditRules.ForeColor = Color.WhiteSmoke;
            lblEditRules.Location = new Point(20, 0);

            chkSleepClause = new CheckBox("chkSleepClause");
            chkSleepClause.Location = new Point(15, 48);
            chkSleepClause.Size = new System.Drawing.Size(200, 32);
            chkSleepClause.BackColor = Color.Transparent;
            chkSleepClause.Font = FontManager.LoadFont("PMU", 32);
            chkSleepClause.ForeColor = Color.WhiteSmoke;
            chkSleepClause.Text = "Sleep Clause";
            chkSleepClause.Checked = rules.SleepClause;

            chkAccuracyClause = new CheckBox("chkAccuracyClause");
            chkAccuracyClause.Location = new Point(15, 80);
            chkAccuracyClause.Size = new System.Drawing.Size(200, 32);
            chkAccuracyClause.BackColor = Color.Transparent;
            chkAccuracyClause.Font = FontManager.LoadFont("PMU", 32);
            chkAccuracyClause.ForeColor = Color.WhiteSmoke;
            chkAccuracyClause.Text = "Accuracy Clause";
            chkAccuracyClause.Checked = rules.AccuracyClause;

            chkSpeciesClause = new CheckBox("chkSpeciesClause");
            chkSpeciesClause.Location = new Point(15, 112);
            chkSpeciesClause.Size = new System.Drawing.Size(200, 32);
            chkSpeciesClause.BackColor = Color.Transparent;
            chkSpeciesClause.Font = FontManager.LoadFont("PMU", 32);
            chkSleepClause.ForeColor = Color.WhiteSmoke;
            chkSpeciesClause.Text = "Species Clause";
            chkSpeciesClause.Checked = rules.SpeciesClause;

            chkFreezeClause = new CheckBox("chkFreezeClause");
            chkFreezeClause.Location = new Point(15, 144);
            chkFreezeClause.Size = new System.Drawing.Size(200, 32);
            chkFreezeClause.BackColor = Color.Transparent;
            chkFreezeClause.Font = FontManager.LoadFont("PMU", 32);
            chkFreezeClause.ForeColor = Color.WhiteSmoke;
            chkFreezeClause.Text = "Freeze Clause";
            chkFreezeClause.Checked = rules.FreezeClause;

            chkOHKOClause = new CheckBox("chkOHKOClause");
            chkOHKOClause.Location = new Point(15, 176);
            chkOHKOClause.Size = new System.Drawing.Size(200, 32);
            chkOHKOClause.BackColor = Color.Transparent;
            chkOHKOClause.Font = FontManager.LoadFont("PMU", 32);
            chkOHKOClause.ForeColor = Color.WhiteSmoke;
            chkOHKOClause.Text = "OHKO Clause";
            chkOHKOClause.Checked = rules.OHKOClause;

            chkSelfKOClause = new CheckBox("chkSelfKOClause");
            chkSelfKOClause.Location = new Point(15, 208);
            chkSelfKOClause.Size = new System.Drawing.Size(200, 32);
            chkSelfKOClause.BackColor = Color.Transparent;
            chkSelfKOClause.Font = FontManager.LoadFont("PMU", 32);
            chkSelfKOClause.ForeColor = Color.WhiteSmoke;
            chkSelfKOClause.Text = "Self-KO Clause";
            chkSelfKOClause.Checked = rules.SelfKOClause;

            btnSave = new Button("btnSave");
            btnSave.Location = new Point(15, 245);
            btnSave.Size = new Size(100, 15);
            btnSave.Text = "Save";
            btnSave.Click += new EventHandler<MouseButtonEventArgs>(btnSave_Click);

            this.AddWidget(chkSleepClause);
            this.AddWidget(chkAccuracyClause);
            this.AddWidget(chkSpeciesClause);
            this.AddWidget(chkFreezeClause);
            this.AddWidget(chkOHKOClause);
            this.AddWidget(chkSelfKOClause);
            this.AddWidget(btnSave);
            this.AddWidget(lblEditRules);
        }

        void btnSave_Click(object sender, MouseButtonEventArgs e) {
            TournamentRules rules = new TournamentRules();
            rules.SleepClause = chkSleepClause.Checked;
            rules.AccuracyClause = chkAccuracyClause.Checked;
            rules.SpeciesClause = chkSpeciesClause.Checked;
            rules.FreezeClause = chkFreezeClause.Checked;
            rules.OHKOClause = chkOHKOClause.Checked;
            rules.SelfKOClause = chkSelfKOClause.Checked;

            Network.Messenger.SendSaveTournamentRules(rules);

            Menus.MenuSwitcher.CloseAllMenus();
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e) {
            base.OnKeyboardDown(e);
            switch (e.Key) {
                case SdlDotNet.Input.Key.Return: {

                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {


                    }
                    break;
            }
        }

    }
}
