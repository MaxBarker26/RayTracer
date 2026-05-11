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

    public Vector NormalAt(Point p)
    {
        Tuple objPoint = TransformMatrix.Invert() * p;
        Vector objNormal = objPoint.ToPoint() - Center;
        Tuple worldNormal = TransformMatrix.Invert().Transpose() * objNormal;
        // This line is meant to ensure that the W coordinate of the world vector is 0,
        // meaning that it is a proper vector (which can be messed up by the invert and transpose methods)
        Vector worldNormalVectorized = new(worldNormal.X, worldNormal.Y, worldNormal.Z);
        return worldNormalVectorized.Normalized;
    }
}
