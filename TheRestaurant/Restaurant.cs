﻿using System;
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



        public Restaurant()
        {
            TickCounter = 0;
        }

        public void Start()
        {
            int tablenumber = 0;
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
                int i = 33;
                foreach (Waiter w in waiters)
                {
                    foreach (var wat in entrance.WaiterAtTable)
                    {
                        if (w.Name == wat.Value.Name)
                        {
                            tablenumber = wat.Key;
                        }
                    }
                    Console.SetCursorPosition(100, i);
                    Console.WriteLine($"{w.Name} E: {w.AtEntrance} K: {w.AtKitchen} T: {w.AtTable} {tablenumber}" +
                        $" Holdsfood:{w.HoldsFood} OrdertoKitchen:{w.HasOrderToKitchen}");
                    i++;
                }
                Console.ReadKey();
                Console.Clear();
                Console.SetCursorPosition(0, 35);
                kitchen.HandlingChef(order.Orderlist);
                CheckTablesForOrders(order.Orderlist, waiters);
                entrance.HandleWaiter(waiters, tables, waitingList, kitchen, order.Orderlist);
                EatingFood(tables, entrance.WaiterAtTable, order.Orderlist);
                entrance.CheckWaitingList(waitingList);
                TickCounter++;
            }
        }

        private void EatingFood(List<Table> tables, Dictionary<int, Waiter> waiterAtTable, Dictionary<int, Group> orderlist)
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
                            //guest.GotFood = false;
                            Console.WriteLine($"Table number {table.TableID} is finished eating");
                            table.Occupied = true;
                            table.groupInTable.guests.Clear();
                            orderlist.Remove(table.TableID);
                            waiterAtTable.Remove(table.TableID);
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

