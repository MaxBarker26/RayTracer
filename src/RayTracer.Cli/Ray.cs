namespace RayTracer.Cli;

/// <summary>
/// Represents a ray in 3D space, defined by an origin point and a direction vector.
/// </summary>
public class Ray
{
    /// <summary>
    /// Gets the origin point of the ray.
    /// </summary>
    public Point Origin { get; }

    /// <summary>
    /// Gets the direction vector of the ray.
    /// </summary>
    public Vector Direction { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Ray"/> class.
    /// </summary>
    /// <param name="origin">The origin point of the ray.</param>
    /// <param name="direction">The direction vector of the ray.</param>
    public Ray(Point origin, Vector direction)
    {
        Origin = origin;
        Direction = direction;
    }

    /// <summary>
    /// Calculates the position of a point along the ray at a given distance t.
    /// </summary>
    /// <param name="t">The distance along the ray from the origin.</param>
    /// <returns>The point in 3D space at the specified distance t along the ray.</returns>
    public Point Position(double t)
    {
        return (Origin + (Direction * t)).ToPoint();
    }

    /// <summary>
    /// Calculates the intersection points of the ray with a given sphere.
    /// </summary>
    /// <param name="s">The sphere to check for intersection with.</param>
    /// <returns>An array of doubles representing the intersection distances (t-values). Returns an empty array if no intersection occurs.</returns>
    public double[] Intersects(Sphere s)
    {
        Vector sphereToRay = Origin - s.Center;

        double a = Direction.Dot(Direction);
        double b = 2 * Direction.Dot(sphereToRay);
        double c = sphereToRay.Dot(sphereToRay) - 1;

        double discriminant = (b * b) - (4 * a * c);
        if (discriminant < 0)
            return new double[0];

        double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
        return new double[2] { t1, t2 };
    }

    public Ray Transform(Matrix transformation)
    {
        return new((transformation * Origin).ToPoint(), (transformation * Direction).ToVector());
    }
}
