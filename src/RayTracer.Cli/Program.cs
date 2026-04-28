// See https://aka.ms/new-console-template for more information
namespace RayTracer.Cli;

public class Program
{
    static void Main()
    {
        Console.WriteLine("Enter File Path");
        string? filePath = Console.ReadLine();
        Console.WriteLine("Enter Projectile Starting Coordinates");
        Console.WriteLine("X:");
        double projX = double.Parse(Console.ReadLine());
        Console.WriteLine("Y:");
        double projY = double.Parse(Console.ReadLine());
        Console.WriteLine("Z:");
        double projZ = double.Parse(Console.ReadLine());

        Console.WriteLine("Enter Vector Starting Coordinates");
        Console.WriteLine("X:");
        double vecX = double.Parse(Console.ReadLine());
        Console.WriteLine("Y:");
        double vecY = double.Parse(Console.ReadLine());
        Console.WriteLine("Z:");
        double vecZ = double.Parse(Console.ReadLine());

        Environment env = new Environment();
        Projectile proj = new Projectile(
            new Point(projX, projY, projZ),
            new Vector(vecX, vecY, vecZ)
        );
        env.Fire(proj);
        string ppm = env.canvas.SavePPM();

        File.AppendAllText(filePath, ppm);
    }
}
