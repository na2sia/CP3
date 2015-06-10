using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    internal class Client
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public int Number { get; private set; }
        public DateTime LastPaymentTime { get; private set; }

        public Client(string firstName, string lastName, int number)
        {
            LastPaymentTime = DateTime.Now;
            Number = number;
            LastName = lastName;
            FirstName = firstName;
        }

        public override string ToString()
        {
            return FirstName + " " + LastName + " " + Number;
        }
    }
}
