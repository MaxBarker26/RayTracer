// See https://aka.ms/new-console-template for more information
namespace RayTracer.Cli;

public class Program
{
    static void Main()
    {
        Environment env = new Environment();
        Projectile proj = new Projectile(new Tuple(1, 1, 1, 1), new Vector(7, 8, 9));
        env.Fire(proj);
    }
}
