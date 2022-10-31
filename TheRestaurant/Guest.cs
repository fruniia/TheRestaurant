using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Guest : Person
    {
        Random random = new Random();
        private bool Satisfaction { get; set; }
        internal int Money { get; set; }


        public Guest()
        {
            TimeEstimate = 20;
            Money = random.Next(500, 1000);
        }


    }
}
