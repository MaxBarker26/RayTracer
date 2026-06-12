namespace RayTracer.Tests;

using RayTracer.Cli;

public class WorldTests()
{
    [Fact]
    public void WorldConstructor_NewWorldIsEmpty()
    {
        World w = new();
        Assert.Equal(0, w.Objects.Count);
        Assert.Null(w.LightSource);
    }

    [Fact]
    public void DefaultWorld_ContainsDefaultLightSourceAndObjects()
    {
        World w = World.Default();
        //change this test to test the properties of the default world's objects / light
        Assert.Equal(new Color(0.8, 1.0, 0.6), w.Objects[0].Material.Color);
        Assert.Equal(0.7, w.Objects[0].Material.Diffuse);
        Assert.Equal(0.2, w.Objects[0].Material.Specular);

        Assert.Equal(Matrix.Scaling(0.5, 0.5, 0.5), w.Objects[1].TransformMatrix);

        PointLight compareLight = new(new(-10, 10, -10), new(1, 1, 1));
        Assert.Equal(compareLight.Intensity, w.LightSource?.Intensity);
        Assert.Equal(compareLight.Position, w.LightSource?.Position);
    }

    [Fact]
    public void ShadeHit_ShadingAnIntersection_ColorIsExpected()
    {
        World w = World.Default();
        Ray r = new(new(0, 0, -5), new(0, 0, 1));
        IShape shape = w.Objects[0];
        Intersection i = new(4, shape);
        IntersectionComps comps = new(i, r);
        Color c = w.ShadeHit(comps);
        Assert.Equal(new Color(0.38066, 0.47583, 0.2855), c);
    }

    [Fact]
    public void ShadeHit_ShadingAnIntersectionFromInside_ColorIsExpected()
    {
        World w = World.Default();
        w.LightSource = new(new(0, 0.25, 0), new(1, 1, 1));
        Ray r = new(new(0, 0, 0), new(0, 0, 1));
        IShape shape = w.Objects[1];
        Intersection i = new(0.5, shape);
        IntersectionComps comps = new(i, r);
        Color c = w.ShadeHit(comps);
        Assert.Equal(new(0.90498, 0.90498, 0.90498), c);
    }

    [Fact]
    public void ColorAt_ColorWhenRayMisses_ColorIsBlack()
    {
        World w = World.Default();
        Ray r = new(new(0, 0, -5), new(0, 1, 0));
        Color c = w.ColorAt(r);
        Assert.Equal(new(0, 0, 0), c);
    }

    [Fact]
    public void ColorAt_ColorWhenRayMisses_ColorIsExpected()
    {
        World w = World.Default();
        Ray r = new(new(0, 0, -5), new(0, 0, 1));
        Color c = w.ColorAt(r);
        Assert.Equal(new(0.38066, 0.47583, 0.2855), c);
    }

    [Fact]
    public void ColorAt_ColorWithAnIntersectionBehindTheRay_ColorMatchesInnerMaterial()
    {
        World w = World.Default();
        IShape outer = w.Objects[0];
        outer.Material.Ambient = 1;
        IShape inner = w.Objects[1];
        inner.Material.Ambient = 1;
        Ray r = new(new(0, 0, 0.75), new(0, 0, -1));
        Color c = w.ColorAt(r);
        Assert.Equal(inner.Material.Color, c);
    }
}
