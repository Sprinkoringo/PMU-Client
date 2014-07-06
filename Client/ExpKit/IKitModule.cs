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
