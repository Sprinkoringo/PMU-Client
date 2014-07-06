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
