
using System.Data;

public class BudgetSheetNullException : NoNullAllowedException
{
    public BudgetSheetNullException() : base($"Current Budget Sheet Null Exception! [Path]: {Application.FilePath}")
    {

    }

}
