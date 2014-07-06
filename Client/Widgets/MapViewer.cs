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
using SdlDotNet.Widgets;
using System.Drawing;
using Client.Logic.Graphics.Renderers.Screen;

namespace Client.Logic.Widgets
{
    class MapViewer : AnimatedWidget
    {
        #region Fields

        SdlDotNet.Graphics.Font font;
        int lastAnimTick;

        #endregion Fields

        #region Constructors

        public MapViewer(string name)
            : base(name) {
            base.BackColor = Color.Black;
            ScreenRenderer.Initialize();
            //gl = new Graphics.GLRoutines();
            //activeMap = new Maps.Map();
            font = Graphics.FontManager.LoadFont("PMU.ttf", 24);

            this.BufferResized += new EventHandler(MapViewer_Resized);

            this.RenderFrame += new EventHandler(MapViewer_RenderFrame);
        }

        void MapViewer_Resized(object sender, EventArgs e) {
            RecreateRendererDestinationData();
        }

        Logic.Graphics.Renderers.RendererDestinationData destData;
        private void RecreateRendererDestinationData() {
            destData = new Graphics.Renderers.RendererDestinationData(base.Buffer, new Point(0, 0), this.Size);
        }

        #endregion Constructors

        #region Events

        public event EventHandler MapUpdated;

        #endregion Events

        #region Properties

        public Maps.Map ActiveMap {
            get { return Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map; }
            set {
                // Reset the camera
                //ScreenRenderer.Camera.X = 0;
                //ScreenRenderer.Camera.X2 = 20;
                //ScreenRenderer.Camera.Y = 0;
                //ScreenRenderer.Camera.Y2 = 15;
                ScreenRenderer.RenderOptions.Map = value;
                if (MapUpdated != null)
                    MapUpdated(this, null);
                if (value != null) {
                    ScreenRenderer.Camera.FocusOnSprite(Players.PlayerManager.MyPlayer);

                    // Get the camera coordinates
                    ScreenRenderer.Camera.X = ScreenRenderer.GetScreenLeft() - 1;
                    ScreenRenderer.Camera.Y = ScreenRenderer.GetScreenTop();
                    ScreenRenderer.Camera.X2 = ScreenRenderer.GetScreenRight();
                    ScreenRenderer.Camera.Y2 = ScreenRenderer.GetScreenBottom() + 1;
                    // Verify that the coordinates aren't outside the map bounds
                    if (ScreenRenderer.Camera.X < 0 && ScreenRenderer.RenderOptions.Map.Left < 1) {
                        ScreenRenderer.Camera.X = 0;
                        ScreenRenderer.Camera.X2 = 20;
                    } else if (ScreenRenderer.Camera.X2 > ScreenRenderer.RenderOptions.Map.MaxX + 1 && ScreenRenderer.RenderOptions.Map.Right < 1) {
                        ScreenRenderer.Camera.X = ScreenRenderer.RenderOptions.Map.MaxX - 19;
                        ScreenRenderer.Camera.X2 = ScreenRenderer.RenderOptions.Map.MaxX + 1;
                    }
                    if (ScreenRenderer.Camera.Y < 0 && ScreenRenderer.RenderOptions.Map.Up < 1) {
                        ScreenRenderer.Camera.Y = 0;
                        ScreenRenderer.Camera.Y2 = 15;
                    } else if (ScreenRenderer.Camera.Y2 > ScreenRenderer.RenderOptions.Map.MaxY + 1 && ScreenRenderer.RenderOptions.Map.Down < 1) {
                        ScreenRenderer.Camera.Y = ScreenRenderer.RenderOptions.Map.MaxY - 14;
                        ScreenRenderer.Camera.Y2 = ScreenRenderer.RenderOptions.Map.MaxY + 1;
                    }
                }
            }
        }

        #endregion Properties

        #region Methods

        

        public override void FreeResources() {
            base.FreeResources();
        }

        public override void OnTick(SdlDotNet.Core.TickEventArgs e) {
            if (e.Tick > lastAnimTick + 250) {
                Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayAnimation = !Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayAnimation;
                lastAnimTick = e.Tick;
            }
            if (Globals.Tick > ((Client.Logic.Music.Bass.BassAudioPlayer)Music.Music.AudioPlayer).TimeOfNextSong
                && ((Client.Logic.Music.Bass.BassAudioPlayer)Music.Music.AudioPlayer).TimeOfNextSong > 0) {
                ((Client.Logic.Music.Bass.BassAudioPlayer)Music.Music.AudioPlayer).PlayNextMusic();
            }
            base.OnTick(e);
            RequestRedraw();
        }

        void MapViewer_RenderFrame(object sender, EventArgs e) {
            try {
                if (Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map != null && Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map.Loaded) {
                    //Graphics.Renderers.Screen.ScreenRenderer.RenderScreen(base.Buffer, this.Location);
                    //gl.DrawScreen(base.Buffer, activeMap, mapAnim, this.cameraX, this.cameraX2, this.cameraY, this.cameraY2, displayAttributes, displayMapGrid, displayLocation, overlay);
                    Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderScreen(destData);
                }
            } catch (Exception ex){
                System.Diagnostics.Debug.WriteLine("Rendering:");
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                System.Diagnostics.Debug.WriteLine(ex.StackTrace);
            }
        }

        //protected override void DrawBuffer() {
        //    base.DrawBuffer();
        //    if (Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map != null && Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map.Loaded) {
        //        //Graphics.Renderers.Screen.ScreenRenderer.RenderScreen(base.Buffer, this.Location);
        //        //gl.DrawScreen(base.Buffer, activeMap, mapAnim, this.cameraX, this.cameraX2, this.cameraY, this.cameraY2, displayAttributes, displayMapGrid, displayLocation, overlay);
        //        Logic.Graphics.Renderers.Screen.ScreenRenderer.RenderScreen(destData);
        //    }
        //    if (font != null) {
        //        //if (IO.Settings.DisplayCursorLocation) {
        //        //    fpsY += 27;
        //        //    DrawText("X: " + (Globals.CursorX + 1).ToString() + " Y: " + (Globals.CursorY + 1).ToString(), Color.Red, 5, 0);
        //        //}
        //        //if (IO.Settings.DisplayFPS) {
        //        //    DrawText("FPS: " + SdlDotNet.Core.Events.Fps.ToString(), Color.Red, 5, fpsY);
        //        //}
        //    }
        //    base.DrawBorder();
        //    base.DrawComplete();
        //}

        public SdlDotNet.Graphics.Surface CaptureMapImage(bool captureVisibleArea, bool captureAttributes, bool captureMapGrid) {
            int cameraX;
            int cameraX2;
            int cameraY;
            int cameraY2;
            if (captureVisibleArea) {
                cameraX = ScreenRenderer.Camera.X;
                cameraX2 = ScreenRenderer.Camera.X2;
                cameraY = ScreenRenderer.Camera.Y;
                cameraY2 = ScreenRenderer.Camera.Y2;
            } else {
                cameraX = 0;
                cameraX2 = Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map.MaxX;
                cameraY = 0;
                cameraY2 = Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map.MaxY;
            }
            SdlDotNet.Graphics.Surface screenshotSurf = new SdlDotNet.Graphics.Surface((cameraX2 - cameraX) * Constants.TILE_WIDTH, (cameraY2 - cameraY) * Constants.TILE_HEIGHT);
            screenshotSurf.Fill(Color.White);
            Client.Logic.Graphics.Renderers.Maps.MapRenderer.DrawTiles(new Logic.Graphics.Renderers.RendererDestinationData(screenshotSurf, new Point(0, 0), screenshotSurf.Size), Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.Map, Graphics.Renderers.Screen.ScreenRenderer.RenderOptions.DisplayAnimation, cameraX, cameraX2, cameraY, cameraY2, captureAttributes, captureMapGrid);
            return screenshotSurf;
        }



        #endregion Methods
    }
}
