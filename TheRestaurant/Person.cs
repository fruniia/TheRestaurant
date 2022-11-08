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
        internal int TimeEstimate { get; set; }
        internal Food TypeOfFood { get; set; }
        internal Person()
        {
            Name = GetRandomName();
            TimeEstimate = 20;
        }
        internal string GetRandomName()
        {
            string[] allNamnes = File.ReadAllLines("name.txt");
            int rnd = random.Next(0, allNamnes.Length);
            return allNamnes[rnd];
        }
    }
}
