
using System.CommandLine;

public class DeleteCommand : Command
{
    public Argument<string> GuidArgument { get; private set; }

    public DeleteCommand() : base("del", "Remove a transaction")
    {
        GuidArgument = new Argument<string>("guid", "Global unique identifier for transaction");
        AddArgument(GuidArgument);

        this.SetHandler((guid) =>
        {
            if (Application.CurrentBudgetSheet != null)
            {
                Transaction? transaction = Application.CurrentBudgetSheet.DeleteTransaction(guid);
                if (transaction != null)
                {
                    Application.LogInfo($"Deleted The [\"{transaction.Value.Name}\"] Transaction", ELogVerbosity.Info, true);
                }
            }
            else
            {
                throw new BudgetSheetNullException();
            }

        }, GuidArgument);
    }
}
