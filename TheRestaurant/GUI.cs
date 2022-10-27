using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class GUI
    {

        Random random = new Random();
        Restaurant restaurant = new Restaurant();
        List<Restaurant> emptyTables = new List<Restaurant>();
        List<Guest> waitingList = new List<Guest>();
        List<Chef> chefList = new List<Chef>();
        List<Waiter> waiterList = new List<Waiter> ();

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

        public void CreateGuest()
        {
            for (int i = 0; i < 10; i++)
            {
                Guest guest = new Guest();
                waitingList.Add(guest);
            }
            foreach (var wl in waitingList)
            {
                Console.WriteLine($"{wl.Name} + {wl.Company}");
            }
        }

        public void CreateChef()
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefList.Add(chef);
            }
            foreach (var c in chefList)
            {
                Console.WriteLine($"{c.GetType().Name} {c.Name}");
            }
        }

        public void CreateWaiter()
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiterList.Add(waiter);
            }
            foreach (var w in waiterList)
            {
                Console.WriteLine($"{w.GetType().Name} {w.Name}");
            }
        }
    }
}