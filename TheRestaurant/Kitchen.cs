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

        public void CookingFood()
        {
            foreach (Chef chef in chefs)
            {
                if (chef.Available == true)
                {
                    foreach (var b in bongQueue)
                    {
                        foreach (var kvp in b)
                        {
                            chef.PreparingFood.Add(kvp.Key, kvp.Value);
                            b.Remove(kvp.Key);
                            chef.Available = false;
                            break;
                        }
                        break;
                    }                
                }
            }
        }
    }
}
