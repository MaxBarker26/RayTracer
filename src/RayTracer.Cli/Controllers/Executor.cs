namespace RayTracer.Cli;

public class Executor
{
    private Scene _scene;
    private bool _visualMode;

    public Executor(Scene s)
    {
        _scene = s;
        _visualMode = false;
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
        _scene.OutputPath = filePath;

        Render();
    }

    // Renders the current scene to canvas and saves the PPM file
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

    public void SetViewX(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Must give an integer between 1 and 10,000");
            return;
        }

        if (args.Length > 2)
        {
            Console.WriteLine(
                "viewx command takes only one argument, an integer between 1 and 10,000"
            );
            return;
        }

        if (int.TryParse(args[1], out int x))
        {
            if (x < 1 || x > 10000)
            {
                Console.WriteLine("Must give an integer between 1 and 10,000");
                return;
            }
            Matrix cameraView = _scene.Camera.Transform;
            _scene.Camera = new(x, _scene.Camera.VSize, _scene.Camera.FieldOfView);
            _scene.Camera.Transform = cameraView;
        }
        else
        {
            Console.WriteLine("Must provide a valid integer as argument");
        }
    }

    public void SetViewY(string[] args)
    {
        if (args.Length < 2)
        {
            Console.WriteLine("Must give an integer between 1 and 10,000");
            return;
        }

        if (args.Length > 2)
        {
            Console.WriteLine(
                "viewy command takes only one argument, an integer between 1 and 10,000"
            );
            return;
        }

        if (int.TryParse(args[1], out int y))
        {
            if (y < 1 || y > 10000)
            {
                Console.WriteLine("Must give an integer between 1 and 10,000");
                return;
            }
            Matrix cameraView = _scene.Camera.Transform;
            _scene.Camera = new(_scene.Camera.HSize, y, _scene.Camera.FieldOfView);
            _scene.Camera.Transform = cameraView;
        }
        else
        {
            Console.WriteLine("Must provide a valid integer as argument");
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
        TerminalGraphics.PrintScene(_scene);
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

    //returns the IDs of all of the objects in the world
    public void Objects(string[] args)
    {
        Console.WriteLine("IDs of all objects:");
        foreach (var obj in _scene.World.Objects)
        {
            Console.WriteLine(obj.ID);
        }

        if (args.Length > 1)
        {
            Console.WriteLine("Note: the \"objects\" command does not take any arguments");
        }
    }

    //returns the IDs of all of the selected objects in the world
    public void Selected(string[] args)
    {
        Console.WriteLine("IDs of currently selected objects:");
        foreach (var obj in _scene.Selected)
        {
            Console.WriteLine(obj.ID);
        }

        if (args.Length > 1)
        {
            Console.WriteLine("Note: the \"selected\" command does not take any arguments");
        }
    }

    public void ToggleVisualMode()
    {
        _visualMode = !_visualMode;
        if (_visualMode)
            Console.WriteLine("Visual Mode: ON");
        else
            Console.WriteLine("Visual Mode: OFF");
    }

    private void Select(string id)
    {
        if (_scene.World.IdToObject.TryGetValue(id, out IShape? obj))
        {
            _scene.Selected.Add(obj);
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
                catch (ArgumentException)
                {
                    Console.WriteLine($"No selected object with the ID \"{args[i]}\" was found.");
                }
            }
        }
    }

    //deselect overload with no arguments simply clears any selected objects
    private void Deselect()
    {
        _scene.Selected.Clear();
    }

    private void Deselect(string id)
    {
        if (_scene.World.IdToObject.TryGetValue(id, out IShape? obj))
        {
            if (!_scene.Selected.Contains(obj))
                throw new ArgumentException($"ID {id} was not found");
            _scene.Selected.Remove(obj);
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
                case "up":
                    MoveUp(distance);
                    break;
                case "down":
                    MoveDown(distance);
                    break;
                case "forward":
                    MoveForward(distance);
                    break;
                case "back":
                    MoveBack(distance);
                    break;
                default:
                    Console.WriteLine(
                        "direction not recognized. Valid directions for the move command are: left, right, down, up, forward, back and follow the \"move\" command."
                    );
                    break;
            }
        }
        else if (args.Length > 3) //If there are more than three arguments it is assumed that the arguments preceding direction and distance are the ids of objects to be moved
        {
            string direction = args[args.Length - 2];
            string distance = args[args.Length - 1];
            string[] directionAndDistance = { "move", direction, distance };
            //save a deep copy of the currectly selected objects
            List<IShape> currentlySelected = new();
            foreach (var obj in _scene.Selected)
            {
                currentlySelected.Add(obj);
            }
            // deselect current and select the new objects specified in the arguments
            Deselect();
            for (int i = 1; i < args.Length - 2; i++)
            {
                try
                {
                    Select(args[i]);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"The ID {args[i]} does not exist");
                }
            }
            //call move with the direction and distance as arguments
            Move(directionAndDistance);
            _scene.Selected = currentlySelected;
            //prevents preview from being run if visual mode is activated.
            return;
        }
        else
        {
            Console.WriteLine(
                "The move command must be followed by a direction (up, down, left, right, forward, back) AND THEN a distance (a double value)"
            );
            //prevents preview from being run if visual mode is activated.
            return;
        }
        //automatically call preview if visualMode is activated
        if (_visualMode)
            Preview();
    }

    private void MoveLeft(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, distance, 0, 0)
                * obj.TransformMatrix;
        }
    }

    private void MoveRight(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, -distance, 0, 0)
                * obj.TransformMatrix;
        }
    }

    private void MoveUp(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, 0, distance, 0)
                * obj.TransformMatrix;
        }
    }

    private void MoveDown(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, 0, -distance, 0)
                * obj.TransformMatrix;
        }
    }

    private void MoveForward(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, 0, 0, distance)
                * obj.TransformMatrix;
        }
    }

    private void MoveBack(double distance)
    {
        foreach (IShape obj in _scene.Selected)
        {
            obj.TransformMatrix =
                Matrix.CameraRelativeTranslation(_scene.Camera, 0, 0, -distance)
                * obj.TransformMatrix;
        }
    }

    public void Rotate(string[] args)
    {
        // if there are only three arguments passed in, one is assumed to be the "rotate" command itself,
        // the next to be the direction relative to the camera frame, and the last to be the angle. Currently selected objects will be rotated.
        if (args.Length == 3)
        {
            string direction = args[1];
            double angle = double.Parse(args[2]);
            switch (direction)
            {
                case "left":
                    RotateLeft(angle);
                    break;
                case "right":
                    RotateRight(angle);
                    break;
                case "up":
                    RotateUp(angle);
                    break;
                case "down":
                    RotateDown(angle);
                    break;
                case "forward":
                    RotateForward(angle);
                    break;
                case "back":
                    RotateBack(angle);
                    break;
                default:
                    Console.WriteLine(
                        "direction not recognized. Valid directions for the move command are: left, right, down, up, forward, back and follow the \"move\" command."
                    );
                    break;
            }
        }
        else if (args.Length > 3) //If there are more than three arguments it is assumed that the arguments preceding direction and distance are the ids of objects to be moved
        {
            string direction = args[args.Length - 2];
            string angle = args[args.Length - 1];
            string[] directionAndDistance = { "move", direction, angle };
            //save a deep copy of the currectly selected objects
            List<IShape> currentlySelected = new();
            foreach (var obj in _scene.Selected)
            {
                currentlySelected.Add(obj);
            }
            // deselect current and select the new objects specified in the arguments
            Deselect();
            for (int i = 1; i < args.Length - 2; i++)
            {
                try
                {
                    Select(args[i]);
                }
                catch (ArgumentException)
                {
                    Console.WriteLine($"The ID {args[i]} does not exist");
                }
            }
            //call move with the direction and distance as arguments
            Move(directionAndDistance);
            //reassign Selected to the objects which were selected before the rotation
            _scene.Selected = currentlySelected;
            //return prevents preview from being run if visual mode is activated.
            return;
        }
        else
        {
            Console.WriteLine(
                "The move command must be followed by a direction (up, down, left, right, forward, back) AND THEN a distance (a double value)"
            );
            //prevents preview from being run if visual mode is activated.
            return;
        }
        //automatically call preview if visualMode is activated
        if (_visualMode)
            Preview();
    }

    private void RotateLeft(double angle)
    {
        foreach (IShape obj in _scene.Selected)
        {
            Matrix rotation = Matrix.CameraRelativeRotationX(_scene.Camera, -angle);
            Point center = (obj.TransformMatrix * obj.Center).ToPoint();
            Matrix moveToOrgin = Matrix.Translation(-center.X, -center.Y, -center.Z);
            Matrix moveToOrignalPosition = Matrix.Translation(center.X, center.Y, center.Z);

            obj.TransformMatrix =
                moveToOrignalPosition * rotation * moveToOrgin * obj.TransformMatrix;
        }
    }

    private void RotateRight(double angle)
    {
        foreach (IShape obj in _scene.Selected) { }
    }

    private void RotateForward(double angle)
    {
        foreach (IShape obj in _scene.Selected) { }
    }

    private void RotateBack(double angle)
    {
        foreach (IShape obj in _scene.Selected) { }
    }

    private void RotateUp(double angle)
    {
        foreach (IShape obj in _scene.Selected) { }
    }

    private void RotateDown(double angle)
    {
        foreach (IShape obj in _scene.Selected) { }
    }

    public void CameraDolly(string[] args)
    {
        if (args.Length == 3)
        {
            string direction = args[1];
            double distance = double.Parse(args[2]);
            switch (direction)
            {
                case "left":
                    DollyLeft(distance);
                    break;
                case "right":
                    DollyRight(distance);
                    break;
                case "up":
                    DollyUp(distance);
                    break;
                case "down":
                    DollyDown(distance);
                    break;
                case "forward":
                    DollyForward(distance);
                    break;
                case "back":
                    DollyBack(distance);
                    break;
                default:
                    Console.WriteLine(
                        "direction not recognized. Valid directions for the move command are: left, right, down, up, forward, back and follow the \"move\" command."
                    );
                    break;
            }
        }
        else
        {
            Console.WriteLine(
                "The \"dolly\" command must be followed by a direction (up, down, left, right, forward, back) AND THEN a distance (a double value)"
            );
            //prevents preview from being run if visual mode is activated.
            return;
        }
        //automatically call preview if visualMode is activated
        if (_visualMode)
            Preview();
    }

    private void DollyForward(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(0, 0, distance) * _scene.Camera.Transform;
    }

    private void DollyBack(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(0, 0, -distance) * _scene.Camera.Transform;
    }

    private void DollyLeft(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(-distance, 0, 0) * _scene.Camera.Transform;
    }

    private void DollyRight(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(distance, 0, 0) * _scene.Camera.Transform;
    }

    private void DollyUp(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(0, -distance, 0) * _scene.Camera.Transform;
    }

    private void DollyDown(double distance)
    {
        _scene.Camera.Transform = Matrix.Translation(0, distance, 0) * _scene.Camera.Transform;
    }
}
