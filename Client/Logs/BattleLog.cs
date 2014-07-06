using System;
using System.Collections.Generic;
using System.Text;

using SdlDotNet.Widgets;
using System.Drawing;

namespace Client.Logic.Logs {
    class BattleLog {


        public static readonly int MaxMessages = 500;
        public static readonly int MaxShownMessages = 5;

        public static Queue<String> Messages;
        public static Queue<Color> MessageColor;


        public static void Initialize() {
            Messages = new Queue<string>();
            MessageColor = new Queue<Color>();

        }

        public static void AddLog(string message, Color color) {
            if (Messages.Count == MaxMessages) {
                Messages.Dequeue();
                MessageColor.Dequeue();
            }
            Messages.Enqueue(message);
            MessageColor.Enqueue(color);

        }

    }
}
