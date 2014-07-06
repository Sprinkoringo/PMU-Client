namespace Client.Logic.Graphics.Renderers.Sprites
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Graphics;
    using SdlDotNet.Widgets;

    class SpeechBubble
    {
        #region Fields

        int bubbleDisplayStart;
        string bubbleText;
        Surface buffer;
        SdlDotNet.Graphics.Font font;
        bool markedForRemoval;

        #endregion Fields

        #region Constructors

        public SpeechBubble() {

        }

        #endregion Constructors

        #region Properties

        public bool RedrawRequested {
            get;
            set;
        }

        public int BubbleDisplayStart {
            get { return bubbleDisplayStart; }
            set { bubbleDisplayStart = value; }
        }
        
        public Surface Buffer {
            get { return buffer; }
        }

        public bool MarkedForRemoval {
            get { return markedForRemoval; }
        }

        #endregion Properties

        #region Methods

        public void DrawBuffer() {
            Surface textSurf;
            Size textSize = SdlDotNet.Widgets.TextRenderer.SizeText2(font, bubbleText, false, 0);
            CharRenderOptions[] renderOptions = new CharRenderOptions[bubbleText.Length];
            for (int i = 0; i < renderOptions.Length; i++) {
                renderOptions[i] = new CharRenderOptions(Color.WhiteSmoke);
            }
            renderOptions = Network.MessageProcessor.ParseText(renderOptions, ref bubbleText);
            if (textSize.Width > 300) {
                textSurf = SdlDotNet.Widgets.TextRenderer.RenderTextBasic2(font, bubbleText, renderOptions, Color.WhiteSmoke, false, 300, 0, 0, 0);
            } else {
                textSurf = SdlDotNet.Widgets.TextRenderer.RenderTextBasic2(font, bubbleText, renderOptions, Color.WhiteSmoke, false, 0, 0, 0, 0);
            }
            int tilesWidth = System.Math.Max(textSurf.Width / Constants.TILE_WIDTH, 2);
            int tilesHeight = System.Math.Max(textSurf.Height / Constants.TILE_HEIGHT, 2);

            if (textSurf.Width > tilesWidth * Constants.TILE_WIDTH) {
                tilesWidth++;
            }
            if (textSurf.Height > tilesHeight * Constants.TILE_HEIGHT * 0.7) {
                tilesHeight++;
            }
            if (buffer != null) {
                buffer.Close();
            }
            buffer = new Surface(new Size((tilesWidth * Constants.TILE_WIDTH), (tilesHeight * Constants.TILE_HEIGHT)));
            buffer.Fill(Color.Transparent);
            buffer.TransparentColor = Color.Transparent;
            buffer.Transparent = true;
            for (int i = 0; i < tilesHeight; i++) {
                if (i == 0) {
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 1, 0, 0);
                    for (int n = 0; n < tilesWidth - 2; n++) {
                        Maps.MapRenderer.DrawTileToSurface(buffer, 10, 16, n + 1, 0);
                    }
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 2, tilesWidth - 1, 0);
                } else if (i == tilesHeight - 1) {
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 3, 0, tilesHeight - 1);
                    for (int n = 0; n < tilesWidth - 2; n++) {
                        Maps.MapRenderer.DrawTileToSurface(buffer, 10, 15, n + 1, tilesHeight - 1);
                    }
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 4, tilesWidth - 1, tilesHeight - 1);
                } else {
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 18, 0, i);
                    for (int n = 0; n < tilesWidth - 2; n++) {
                        Maps.MapRenderer.DrawTileToSurface(buffer, 10, 5, n + 1, i);
                    }
                    Maps.MapRenderer.DrawTileToSurface(buffer, 10, 17, tilesWidth - 1, i);
                }
            }
            buffer = buffer.CreateScaledSurface(1, 0.7);
            buffer.Transparent = true;
            buffer.Blit(textSurf, new Point(Logic.Graphics.DrawingSupport.GetCenterX(buffer.Width, textSurf.Width), 0));
            textSurf.Close();
            RedrawRequested = false;
        }

        public void FreeResources() {
            if (buffer != null) {
                buffer.Close();
                buffer = null;
            }
            if (font != null) {
                font.Close();
                font = null;
            }
        }

        public void Process(int tick) {
            if (tick > bubbleDisplayStart + 2000) {
                markedForRemoval = true;
            }
        }

        public void SetBubbleText(string text) {
            CheckFont();
            bubbleText = text;
            RedrawRequested = true;
            //DrawBuffer();
        }

        private void CheckFont() {
            if (font == null) {
                font = FontManager.LoadFont("PMU", 24);
            }
        }

        #endregion Methods
    }
}