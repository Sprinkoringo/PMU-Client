using System;
using System.Collections.Generic;
using System.Text;

namespace Client.Logic.Sdl
{
    class SdlCore
    {
        /// <summary>
        /// Quits the application.
        /// </summary>
        public static void QuitApplication() {
            SdlDotNet.Core.Events.Tick -= new EventHandler<SdlDotNet.Core.TickEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_Tick);
            //SdlDotNet.Core.Events.QuitApplication();

            IO.Options.SaveXml();

            Environment.Exit(0);
        }
    }
}
