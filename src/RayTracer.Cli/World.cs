namespace RayTracer.Cli;

public class World
{
    public List<IShape> Objects { get; } = [];
    public PointLight? LightSource { get; set; }
    public Dictionary<string, IShape> IdToObject { get; } = new();

    public static World Default()
    {
        World w = new();
        PointLight l = new(new(-10, 10, -10), new(1, 1, 1));
        w.LightSource = l;

        Sphere s1 = new("1");
        s1.Material.Color = new(0.8, 1.0, 0.6);
        s1.Material.Diffuse = 0.7;
        s1.Material.Specular = 0.2;

        Sphere s2 = new("2");
        s2.TransformMatrix = Matrix.Scaling(0.5, 0.5, 0.5);

        w.Objects.Add(s1);
        w.IdToObject.Add(s1.ID, s1);
        w.Objects.Add(s2);
        w.IdToObject.Add(s2.ID, s2);
        return w;
    }

    public static World Default2()
    {
        World w = new();

        //create floors and walls
        IShape floor = new Sphere("floor");
        floor.TransformMatrix = Matrix.Scaling(10, 0.01, 10);
        floor.Material.Color = new(1, 0.9, 0.9);
        floor.Material.Specular = 0;

        IShape leftWall = new Sphere("leftWall");
        leftWall.TransformMatrix =
            Matrix.Translation(0, 0, 5)
            * Matrix.RotationY(-Math.PI / 4)
            * Matrix.RotationX(Math.PI / 2)
            * Matrix.Scaling(10, 0.01, 10);
        leftWall.Material = floor.Material;

        IShape rightWall = new Sphere("rightWall");
        rightWall.TransformMatrix =
            Matrix.Translation(0, 0, 5)
            * Matrix.RotationY(Math.PI / 4)
            * Matrix.RotationX(Math.PI / 2)
            * Matrix.Scaling(10, 0.01, 10);
        rightWall.Material = floor.Material;

        //cretae spheres
        Sphere middle = new("middle");
        middle.TransformMatrix = Matrix.Translation(-0.5, 1, 0.5);
        middle.Material.Color = new(0.1, 1, 0.5);
        middle.Material.Diffuse = 0.7;
        middle.Material.Specular = 0.3;

        Sphere right = new("right");
        right.TransformMatrix = Matrix.Translation(1.5, 0.5, -0.5) * Matrix.Scaling(0.5, 0.5, 0.5);
        right.Material.Color = new(0.5, 1, 0.1);
        right.Material.Diffuse = 0.7;
        right.Material.Specular = 0.3;

        Sphere left = new("left");
        left.TransformMatrix =
            Matrix.Translation(-1.5, 0.33, -0.75) * Matrix.Scaling(0.33, 0.33, 0.33);
        right.Material.Color = new(1, 0.8, 0.1);
        right.Material.Diffuse = 0.7;
        right.Material.Specular = 0.3;

        //Add objects
        w.Objects.AddRange(new List<IShape> { floor, leftWall, rightWall, left, right, middle });
        w.IdToObject.Add(floor.ID, floor);
        w.IdToObject.Add(leftWall.ID, leftWall);
        w.IdToObject.Add(rightWall.ID, rightWall);
        w.IdToObject.Add(right.ID, right);
        w.IdToObject.Add(left.ID, left);
        w.IdToObject.Add(middle.ID, middle);
        //Add light source
        w.LightSource = new(new(-10, 10, -10), new(1, 1, 1));

        return w;
    }

    public Color ShadeHit(IntersectionComps comps)
    {
        if (this.LightSource is null)
            throw new InvalidOperationException(
                "Must establish a LightSource for this World before calling ShadeHit."
            );
        return Color.Lighting(
            comps.Shape.Material,
            this.LightSource,
            comps.Point,
            comps.EyeV,
            comps.NormalV
        );
    }

    public Color ColorAt(Ray r)
    {
        PriorityQueue<Intersection, double> xs = r.Intersects(this);
        //could be null if there are no intersections, in which case return black.
        Intersection? i = Intersection.Hit(xs);
        if (i is null)
            return new Color(0, 0, 0);

        IntersectionComps comps = new(i, r);
        return ShadeHit(comps);
    }
}
