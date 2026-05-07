// See https://aka.ms/new-console-template for more information
namespace RayTracer.Cli;

public class Program
{
    public static string[] _routines = { "projectile", "clock" };

    static void Main()
    {
        Console.WriteLine("Enter the name of the routine you would like to run.");
        while (true)
        {
            ListRoutines();
            string? selected = Console.ReadLine();
            switch (selected)
            {
                case "projectile":
                    Projectile();
                    break;
                case "clock":
                    Clock();
                    break;
                default:
                    Console.WriteLine("Valid routine not detected.");
                    break;
            }
        }
    }

    public static void Projectile()
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

    public static void Clock()
    {
        Console.WriteLine("Enter File Path");
        string? filePath = Console.ReadLine();
        Canvas canvas = new(500, 500);

        for (int i = 1; i <= 12; i++)
        {
            Tuple p = new Point(0, 100, 0).RotateZ((i * Math.PI * 2) / 12);

            Console.WriteLine("" + (int)p.X + " " + "" + (int)p.Y);
            canvas.SetPixel(250 + (int)p.X, (int)p.Y + 250, new Color(255, 255, 0));
            Console.WriteLine(canvas.GetPixel(250 + (int)p.X, (int)p.Y + 250));
        }

        string ppm = canvas.SavePPM();
        File.AppendAllText(filePath, ppm);
    }

    public static void ListRoutines()
    {
        Console.WriteLine("Available routines are: ");
        foreach (string s in _routines)
        {
            Console.WriteLine(s);
        }
    }
}
