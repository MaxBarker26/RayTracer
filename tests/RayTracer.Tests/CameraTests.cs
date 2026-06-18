namespace RayTracer.Tests;
using RayTracer.Cli;

public class CameraTests() {
  [Fact]
  public void CameraConstructor() {
    Camera c = new(160, 120, Math.PI/2);
    
    Assert.Equal(160, c.hSize);
    Assert.Equal(120, c.vSize);
    Assert.Equal(Math.PI/2, c.fieldOfView);
    Assert.Equal(Matrix.Identity(), c.transform);
  }
}


