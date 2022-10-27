using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Restaurant
    {
        protected int MaxNumberOfGuests = 30;
        public int TableForTwo = 2;
        public int TableForFour = 4;
        protected string FacilityName { get; set; }

    }
}
