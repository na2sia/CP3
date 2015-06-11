using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
namespace ATS
{
    internal class CallArgs : EventArgs
    {
        public readonly int NumberSender;
        public readonly int NumberReceiver;

        public CallArgs(int numberSender, int numberReceiver)
        {
            NumberSender = numberSender;
            NumberReceiver = numberReceiver;
        }
    }
}