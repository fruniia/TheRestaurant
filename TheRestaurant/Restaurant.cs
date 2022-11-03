using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Restaurant
    {
        protected int maxNumberOfGuests = 80;
        int startTop = 5;
        int startLeft = 5;
        List<Table> tables = new();

        List<Waiter> waiters = new();
        List<Group> groups = new();
        List<Group> waitingList = new();




        public Restaurant()
        {

        }

        public void Start()
        {
            Restaurant restaurant = new Restaurant();
            Entrance entrance = new Entrance();
            Kitchen kitchen = new Kitchen();
            Menu();
            CreateTable();
            CreateWaiter(waiters);
            kitchen.CreateChef();
            entrance.CreateWaitingList(waitingList);

            while (true)
            {
                if (waitingList.Count < 10)
                {
                    entrance.CreateGroup(waitingList);
                }
                entrance.AvailableWaiter(waiters, tables, waitingList);
                DrawTables<Table>(startLeft, startTop, tables);

                entrance.DrawWaitingList<Group>("Waitinglist", 120, 1, waitingList);
                Console.ReadKey();
                Console.Clear();

            }
        }


        public void Menu()
        {
            List<Food> menu = new();
            menu.Add(new Fish("Fish and pasta", 199));
            menu.Add(new Fish("Fish and chips", 259));
            menu.Add(new Fish("Fish and rice", 219));
            menu.Add(new Meat("Meat and pasta", 299));
            menu.Add(new Meat("Meat and chips", 179));
            menu.Add(new Meat("Meat and rice", 329));
            menu.Add(new Vegetarian("Beans and pasta", 179));
            menu.Add(new Vegetarian("Beans and greens", 199));
            menu.Add(new Vegetarian("Just beans", 129));
        }

        private void CreateTable()
        {
            for (int i = 0; i < 5; i++)
            {
                TableForTwo smallTable = new();
                tables.Add(smallTable);
            }
            for (int i = 0; i < 5; i++)
            {
                TableForFour bigTable = new();
                tables.Add(bigTable);
            }
        }

        private void CreateWaiter(List<Waiter> waiters)
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiters.Add(waiter);
            }
        }

        public void DrawTables<T>(int fromLeft, int fromTop, List<T> anyList)
        {
            string header = "";
            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Table)
                {
                    header = $"TableTwo {i + 1}";
                    if(i > 4)
                    {
                        header = $"TableFour {i + 1}";
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

