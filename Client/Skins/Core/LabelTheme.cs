namespace Client.Logic.Skins.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class LabelTheme : SkinCore
    {
        #region Fields

        public Color Backcolor;
        public Color Forecolor;

        #endregion Fields

        #region Methods

        public void LoadFromXml(IO.XmlEditor xml, string node)
        {
            Forecolor = GetColorFromXml(xml, node, "Forecolor");
            Backcolor = GetColorFromXml(xml, node, "Backcolor");
        }

        public void SaveToXml(IO.XmlEditor xml, string node)
        {
            xml.SaveNode("Forecolor", node, GetXmlFromColor(Forecolor));
            xml.SaveNode("Backcolor", node, GetXmlFromColor(Backcolor));
        }

        #endregion Methods
    }
}