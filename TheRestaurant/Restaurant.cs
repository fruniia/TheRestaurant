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
        internal Random random = new();
        protected int maxNumberOfGuests = 80;
        private int NumberOfWaiters { get; set; }
        readonly private int startTop = 12;
        readonly private int startLeft = 5;
        protected int TickCounter { get; set; }
        readonly List<Table> tables = new();
        readonly List<Waiter> waiters = new();
        readonly List<Group> waitingList = new();
        internal Restaurant()
        {
            TickCounter = 0;
            NumberOfWaiters = 3;
        }
        public void Start()
        {
            bool restaurantLoop = true;
            Entrance entrance = new();
            Kitchen kitchen = new();
            Menu menu = new();
            Order order = new();
            Register register = new();
            CreateTable();
            CreateWaiter(waiters);
            entrance.CreateGroupForWaitingList(waitingList);
            while (restaurantLoop)
            {
                Draw.DrawingT("Table", startLeft, startTop, tables);
                Draw.Drawing("Kitchen", 40, 0, kitchen.chefs);
                Draw.Drawing<Group>("Waitinglist", 72, 0, waitingList);
                Draw.Drawing("Menu", 5, 0, menu.menu);
                Draw.Drawing("Waiters", 30, 20, waiters);
                DisplayResturantsRevenueAndTip(register);
                restaurantLoop = entrance.WentToMcDonalds(restaurantLoop);
                restaurantLoop = entrance.CheckGuestCount(tables, restaurantLoop);
                Console.ReadKey();
                //Thread.Sleep(200);
                Console.Clear();
                Console.SetCursorPosition(0, 35);
                kitchen.HandlingChef(order.Orderlist);
                CheckTablesForOrders(order.Orderlist, waiters);
                entrance.HandleWaiter(waiters, tables, waitingList, kitchen, order.Orderlist);
                EatingFood(tables, entrance.WaiterAtTable, order.Orderlist, register);
                entrance.CheckWaitingList(waitingList);
                TickCounter++;
            }
            Outro(entrance.TotalGuests, register.TonightsRevenue, register.TonightsTotalTip);
        }

        internal static void Outro(int totalguests, int revenue, int tip)
        {
            Console.SetCursorPosition(0, 7);
            Console.WriteLine($"\tThe Restaurant is closed for the evening and we want to thank all of guests who has visited us. \n" +
                $"\tTonight we served {totalguests} wonderful guests, who added {tip} SEK in tips to our total revenue of {revenue} SEK\n" +
                $"\n\tPlease, come again.\n\n\n\n\n\n");
        }

        private static void DisplayResturantsRevenueAndTip(Register register)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.SetCursorPosition(80, 35);
            Console.WriteLine($"Tonights Revenue: {register.TonightsRevenue} SEK");
            Console.SetCursorPosition(80, 36);
            Console.WriteLine($"of which {register.TonightsTotalTip} SEK is tip from happy guests.");
            Console.ResetColor();
        }
        private static void EatingFood(List<Table> tables, Dictionary<int, Waiter> waiterAtTable, Dictionary<int, Group> orderlist, Register register)
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
                    foreach (var guest in table.groupInTable.guests)
                    {
                        if (guest.OrderedFood == false)
                        {
                            guest.TypeOfFood = guest.OrderFood();
                            guest.DrawOrderFood(); 
                            table.groupInTable.TotalPrice += guest.TypeOfFood.Price;
                        }
                    }
                    table.GroupHasOrderedFood = true;
                    orderlist.Add(table.TableID, table.groupInTable);

                    foreach (var waiter in waiters)
                    {
                        if (waiter.AtTable == true && waiter.HasOrderToKitchen == false)
                        {
                            waiter.HasOrderToKitchen = true;
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
            for (int i = 0; i < NumberOfWaiters; i++)
            {
                Waiter waiter = new();
                waiters.Add(waiter);
            }
            return waiters;
        }
    }
}

