using System;
using System.Collections.Generic;
using System.Text;

using Client.Logic.Menus.Core;

namespace Client.Logic.CustomMenus
{
    class CustomMenu : Widgets.BorderedPanel, IMenu
    {
        public Widgets.BorderedPanel MenuPanel {
            get { return this; }
        }

        public bool Modal {
            get;
            set;
        }

        CustomMenuOptions menuOptions;

        public CustomMenu(CustomMenuOptions menuOptions, SdlDotNet.Widgets.WidgetCollection widgets)
            : base(menuOptions.Name) {

            this.menuOptions = menuOptions;

            base.Size = menuOptions.Size;

            base.MenuDirection = Enums.MenuDirection.Vertical;
            Modal = menuOptions.Modal;

            for (int i = 0; i < widgets.Count; i++) {
                this.AddWidget(widgets[i]);
            }
        }

        public void ProcessPacketCommand(string[] data) {
            switch (data[0]) {
                
            }
        }
    }
}
