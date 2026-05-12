namespace RayTracer.Cli;

public interface IShape
{
    public Matrix TransformMatrix { get; set; }
    public Material Material { get; set; }

    public Vector NormalAt(Point point);
}
