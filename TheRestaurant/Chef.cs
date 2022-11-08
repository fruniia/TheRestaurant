using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Chef : Person
    {
        private int Experience { get; set; }
        internal bool Available { get; set; }
        internal bool FoodDone { get; set; }

        private string[] chefInAction = { "Cooking food", "Smoking", "Washing hands", "Chitchatting" };
        internal Dictionary<int, Group> PreparingFood { get; set; }

        internal Chef() : base()
        {
            Experience = random.Next(5);
            Available = true;
            TimeEstimate = 10;
            PreparingFood = new Dictionary<int, Group>();
            FoodDone = false;

        }

        internal string ChefInAction()
        {
            string action = Randomize();
            return action;
        }

        internal string Randomize()
        {
            for (int i = 0; i < chefInAction.Length; i++)
            {
                if (Available == false)
                {
                    return chefInAction[0];
                }
                if (Available == true)
                {
                    return chefInAction[random.Next(1, chefInAction.Length)];
                }
            }
            return "";
        }
    }
}
