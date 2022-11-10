using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Menu
    {
        readonly private Random random = new();
        internal List<Food> menu = new();
        internal Menu()
        {
            CreateMenu();
            Food food = RandomFood();
        }
        internal Food RandomFood()
        {
            Food food = menu[random.Next(menu.Count)];
            return food;
        }
        private void CreateMenu()
        {
            menu.Add(new Fish("Fish and pasta", 199));
            menu.Add(new Fish("Fish and chips", 259));
            menu.Add(new Fish("Fish and rice", 219));
            menu.Add(new Meat("Meat and pasta", 299));
            menu.Add(new Meat("Meat and chips", 179));
            menu.Add(new Meat("Meat and rice", 329));
            menu.Add(new Vegetarian("Beans and pasta", 179));
            menu.Add(new Vegetarian("Beans and greens", 199));
            menu.Add(new Vegetarian("Just beans", 129));
        }
    }
}
