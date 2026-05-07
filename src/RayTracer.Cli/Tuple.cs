namespace RayTracer.Cli;

/// <summary>
/// Represents a 4-dimensional tuple, which can act as either a point or a vector depending on its W component.
/// Points have W=1.0 and vectors have W=0.0.
/// </summary>
public class Tuple
{
    /// <summary>
    /// Gets the X component of the tuple.
    /// </summary>
    public double X { get; }

    /// <summary>
    /// Gets the Y component of the tuple.
    /// </summary>
    public double Y { get; }

    /// <summary>
    /// Gets the Z component of the tuple.
    /// </summary>
    public double Z { get; }

    /// <summary>
    /// Gets the W component of the tuple, which distinguishes between points (W=1.0) and vectors (W=0.0).
    /// </summary>
    public double W { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Tuple"/> class with specified components.
    /// </summary>
    /// <param name="x">The X component.</param>
    /// <param name="y">The Y component.</param>
    /// <param name="z">The Z component.</param>
    /// <param name="w">The W component (1.0 for a point, 0.0 for a vector).</param>
    public Tuple(double x, double y, double z, double w)
    {
        X = x;
        Y = y;
        Z = z;
        W = w;
    }

    /// <summary>
    /// Determines whether this tuple represents a point (W component is 1.0).
    /// </summary>
    /// <returns><c>true</c> if this tuple is a point; otherwise, <c>false</c>.</returns>
    public bool IsPoint()
    {
        if (W == 1.0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Determines whether this tuple represents a vector (W component is 0.0).
    /// </summary>
    /// <returns><c>true</c> if this tuple is a vector; otherwise, <c>false</c>.</returns>
    public bool IsVector()
    {
        if (W == 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Creates a new <see cref="Tuple"/> instance representing a point.
    /// </summary>
    /// <param name="x">The X coordinate of the point.</param>
    /// <param name="y">The Y coordinate of the point.</param>
    /// <param name="z">The Z coordinate of the point.</param>
    /// <returns>A new <see cref="Tuple"/> instance with W component set to 1.0.</returns>
    public static Tuple Point(double x, double y, double z)
    {
        return new Tuple(x, y, z, 1.0);
    }

    /// <summary>
    /// Creates a new <see cref="Tuple"/> instance representing a vector.
    /// </summary>
    /// <param name="x">The X component of the vector.</param>
    /// <param name="y">The Y component of the vector.</param>
    /// <param name="z">The Z component of the vector.</param>
    /// <returns>A new <see cref="Tuple"/> instance with W component set to 0.0.</returns>
    public static Tuple Vector(double x, double y, double z)
    {
        return new Tuple(x, y, z, 0);
    }

    ///<summary>
    /// Equals override for tuples. Makes use of IsNearly double class extension in order to
    /// compare the x, y, z, and w properties of two Tuple objects.
    ///</summary>
    /// <param name="other">The object to compare with the current tuple.</param>
    /// <returns><c>true</c> if the specified object is equal to the current tuple; otherwise, <c>false</c>.</returns>
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
    /// <returns>A hash code for the current tuple.</returns>
    public override int GetHashCode()
    {
        return this.X.GetHashCode()
            + this.Y.GetHashCode()
            + this.Z.GetHashCode()
            + this.W.GetHashCode();
    }

    /// <summary>
    /// Returns a string representation of the tuple.
    /// </summary>
    /// <returns>A string in the format "X, Y, Z, W".</returns>
    public override string ToString()
    {
        return X + ", " + Y + ", " + Z + ", " + W;
    }

    /// <summary>
    /// Overloads the addition operator to add two tuples.
    /// </summary>
    /// <param name="a">The first tuple to add.</param>
    /// <param name="b">The second tuple to add.</param>
    /// <returns>A new tuple representing the sum of the two input tuples.</returns>
    public static Tuple operator +(Tuple a, Tuple b)
    {
        return Add(a, b);
    }

    /// <summary>
    /// Adds two tuples component-wise.
    /// </summary>
    /// <param name="left">The first tuple.</param>
    /// <param name="right">The second tuple.</param>
    /// <returns>A new tuple with components as the sum of corresponding components of the input tuples.</returns>
    public static Tuple Add(Tuple left, Tuple right)
    {
        double x = left.X + right.X;
        double y = left.Y + right.Y;
        double z = left.Z + right.Z;
        double w = left.W + right.W;
        return new Tuple(x, y, z, w);
    }

    /// <summary>
    /// Overloads the subtraction operator to subtract one tuple from another.
    /// </summary>
    /// <param name="a">The tuple to subtract from.</param>
    /// <param name="b">The tuple to subtract.</param>
    /// <returns>A new tuple representing the difference between the two input tuples.</returns>
    public static Tuple operator -(Tuple a, Tuple b)
    {
        return Subtract(a, b);
    }

    ///<summary>
    /// Unary subtraction operator. Returns the negation of the tuple passed to it.
    ///</summary>
    /// <param name="a">The tuple to negate.</param>
    /// <returns>A new tuple with all components negated.</returns>
    public static Tuple operator -(Tuple a)
    {
        Tuple zeroVector = new Tuple(0, 0, 0, 0);
        return Subtract(zeroVector, a);
    }

    /// <summary>
    /// Subtracts one tuple from another component-wise.
    /// </summary>
    /// <param name="left">The tuple to subtract from.</param>
    /// <param name="right">The tuple to subtract.</param>
    /// <returns>A new tuple with components as the difference of corresponding components of the input tuples.</returns>
    public static Tuple Subtract(Tuple left, Tuple right)
    {
        double x = left.X - right.X;
        double y = left.Y - right.Y;
        double z = left.Z - right.Z;
        double w = left.W - right.W;
        return new Tuple(x, y, z, w);
    }

    /// <summary>
    /// Multiplies a tuple by a scalar value.
    /// </summary>
    /// <param name="tuple">The tuple to multiply.</param>
    /// <param name="scalar">The scalar value to multiply by.</param>
    /// <returns>A new tuple with each component multiplied by the scalar.</returns>
    public static Tuple MultiplyScalar(Tuple tuple, double scalar)
    {
        double x = tuple.X * scalar;
        double y = tuple.Y * scalar;
        double z = tuple.Z * scalar;
        double w = tuple.W * scalar;
        return new Tuple(x, y, z, w);
    }

    /// <summary>
    /// Calculates the Dot Product of two tuples.
    /// </summary>
    /// <param name="a">The first tuple.</param>
    /// <param name="b">The second tuple.</param>
    /// <returns>The dot product of the two tuples.</returns>
    public static double MultiplyTuple(Tuple a, Tuple b)
    {
        return (a.X * b.X) + (a.Y * b.Y) + (a.Z * b.Z) + (a.W * b.W);
    }

    /// <summary>
    /// Overloads the * operator to perform a dot product between two tuples.
    /// This is used by the Matrix multiplication logic.
    /// </summary>
    /// <param name="a">The first tuple.</param>
    /// <param name="b">The second tuple.</param>
    /// <returns>The dot product of the two tuples.</returns>
    public static double operator *(Tuple a, Tuple b)
    {
        return MultiplyTuple(a, b);
    }

    /// <summary>
    /// Overloads the multiplication operator to multiply a tuple by a scalar value.
    /// </summary>
    /// <param name="tuple">The tuple to multiply.</param>
    /// <param name="scalar">The scalar value.</param>
    /// <returns>A new tuple with each component multiplied by the scalar.</returns>
    public static Tuple operator *(Tuple tuple, double scalar)
    {
        return MultiplyScalar(tuple, scalar);
    }

    /// <summary>
    /// Overloads the division operator to divide a tuple by a scalar value.
    /// </summary>
    /// <param name="tuple">The tuple to divide.</param>
    /// <param name="scalar">The scalar value to divide by.</param>
    /// <returns>A new tuple with each component divided by the scalar.</returns>
    /// <exception cref="DivideByZeroException">Thrown when the scalar is zero.</exception>
    public static Tuple operator /(Tuple tuple, double scalar)
    {
        if (scalar == 0)
            throw new DivideByZeroException();
        return MultiplyScalar(tuple, (1 / scalar));
    }

    /// <summary>
    /// Translates this matrix by the specified offset values.
    /// </summary>
    /// <param name="x">The x-coordinate offset.</param>
    /// <param name="y">The y-coordinate offset.</param>
    /// <param name="z">The z-coordinate offset.</param>
    /// <returns>A new matrix representing the translated matrix.</returns>
    public Tuple Translate(int x, int y, int z)
    {
        return (Matrix.Translation(x, y, z) * this);
    }

    /// <summary>
    /// Scales this matrix by the given factors along the x, y, and z axes.
    /// </summary>
    /// <param name="x">The scaling factor for the x-axis.</param>
    /// <param name="y">The scaling factor for the y-axis.</param>
    /// <param name="z">The scaling factor for the z-axis.</param>
    /// <returns>A new scaled Matrix.</returns>
    public Tuple Scale(int x, int y, int z)
    {
        return (Matrix.Scaling(x, y, z) * this);
    }

    /// <summary>
    /// Rotates this matrix around the x-axis by the given angle.
    /// </summary>
    /// <param name="radians">The angle of rotation in radians.</param>
    /// <returns>A new rotated Matrix.</returns>
    public Tuple RotateX(double radians)
    {
        return (Matrix.RotationX(radians) * this);
    }

    /// <summary>
    /// Rotates this matrix around the y-axis by the given angle.
    /// </summary>
    /// <param name="radians">The angle of rotation in radians.</param>
    /// <returns>A new rotated Matrix.</returns>
    public Tuple RotateY(double radians)
    {
        return (Matrix.RotationY(radians) * this);
    }

    /// <summary>
    /// Rotates this matrix around the z-axis by the given angle.
    /// </summary>
    /// <param name="radians">The angle of rotation in radians.</param>
    /// <returns>A new rotated Matrix.</returns>
    public Tuple RotateZ(double radians)
    {
        return (Matrix.RotationZ(radians) * this);
    }

    /// <summary>
    /// Applies a shearing transformation to the current tuple.
    /// </summary>
    /// <param name="xy">The shearing factor for the X-component relative to the Y-axis.</param>
    /// <param name="xz">The shearing factor for the X-component relative to the Z-axis.</param>
    /// <param name="yx">The shearing factor for the Y-component relative to the X-axis.</param>
    /// <param name="yz">The shearing factor for the Y-component relative to the Z-axis.</param>
    /// <param name="zx">The shearing factor for the Z-component relative to the X-axis.</param>
    /// <param name="zy">The shearing factor for the Z-component relative to the Y-axis.</param>
    /// <returns>A new <see cref="Matrix"/> instance representing the result of the shearing operation.</returns>
    public Tuple Shear(int xy, int xz, int yx, int yz, int zx, int zy)
    {
        return (Matrix.Shearing(xy, xz, yx, yz, zx, zy) * this);
    }

    /// <summary>
    /// Converts the current instance to a <see cref="Vector"/> if it represents a vector.
    /// </summary>
    /// <returns>A new <see cref="Vector"/> instance with the X, Y, and Z components of this object.</returns>
    /// <exception cref="InvalidCastException">Thrown if the current object does not represent a vector.</exception>
    public Vector ToVector()
    {
        if (this.IsVector())
        {
            return new Vector(X, Y, Z);
        }
        throw new InvalidCastException("This object is not of type Vector");
    }

    /// <summary>
    /// Converts the tuple's X, Y, Z components into a <see cref="Color"/> object.
    /// The W component is ignored.
    /// </summary>
    /// <returns>A new <see cref="Color"/> instance.</returns>
    public Color ToColor()
    {
        return new Color(X, Y, Z);
    }

    /// <summary>
    /// Converts a <see cref="Tuple"/> to a <see cref="Point"/> if the tuple represents a point.
    /// </summary>
    /// <returns>A new <see cref="Point"/> instance initialized with the tuple's X, Y, and Z components.</returns>
    /// <exception cref="InvalidCastException">Thrown if the tuple is not a point (i.e., <see cref="Tuple.IsPoint"/> returns false).</exception>
    public Point ToPoint()
    {
        if (this.IsPoint())
        {
            return new Point(X, Y, Z);
        }
        throw new InvalidCastException();
    }
}
