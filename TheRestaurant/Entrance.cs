using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Entrance : Restaurant
    {
        internal Dictionary<int, Waiter> WaiterAtTable = new();
        private bool IsOpened { get; set; }
        internal int TotalGuests { get; set; }
        private int GuestsLeaveCount { get; set; }
        private bool EveryOneHasLeft { get; set; }
        private bool GroupsWentToMcDonalds { get; set; }
        internal Entrance() : base()
        {
            IsOpened = true;
            TotalGuests = 0;
        }
        internal bool WentToMcDonalds(bool restaurantLoop)
        {
            if (EveryOneHasLeft == true)
            {
                Console.SetCursorPosition(0, 35);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"The guests went to McDonalds instead.");
                Console.ResetColor();
                Console.ReadKey(true);
                restaurantLoop = false;
            }
            return restaurantLoop;
        }
        internal void CheckWaitingList(List<Group> waitingList)
        {
            if (waitingList.Count < 6 && IsOpened == true)
            {
                CreateGroupForWaitingList(waitingList);
            }
            else if (waitingList.Count != 0 && IsOpened == false)
            {
                waitingList.RemoveAt(0);
            }
        }
        internal void CreateGroupForWaitingList(List<Group> waitingList)
        {
            Group group = new();
            group.CreateGuest();
            waitingList.Add(group);
        }
        internal void HandleWaiter(List<Waiter> waiters, List<Table> tables, List<Group> waitingList, Kitchen kitchen, Dictionary<int, Group> orderlist)
        {
            foreach (Waiter waiter in waiters)
            {
                if (waiter.AtEntrance == true && waiter.HasOrderToKitchen == false)
                {
                    if (kitchen.FoodInTheHatch == true)
                    {
                        waiter.GetFoodFromHatch(waiter, kitchen.chefs, tables, orderlist);
                        kitchen.FoodInTheHatch = false;
                    }
                    else if (IsOpened == true)
                    {
                        CheckForEmptyTable(tables, waitingList, waiter);
                    }

                }
                else if (waiter.AtTable == true)
                {
                    if (waiter.HasOrderToKitchen == true)
                    {
                        waiter.SetWaiterToKitchen();
                    }
                    else
                    {
                        waiter.SetWaiterToEntrance();
                    }
                }
                else if (waiter.AtKitchen == true)
                {
                    if (waiter.HoldsFood == true)
                    {
                        waiter.ServeFood(waiter, tables, orderlist);
                    }
                    else
                    {
                        waiter.HasOrderToKitchen = false;
                        waiter.SetWaiterToEntrance();
                    }
                }
            }
        }
        private void CheckForEmptyTable(List<Table> tables, List<Group> waitingList, Waiter waiter)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waiter.HasOrderToKitchen == false && waiter.AtEntrance == true)
                {
                    for (int j = 0; j < waitingList.Count; j++)
                    {
                        if (tables[i] is TableForTwo && waitingList[j].guests.Count <= tables[i].MaxNumberOfGuestsAtTable && tables[i].Occupied == false)
                        {
                            ShowGuestsToTable(tables, waitingList, i, j, waiter);
                        }
                        else if (tables[i] is TableForFour && waitingList[j].guests.Count <= tables[i].MaxNumberOfGuestsAtTable && tables[i].Occupied == false)
                        {
                            ShowGuestsToTable(tables, waitingList, i, j, waiter);
                        }
                    }
                }
            }
        }
        private void ShowGuestsToTable(List<Table> tables, List<Group> waitingList, int tIndex, int wIndex, Waiter waiter)
        {
            waiter.SetWaiterToTable();
            tables[tIndex].Occupied = true;
            tables[tIndex].groupInTable.guests = waitingList[wIndex].guests;
            TotalGuests += tables[tIndex].groupInTable.guests.Count;
            tables[tIndex].groupInTable.GroupExperience += tables[tIndex].QualityLevel;
            Console.WriteLine($"Table number {tIndex + 1} is served by {waiter.Name}");
            WaiterAtTable.Add(tables[tIndex].TableID, waiter);
            waitingList.Remove(waitingList[wIndex]);
        }

        internal bool CheckGuestCount(List<Table> tables, bool restaurantLoop)
        {
            Console.SetCursorPosition(30, 18);
            if (TotalGuests < maxNumberOfGuests)
            {
                Console.WriteLine($"{TotalGuests} guests has entered the restaurant tonight");
            }
            else
            {
                IsOpened = false;
                Console.WriteLine($"The restaurant has filled tonights seats");
            }
            if (TotalGuests >= maxNumberOfGuests)
            {
                GuestsLeaveCount++;
                if (GuestsLeaveCount == 40)
                {
                    foreach (var table in tables)
                    {
                        if (table.Occupied == true)
                        {
                            table.groupInTable.guests.Clear();
                            Console.SetCursorPosition(0, 35);
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine($"The rest of the table/tables didn't get their food and became angry!!");
                            Console.ReadKey(true);
                            Console.ResetColor();
                            EveryOneHasLeft = true;
                            GroupsWentToMcDonalds = true;
                        }
                    }
                    if (GroupsWentToMcDonalds == false)
                    {
                        Console.SetCursorPosition(0, 35);
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("The Restaurant has closed for tonight");
                        Console.ReadKey(true);
                        Console.ResetColor();
                        restaurantLoop = false;
                    }
                }
            }
            return restaurantLoop;
        }
    }
}
