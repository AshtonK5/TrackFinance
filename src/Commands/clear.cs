
using System.CommandLine;

public class ClearCommand : Command
{
    public Option<bool> bShouldClearTransactionsOption;

    public ClearCommand() : base(":clear", "Clears the current console")
    {
        bShouldClearTransactionsOption = new Option<bool>("-s", "Clears the budget data sheet");
        bShouldClearTransactionsOption.AddAlias("--sheet");

        AddOption(bShouldClearTransactionsOption);
        this.SetHandler((bShouldClearBudgetSheet) =>
        {
            if (bShouldClearBudgetSheet == true)
            {
                if (Application.CurrentBudgetSheet != null)
                {
                    Application.LogInfo("Clearing Transactions...", ELogVerbosity.Info, true);
                    Application.CurrentBudgetSheet.ClearTransactions();
                }
                else
                {
                    throw new BudgetSheetNullException();
                }
            }
            else
            {
                Console.Clear();
            }

        }, bShouldClearTransactionsOption);
    }
}
