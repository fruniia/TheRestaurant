using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Person
    {
        internal Random random = new Random();
        internal string Name { get; set; }
        public int TimeEstimate { get; set; }
        public Food TypeOfFood { get; set; }
        public string GetRandomName()
        {
            string[] allNamnes = File.ReadAllLines("name.txt");
            int rnd = random.Next(0, allNamnes.Length);
            return allNamnes[rnd];
        }
        public Person()
        {
            Name = GetRandomName();
            TimeEstimate = 0;
        }
    }
}
