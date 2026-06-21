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

    [Fact]
    public void Camera()
    {
        //setup output file
        string outputDir = AppContext.BaseDirectory;
        string rendersDir = Path.Combine(outputDir, "Renders");
        Directory.CreateDirectory(rendersDir);
        string filePath = Path.Combine(rendersDir, "camera.ppm");

        //create world
        World w = new();

        //create floors and walls
        IShape floor = new Sphere();
        floor.TransformMatrix = Matrix.Scaling(10, 0.01, 10);
        floor.Material.Color = new(1, 0.9, 0.9);
        floor.Material.Specular = 0;

        IShape leftWall = new Sphere();
        leftWall.TransformMatrix =
            Matrix.Translation(0, 0, 5)
            * Matrix.RotationY(-Math.PI / 4)
            * Matrix.RotationX(Math.PI / 2)
            * Matrix.Scaling(10, 0.01, 10);
        leftWall.Material = floor.Material;

        IShape rightWall = new Sphere();
        rightWall.TransformMatrix =
            Matrix.Translation(0, 0, 5)
            * Matrix.RotationY(Math.PI / 4)
            * Matrix.RotationX(Math.PI / 2)
            * Matrix.Scaling(10, 0.01, 10);
        rightWall.Material = floor.Material;

        //cretae spheres
        Sphere middle = new();
        middle.TransformMatrix = Matrix.Translation(-0.5, 1, 0.5);
        middle.Material.Color = new(0.1, 1, 0.5);
        middle.Material.Diffuse = 0.7;
        middle.Material.Specular = 0.3;

        Sphere right = new();
        right.TransformMatrix = Matrix.Translation(1.5, 0.5, -0.5) * Matrix.Scaling(0.5, 0.5, 0.5);
        right.Material.Color = new(0.5, 1, 0.1);
        right.Material.Diffuse = 0.7;
        right.Material.Specular = 0.3;

        Sphere left = new();
        left.TransformMatrix =
            Matrix.Translation(-1.5, 0.33, -0.75) * Matrix.Scaling(0.33, 0.33, 0.33);
        right.Material.Color = new(1, 0.8, 0.1);
        right.Material.Diffuse = 0.7;
        right.Material.Specular = 0.3;

        //Add objects
        w.Objects.AddRange(new List<IShape> { floor, leftWall, rightWall, left, right, middle });
        //Add light source
        w.LightSource = new(new(-10, 10, -10), new(1, 1, 1));

        //Add camera
        Camera c = new(1000, 1000, Math.PI / 3);
        c.Transform = Matrix.View(new(0, 1.5, -5), new(0, 1, 0), new(0, 1, 0));

        //render to Canvas
        Canvas canvas = c.Render(w);

        //output canvas
        string ppm = canvas.SavePPM();
        File.AppendAllText(filePath, ppm);
    }
}
