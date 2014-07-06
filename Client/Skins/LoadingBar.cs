namespace Client.Logic.Skins
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class LoadingBar : Core.ISkinCore
    {
        #region Fields

        IO.XmlEditor Xml;

        #endregion Fields

        #region Properties

        public Core.LabelTheme LoadingLabel
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

            LoadingLabel = new Client.Logic.Skins.Core.LabelTheme();
            LoadingLabel.Forecolor = Color.Black;
            LoadingLabel.Backcolor = Color.Transparent;
        }

        public void LoadFromXml()
        {
            WindowTheme = new Client.Logic.Skins.Core.WindowTheme();
            WindowTheme.LoadFromXml(Xml, "Window");

            LoadingLabel = new Client.Logic.Skins.Core.LabelTheme();
            LoadingLabel.LoadFromXml(Xml, "LoadingLabel");
        }

        public void SaveToXml()
        {
            WindowTheme.SaveToXml(Xml, "Window");

            LoadingLabel.SaveToXml(Xml, "LoadingLabel");

            Xml.Save();
        }

        #endregion Methods
    }
}