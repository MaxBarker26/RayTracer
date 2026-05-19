namespace RayTracer.Cli;

public interface IShape
{
    public Matrix TransformMatrix { get; set; }
    public Material Material { get; set; }

    //The center must be the point (0, 0, 0)
    public Point Center { get; }

    public Vector NormalAt(Point point);
}
