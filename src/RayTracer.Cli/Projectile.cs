namespace RayTracer.Cli;

/// <summary>
/// Represents a projectile with a position and velocity.
/// </summary>
public class Projectile
{
    /// <summary>
    /// Gets the current position of the projectile.
    /// </summary>
    public Tuple Position { get; }

    /// <summary>
    /// Gets the current velocity of the projectile.
    /// </summary>
    public Vector Velocity { get; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Projectile"/> class.
    /// </summary>
    /// <param name="postion">The initial position of the projectile.</param>
    /// <param name="velocity">The initial velocity of the projectile.</param>
    public Projectile(Tuple postion, Vector velocity)
    {
        Velocity = velocity;
        Position = postion;
    }
}
