﻿using Restaurant.Moels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Models
{
    public class Cook
    {
        public delegate void ProcessedDelegate(TableRequests table);
        public event ProcessedDelegate Processed;
        public void Process(TableRequests table)
        {
            //TODO: The Cook should not think about drinks because only the Server can obtain and server drinks. Cook may take only foods.
            var drinks = table.Get<Drink>();
            foreach (var drink in drinks)
            {
                drink.Obtain();
            }

            var eggs = table.Get<Egg>();
            foreach (var o in eggs)
            {
                using (var egg = (Egg)o)
                {
                    egg.Obtain();
                    try
                    {
                        egg.Crack();
                    }
                    catch
                    {
                    }
                    egg.Cook();
                }
            }

            var chickens = table.Get<Chicken>();
            foreach (Chicken chicken in chickens)
            {
                chicken.Obtain();
                chicken.CutUp();
                chicken.Cook();
            }

            Processed?.Invoke(table);
        }
    }
}
