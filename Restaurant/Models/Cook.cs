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
        public void Process(TableRequests table)
        {
            foreach (List<IMenuItem> items in table)
            {
                foreach (var o in items)
                {
                    if (o is Chicken)
                    {
                        var chicken = (Chicken)o;
                        chicken.Obtain();
                        chicken.CutUp();
                        chicken.Cook();
                    }
                    else if (o is Egg)
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
                    else if (o is Drink)
                    {
                        var drink = (Drink)o;
                        drink.Obtain();
                    }
                }
            }            
        }
    }
}