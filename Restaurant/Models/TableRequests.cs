
using Restaurant.Moels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace Restaurant.Models
{
    public class TableRequests : IEnumerable
    {
        
        Dictionary<string, List<IMenuItem>> items = new Dictionary<string, List<IMenuItem>>();
        Type[] ordersType = new Type[] { typeof(Drink), typeof(Chicken), typeof(Egg) };


        public Egg Add<OrderType>(string customerName)
        {
            Egg egg = null;
           if (typeof(OrderType) == typeof(Chicken))
           {
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(new Chicken(1));
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { new Chicken(1) });
                }
           }
           else if (typeof(OrderType) == typeof(Egg))
           {
                egg = new Egg(1);
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(egg);
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { egg });
                }
           }
           else if (typeof(OrderType) == typeof(Coca_Cola))
            {
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(new Coca_Cola());
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { new Coca_Cola() });
                }
            }
           else if (typeof(OrderType) == typeof(Juice))
            {
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(new Juice());
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { new Juice() });
                }
            }
           else if (typeof(OrderType) == typeof(Tea))
            {
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(new Tea());
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { new Tea() });
                }
            }
           else if (typeof(OrderType) == typeof(RC_Cola))
            {
                if (items.ContainsKey(customerName))
                {
                    items[customerName].Add(new RC_Cola());
                }
                else
                {
                    items.Add(customerName, new List<IMenuItem> { new RC_Cola() });
                }
            }
            return egg;
        }
        
        public List<IMenuItem> this [string customerName]
        {
            get 
            {
                return this.items[customerName];
            }
        }

        public List<IMenuItem> Get(Type OrderType)
        {
            List<IMenuItem> orders = new List<IMenuItem>();

            foreach (KeyValuePair<string,List<IMenuItem>> item in items)
            {
               orders.AddRange(item.Value.FindAll(i => i.GetType() == OrderType));
            }
            return orders;
        }

        public IEnumerator GetEnumerator()
        {
            for (int i = 0; i < ordersType.Length; i++ )
            {
                yield return this.Get(ordersType[i]);
            }
        }
    }
}