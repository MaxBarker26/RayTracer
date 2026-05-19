namespace RayTracer.Tests;

using RayTracer.Cli;

public class IOTests()
{
    [Fact]
    public void Sphere()
    {
        string? filePath = "/home/maxbarker/Desktop/sphere.ppm";
        int pixels = 500;
        double wallZ = 5;
        double wallSize = 25;
        double pixel_size = wallSize / pixels;
        double half = wallSize / 2;

        Canvas c = new(pixels, pixels);
        Point origin = new(0, 50, -100);
        Sphere s = new();
        s.TransformMatrix =
            Matrix.RotationX(Math.PI / 4)
            * Matrix.Translation(5, 5, -15)
            * Matrix.Scaling(1, 5, 10);
        s.Material.Color = new(1, 1, 0);
        Point lightPosition = new(-25, 50, -25);
        Color lightColor = new(1, 1, 1);
        PointLight light = new(lightPosition, lightColor);

        double worldY;
        double worldX;
        for (int y = 0; y < pixels; y++)
        {
            worldY = half - pixel_size * y;
            for (int x = 0; x < pixels; x++)
            {
                worldX = -half + pixel_size * x;
                Point position = new(worldX, worldY, wallZ);
                Ray r = new(origin, (position - origin).Normalized);
                PriorityQueue<Intersection, double> xs = r.Intersects(s);
                Intersection? hit = Intersection.Hit(xs);

                if (hit != null)
                {
                    Point point = r.Position(hit.T);
                    Vector normal = hit.Shape.NormalAt(point);
                    Vector eye = (-r.Direction).ToVector();
                    Color pixelColor = Color.Lighting(
                        hit.Shape.Material,
                        light,
                        point,
                        eye,
                        normal
                    );
                    c.SetPixel(x, y, pixelColor);
                }
            }
        }

        string ppm = c.SavePPM();
        File.AppendAllText(filePath, ppm);
    }
}
