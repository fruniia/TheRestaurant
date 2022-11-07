using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Kitchen : Restaurant
    {
        internal int NumberOfChefs { get => 5; }
        internal List<Chef> chefs = new();
        internal List<Dictionary<int, Group>> bongQueue = new();

        public Kitchen() : base()
        {
            CreateChef();
        }

        public void CreateChef()
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefs.Add(chef);
            }
        }

        public void CookingFood(Chef chef, Dictionary<int, Group> orderlist)
        {
            foreach (var b in orderlist)
            {
                chef.PreparingFood.Add(b.Key, b.Value);
                orderlist.Remove(b.Key);
                chef.Available = false;
                break;
            }
        }
        public void HandlingChef(Dictionary<int, Group> orderlist)
        {
            foreach (var chef in chefs)
            {
                if (chef.Available == true)
                {
                    CookingFood(chef, orderlist);
                }
                else
                {
                    chef.TimeEstimate--;
                    if (chef.TimeEstimate == 0)
                    {
                        chef.TimeEstimate = 10;
                        chef.Available = true;
                    }
                }
            }
        }
    }
}
