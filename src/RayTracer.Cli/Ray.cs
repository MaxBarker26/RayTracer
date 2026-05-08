namespace RayTracer.Cli;

public class Ray
{
    public Point Origin { get; }
    public Vector Direction { get; }

    public Ray(Point origin, Vector direction)
    {
        Origin = origin;
        Direction = direction;
    }

    public Point Position(double t)
    {
        return (Origin + (Direction * t)).ToPoint();
    }

    public double[] Intersects(Sphere s)
    {
        if (Discriminant(s) < 0)
            return new double[0];
    }

    private double Discriminant(Sphere s)
    {
        Vector sphereToRay = s.Center - Origin;

        double a = Direction.Dot(Direction);
        double b = Direction.Dot(sphereToRay);
    }
}
