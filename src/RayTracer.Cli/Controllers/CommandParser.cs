namespace RayTracer.Cli;

public class CommandParser
{
    private Executor Exec;

    public CommandParser()
    {
        Scene scene = new();
        Exec = new(scene);
    }

    public void Parse(string cmd)
    {
        string[] args = Tokenize(cmd, out string[] originalCaseArgs);

        switch (args[0])
        {
            case "render":
                Exec.Render(originalCaseArgs);
                break;
            case "preview":
                Exec.Preview(args);
                break;
            case "visual":
                Exec.ToggleVisualMode();
                break;
            case "select":
                Exec.Select(args);
                break;
            case "selected":
                Exec.Selected(args);
                break;
            case "objects":
                Exec.Objects(args);
                break;
            case "deselect":
                Exec.Deselect(args);
                break;
            //object transformations
            case "move":
                try
                {
                    Exec.Move(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Could not parse distance, it should go at the end as the last argument of the move command and be a valid double."
                    );
                }
                break;
            case "rotate":
                try
                {
                    Exec.Rotate(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Could not parse rotation, it should go at the end as the last argument of the move command and be a valid double."
                    );
                }
                break;
            case "stretch":
                try
                {
                    Exec.Stretch(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Unable to parse the plane on which the object is to be stretched. Please include x, y, or z as the second to last argument to the \"stretch\" command."
                    );
                }
                break;
            case "scale":
                try
                {
                    Exec.Scale(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Unable to parse the fator by which the object(s) is/are to be scaled please place a double as the last argument to the \"scale\" command"
                    );
                }
                break;
            case "viewx":
                Exec.SetViewX(args);
                break;
            case "viewy":
                Exec.SetViewY(args);
                break;
            // camera movements
            case "dolly":
                try
                {
                    Exec.CameraDolly(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Could not parse distance, it should go at the end as the last argument of the move command and be a valid double."
                    );
                }
                break;
            case "tilt":
                break;
            case "pan":
                break;
            case "zoom":
                break;
            default:
                Console.WriteLine($"Unable to parse command \"{args[0]}\"");
                break;
        }
    }

    public static string[] Tokenize(string cmd, out string[] originalCaseArgs)
    {
        char[] delimiters = { ' ', '\t', '\n' };
        string[] tokens = cmd.Split(
            delimiters,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );
        originalCaseArgs = tokens;
        //string immutability, lowercased tokens must be saved in a new array
        string[] lowerTokens = new string[tokens.Length];
        int i = 0;
        foreach (string token in tokens)
        {
            lowerTokens[i] = token.ToLowerInvariant();
            i++;
        }
        return lowerTokens;
    }
}
