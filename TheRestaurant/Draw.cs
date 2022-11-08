using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheRestaurant;

namespace TheRestaurant
{
    internal class Draw
    {
        internal static void Drawing<T>(string header, int fromLeft, int fromTop, List<T> anyList)
        {
            string[] graphics = new string[anyList.Count];

            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Chef chef)
                {
                    graphics[i] = $"{chef.Name} is {chef.ChefInAction()}";
                }

                if (anyList[i] is Group)
                {
                    var groups = (anyList[i] as Group).guests;
                    foreach (var g in groups)
                    {
                        graphics[i] = $"Company of {groups.Count}  {g.Name}";
                    }
                }
                if (anyList[i] is Food food)
                {
                    graphics[i] = $"~ {food.FoodName} {food.Price} SEK ~";
                }
            }

            GUI.Draw(header, fromLeft, fromTop, graphics);
        }
        internal static void DrawingT<T>(string header, int fromLeft, int fromTop, List<T> anyList)
        {
            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Table)
                {
                    header = $"Table {i + 1}";
                    if (i > 4)
                    {
                        header = $"Table {i + 1}";
                    }
                    var groups = (anyList[i] as Table).groupInTable.guests;
                    string[] graphics = new string[groups.Count];
                    int count = 0;
                    foreach (var g in groups)
                    {
                        graphics[count] = $"{g.Name}";
                        count++;
                    }
                    GUI.Draw(header, fromLeft, fromTop, graphics);
                }
                fromLeft += 20;
                if (fromLeft > 90)
                {
                    fromTop += 15;
                    fromLeft = 5;
                }
            }
        }
    }
}

