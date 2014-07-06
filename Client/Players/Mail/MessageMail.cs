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
