using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Order
    {
        public Dictionary<int, Group> Orderlist { get; set; }

        public Order()
        {
            Orderlist = new();
        }
    }
}
