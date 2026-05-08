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

    public static Intersection? Hit(PriorityQueue<Intersection, double> xs)
    {
        while (xs.TryPeek(out Intersection? i, out double t))
        {
            if (t > 0)
                return i;
            xs.Dequeue();
        }

        return null;
    }
}
