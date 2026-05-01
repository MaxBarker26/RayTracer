namespace RayTracer.Cli;

/// <summary>
/// Represents a color with red, green, and blue components. Inherits from <see cref="Tuple"/>.
/// </summary>
public class Color : Tuple
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Color"/> class with specified red, green, and blue components.
    /// </summary>
    /// <param name="r">The red component.</param>
    /// <param name="g">The green component.</param>
    /// <param name="b">The blue component.</param>
    public Color(double r, double g, double b)
        : base(r, g, b, double.NaN) { }

    /// <summary>
    /// Adds two colors component-wise.
    /// </summary>
    /// <param name="a">The first color.</param>
    /// <param name="b">The second color.</param>
    /// <returns>A new <see cref="Color"/> representing the sum of the two colors.</returns>
    public static Color operator +(Color a, Color b)
    {
        return Add(a, b).ToColor();
    }

    /// <summary>
    /// Subtracts the components of one color from another.
    /// </summary>
    /// <param name="a">The color to subtract from.</param>
    /// <param name="b">The color to subtract.</param>
    /// <returns>A new <see cref="Color"/> representing the difference between the two colors.</returns>
    public static Color operator -(Color a, Color b)
    {
        return Subtract(a, b).ToColor();
    }

    /// <summary>
    /// Multiplies a color by a scalar value component-wise.
    /// </summary>
    /// <param name="a">The color to multiply.</param>
    /// <param name="b">The scalar value.</param>
    /// <returns>A new <see cref="Color"/> representing the scaled color.</returns>
    public static Color operator *(Color a, double b)
    {
        return MultiplyScalar(a, b).ToColor();
    }

    /// <summary>
    /// Divides a color by a scalar value component-wise.
    /// </summary>
    /// <param name="a">The color to divide.</param>
    /// <param name="b">The scalar value.</param>
    /// <returns>A new <see cref="Color"/> representing the divided color.</returns>
    public static Color operator /(Color a, double b)
    {
        return MultiplyScalar(a, 1 / b).ToColor();
    }

    /// <summary>
    /// Performs a component-wise product (Hadamard product) of this color with another color.
    /// </summary>
    /// <param name="other">The other color to multiply with.</param>
    /// <returns>A new <see cref="Color"/> representing the component-wise product.</returns>
    public Color Prod(Color other)
    {
        return new Color(X * other.X, Y * other.Y, Z * other.Z);
    }
}
