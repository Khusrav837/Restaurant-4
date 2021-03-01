using Restaurant.Moels;
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
