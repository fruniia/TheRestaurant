using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Guest : Person
    {
        private bool Allergic { get; set; }
        private bool Satisfaction { get; set; }
        private int Money { get; set; }
        private int Company { get; set; }

        Random random = new Random();

        public Guest()
        { 
            Company = random.Next(4);
        }
    }
}
