namespace RayTracer.Cli;

public static class DoubleExtensions
{
    private const double Epsilon = 0.001;

    public static bool IsNearly(this double a, double b)
    {
        if (Math.Abs(a - b) >= Epsilon)
        {
            return false;
        }
        return true;
    }
}
