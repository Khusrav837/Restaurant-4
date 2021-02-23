
using System;

namespace Restaurant.Models
{
    //Slide #10 shown the class Hierarchy. There you can see that the Drink class should be base class for Tea, Pepsi,...casses.
    public abstract class Drink : IMenuItem
    {
        abstract public void Obtain();
        abstract public void Serve();
    }

    public class Tea : Drink
    {
        public override void Obtain() {}
        public override void Serve() {}
    }

    public class Juice : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }

    public class RC_Cola : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }

    public class Coca_Cola : Drink
    {
        public override void Obtain() { }
        public override void Serve() { }
    }
}
