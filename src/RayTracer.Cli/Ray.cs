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
    public PriorityQueue<Intersection, double> Intersects(IShape s)
    {
        Ray r = this.Transform(s.TransformMatrix.Invert());
        Vector shapeToRay = r.Origin - s.Center;
        PriorityQueue<Intersection, double> pq = new();

        double a = r.Direction.Dot(r.Direction);
        double b = 2 * r.Direction.Dot(shapeToRay);
        double c = shapeToRay.Dot(shapeToRay) - 1;

        double discriminant = (b * b) - (4 * a * c);
        if (discriminant < 0)
            return pq;

        double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
        pq.Enqueue(new(t1, s), t1);
        pq.Enqueue(new(t2, s), t2);

        return pq;
    }

    /// <summary>
    /// Calculates the intersections between this ray and all objects in the given world.
    /// </summary>
    /// <param name="w">The world containing objects to intersect with.</param>
    /// <returns>A priority queue of intersections, ordered by their distance from the ray's origin.</returns>
    public PriorityQueue<Intersection, double> Intersects(World w)
    {
        //List of tuples will contain the intersection and priority of all items
        List<(Intersection, double)> list = new();

        foreach (var obj in w.Objects)
        {
            var temp = this.Intersects(obj);
            while (temp.TryDequeue(out Intersection? x, out double p))
            {
                //items are added with their associated priority after intersections are calculated
                list.Add((x, p));
            }
        }
        //List is heapified by creating a new priority queue
        PriorityQueue<Intersection, double> pq = new(list);
        return pq;
    }

    public Ray Transform(Matrix transformation)
    {
        return new((transformation * Origin).ToPoint(), (transformation * Direction).ToVector());
    }
}
