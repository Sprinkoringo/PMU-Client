using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.Widgets
{
    class TilesetViewer : ContainerWidget
    {
        #region Fields

        Logic.Graphics.Tileset activeTilesetSurf;
        HScrollBar hScroll;
        Point selectedTile;
        Point endTile;
        VScrollBar vScroll;

        #endregion Fields

        #region Constructors

        public TilesetViewer(string name)
            : base(name) {
            base.BackColor = Color.White;

            vScroll = new VScrollBar("vScroll");
            vScroll.Visible = false;
            vScroll.BackColor = Color.Transparent;
            vScroll.ValueChanged += new EventHandler<ValueChangedEventArgs>(vScroll_ValueChanged);

            hScroll = new HScrollBar("hScroll");
            hScroll.Visible = false;
            hScroll.BackColor = Color.Transparent;
            hScroll.ValueChanged += new EventHandler<ValueChangedEventArgs>(hScroll_ValueChanged);

            base.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(TilesetViewer_Click);
            base.MouseEnter += new EventHandler(TilesetViewer_MouseEnter);
            base.MouseLeave += new EventHandler(TilesetViewer_MouseLeave);

            selectedTile = new Point(0, 0);

            this.AddWidget(vScroll);
            this.AddWidget(hScroll);

            base.Paint += new EventHandler(TilesetViewer_Paint);
        }

        

        void TilesetViewer_MouseLeave(object sender, EventArgs e) {
            this.Width = originalWidth;
            RecalculateScrollBars();
            RequestRedraw();
            if ((selectedTile.X + 1) * 32 > this.Width) {
                int value = selectedTile.X - 1;
                hScroll.Value = value;
            }
        }

        int originalWidth;
        void TilesetViewer_MouseEnter(object sender, EventArgs e) {
            originalWidth = this.Width;
            this.Size = new Size(this.ActiveTilesetSurface.Size.Width + 32, this.Height);
            //this.Width = this.ActiveTilesetSurface.Size.Width + 32;
            //RecalculateScrollBars();
            //RequestRedraw();
        }

        #endregion Constructors

        #region Properties

        public Logic.Graphics.Tileset ActiveTilesetSurface {
            get { return activeTilesetSurf; }
            set {
                activeTilesetSurf = value;
                RecalculateScrollBars();
                RequestRedraw();
            }
        }

        public Point SelectedTile {
            get { return selectedTile; }
            set {
                selectedTile = value;
                RequestRedraw();
            }
        }

        public Point EndTile
        {
            get { return endTile; }
            set
            {
                endTile = value;
                RequestRedraw();
            }
        }

        private new Size Size {
            get { return base.Size; }
            set {
                base.Size = value;
                RecalculateScrollBars();
                RequestRedraw();
            }
        }

        #endregion Properties

        #region Methods

        public override void FreeResources() {
            base.FreeResources();
        }

        public override void OnMouseDown(SdlDotNet.Widgets.MouseButtonEventArgs e) {
            base.OnMouseDown(e);
        }

        public override void OnMouseMotion(SdlDotNet.Input.MouseMotionEventArgs e) {
            base.OnMouseMotion(e);
        }

        public override void OnMouseUp(SdlDotNet.Widgets.MouseButtonEventArgs e) {
            base.OnMouseUp(e);
        }

        public override void OnTick(SdlDotNet.Core.TickEventArgs e) {
            base.OnTick(e);
            if (vScroll != null) {
                vScroll.OnTick(e);
            }
            if (hScroll != null) {
                hScroll.OnTick(e);
            }
        }

        void TilesetViewer_Paint(object sender, EventArgs e) {
            if (vScroll != null && hScroll != null) {
                vScroll.BlitToScreen(base.Buffer);
                hScroll.BlitToScreen(base.Buffer);
                DrawTiles();
                SdlDotNet.Graphics.Primitives.Box box = new SdlDotNet.Graphics.Primitives.Box(new Point((selectedTile.X - hScroll.Value) * Constants.TILE_WIDTH, (selectedTile.Y - vScroll.Value) * Constants.TILE_HEIGHT), new Size(Constants.TILE_WIDTH * (1 + endTile.X - selectedTile.X), Constants.TILE_HEIGHT * (1 + endTile.Y - selectedTile.Y)));
                base.Buffer.Draw(box, Color.Red);
                base.DrawBorder();
                if (ParentContainer != null) {
                    ParentContainer.RequestRedraw();
                }
            }
        }

        private int DetermineTileNumber(int x, int y) {
            return (y * (activeTilesetSurf.Size.Width / Constants.TILE_WIDTH) + x);
        }

        private void DrawTiles() {
            if (vScroll != null && hScroll != null & activeTilesetSurf != null) {
                int maxTilesX = System.Math.Min(activeTilesetSurf.Size.Width, (this.Width - vScroll.Width)) / Constants.TILE_WIDTH;
                int maxTilesY = System.Math.Min(activeTilesetSurf.Size.Height, (this.Height - hScroll.Height)) / Constants.TILE_HEIGHT;
                int startX = hScroll.Value;
                int startY = vScroll.Value;
                for (int y = startY; y < maxTilesY + startY; y++) {
                    for (int x = startX; x < maxTilesX + startX; x++) {
                        int num = DetermineTileNumber(x, y);
                        if (num < activeTilesetSurf.TileCount) {
                            base.Buffer.Blit(activeTilesetSurf[num], new Point((x - startX) * Constants.TILE_WIDTH, (y - startY) * Constants.TILE_HEIGHT));
                        }
                    }
                }
            }
        }

        private void RecalculateScrollBars() {
            vScroll.Size = new System.Drawing.Size(12, this.Height - vScroll.ButtonHeight);
            vScroll.Location = new Point(this.Width - vScroll.Width, 0);
            hScroll.Size = new System.Drawing.Size(this.Width - hScroll.ButtonWidth, 12);
            hScroll.Location = new Point(0, this.Height - hScroll.Height);
            if (vScroll != null) {
                if (activeTilesetSurf.Size.Height > this.Height - hScroll.Height) {
                    vScroll.Visible = true;
                    vScroll.Minimum = 0;
                } else {
                    vScroll.Visible = false;
                }
                vScroll.Maximum = System.Math.Max(vScroll.Minimum, (activeTilesetSurf.Size.Height / Constants.TILE_HEIGHT) - (this.Height / Constants.TILE_HEIGHT));
            }
            if (hScroll != null) {
                if (activeTilesetSurf.Size.Width > this.Width - vScroll.Width) {
                    hScroll.Visible = true;
                    hScroll.Minimum = 0;
                } else {
                    hScroll.Visible = false;
                }
                hScroll.Maximum = System.Math.Max(hScroll.Minimum, (activeTilesetSurf.Size.Width / Constants.TILE_WIDTH) - (this.Width / Constants.TILE_WIDTH));
            }
        }

        void TilesetViewer_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e) {
            //Point location = this.ScreenLocation;
            //Point relPoint = new Point(e.Position.X - location.X, e.Position.Y - location.Y);
            if (e.MouseEventArgs.Button == SdlDotNet.Input.MouseButton.PrimaryButton)
            {
                if (!DrawingSupport.PointInBounds(e.RelativePosition, vScroll.Bounds) && !DrawingSupport.PointInBounds(e.RelativePosition, hScroll.Bounds))
                {
                    if (e.RelativePosition.X + (hScroll.Value * Constants.TILE_WIDTH) > activeTilesetSurf.Size.Width)
                    {
                        selectedTile.X = (activeTilesetSurf.Size.Width / 32) - 1;
                        endTile.X = selectedTile.X;
                    }
                    else
                    {
                        selectedTile.X = (e.RelativePosition.X / 32) + hScroll.Value;
                        endTile.X = selectedTile.X;
                    }
                    if (e.RelativePosition.Y + (vScroll.Value * Constants.TILE_HEIGHT) > activeTilesetSurf.Size.Height)
                    {
                        selectedTile.Y = (activeTilesetSurf.Size.Height / 32) - 1;
                        endTile.Y = selectedTile.Y;
                    }
                    else
                    {
                        selectedTile.Y = (e.RelativePosition.Y / 32) + vScroll.Value;
                        endTile.Y = selectedTile.Y;
                    }
                }
                RequestRedraw();
            }
            else if (e.MouseEventArgs.Button == SdlDotNet.Input.MouseButton.SecondaryButton)
            {
                if (!DrawingSupport.PointInBounds(e.RelativePosition, vScroll.Bounds) && !DrawingSupport.PointInBounds(e.RelativePosition, hScroll.Bounds))
                {
                    if (e.RelativePosition.X + (hScroll.Value * Constants.TILE_WIDTH) > activeTilesetSurf.Size.Width)
                    {
                        endTile.X = (activeTilesetSurf.Size.Width / 32) - 1;
                    }
                    else
                    {
                        endTile.X = (e.RelativePosition.X / 32) + hScroll.Value;
                    }
                    if (e.RelativePosition.Y + (vScroll.Value * Constants.TILE_HEIGHT) > activeTilesetSurf.Size.Height)
                    {
                        endTile.Y = (activeTilesetSurf.Size.Height / 32) - 1;
                    }
                    else
                    {
                        endTile.Y = (e.RelativePosition.Y / 32) + vScroll.Value;
                    }
                    if (DetermineTileNumber(endTile.X, endTile.Y) < DetermineTileNumber(selectedTile.X, selectedTile.Y))
                    {
                        endTile = selectedTile;
                    }
                }
                RequestRedraw();
            }
        }

        void hScroll_ValueChanged(object sender, ValueChangedEventArgs e) {
            RequestRedraw();
        }

        void vScroll_ValueChanged(object sender, ValueChangedEventArgs e) {
            RequestRedraw();
        }

        #endregion Methods
    }
}
