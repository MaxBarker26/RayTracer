namespace RayTracer.Cli;

public interface IShape
{
    public Matrix TransformMatrix { get; set; }
    public Material Material { get; set; }

    //The center must be the point (0, 0, 0)
    //TODO: consider making center a constant in this interface.
    public Point Center { get; }

    public Vector NormalAt(Point point);
}
