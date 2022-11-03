using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Group
    {
        internal List<Guest> guests = new List<Guest>();
        internal List<Food> foods = new List<Food>();
        private Random random = new Random();
        private int minNumberOfGuests = 1;
        private int maxNumberOfGuests = 4;

        public List<Guest> CreateGuest()
        {
            int number = random.Next(minNumberOfGuests, (maxNumberOfGuests + 1));

            for (int i = 0; i < number; i++)
            {
                Guest guest = new Guest();
                guests.Add(guest);
            }
            return guests;
        }
    }
}
