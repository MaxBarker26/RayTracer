namespace RayTracer.Cli;

public class CommandParser
{
    private Scene _scene;
    private Executor _exec;

    public CommandParser()
    {
        _scene = new();
        _exec = new(_scene);
    }

    public void Parse(string cmd)
    {
        string[] args = Tokenize(cmd);

        switch (args[0])
        {
            case "render":
                _exec.Render(args);
                break;
            case "preview":
                _exec.Preview(args);
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
        foreach (string token in tokens)
        {
            token.ToLowerInvariant();
        }
        return tokens;
    }
}
