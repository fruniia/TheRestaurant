using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Order : Kitchen
    {
        internal Dictionary<int, Group> Orderlist { get; set; }
        public int TimeEstimate { get; set; }
        public bool OrderDone { get; set; }

        internal Order() : base()
        {
            Orderlist = new();
        }
    }
}
