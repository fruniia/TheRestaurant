using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Entrance : Restaurant
    {
        public Entrance() : base()
        {
            List<Waiter> waiters = new();
            List<Group> waitingList = new();
            for (int i = 0; i < 6; i++)
            {
                CreateGroup(waitingList);
            }
            DrawWaitingList<Group>("Waitinglist", 100, 1, waitingList);
        }
        public void DrawWaitingList<T>(string header, int fromLeft, int fromTop, List<T> anyList)
        {
            string[] graphics = new string[anyList.Count];

            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Group)
                {
                    var groups = (anyList[i] as Group).guests;
                    foreach (var g in groups)
                    {
                        graphics[i] = $"{groups.Count}  {g.Name}";
                    }
                }
            }
            GUI.Draw(header, fromLeft, fromTop, graphics);
        }
        public void CreateGroup(List<Group> waitingList)
        {
            Group group = new Group();
            group.CreateGuest();
            waitingList.Add(group);
        }
        public void AvailableWaiter(List<Waiter> waiters, List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < waiters.Count; i++)
            {
                if (waiters[i].Available == true)
                {
                    if (waitingList is not null)
                    {
                        waiters[i].Available = false;
                        CheckForEmptyTable(tables, waitingList);
                    }
                    else
                    {
                        continue;
                    }
                }
            }
        }
        public void CheckForEmptyTable(List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false)
                {
                    for (int j = 0; j < waitingList.Count; j++)
                    {
                        if (tables[i] is TableForTwo && waitingList[j].guests.Count <= 2)
                        {
                            HandleWaitingList(tables, waitingList, i, j);
                            RemoveFromWaitingList(waitingList, j);
                        }
                        else if (tables[i] is TableForFour && waitingList[j].guests.Count <= 4)
                        {
                            HandleWaitingList(tables, waitingList, i, j);
                            RemoveFromWaitingList(waitingList, j);
                        }
                    }
                }
            }
        }
        public void HandleWaitingList(List<Table> tables, List<Group> waitingList, int tableIndex, int wlIndex)
        {
            tables[tableIndex].Occupied = true;
            tables[tableIndex].groupInTable.guests = waitingList[wlIndex].guests;
        }
        public void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
        }
    }
}
