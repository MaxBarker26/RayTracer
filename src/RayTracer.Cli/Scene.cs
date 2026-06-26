namespace RayTracer.Cli;

public class Scene
{
    public string? OutputPath { get; set; }

    public World World { get; set; }

    public Camera Camera { get; set; }

    public Scene()
    {
        World = World.Default2();
        Camera = new(300, 150, Math.PI / 3);
        Camera.Transform = Matrix.View(new(0, 1.5, -5), new(0, 1, 0), new(0, 1, 0));
    }
}
