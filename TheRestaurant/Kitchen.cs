using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Kitchen : Restaurant
    {
        internal int NumberOfChefs { get => 5; }
        public List<Chef> chefs = new();
        public Kitchen() : base()
        {
            CreateChef();
        }
        public void DrawKitchen<T>(string header, int fromLeft, int fromTop, List<T> anyList)
        {
            string[] graphics = new string[anyList.Count];

            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Chef chef)
                {
                    graphics[i] = $"{chef.Name} {chef.Available}";
                }
            }
            GUI.Draw(header, fromLeft, fromTop, graphics);
        }
        public void CreateChef()
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefs.Add(chef);
            }
        }
    }
}
