namespace RayTracer.Cli;

public class Tuple
{
    public double X { get; }
    public double Y { get; }
    public double Z { get; }
    public double W { get; }

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

    ///<summary>
    /// Override of hashcode since equals is overridden.
    ///</summary>
    public override int GetHashCode()
    {
        return this.X.GetHashCode()
            + this.Y.GetHashCode()
            + this.Z.GetHashCode()
            + this.W.GetHashCode();
    }

    public override string ToString()
    {
        return X + ", " + Y + ", " + Z + ", " + W;
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
        return new Tuple(x, y, z, w);
    }

    public static Tuple operator -(Tuple a, Tuple b)
    {
        return Subtract(a, b);
    }

    ///<summary>
    /// Unary subtraction operator. Returns the negation of the tuple passed to it.
    ///</summary>
    public static Tuple operator -(Tuple a)
    {
        Tuple zeroVector = new Tuple(0, 0, 0, 0);
        return Subtract(zeroVector, a);
    }

    public static Tuple Subtract(Tuple left, Tuple right)
    {
        double x = left.X - right.X;
        double y = left.Y - right.Y;
        double z = left.Z - right.Z;
        double w = left.W - right.W;
        return new Tuple(x, y, z, w);
    }

    public static Tuple MultiplyScalar(Tuple tuple, double scalar)
    {
        double x = tuple.X * scalar;
        double y = tuple.Y * scalar;
        double z = tuple.Z * scalar;
        double w = tuple.W * scalar;
        return new Tuple(x, y, z, w);
    }

    public static Tuple operator *(Tuple tuple, double scalar)
    {
        return MultiplyScalar(tuple, scalar);
    }

    public static Tuple operator /(Tuple tuple, double scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        return MultiplyScalar(tuple, (1 / scalar));
    }

    ///<summary>
    /// Returns a Vector type when a vector tuple is passed as parameter.
    /// <param name="tuple">Must be a vector type Tuple (W property of 0). </param>
    ///</summary>
    public Vector ToVector()
    {
        if (this.IsVector())
        {
            return new Vector(X, Y, Z);
        }
        throw new ArgumentException("Argument passed is not a vector.");
    }

    public Color ToColor()
    {
        return new Color(X, Y, Z);
    }
}
