namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {

            Restaurant restaurant = new();

            List<Restaurant> emptyTables = new();
            List<Group> waitingList = new();
            List<Chef> chefs = new();
            List<Waiter> waiters = new();

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

                foreach(var guest in wl.guests)
                {
                    Console.WriteLine(guest.Name + " " + guest.Money + " sek");
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