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
            //TODO: Agar hamin proccessi puxtani chicken va egg-ro ba xudi classhoi Chicken va Egg implement kuned dar in metodi Process() code tamoman kam meshavad.
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
