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


using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;
using System.Drawing;

namespace Client.Logic.Graphics.Renderers
{
    class TextRenderer
    {
        public static SdlDotNet.Graphics.Font Font {
            get { return FontManager.GameFont; }
        }

        public static void DrawText(RendererDestinationData destData, SdlDotNet.Graphics.Font font, string text, Color textColor, Point destinationPosition) {
            Surface textSurface = font.Render(text, textColor);
            destData.Blit(textSurface, destinationPosition);
            textSurface.Close();
        }

        public static void DrawText(RendererDestinationData destData, string text, Color textColor, Point destinationPosition) {
            Surface textSurface = Logic.Graphics.FontManager.GameFont.Render(text, textColor);
            destData.Blit(textSurface, destinationPosition);
            textSurface.Close();
        }

        public static void DrawText(RendererDestinationData destData, string text, Color textColor, int destX, int destY) {
            DrawText(destData, text, textColor, new Point(destX, destY));
        }

        public static void DrawText(RendererDestinationData destData, string text, Color textColor, Color borderColor, Point destinationPosition) {
            Surface borderSurf = FontManager.GameFont.Render(text, borderColor);
            destData.Blit(borderSurf, new Point(destinationPosition.X + 1, destinationPosition.Y + 1));
            destData.Blit(borderSurf, new Point(destinationPosition.X + 2, destinationPosition.Y));
            destData.Blit(borderSurf, new Point(destinationPosition.X, destinationPosition.Y + 2));
            //destData.Blit(borderSurf, new Point(destinationPosition.X, destinationPosition.Y + 1));
            //destData.Blit(borderSurf, new Point(destinationPosition.X - 1, destinationPosition.Y));
            //destData.Blit(borderSurf, new Point(destinationPosition.X, destinationPosition.Y - 1));
            Surface textSurface = FontManager.GameFont.Render(text, textColor);
            destData.Blit(textSurface, destinationPosition);
            textSurface.Close();
            borderSurf.Close();
        }

        public static void DrawText(RendererDestinationData destData, string text, Color textColor, Color borderColor, int destX, int destY) {
            DrawText(destData, text, textColor, borderColor, new Point(destX, destY));
        }

        public static Size SizeText(string text) {
            return Font.SizeText(text);
        }
    }
}
