namespace RayTracer.Cli;

public class Material
{
    public Color Color { get; } = new Color(1, 1, 1);
    public double Ambient { get; } = 0.1;
    public double Diffuse { get; } = 0.9;
    public double Specular { get; } = 0.9;
    public double Shininess { get; } = 200.0;

    public Material() { }
}
