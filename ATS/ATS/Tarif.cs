using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class Tarif
    {
        private Tarif(int priceByMin, int monthlyFee)
        {
            MonthlyFee = monthlyFee;
            PriceByMin = priceByMin;
        }

        public TarifType TarifType { get; set; }
        public int PriceByMin { get; private set; }
        public int MonthlyFee { get; private set; }
    }
}
