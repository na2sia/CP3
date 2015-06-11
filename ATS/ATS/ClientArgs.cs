using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ATS
{
    internal class ClientArgs : EventArgs
    {
        public ClientArgs(int number)
        {
            Number = number;
        }

        public int Number { get; private set; }
    }
}