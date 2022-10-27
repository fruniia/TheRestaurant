namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Restaurant restaurant = new Restaurant();
            List<Restaurant> tables = new List<Restaurant>();

            for (int i = 0; i < 5; i++)
            {
                Table smallTable = new Table(restaurant.TableForTwo);
                tables.Add(smallTable);
                Table bigTable = new Table(restaurant.TableForFour);
                tables.Add(bigTable);
            }

            foreach (var table in tables)
            { 
            Console.WriteLine(table.GetType().Name);
            }
        }
    }
}