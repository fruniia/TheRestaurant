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
        int startTop = 12;
        int startLeft = 5;
        protected int TickCounter { get; set; }
        List<Table> tables = new();
        List<Waiter> waiters = new();
        List<Group> waitingList = new();
        //Dictionary<int, Group> order = new();


        public Restaurant()
        {
            TickCounter = 0;
        }

        public void Start()
        {
            Waiter waiter = new Waiter();
            Entrance entrance = new Entrance();
            Kitchen kitchen = new Kitchen();
            Menu menu = new Menu();
            Order order = new Order();
            CreateTable();
            CreateWaiter(waiters);
            entrance.CreateWaitingList(waitingList);

            while (true) 
            {
                Draw.DrawingT("Table", startLeft, startTop, tables);
                Draw.Drawing("Kitchen", 40, 0, kitchen.chefs);
                Draw.Drawing<Group>("Waitinglist", 72, 0, waitingList);
                Draw.Drawing("Menu", 5, 0, menu.menu);
                Console.SetCursorPosition(0, 33);
                Console.WriteLine($"Antal snurr: {TickCounter}");
                Console.ReadKey();
                Console.Clear();
                Console.SetCursorPosition(0, 35);
                entrance.CheckWaitingList(waitingList);
                kitchen.HandlingChef(order.Orderlist);
                CheckTablesForOrders(order.Orderlist, entrance.WaiterAtTable);
                entrance.HandleWaiter(waiters, tables, waitingList, kitchen.FoodInTheHatch, kitchen.chefs);
                EatingFood(tables);
                TickCounter++;
            }
        }

        private void EatingFood(List<Table> tables)
        {
            foreach (var table in tables)
            {
                foreach (var guest in table.groupInTable.guests)
                {
                    if (guest.GotFood == true)
                    {
                        guest.TimeEstimate--;
                        if (guest.TimeEstimate == 0)
                        
                        {
                            guest.GotFood = false;
                            //table.groupInTable.guests.Remove(guest);
                            Console.WriteLine($"{guest.Name} har ätit klart");

                        }
                    }
                }
            }
        }

        internal void GroupDecidesFood(List<Table> tables)
        {
            foreach (Table table in tables)
            {
                foreach (var a in table.groupInTable.guests)
                {
                    if (a.OrderedFood == false)
                    {
                        a.TypeOfFood = a.OrderFood();
                        a.DrawOrderFood(); //Gjorde en metod DrawOrderFood i Guest för utskriften av maten
                    }
                }
            }
        }
        private void CheckTablesForOrders(Dictionary<int, Group> orderlist, Dictionary<int, Waiter> waiterAtTable)
        {
            foreach (Table table in tables)
            {
                if (table.Occupied == true && table.GroupHasOrderedFood == false)
                {
                    table.GroupHasOrderedFood = true;
                    orderlist.Add(table.TableID, table.groupInTable);
                    foreach (KeyValuePair<int, Waiter> kvp in waiterAtTable) // loopar igenom dictionaryn WaiterAtTable
                    {
                        if (orderlist.ContainsKey(table.TableID) == waiterAtTable.ContainsKey(table.TableID))
                        {
                            kvp.Value.HasOrderToKitchen = true;
                            kvp.Value.AtTable = false;
                            kvp.Value.AtKitchen = true;
                            kvp.Value.Available = false;
                        }
                    }
                }
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
    }
}

