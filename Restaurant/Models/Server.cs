

using Restaurant.Moels;
using System;
using System.Collections.Generic;

namespace Restaurant.Models
{

    public class Server
    {
        private Cook cook;
        List<string> resultOfCooks;
        HashSet<string> customers;
        private TableRequests tableRequests;
        Boolean sendedToCook = false;
        Boolean served = false;
        public delegate void StopGetOrderDelegate(TableRequests table);
        public event StopGetOrderDelegate StopOrders;

        public Server()
        {
            customers = new HashSet<string>();
            resultOfCooks = new List<string>();
            cook = new Cook();
            tableRequests = new TableRequests();
            StopOrders += cook.Process;
        }

        //TODO: It's not good solution if this method's return type is Egg. It should be void (or maybe bool). 
        public Egg Receive(string customerName, int chickenQuantity, int eggQuantity, object drink)
        {
            Egg egg = null;
            if (chickenQuantity > 0)
            {
                tableRequests.Add<Chicken>(customerName);
            }

            if (eggQuantity > 0)
            {
               egg = tableRequests.Add<Egg>(customerName);
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
            customers.Add(customerName);
            return egg;
        }

        public void SendToCook()
        {
            if (sendedToCook)
            {
                throw new Exception("already cooked!");
            }
            sendedToCook = true;
            StopOrders?.Invoke(tableRequests);
            foreach (var customer in customers)
            {
                var orders = tableRequests[customer];
                var ch = 0;
                var e = 0;
                Type t = null;
                if (orders == null)
                {
                    continue;
                }
                foreach (var order in orders)
                {
                    if (order is Chicken)
                    {
                        var o = (Chicken)order;
                        ch = o.GetQuantity();
                    }
                    else if (order is Egg)
                    {
                        var o = (Egg)order;
                        e = o.GetQuantity();
                    }
                    else if (order is IMenuItem)
                    {
                        t = order.GetType();
                    }
                }

                var str = $"Customer {customer} is served {ch} chicken, {e} egg, ";

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
