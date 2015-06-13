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

        //Connected
        public event EventHandler<ClientArgs> ConnectInitiation;

        public void Connect()
        {
            OnConnect(this, new ClientArgs(Number));
        }

        protected virtual void OnConnect(object sender, ClientArgs clientArgs)
        {
            if (ConnectInitiation != null)
            {
                ConnectInitiation(this, new ClientArgs(Number));
            }
        }

        //Disconnected
        public event EventHandler<ClientArgs> DisconectInitiation;

        public void Disconect()
        {
            OnDisconnect(this, new ClientArgs(Number));
        }

        protected virtual void OnDisconnect(object sender, ClientArgs clientArgs)
        {
            if (DisconectInitiation != null)
            {
                DisconectInitiation(sender, clientArgs);
            }
        }


        //Calling
        public event EventHandler<CallArgs> CallInitiation;

        public void Call(int targetNumber)
        {
            OnCallInitiation(this, new CallArgs(Number, targetNumber));
        }

        protected virtual void OnCallInitiation(object sender, CallArgs args)
        {
            if (CallInitiation != null)
            {
                CallInitiation(sender, args);
            }
        }
        
        //EndOfCall
        public event EventHandler<CallArgs> EndCallInitiation;

        public void EndTheCall()
        {
            OnEndCall(this, new CallArgs(Number, 0));
        }

        protected virtual void OnEndCall(object sender, CallArgs args)
        {
            if (EndCallInitiation != null)
            {
                EndCallInitiation(sender, args);
            }
        }

        //ChangingTarif
        public event EventHandler<ClientArgs> ChangingTarifInitiation;

        public void ChangeTarif()
        {
            OnChangingTarif(this, new ClientArgs(Number));
        }
        public virtual void OnChangingTarif(object sender, ClientArgs args)
        {
            if (ChangingTarifInitiation != null)
            {
                ChangingTarifInitiation(sender, args);
            }
        }
        
        //Payment
        public event EventHandler<PaymentArgs> PaymentInitiation;

        public void Pay(int summ)
        {
            OnPay(this, new PaymentArgs(summ, Number));
        }

        protected virtual void OnPay(object sender, PaymentArgs payArgs)
        {
            if (PaymentInitiation != null)
            {
                PaymentInitiation(this, payArgs);
            }
        }

    
        //CallingHistory
        public event EventHandler<ClientArgs> GetCallingsByDateInitiation;
        public event EventHandler<ClientArgs> GetCallingsByPriceInitiation;
        public event EventHandler<ClientArgs> GetCallingsByAbonentInitiation;

        public void GetCallings(int num)
        {
            switch (num)
            {
                case 0:
                    OnGetCallingByDate(this, new ClientArgs(Number));
                    break;
                case 1:
                    OnGetCallingsByPrice(this, new ClientArgs(Number));
                    break;
                default:
                    OnGetCallingsByAbonent(this, new ClientArgs(Number));
                    break;
            }
        }

        private void OnGetCallingByDate(object sender, ClientArgs clientArgs)
        {
            if (GetCallingsByDateInitiation != null)
            {
                GetCallingsByDateInitiation(sender, clientArgs);
            }
        }
                
        protected virtual void OnGetCallingsByPrice(object sender, ClientArgs clientArgs)
        {
            if (GetCallingsByPriceInitiation != null)
            {
                GetCallingsByPriceInitiation(sender, clientArgs);
            }
        }

        protected virtual void OnGetCallingsByAbonent(object sender, ClientArgs clientArgs)
        {
            if (GetCallingsByAbonentInitiation != null)
            {
                GetCallingsByAbonentInitiation(sender, clientArgs);
            }
        }
                       
    }
}
