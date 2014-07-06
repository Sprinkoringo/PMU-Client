namespace Client.Logic.Skins
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class MainMenu : Core.ISkinCore
    {
        #region Fields

        IO.XmlEditor Xml;

        #endregion Fields

        #region Properties

        public Core.LabelTheme AccountSettingsLabel
        {
            get; set;
        }

        public Core.LabelTheme CreditsLabel
        {
            get; set;
        }

        public Core.LabelTheme ExitLabel
        {
            get; set;
        }

        public Core.LabelTheme HelpLabel
        {
            get; set;
        }

        public Core.LabelTheme LoginLabel
        {
            get; set;
        }

        public Core.LabelTheme NewAccountLabel
        {
            get; set;
        }

        public Core.WindowTheme WindowTheme
        {
            get; set;
        }

        #endregion Properties

        #region Methods

        public void Init(string filePath)
        {
            if (IO.IO.DirExists(System.IO.Path.GetDirectoryName(filePath)) == false) {
                IO.IO.CreateDir(System.IO.Path.GetDirectoryName(filePath));
            }
            Xml = new Client.Logic.IO.XmlEditor(filePath, "Data");
            if (IO.IO.FileExists(filePath) == false) {
                InitDefaultSkin();
                Xml.ReloadDocument();
                SaveToXml();
            } else {
                Xml.ReloadDocument();
                LoadFromXml();
            }
        }

        public void InitDefaultSkin()
        {
            WindowTheme = new Client.Logic.Skins.Core.WindowTheme();
            WindowTheme.BorderColor = Color.SkyBlue;

            LoginLabel = new Client.Logic.Skins.Core.LabelTheme();
            LoginLabel.Forecolor = Color.Black;
            LoginLabel.Backcolor = Color.Transparent;

            NewAccountLabel = new Client.Logic.Skins.Core.LabelTheme();
            NewAccountLabel.Forecolor = Color.Black;
            NewAccountLabel.Backcolor = Color.Transparent;

            AccountSettingsLabel = new Client.Logic.Skins.Core.LabelTheme();
            AccountSettingsLabel.Forecolor = Color.Black;
            AccountSettingsLabel.Backcolor = Color.Transparent;

            HelpLabel = new Client.Logic.Skins.Core.LabelTheme();
            HelpLabel.Forecolor = Color.Black;
            HelpLabel.Backcolor = Color.Transparent;

            CreditsLabel = new Client.Logic.Skins.Core.LabelTheme();
            CreditsLabel.Forecolor = Color.Black;
            CreditsLabel.Backcolor = Color.Transparent;

            ExitLabel = new Client.Logic.Skins.Core.LabelTheme();
            ExitLabel.Forecolor = Color.Black;
            ExitLabel.Backcolor = Color.Transparent;
        }

        public void LoadFromXml()
        {
            WindowTheme = new Client.Logic.Skins.Core.WindowTheme();
            WindowTheme.LoadFromXml(Xml, "Window");

            LoginLabel = new Client.Logic.Skins.Core.LabelTheme();
            LoginLabel.LoadFromXml(Xml, "LoginButton");
            NewAccountLabel = new Client.Logic.Skins.Core.LabelTheme();
            NewAccountLabel.LoadFromXml(Xml, "NewAccountButton");
            AccountSettingsLabel = new Client.Logic.Skins.Core.LabelTheme();
            AccountSettingsLabel.LoadFromXml(Xml, "AccountSettingsButton");
            HelpLabel = new Client.Logic.Skins.Core.LabelTheme();
            HelpLabel.LoadFromXml(Xml, "HelpButton");
            CreditsLabel = new Client.Logic.Skins.Core.LabelTheme();
            CreditsLabel.LoadFromXml(Xml, "CreditsButton");
            ExitLabel = new Client.Logic.Skins.Core.LabelTheme();
            ExitLabel.LoadFromXml(Xml, "ExitButton");
        }

        public void SaveToXml()
        {
            WindowTheme.SaveToXml(Xml, "Window");

            LoginLabel.SaveToXml(Xml, "LoginButton");
            NewAccountLabel.SaveToXml(Xml, "NewAccountButton");
            AccountSettingsLabel.SaveToXml(Xml, "AccountSettingsButton");
            HelpLabel.SaveToXml(Xml, "HelpButton");
            CreditsLabel.SaveToXml(Xml, "CreditsButton");
            ExitLabel.SaveToXml(Xml, "ExitButton");

            Xml.Save();
        }

        #endregion Methods
    }
}