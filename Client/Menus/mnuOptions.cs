using System;
using System.Collections.Generic;

using System.Drawing;
using System.Text;

using Client.Logic.Graphics;

using SdlDotNet.Widgets;

namespace Client.Logic.Menus
{
    class mnuOptions : Widgets.BorderedPanel, Core.IMenu
    {

        public bool Modal {
            get;
            set;
        }

        #region Fields

        const int MAX_ITEMS = 12;

        Widgets.MenuItemPicker itemPicker;
        Label lblOptions;
        Label lblPlayerData;
        Label lblPlayerDataName;
        Label lblPlayerDataDamage;
        Label lblPlayerDataMiniHP;
        Label lblPlayerDataAutoSaveSpeed;
        Label lblNpcData;
        Label lblNpcDataName;
        Label lblNpcDataDamage;
        Label lblNpcDataMiniHP;
        Label lblSoundData;
        Label lblSoundDataMusic;
        Label lblSoundDataSound;
        Label lblChatData;
        Label lblChatDataSpeechBubbles;
        Label lblChatDataTimeStamps;
        Label lblChatDataAutoScroll;
        Label lblSave;

        bool[] tempOptions;
        int tempAutoSaveSpeed;

        #endregion Fields

        #region Constructors

        public mnuOptions(string name)
            : base(name) {
            this.Size = new Size(280, 390);
            base.MenuDirection = Enums.MenuDirection.Vertical;

            this.Location = new Point(10, 40);

            itemPicker = new Widgets.MenuItemPicker("itemPicker");
            itemPicker.Location = new Point(30, 83);

            lblOptions = new Label("lblOptions");
            lblOptions.Location = new Point(20, 0);
            lblOptions.AutoSize = true;
            lblOptions.Font = FontManager.LoadFont("PMU", 48);
            lblOptions.Text = "Options";
            lblOptions.ForeColor = Color.WhiteSmoke;

            lblPlayerData = new Label("lblPlayerData");
            lblPlayerData.Location = new Point(30, 48);
            lblPlayerData.AutoSize = true;
            lblPlayerData.Font = FontManager.LoadFont("PMU", 32);
            lblPlayerData.Text = "Player Data";
            lblPlayerData.ForeColor = Color.WhiteSmoke;

            lblPlayerDataName = new Label("lblPlayerDataName");
            lblPlayerDataName.Location = new Point(40, 78);
            lblPlayerDataName.AutoSize = true;
            lblPlayerDataName.Font = FontManager.LoadFont("PMU", 16);
            lblPlayerDataName.Text = "Name: ";
            lblPlayerDataName.ForeColor = Color.WhiteSmoke;
            //lblPlayerDataName.HoverColor = Color.Red;

            lblPlayerDataDamage = new Label("lblPlayerDataDamage");
            lblPlayerDataDamage.Location = new Point(40, 92);
            lblPlayerDataDamage.AutoSize = true;
            lblPlayerDataDamage.Font = FontManager.LoadFont("PMU", 16);
            lblPlayerDataDamage.Text = "Damage: ";
            lblPlayerDataDamage.ForeColor = Color.WhiteSmoke;
            //lblPlayerDataDamage.HoverColor = Color.Red;

            lblPlayerDataMiniHP = new Label("lblPlayerDataMiniHP");
            lblPlayerDataMiniHP.Location = new Point(40, 106);
            lblPlayerDataMiniHP.AutoSize = true;
            lblPlayerDataMiniHP.Font = FontManager.LoadFont("PMU", 16);
            lblPlayerDataMiniHP.Text = "Mini-HP: ";
            lblPlayerDataMiniHP.ForeColor = Color.WhiteSmoke;
            //lblPlayerDataMiniHP.HoverColor = Color.Red;

            lblPlayerDataAutoSaveSpeed = new Label("lblPlayerDataAutoSaveSpeed");
            lblPlayerDataAutoSaveSpeed.Location = new Point(40, 120);
            lblPlayerDataAutoSaveSpeed.AutoSize = true;
            lblPlayerDataAutoSaveSpeed.Font = FontManager.LoadFont("PMU", 16);
            lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: ";
            lblPlayerDataAutoSaveSpeed.ForeColor = Color.WhiteSmoke;
            //lblPlayerDataAutoSaveSpeed.HoverColor = Color.Red;


            lblNpcData = new Label("lblNpcData");
            lblNpcData.Location = new Point(30, 130);
            lblNpcData.AutoSize = true;
            lblNpcData.Font = FontManager.LoadFont("PMU", 32);
            lblNpcData.Text = "NPC Data";
            lblNpcData.ForeColor = Color.WhiteSmoke;

            lblNpcDataName = new Label("lblNpcDataName");
            lblNpcDataName.Location = new Point(40, 160);
            lblNpcDataName.AutoSize = true;
            lblNpcDataName.Font = FontManager.LoadFont("PMU", 16);
            lblNpcDataName.Text = "Name: ";
            lblNpcDataName.ForeColor = Color.WhiteSmoke;
            //lblNpcDataName.HoverColor = Color.Red;

            lblNpcDataDamage = new Label("lblNpcDataDamage");
            lblNpcDataDamage.Location = new Point(40, 174);
            lblNpcDataDamage.AutoSize = true;
            lblNpcDataDamage.Font = FontManager.LoadFont("PMU", 16);
            lblNpcDataDamage.Text = "Damage: ";
            lblNpcDataDamage.ForeColor = Color.WhiteSmoke;
            //lblNpcDataDamage.HoverColor = Color.Red;

            lblNpcDataMiniHP = new Label("lblNpcDataMiniHP");
            lblNpcDataMiniHP.Location = new Point(40, 188);
            lblNpcDataMiniHP.AutoSize = true;
            lblNpcDataMiniHP.Font = FontManager.LoadFont("PMU", 16);
            lblNpcDataMiniHP.Text = "Mini-HP: ";
            lblNpcDataMiniHP.ForeColor = Color.WhiteSmoke;
            //lblNpcDataMiniHP.HoverColor = Color.Red;


            lblSoundData = new Label("lblSoundData");
            lblSoundData.Location = new Point(30, 198);
            lblSoundData.AutoSize = true;
            lblSoundData.Font = FontManager.LoadFont("PMU", 32);
            lblSoundData.Text = "Sound Data";
            lblSoundData.ForeColor = Color.WhiteSmoke;

            lblSoundDataMusic = new Label("lblSoundDataMusic");
            lblSoundDataMusic.Location = new Point(40, 228);
            lblSoundDataMusic.AutoSize = true;
            lblSoundDataMusic.Font = FontManager.LoadFont("PMU", 16);
            lblSoundDataMusic.Text = "Music: ";
            lblSoundDataMusic.ForeColor = Color.WhiteSmoke;
            //lblSoundDataMusic.HoverColor = Color.Red;

            lblSoundDataSound = new Label("lblSoundDataSound");
            lblSoundDataSound.Location = new Point(40, 242);
            lblSoundDataSound.AutoSize = true;
            lblSoundDataSound.Font = FontManager.LoadFont("PMU", 16);
            lblSoundDataSound.Text = "Sound: ";
            lblSoundDataSound.ForeColor = Color.WhiteSmoke;
            //lblSoundDataSound.HoverColor = Color.Red;


            lblChatData = new Label("lblChatData");
            lblChatData.Location = new Point(30, 252);
            lblChatData.AutoSize = true;
            lblChatData.Font = FontManager.LoadFont("PMU", 32);
            lblChatData.Text = "Chat Data";
            lblChatData.ForeColor = Color.WhiteSmoke;

            lblChatDataSpeechBubbles = new Label("lblChatDataSpeechBubbles");
            lblChatDataSpeechBubbles.Location = new Point(40, 282);
            lblChatDataSpeechBubbles.AutoSize = true;
            lblChatDataSpeechBubbles.Font = FontManager.LoadFont("PMU", 16);
            lblChatDataSpeechBubbles.Text = "Speech Bubbles: ";
            lblChatDataSpeechBubbles.ForeColor = Color.WhiteSmoke;
            //lblChatDataSpeechBubbles.HoverColor = Color.Red;

            lblChatDataTimeStamps = new Label("lblChatDataTimeStamps");
            lblChatDataTimeStamps.Location = new Point(40, 296);
            lblChatDataTimeStamps.AutoSize = true;
            lblChatDataTimeStamps.Font = FontManager.LoadFont("PMU", 16);
            lblChatDataTimeStamps.Text = "TimeStamps: ";
            lblChatDataTimeStamps.ForeColor = Color.WhiteSmoke;
            //lblChatDataTimeStamps.HoverColor = Color.Red;

            lblChatDataAutoScroll = new Label("lblChatDataAutoScroll");
            lblChatDataAutoScroll.Location = new Point(40, 310);
            lblChatDataAutoScroll.AutoSize = true;
            lblChatDataAutoScroll.Font = FontManager.LoadFont("PMU", 16);
            lblChatDataAutoScroll.Text = "Auto-Scroll: ";
            lblChatDataAutoScroll.ForeColor = Color.WhiteSmoke;
            //lblChatDataAutoScroll.HoverColor = Color.Red;


            lblSave = new Label("lblSave");
            lblSave.Location = new Point(30, 330);
            lblSave.AutoSize = true;
            lblSave.Font = FontManager.LoadFont("PMU", 16);
            lblSave.Text = "Save";
            lblSave.HoverColor = Color.Red;
            lblSave.ForeColor = Color.WhiteSmoke;
            lblSave.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSave_Click);


            this.AddWidget(itemPicker);

            this.AddWidget(lblOptions);

            this.AddWidget(lblPlayerData);
            this.AddWidget(lblPlayerDataName);
            this.AddWidget(lblPlayerDataDamage);
            this.AddWidget(lblPlayerDataMiniHP);
            this.AddWidget(lblPlayerDataAutoSaveSpeed);

            this.AddWidget(lblNpcData);
            this.AddWidget(lblNpcDataName);
            this.AddWidget(lblNpcDataDamage);
            this.AddWidget(lblNpcDataMiniHP);

            this.AddWidget(lblSoundData);
            this.AddWidget(lblSoundDataMusic);
            this.AddWidget(lblSoundDataSound);


            this.AddWidget(lblChatData);
            this.AddWidget(lblChatDataSpeechBubbles);
            this.AddWidget(lblChatDataTimeStamps);
            this.AddWidget(lblChatDataAutoScroll);

            this.AddWidget(lblSave);

            tempOptions = new bool[12];
            tempAutoSaveSpeed = new int();



            for (int i = 0; i < 12; i++) {
                CreateTempOption(i);
                ShowOption(i);
            }
        }


        void lblSave_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(12);
        }


        #endregion Constructors

        #region Methods

        public void ChangeSelected(int itemNum) {
            int pointerX = 30;
            int pointerY = 83;

            if (itemNum > 3) {
                pointerY += 26;
                if (itemNum > 6) {
                    pointerY += 26;
                    if (itemNum > 8) {
                        pointerY += 26;
                        if (itemNum > 11) {
                            pointerX -= 10;
                            pointerY += 6;
                        }
                    }
                }
            }

            itemPicker.Location = new Point(pointerX, pointerY + (14 * itemNum));

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
                case SdlDotNet.Input.Key.LeftArrow: {
                        if (itemPicker.SelectedItem == 3) {
                            if (tempAutoSaveSpeed > 0) {
                                tempAutoSaveSpeed--;
                                ShowOption(3);
                            }
                        } else if (itemPicker.SelectedItem != 12) {
                            SelectItem(itemPicker.SelectedItem);
                        }
                    	Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.RightArrow: {
                        if (itemPicker.SelectedItem == 3) {
                            if (tempAutoSaveSpeed < 10) {
                                tempAutoSaveSpeed++;
                                ShowOption(3);
                            }
                        } else if (itemPicker.SelectedItem != 12) {
                            SelectItem(itemPicker.SelectedItem);
                        }
                    	Music.Music.AudioPlayer.PlaySoundEffect("beep4.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Return: {
                        SelectItem(itemPicker.SelectedItem);
                        Music.Music.AudioPlayer.PlaySoundEffect("beep2.wav");
                    }
                    break;
                case SdlDotNet.Input.Key.Backspace: {
                        // goes to the main menu; should it?
                        MenuSwitcher.ShowOthersMenu();
                        Music.Music.AudioPlayer.PlaySoundEffect("beep3.wav");
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum) {
            if (itemNum == 3) {
                if (tempAutoSaveSpeed < 10) {
                    tempAutoSaveSpeed++;
                    ShowOption(3);
                }
            } else if (itemNum < 12) {
                tempOptions[itemNum] = !tempOptions[itemNum];
                ShowOption(itemNum);
            } else {
                //Save method goes here
                IO.Options.PlayerName = tempOptions[0];
                IO.Options.PlayerDamage = tempOptions[1];
                IO.Options.PlayerBar = tempOptions[2];

                IO.Options.AutoSaveSpeed = tempAutoSaveSpeed;

                IO.Options.NpcName = tempOptions[4];
                IO.Options.NpcDamage = tempOptions[5];
                IO.Options.NpcBar = tempOptions[6];
                IO.Options.Music = tempOptions[7];
                IO.Options.Sound = tempOptions[8];
                IO.Options.SpeechBubbles = tempOptions[9];
                IO.Options.Timestamps = tempOptions[10];
                IO.Options.AutoScroll = tempOptions[11];
                ExpKit.Modules.kitChat chat = (ExpKit.Modules.kitChat)Windows.WindowSwitcher.ExpKit.KitContainer.ModuleSwitcher.FindKitModule(Enums.ExpKitModules.Chat);
                chat.SetAutoScroll(IO.Options.AutoScroll);

                IO.Options.SaveXml();
                IO.Options.UpdateActiveOptions();

                MenuSwitcher.ShowMainMenu();
                
            }

        }

        public void CreateTempOption(int itemNum) {
            switch (itemNum) {
                case 0: {
                        tempOptions[itemNum] = IO.Options.PlayerName;
                    }
                    break;
                case 1: {
                        tempOptions[itemNum] = IO.Options.PlayerDamage;
                    }
                    break;
                case 2: {
                        tempOptions[itemNum] = IO.Options.PlayerBar;
                    }
                    break;
                case 3: {
                        tempAutoSaveSpeed = IO.Options.AutoSaveSpeed;
                    }
                    break;
                case 4: {
                        tempOptions[itemNum] = IO.Options.NpcName;
                    }
                    break;
                case 5: {
                        tempOptions[itemNum] = IO.Options.NpcDamage;
                    }
                    break;
                case 6: {
                        tempOptions[itemNum] = IO.Options.NpcBar;
                    }
                    break;
                case 7: {
                        tempOptions[itemNum] = IO.Options.Music;
                    }
                    break;
                case 8: {
                        tempOptions[itemNum] = IO.Options.Sound;
                    }
                    break;
                case 9: {
                        tempOptions[itemNum] = IO.Options.SpeechBubbles;
                    }
                    break;
                case 10: {
                        tempOptions[itemNum] = IO.Options.Timestamps;
                    }
                    break;
                case 11: {
                        tempOptions[itemNum] = IO.Options.AutoScroll;
                    }
                    break;




            }
        }

        public void ShowOption(int itemNum) {

            switch (itemNum) {
                case 0: {
                        lblPlayerDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 1: {
                        lblPlayerDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 2: {
                        lblPlayerDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 3: {
                        lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: " + tempAutoSaveSpeed;
                    }
                    break;
                case 4: {
                        lblNpcDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 5: {
                        lblNpcDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 6: {
                        lblNpcDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 7: {
                        lblSoundDataMusic.Text = "Music: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 8: {
                        lblSoundDataSound.Text = "Sound Effects: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 9: {
                        lblChatDataSpeechBubbles.Text = "Speech Bubbles: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 10: {
                        lblChatDataTimeStamps.Text = "TimeStamps: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 11: {
                        lblChatDataAutoScroll.Text = "Auto-Scroll: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;




            }
        }

        public String BoolToString(bool setting) {
            if (setting == true) {

                return "On";
            }

            return "Off";
        }

        #endregion Methods

        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }
    }
}
