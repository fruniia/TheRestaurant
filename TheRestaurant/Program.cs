namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Restaurant restaurant = new();

            List<Restaurant> tables = new();
            List<Group> waitingList = new();
            List<Chef> chefs = new();
            List<Waiter> waiters = new();

            

            foreach (Food food in menu)
            {
                Console.WriteLine(food.FoodName + " kostar " + food.Price + " sek");
            }
            Console.WriteLine();

            for (int i = 0; i < 5; i++)
            {
                Group group = new Group();
                group.CreateGuest();
                waitingList.Add(group);
            }

            foreach (var wl in waitingList)
            {

                foreach (var guest in wl.guests)
                {
                    Console.WriteLine(guest.Name + " " + guest.Money + " sek");
                }

                Console.WriteLine();
            }


        }

        public void CreateTable(List<Table> emptyTables, Restaurant restaurant)
        {
            for (int i = 0; i < 5; i++)
            {
                TableForTwo smallTable = new();
                emptyTables.Add(smallTable);
                TableForFour bigTable = new();
                emptyTables.Add(bigTable);
            }

            foreach (var table in emptyTables)
            {
                Console.WriteLine(table.GetType().Name);
            }
        }


        public void CreateChef(List<Chef> chefList)
        {
            for (int i = 0; i < 5; i++)
            {
                Chef chef = new Chef();
                chefList.Add(chef);
            }
            foreach (var c in chefList)
            {
                Console.WriteLine($"{c.GetType().Name} {c.Name}");
            }
        }

        public void CreateWaiter(List<Waiter> waiterList)
        {
            for (int i = 0; i < 3; i++)
            {
                Waiter waiter = new Waiter();
                waiterList.Add(waiter);
            }
            foreach (var w in waiterList)
            {
                Console.WriteLine($"{w.GetType().Name} {w.Name}");
            }
        }

        public void CheckForEmptyTable(List<Table> tables, List<Group> waitingList)
        {
            for (int i = 0; i < tables.Count; i++)
            {
                if (tables[i].Occupied == false && waitingList is not null)
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