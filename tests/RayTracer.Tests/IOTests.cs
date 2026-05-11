namespace RayTracer.Tests;

using RayTracer.Cli;

public class IOTests()
{
    [Fact]
    public void Sphere()
    {
        string? filePath = "/home/maxbarker/Desktop/sphere.ppm";
        int pixels = 100;
        double wallZ = 10;
        double wallSize = 7;
        double pixel_size = wallSize / pixels;
        double half = wallSize / 2;

        Canvas c = new(pixels, pixels);
        Color red = new Color(1, 0, 0);
        Sphere s = new();
        s.TransformMatrix = Matrix.Shearing(1, 0, 0, 0, 0, 0) * Matrix.Scaling(0.5, 1, 1);
        Point origin = new(0, 0, -5);

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
                PriorityQueue<Intersection, double> pq = r.Intersects(s);

                if (Intersection.Hit(pq) != null)
                {
                    c.SetPixel(x, y, red);
                }
            }
        }

        string ppm = c.SavePPM();
        File.AppendAllText(filePath, ppm);
    }
}
