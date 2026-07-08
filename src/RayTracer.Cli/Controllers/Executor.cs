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
                try
                {
                    Select(args[i]);
                }
                catch
                {
                    Console.WriteLine($"No object with the ID \"{args[i]}\" was found.");
                }
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
            throw new ArgumentException($"ID {id} is not valid");
        }
    }

    public void Deselect(string[] args)
    {
        // running deselect with no arguments simply deselects all currently selected objects
        if (args.Length == 1)
        {
            Deselect();
            Console.WriteLine("Deselected all.");
        }
        else
        {
            for (int i = 1; i < args.Length; i++)
            {
                try
                {
                    Deselect(args[i]);
                }
                catch
                {
                    Console.WriteLine($"No object with the ID \"{args[i]}\" was found.");
                }
            }
        }
    }

    //deselect overload with no arguments simply clears any selected objects
    private void Deselect()
    {
        Scene.Selected.Clear();
    }

    private void Deselect(string id)
    {
        if (Scene.World.IdToObject.TryGetValue(id, out IShape? obj))
        {
            Scene.Selected.Remove(obj);
        }
        else
        {
            throw new ArgumentException($"ID {id} was not found");
        }
    }

    public void Move(string[] args)
    {
        // if there are only three arguments passed in, one is assumed to be the "move" command itself,
        // the next to be the direction relative to the camera frame, and the last to be the distance. Currently selected objects will be moved.
        if (args.Length == 3)
        {
            string direction = args[1];
            double distance = double.Parse(args[2]);
            switch (direction)
            {
                case "left":
                    MoveLeft(distance);
                    break;
                case "right":
                    MoveRight(distance);
                    break;
                default:
                    Console.WriteLine(
                        "direction not recognized. Valid directions for the move command are: left, right, down, up, forward, back"
                    );
                    break;
            }
        }
        else if (args.Length > 3) { } //If there are more than three arguments it is assumed that the arguments between the direction and distance are the ids of objects to be moved
    }

    private void MoveLeft(double distance)
    {
        foreach (IShape obj in Scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(Scene.Camera, distance, 0, 0)
                * obj.TransformMatrix;
        }
    }

    private void MoveRight(double distance)
    {
        foreach (IShape obj in Scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(Scene.Camera, -distance, 0, 0)
                * obj.TransformMatrix;
        }
    }
}
