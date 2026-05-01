namespace RayTracer.Cli;

/// <summary>
/// Represents a 3D point with homogeneous coordinates, inheriting from <see cref="Tuple"/>.
/// </summary>
public class Point : Tuple
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Point"/> class with the specified x, y, and z coordinates.
    /// </summary>
    /// <param name="x">The x-coordinate of the point.</param>
    /// <param name="y">The y-coordinate of the point.</param>
    /// <param name="z">The z-coordinate of the point.</param>
    public Point(double x, double y, double z)
        : base(x, y, z, 1) { }
}
