using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class GUI
    {

        Restaurant restaurant = new Restaurant();
        List<Restaurant> emptyTables = new List<Restaurant>();
        
        public void CreateTable()
        { 
            for(int i = 0; i< 5; i++)
            {
                Table smallTable = new Table(restaurant.TableForTwo);
                emptyTables.Add(smallTable);
                Table bigTable = new Table(restaurant.TableForFour);
                emptyTables.Add(bigTable);
            }

            foreach (var table in emptyTables)
            { 
             Console.WriteLine(table.GetType().Name);
            }
        }
    }
}