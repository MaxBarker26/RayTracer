namespace RayTracer.Cli;

/// <summary>
/// Represents the environment in which a projectile moves, including wind, gravity, and a canvas for rendering.
/// </summary>
public class Environment
{
    /// <summary>
    /// Gets the wind vector applied to projectiles.
    /// </summary>
    public Vector Wind { get; } = new(0, 0, 0);

    /// <summary>
    /// Gets the gravity vector applied to projectiles.
    /// </summary>
    public Vector Gravity { get; } = new(0, -0.1, 0);

    /// <summary>
    /// Gets the canvas used for rendering the projectile's path.
    /// </summary>
    public Canvas canvas { get; } = new(1000, 1000);

    /// <summary>
    /// Initializes a new instance of the <see cref="Environment"/> class with specified wind and gravity.
    /// </summary>
    /// <param name="wind">The wind vector.</param>
    /// <param name="gravity">The gravity vector.</param>
    public Environment(Vector wind, Vector gravity)
    {
        Wind = wind;
        Gravity = gravity;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Environment"/> class with specified canvas dimensions, wind, and gravity.
    /// </summary>
    /// <param name="width">The width of the canvas.</param>
    /// <param name="height">The height of the canvas.</param>
    /// <param name="wind">The wind vector.</param>
    /// <param name="gravity">The gravity vector.</param>
    public Environment(int width, int height, Vector wind, Vector gravity)
    {
        canvas = new(width, height);
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Environment"/> class with default values.
    /// </summary>
    public Environment() { }

    /// <summary>
    /// Calculates the next state of a projectile based on current environment conditions.
    /// </summary>
    /// <param name="proj">The current projectile state.</param>
    /// <returns>A new <see cref="Projectile"/> representing its state after one tick.</returns>
    public Projectile Tick(Projectile proj)
    {
        Tuple position = proj.Position + proj.Velocity;
        Vector velocity = proj.Velocity + Wind + Gravity;
        return new Projectile(position, velocity);
    }

    /// <summary>
    /// Simulates the flight of a projectile, printing its position and drawing it on the canvas until it hits the ground.
    /// This method uses recursion.
    /// </summary>
    /// <param name="proj">The initial projectile state to fire.</param>
    public void Fire(Projectile proj)
    {
        if (proj.Position.Y <= 0)
            return;
        Console.WriteLine(proj.Position.ToString());
        canvas.SetPixel(
            (int)proj.Position.X,
            canvas.Height - (int)proj.Position.Y,
            new Color(200, 0, 0)
        );
        Fire(Tick(proj));
    }
}
