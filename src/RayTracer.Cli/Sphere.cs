namespace RayTracer.Cli;

public class Sphere : IShape
{
    public double Radius { get; }
    public Point Center { get; }

    public Sphere()
    {
        Radius = 1.0;
        Center = new Point(0, 0, 0);
    }
}
