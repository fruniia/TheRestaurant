using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Register : Restaurant
    {
        internal int TonightsRevenue { get; set; }
        internal int Tip { get; set; }

        internal int RevenuePerGroup { get; set; }
        public Register()
        {
            TonightsRevenue += RevenuePerGroup;
            Tip = 0;
            RevenuePerGroup = 0;
        }
    }
}
