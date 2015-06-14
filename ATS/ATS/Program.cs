using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ATS
{
    class Program
    {
        static void Main(string[] args)
        {
            var ats = new ATS();

            var c1 = new Contract(new Client("Natalia", "Volkova", 3211190), new DateTime(2015, 04, 13), new Port(),
                Tarif.GeTarifByType(TarifType.NoMonthlyFee), 5);

            var c2 = new Contract(new Client("Ivan", "Petrov", 7838838), DateTime.Now, new Port(),
                Tarif.GeTarifByType(TarifType.FreeMinutes), 5);

            var c3 = new Contract(new Client("Olga", "Markova", 7879080), DateTime.Now, new Port(),
                Tarif.GeTarifByType(TarifType.NoMonthlyFee), 500000000);



            ats.Add(new KeyValuePair<int, Contract>(c1.Client.Number, c1));
            ats.Add(new KeyValuePair<int, Contract>(c2.Client.Number, c2));
            ats.Add(new KeyValuePair<int, Contract>(c3.Client.Number, c3));
            
            Console.WriteLine(c1.Client + " try to call " + c2.Client + " ");
            c1.Client.Call(c2.Client.Number);
            Thread.Sleep(1000);
            c1.Client.EndTheCall();
            
            c1.Client.Disconect();
            
            c1.Client.GetCallings(1);//0 - by date, 1 - by price, 2 - by abonent
            
            Console.WriteLine(Environment.NewLine + c1.Client + " try to call " + c3.Client + " ");
            c1.Client.Call(c3.Client.Number);

            c1.Client.Pay(10000);
            Console.WriteLine(Environment.NewLine + c1.Client + " try to call " + c3.Client + " ");

            
            c1.Client.Call(c3.Client.Number);
            Thread.Sleep(300);
            c3.Client.EndTheCall();
           
            c2.Client.ChangeTarif();
            Console.WriteLine();
            //c1.Client.GetCallings(1);
            Console.ReadKey();
            
        }
    }
}
