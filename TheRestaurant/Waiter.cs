using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Waiter : Person
    {
        internal int ServiceLevel { get; set; }
        internal bool HasOrderToKitchen { get; set; }
        internal bool AtKitchen { get; set; }
        internal bool AtEntrance { get; set; }
        internal bool AtTable { get; set; }
        internal bool HoldsFood { get; set; }
        private bool FoundATable { get; set; }
        private bool TakesFoodFromHatch { get; set; }
        readonly private string[] waiterInAction = { "at a table", "at the entrance", "in the kitchen" };

        internal Waiter() : base()
        {
            TimeEstimate = 3;
            HasOrderToKitchen = false;
            AtKitchen = false;
            AtEntrance = true;
            AtTable = false;
            HoldsFood = false;
            ServiceLevel = 5;
        }

        internal string WaiterInAction()
        {
            string action = WaiterPlace();
            return action;
        }
        private string WaiterPlace()
        {
            for (int i = 0; i < waiterInAction.Length; i++)
            {
                if (AtTable == true)
                {
                    return waiterInAction[0];
                }
                if (AtEntrance == true)
                {
                    return waiterInAction[1];
                }
                if (AtKitchen == true)
                {
                    return waiterInAction[2];
                }
            }
            return "";
        }
        internal void ServeFood(Waiter waiter, List<Table> tables, Dictionary<int, Group> orderlist)
        {
            waiter.SetWaiterToTable();
            foreach (var kvp in orderlist)
            {
                foreach (Table table in tables)
                {
                    if (kvp.Key == table.TableID && table.GroupHasGotFood == false && FoundATable == false)
                    {
                        foreach (var guest in kvp.Value.guests)
                        {
                            if (guest.GotFood == false)
                            {
                                Console.WriteLine($"Waiter {waiter.Name} serves {guest.TypeOfFood.FoodName} to {guest.Name} at table {kvp.Key}");
                                guest.GotFood = true;
                                table.GroupHasGotFood = true;
                                HoldsFood = false;
                                FoundATable = true;
                            }
                        }
                    }
                    if (FoundATable == true)
                    {
                        break;
                    }
                }
            }
            FoundATable = false;
        }
        internal void GetFoodFromHatch(Waiter waiter, List<Chef> chefs, List<Table> tables, Dictionary<int, Group> orderlist)
        {
            foreach (Chef chef in chefs)
            {
                if (chef.FoodDone == true)
                {
                    foreach (Table table in tables)
                    {
                        if (table.GroupHasOrderedFood == true && table.GroupHasGotFood == false)
                        {
                            foreach (var kvp in orderlist)
                            {
                                if (table.TableID == kvp.Key && TakesFoodFromHatch == false)
                                {
                                    waiter.SetWaiterToKitchen();
                                    HoldsFood = true;
                                    TakesFoodFromHatch = true;
                                    chef.FoodDone = false;
                                }
                            }
                        }
                    }
                }
            }
            TakesFoodFromHatch = false;
        }
        internal void SetWaiterToTable()
        {
            AtTable = true;
            AtKitchen = false;
            AtEntrance = false;
        }
        internal void SetWaiterToEntrance()
        {
            AtEntrance = true;
            AtTable = false;
            AtKitchen = false;
        }
        internal void SetWaiterToKitchen()
        {
            AtKitchen = true;
            AtTable = false;
            AtEntrance = false;
        }
        //internal void CleaningTable(Table table)
        //{
        //    table.GroupHasGotFood = false;
        //    table.GroupHasOrderedFood = false;
        //    table.groupInTable.TotalPrice = 0;
        //    table.groupInTable.FoodIsReady = false;
        //}
    }
}
