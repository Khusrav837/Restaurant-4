﻿using System;
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

            IMenuItem menu;
            if (typeof(OrderType).BaseType == typeof(Food))
                menu = (IMenuItem)Activator.CreateInstance(typeof(OrderType), 1);
            else
                menu = (IMenuItem)Activator.CreateInstance(typeof(OrderType));

            items[customerName].Add(menu);
        }

        public void Clear()
        {
            items.Clear();
        }

        public List<IMenuItem> this[string customerName]
        {
            get
            {
                if (!items.ContainsKey(customerName))
                {
                    throw new Exception("This customer didn't give order!");
                }
                return this.items[customerName];
            }
        }

        public List<IMenuItem> Get<OrderType>()
        {
            List<IMenuItem> orders = new List<IMenuItem>();

            foreach (KeyValuePair<string, List<IMenuItem>> item in items)
            {
                orders.AddRange(item.Value.FindAll(i => i is OrderType));
            }
            return orders;
        }

        public IEnumerator GetEnumerator()
        {
            return items.GetEnumerator();
        }
    }
}