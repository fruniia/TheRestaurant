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
        private bool Allergic { get; set; }
        private bool Satisfaction { get; set; }
        private int Money { get; set; }
        internal int Company { get; set; }

        public Guest()
        {
            //Name = GetRandomName(); //Gästen
            TimeEstimate = 20;
            Company = random.Next(4); //Antal kompisar
        }

        //public Guest()
        //{ 
        //    Company = random.Next(4);
        //}
    }
}
