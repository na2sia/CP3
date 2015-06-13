using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class Tarif
    {
        public TarifType TarifType { get; set; }
        public int PriceByMin { get; private set; }
        public int MonthlyFee { get; private set; }

        private Tarif(int priceByMin, int monthlyFee)
        {
            MonthlyFee = monthlyFee;
            PriceByMin = priceByMin;
        }

        private static readonly Tarif[] Tarifs = { new Tarif(10, 100), new Tarif(300, 0) };

        public static Tarif GeTarifByType(TarifType tarifType)
        {
            switch (tarifType)
            {
                case TarifType.FreeMinutes:
                    return Tarifs[0];
                case TarifType.NoMonthlyFee:
                    return Tarifs[1];
                default:
                    return null;
            }


        }
    }
}
