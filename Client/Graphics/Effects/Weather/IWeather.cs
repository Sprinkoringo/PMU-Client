using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Graphics.Effects.Weather
{
    interface IWeather : Overlays.IOverlay
    {
        Enums.Weather ID { get; }
    }
}
