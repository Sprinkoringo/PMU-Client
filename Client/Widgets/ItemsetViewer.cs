using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.Widgets
{
    class ItemsetViewer : ContainerWidget
    {
        #region Fields

        SdlDotNet.Graphics.Surface activeItemSurf;
        HScrollBar hScroll;
        Point selectedTile;
        VScrollBar vScroll;

        #endregion Fields

        #region Constructors

        public ItemsetViewer(string name)
            : base(name)
        {
            base.BackColor = Color.Black;

            vScroll = new VScrollBar("vScroll");
            vScroll.Visible = false;
            vScroll.BackColor = Color.Transparent;
            vScroll.ValueChanged += new EventHandler<ValueChangedEventArgs>(vScroll_ValueChanged);

            hScroll = new HScrollBar("hScroll");
            hScroll.Visible = false;
            hScroll.BackColor = Color.Transparent;
            hScroll.ValueChanged += new EventHandler<ValueChangedEventArgs>(hScroll_ValueChanged);

            base.Click += new EventHandler<SdlDotNet.Widgets.MouseButtonEventArgs>(ItemsetViewer_Click);

            selectedTile = new Point(0, 0);

            this.AddWidget(vScroll);
            this.AddWidget(hScroll);

            base.Paint += new EventHandler(ItemsetViewer_Paint);
        }

       

        #endregion Constructors

        #region Properties

        public SdlDotNet.Graphics.Surface ActiveItemSurface
        {
            get { return activeItemSurf; }
            set
            {
                activeItemSurf = value;
                RecalculateScrollBars();
            }
        }
        
        public Point SelectedTile
        {
            get { return selectedTile; }
            set
            {
                selectedTile = value;
                RequestRedraw();
            }
        }

        private new Size Size
        {
            get { return base.Size; }
            set
            {
                base.Size = value;
                RecalculateScrollBars();
            }
        }

        #endregion Properties

        #region Methods

        public override void FreeResources()
        {
            base.FreeResources();
        }

        public override void OnMouseDown(SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            base.OnMouseDown(e);
        }

        public override void OnMouseMotion(SdlDotNet.Input.MouseMotionEventArgs e)
        {
            base.OnMouseMotion(e);
        }

        public override void OnMouseUp(SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            base.OnMouseUp(e);
        }

        public override void OnTick(SdlDotNet.Core.TickEventArgs e)
        {
            base.OnTick(e);
            if (vScroll != null)
            {
                vScroll.OnTick(e);
            }
            if (hScroll != null)
            {
                hScroll.OnTick(e);
            }
        }

        void ItemsetViewer_Paint(object sender, EventArgs e) {
            if (vScroll != null && hScroll != null) {
                vScroll.BlitToScreen(base.Buffer);
                hScroll.BlitToScreen(base.Buffer);
                DrawTiles();
                SdlDotNet.Graphics.Primitives.Box box = new SdlDotNet.Graphics.Primitives.Box(new Point((selectedTile.X - hScroll.Value) * Constants.TILE_WIDTH, (selectedTile.Y - vScroll.Value) * Constants.TILE_HEIGHT), new Size(Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                base.Buffer.Draw(box, Color.Red);
                base.DrawBorder();
            }
        }

        public int DetermineTileNumber(int x, int y)
        {
            return (y * (activeItemSurf.Size.Width / Constants.TILE_WIDTH) + x);
        }

        private void DrawTiles()
        {
            if (vScroll != null && hScroll != null & activeItemSurf != null)
            {
                int maxTilesX = System.Math.Min(activeItemSurf.Size.Width, (this.Width - vScroll.Width)) / Constants.TILE_WIDTH;
                int maxTilesY = System.Math.Min(activeItemSurf.Size.Height, (this.Height - hScroll.Height)) / Constants.TILE_HEIGHT;
                int startX = hScroll.Value;
                int startY = vScroll.Value;
                
                for (int y = startY; y < maxTilesY + startY; y++)
                {
                    for (int x = startX; x < maxTilesX + startX; x++)
                    {
                        //int num = DetermineTileNumber(x, y);
                        //if (x < (activeItemSurf.Size.Width / Constants.TILE_WIDTH) && y < (activeItemSurf.Size.Height / Constants.TILE_HEIGHT))
                        //{
                        SdlDotNet.Graphics.Surface tile = new SdlDotNet.Graphics.Surface(Constants.TILE_WIDTH, Constants.TILE_HEIGHT);
                        tile.Blit(activeItemSurf, new Point(0, 0), new Rectangle(x * Constants.TILE_WIDTH, y * Constants.TILE_HEIGHT, Constants.TILE_WIDTH, Constants.TILE_HEIGHT));
                        base.Buffer.Blit(tile, new Point((x - startX) * Constants.TILE_WIDTH, (y - startY) * Constants.TILE_HEIGHT));
                        //}
                    }
                }
            }
        }

        private void RecalculateScrollBars()
        {
            vScroll.Size = new System.Drawing.Size(12, this.Height - vScroll.ButtonHeight);
            vScroll.Location = new Point(this.Width - vScroll.Width, 0);
            hScroll.Size = new System.Drawing.Size(this.Width - hScroll.ButtonWidth, 12);
            hScroll.Location = new Point(0, this.Height - hScroll.Height);
            if (vScroll != null)
            {
                if (activeItemSurf.Size.Height > this.Height - hScroll.Height)
                {
                    vScroll.Visible = true;
                    vScroll.Minimum = 0;
                }
                else
                {
                    vScroll.Visible = false;
                }
                vScroll.Maximum = System.Math.Max(vScroll.Minimum, (activeItemSurf.Size.Height / Constants.TILE_HEIGHT) - (this.Height / Constants.TILE_HEIGHT));
            }
            if (hScroll != null)
            {
                if (activeItemSurf.Size.Width > this.Width - vScroll.Width)
                {
                    hScroll.Visible = true;
                    hScroll.Minimum = 0;
                }
                else
                {
                    hScroll.Visible = false;
                }
                hScroll.Maximum = System.Math.Max(hScroll.Minimum, (activeItemSurf.Size.Width / Constants.TILE_WIDTH) - (this.Width / Constants.TILE_WIDTH));
            }
            RequestRedraw();
        }

        void ItemsetViewer_Click(object sender, SdlDotNet.Widgets.MouseButtonEventArgs e)
        {
            Point location = this.Location;
            Point relPoint = new Point(e.Position.X - location.X, e.Position.Y - location.Y);
            if (!DrawingSupport.PointInBounds(relPoint, vScroll.Bounds) && !DrawingSupport.PointInBounds(relPoint, hScroll.Bounds))
            {
                if (relPoint.X + (hScroll.Value * Constants.TILE_WIDTH) > activeItemSurf.Size.Width)
                {
                    selectedTile.X = (activeItemSurf.Size.Width / 32) - 1;
                }
                else
                {
                    selectedTile.X = (relPoint.X / 32) + hScroll.Value;
                }
                if (relPoint.Y + (vScroll.Value * Constants.TILE_HEIGHT) > activeItemSurf.Size.Height)
                {
                    selectedTile.Y = (activeItemSurf.Size.Height / 32) - 1;
                }
                else
                {
                    selectedTile.Y = (relPoint.Y / 32) + vScroll.Value;
                }
            }
            RequestRedraw();
        }

        void hScroll_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RequestRedraw();
        }

        void vScroll_ValueChanged(object sender, ValueChangedEventArgs e)
        {
            RequestRedraw();
        }

        #endregion Methods
    }
}
