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
using SdlDotNet.Graphics;

namespace Client.Logic.Graphics
{
    class GraphicsCache
    {
        public static Surface MenuHorizontal { get { return menuHorizontal; } }
        static Surface menuHorizontal;
        public static Surface MenuHorizontalFill { get { return menuHorizontalFill; } }
        static Surface menuHorizontalFill;
        public static Surface MenuHorizontalBorder { get { return menuHorizontalBorder; } }
        static Surface menuHorizontalBorder;

        public static Surface MenuVertical { get { return menuVertical; } }
        static Surface menuVertical;
        public static Surface MenuVerticalFill { get { return menuVerticalFill; } }
        static Surface menuVerticalFill;
        public static Surface MenuVerticalBorder { get { return menuVerticalBorder; } }
        static Surface menuVerticalBorder;

        public static void LoadCache() {
            menuHorizontal = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-horizontal.png", false);
            menuHorizontalFill = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-horizontal-fill.png", false);
            menuHorizontalBorder = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-horizontal-border.png", false);
            menuVertical = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-vertical.png", false);
            menuVerticalFill = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-vertical-fill.png", false);
            menuVerticalBorder = Skins.SkinManager.LoadGuiElement("General\\Menus", "menu-vertical-border.png", false);
        }
    }
}
