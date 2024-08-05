using System.CommandLine;

public class NewCommand : Command
{
    public Argument<string> NameArgument { get; private set; }
    public Argument<string> PathArgument { get; private set; }

    public NewCommand() : base("new", "Create a new Budget Sheet")
    {
        NameArgument = new Argument<string>("name");
        PathArgument = new Argument<string>("path", getDefaultValue: () => ".");

        AddArgument(NameArgument);
        AddArgument(PathArgument);

        this.SetHandler((name, path) =>
        {
            BudgetSheet budgetSheet = new BudgetSheet(name, Path.GetFullPath(path));
            budgetSheet.Create();

        }, NameArgument, PathArgument);
    }
}
