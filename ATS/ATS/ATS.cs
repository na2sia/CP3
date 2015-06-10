using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class ATS: IDictionary<int,Contract>
    {
        private readonly IDictionary<int, Contract> contracts;
    }
}
