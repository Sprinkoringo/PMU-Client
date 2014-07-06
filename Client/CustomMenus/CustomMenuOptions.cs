using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Logic.CustomMenus
{
    class CustomMenuOptions
    {
        public Size Size { get; set; }
        public Point Location { get; set; }
        public string Name { get; set; }
        public bool Modal { get; set; }
    }
}
