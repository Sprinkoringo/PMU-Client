namespace Client.Logic.Skins.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class WindowTheme : SkinCore
    {
        #region Fields

        public Color BorderColor;

        #endregion Fields

        #region Methods

        public void LoadFromXml(IO.XmlEditor xml, string node)
        {
            BorderColor = GetColorFromXml(xml, node, "Bordercolor");
        }

        public void SaveToXml(IO.XmlEditor xml, string node)
        {
            xml.SaveNode("Bordercolor", node, GetXmlFromColor(BorderColor));
        }

        #endregion Methods
    }
}