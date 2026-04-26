namespace RayTracer.Cli;

public class Tuple
{
    public double X { get; set; }
    public double Y { get; set; }
    public double Z { get; set; }
    public double W { get; set; }

    public Tuple(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    public bool IsPoint()
    {
        if (W == 1.0)
        {
            return true;
        }
        return false;
    }

    public bool IsVector()
    {
        if (W == 0)
        {
            return true;
        }
        return false;
    }
}
