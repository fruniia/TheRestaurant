using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Entrance : Restaurant
    {
        internal Dictionary<int, Waiter> WaiterAtTable = new();
        internal Entrance() : base()
        {

        }

        internal void CheckWaitingList(List<Group> waitingList)
        {
            if (waitingList.Count < 4)
            {
                CreateGroup(waitingList);
            }
        }
        internal void CreateWaitingList(List<Group> waitingList)
        {
            for (int i = 0; i < 6; i++)
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
        internal void HandleWaiter(List<Waiter> waiters, List<Table> tables, List<Group> waitingList, bool foodInTheHatch, List<Chef> chefs)
        {
            foreach (Waiter waiter in waiters)
            {
                if (waiter.Available == true && waiter.AtEntrance == true && waiter.HoldsFood == false && foodInTheHatch == false)
                {
                    CheckForEmptyTable(tables, waitingList, waiter);
                    if (TickCounter < 3)
                        break;
                }
                else if (waiter.Available == true && foodInTheHatch == true)
                {
                    waiter.GetFoodFromHatch(waiter, chefs);
                    foodInTheHatch = false;
                }

                else if (waiter.HasOrderToKitchen == true && waiter.AtKitchen == true)
                {
                    waiter.LeaveOrderToKitchen(waiter);
                }

                else if (waiter.AtKitchen == true && waiter.HoldsFood == true)
                {
                    waiter.ServeFood(waiter, WaiterAtTable, tables);
                }

            }
        }
        private void CheckForEmptyTable(List<Table> tables, List<Group> waitingList, Waiter waiter)
        {
            waiter.AtTable = false;
            waiter.AtEntrance = true;
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waiter.Available == true && waiter.HasOrderToKitchen == false)
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
            waiter.AtEntrance = false;
            waiter.AtTable = true;
            waiter.Available = false;
            tables[tIndex].Occupied = true;
            tables[tIndex].groupInTable.guests = waitingList[wIndex].guests;

            //flyttade hit texten så den kommer samtidigt som vi sätter gästerna vid bordet. 
            Console.WriteLine($"Table number {tIndex + 1} is served by {waiter.Name}");
            GroupDecidesFood(tables);
            WaiterAtTable.Add(tables[tIndex].TableID, waiter);
            RemoveFromWaitingList(waitingList, wIndex);
        }

        private static void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
        }
    }
}
