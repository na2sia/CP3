using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ATS
{
    class Contract
    {
        public Contract(Client client, DateTime timeOfConclusionOfTheContract, Port port, Tarif tarif, int currentBalance)
        {
            CurrentBalance = currentBalance;
            Tarif = tarif;
            Port = port;
            TimeOfConclusionOfTheContract = timeOfConclusionOfTheContract;
            Client = client;
            //LastFeeTakenDate = timeOfConclusionOfTheContract;
            LastChangingTarifTime = timeOfConclusionOfTheContract;
        }



        public Client Client { get; private set; }
        public DateTime TimeOfConclusionOfTheContract { get; private set; }
        public Port Port { get; private set; }
        public Tarif Tarif { get; private set; }
        public double CurrentBalance { get; set; }
        //public DateTime LastFeeTakenDate { get; set; }
        public DateTime LastChangingTarifTime { get; set; }
    }
}
