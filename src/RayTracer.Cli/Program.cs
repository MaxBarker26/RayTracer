// See https://aka.ms/new-console-template for more information
namespace RayTracer.Cli;

public class Program
{
    static void Main()
    {
        Scene s = new(World.Default(), new(100, 100, Math.PI / 2));
        CommandParser parser = new(s);

        while (true)
        {
            string? input = Console.ReadLine();
            parser.Parse(input);
        }
    }
}
