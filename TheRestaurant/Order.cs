using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Order : Kitchen
    {
        public Dictionary<int, Group> Orderlist { get; set; }

        public Order() : base()
        {
            Orderlist = new();
        }
    }
}
