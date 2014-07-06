namespace Client.Logic.Menus.Core
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Text;

    using SdlDotNet.Widgets;
    using Client.Logic.Graphics;

    class MenuBase : Panel
    {
        #region Fields

        Enums.MenuDirection menuDirection;

        #endregion Fields

        #region Constructors

        public MenuBase(string name)
            : base(name) {
            this.BackColor = Color.Transparent;
            //this.BackgroundImageSizeMode = ImageSizeMode.StretchImage;
            this.PreventFocus = true;
        }

        #endregion Constructors

        #region Properties

        public bool ReadyToConfigure {
            get;
            set;
        }

        public Enums.MenuDirection MenuDirection {
            get { return menuDirection; }
            set {
                menuDirection = value;
                switch (menuDirection) {
                    case Enums.MenuDirection.Horizontal: {
                            this.BackgroundImage = GraphicsCache.MenuHorizontal.CreateStretchedSurface(this.Size);
                        }
                        break;
                    case Enums.MenuDirection.Vertical: {
                            this.BackgroundImage = GraphicsCache.MenuVertical.CreateStretchedSurface(this.Size);
                        }
                        break;
                }
            }
        }

        #endregion Properties

        #region Methods

        public virtual void Close() {
            base.FreeResources();
        }

        #endregion Methods
    }
}