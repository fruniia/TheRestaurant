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
            HasOrderToKitchen = false;
            AtKitchen = false;
            AtEntrance = true;
            AtTable = false;
            HoldsFood = false;
        }
        internal void ServeFood(Waiter waiter, List<Chef> chefs, List<Table> tables)
        {
            waiter.AtKitchen = false;
            waiter.AtTable = true;
            waiter.HoldsFood = false;
            waiter.Available = false;
            foreach (Chef chef in chefs)
            {
                if (chef.FoodDone == true)
                {
                    foreach (var kvp in chef.PreparingFood)
                    {
                        foreach (var g in kvp.Value.guests)
                        {
                            Console.WriteLine($"Waiter {waiter.Name} serves {g.TypeOfFood.FoodName} to {g.Name}" +
                                $" at table {kvp.Key}");
                            g.GotFood = true;
                        }
                        foreach (Table table in tables)
                        {
                            if (kvp.Key == table.TableID)
                            {
                                table.GroupHasGotFood = true;
                                chef.PreparingFood.Remove(kvp.Key); //Spara mat och pris i en variabel - Gör en nota.
                            }
                        }
                    chef.FoodDone = false;
                    break;
                    }
                }
            }
        }
        internal void GetFoodFromHatch(Waiter waiter, List<Chef> chefs, List<Table> tables)
        {
            foreach (Chef chef in chefs)
            {
                if (chef.FoodDone == true)
                {
                    waiter.AtEntrance = false;
                    waiter.Available = false;
                    waiter.AtKitchen = true;
                    waiter.HoldsFood = true;
                    //chef.FoodDone = false;
                }
            }
        }
        internal void LeaveOrderToKitchen(Waiter waiter)
        {
            waiter.AtEntrance = true;
            waiter.HasOrderToKitchen = false;
            waiter.AtKitchen = false;
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
