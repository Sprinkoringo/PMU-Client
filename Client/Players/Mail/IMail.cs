using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet.Widgets;

namespace Client.Logic.Players.Mail
{
    interface IMail
    {
        string SenderID { get; set; }
        string RecieverID { get; set; }
        MailType Type { get; }
        bool Unread { get; set; }

        Panel MailInterfacePanel { get; }
    }
}
