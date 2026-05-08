namespace RayTracer.Cli;

public class Intersection
{
    public IShape Shape { get; }
    public double T { get; }

    public Intersection(double t, IShape shape)
    {
        Shape = shape;
        T = t;
    }
}
