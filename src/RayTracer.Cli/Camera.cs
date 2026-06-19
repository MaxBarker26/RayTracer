namespace RayTracer.Cli;

public class Camera
{
    public int HSize { get; }
    public int VSize { get; }
    public double FieldOfView { get; }
    public Matrix Transform { get; set; } = Matrix.Identity();
    public double PixelSize { get; private set; }

    private double HalfWidth { get; set; }
    private double HalfHeight { get; set; }

    public Camera(int hSize, int vSize, double fieldOfView)
    {
        this.HSize = hSize;
        this.VSize = vSize;
        this.FieldOfView = fieldOfView;
        SetPixelSize();
    }

    public Ray RayForPixel(int x, int y)
    {
        double xOffset = (x + 0.5) * PixelSize;
        double yOffset = (y + 0.5) * PixelSize;

        double worldX = HalfWidth - xOffset;
        double worldY = HalfHeight - yOffset;

        Tuple pixel = Transform.Invert() * new Point(worldX, worldY, -1);
        Point origin = (Transform.Invert() * new Point(0, 0, 0)).ToPoint();
        Vector direction = (pixel - origin).ToVector().Normalized;

        return new(origin, direction);
    }

    public Canvas Render(World w)
    {
        Canvas image = new(HSize, VSize);
        for (int y = 0; y < VSize - 1; y++)
        {
            for (int x = 0; x < HSize - 1; x++)
            {
                Ray r = RayForPixel(x, y);
                Color color = w.ColorAt(r);
                image.SetPixel(x, y, color);
            }
        }
        return image;
    }

    private void SetPixelSize()
    {
        double halfView = Math.Tan(FieldOfView / 2);
        double aspect = (double)HSize / VSize;
        double halfWidth;
        double halfHeight;
        if (aspect >= 1)
        {
            halfWidth = halfView;
            halfHeight = halfView / aspect;
        }
        else
        {
            halfHeight = halfView;
            halfWidth = halfView * aspect;
        }
        PixelSize = (halfWidth * 2) / HSize;
        HalfWidth = halfWidth;
        HalfHeight = halfHeight;
    }
}
