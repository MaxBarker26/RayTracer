namespace RayTracer.Cli;

public class Scene
{
    World World { get; set; }

    Camera Camera { get; set; }

    public Scene(World w, Camera c)
    {
        World = w;
        Camera = c;
    }
}
