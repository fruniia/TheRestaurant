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
        internal bool FoundATable { get; set; }
        internal bool TakesFoodFromHatch { get; set; }

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
        internal void ServeFood(Waiter waiter, List<Chef> chefs, List<Table> tables, Dictionary<int, Group> orderlist)
        {
            waiter.SetWaiterToTable(waiter);
            foreach (var o in orderlist)
            {
                foreach (Table table in tables)
                {
                    if (o.Key == table.TableID && table.GroupHasGotFood == false && FoundATable == false)
                    {
                        foreach (var kvp in o.Value.guests)
                        {
                            if (kvp.GotFood == false)
                            {
                                Console.WriteLine($"Waiter {waiter.Name} serves {kvp.TypeOfFood.FoodName} to {kvp.Name} at table {o.Key}");
                                kvp.GotFood = true;
                                table.GroupHasGotFood = true;
                                waiter.HoldsFood = false;
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
                                if (table.TableID == kvp.Key && waiter.TakesFoodFromHatch == false)
                                {
                                    waiter.SetWaiterToKitchen(waiter);
                                    waiter.HoldsFood = true;
                                    waiter.TakesFoodFromHatch = true;
                                    chef.FoodDone = false;
                                }
                            }
                        }
                    }
                    //chef.FoodDone = false;
                }
            }
            waiter.TakesFoodFromHatch = false;
        }
        internal void SetWaiterToTable(Waiter waiter)
        {
            waiter.AtTable = true;
            waiter.AtKitchen = false;
            waiter.AtEntrance = false;
        }
        internal void SetWaiterToEntrance(Waiter waiter)
        {
            waiter.AtEntrance = true;
            waiter.AtTable = false;
            waiter.AtKitchen = false;
        }
        internal void SetWaiterToKitchen(Waiter waiter)
        {
            waiter.AtKitchen = true;
            waiter.AtTable = false;
            waiter.AtEntrance = false;
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
