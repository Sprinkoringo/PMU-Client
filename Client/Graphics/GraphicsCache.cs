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
