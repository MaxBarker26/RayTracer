namespace RayTracer.Cli;

public class Environment
{
    public Vector Wind { get; } = new(0, 0, 0);
    public Vector Gravity { get; } = new(0, -0.1, 0);

    public Environment(Vector wind, Vector gravity)
    {
        Wind = wind;
        Gravity = gravity;
    }

    public Environment() { }

    public Projectile Tick(Projectile proj)
    {
        Tuple position = proj.Position + proj.Velocity;
        Vector velocity = (proj.Velocity + Wind + Gravity).ToVector();
        return new Projectile(position, velocity);
    }

    public void Fire(Projectile proj)
    {
        if (proj.Position.Y <= 0)
            return;
        Console.WriteLine(proj.Position.ToString());
        Fire(Tick(proj));
    }
}
