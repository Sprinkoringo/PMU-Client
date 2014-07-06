using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.ExpKit
{
    interface IKitModule
    {
        void Created(int index);
        /// <summary>
        /// Called when the module is switched for another module
        /// </summary>
        void SwitchOut();
        /// <summary>
        /// Called when the module is set as the active module
        /// </summary>
        void Initialize(Size containerSize);
        /// <summary>
        /// Gets the index # of the module
        /// </summary>
        int ModuleIndex { get; }
        /// <summary>
        /// Gets the friendly name of the module
        /// </summary>
        string ModuleName { get; }
        Panel ModulePanel { get; }
        bool Enabled { get; set; }
        Enums.ExpKitModules ModuleID { get; }
        event EventHandler EnabledChanged;
    }
}
