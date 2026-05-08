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
        Vector sphereToRay = Origin - s.Center;

        double a = Direction.Dot(Direction);
        double b = 2 * Direction.Dot(sphereToRay);
        double c = sphereToRay.Dot(sphereToRay) - 1;

        double discriminant = (b * b) - (4 * a * c);
        if (discriminant < 0)
            return new double[0];

        double t1 = (-b - Math.Sqrt(discriminant)) / (2 * a);
        double t2 = (-b + Math.Sqrt(discriminant)) / (2 * a);
        return new double[2] { t1, t2 };
    }
}
