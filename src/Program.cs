using System.CommandLine;

internal class Program
{
    public async static Task<int> Main(string[] args)
    {
        Application app = new Application();

        return await app.InvokeAsync(args);
    }
}
