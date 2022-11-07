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
        Random random = new Random();
        int startTop = 12;
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

            while (true) //ändrat lite ordning så vi ritar ut tom restaurang först
            {
                Draw.DrawingT("Table", startLeft, startTop, tables);
                Draw.Drawing("Kitchen", 40, 0, kitchen.chefs);
                Draw.Drawing<Group>("Waitinglist", 70, 0, waitingList);
                Draw.Drawing("Menu", 5, 0, menu.menu);
                Console.ReadKey();
                Console.Clear();
                Console.SetCursorPosition(0, 33);
                if (waitingList.Count < 4)
                {
                    entrance.CreateGroup(waitingList);
                }
                foreach (var chef in kitchen.chefs)
                {
                    if (chef.Available == true)
                    {

                    }
                    else
                    {
                        chef.TimeEstimate--;
                        if(chef.TimeEstimate == 0)
                        {
                            chef.TimeEstimate = 10;
                            chef.Available = true;
                        }
                    }
                }
                kitchen.CookingFood();
                waiter.LeaveOrder(kitchen.bongQueue, waiters); //ny metod för att lämna order till kök
                for (int i = 0; i < tables.Count; i++)
                {
                    foreach (var a in tables[i].groupInTable.guests)
                    {
                        if (a.OrderedFood == false)
                        {
                            a.TypeOfFood = a.OrderFood();
                            a.DrawOrderFood(); //Gjorde en metod DrawOrderFood i Guest för utskriften av maten
                        }
                    }
                    if (tables[i].Occupied == true && tables[i].GroupHasOrderedFood == false)
                    {
                        tables[i].GroupHasOrderedFood = true;
                        order.Add(tables[i].TableID, tables[i].groupInTable);
                        foreach (KeyValuePair<int, Waiter> kvp in entrance.WaiterAtTable) // loopar igenom dictionaryn WaiterAtTable
                        {
                            Console.WriteLine($"Table number {kvp.Key} is served by {kvp.Value.Name}");
                            // tar med order samt waiter, alltså value i waiterAtTable
                            waiter.OrderToKitchen(order, kvp.Value);
                        }

                    }
                } //beställ mat
                entrance.CheckForAvailableWaiter(waiters, tables, waitingList);


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

