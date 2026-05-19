namespace RayTracer.Cli;

/// <summary>
/// Represents precomputed values for an intersection, useful for shading and other calculations.
/// </summary>
public class IntersectionComps
{
    /// <summary>
    /// The `t` value of the intersection, representing the distance along the ray.
    /// </summary>
    public double T { get; }

    /// <summary>
    /// The shape that was intersected.
    /// </summary>
    public IShape Shape { get; }

    /// <summary>
    /// The world-space point of the intersection.
    /// </summary>
    public Point Point { get; }

    /// <summary>
    /// The eye vector, pointing from the intersection point back towards the eye/ray origin.
    /// </summary>
    public Vector EyeV { get; }

    /// <summary>
    /// The normal vector at the intersection point, normalized.
    /// </summary>
    public Vector NormalV { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="IntersectionComps"/> class.
    /// </summary>
    /// <param name="i">The intersection object containing `t` and the intersected shape.</param>
    /// <param name="r">The ray that caused the intersection.</param>
    public IntersectionComps(Intersection i, Ray r)
    {
        T = i.T;
        Shape = i.Shape;
        Point = r.Position(T);
        EyeV = (-r.Direction).ToVector();
        NormalV = Shape.NormalAt(Point);
    }
}
