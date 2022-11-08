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
        public Entrance() : base()
        {

        }

        public void CreateWaitingList(List<Group> waitingList)
        {
            for (int i = 0; i < 6; i++)
            {
                CreateGroup(waitingList);
            }
        }
        public void CreateGroup(List<Group> waitingList)
        {
            Group group = new();
            group.CreateGuest();
            waitingList.Add(group);
        }
        internal void HandleWaiter(List<Waiter> waiters, List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < waiters.Count; i++)
            {
                if (waiters[i].Available == true && waiters[i].HasOrderToKitchen == false)
                {
                    CheckForEmptyTable(tables, waitingList, waiters[i]);
                    break;
                }
            }
        }
        private void CheckForEmptyTable(List<Table> tables, List<Group> waitingList, Waiter waiter)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waiter.Available == true && waiter.HasOrderToKitchen == false)
                {
                    for (int j = 0; j < waitingList.Count; j++)
                    {

                        if (tables[i] is TableForTwo && waitingList[j].guests.Count <= tables[i].MaxNumberOfGuestsAtTable && tables[i].Occupied == false)
                        {
                            HandleWaitingList(tables, waitingList, i, j, waiter);
                        }
                        else if (tables[i] is TableForFour && waitingList[j].guests.Count <= tables[i].MaxNumberOfGuestsAtTable && tables[i].Occupied == false)
                        {
                            HandleWaitingList(tables, waitingList, i, j, waiter);
                        }
                    }
                }
            }
        }
        private void HandleWaitingList(List<Table> tables, List<Group> waitingList, int tIndex, int wIndex, Waiter waiter)
        {
            waiter.AtEntrance = false;
            waiter.AtTable = true;
            waiter.Available = false;
            tables[tIndex].Occupied = true;
            tables[tIndex].groupInTable.guests = waitingList[wIndex].guests;
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
            WaiterAtTable.Add(tables[tIndex].TableID, waiter);
            RemoveFromWaitingList(waitingList, wIndex);
        }
        private static void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
        }
    }
}
