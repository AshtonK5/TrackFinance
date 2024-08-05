using System.CommandLine;

public enum ELogVerbosity
{
    Normal,
    Info,
    Warn,
    Success,
    Error
}

public class Application : RootCommand, IDisposable
{
    public static bool bIsRunning { get; private set; } = false;

    public static string FilePath { get; private set; } = string.Empty;

    public static BudgetSheet? CurrentBudgetSheet { get; private set; }

    private Option<string> m_PathOption;

    public Application() : base("A simple command line application that allows users to display and track their finances.")
    {
        m_PathOption = new Option<string>("-o", "Path option relative to budget sheet");
        m_PathOption.AddAlias("--open");

        AddOption(m_PathOption);

        AddCommand(new NewCommand());
        AddCommand(new AddCommand());
        AddCommand(new DeleteCommand());
        AddCommand(new ShowCommand());
        AddCommand(new ClearCommand());
        AddCommand(new QuitCommand(this));

        this.SetHandler((string fileOption) =>
        {
            if (!string.IsNullOrEmpty(fileOption))
            {
                if (File.Exists(fileOption) && Path.GetExtension(fileOption) == BudgetSheet.FileExtensionName)
                {
                    FilePath = fileOption;
                    CurrentBudgetSheet = BudgetSheet.ReadFormat(FilePath);
                }
            }

            RunApplication();

        }, m_PathOption);
    }

    public void RunApplication()
    {
        bIsRunning = true;
        //LogInfo(File.ReadAllText("C:\\Users\\ashto\\Desktop\\TrackFinance\\examples\\TableFormat.txt"), ELogVerbosity.Info, true);
        if (!string.IsNullOrEmpty(FilePath))
            LogInfo($"Running application with file path: {FilePath}", ELogVerbosity.Info, false);

        while (bIsRunning)
        {
#if DEBUG
            DrawDebugInfo();
#endif
            LogInput();
            string? commandLine = Console.ReadLine();

            if (commandLine != null)
            {
                this.InvokeAsync(commandLine);
            }
        }

        Dispose();
    }

#if DEBUG
    private static void DrawDebugInfo()
    {
        const string debugMessage = "========== (DEBUG MODE) ==========";
        var lastCursorPos = Console.GetCursorPosition();
        int leftPosition = Console.WindowWidth - debugMessage.Length;

        // Ensure the cursor doesn't go out of bounds
        if (leftPosition < 0)
        {
            leftPosition = 0;
        }

        Console.SetCursorPosition(leftPosition, Console.WindowHeight - 1);
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(debugMessage);
        Console.ResetColor();
        Console.SetCursorPosition(lastCursorPos.Left, lastCursorPos.Top);
    }
#endif

    public static void LogInfo(string msg, ELogVerbosity verbosity, bool bShouldLogSpace = false)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;

        switch (verbosity)
        {
            case (ELogVerbosity.Normal):
            {
                foregroundColor = ConsoleColor.Gray;
                break;
            }
            case (ELogVerbosity.Info):
                {
                    foregroundColor = ConsoleColor.DarkGray;
                    break;
                }

            case (ELogVerbosity.Success):
                {
                    foregroundColor = ConsoleColor.Green;
                    break;
                }

            case (ELogVerbosity.Warn):
                {
                    foregroundColor = ConsoleColor.DarkYellow;
                    break;
                }

            case (ELogVerbosity.Error):
                {
                    foregroundColor = ConsoleColor.Red;
                    break;
                }
        }

        Console.ForegroundColor = foregroundColor;
        Console.WriteLine(msg);
        Console.ResetColor();

        if (bShouldLogSpace)
            LogEmptyLine();
    }

    public static void LogEmptyLine()
    {
        Console.WriteLine(string.Empty);
    }

    public static void LogInput(string? msg = null)
    {
        Console.Write($"[TrackFinance] -> {(msg != null ? msg : string.Empty)}");
    }

    public void Dispose()
    {
        bIsRunning = false;
    }
}
