

using Restaurant.Moels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Restaurant.Models
{

    public class Server
    {
        private Cook cook;
        List<string> resultOfCooks;
        private TableRequests tableRequests;
        Boolean sendedToCook = false;
        Boolean served = false;
        public delegate void ReadyDelagate(TableRequests table);
        public event ReadyDelagate Ready;

        public Server()
        {
            resultOfCooks = new List<string>();
            cook = new Cook();  //TODO: The purpose of using events is decoupling server and cook. Server should not know about cook and cook should not know about server.
            tableRequests = new TableRequests();
            //TODO: Subscribing can be done in MainWindow.cs 
            cook.Processed += tableRequests => { Processed(tableRequests); };
            Ready += cook.Process;
        }
 
        public void Receive(string customerName, int chickenQuantity, int eggQuantity, object drink)
        {
            //TODO: Do we need this "if" condition here?
            if (chickenQuantity > 0)
            {
                foreach (var _ in Enumerable.Range(1, chickenQuantity))
                {
                    tableRequests.Add<Chicken>(customerName);
                }
            }

            if (eggQuantity > 0)
            {
                foreach (var _ in Enumerable.Range(1, eggQuantity))
                {
                    tableRequests.Add<Egg>(customerName);
                }
            }
            if (drink is Drinks)
            {
                var d = (Drinks)drink;
                if (d == Drinks.Coca_Cola)
                {
                    tableRequests.Add<Coca_Cola>(customerName);
                }
                else if (d == Drinks.Juice)
                {
                    tableRequests.Add<Juice>(customerName);
                }
                else if (d == Drinks.RC_Cola)
                {
                    tableRequests.Add<RC_Cola>(customerName);
                }
                else if (d == Drinks.Tea)
                {
                    tableRequests.Add<Tea>(customerName);
                }
            }
        }
        public void SendToCook()
        {
            if (sendedToCook)
            {
                throw new Exception("already cooked!");
            }
            sendedToCook = true;
            Ready?.Invoke(tableRequests);
        }

        public void Processed(TableRequests tableRequests)
        {
            foreach (KeyValuePair<string, List<IMenuItem>> row in tableRequests)
            {
                var ch = 0;
                var e = 0;
                Type t = null;

                foreach (IMenuItem value in row.Value)
                {
                    if (value is Chicken)
                    {
                        ch++;
                    }
                    else if (value is Egg)
                    {
                        e++;
                    }
                    else if (value is Drink)
                    {
                        t = value.GetType();
                    }
                }

                var str = $"Customer {row.Key} is served {ch} chicken, {e} egg, ";

                if (t != null)
                {
                    str += $"{t.Name}";
                }
                else
                {
                    str += "no drinks";
                }
                resultOfCooks.Add(str);
            }
        }

        public List<string> Serve()
        {
            if (served)
            {
                throw new Exception("Customers already served!");
            }
            if (!sendedToCook)
            {
                throw new Exception("You didn't cook!");
            }
            served = true;
            return resultOfCooks;
        }
    }

    public enum Drinks : short
    {
        Tea,
        Juice,
        RC_Cola,
        Coca_Cola
    }
}
