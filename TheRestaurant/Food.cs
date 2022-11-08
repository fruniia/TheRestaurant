using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Food
    {
        internal string FoodName { get; set; }
        internal int Price { get; set; }

        internal Food(string foodName, int price)
        {
            FoodName = foodName;
            Price = price;
        }
    }
}
