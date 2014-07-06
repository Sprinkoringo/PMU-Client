using System;
using System.Collections.Generic;
using System.Text;
using SdlDotNet.Widgets;

namespace Client.Logic.Players.Mail
{
    class MessageMail : IMail
    {
        #region Fields

        InventoryItem attachedItem;
        string recieverID;
        string senderID;
        string text;
        string title;

        Panel pnlMailInterface;

        #endregion Fields

        #region Properties

        public InventoryItem AttachedItem {
            get { return attachedItem; }
            set { attachedItem = value; }
        }

        public string RecieverID {
            get { return recieverID; }
            set { recieverID = value; }
        }

        public string SenderID {
            get { return senderID; }
            set { senderID = value; }
        }

        public string Text {
            get { return text; }
            set { text = value; }
        }

        public string Title {
            get { return title; }
            set { title = value; }
        }

        public MailType Type {
            get { return MailType.Message; }
        }

        public bool Unread {
            get;
            set;
        }

        #endregion Properties


        public SdlDotNet.Widgets.Panel MailInterfacePanel {
            get {
                CheckMailInterfacePanel();
                return pnlMailInterface;
            }
        }

        private void CheckMailInterfacePanel() {
            if (pnlMailInterface == null) {

            }
        }

        private void CreateMailInterfacePanel() {
            pnlMailInterface = new Panel("pnlMailInterface");
            pnlMailInterface.Size = new System.Drawing.Size(300, 400);


        }
    }
}
