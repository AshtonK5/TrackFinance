using System.CommandLine;

public class AddCommand : TransactionCommand
{

    public AddCommand() : base("add", "Add a transaction")
    {
        this.SetHandler((type, amt, name, acc, category) =>
        {
            Transaction currentTransaction = new Transaction(
                type,
                amt,
                name,
                category,
                acc
            );

#if DEBUG
            Application.LogInfo($"[Type] {currentTransaction.Type.ToString()}", ELogVerbosity.Info, false);
            Application.LogInfo($"[Amount] {currentTransaction.Amount.ToString()}", ELogVerbosity.Info, false);
            Application.LogInfo($"[Name] {currentTransaction.Name}", ELogVerbosity.Info, false);
            Application.LogInfo($"[Account] {currentTransaction.Account}", ELogVerbosity.Info, false);
            Application.LogInfo($"[Category] {currentTransaction.Category}", ELogVerbosity.Info, false);
            Application.LogInfo($"[GUID] {currentTransaction.ID}", ELogVerbosity.Info, false);
            Application.LogInfo("====================", ELogVerbosity.Info, false);
#endif

            if (Application.CurrentBudgetSheet != null)
            {
                Application.CurrentBudgetSheet.CreateTransaction(ref currentTransaction);
            }
            else
            {
                throw new BudgetSheetNullException();
            }

        }, TypeArgument, AmountArgument, NameArgument, AccountArgument, CategoryArgument);
    }

}
