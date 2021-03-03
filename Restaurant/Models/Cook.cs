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
            foreach (Egg egg in eggs)
            {
                egg.Prepare();
            }

            var chickens = table.Get<Chicken>();
            foreach (Chicken chicken in chickens)
            {
                chicken.Prepare();
            }

            Processed?.Invoke(table);
        }
    }
}
