namespace RayTracer.Cli;

public class Scene
{
    public string? OutputPath { get; set; }

    public World World { get; set; }

    public Camera Camera { get; set; }

    public Scene(World w)
    {
        World = w;
        Camera = new(300, 150, Math.PI / 3);
    }
}
