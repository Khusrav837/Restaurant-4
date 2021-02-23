using Restaurant.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Moels
{
    public class Chicken : Food
    {
        public Chicken(int quantity) 
        {
            this.Quantity = quantity;
        }

        public override void Cook() {}

        public void CutUp() { }

        public override void Obtain() {}

        public override void Serve() {}
    }
}
