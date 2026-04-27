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

    public double Dot(Vector v)
    {
        return (X * v.X) + (Y * v.Y) + (Z * v.Z);
    }

    public Vector Cross(Vector v)
    {
        return new Vector(Y * v.Z - Z * v.Y, Z * v.X - X * v.Z, X * v.Y - Y * v.X);
    }

    public static Vector operator +(Vector a, Vector b)
    {
        return Add(a, b).ToVector();
    }

    public static Vector operator -(Vector a, Vector b)
    {
        return Subtract(a, b).ToVector();
    }

    public static Vector operator *(Vector a, double b)
    {
        return MultiplyScalar(a, b).ToVector();
    }

    public static Vector operator /(Vector a, double b)
    {
        return MultiplyScalar(a, 1 / b).ToVector();
    }

    private void CalculateNormalized()
    {
        _normalized = new(X / Magnitude, Y / Magnitude, Z / Magnitude);
    }
}
