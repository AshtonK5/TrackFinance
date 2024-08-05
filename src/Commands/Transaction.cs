
using System.CommandLine;

public class TransactionCommand : Command
{
    public Argument<ETransactionType> TypeArgument;

    public Argument<double> AmountArgument;

    public Argument<string> NameArgument;

    public Argument<string> CategoryArgument;

    public Argument<string> AccountArgument;

    public TransactionCommand(string name, string? desc) : base(name, desc)
    {
        TypeArgument = new Argument<ETransactionType>("Type", getDefaultValue: () => ETransactionType.Expense);
        AmountArgument = new Argument<double>("Amount", getDefaultValue: () => 0);
        NameArgument = new Argument<string>("Transaction");
        CategoryArgument = new Argument<string>("Category", getDefaultValue: () => string.Empty);
        AccountArgument = new Argument<string>("Account", getDefaultValue: () => string.Empty);

        AddArgument(TypeArgument);
        AddArgument(AmountArgument);
        AddArgument(NameArgument);
        AddArgument(CategoryArgument);
        AddArgument(AccountArgument);
    }
}
