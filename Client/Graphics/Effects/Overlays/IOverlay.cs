using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics.Effects.Overlays
{
    interface IOverlay
    {
        bool Disposed { get; }
        void Render(Renderers.RendererDestinationData destData, int tick);
        void FreeResources();
    }
}
