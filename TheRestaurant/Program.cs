namespace TheRestaurant
{
    internal class Program
    {
        static void Main(string[] args)
        {
            GUI gui = new();
            gui.CreateGuest();
            Console.WriteLine();
            gui.CreateChef();
            Console.WriteLine();
            gui.CreateWaiter();
        }
    }
}