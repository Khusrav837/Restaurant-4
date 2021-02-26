
using Restaurant.Moels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class TableRequests : IEnumerable
    {

        Dictionary<string, List<IMenuItem>> items = new Dictionary<string, List<IMenuItem>>();

        public void Add<OrderType>(string customerName)
        {
            if (!items.ContainsKey(customerName))
            {
                items.Add(customerName, new List<IMenuItem> { });
            }

            if (typeof(OrderType) == typeof(Chicken))
            {
                items[customerName].Add(new Chicken(1));
            }
            else if (typeof(OrderType) == typeof(Egg))
            {
                items[customerName].Add(new Egg(1));
            }
            else if (typeof(OrderType) == typeof(Coca_Cola))
            {
                items[customerName].Add(new Coca_Cola());
            }
            else if (typeof(OrderType) == typeof(Juice))
            {
                items[customerName].Add(new Juice());
            }
            else if (typeof(OrderType) == typeof(Tea))
            {
                items[customerName].Add(new Tea());
            }
            else if (typeof(OrderType) == typeof(RC_Cola))
            {
                items[customerName].Add(new RC_Cola());
            }
        }

        public List<IMenuItem> this[string customerName]
        {
            get
            {
                return this.items[customerName];
            }
        }

        public List<IMenuItem> Get<OrderType>()
        {
            List<IMenuItem> orders = new List<IMenuItem>();

            foreach (KeyValuePair<string, List<IMenuItem>> item in items)
            {
                orders.AddRange(item.Value.FindAll(i => i.GetType() == typeof(OrderType)));
            }
            return orders;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (KeyValuePair<string, List<IMenuItem>> item in items)
            {
                yield return item;
            }
        }
    }
}