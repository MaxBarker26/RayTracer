namespace RayTracer.Cli;

public class Vector : Tuple
{
    public double Magnitude { get; }

    public Vector(double x, double y, double z)
        : base(x, y, z, 0)
    {
        Magnitude = Math.Sqrt((x * x) + (y * y) + (z * z));
    }
}
