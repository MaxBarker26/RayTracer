namespace RayTracer.Cli;

public class CommandParser
{
    private Scene _scene;

    public CommandParser(Scene s)
    {
        _scene = s;
    }

    public void Parse(string cmd)
    {
        string[] tokens = Tokenize(cmd);

        //switch (tokens[0]):
        // case "Render"
    }

    public static string[] Tokenize(string cmd)
    {
        char[] delimiters = { ' ', '\t', '\n' };
        string[] tokens = cmd.Split(
            delimiters,
            StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries
        );
        return tokens;
    }
}
