using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Client.Logic.Sdl
{
    class SdlLoader
    {
        public static void InitializeSdl() {
            SdlDotNet.Graphics.Video.SetVideoMode(850, 640, 32, false, false, false);
            SdlDotNet.Graphics.Video.WindowIcon();
            SdlDotNet.Graphics.Video.WindowCaption = "Pokemon Mystery Universe";
            SdlDotNet.Input.Keyboard.EnableKeyRepeat(5, 5);

            SdlDotNet.Core.Events.Tick += new EventHandler<SdlDotNet.Core.TickEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_Tick);
            SdlDotNet.Core.Events.Quit += new EventHandler<SdlDotNet.Core.QuitEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_Quit);
            SdlDotNet.Core.Events.KeyboardDown += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_KeyboardDown);
            SdlDotNet.Core.Events.KeyboardUp += new EventHandler<SdlDotNet.Input.KeyboardEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_KeyboardUp);
            SdlDotNet.Core.Events.MouseMotion += new EventHandler<SdlDotNet.Input.MouseMotionEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_MouseMotion);
            SdlDotNet.Core.Events.MouseButtonDown += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_MouseButtonDown);
            SdlDotNet.Core.Events.MouseButtonUp += new EventHandler<SdlDotNet.Input.MouseButtonEventArgs>(SdlEventHandler.SdlDotNet_Core_Events_MouseButtonUp);

            SdlDotNet.Core.Events.TargetFps = Constants.FRAME_RATE;

            SdlDotNet.Widgets.WindowManager.SetMainThread();

            // Create and run the loading thread
            Thread loadThread = new Thread(new ThreadStart(Loader.LoadData));
            loadThread.IsBackground = true;
            loadThread.Name = "PMU Load Thread";
            loadThread.Priority = ThreadPriority.Normal;
            loadThread.Start();

            SdlDotNet.Core.Events.Run();

            IO.Options.SaveXml();
        }

    }
}
