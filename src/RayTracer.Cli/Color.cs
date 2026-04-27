namespace RayTracer.Cli;

public class Color : Tuple
{
    public Color(double x, double y, double z)
        : base(x, y, z, double.NaN) { }

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
}
