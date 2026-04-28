namespace RayTracer.Cli;

public class Environment
{
    public Vector Wind { get; } = new(0, 0, 0);
    public Vector Gravity { get; } = new(0, -0.1, 0);
    public Canvas canvas { get; } = new(1000, 1000);

    public Environment(Vector wind, Vector gravity)
    {
        Wind = wind;
        Gravity = gravity;
    }

    public Environment(int width, int height, Vector wind, Vector gravity)
    {
        canvas = new(width, height);
    }

    public Environment() { }

    public Projectile Tick(Projectile proj)
    {
        Tuple position = proj.Position + proj.Velocity;
        Vector velocity = proj.Velocity + Wind + Gravity;
        return new Projectile(position, velocity);
    }

    public void Fire(Projectile proj)
    {
        if (proj.Position.Y <= 0)
            return;
        Console.WriteLine(proj.Position.ToString());
        canvas.SetPixel(
            (int)proj.Position.X,
            canvas.Height - (int)proj.Position.Y,
            new Color(200, 0, 0)
        );
        Fire(Tick(proj));
    }
}
