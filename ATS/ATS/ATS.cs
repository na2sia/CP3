using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace ATS
{
    class ATS: IDictionary<int,Contract>, IList<Calling>
    {
        private readonly IDictionary<int, Contract> contracts;
        private readonly IList<Calling> callings;
        private readonly List<CallArgs> connectedArgs = new List<CallArgs>();
               
        public ATS()
        {
            contracts = new Dictionary<int, Contract>();
            callings = new List<Calling>();
        
        }
    
        #region IList<Calling>

        public int IndexOf(Calling item)
        {
           return callings.IndexOf(item);
        }

        public void Insert(int index, Calling item)
        {
            callings.Insert(index,item);
        }

        public void RemoveAt(int index)
        {
            callings.RemoveAt(index);
        }

        Calling IList<Calling>.this[int index]
        {
            get { return callings[index]; }
            set { callings[index] = value; }
        }
               
        public void Add(Calling item)
        {
            callings.Add(item);
        }

        public void Clear()
        {
            callings.Clear();
        }

        public bool Contains(Calling item)
        {
            return callings.Contains(item);
        }

        public void CopyTo(Calling[] array, int arrayIndex)
        {
            callings.CopyTo(array,arrayIndex);
        }

        public int Count{get { return callings.Count(); }}

        public bool IsReadOnly { get { return callings.IsReadOnly; } }

        public bool Remove(Calling item)
        {
            return callings.Remove(item);
        }

        public IEnumerator<Calling> GetEnumerator()
        {
            return callings.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        #endregion

        #region IDictionary
        public void Add(KeyValuePair<int, Contract> item)
        {
            contracts.Add(item);

            item.Value.Client.CallInitiation += AbonentCall;
            item.Value.Client.EndCallInitiation += AbonentEndCallInitiation;
            item.Value.Client.DisconectInitiation += AbonentDisconnect;
            item.Value.Client.ConnectInitiation += AbonentConnect;
            item.Value.Client.PaymentInitiation += AbonentPay;
            item.Value.Client.ChangingTarifInitiation += AbonentChangeTarif;
            item.Value.Client.GetCallingsByPriceInitiation += PrintCallingsByPrice;
            item.Value.Client.GetCallingsByDateInitiation += PrintCallingsByDate;
            item.Value.Client.GetCallingsByAbonentInitiation += PrintCallingsByAbonent;
        }

        public void Add(int key, Contract value)
        {
            contracts.Add(key,value);
        }

        public bool ContainsKey(int key)
        {
            return contracts.ContainsKey(key);
        }

        public ICollection<int> Keys
        {
            get { return contracts.Keys; }
        }

        public bool Remove(int key)
        {
            
            return contracts.Remove(key);
        }

        public bool TryGetValue(int key, out Contract value)
        {
            return contracts.TryGetValue(key,out value);
        }

        public ICollection<Contract> Values
        {
            get { return contracts.Values; }
        }

        public Contract this[int key]
        {
            get { return contracts[key]; }
            set { contracts[key] = value; }
        }
        
        public bool Contains(KeyValuePair<int, Contract> item)
        {
            return contracts.Contains(item);
        }

        public void CopyTo(KeyValuePair<int, Contract>[] array, int arrayIndex)
        {
            contracts.CopyTo(array,arrayIndex);
        }

        public bool Remove(KeyValuePair<int, Contract> item)
        {
            item.Value.Client.CallInitiation -= AbonentCall;
            item.Value.Client.CallInitiation -= AbonentCall;
            item.Value.Client.EndCallInitiation -= AbonentEndCallInitiation;
            item.Value.Client.DisconectInitiation -= AbonentDisconnect;
            item.Value.Client.ConnectInitiation -= AbonentConnect;
            item.Value.Client.PaymentInitiation -= AbonentPay;
            item.Value.Client.ChangingTarifInitiation -= AbonentChangeTarif;
            item.Value.Client.GetCallingsByPriceInitiation -= PrintCallingsByPrice;
            item.Value.Client.GetCallingsByDateInitiation -= PrintCallingsByDate;
            item.Value.Client.GetCallingsByAbonentInitiation -= PrintCallingsByAbonent;
            return contracts.Remove(item);
        }

        bool ICollection<KeyValuePair<int, Contract>>.IsReadOnly { get { return contracts.IsReadOnly; } }

        int ICollection<KeyValuePair<int, Contract>>.Count { get { return contracts.Count; } }

        IEnumerator<KeyValuePair<int, Contract>> IEnumerable<KeyValuePair<int, Contract>>.GetEnumerator()
        {
            return contracts.GetEnumerator();
        }
        #endregion

        //Calling
        public void AbonentCall(object sender, CallArgs callArgs)
        {
            if (!ContainsKey(callArgs.NumberSender) || !ContainsKey(callArgs.NumberReceiver) ||
                this[callArgs.NumberSender].Port.PortStatus != PortStatus.On ||
                this[callArgs.NumberReceiver].Port.PortStatus != PortStatus.On)
            {
                if (!ContainsKey(callArgs.NumberReceiver))
                {
                    Console.WriteLine("Receiver isn't found");
                }
                if (this[callArgs.NumberSender].Port.PortStatus == PortStatus.Banned)
                {
                    Console.WriteLine("Sender is banned");
                }
                if (this[callArgs.NumberSender].Port.PortStatus == PortStatus.Off)
                {
                    Console.WriteLine("PLS, turn on your phone");
                }
                if (this[callArgs.NumberReceiver].Port.PortStatus == PortStatus.Off)
                {
                    Console.WriteLine("Receiver's phone is turned off");
                }
                if (this[callArgs.NumberReceiver].Port.PortStatus == PortStatus.Banned)
                {
                    Console.WriteLine("Receiver is banned");
                }
                if (this[callArgs.NumberReceiver].Port.PortStatus == PortStatus.Call)
                {
                    Console.WriteLine("Receiver is busy");
                }
                if (this[callArgs.NumberSender].Port.PortStatus == PortStatus.Call)
                {
                    Console.WriteLine("Sender is busy");
                }
                return;
            }
            this[callArgs.NumberSender].Port.PortStatus = PortStatus.Call;
            this[callArgs.NumberReceiver].Port.PortStatus = PortStatus.Call;

            connectedArgs.Add(new CallArgs(this[callArgs.NumberSender].Client.Number,
                this[callArgs.NumberReceiver].Client.Number));

            Add(new Calling(this[callArgs.NumberSender], this[callArgs.NumberReceiver], DateTime.Now));
        }

        //EndCalling
        public void AbonentEndCallInitiation(object sender, CallArgs callArgs)
        {
            var thisConnect = connectedArgs.Find(x => x.NumberSender == callArgs.NumberSender || x.NumberReceiver == callArgs.NumberSender);
            if (thisConnect != null)
            {
                this[thisConnect.NumberSender].Port.PortStatus = PortStatus.On;
                this[thisConnect.NumberReceiver].Port.PortStatus = PortStatus.On;

                //added calling 

                var currentCalling = callings.Last(calling => calling.SenderOfCall.Client.Number == thisConnect.NumberSender);
                Remove(callings.Last(calling => calling.SenderOfCall.Client.Number == thisConnect.NumberSender));
                currentCalling.TimeOfCalling = DateTime.Now - currentCalling.StartCallingTime;

                currentCalling.Price = CalculatePrice(this[thisConnect.NumberSender], currentCalling.TimeOfCalling.TotalMinutes);
                Add(currentCalling);

                //remove money
                this[thisConnect.NumberSender].CurrentBalance -= this[thisConnect.NumberSender].Tarif.PriceByMin * currentCalling.TimeOfCalling.TotalMinutes;
                this[thisConnect.NumberReceiver].CurrentBalance -= this[thisConnect.NumberReceiver].Tarif.PriceByMin * currentCalling.TimeOfCalling.TotalMinutes;

                //changed port status
                if (this[thisConnect.NumberReceiver].CurrentBalance <= 0)
                {
                    this[thisConnect.NumberReceiver].Port.PortStatus = PortStatus.Banned;
                }
                if (this[thisConnect.NumberSender].CurrentBalance <= 0)
                {
                    this[thisConnect.NumberSender].Port.PortStatus = PortStatus.Banned;
                }
            }
            connectedArgs.Remove(thisConnect);
        }

        //AllPrice
        private static double CalculatePrice(Contract contractSender, double totalMins)
        {
            return contractSender.Tarif.PriceByMin * totalMins;
        }

        //Connect
        private void AbonentConnect(object sender, ClientArgs clientArgs)
        {
            if (this[clientArgs.Number].Port.PortStatus == PortStatus.Off)
            {
                this[clientArgs.Number].Port.PortStatus = PortStatus.On;
            }
        }

        //Disconnect
        public void AbonentDisconnect(object sender, ClientArgs clientArgs)
        {
            if (this[clientArgs.Number].Port.PortStatus != PortStatus.On)
            {
                this[clientArgs.Number].Port.PortStatus = PortStatus.Off;
            }
        }

        //Payment
        private void AbonentPay(object sender, PaymentArgs paymentArgs)
        {
            this[paymentArgs.Number].CurrentBalance += paymentArgs.Sum;
            if ((this[paymentArgs.Number].CurrentBalance >= 0)&&(this[paymentArgs.Number].Port.PortStatus!=PortStatus.Off))
            {
                this[paymentArgs.Number].Port.PortStatus = PortStatus.On;
            }
        }

        //ChangingTarif
        private void AbonentChangeTarif(object sender, ClientArgs clientArgs)
        {
            if ((DateTime.Now - this[clientArgs.Number].LastChangingTarifTime).Days >= 30)
            {
                this[clientArgs.Number].Tarif.TarifType = this[clientArgs.Number].Tarif.TarifType == TarifType.FreeMinutes ? TarifType.NoMonthlyFee : TarifType.FreeMinutes;
            }
            else
            {
                Console.WriteLine(Environment.NewLine + "It took less than a month");
            }
        }

        //GetCalling
        
        private IEnumerable<Calling> GetCallings(Contract contract)
        {
            return callings.Where(calling => Equals(calling.SenderOfCall, contract)).ToList();
        }

        private void PrintCallingsByAbonent(object sender, ClientArgs e)
        {
            foreach (var calling in GetCallings(this[e.Number]).OrderBy(calling => calling.ReceiverOfCall))
            {
                Console.WriteLine(calling + Environment.NewLine);
            }
        }

        private void PrintCallingsByDate(object sender, ClientArgs e)
        {
            foreach (var calling in GetCallings(this[e.Number]).OrderBy(calling => calling.StartCallingTime))
            {
                Console.WriteLine(calling + Environment.NewLine);
            }
        }

        private void PrintCallingsByPrice(object sender, ClientArgs e)
        {
            foreach (var calling in GetCallings(this[e.Number]).OrderBy(calling => calling.Price))
            {
                Console.WriteLine(calling + Environment.NewLine);
            }
        }
    }
}
