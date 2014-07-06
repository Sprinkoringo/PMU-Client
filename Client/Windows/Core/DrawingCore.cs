namespace Client.Logic.Windows.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using Client.Logic.Graphics;

    using SdlVideo = SdlDotNet.Graphics.Video;

    class DrawingCore
    {
        #region Methods

        //public void DrawPlayer() {
        //    SdlVideo.Screen.Blit(GraphicsManager.SpriteSheets, new Point(GraphicManager.PlayerSprite.X * 32, Globals.GraphicManager.PlayerSprite.Y * 32), new Rectangle(0, 0, 21, 21));
        //}
        //public void DrawTile(int sheet, int sheetX, int sheetY, int destX, int destY) {
        //    SdlVideo.Screen.Blit(GraphicManager.TileSheetss[sheet], new Point(destX, destY), new Rectangle(sheetX, sheetY, Maps.Tile.TILEX, Maps.Tile.TILEY));
        //}
        public void DrawText(string text, Color textColor, Point destinationPosition)
        {
            SdlVideo.Screen.Blit(FontManager.MainFont.Render(text, textColor), destinationPosition);
        }

        public void DrawText(string text, Color textColor, int destX, int destY)
        {
            DrawText(text, textColor, new Point(destX, destY));
        }

        public void DrawText(SdlDotNet.Graphics.Font font, string text, Color textColor, Point destinationPosition)
        {
            SdlVideo.Screen.Blit(font.Render(text, textColor), destinationPosition);
        }

        public void DrawText(SdlDotNet.Graphics.Font font, string text, Color textColor, int destX, int destY)
        {
            DrawText(font, text, textColor, new Point(destX, destY));
        }

        public void DrawText(SdlDotNet.Graphics.Font font, string text, Color textColor, Color backgroundColor, Point destinationPosition)
        {
            SdlVideo.Screen.Blit(font.Render(text, textColor, backgroundColor), destinationPosition);
        }

        public void DrawText(SdlDotNet.Graphics.Font font, string text, Color textColor, Color backgroundColor, int destX, int destY)
        {
            DrawText(font, text, textColor, backgroundColor, new Point(destX, destY));
        }

        public void DrawText(string text, Color textColor, Color backgroundColor, Point destinationPosition)
        {
            SdlVideo.Screen.Blit(FontManager.MainFont.Render(text, textColor, backgroundColor), destinationPosition);
        }

        public void DrawText(string text, Color textColor, Color backgroundColor, int destX, int destY)
        {
            DrawText(text, textColor, backgroundColor, new Point(destX, destY));
        }

        public void FadeIn(byte fadeinSpeed, GameScreen.FadeCallback callback)
        {
            GraphicsManager.FadeSurface.AlphaBlending = true;
            GraphicsManager.FadeSurface.Alpha = 255;
            GraphicsManager.FadeSurface.Fill(Color.Black);
            Globals.GameScreen.fadeSpeed = fadeinSpeed;
            Globals.GameScreen.fadeCallback = callback;
            Globals.GameScreen.fadeOut = false;
            Globals.GameScreen.isFading = true;
        }

        public void FadeOut(byte fadeoutSpeed, GameScreen.FadeCallback callback)
        {
            GraphicsManager.FadeSurface.AlphaBlending = true;
            GraphicsManager.FadeSurface.Alpha = 0;
            GraphicsManager.FadeSurface.Fill(Color.Black);
            Globals.GameScreen.fadeSpeed = fadeoutSpeed;
            Globals.GameScreen.fadeCallback = callback;
            Globals.GameScreen.fadeOut = true;
            Globals.GameScreen.isFading = true;
        }

        public Point GetCenter(SdlDotNet.Graphics.Surface mTexture, Size graphicSize)
        {
            return new Point((mTexture.Width / 2) - (graphicSize.Width / 2), (mTexture.Height / 2) - (graphicSize.Height / 2));
        }

        public Point GetCenter(Size graphicSize)
        {
            return GetCenter(SdlVideo.Screen, graphicSize);
        }

        #endregion Methods
    }
}