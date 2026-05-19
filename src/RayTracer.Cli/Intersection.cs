namespace RayTracer.Cli;

/// <summary>
/// Represents an intersection between a ray and a shape.
/// </summary>
public class Intersection
{
    /// <summary>
    /// Gets the shape that was intersected.
    /// </summary>
    public IShape Shape { get; }

    /// <summary>
    /// Gets the distance along the ray to the intersection point.
    /// </summary>
    public double T { get; }

    /// <summary>
    /// Gets the point where this intersection occurred. This is precomputed with the
    /// PrepareComputations method.
    /// </summary>
    public Point Point { get; private set; }

    /// <summary>
    /// Gets the eye vector. Precomputed with the PrepareComputation method.
    /// </summary>
    public Vector Eyev { get; private set; }

    /// <summary>
    /// Gets the normal vector. Precomputed with the PrepareComputation method.
    /// </summary>
    public Vector Normalv { get; private set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Intersection"/> class.
    /// </summary>
    /// <param name="t">The distance along the ray to the intersection point.</param>
    /// <param name="shape">The shape that was intersected.</param>
    public Intersection(double t, IShape shape)
    {
        Shape = shape;
        T = t;
    }

    /// <summary>
    /// Determines the first valid (positive T) intersection from a collection of intersections.
    /// </summary>
    /// <param name="xs">A priority queue of intersections, ordered by their 't' value.</param>
    /// <returns>
    /// The first intersection with a positive 't' value, or <see langword="null"/> if no such intersection exists.
    /// </returns>
    public static Intersection? Hit(PriorityQueue<Intersection, double> xs)
    {
        while (xs.TryPeek(out Intersection? i, out double t))
        {
            if (t > 0)
                return i;
            xs.Dequeue();
        }

        return null;
    }
}
