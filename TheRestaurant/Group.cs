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
        private Random random = new Random();

        public List<Guest> CreateGuest()
        {
            int number = random.Next(1, 5);

            for (int i = 0; i < number; i++)
            {
                Guest guest = new Guest();
                guests.Add(guest);
            }
            return guests;
        }

    }
}
