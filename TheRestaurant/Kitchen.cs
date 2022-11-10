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
        private int NumberOfChefs { get; set; }
        internal List<Chef> chefs = new();
        internal bool FoodInTheHatch { get; set; }
        internal Kitchen() : base()
        {
            NumberOfChefs = 5;
            CreateChef();
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
        private void CreateChef()
        {
            for (int i = 0; i < NumberOfChefs; i++)
            {
                Chef chef = new();
                chefs.Add(chef);
            }
        }
        private static void CookingFood(Chef chef, Dictionary<int, Group> orderlist)
        {
            foreach (var kvp in orderlist)
            {
                if (kvp.Value.FoodIsReady == false)
                {
                    foreach (Guest guest in kvp.Value.guests)
                    {
                        if (guest.GotFood == false)
                        {
                            Console.WriteLine($"Chef {chef.Name} cooks {guest.TypeOfFood.FoodName} for {guest.Name} at table number {kvp.Key}");
                        }
                    }
                    kvp.Value.GroupExperience += chef.Experience;
                    kvp.Value.FoodIsReady = true;
                    chef.Available = false;
                    break;
                }
            }
        }
    }
}
