namespace RayTracer.Cli;

/// <summary>
/// Represents a sphere shape in a 3D ray tracing environment.
/// Implements the <see cref="IShape"/> interface.
/// </summary>
public class Sphere : IShape
{
    /// <summary>
    /// Gets or sets the transformation matrix for the sphere.
    /// </summary>
    public Matrix TransformMatrix { get; set; } = Matrix.Identity(4);

    /// <summary>
    /// Gets the radius of the sphere.
    /// </summary>
    public double Radius { get; }

    /// <summary>
    /// Gets the center point of the sphere.
    /// </summary>
    public Point Center { get; }

    /// <summary>
    /// Gets or sets the material associated with this object.
    /// </summary>
    public Material Material { get; set; } = new();

    public string ID { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Sphere"/> class with a default radius of 1.0
    /// and a center at the origin (0, 0, 0).
    /// </summary>
    public Sphere(string id)
    {
        Radius = 1.0;
        Center = new Point(0, 0, 0);
        //IDs are not case sensitive
        ID = id.ToLowerInvariant();
    }

    /// <summary>
    /// Calculates the normal vector at a given point on the sphere's surface.
    /// </summary>
    /// <param name="p">The point on the sphere's surface for which to calculate the normal.</param>
    /// <returns>A normalized <see cref="Vector"/> representing the normal at the specified point.</returns>
    public Vector NormalAt(Point p)
    {
        Tuple objPoint = TransformMatrix.Invert() * p;
        Vector objNormal = objPoint.ToPoint() - Center;
        Tuple worldNormal = TransformMatrix.Invert().Transpose() * objNormal;
        // This line is meant to ensure that the W coordinate of the world vector is 0,
        // meaning that it is a proper vector (which can be messed up by the invert and transpose methods)
        Vector worldNormalVectorized = new(worldNormal.X, worldNormal.Y, worldNormal.Z);
        return worldNormalVectorized.Normalized;
    }
}
