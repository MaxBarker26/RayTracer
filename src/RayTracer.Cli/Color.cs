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

    public static Color operator *(Color l, Color r)
    {
        return l.Prod(r);
    }

    public static Color Lighting(
        Material material,
        PointLight light,
        Point position,
        Vector eyeVector,
        Vector normalVector
    )
    {
        Color effectiveColor = material.Color * light.Intensity;
        Vector lightVector = (light.Position - position).Normalized;
        Color ambient = effectiveColor * material.Ambient;
        Color diffuse;
        Color specular;
        double reflectDotEye;
        double lightDotNormal = lightVector.Dot(normalVector);

        if (lightDotNormal < 0)
        {
            diffuse = new Color(0, 0, 0);
            specular = new Color(0, 0, 0);
        }
        else
        {
            diffuse = effectiveColor * material.Diffuse * lightDotNormal;
            //negating light vector converts it to tuple, so it is converted
            //back to Vector before the Reflect method is applied.
            Vector negatedLightV = (-lightVector).ToVector();
            Vector reflectVector = negatedLightV.Reflect(normalVector);
            reflectDotEye = reflectVector.Dot(eyeVector);
            if (reflectDotEye <= 0)
            {
                specular = new Color(0, 0, 0);
            }
            else
            {
                double factor = Math.Pow(reflectDotEye, material.Shininess);
                specular = light.Intensity * material.Specular * factor;
            }
        }

        return ambient + diffuse + specular;
    }
}
