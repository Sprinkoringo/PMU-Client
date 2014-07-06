using System;
using System.Collections.Generic;

using System.Drawing;
using System.Text;

using SdlDotNet.Widgets;

namespace Client.Logic.Windows
{
    class winOptions : Core.WindowCore
    {

        #region Fields

        Label lblPlayerData;
        Label lblPlayerDataName;
        Label lblPlayerDataDamage;
        Label lblPlayerDataMiniHP;
        Label lblPlayerDataAutoSaveSpeed;
        Label lblLeft;
        Label lblRight;
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
        int tempAutoSaveSpeed2;

        #endregion Fields

        #region Constructors

        public winOptions()
            : base("winOptions")
        {
            this.Windowed = true;
            this.ShowInWindowSwitcher = false;
            this.TitleBar.Text = "Options";
            this.TitleBar.CloseButton.Visible = false;
            this.Size = new Size(600, 280);
            //this.BackgroundImage = Skins.SkinManager.LoadGui("Options");
            this.Location = DrawingSupport.GetCenter(SdlDotNet.Graphics.Video.Screen.Size, this.Size);

            lblPlayerData = new Label("lblPlayerData");
            lblPlayerData.Location = new Point(0, 0);
            lblPlayerData.AutoSize = true;
            lblPlayerData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblPlayerData.Text = "Player Data:";

            lblPlayerDataName = new Label("lblPlayerDataName");
            lblPlayerDataName.Location = new Point(130, 8);
            lblPlayerDataName.AutoSize = true;
            lblPlayerDataName.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblPlayerDataName.Text = "Name: ";
            lblPlayerDataName.HoverColor = Color.Red;
            lblPlayerDataName.Click +=new EventHandler<MouseButtonEventArgs>(lblPlayerDataName_Click);

            lblPlayerDataDamage = new Label("lblPlayerDataDamage");
            lblPlayerDataDamage.Location = new Point(250, 8);
            lblPlayerDataDamage.AutoSize = true;
            lblPlayerDataDamage.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblPlayerDataDamage.Text = "Damage: ";
            lblPlayerDataDamage.HoverColor = Color.Red;
            lblPlayerDataDamage.Click +=new EventHandler<MouseButtonEventArgs>(lblPlayerDataDamage_Click);

            lblPlayerDataMiniHP = new Label("lblPlayerDataMiniHP");
            lblPlayerDataMiniHP.Location = new Point(400, 8);
            lblPlayerDataMiniHP.AutoSize = true;
            lblPlayerDataMiniHP.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblPlayerDataMiniHP.Text = "Mini-HP: ";
            lblPlayerDataMiniHP.HoverColor = Color.Red;
            lblPlayerDataMiniHP.Click +=new EventHandler<MouseButtonEventArgs>(lblPlayerDataMiniHP_Click);

            lblPlayerDataAutoSaveSpeed = new Label("lblPlayerDataAutoSaveSpeed");
            lblPlayerDataAutoSaveSpeed.Location = new Point(130, 32);
            lblPlayerDataAutoSaveSpeed.AutoSize = true;
            lblPlayerDataAutoSaveSpeed.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: ";

            lblLeft = new Label("lblLeft");
            lblLeft.Location = new Point(100, 32);
            lblLeft.AutoSize = true;
            lblLeft.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblLeft.Text = "<";
            lblLeft.HoverColor = Color.Red;
            lblLeft.Click += new EventHandler<MouseButtonEventArgs>(lblLeft_Click);

            lblRight = new Label("lblRight");
            lblRight.Location = new Point(300, 32);
            lblRight.AutoSize = true;
            lblRight.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblRight.Text = ">";
            lblRight.HoverColor = Color.Red;
            lblRight.Click += new EventHandler<MouseButtonEventArgs>(lblRight_Click);

            lblNpcData = new Label("lblNpcData");
            lblNpcData.Location = new Point(0, 56);
            lblNpcData.AutoSize = true;
            lblNpcData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblNpcData.Text = "NPC Data:";

            lblNpcDataName = new Label("lblNpcDataName");
            lblNpcDataName.Location = new Point(130, 64);
            lblNpcDataName.AutoSize = true;
            lblNpcDataName.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblNpcDataName.Text = "Name: ";
            lblNpcDataName.HoverColor = Color.Red;
            lblNpcDataName.Click +=new EventHandler<MouseButtonEventArgs>(lblNpcDataName_Click);

            lblNpcDataDamage = new Label("lblNpcDataDamage");
            lblNpcDataDamage.Location = new Point(250, 64);
            lblNpcDataDamage.AutoSize = true;
            lblNpcDataDamage.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblNpcDataDamage.Text = "Damage: ";
            lblNpcDataDamage.HoverColor = Color.Red;
            lblNpcDataDamage.Click +=new EventHandler<MouseButtonEventArgs>(lblNpcDataDamage_Click);

            lblNpcDataMiniHP = new Label("lblNpcDataMiniHP");
            lblNpcDataMiniHP.Location = new Point(400, 64);
            lblNpcDataMiniHP.AutoSize = true;
            lblNpcDataMiniHP.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblNpcDataMiniHP.Text = "Mini-HP: ";
            lblNpcDataMiniHP.HoverColor = Color.Red;
            lblNpcDataMiniHP.Click +=new EventHandler<MouseButtonEventArgs>(lblNpcDataMiniHP_Click);

            lblSoundData = new Label("lblSoundData");
            lblSoundData.Location = new Point(0, 88);
            lblSoundData.AutoSize = true;
            lblSoundData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblSoundData.Text = "Sound Data:";

            lblSoundDataMusic = new Label("lblSoundDataMusic");
            lblSoundDataMusic.Location = new Point(130, 96);
            lblSoundDataMusic.AutoSize = true;
            lblSoundDataMusic.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblSoundDataMusic.Text = "Music: ";
            lblSoundDataMusic.HoverColor = Color.Red;
            lblSoundDataMusic.Click +=new EventHandler<MouseButtonEventArgs>(lblSoundDataMusic_Click);

            lblSoundDataSound = new Label("lblSoundDataSound");
            lblSoundDataSound.Location = new Point(250, 96);
            lblSoundDataSound.AutoSize = true;
            lblSoundDataSound.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblSoundDataSound.Text = "Sound: ";
            lblSoundDataSound.HoverColor = Color.Red;
            lblSoundDataSound.Click +=new EventHandler<MouseButtonEventArgs>(lblSoundDataSound_Click);

            lblChatData = new Label("lblChatData");
            lblChatData.Location = new Point(0, 120);
            lblChatData.AutoSize = true;
            lblChatData.Font = Graphics.FontManager.LoadFont("PMU", 32);
            lblChatData.Text = "Chat Data";

            lblChatDataSpeechBubbles = new Label("lblChatDataSpeechBubbles");
            lblChatDataSpeechBubbles.Location = new Point(130, 128);
            lblChatDataSpeechBubbles.AutoSize = true;
            lblChatDataSpeechBubbles.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblChatDataSpeechBubbles.Text = "Speech Bubbles: ";
            lblChatDataSpeechBubbles.HoverColor = Color.Red;
            lblChatDataSpeechBubbles.Click +=new EventHandler<MouseButtonEventArgs>(lblChatDataSpeechBubbles_Click);

            lblChatDataTimeStamps = new Label("lblChatDataTimeStamps");
            lblChatDataTimeStamps.Location = new Point(300, 128);
            lblChatDataTimeStamps.AutoSize = true;
            lblChatDataTimeStamps.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblChatDataTimeStamps.Text = "TimeStamps: ";
            lblChatDataTimeStamps.HoverColor = Color.Red;
            lblChatDataTimeStamps.Click +=new EventHandler<MouseButtonEventArgs>(lblChatDataTimeStamps_Click);

            lblChatDataAutoScroll = new Label("lblChatDataAutoScroll");
            lblChatDataAutoScroll.Location = new Point(130, 152);
            lblChatDataAutoScroll.AutoSize = true;
            lblChatDataAutoScroll.Font = Graphics.FontManager.LoadFont("PMU", 24);
            lblChatDataAutoScroll.Text = "Auto-Scroll: ";
            lblChatDataAutoScroll.HoverColor = Color.Red;
            lblChatDataAutoScroll.Click +=new EventHandler<MouseButtonEventArgs>(lblChatDataAutoScroll_Click);

            lblSave = new Label("lblSave");
            lblSave.Location = new Point(480, 170);
            lblSave.AutoSize = true;
            lblSave.Font = Graphics.FontManager.LoadFont("PMU", 72);
            lblSave.Text = "Save";
            lblSave.HoverColor = Color.Red;
            lblSave.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(lblSave_Click);


            this.AddWidget(lblPlayerData);
            this.AddWidget(lblPlayerDataName);
            this.AddWidget(lblPlayerDataDamage);
            this.AddWidget(lblPlayerDataMiniHP);
            this.AddWidget(lblPlayerDataAutoSaveSpeed);
            this.AddWidget(lblLeft);
            this.AddWidget(lblRight);

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

            this.LoadComplete();

            tempOptions = new bool[12];
            tempAutoSaveSpeed = new int();
            tempAutoSaveSpeed2 = new int();


            for (int i = 0; i < 12; i++)
            {
                CreateTempOption(i);
                ShowOption(i);
            }
        }

        #endregion Constructors
        
        #region Methods

        void lblPlayerDataName_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(0);
        }

        void lblPlayerDataDamage_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(1);
        }
        
        void lblPlayerDataMiniHP_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(2);
        }
        
        void lblRight_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(3);
        }

        void lblLeft_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            SelectItem(13);
        }

        void lblNpcDataName_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(4);
        }

        void lblNpcDataDamage_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(5);
        }

        void lblNpcDataMiniHP_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(6);
        }

        void lblSoundDataMusic_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(7);
        }

        void lblSoundDataSound_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(8);
        }

        void lblChatDataSpeechBubbles_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(9);
        }

        void lblChatDataTimeStamps_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(10);
        }

        void lblChatDataAutoScroll_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(11);
        }
        
        void lblSave_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            SelectItem(12);
        }

        public override void OnKeyboardDown(SdlDotNet.Input.KeyboardEventArgs e)
        {
            base.OnKeyboardDown(e);
            switch (e.Key)
            {
                case SdlDotNet.Input.Key.Backspace:
                    {
                        WindowSwitcher.ShowMainMenu();
                    }
                    break;
            }
        }

        private void SelectItem(int itemNum)
        {
            if (itemNum == 3)
            {
                if (tempAutoSaveSpeed < 11)
                {
                    tempAutoSaveSpeed++;
                    ShowOption(3);
                }

                if (tempAutoSaveSpeed == 11)
                {
                    tempAutoSaveSpeed = 0;
                    tempAutoSaveSpeed2 = 1;
                    ShowOption(3);
                }
            }
            else if (itemNum < 12)
            {
                tempOptions[itemNum] = !tempOptions[itemNum];
                ShowOption(itemNum);
            }
            else if (itemNum == 13)
            {
                if (tempAutoSaveSpeed > 0)
                {
                    tempAutoSaveSpeed--;
                    ShowOption(3);
                }
                if (tempAutoSaveSpeed == 0)
                {
                    tempAutoSaveSpeed2 ++;
                    ShowOption(3);
                }
                if (tempAutoSaveSpeed2 == 2)
                {
                    tempAutoSaveSpeed = 10;
                    tempAutoSaveSpeed2 = 0;
                    ShowOption(3);
                }
            }
            else
            {
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

                if (IO.Options.Music == false)
                {
                    Logic.Music.Music.AudioPlayer.StopMusic();
                }
                WindowSwitcher.ShowMainMenu();
                this.Close();
            }

        }

        public void CreateTempOption(int itemNum)
        {
            switch (itemNum)
            {
                case 0:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerName;
                    }
                    break;
                case 1:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerDamage;
                    }
                    break;
                case 2:
                    {
                        tempOptions[itemNum] = IO.Options.PlayerBar;
                    }
                    break;
                case 3:
                    {
                        tempAutoSaveSpeed = IO.Options.AutoSaveSpeed;
                    }
                    break;
                case 4:
                    {
                        tempOptions[itemNum] = IO.Options.NpcName;
                    }
                    break;
                case 5:
                    {
                        tempOptions[itemNum] = IO.Options.NpcDamage;
                    }
                    break;
                case 6:
                    {
                        tempOptions[itemNum] = IO.Options.NpcBar;
                    }
                    break;
                case 7:
                    {
                        tempOptions[itemNum] = IO.Options.Music;
                    }
                    break;
                case 8:
                    {
                        tempOptions[itemNum] = IO.Options.Sound;
                    }
                    break;
                case 9:
                    {
                        tempOptions[itemNum] = IO.Options.SpeechBubbles;
                    }
                    break;
                case 10:
                    {
                        tempOptions[itemNum] = IO.Options.Timestamps;
                    }
                    break;
                case 11:
                    {
                        tempOptions[itemNum] = IO.Options.AutoScroll;
                    }
                    break;




            }
        }

        public void ShowOption(int itemNum)
        {

            switch (itemNum)
            {
                case 0:
                    {
                        lblPlayerDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 1:
                    {
                        lblPlayerDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 2:
                    {
                        lblPlayerDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 3:
                    {
                        lblPlayerDataAutoSaveSpeed.Text = "Auto-Save Speed: " + tempAutoSaveSpeed;
                    }
                    break;
                case 4:
                    {
                        lblNpcDataName.Text = "Name: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 5:
                    {
                        lblNpcDataDamage.Text = "Damage: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 6:
                    {
                        lblNpcDataMiniHP.Text = "Mini-HP Bar: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 7:
                    {
                        lblSoundDataMusic.Text = "Music: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 8:
                    {
                        lblSoundDataSound.Text = "Sound Effects: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 9:
                    {
                        lblChatDataSpeechBubbles.Text = "Speech Bubbles: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 10:
                    {
                        lblChatDataTimeStamps.Text = "TimeStamps: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;
                case 11:
                    {
                        lblChatDataAutoScroll.Text = "Auto-Scroll: " + BoolToString(tempOptions[itemNum]);
                    }
                    break;

            }
        }

        public String BoolToString(bool setting)
        {
            if (setting == true)
            {

                return "On";
            }

            return "Off";
        }

        #endregion Methods

        
    }
}
