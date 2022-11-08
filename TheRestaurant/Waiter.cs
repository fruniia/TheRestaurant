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
        private int ServiceLevel { get; set; }
        internal bool Available { get; set; }
        internal Dictionary<int, Group> ServingToTable { get; set; }
        internal bool HasOrderToKitchen { get; set; }
        internal bool AtKitchen { get; set; }
        internal bool AtEntrance { get; set; }
        internal bool AtTable { get; set; }
        internal bool HoldsFood { get; set; }


        // Kocken lagar maten(tar 10 i “tid”)
        // Servitören hämtar maten
        // Servitören serverar maten
        // Gästen äter maten(tar 20 i “tid”)
        // Gästen betalar till servitören
        // Kundens krav: Vällagad mat, Bra service, Bra bord, Korta väntetider
        // Om kunden är nöjd så dricksar denne ett visst antal procent
        // Antingen går gästen eller så får hen diska
        // Servitör dukar av bordet(tar 3 I “tid”)

        internal Waiter() : base()
        {
            Available = true;
            TimeEstimate = 3;
            //OrderOnTheGo = new Dictionary<int, Group>();
            HasOrderToKitchen = false;
            AtKitchen = false;
            AtEntrance = true;
            AtTable = false;
            HoldsFood = false;
        }
        internal void ServeFood(Waiter waiter, Dictionary<int, Waiter> waiterAtTable, List<Table> tables)
        {
            waiter.AtKitchen = false;
            waiter.AtTable = true;
            foreach (var kvp in waiter.ServingToTable)
            {
                foreach (var kvp2 in kvp.Value.guests)
                {
                    kvp2.GotFood = true;

                    Console.WriteLine($"Waiter {waiter.Name} serves {kvp2.TypeOfFood.FoodName} to {kvp2.Name}" +
                        $" at table {kvp.Key}");

                    foreach (Table table in tables)
                    {
                        if (kvp.Key == table.TableID)
                        {
                            table.GroupHasGotFood = true;
                        }
                    }


                }
                break;
            }
            waiter.HoldsFood = false;
            waiter.Available = true;
        }
        internal void GetFoodFromHatch(Waiter waiter, List<Chef> chefs)
        {
            foreach (Chef chef in chefs)
            {
                if (chef.FoodDone == true && waiter.Available == true)
                {
                    waiter.Available = false;
                    waiter.AtKitchen = true;
                    waiter.ServingToTable = chef.PreparingFood;
                    waiter.HoldsFood = true;
                    chef.FoodDone = false;
                }
            }
        }
        internal void LeaveOrderToKitchen(Waiter waiter)
        {
            waiter.HasOrderToKitchen = false;
            waiter.AtKitchen = false;
            waiter.AtEntrance = true;
            waiter.Available = true; //sätter man true här fyller man alla bord, men servitör nr 1 gör nästan allt                  
        }

        internal void OrderToKitchen(Waiter waiter)
        {
            waiter.AtTable = false;
            waiter.AtKitchen = true;
            waiter.Available = false;
        }
        internal void BringCheckToTable(Waiter waiter)
        {

        }
        internal void CleaningTable(Waiter waiter)
        {

            //TimeEstimate-- == 0
            waiter.AtTable = false;
        }
    }
}
