// See https://aka.ms/new-console-template for more information
namespace RayTracer.Cli;

public class Program
{
    static void Main()
    {
        CommandParser parser = new();

        while (true)
        {
            Console.Write("> ");
            string? input = Console.ReadLine();
            if (input is null)
                throw new NullReferenceException("Console input was null");
            parser.Parse(input);
        }
    }
}
