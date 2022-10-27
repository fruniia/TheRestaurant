using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Table : Restaurant
    {
        private int NumberOfGuestsAtTable { get; set; }
        private bool Occupied { get; set; }
        private int QualityLevel { get; set; }

        private List<Food> Menu; 
    }
}
