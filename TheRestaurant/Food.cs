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
        protected string FoodName { get; set; }
        protected int Price { get; set; }
        protected bool Allergen { get; set; }

        public Food()
        {

        }
    }
}
