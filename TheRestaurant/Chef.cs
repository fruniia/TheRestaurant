using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Chef : Person
    {
        private int Experience { get; set; }
        public bool Available { get; set; }
        private string[] chefInAction = { "Cooking food", "Smoking", "Washing hands", "Chitchatting" };
        public Chef() : base()
        {
            Available = true;
            TimeEstimate = 10;
        }

        public string ChefInAction()
        {
            string action = Randomize();
            return action;
        }

        public string Randomize()
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
