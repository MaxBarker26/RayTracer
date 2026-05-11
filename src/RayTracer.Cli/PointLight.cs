namespace RayTracer.Cli;

public class PointLight
{
    public Color Intensity { get; }
    public Point Position { get; }

    public PointLight(Point position, Color intensity)
    {
        Intensity = intensity;
        Position = position;
    }
}
