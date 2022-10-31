namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Restaurant restaurant = new Restaurant();
            List<Restaurant> emptyTables = new List<Restaurant>();
            List<Group> waitingList = new List<Group>();
            List<Chef> chefs = new List<Chef>();
            List<Waiter> waiters = new List<Waiter>();

            for (int i = 0; i < 5; i++)
            {
                Group group = new Group();
                group.CreateGuest();
                waitingList.Add(group);
            }

            foreach (var wl in waitingList)
            {

                foreach(var guest in wl.guests)
                {
                    Console.WriteLine(guest.Name + " " + guest.Money + " Sek");
                }

                Console.WriteLine();
            }


        }

        public void CreateTable(List<Restaurant> emptyTables, Restaurant restaurant)
        {
            for (int i = 0; i < 5; i++)
            {
                Table smallTable = new Table(restaurant.TableForTwo);
                emptyTables.Add(smallTable);
                Table bigTable = new Table(restaurant.TableForFour);
                emptyTables.Add(bigTable);
            }

            foreach (var table in emptyTables)
            {
                Console.WriteLine(table.GetType().Name);
            }
        }


        //Create Company()
        //Skapa lista
        //Random mnellan 1-4
        //Loopa igenom random antal gånger för att skapa gäster
        //Lägga alla dessa i watingList.add


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
    }
}