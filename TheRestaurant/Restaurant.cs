using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Restaurant
    {
        protected int maxNumberOfGuests = 80;
        int startTop = 5;
        int startLeft = 5;
        List<Table> tables = new();
        //protected string? FacilityName { get; set; }
        List<Waiter> waiters = new();
        List<Chef> chefs = new();


        public Restaurant()
        {
            ////Entrance entrance = new();

            //CreateWaiter(waiters);
        }

        public void Start()
        {
            Menu();
            CreateTable();
            CreateWaiter(waiters);
            CreateChef(chefs);
            
            for (int i = 0; i < tables.Count; i++)
            {
                DrawTables<Table>($"Table {i+1}", startLeft, startTop, tables);
                startLeft += 20;
                if (startLeft > 90)
                {
                    startTop += 15;
                    startLeft = 5;
                }
            }
        }


        public void Menu()
        {
            List<Food> menu = new();
            menu.Add(new Fish("Fish and pasta", 199));
            menu.Add(new Fish("Fish and chips", 259));
            menu.Add(new Fish("Fish and rice", 219));
            menu.Add(new Meat("Meat and pasta", 299));
            menu.Add(new Meat("Meat and chips", 179));
            menu.Add(new Meat("Meat and rice", 329));
            menu.Add(new Vegetarian("Beans and pasta", 179));
            menu.Add(new Vegetarian("Beans and greens", 199));
            menu.Add(new Vegetarian("Just beans", 129));
        }

        private void CreateTable()
        {
            for (int i = 0; i < 5; i++)
            {
                TableForTwo smallTable = new();
                tables.Add(smallTable);
                TableForFour bigTable = new();
                tables.Add(bigTable);
            }
        }

        private void CreateWaiter(List<Waiter> waiters)
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiters.Add(waiter);
            }
        }

        public void CreateChef(List<Chef> chefs)
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefs.Add(chef);
            }
        }

        public void DrawTables<T>(string header, int fromLeft, int fromTop, List<T> anyList)
        {
            string[] graphics = new string[anyList.Count];

            for (int i = 0; i < anyList.Count; i++)
            {
                if (anyList[i] is Table)
                {
                    graphics[i] = $"{tables.Count}";
                }
            }
            GUI.Draw(header, fromLeft, fromTop, graphics);
        }
    }
}

