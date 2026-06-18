namespace RayTracer.Cli;

public class Camera
{
    public double HSize { get; }
    public double VSize { get; }
    public double FieldOfView { get; }
    public Matrix Transform { get; set; } = Matrix.Identity();

    public double PixelSize { get; private set; }

    public Camera(int hSize, int vSize, double fieldOfView)
    {
        this.HSize = hSize;
        this.VSize = vSize;
        this.FieldOfView = fieldOfView;
        SetPixelSize();
    }

    private void SetPixelSize()
    {
        double halfView = Math.Tan(FieldOfView / 2);
        double aspect = HSize / VSize;
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
    }
}
