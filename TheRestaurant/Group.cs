using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Group
    {
        internal List<Guest> guests = new();
        protected List<Food> foods = new();
        readonly private Random random = new();
        internal int GroupExperience { get; set; }
        readonly private int minNumberOfGuests = 1;
        readonly private int maxNumberOfGuests = 4;
        internal bool FoodIsReady { get; set; }
        internal int TotalPrice { get; set; }


        internal List<Guest> CreateGuest()
        {
            int rndNumber = random.Next(minNumberOfGuests, (maxNumberOfGuests + 1));
            for (int i = 0; i < rndNumber; i++)
            {
                Guest guest = new();
                guests.Add(guest);
            }
            return guests;
        }
    }
}
