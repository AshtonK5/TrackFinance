using System.CommandLine;

public class QuitCommand : Command
{
    private Application m_AppContext;

    public QuitCommand(Application appContext) : base(":quit", "Halts the current application from execution")
    {
        m_AppContext = appContext;

        AddAlias(":q");
        AddAlias(":exit");
        AddAlias(":e");

        this.SetHandler(Run);
    }

    private void Run()
    {
        m_AppContext.Dispose();

    }
}
