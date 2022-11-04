﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Entrance : Restaurant
    {
        Dictionary<Table, Waiter> WaiterAtTable = new Dictionary<Table, Waiter>();
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
            Group group = new Group();
            group.CreateGuest();
            waitingList.Add(group);
        }
        public void CheckForAvailableWaiter(List<Waiter> waiters, List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < waiters.Count; i++)
            {
                if (waiters[i].Available == true)
                {
                    CheckForEmptyTable(tables, waitingList, waiters[i]);
                    break;
                }
            }
        }
        public void CheckForEmptyTable(List<Table> tables, List<Group> waitingList, Waiter waiter)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waiter.Available == true)
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
        public void HandleWaitingList(List<Table> tables, List<Group> waitingList, int tableIndex, int wlIndex, Waiter waiter)
        {
            waiter.Available = false;
            tables[tableIndex].Occupied = true;
            tables[tableIndex].groupInTable.guests = waitingList[wlIndex].guests;
            WaiterAtTable.Add(tables[tableIndex], waiter);
            RemoveFromWaitingList(waitingList, wlIndex);

            //Utskriftsversion som visar när servitör tar emot gäster
            //string p = "";
            //for (int i = 0; i < tables[tableIndex].groupInTable.guests.Count; i++)
            //{
            //    p += tables[tableIndex].groupInTable.guests[i].Name + " ";
            //}
            //Console.WriteLine("Vid bord nummer: " + (tableIndex + 1) + " sitter " + p + " serveras av " + waiter.Name);
        }
        public void RemoveFromWaitingList(List<Group> waitingList, int index)
        {
            waitingList.Remove(waitingList[index]);
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
    }
}
