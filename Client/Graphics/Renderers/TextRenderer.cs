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
