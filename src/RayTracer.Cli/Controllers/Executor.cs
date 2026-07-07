namespace RayTracer.Cli;

public class Executor
{
    private Scene Scene;

    public Executor(Scene s)
    {
        Scene = s;
    }

    // This is the public facing method that takes the arguments passed via command line.
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
        Scene.OutputPath = filePath;

        Render();
    }

    // Renders the current scene to canvas and saves the PPM file
    private void Render()
    {
        //render to Canvas
        Canvas canvas = Scene.Camera.Render(Scene.World);

        //output canvas
        string ppm = canvas.SavePPM();
        if (Scene.OutputPath is not null)
        {
            File.AppendAllText(Scene.OutputPath, ppm);
        }
        else
        {
            Console.WriteLine("No file path has been set for output");
        }
    }

    // public facing Preview method calls the appropriate overload based on the provided arguments
    public void Preview(string[] args)
    {
        if (args.Length == 1)
        {
            Preview();
        }
    }

    private void Preview()
    {
        TerminalGraphics.PrintScene(Scene);
    }

    public void Select(string[] args)
    {
        if (args.Length == 1)
        {
            Console.WriteLine("No objects specified. To see available objects, type 'objects'.");
        }
        else
        {
            for (int i = 1; i < args.Length; i++)
            {
                Select(args[i]);
            }
        }
    }

    private void Select(string id)
    {
        if (Scene.World.IdToObject.TryGetValue(id, out IShape? obj))
        {
            Scene.Selected.Add(obj);
        }
        else
        {
            Console.WriteLine($"No object with the ID \"{id}\" was found.");
        }
    }
}
