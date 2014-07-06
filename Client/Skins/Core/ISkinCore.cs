namespace Client.Logic.Skins.Core
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Basic interface for skin classes.
    /// </summary>
    public interface ISkinCore
    {
        #region Methods

        /// <summary>
        /// Initializes the skin using specified file path.
        /// </summary>
        /// <param name="filePath">The file path.</param>
        void Init(string filePath);

        /// <summary>
        /// Initializes the default skin.
        /// </summary>
        void InitDefaultSkin();

        /// <summary>
        /// Loads skin data from the XML file.
        /// </summary>
        void LoadFromXml();

        /// <summary>
        /// Saves skin data to the XML file.
        /// </summary>
        void SaveToXml();

        #endregion Methods
    }
}