namespace RayTracer.Cli;

public class Material
{
    public Color Color { get; set; } = new Color(1, 1, 1);
    public double Ambient { get; set; } = 0.1;
    public double Diffuse { get; set; } = 0.9;
    public double Specular { get; set; } = 0.9;
    public double Shininess { get; set; } = 200.0;

    public Material() { }

    public override bool Equals(object? obj)
    {
        if (!(obj is Material))
            return false;
        Material otherMaterial = (Material)obj;

        if (Color != otherMaterial.Color)
            return false;
        if (!Ambient.IsNearly(otherMaterial.Ambient))
            return false;
        if (!Diffuse.IsNearly(otherMaterial.Diffuse))
            return false;
        if (!Specular.IsNearly(otherMaterial.Specular))
            return false;
        if (!Shininess.IsNearly(otherMaterial.Shininess))
            return false;
        return true;
    }
}
