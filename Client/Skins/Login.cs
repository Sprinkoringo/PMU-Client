namespace Client.Logic.Skins
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    class Login : Core.ISkinCore
    {
        #region Fields

        IO.XmlEditor Xml;

        #endregion Fields

        #region Properties

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
        }

        public void LoadFromXml()
        {
        }

        public void SaveToXml()
        {
        }

        #endregion Methods
    }
}