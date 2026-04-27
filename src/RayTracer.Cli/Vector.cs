namespace RayTracer.Cli;

public class Vector : Tuple
{
    private Vector? _normalized;
    public double Magnitude { get; }
    public Vector Normalized
    {
        get
        {
            if (_normalized is null)
                CalculateNormalized();
            return _normalized;
        }
    }

    public Vector(double x, double y, double z)
        : base(x, y, z, 0)
    {
        Magnitude = Math.Sqrt((x * x) + (y * y) + (z * z));
    }

    private void CalculateNormalized()
    {
        _normalized = new(X / Magnitude, Y / Magnitude, Z / Magnitude);
    }
}
