namespace RayTracer.Tests;

using RayTracer.Cli;

public class IOTests()
{
    [Fact]
    public void Sphere()
    {
        string? filePath = "/home/maxbarker/Desktop/sphere.ppm";
        int pixels = 1000;
        double wallZ = 10;
        double wallSize = 7;
        double pixel_size = wallSize / pixels;
        double half = wallSize / 2;

        Canvas c = new(pixels, pixels);
        Color red = new(1, 0, 0);
        Point origin = new(0, 0, -5);
        Sphere s = new();
        s.Material.Color = new(1, 0.2, 1);
        Point lightPosition = new(-10, 10, -10);
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
