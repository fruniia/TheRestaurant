using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Waiter : Person
    {
        private int ServiceLevel { get; set; }
        public bool Available { get; set; }
        public Dictionary<int, Group> OrderOnTheGo { get; set; }


        // Servitör går med beställningen till kocken
        // Kocken tar emot beställningen
        // Kocken lagar maten(tar 10 i “tid”)
        // Servitören hämtar maten
        // Servitören serverar maten
        // Gästen äter maten(tar 20 i “tid”)
        // Gästen betalar till servitören
        // Kundens krav: Vällagad mat, Bra service, Bra bord, Korta väntetider
        // Om kunden är nöjd så dricksar denne ett visst antal procent
        // Antingen går gästen eller så får hen diska
        // Servitör dukar av bordet(tar 3 I “tid”)

        public Waiter() : base()
        {
            Available = true;
            TimeEstimate = 3;
            OrderOnTheGo = new Dictionary<int, Group>();
        }

        public void OrderToKitchen(Dictionary<int, Group> foodorder)
        {
            OrderOnTheGo = foodorder;
            foreach (KeyValuePair<int, Group> kvp in foodorder)
            {
                foreach (var group in kvp.Value.guests)
                {
                    Console.WriteLine($"{kvp.Key} + {group.TypeOfFood.FoodName}");
                }
            }
        }
    }
}
