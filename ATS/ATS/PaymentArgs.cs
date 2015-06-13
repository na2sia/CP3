using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ATS
{
    internal class PaymentArgs : EventArgs
    {
        public PaymentArgs(int sum, int number)
        {
            Number = number;
            Sum = sum;
        }
        public int Sum { get; private set; }
        public int Number { get; private set; }
    }
}