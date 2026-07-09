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
        string[] args = Tokenize(cmd);

        switch (args[0])
        {
            case "render":
                Exec.Render(args);
                break;
            case "preview":
                Exec.Preview(args);
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
            case "move":
                try
                {
                    Exec.Move(args);
                }
                catch (FormatException)
                {
                    Console.WriteLine(
                        "Could not parse distance, it should go at the end as the last argument of the move command"
                    );
                }
                break;
        }
    }

    public static string[] Tokenize(string cmd)
    {
        char[] delimiters = { ' ', '\t', '\n' };
        string[] tokens = cmd.Split(
            delimiters,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );
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
