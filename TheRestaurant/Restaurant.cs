using Microsoft.Win32;
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
        internal int maxNumberOfGuests = 80;
        int startTop = 12;
        int startLeft = 5;
        protected int TickCounter { get; set; }
        List<Table> tables = new();
        List<Waiter> waiters = new();
        List<Group> waitingList = new();
        public Restaurant()
        {
            TickCounter = 0;
        }
        public void Start()
        {
            Waiter waiter = new();
            Entrance entrance = new();
            Kitchen kitchen = new();
            Menu menu = new();
            Order order = new();
            Register register = new();
            CreateTable();
            CreateWaiter(waiters);
            entrance.CreateWaitingList(waitingList);

            while (true)
            {
                Draw.DrawingT("Table", startLeft, startTop, tables);
                Draw.Drawing("Kitchen", 40, 0, kitchen.chefs);
                Draw.Drawing<Group>("Waitinglist", 72, 0, waitingList);
                Draw.Drawing("Menu", 5, 0, menu.menu);
                CheckPosition(waiters);
                entrance.CheckGuestCount();
                DisplayResturantsRevenueAndTip(register);
                //Console.ReadKey();
                Thread.Sleep(200);
                Console.Clear();
                Console.SetCursorPosition(0, 35);
                kitchen.HandlingChef(order.Orderlist);
                CheckTablesForOrders(order.Orderlist, waiters);
                entrance.HandleWaiter(waiters, tables, waitingList, kitchen, order.Orderlist);
                EatingFood(tables, entrance.WaiterAtTable, order.Orderlist, register);
                entrance.CheckWaitingList(waitingList);
                TickCounter++;
            }
        }
        private void DisplayResturantsRevenueAndTip(Register register)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(80, 35);
            Console.WriteLine($"Tonights Revenue: {register.TonightsRevenue} SEK");
            Console.SetCursorPosition(80, 36);
            Console.WriteLine($"of which {register.TonightsTotalTip} SEK is tip from happy guests.");
            Console.ResetColor();
        }
        private void CheckPosition(List<Waiter> waiters)
        {
            int i = 20;
            foreach (Waiter waiter in waiters)
            {
                Console.SetCursorPosition(30, i);
                if (waiter.AtEntrance == true)
                {
                    Console.WriteLine($"Waiter {waiter.Name} is at the entrance");
                }
                else if (waiter.AtKitchen == true)
                {
                    Console.WriteLine($"Waiter {waiter.Name} is at the kitchen");
                }
                else if (waiter.AtTable == true)
                {
                    Console.WriteLine($"Waiter {waiter.Name} is at a table");
                }
                i++;
            }
        }
        private void EatingFood(List<Table> tables, Dictionary<int, Waiter> waiterAtTable, Dictionary<int, Group> orderlist, Register register)
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

                            Console.WriteLine($"Table number {table.TableID} is finished eating.");
                            table.Occupied = false;
                            table.groupInTable.GroupExperience += waiterAtTable[table.TableID].ServiceLevel;
                            table.groupInTable.GroupExperience /= 3;
                            register.CalculateRevenue(table);
                            table.GroupHasGotFood = false;
                            table.GroupHasOrderedFood = false;
                            table.groupInTable.TotalPrice = 0;
                            table.groupInTable.FoodIsReady = false;
                            orderlist.Remove(table.TableID);
                            waiterAtTable.Remove(table.TableID);
                            table.groupInTable.guests.Clear();
                            break;
                        }
                    }
                }
            }
        }
        private void CheckTablesForOrders(Dictionary<int, Group> orderlist, List<Waiter> waiters)
        {
            foreach (Table table in tables)
            {
                if (table.Occupied == true && table.GroupHasOrderedFood == false)
                {
                    foreach (var a in table.groupInTable.guests)
                    {
                        if (a.OrderedFood == false)
                        {
                            a.TypeOfFood = a.OrderFood();
                            a.DrawOrderFood(); //Gjorde en metod DrawOrderFood i Guest för utskriften av maten
                            table.groupInTable.TotalPrice += a.TypeOfFood.Price;
                        }
                    }
                    table.GroupHasOrderedFood = true;
                    orderlist.Add(table.TableID, table.groupInTable);

                    foreach (var w in waiters) // loopar igenom dictionaryn WaiterAtTable
                    {
                        if (w.AtTable == true && w.HasOrderToKitchen == false)
                        {
                            w.HasOrderToKitchen = true;
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

        internal List<Waiter> CreateWaiter(List<Waiter> waiters)
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiters.Add(waiter);
            }
            return waiters;
        }
    }
}

