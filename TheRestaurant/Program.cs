namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.Title = "The Restaurant";
            Console.CursorVisible = false;
            Restaurant restaurant = new();
            restaurant.Start();
        }
    }
}