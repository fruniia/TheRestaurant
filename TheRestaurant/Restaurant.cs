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
        int startTop = 10;
        int startLeft = 5;
        List<Table> tables = new();
        List<Waiter> waiters = new();
        List<Group> waitingList = new();
        Dictionary<int, Group> order = new();


        public Restaurant()
        {

        }

        public void Start()
        {
            Waiter waiter = new Waiter();
            Entrance entrance = new Entrance();
            Kitchen kitchen = new Kitchen();
            Menu menu = new Menu();
            CreateTable();
            CreateWaiter(waiters);
            entrance.CreateWaitingList(waitingList);

            while (true)
            {
                if (waitingList.Count < 2)
                {
                    entrance.CreateGroup(waitingList);
                }
                entrance.CheckForAvailableWaiter(waiters, tables, waitingList);

                for (int i = 0; i < tables.Count; i++)
                {
                    foreach (var a in tables[i].groupInTable.guests)
                    {
                        if (a.OrderedFood == false)
                        {
                            a.TypeOfFood = a.OrderFood();
                            Console.WriteLine($"{a.Name} har beställt {a.TypeOfFood.FoodName} som kostar {a.TypeOfFood.Price}");
                        }
                    }
                    if (tables[i].Occupied == true && tables[i].GroupHasOrderedFood == false)
                    {
                        tables[i].GroupHasOrderedFood = true;
                        order.Add(tables[i].TableID, tables[i].groupInTable);
                        foreach (KeyValuePair<int, Waiter> kvp in entrance.WaiterAtTable) // loopar igenom dectionaryn WaiterAtTable
                        {
                            //Kollar om det är samma nyckel på order som waiterattable
                            if (order.ContainsKey(tables[i].TableID) == entrance.WaiterAtTable.ContainsKey(tables[i].TableID))
                            {
                                Console.WriteLine($"Table number {kvp.Key} is served by {kvp.Value.Name}");
                                // tar med order samt waiter, alltså value i waiterAtTable
                                waiter.OrderToKitchen(order, kvp.Value);
                                order.Remove(tables[i].TableID); // tar bort beställningen från order som vi gett vidare

                            }
                        }

                    }
                }

                //DrawTables<Table>(startLeft, startTop, tables);
                Draw.DrawingT("Table", startLeft, startTop, tables);
                Draw.Drawing("Kitchen", 65, 0, kitchen.chefs);
                Draw.Drawing<Group>("Waitinglist", 110, 1, waitingList);
                //entrance.DrawWaitingList<Group>("Waitinglist", 110, 1, waitingList);
                Draw.Drawing("Menu", 5, 30, menu.menu);
                Console.ReadKey();
                Console.Clear();
            }
        }


        private void CreateTable()
        {
            for (int i = 0; i < 10; i++)
            {
                if (i < 5)
                {
                    TableForTwo smallTable = new();
                    smallTable.TableID = i + 1;
                    tables.Add(smallTable);
                }
                else
                {
                    TableForFour bigTable = new();
                    bigTable.TableID = i + 1;
                    tables.Add(bigTable);
                }

            }
        }

        internal void CreateWaiter(List<Waiter> waiters)
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

