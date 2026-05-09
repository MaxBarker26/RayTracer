namespace RayTracer.Cli;

public class Sphere : IShape
{
    public Matrix TransformMatrix { get; set; } = Matrix.Identity(4);
    public double Radius { get; }
    public Point Center { get; }

    public Sphere()
    {
        Radius = 1.0;
        Center = new Point(0, 0, 0);
    }
}
