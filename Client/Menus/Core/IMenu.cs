using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;

namespace Client.Logic.Menus.Core
{
    interface IMenu
    {
        Widgets.BorderedPanel MenuPanel { get; }
        bool Modal { get; set; }
    }
}
