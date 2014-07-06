namespace Client.Logic.Graphics.Renderers
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Graphics;

    class RenderLocation
    {
        #region Constructors

        public RenderLocation(RendererDestinationData destinationData, int x, int y) {
            DestinationSurface = destinationData;
            DestinationPoint = new Point(x, y);
        }

        public RenderLocation(RendererDestinationData destinationData, Point destinationPoint) {
            DestinationSurface = destinationData;
            DestinationPoint = destinationPoint;
        }

        #endregion Constructors

        #region Properties

        public Point DestinationPoint {
            get;
            set;
        }

        public RendererDestinationData DestinationSurface {
            get;
            set;
        }

        #endregion Properties
    }
}