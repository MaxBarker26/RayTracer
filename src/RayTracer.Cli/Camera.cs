namespace RayTracer.Cli;

public class Camera
{
  public int hSize {get;}
  public int vSize {get;}
  public double fieldOfView {get; set;}
  public Matrix transform {get; set;} = Matrix.Identity();

  public Camera(int hSize, int vSize, double fieldOfView) {
    this.hSize = hSize;
    this.vSize = vSize;
    this.fieldOfView = fieldOfView;
  }
}
