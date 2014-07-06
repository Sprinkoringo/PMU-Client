namespace Client.Logic.Skins.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    class ButtonTheme : SkinCore
    {
        #region Fields

        public Color BackColor;
        public Color BorderColor;
        public Color ForeColor;
        public Color HoverColor;

        #endregion Fields

        #region Methods

        public void LoadFromXml(IO.XmlEditor xml, string node)
        {
            ForeColor = GetColorFromXml(xml, node, "Forecolor");
            BackColor = GetColorFromXml(xml, node, "Backcolor");
            BorderColor = GetColorFromXml(xml, node, "Bordercolor");
            HoverColor = GetColorFromXml(xml, node, "Hovercolor");
        }

        public void SaveToXml(IO.XmlEditor xml, string node)
        {
            xml.SaveNode("Forecolor", node, GetXmlFromColor(ForeColor));
            xml.SaveNode("Backcolor", node, GetXmlFromColor(BackColor));
            xml.SaveNode("Bordercolor", node, GetXmlFromColor(BorderColor));
            xml.SaveNode("Hovercolor", node, GetXmlFromColor(HoverColor));
        }

        #endregion Methods
    }
}