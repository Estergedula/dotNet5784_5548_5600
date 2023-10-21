partial class Program
{
    private static void Main(string[] args)
    {
        Welcome5600();
        Welcome5548();
        Console.ReadKey();
    }
    static partial void Welcome5548();

    private static void Welcome5600()
    {
        Console.WriteLine("Enter your name: ");
        string? name = Console.ReadLine();
        Console.WriteLine("{0}, welcome to my first application", name);
    }
}