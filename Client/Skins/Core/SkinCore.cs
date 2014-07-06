namespace Client.Logic.Skins.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    /// <summary>
    /// Core class for creating skins. Provides
    /// </summary>
    public abstract class SkinCore
    {
        #region Methods

        /// <summary>
        /// Gets the color from a XML node.
        /// </summary>
        /// <param name="xml">The XML Editor instance.</param>
        /// <param name="node">The node.</param>
        /// <param name="key">The key.</param>
        /// <returns></returns>
        protected Color GetColorFromXml(IO.XmlEditor xml, string node, string key)
        {
            return Color.FromArgb(xml.TryGetAttributeValue(key, node, "A").ToInt(),
                xml.TryGetAttributeValue(key, node, "R").ToInt(),
                xml.TryGetAttributeValue(key, node, "G").ToInt(),
                xml.TryGetAttributeValue(key, node, "B").ToInt());
        }

        /// <summary>
        /// Converts a color to a XML node.
        /// </summary>
        /// <param name="color">The color.</param>
        /// <returns></returns>
        protected IO.XmlAttributes GetXmlFromColor(Color color)
        {
            IO.XmlAttributes attrib = new Client.Logic.IO.XmlAttributes();
            attrib.Add("A", color.A.ToString());
            attrib.Add("R", color.R.ToString());
            attrib.Add("G", color.G.ToString());
            attrib.Add("B", color.B.ToString());
            return attrib;
        }

        #endregion Methods
    }
}