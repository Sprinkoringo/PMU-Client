namespace Client.Logic.IO
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Xml;

    using PMU.Core;

    /// <summary>
    /// Handles and stores game options
    /// </summary>
    public class Options
    {
        #region Properties

        /// <summary>
        /// Gets or sets a value indicating whether the client will auto save.
        /// </summary>
        /// <value><c>true</c> if the client will auto save; otherwise, <c>false</c>.</value>
        public static bool AutoSave {
            get;
            set;
        }

        /// <summary>
        /// gets or sets whether or not to ask for ping
        /// </summary>
        public static bool Ping {
            get;
            set;
        }

        /// <summary>
        /// gets or sets whether or not to ask for FPS
        /// </summary>
        public static bool FPS {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the auto save speed.
        /// </summary>
        /// <value>The auto save speed.</value>
        public static int AutoSaveSpeed {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the client will auto scroll.
        /// </summary>
        /// <value><c>true</c> if the client will auto scroll; otherwise, <c>false</c>.</value>
        public static bool AutoScroll {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection IP.
        /// </summary>
        /// <value>The connection IP.</value>
        public static string ConnectionIP
        {
            get;
            set;
        }

        public static string UpdateAddress
        {
            get;
            set;
        }

        public static string MusicAddress
        {
            get;
            set;
        }

        public static string SoundAddress
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the connection port.
        /// </summary>
        /// <value>The connection port.</value>
        public static int ConnectionPort {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether [map grid].
        /// </summary>
        /// <value><c>true</c> if [map grid]; otherwise, <c>false</c>.</value>
        public static bool MapGrid {
            get;
            set;
        }

        public static bool DisplayAttributes {
            get;
            set;
        }

        public static bool DisplayDungeonValues {
            get;
            set;
        }

        public static bool DragAndPlace {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether music is enabled.
        /// </summary>
        /// <value><c>true</c> if music is enabled; otherwise, <c>false</c>.</value>
        public static bool Music {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether NPC bar's are enabled.
        /// </summary>
        /// <value><c>true</c> if NPC bar's are enabled; otherwise, <c>false</c>.</value>
        public static bool NpcBar {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether NPC damage is displayed.
        /// </summary>
        /// <value><c>true</c> if NPC damage is displayed; otherwise, <c>false</c>.</value>
        public static bool NpcDamage {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether NPC name's are displayed.
        /// </summary>
        /// <value><c>true</c> if NPC name's are displayed; otherwise, <c>false</c>.</value>
        public static bool NpcName {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player health bar is displayed.
        /// </summary>
        /// <value><c>true</c> if the player health bar is displayed; otherwise, <c>false</c>.</value>
        public static bool PlayerBar {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's damage is displayed.
        /// </summary>
        /// <value><c>true</c> if the player's damage is displayed; otherwise, <c>false</c>.</value>
        public static bool PlayerDamage {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the player's name is displayed.
        /// </summary>
        /// <value><c>true</c> if the player's name is displayed; otherwise, <c>false</c>.</value>
        public static bool PlayerName {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the saved account.
        /// </summary>
        /// <value>The saved account.</value>
        public static string SavedAccount {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the saved password.
        /// </summary>
        /// <value>The saved password.</value>
        public static string SavedPassword {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether sound is enabled.
        /// </summary>
        /// <value><c>true</c> if sound is enabled; otherwise, <c>false</c>.</value>
        public static bool Sound {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether speech bubbles are enabled.
        /// </summary>
        /// <value><c>true</c> if speech bubbles are enabled; otherwise, <c>false</c>.</value>
        public static bool SpeechBubbles {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating whether timestamps are enabled.
        /// </summary>
        /// <value><c>true</c> if timestamps are enabled; otherwise, <c>false</c>.</value>
        public static bool Timestamps {
            get;
            set;
        }

        public static string ActiveSkin {
            get;
            set;
        }

        public static XmlWriterSettings XmlWriterSettings { get; set; }

        #endregion Properties

        #region Methods

        public static void Initialize() {
            XmlWriterSettings = new System.Xml.XmlWriterSettings();   
            XmlWriterSettings.OmitXmlDeclaration = false; 
            XmlWriterSettings.IndentChars = "  ";
            XmlWriterSettings.Indent = true;
            XmlWriterSettings.NewLineChars = System.Environment.NewLine;
        }

        /// <summary>
        /// Loads the options.
        /// </summary>
        public static void LoadOptions() {
            if (IO.FileExists(Paths.CreateOSPath("settings.xml")) == false) {
                SaveXml();
            }
            try {
                using (XmlReader reader = XmlReader.Create(Paths.CreateOSPath("settings.xml"))) {
                    while (reader.Read()) {
                        if (reader.IsStartElement()) {
                            switch (reader.Name) {
                                case "AutoSave": {
                                        AutoSave = reader.ReadString().ToBool();
                                    }
                                    break;
                                case "AutoSaveSpeed": {
                                        AutoSaveSpeed = reader.ReadString().ToInt();
                                    }
                                    break;
                                case "AutoScroll": {
                                        AutoScroll = reader.ReadString().ToBool();
                                    }
                                    break;
                                case "Music": {
                                        Music = reader.ReadString().ToBool();
                                    }
                                    break;
                                case "NpcBar": {
                                        NpcBar = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "NpcDamage": {
                                        NpcDamage = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "NpcName": {
                                        NpcName = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "PlayerBar": {
                                        PlayerBar = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "PlayerDamage": {
                                        PlayerDamage = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "PlayerName": {
                                        PlayerName = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "SavedAccount": {
                                        SavedAccount = reader.ReadString();
                                    }
                                    break;
                                case "SavedPassword": {
                                        SavedPassword = reader.ReadString();
                                    }
                                    break;
                                case "Sound": {
                                        Sound = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "SpeechBubbles": {
                                        SpeechBubbles = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "Timestamps": {
                                        Timestamps = reader.ReadString().ToBool();
                                    }
                                    break;
                                case "Port": {
                                        ConnectionPort = reader.ReadString().ToInt(4001);
                                    }
                                    break;
                                case "Server":
                                    {
                                        ConnectionIP = reader.ReadString();
                                    }
                                    break;
                                case "UpdateLink":
                                    {
                                        UpdateAddress = reader.ReadString();
                                    }
                                    break;
                                case "MusicLink":
                                    {
                                        MusicAddress = reader.ReadString();
                                    }
                                    break;
                                case "SFXLink":
                                    {
                                        SoundAddress = reader.ReadString();
                                    }
                                    break;
                                case "MapGrid": {
                                        MapGrid = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "DisplayAttributes": {
                                        DisplayAttributes = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "DragAndPlace": {
                                        DragAndPlace = reader.ReadString().ToBool(true);
                                    }
                                    break;
                                case "ActiveSkin": {
                                        ActiveSkin = reader.ReadString();
                                    }
                                    break;
                            }
                        }
                    }
                }
            } catch (XmlException) {
                // Fix for root element missing errors
                SaveXml();
            }

            // Verify all options are correct
            if (AutoSaveSpeed < 0 /*|| AutoSaveSpeed > 10*/)
                AutoSaveSpeed = 0;
            if (string.IsNullOrEmpty(ConnectionIP)) {
                ConnectionIP = "game.pmuniverse.net";
            }
            if (ConnectionPort == 0) {
                ConnectionPort = 4001;
            }
            if (string.IsNullOrEmpty(ActiveSkin)) {
                ActiveSkin = "Main Theme";
            }
            // TODO: Remove when unneeded (Autoswitch to localhost server if compiled under DEBUG)
#if DEBUG
            //ConnectionIP = "localhost";
#endif
        }

        /// <summary>
        /// Saves the XML document.
        /// </summary>
        public static void SaveXml() {
            using (XmlWriter writer = XmlWriter.Create(Paths.CreateOSPath("settings.xml"), XmlWriterSettings)) {
                writer.WriteStartDocument();
                writer.WriteStartElement("Options");

                writer.WriteStartElement("General");

                writer.WriteElementString("AutoSave", AutoSave.ToString());
                writer.WriteElementString("AutoSaveSpeed", AutoSaveSpeed.ToString());
                writer.WriteElementString("AutoScroll", AutoScroll.ToString());
                writer.WriteElementString("Music", Music.ToString());
                writer.WriteElementString("NpcBar", NpcBar.ToString());
                writer.WriteElementString("NpcDamage", NpcDamage.ToString());
                writer.WriteElementString("NpcName", NpcName.ToString());
                writer.WriteElementString("PlayerBar", PlayerBar.ToString());
                writer.WriteElementString("PlayerDamage", PlayerDamage.ToString());
                writer.WriteElementString("PlayerName", PlayerName.ToString());
                writer.WriteElementString("SavedAccount", SavedAccount);
                writer.WriteElementString("SavedPassword", SavedPassword);
                writer.WriteElementString("Sound", Sound.ToString());
                writer.WriteElementString("SpeechBubbles", SpeechBubbles.ToString());
                writer.WriteElementString("Timestamps", Timestamps.ToString());
                writer.WriteElementString("ActiveSkin", ActiveSkin);

                writer.WriteEndElement();
                writer.WriteStartElement("ConnectionInfo");

                writer.WriteElementString("Port", ConnectionPort.ToString());
                writer.WriteElementString("Server", ConnectionIP);
                writer.WriteElementString("UpdateLink", UpdateAddress);
                writer.WriteElementString("SFXLink", SoundAddress);
                writer.WriteElementString("MusicLink", MusicAddress);

                writer.WriteEndElement();
                writer.WriteStartElement("Editor");

                writer.WriteElementString("MapGrid", MapGrid.ToString());
                writer.WriteElementString("DisplayAttributes", DisplayAttributes.ToString());
                writer.WriteElementString("DragAndPlace", DragAndPlace.ToString());

                writer.WriteEndElement();

                writer.WriteEndElement();
                writer.WriteEndDocument();
            }
        }

        public static void UpdateActiveOptions() {
            if (Music == false) {
                Logic.Music.Music.AudioPlayer.StopMusic();
            } else {
                if (Maps.MapHelper.ActiveMap != null) {
                    //Logic.Music.Music.AudioPlayer.PlayMusic(Maps.MapHelper.ActiveMap.Music);
                    ((Client.Logic.Music.Bass.BassAudioPlayer)Logic.Music.Music.AudioPlayer).FadeToNext(Maps.MapHelper.ActiveMap.Music, 1000);
                }
            }
        }

        #endregion Methods
    }
}