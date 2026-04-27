namespace RayTracer.Cli;

public class Projectile
{
    public Tuple Position { get; }
    public Vector Velocity { get; }

    public Projectile(Tuple postion, Vector velocity)
    {
        Velocity = velocity;
        Position = postion;
    }
}
