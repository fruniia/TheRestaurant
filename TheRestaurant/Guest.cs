using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Guest : Person
    {

        Menu menu = new Menu();
        private bool Satisfaction { get; set; }
        internal int Money { get; set; }
        public bool OrderedFood { get; set; }


        public Guest()
        {
            TimeEstimate = 20;
            Money = random.Next(500, 1000);
        }

        public Food OrderFood()
        {
            TypeOfFood = menu.RandomFood();
            OrderedFood = true;
            return TypeOfFood;
        }

        public void DrawOrderFood()
        {
            Console.WriteLine($"{this.Name} has ordered {TypeOfFood.FoodName} for {this.TypeOfFood.Price} SEK");
        }
    }
}
