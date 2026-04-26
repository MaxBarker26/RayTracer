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

    public static Tuple Point(double x, double y, double z)
    {
        return new Tuple(x, y, z, 1.0);
    }

    public static Tuple Vector(double x, double y, double z)
    {
        return new Tuple(x, y, z, 0);
    }

    ///<summary>
    /// Equals override for tuples. Makes use of IsNearly double class extension in order to
    /// compare the x, y, z, and w properties of two Tuple objects.
    ///</summary>
    public override bool Equals(object? other)
    {
        Tuple otherTuple;
        if (other is null)
            return false;
        if (other is Tuple)
            otherTuple = (Tuple)other;
        else
            return false;
        if (!this.X.IsNearly(otherTuple.X))
            return false;
        if (!this.Y.IsNearly(otherTuple.Y))
            return false;
        if (!this.Z.IsNearly(otherTuple.Z))
            return false;
        if (!this.W.IsNearly(otherTuple.W))
            return false;
        return true;
    }

    public static Tuple operator +(Tuple a, Tuple b)
    {
        return Add(a, b);
    }

    public static Tuple Add(Tuple left, Tuple right)
    {
        double x = left.X + right.X;
        double y = left.Y + right.Y;
        double z = left.Z + right.Z;
        double w = left.W + right.W;
        if (w != 0 && w != 1)
            throw new InvalidOperationException(
                "Resulting W property must be 1 or 0. Is currently: " + w
            );
        return new Tuple(x, y, z, w);
    }

    public static Tuple operator -(Tuple a, Tuple b)
    {
        return Subtract(a, b);
    }

    public static Tuple Subtract(Tuple left, Tuple right)
    {
        double x = left.X - right.X;
        double y = left.Y - right.Y;
        double z = left.Z - right.Z;
        double w = left.W - right.W;
        if (w != 0 && w != 1)
            throw new InvalidOperationException(
                "Resulting W property must be 1 or 0. Is currently: " + w
            );
        return new Tuple(x, y, z, w);
    }
}
