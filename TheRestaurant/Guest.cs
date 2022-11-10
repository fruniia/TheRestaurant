using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Guest : Person
    {
        readonly Menu menu = new();
        protected int Money { get; set; }
        internal bool OrderedFood { get; set; }
        internal bool GotFood { get; set; }
        internal Guest()
        {
            GotFood = false;
            TimeEstimate = 20;
            Money = random.Next(500, 1000);
        }
        internal Food OrderFood()
        {
            TypeOfFood = menu.RandomFood();
            OrderedFood = true;
            return TypeOfFood;
        }
        internal void DrawOrderFood()
        {
            Console.WriteLine($"{Name} has ordered {TypeOfFood.FoodName} for {TypeOfFood.Price} SEK");
        }
    }
}
