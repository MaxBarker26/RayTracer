namespace RayTracer.Tests;

using RayTracer.Cli;

public class ExecutorTests()
{
    [Fact]
    public void Select_CorrectObjectsAreSelected()
    {
        Scene s = new();
        Executor exec = new(s);

        string[] args = { "select", "left", "right", "middle" };

        exec.Select(args);

        Assert.Equal(args[1], s.Selected[0].ID);
        Assert.Equal(args[2], s.Selected[1].ID);
        Assert.Equal(args[3], s.Selected[2].ID);
    }

    [Fact]
    public void Select_AttempToSelectObjectWhichDoesNotExist_ExistingObjectsAreSelected()
    {
        Scene s = new();
        Executor exec = new(s);

        //center doesn't exist in World.default2()
        string[] args = { "select", "left", "right", "center" };

        exec.Select(args);

        Assert.Equal(2, s.Selected.Count);
    }

    [Fact]
    public void Deselect_DeselectSelectedObject()
    {
        Scene s = new();
        Executor exec = new(s);

        //center doesn't exist in World.default2()
        string[] selectArgs = { "select", "left", "right", "middle" };

        exec.Select(selectArgs);

        string[] deselectArgs = { "deselect", "middle", "left" };

        exec.Deselect(deselectArgs);

        Assert.Equal(1, s.Selected.Count);
    }

    [Fact]
    public void Deselect_DeselectAllSelectedObjects_NoObjectsAreSelected()
    {
        Scene s = new();
        Executor exec = new(s);

        //center doesn't exist in World.default2()
        string[] selectArgs = { "select", "left", "right", "middle" };

        exec.Select(selectArgs);

        string[] deselectArgs = { "deselect" };

        exec.Deselect(deselectArgs);

        Assert.Equal(0, s.Selected.Count);
    }
}
