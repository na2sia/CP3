using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;


namespace ATS
{
    internal struct Calling
    {
        public Calling(Contract senderOfCall, Contract receiverOfCall, DateTime startCallingTime)
            : this()
        {
            StartCallingTime = startCallingTime;
            ReceiverOfCall = receiverOfCall;
            SenderOfCall = senderOfCall;
        }

        public Calling(Contract senderOfCall, Contract receiverOfCall, DateTime startCallingTime, TimeSpan lenghtSpan,
            double price)
            : this()
        {
            SenderOfCall = senderOfCall;
            ReceiverOfCall = receiverOfCall;
            StartCallingTime = startCallingTime;
            TimeOfCalling = lenghtSpan;
            Price = price;
        }

        public Contract SenderOfCall { get; private set; }
        public Contract ReceiverOfCall { get; private set; }
        public DateTime StartCallingTime { get; private set; }
        public TimeSpan TimeOfCalling { get; set; }
        public double Price { get; set; }

        public override string ToString()
        {
            return @"Sender is " + SenderOfCall.Client
                + Environment.NewLine
                + @"Receiver is " + ReceiverOfCall.Client
                + Environment.NewLine
                + "Starting time of call " + StartCallingTime.ToString("G")
                + Environment.NewLine
                + "Time of call:" + TimeOfCalling
                + Environment.NewLine
                + "Price: " + Price + Environment.NewLine;
        }
    }
}
