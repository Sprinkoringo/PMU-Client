/*The MIT License (MIT)

Copyright (c) 2014 PMU Staff

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in
all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.
*/


namespace Client.Logic
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Windows.Forms;

    /// <summary>
    /// Provides misc. methods
    /// </summary>
    public class Tools
    {
        #region Methods

        /// <summary>
        /// Displays a messagebox.
        /// </summary>
        /// <param name="caption">The caption.</param>
        /// <param name="text">The text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        public static void MessageBox(string caption, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            System.Windows.Forms.MessageBox.Show(text, caption, buttons, icon);
        }

        /// <summary>
        /// Displays a messagebox.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="icon">The icon.</param>
        public static void MessageBox(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            MessageBox("Pokemon Mystery Universe", text, buttons, icon);
        }

        /// <summary>
        /// Displays a messagebox.
        /// </summary>
        /// <param name="text">The text.</param>
        /// <param name="buttons">The buttons.</param>
        public static void MessageBox(string text, MessageBoxButtons buttons)
        {
            MessageBox("Pokemon Mystery Universe", text, buttons, MessageBoxIcon.None);
        }

        /// <summary>
        /// Displays a messagebox.
        /// </summary>
        /// <param name="text">The text.</param>
        public static void MessageBox(string text)
        {
            MessageBox("Pokemon Mystery Universe", text, MessageBoxButtons.OK, MessageBoxIcon.None);
        }
        
        /// <summary>
        /// Crops a surface from the specified surface.
        /// </summary>
        /// <param name="surfaceToCrop">The surface to crop.</param>
        /// <param name="cropRectangle">The rectangle bounds to crop.</param>
        /// <returns>The cropped surface.</returns>
        public static SdlDotNet.Graphics.Surface CropImage(SdlDotNet.Graphics.Surface surfaceToCrop, System.Drawing.Rectangle cropRectangle) {
            SdlDotNet.Graphics.Surface returnSurf = new SdlDotNet.Graphics.Surface(cropRectangle.Size);
            returnSurf.Transparent = surfaceToCrop.Transparent;
            //returnSurf.Fill(System.Drawing.Color.Transparent);
            //returnSurf.TransparentColor = surfaceToCrop.TransparentColor;
            returnSurf.Blit(surfaceToCrop, new System.Drawing.Point(0, 0), cropRectangle);
            return returnSurf;
        }
        
        /// <summary>
        /// Combines two surfaces together.
        /// </summary>
        /// <param name="bottomImage">The surface that will be used as the background.</param>
        /// <param name="topImage">The surface that will be used as the foreground.</param>
        /// <returns>The combined surface.</returns>
        public static SdlDotNet.Graphics.Surface CombineImage(SdlDotNet.Graphics.Surface bottomImage, SdlDotNet.Graphics.Surface topImage) {
            SdlDotNet.Graphics.Surface returnSurf = new SdlDotNet.Graphics.Surface(new System.Drawing.Size(System.Math.Max(bottomImage.Width, topImage.Width), System.Math.Max(bottomImage.Height, topImage.Height)));
            returnSurf.Blit(bottomImage, new System.Drawing.Point(0, 0));
            returnSurf.Blit(topImage, new System.Drawing.Point(0, 0));
            return returnSurf;
        }
        
        #endregion Methods
    }
}