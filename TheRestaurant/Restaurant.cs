using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheRestaurant
{
    internal class Restaurant
    {
        protected int maxNumberOfGuests = 30;
        protected string? FacilityName { get; set; }

        public Restaurant()
        {
            Menu();
            CreateTable();

            List<Group> waitingList = new();
            CreateGroup(waitingList);

            List<Chef> chefs = new();
            CreateChef(chefs);

            List<Waiter> waiters = new();
            CreateWaiter(waiters);
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

        public void CreateTable()
        {
            List<Table> tables = new();
            for (int i = 0; i < 5; i++)
            {
                TableForTwo smallTable = new();
                tables.Add(smallTable);
                TableForFour bigTable = new();
                tables.Add(bigTable);
            }
        }
        public void CreateChef(List<Chef> chefList)
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefList.Add(chef);
            }
        }
        public void CreateWaiter(List<Waiter> waiterList)
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiterList.Add(waiter);
            }
        }
        public void CreateGroup(List<Group> waitingList)
        {
            for (int i = 0; i < 5; i++)
            {
                Group group = new Group();
                group.CreateGuest();
                waitingList.Add(group);
            }
        }

    }

}

