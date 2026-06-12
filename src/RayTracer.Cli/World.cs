namespace RayTracer.Cli;

public class World
{
    public List<IShape> Objects { get; } = [];
    public PointLight? LightSource { get; set; }

    public static World Default()
    {
        World w = new();
        PointLight l = new(new(-10, 10, -10), new(1, 1, 1));
        w.LightSource = l;

        Sphere s1 = new();
        s1.Material.Color = new(0.8, 1.0, 0.6);
        s1.Material.Diffuse = 0.7;
        s1.Material.Specular = 0.2;

        Sphere s2 = new();
        s2.TransformMatrix = Matrix.Scaling(0.5, 0.5, 0.5);

        w.Objects.Add(s1);
        w.Objects.Add(s2);
        return w;
    }

    public Color ShadeHit(IntersectionComps comps)
    {
        if (this.LightSource is null)
            throw new InvalidOperationException(
                "Must establish a LightSource before calling ShadeHit on a world."
            );
        return Color.Lighting(
            comps.Shape.Material,
            this.LightSource,
            comps.Point,
            comps.EyeV,
            comps.NormalV
        );
    }
}
