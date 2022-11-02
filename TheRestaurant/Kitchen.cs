using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Kitchen : Restaurant
    {
        private int NumberOfChefs { get => 5; }
        public Kitchen()
        {

        }
        public void CreateChef()
        {
            List<Chef> chefs = new();

            for (int i = 0; i < NumberOfChefs; i++)
            {
                Chef chef = new Chef();
                chefs.Add(chef);
            }
        }
    }
}
