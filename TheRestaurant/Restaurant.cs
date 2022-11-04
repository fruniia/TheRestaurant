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
        Random random = new Random();
        int startTop = 5;
        int startLeft = 5;
        List<Table> tables = new();
        List<Waiter> waiters = new();
        List<Group> waitingList = new();
        //Food[] foods = new Food[3];
        //Dictionary<Table, List<Food>> order = new();

        // Nyckeln bordet - Lista med gäst + maträtt


        public Restaurant()
        {

        }

        public void Start()
        {
            Entrance entrance = new Entrance();
            Kitchen kitchen = new Kitchen();
            Menu menu = new Menu();
            CreateTable();
            CreateWaiter(waiters);
            entrance.CreateWaitingList(waitingList);

            while (true)
            {
                if (waitingList.Count < 4)
                {
                    entrance.CreateGroup(waitingList);
                }
                entrance.AvailableWaiter(waiters, tables, waitingList);
                foreach (var w in tables)
                {
                    foreach (var a in w.groupInTable.guests)
                    {
                        if (a.OrderedFood == false)
                        {
                            a.TypeOfFood = a.OrderFood();
                            //Console.WriteLine($"{a.Name} har beställt {a.TypeOfFood.FoodName} som kostar {a.TypeOfFood.Price}");
                        }
                    }
                }
                //foreach(var c in kitchen.chefs)
                //{
                //    Console.WriteLine(c.Name);
                //}
                //guest.OrderFood();
                DrawTables<Table>(startLeft, startTop, tables);

                entrance.DrawWaitingList<Group>("Waitinglist", 120, 1, waitingList);
                Console.ReadKey();
                Console.Clear();
            }
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
                    if (i > 4)
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

