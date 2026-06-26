namespace RayTracer.Cli;

public class Executor
{
    private Scene _scene;

    public Executor(Scene s)
    {
        _scene = s;
    }

    public void Render(string[] args)
    {
        string? filePath;
        if (args.Length == 1)
        {
            Render();
        }
        //if command contains only one argument assume it is
        //file path for output
        if (args.Length == 2)
        {
            filePath = args[1];
            Render(filePath);
        }
    }

    //Oerload for the Render method that renders to the given file path
    //using the current width height and zoom of the scene
    private void Render(string filePath)
    {
        _scene.OutputPath = filePath;

        Render();
    }

    private void Render()
    {
        //render to Canvas
        Canvas canvas = _scene.Camera.Render(_scene.World);

        //output canvas
        string ppm = canvas.SavePPM();
        if (_scene.OutputPath is not null)
        {
            File.AppendAllText(_scene.OutputPath, ppm);
        }
        else
        {
            Console.WriteLine("No file path has been set for output");
        }
    }
}
