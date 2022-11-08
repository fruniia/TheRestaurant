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
        internal int NumberOfChefs { get; set; }
        internal List<Chef> chefs = new();
        internal List<Dictionary<int, Group>> bongQueue = new();
        internal bool FoodInTheHatch { get; set; }


        internal Kitchen() : base()
        {
            NumberOfChefs = 5;
            CreateChef();
        }

        internal void CreateChef()
        {
            for (int i = 0; i < NumberOfChefs; i++)
            {
                Chef chef = new Chef();
                chefs.Add(chef);
                
            }
        }

        internal void CookingFood(Chef chef, Dictionary<int, Group> orderlist)
        {
            foreach (var b in orderlist)
            {
                foreach (Guest kvp in b.Value.guests)
                {
                    Console.WriteLine($"Kocken {chef.Name} lagar {kvp.TypeOfFood.FoodName} åt {kvp.Name} på bord nummer {b.Key}");
                }
                chef.PreparingFood.Add(b.Key, b.Value);
                orderlist.Remove(b.Key);
                
                chef.Available = false;
                break;
            }
        }
        internal void HandlingChef(Dictionary<int, Group> orderlist)
        {
            foreach (var chef in chefs)
            {
                if (chef.Available == true && orderlist.Count > 0)
                {
                    CookingFood(chef, orderlist);
                }
                else if (chef.Available == false)
                {
                    chef.ChefTimer--;
                    if (chef.ChefTimer == 0)
                    {
                        chef.ChefTimer = chef.TimeEstimate;
                        chef.FoodDone = true;
                        chef.Available = true;
                        FoodInTheHatch = true;
                    }
                }
            }
        }
    }
}
