using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Entrance : Restaurant
    {
        internal Dictionary<int, Waiter> WaiterAtTable = new();
        public bool IsOpened { get; set; }
        public int TotalGuests { get; set; }
        public int GuestsLeaveCount { get; set; }
        public bool EveryOneHasLeft { get; set; }
        public bool GroupsWentToMcDonalds { get; set; }
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
                Console.ReadKey();
                restaurantLoop = false;
            }
            return restaurantLoop;
        }
        internal void CheckWaitingList(List<Group> waitingList)
        {
            if (waitingList.Count < 6 && IsOpened == true)
            {
                CreateGroup(waitingList);
            }
            else if (waitingList.Count != 0 && IsOpened == false)
            {
                waitingList.RemoveAt(0);
            }
        }
        internal void CreateWaitingList(List<Group> waitingList)
        {
            for (int i = 0; i < 1; i++)
            {
                CreateGroup(waitingList);
            }
        }
        private void CreateGroup(List<Group> waitingList)
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
                        waiter.SetWaiterToKitchen(waiter);
                    }
                    else
                    {
                        waiter.SetWaiterToEntrance(waiter);
                    }
                }
                else if (waiter.AtKitchen == true)
                {
                    if (waiter.HoldsFood == true)
                    {
                        waiter.ServeFood(waiter, kitchen.chefs, tables, orderlist);
                    }
                    else
                    {
                        waiter.HasOrderToKitchen = false;
                        waiter.SetWaiterToEntrance(waiter);
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
            waiter.SetWaiterToTable(waiter);
            tables[tIndex].Occupied = true;
            tables[tIndex].groupInTable.guests = waitingList[wIndex].guests;
            TotalGuests += tables[tIndex].groupInTable.guests.Count;
            tables[tIndex].groupInTable.GroupExperience += tables[tIndex].QualityLevel;

            Console.WriteLine($"Table number {tIndex + 1} is served by {waiter.Name}");

            WaiterAtTable.Add(tables[tIndex].TableID, waiter);
            RemoveFromWaitingList(waitingList, wIndex);
        }
        private static void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
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
                            Console.ReadKey();
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
                        Console.ReadKey();
                        Console.ResetColor();
                        restaurantLoop = false;
                    }
                }
            }
            return restaurantLoop;


        }
    }
}
