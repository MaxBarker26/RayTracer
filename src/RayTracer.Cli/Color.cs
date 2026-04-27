namespace RayTracer.Cli;

public class Color : Tuple
{
    public Color(double r, double g, double b)
        : base(r, g, b, double.NaN) { }

    public static Color operator +(Color a, Color b)
    {
        return Add(a, b).ToColor();
    }

    public static Color operator -(Color a, Color b)
    {
        return Subtract(a, b).ToColor();
    }

    public static Color operator *(Color a, double b)
    {
        return MultiplyScalar(a, b).ToColor();
    }

    public static Color operator /(Color a, double b)
    {
        return MultiplyScalar(a, 1 / b).ToColor();
    }

    public Color Prod(Color other)
    {
        return new Color(X * other.X, Y * other.Y, Z * other.Z);
    }
}
