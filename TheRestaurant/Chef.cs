using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Chef : Person
    {
        private int Experience { get; set; }
        public bool Available { get; set; }
        public Chef(): base()
        {
            Available = true;
            TimeEstimate = 10;
        }
    }
}
