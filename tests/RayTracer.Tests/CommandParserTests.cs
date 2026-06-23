namespace RayTracer.Tests;

using RayTracer.Cli;

public class CommandParserTests()
{
    [Fact]
    public void Tokenize_CommandIncludesMultipleSpacesNewLinesAndTabs_TokensAreExpected()
    {
        string[] expected = { "command", "arg1", "--flag", "arg2" };

        string command = "command      arg1  \t--flag\n arg2";

        string[] tokenized = CommandParser.Tokenize(command);

        Assert.Equal(expected[0], tokenized[0]);
        Assert.Equal(expected[1], tokenized[1]);
        Assert.Equal(expected[2], tokenized[2]);
        Assert.Equal(expected[3], tokenized[3]);
    }
}
